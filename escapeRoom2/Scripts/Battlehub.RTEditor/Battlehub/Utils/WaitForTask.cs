using System.Threading.Tasks;
using UnityEngine;

namespace Battlehub.Utils
{
	public class WaitForTask : CustomYieldInstruction
	{
		private Task m_task;

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
				return false;
			}
		}

		public WaitForTask(Task task)
		{
			m_task = task;
		}
	}
}
