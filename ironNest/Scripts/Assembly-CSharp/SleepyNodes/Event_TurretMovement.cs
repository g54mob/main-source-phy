namespace SleepyNodes
{
	[CreateNodeMenu("Events/Turret Movement")]
	[NodeWidth(400)]
	[NodeName("[Event] Turret Movement")]
	public class Event_TurretMovement : EventNode
	{
		public EventData_TurretMovement.MovementTypes MovementType;

		public override void ResetNode()
		{
		}

		protected override bool ShouldRun(EventData data)
		{
			return false;
		}
	}
}
