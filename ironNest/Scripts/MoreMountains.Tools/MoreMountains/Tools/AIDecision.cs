using UnityEngine;

namespace MoreMountains.Tools
{
	public abstract class AIDecision : MonoBehaviour
	{
		[Tooltip("a label you can set to organize your AI Decisions, not used by anything else")]
		public string Label;

		protected AIBrain _brain;

		public virtual bool DecisionInProgress { get; set; }

		public abstract bool Decide();

		protected virtual void Awake()
		{
		}

		public virtual void Initialization()
		{
		}

		public virtual void OnEnterState()
		{
		}

		public virtual void OnExitState()
		{
		}
	}
}
