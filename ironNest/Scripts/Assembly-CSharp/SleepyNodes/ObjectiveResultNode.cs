namespace SleepyNodes
{
	[CreateNodeMenu("Objectives/Result")]
	[NodeName("Objective Result")]
	[NodeWidth(400)]
	public class ObjectiveResultNode : ObjectiveStateNode
	{
		public ObjectiveGraph.ObjectiveResults Result;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
