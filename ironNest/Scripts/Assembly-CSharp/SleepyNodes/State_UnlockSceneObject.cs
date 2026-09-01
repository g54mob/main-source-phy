namespace SleepyNodes
{
	[CreateNodeMenu("Mission/Unlock Scene Object")]
	[NodeWidth(400)]
	[NodeName("Unlock Scene Object")]
	public class State_UnlockSceneObject : StateNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public string ObjectID;

		public override void OnEnter(NodeExecutionState state)
		{
		}
	}
}
