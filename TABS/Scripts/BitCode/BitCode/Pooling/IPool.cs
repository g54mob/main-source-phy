using System;

namespace BitCode.Pooling
{
	public interface IPool
	{
		int TotalCount { get; }

		int AvailableCount { get; }

		IPoolable Get(Action<IPoolable> resetOverride = null);

		bool Contains(IPoolable pooledItem);

		void Return(IPoolable pooledItem);

		void ReturnAll();
	}
	public interface IPool<T> : IPool where T : IPoolable
	{
		T Get(Action<T> resetOverride = null);

		bool Contains(T pooledItem);

		void Return(T pooledItem);
	}
}
