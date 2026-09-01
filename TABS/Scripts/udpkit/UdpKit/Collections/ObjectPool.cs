using System.Collections.Generic;

namespace UdpKit.Collections
{
	public class ObjectPool<T> where T : new()
	{
		private readonly object contextLock = new object();

		private readonly Stack<T> _pool = new Stack<T>();

		public bool Available => _pool.Count > 0;

		public ObjectPool()
		{
			_pool = new Stack<T>();
		}

		public void Return(T obj)
		{
			lock (contextLock)
			{
				_pool.Push(obj);
			}
		}

		public T Get()
		{
			lock (contextLock)
			{
				return (_pool.Count > 0) ? _pool.Pop() : new T();
			}
		}
	}
}
