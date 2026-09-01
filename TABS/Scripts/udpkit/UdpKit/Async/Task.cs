using System;
using System.Threading;

namespace UdpKit.Async
{
	internal class Task
	{
		private readonly Thread _thread;

		private readonly AutoResetEvent _event;

		private Action _action;

		private readonly object _locker = new object();

		private volatile bool _abort;

		public bool IsRunning => _action != null;

		public string Name => _thread.ManagedThreadId.ToString();

		public Task()
		{
			_abort = false;
			_event = new AutoResetEvent(initialState: false);
			_thread = new Thread(Runner)
			{
				IsBackground = true,
				Priority = ThreadPriority.AboveNormal
			};
			_thread.Start();
		}

		public void Run(Action callback)
		{
			lock (_locker)
			{
				_action = callback;
				_event.Set();
			}
		}

		public void Abort()
		{
			_abort = true;
			_event.Set();
		}

		public void ForceAbort()
		{
			_thread.Abort();
		}

		private void Runner()
		{
			try
			{
				while (!_abort)
				{
					_event.WaitOne();
					lock (_locker)
					{
						if (_action != null)
						{
							_action();
							_action = null;
						}
					}
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (ThreadInterruptedException)
			{
			}
		}
	}
}
