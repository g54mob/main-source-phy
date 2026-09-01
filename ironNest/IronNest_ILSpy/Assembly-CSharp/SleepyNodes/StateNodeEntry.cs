using System;

namespace SleepyNodes;

[Serializable]
public abstract class StateNodeEntry : Node
{
	public StateNode To;

	public virtual void Run(StateNode.NodeExecutionState state)
	{
		StateNode connectedNode = GetConnectedNode<StateNode>("To", out var _);
		state.lastFieldPort = null;
		if (connectedNode != null)
		{
			connectedNode.OnEnter(state);
		}
	}

	public override object GetValue(NodePort port)
	{
		return this;
	}
}
