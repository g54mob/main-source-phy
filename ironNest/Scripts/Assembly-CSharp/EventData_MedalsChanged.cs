using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SleepyNodes;

public class EventData_MedalsChanged : EventNode.EventData
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Changes
	{
		Increase = 0,
		Decrease = 1
	}

	public string MedalID;

	public Changes Change;
}
