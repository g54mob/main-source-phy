namespace SleepyNodes;

public class EventData_TurretMovement : EventNode.EventData
{
	public enum MovementTypes
	{
		Started,
		Finished
	}

	public MovementTypes MovementType;
}
