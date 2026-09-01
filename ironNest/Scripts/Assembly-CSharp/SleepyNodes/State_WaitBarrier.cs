using UnityEngine;

namespace SleepyNodes
{
	[CreateNodeMenu("Wait/Barrier")]
	[NodeName("Wait Barrier")]
	[NodeWidth(400)]
	public class State_WaitBarrier : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)]
		public StateNode ResetCounter;

		public int Count;

		[Header("Only One Of:")]
		public bool AutoReset;

		public bool StopAfter;

		public bool AllowUpTo;

		private int current;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
