using System;
using System.Collections.Generic;
using System.Threading;

namespace FluffyUnderware.DevTools
{
	public class ThreadPoolWorker<T> : IDisposable
	{
		private readonly SimplePool<QueuedCallback> queuedCallbackPool = new SimplePool<QueuedCallback>(4);

		private readonly SimplePool<LoopState<T>> loopStatePool = new SimplePool<LoopState<T>>(4);

		private int _remainingWorkItems = 1;

		private ManualResetEvent _done = new ManualResetEvent(initialState: false);

		private WaitCallback handleWorkItemCallBack;

		private WaitCallback handleLoopCallBack;

		public ThreadPoolWorker()
		{
			handleWorkItemCallBack = delegate(object o)
			{
				QueuedCallback queuedCallback = (QueuedCallback)o;
				try
				{
					queuedCallback.Callback(queuedCallback.State);
				}
				finally
				{
					lock (queuedCallbackPool)
					{
						queuedCallbackPool.ReleaseItem(queuedCallback);
					}
					DoneWorkItem();
				}
			};
			handleLoopCallBack = delegate(object state)
			{
				LoopState<T> loopState = (LoopState<T>)state;
				for (int i = loopState.StartIndex; i <= loopState.EndIndex; i++)
				{
					loopState.Action(loopState.Items[i]);
				}
				lock (loopStatePool)
				{
					loopStatePool.ReleaseItem(loopState);
				}
			};
		}

		public void ParralelFor(Action<T> action, List<T> list)
		{
			ThreadPool.GetAvailableThreads(out var workerThreads, out var _);
			int num = 1 + Math.Min(workerThreads, Environment.ProcessorCount - 1);
			int count = list.Count;
			int num2 = ((num == 1) ? count : ((int)Math.Ceiling((float)count / (float)num)));
			int num3 = 0;
			while (num3 < count)
			{
				int num4 = Math.Min(num3 + num2 - 1, count - 1);
				if (num4 == count - 1)
				{
					for (int i = num3; i <= num4; i++)
					{
						action(list[i]);
					}
				}
				else
				{
					QueuedCallback item;
					lock (queuedCallbackPool)
					{
						item = queuedCallbackPool.GetItem();
					}
					LoopState<T> item2;
					lock (loopStatePool)
					{
						item2 = loopStatePool.GetItem();
					}
					item2.StartIndex = (short)num3;
					item2.EndIndex = (short)num4;
					item2.Action = action;
					item2.Items = list;
					item.State = item2;
					item.Callback = handleLoopCallBack;
					ThrowIfDisposed();
					lock (_done)
					{
						_remainingWorkItems++;
					}
					ThreadPool.QueueUserWorkItem(handleWorkItemCallBack, item);
				}
				num3 = num4 + 1;
			}
			WaitAll(-1, exitContext: false);
		}

		private bool WaitAll(int millisecondsTimeout, bool exitContext)
		{
			ThrowIfDisposed();
			DoneWorkItem();
			bool flag = _done.WaitOne(millisecondsTimeout, exitContext);
			lock (_done)
			{
				if (flag)
				{
					_remainingWorkItems = 1;
					_done.Reset();
				}
				else
				{
					_remainingWorkItems++;
				}
			}
			return flag;
		}

		private void ThrowIfDisposed()
		{
			if (_done == null)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}

		private void DoneWorkItem()
		{
			lock (_done)
			{
				_remainingWorkItems--;
				if (_remainingWorkItems == 0)
				{
					_done.Set();
				}
			}
		}

		public void Dispose()
		{
			if (_done != null)
			{
				((IDisposable)_done).Dispose();
				_done = null;
			}
		}
	}
	public class ThreadPoolWorker : IDisposable
	{
		private int _remainingWorkItems = 1;

		private ManualResetEvent _done = new ManualResetEvent(initialState: false);

		public void QueueWorkItem(WaitCallback callback)
		{
			QueueWorkItem(callback, null);
		}

		public void QueueWorkItem(Action act)
		{
			QueueWorkItem(act, null);
		}

		public void ParralelFor<T>(Action<T> action, List<T> list)
		{
			ThreadPool.GetAvailableThreads(out var workerThreads, out var _);
			int num = 1 + Math.Min(workerThreads, Environment.ProcessorCount - 1);
			int count = list.Count;
			if (num == 1 || count == 1)
			{
				for (int i = 0; i < count; i++)
				{
					action(list[i]);
				}
				return;
			}
			int num2 = (int)Math.Ceiling((float)count / (float)num);
			int num3 = 0;
			while (num3 < count)
			{
				QueuedCallback queuedCallback = new QueuedCallback();
				int num4 = Math.Min(num3 + num2, count - 1);
				LoopState<T> loopState = new LoopState<T>();
				loopState.StartIndex = (short)num3;
				loopState.EndIndex = (short)num4;
				loopState.Action = action;
				loopState.Items = list;
				queuedCallback.State = loopState;
				queuedCallback.Callback = delegate(object state)
				{
					LoopState<T> loopState2 = (LoopState<T>)state;
					for (int j = loopState2.StartIndex; j <= loopState2.EndIndex; j++)
					{
						loopState2.Action(loopState2.Items[j]);
					}
				};
				QueueWorkItem(queuedCallback);
				num3 = num4 + 1;
			}
		}

		private void QueueWorkItem(QueuedCallback callback)
		{
			ThrowIfDisposed();
			lock (_done)
			{
				_remainingWorkItems++;
			}
			ThreadPool.QueueUserWorkItem(HandleWorkItem, callback);
		}

		public void QueueWorkItem(WaitCallback callback, object state)
		{
			QueuedCallback queuedCallback = new QueuedCallback();
			queuedCallback.Callback = callback;
			queuedCallback.State = state;
			QueueWorkItem(queuedCallback);
		}

		public void QueueWorkItem(Action act, object state)
		{
			QueuedCallback queuedCallback = new QueuedCallback();
			queuedCallback.Callback = delegate
			{
				act();
			};
			queuedCallback.State = state;
			QueueWorkItem(queuedCallback);
		}

		public bool WaitAll()
		{
			return WaitAll(-1, exitContext: false);
		}

		public bool WaitAll(TimeSpan timeout, bool exitContext)
		{
			return WaitAll((int)timeout.TotalMilliseconds, exitContext);
		}

		public bool WaitAll(int millisecondsTimeout, bool exitContext)
		{
			ThrowIfDisposed();
			DoneWorkItem();
			bool flag = _done.WaitOne(millisecondsTimeout, exitContext);
			lock (_done)
			{
				if (flag)
				{
					_remainingWorkItems = 1;
					_done.Reset();
				}
				else
				{
					_remainingWorkItems++;
				}
			}
			return flag;
		}

		private void HandleWorkItem(object state)
		{
			QueuedCallback queuedCallback = (QueuedCallback)state;
			try
			{
				queuedCallback.Callback(queuedCallback.State);
			}
			finally
			{
				DoneWorkItem();
			}
		}

		private void DoneWorkItem()
		{
			lock (_done)
			{
				_remainingWorkItems--;
				if (_remainingWorkItems == 0)
				{
					_done.Set();
				}
			}
		}

		private void ThrowIfDisposed()
		{
			if (_done == null)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}

		public void Dispose()
		{
			if (_done != null)
			{
				((IDisposable)_done).Dispose();
				_done = null;
			}
		}
	}
}
