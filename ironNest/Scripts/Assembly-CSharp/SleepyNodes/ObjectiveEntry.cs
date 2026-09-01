using System;

namespace SleepyNodes
{
	[Serializable]
	[CreateNodeMenu("Objectives/Entry")]
	[NodeName("Start Objective")]
	[NodeWidth(200)]
	public class ObjectiveEntry : Node
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
