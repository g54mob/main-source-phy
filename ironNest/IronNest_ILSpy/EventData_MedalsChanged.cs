using SleepyNodes;

public class EventData_MedalsChanged : EventNode.EventData
{
	public enum Changes
	{
		Increase,
		Decrease
	}

	public string MedalID;

	public Changes Change;
}
