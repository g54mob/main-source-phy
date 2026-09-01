using System;

namespace SleepyNodes
{
	[Serializable]
	public abstract class EventNode : StateNode
	{
		public class EventData
		{
		}

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public bool OnlyOnce;

		public bool EnableOnStart;

		[NonSerialized]
		private bool AlreadyTriggered;

		[NonSerialized]
		private bool EventEnabled;

		public override void ResetNode()
		{
		}

		public bool CheckShouldRun(EventData data)
		{
			return false;
		}

		protected abstract bool ShouldRun(EventData data);

		public virtual void Run(NodeExecutionState state)
		{
		}

		public sealed override void OnEnter(NodeExecutionState state)
		{
		}

		public sealed override void OnExit(NodeExecutionState state, StateNode To, string alternatePort = "")
		{
		}

		public sealed override void OnExecute(NodeExecutionState state)
		{
		}
	}
}
