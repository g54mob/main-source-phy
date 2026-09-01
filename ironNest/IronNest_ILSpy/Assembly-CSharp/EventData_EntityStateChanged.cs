using SleepyNodes;

public class EventData_EntityStateChanged : EventNode.EventData
{
	public MapEntity Entity;

	public MapEntityStates oldState;

	public MapEntityStates newState;
}
