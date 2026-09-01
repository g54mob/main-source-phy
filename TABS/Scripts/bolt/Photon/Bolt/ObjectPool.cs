using System.Collections.Generic;

namespace Photon.Bolt
{
	public class ObjectPool<T> where T : new()
	{
		private readonly Stack<T> _pool = new Stack<T>();

		public bool Available => _pool.Count > 0;

		public ObjectPool()
		{
			_pool = new Stack<T>();
		}

		public void Return(T obj)
		{
			_pool.Push(obj);
		}

		public T Get()
		{
			if (_pool.Count <= 0)
			{
				return new T();
			}
			return _pool.Pop();
		}
	}
}
