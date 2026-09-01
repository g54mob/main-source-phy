using System;

namespace SleepyNodes
{
	[CreateNodeMenu("Wait/For Seconds")]
	[NodeName("Wait Seconds")]
	[NodeWidth(300)]
	public class State_WaitSeconds : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)]
		public StateNode Cancel;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode OnCancelled;

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)]
		public StateNode InstantProgress;

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)]
		public StateNode ResetTime;

		public float Seconds;

		[NonSerialized]
		private bool cancelCalled;

		private bool instantProgress;

		private bool resetTime;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}
	}
}
