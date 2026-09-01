using System;

namespace MoreMountains.Tools
{
	[Serializable]
	public class AIState
	{
		public string StateName;

		[MMReorderableAttribute(null, "Action", null)]
		public AIActionsList Actions;

		[MMReorderableAttribute(null, "Transition", null)]
		public AITransitionsList Transitions;

		protected AIBrain _brain;

		public virtual void SetBrain(AIBrain brain)
		{
		}

		public virtual void EnterState()
		{
		}

		public virtual void ExitState()
		{
		}

		public virtual void PerformActions()
		{
		}

		public virtual void EvaluateTransitions()
		{
		}
	}
}
