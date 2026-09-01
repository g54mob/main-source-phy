using ArticleSystem;

namespace SleepyNodes
{
	[CreateNodeMenu("Mission/Queue Article")]
	[NodeWidth(400)]
	[NodeName("Queue Article")]
	public class State_QueueArticle : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public ArticlePoolDefinition Article;

		public int Amount;

		public string Note;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
