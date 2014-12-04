using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Camp.HogRider
{
    public class MessageDispatcher
    {
        Queue<object> _queue = new Queue<object>();
        Dictionary<Type, Action<object>> _callbacks = new Dictionary<Type, Action<object>>();

        /// <summary>
        /// Push the specified message, thread-safe.
        /// </summary>
        /// <param name="message">Message.</param>
        public void Push(object message)
        {
            lock (_queue)
            {
                _queue.Enqueue(message);
            }
        }

        #region Registration

        public void RegisterHandlerHost(object host, bool inherit = true)
        {
            foreach (var m in host.GetType().GetMethods())
            {
                if (!Attribute.IsDefined(m, typeof(MessageHandlerAttribute), inherit))
                    continue;

                if (m.GetParameters().Length != 1)
                    throw new ArgumentException("Method '{0}' should accept and only accept 1 argument.", m.Name);

                Type messageType = m.GetParameters().Single().ParameterType;
                _callbacks[messageType] = o => m.Invoke(host, new [] { o });
            }
        }

        public void RegisterHandler<T>(Action<T> callback)
        {
            _callbacks[typeof(T)] = o => callback((T)o);
        }

        public void ClearAllRegistrations()
        {
            _callbacks.Clear();
        }
        #endregion Registration

        #region Dispatching

        /// <summary>
        /// Dispatchs the message immediately.
        /// </summary>
        /// <param name="message">Message.</param>
		public void DispatchMessage(object message)
		{
			Action<object> callback;
			if (_callbacks.TryGetValue(message.GetType(), out callback))
				callback(message);
			else
			{
				if (UnregisteredMessageReceived != null)
					UnregisteredMessageReceived(message);
			}
		}

        /// <summary>
        /// Gets single message in received queue, thread-safe.
        /// </summary>
        /// <returns>The message.</returns>
        /// <exception cref="InvalidOperationException">Throw while the queue is empty.</exception>
        public object Pop()
        {
            lock (_queue)
            {
                return _queue.Dequeue();
            }
        }

        /// <summary>
        /// Dispatch received messages to registered handlers, thread-safe.
        /// </summary>
        public void DispatchQueuedMessages()
        {
            object[] messages;
            lock (_queue)
            {
                int messageCount = _queue.Count;
                if (messageCount == 0)
                    return;

                messages = new object[messageCount];
                for (int i = 0; i < messageCount; i++)
                    messages[i] = _queue.Dequeue();
            }

            foreach (var m in messages)
            {
				DispatchMessage(m);
            }
        }

        /// <summary>
        /// Occurs when unregistered message received.
        /// </summary>
        public event Action<object> UnregisteredMessageReceived;

        #endregion Dispatching
    }
}

