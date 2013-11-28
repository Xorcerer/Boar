using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoganZhou.Boar
{
	public static class TaskExtensions
	{
		public static Task<T> TimeoutAfter<T>(this Task<T> task, int millisecondsTimeout)
		{
			if (task.IsCompleted || millisecondsTimeout == Timeout.Infinite) {
				return task;
			}

			var tcs = new TaskCompletionSource<T> ();
			if (millisecondsTimeout == 0) {
				tcs.TrySetException (new TimeoutException ());
				return tcs.Task;
			}

			var timer = new Timer ((state) => {
				tcs.TrySetException (new TimeoutException ());
			}, null, millisecondsTimeout, Timeout.Infinite);

			task.ContinueWith ((source) => {
				timer.Dispose ();
				switch (source.Status) {
				case TaskStatus.Faulted:
					tcs.TrySetException (source.Exception);
					break;
				case TaskStatus.Canceled:
					tcs.TrySetCanceled ();
					break;
				case TaskStatus.RanToCompletion:
					tcs.TrySetResult (source == null ? default(T) : source.Result);
					break;
				}
			}, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

			return tcs.Task;
		}
		public static Task TimeoutAfter(this Task task, int millisecondsTimeout)
		{
			if (task.IsCompleted || millisecondsTimeout == Timeout.Infinite) {
				return task;
			}

			var tcs = new TaskCompletionSource<object> ();
			if (millisecondsTimeout == 0) {
				tcs.TrySetException (new TimeoutException ());
				return tcs.Task;
			}

			var timer = new Timer ((state) => {
				tcs.TrySetException (new TimeoutException ());
			}, null, millisecondsTimeout, Timeout.Infinite);

			task.ContinueWith ((source) => {
				timer.Dispose ();
				switch (source.Status) {
				case TaskStatus.Faulted:
					tcs.TrySetException (source.Exception);
					break;
				case TaskStatus.Canceled:
					tcs.TrySetCanceled ();
					break;
				case TaskStatus.RanToCompletion:
					tcs.TrySetResult (null);
					break;
				}
			}, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

			return tcs.Task;
		}
	}
}