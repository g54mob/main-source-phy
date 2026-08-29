using System;
using System.Runtime.CompilerServices;

public struct OVRTaskBuilder<T>
{
	private interface IPooledStateMachine : IDisposable
	{
		OVRTask<T>? Task { get; set; }

		Action MoveNext { get; }
	}

	private class PooledStateMachine<TStateMachine> : IPooledStateMachine, IDisposable, OVRObjectPool.IPoolObject where TStateMachine : IAsyncStateMachine
	{
		public TStateMachine StateMachine;

		public OVRTask<T>? Task { get; set; }

		public Action MoveNext { get; }

		public static PooledStateMachine<TStateMachine> Get()
		{
			return OVRObjectPool.Get<PooledStateMachine<TStateMachine>>();
		}

		public void Dispose()
		{
			OVRObjectPool.Return(this);
		}

		public PooledStateMachine()
		{
			MoveNext = ExecuteMoveNext;
		}

		private void ExecuteMoveNext()
		{
			StateMachine.MoveNext();
		}

		void OVRObjectPool.IPoolObject.OnGet()
		{
			StateMachine = default(TStateMachine);
			Task = null;
		}

		void OVRObjectPool.IPoolObject.OnReturn()
		{
			StateMachine = default(TStateMachine);
			Task = null;
		}
	}

	private IPooledStateMachine _pooledStateMachine;

	private OVRTask<T>? _task;

	public OVRTask<T> Task
	{
		get
		{
			if (_task.HasValue)
			{
				return _task.Value;
			}
			OVRTask<T>? task;
			if (_pooledStateMachine != null)
			{
				IPooledStateMachine pooledStateMachine = _pooledStateMachine;
				task = pooledStateMachine.Task;
				OVRTask<T> valueOrDefault = task.GetValueOrDefault();
				OVRTask<T> value;
				if (!task.HasValue)
				{
					valueOrDefault = OVRTask.FromGuid<T>(Guid.NewGuid());
					OVRTask<T>? task2 = valueOrDefault;
					pooledStateMachine.Task = task2;
					value = valueOrDefault;
				}
				else
				{
					value = valueOrDefault;
				}
				task = (_task = value);
				return task.Value;
			}
			task = (_task = OVRTask.FromGuid<T>(Guid.NewGuid()));
			return task.Value;
		}
	}

	public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
	{
		IPooledStateMachine pooledStateMachine = GetPooledStateMachine<TStateMachine>();
		((PooledStateMachine<TStateMachine>)pooledStateMachine).StateMachine = stateMachine;
		Action moveNext = pooledStateMachine.MoveNext;
		awaiter.OnCompleted(moveNext);
	}

	public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
	{
		IPooledStateMachine pooledStateMachine = GetPooledStateMachine<TStateMachine>();
		((PooledStateMachine<TStateMachine>)pooledStateMachine).StateMachine = stateMachine;
		Action moveNext = pooledStateMachine.MoveNext;
		awaiter.UnsafeOnCompleted(moveNext);
	}

	public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
	{
		((PooledStateMachine<TStateMachine>)GetPooledStateMachine<TStateMachine>()).StateMachine = stateMachine;
		stateMachine.MoveNext();
	}

	public static OVRTaskBuilder<T> Create()
	{
		return default(OVRTaskBuilder<T>);
	}

	private IPooledStateMachine GetPooledStateMachine<TStateMachine>() where TStateMachine : IAsyncStateMachine
	{
		if (_pooledStateMachine == null)
		{
			_pooledStateMachine = PooledStateMachine<TStateMachine>.Get();
			_pooledStateMachine.Task = _task;
		}
		return _pooledStateMachine;
	}

	public void SetException(Exception exception)
	{
		Task.SetException(exception);
		_pooledStateMachine?.Dispose();
		_pooledStateMachine = null;
	}

	public void SetResult(T result)
	{
		Task.SetResult(result);
		_pooledStateMachine?.Dispose();
		_pooledStateMachine = null;
	}

	public void SetStateMachine(IAsyncStateMachine stateMachine)
	{
	}
}
