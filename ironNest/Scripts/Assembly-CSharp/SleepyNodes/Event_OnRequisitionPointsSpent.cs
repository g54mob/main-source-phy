namespace SleepyNodes
{
	[CreateNodeMenu("Events/Requisition Points Spent")]
	[NodeWidth(400)]
	[NodeName("[Event] Requisition Points Spent")]
	public class Event_OnRequisitionPointsSpent : EventNode
	{
		public string ContextKey;

		private EventData_RequisitionPointsSpent lastTriggered;

		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}

		public override void Run(NodeExecutionState state)
		{
		}
	}
}
