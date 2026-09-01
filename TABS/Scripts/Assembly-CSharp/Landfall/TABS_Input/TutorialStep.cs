using System;
using System.Collections.Generic;
using InControl;
using UnityEngine;

namespace Landfall.TABS_Input
{
	public class TutorialStep
	{
		private List<PlayerAction> m_Bindings;

		private TutorialSteps m_Step;

		private TutorialSteps m_BlockingStep;

		private int m_CompletedStep;

		private StepType m_StepType;

		public bool Completed { get; private set; }

		public TutorialStep(Action<Action> completeAction, TutorialSteps step, TutorialSteps blockingStep = TutorialSteps.None)
		{
			completeAction(Complete);
			m_Step = step;
			m_BlockingStep = blockingStep;
			m_StepType = StepType.Action;
		}

		public TutorialStep(PlayerAction[] bindings, TutorialSteps step, TutorialSteps blockingStep = TutorialSteps.None)
		{
			Debug.Log("New TutorialStep Added with: " + bindings.Length + " Bindings!");
			m_Bindings = new List<PlayerAction>();
			foreach (PlayerAction item in bindings)
			{
				m_Bindings.Add(item);
			}
			m_Step = step;
			m_BlockingStep = blockingStep;
			m_StepType = StepType.Key;
		}

		private void Complete()
		{
			Debug.Log("Completed Tutorial Step: " + m_Step);
			Completed = true;
		}

		public void Check(Dictionary<TutorialSteps, TutorialStep> m_TutorialSteps)
		{
			if (Completed || (m_BlockingStep != TutorialSteps.None && !m_TutorialSteps[m_BlockingStep].Completed) || m_StepType != StepType.Key)
			{
				return;
			}
			for (int num = m_Bindings.Count - 1; num >= 0; num--)
			{
				if (m_Bindings[num].WasPressed)
				{
					m_CompletedStep++;
					m_Bindings.RemoveAt(num);
				}
			}
			if (m_Bindings.Count == 0 || m_CompletedStep >= 2)
			{
				Complete();
			}
		}
	}
}
