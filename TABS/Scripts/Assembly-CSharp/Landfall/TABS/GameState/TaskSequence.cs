using System.Collections;

namespace Landfall.TABS.GameState
{
	public class TaskSequence
	{
		public delegate IEnumerator TaskDelegate();

		private TaskDelegate m_task;

		public bool IsRunning { get; set; }

		public TaskDelegate Task => m_task;

		public TaskSequence(TaskDelegate task)
		{
			m_task = task;
		}

		public IEnumerator Execute()
		{
			yield return m_task();
			IsRunning = false;
		}
	}
}
