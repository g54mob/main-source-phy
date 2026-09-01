namespace SleepyNodes
{
	[CreateNodeMenu("Timer/Start Generic Timer")]
	[NodeWidth(400)]
	[NodeName("Start Generic Timer")]
	public class State_GenericTimer : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public string TimerID;

		public float InitialSeconds;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
