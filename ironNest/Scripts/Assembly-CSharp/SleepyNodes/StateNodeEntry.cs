using System;

namespace SleepyNodes
{
	[Serializable]
	public abstract class StateNodeEntry : Node
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public virtual void Run(StateNode.NodeExecutionState state)
		{
		}

		public override object GetValue(NodePort port)
		{
			return null;
		}
	}
}
