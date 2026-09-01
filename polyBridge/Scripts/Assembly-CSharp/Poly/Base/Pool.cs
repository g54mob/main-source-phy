using System;
using System.Collections.Generic;

namespace Poly.Base
{
	public class Pool<T> : IDisposable
	{
		private Stack<T> pool = new Stack<T>();

		private Func<T> factoryFunction;

		public Pool(Func<T> factoryFunction)
		{
			this.factoryFunction = factoryFunction;
		}

		public T Get()
		{
			if (0 >= pool.Count)
			{
				return factoryFunction();
			}
			return pool.Pop();
		}

		public void Release(T elem)
		{
			pool.Push(elem);
		}

		public void Dispose()
		{
			while (0 < pool.Count)
			{
				(pool.Pop() as IDisposable)?.Dispose();
			}
		}
	}
}
