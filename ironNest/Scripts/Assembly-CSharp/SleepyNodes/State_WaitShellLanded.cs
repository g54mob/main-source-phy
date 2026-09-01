using System;

namespace SleepyNodes
{
	[CreateNodeMenu("Wait/For Shell Landed")]
	[NodeName("Wait Shell Landed")]
	[NodeWidth(400)]
	public class State_WaitShellLanded : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)]
		public StateNode Cancel;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode OnCancelled;

		public TargetSelection EntityFilter;

		public LocationFilter LocationFilter;

		public ShellDefinition Shell;

		[NonSerialized]
		private bool cancelCalled;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}

		public override void OnEvent(EventNode.EventData data, NodeExecutionState state)
		{
		}
	}
}
