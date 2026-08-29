using System;
using System.Threading;
using System.Threading.Tasks;

namespace Battlehub.Utils
{
	public class Lock
	{
		public struct LockReleaser : IDisposable
		{
			private readonly SemaphoreSlim m_toRelease;

			private Action m_callback;

			public LockReleaser(SemaphoreSlim toRelease, Action callback = null)
			{
				m_toRelease = toRelease;
				m_callback = callback;
			}

			public void Dispose()
			{
				m_callback?.Invoke();
				if (m_toRelease != null)
				{
					m_toRelease.Release();
				}
			}
		}

		private readonly SemaphoreSlim toLock;

		public Lock()
		{
			toLock = new SemaphoreSlim(1, 1);
		}

		public async Task<LockReleaser> Wait(Action lockedCallback = null, Action releasedCallback = null, CancellationToken ct = default(CancellationToken))
		{
			await toLock.WaitAsync(ct);
			lockedCallback?.Invoke();
			return new LockReleaser(toLock, releasedCallback);
		}
	}
}
