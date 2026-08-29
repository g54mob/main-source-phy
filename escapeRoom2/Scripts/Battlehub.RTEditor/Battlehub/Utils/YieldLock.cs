using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Battlehub.Utils
{
	public class YieldLock : CustomYieldInstruction, IDisposable
	{
		private Lock.LockReleaser m_releaser;

		private Task<Lock.LockReleaser> m_task;

		public override bool keepWaiting
		{
			get
			{
				if (m_task == null)
				{
					return false;
				}
				if (!m_task.IsCompleted)
				{
					return true;
				}
				if (m_task.IsFaulted)
				{
					throw m_task.Exception;
				}
				m_releaser = m_task.Result;
				return false;
			}
		}

		public YieldLock(Task<Lock.LockReleaser> task)
		{
			m_task = task;
		}

		public void Dispose()
		{
			m_releaser.Dispose();
		}
	}
}
