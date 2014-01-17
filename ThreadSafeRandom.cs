using System;
using System.Threading;

namespace LoganZhou.Boar
{
	static public class ThreadSafeRandom
	{
		private static readonly object _lock = new object();
		private static readonly Random _seedProvider = new Random();

		[ThreadStatic]
		private static Random _rand;

		public static Random Rand
		{
			get
			{
				if (_rand != null) 
				{
					return _rand;
				}
				lock (_lock)
				{
					if (_rand == null)
					{
						var seed = _seedProvider.Next ();
						_rand = new Random (seed);
					}
					return _rand;
				}
			}
		}
	}
}