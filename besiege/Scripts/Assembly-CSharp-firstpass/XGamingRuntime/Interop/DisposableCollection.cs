using System;
using System.Collections.Generic;

namespace XGamingRuntime.Interop
{
	public class DisposableCollection : IDisposable
	{
		private readonly List<IDisposable> disposables;

		public DisposableCollection()
		{
			disposables = new List<IDisposable>();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool isDisposing)
		{
			foreach (DisposableBuffer disposable in disposables)
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		~DisposableCollection()
		{
			Dispose(false);
		}

		public T Add<T>(T disposable) where T : IDisposable
		{
			disposables.Add(disposable);
			return disposable;
		}
	}
}
