using System;
using System.Collections.Concurrent;

namespace Poly.Base
{
	public class ConcurrentPool<T> : IDisposable
	{
		private ConcurrentStack<T> pool = new ConcurrentStack<T>();

		private Func<T> factoryFunction;

		public ConcurrentPool(Func<T> factoryFunction)
		{
			this.factoryFunction = factoryFunction;
		}

		public T Get()
		{
			if (!pool.TryPop(out var result))
			{
				return factoryFunction();
			}
			return result;
		}

		public void Release(T elem)
		{
			pool.Push(elem);
		}

		public void Dispose()
		{
			T result;
			while (pool.TryPop(out result))
			{
				(result as IDisposable)?.Dispose();
			}
		}
	}
}
