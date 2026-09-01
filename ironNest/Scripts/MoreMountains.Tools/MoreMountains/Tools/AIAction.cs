using UnityEngine;

namespace MoreMountains.Tools
{
	public abstract class AIAction : MonoBehaviour
	{
		public enum InitializationModes
		{
			EveryTime = 0,
			OnlyOnce = 1
		}

		public InitializationModes InitializationMode;

		protected bool _initialized;

		[Tooltip("a label you can set to organize your AI Actions, not used by anything else")]
		public string Label;

		protected AIBrain _brain;

		public virtual bool ActionInProgress { get; set; }

		protected virtual bool ShouldInitialize => false;

		public abstract void PerformAction();

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
