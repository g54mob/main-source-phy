using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABS.GameState
{
	public class TaskSequencer
	{
		public delegate void OnSequenceEventDelegate();

		private List<TaskSequence> m_sequence = new List<TaskSequence>();

		private MonoBehaviour m_coroutineContext;

		public OnSequenceEventDelegate OnStartedSequence;

		public OnSequenceEventDelegate OnFinishedSequence;

		private bool m_isRunning;

		private int m_currentIndex;

		public bool IsRunning => m_isRunning;

		public TaskSequencer(MonoBehaviour coroutineContext)
		{
			m_coroutineContext = coroutineContext;
		}

		public void AddTask(TaskSequence sequence)
		{
			m_sequence.Add(sequence);
		}

		public void RunSequence()
		{
			m_coroutineContext.StartCoroutine(RunSequenceAsync());
		}

		public void AbortSqeuence()
		{
			m_coroutineContext.StopAllCoroutines();
		}

		private IEnumerator RunSequenceAsync()
		{
			m_isRunning = true;
			OnStartedSequence?.Invoke();
			for (TaskSequence currentTask = GetNextTask(); currentTask != null; currentTask = GetNextTask())
			{
				currentTask.IsRunning = true;
				while (currentTask.IsRunning)
				{
					yield return currentTask.Execute();
				}
			}
			OnFinishedSequence?.Invoke();
			Reset();
			m_isRunning = false;
		}

		private TaskSequence GetNextTask()
		{
			if (m_currentIndex >= m_sequence.Count)
			{
				return null;
			}
			TaskSequence result = m_sequence[m_currentIndex];
			m_currentIndex++;
			return result;
		}

		public void Reset()
		{
			m_currentIndex = 0;
		}
	}
}
