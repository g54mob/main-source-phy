namespace SleepyNodes
{
	public class EventData_TurretMovement : EventNode.EventData
	{
		public enum MovementTypes
		{
			Started = 0,
			Finished = 1
		}

		public MovementTypes MovementType;
	}
}
