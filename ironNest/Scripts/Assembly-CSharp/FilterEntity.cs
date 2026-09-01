using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SleepyNodes;

[Serializable]
public class FilterEntity
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FilterEntityTypes
	{
		ID = 0,
		Role = 1,
		Health = 2,
		Armour = 3,
		State = 4,
		Stars = 5,
		DistanceFromEntity = 6,
		DistanceFromLocation = 7
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum OperationTypes
	{
		Equals = 0,
		NotEquals = 1,
		LessThan = 2,
		GreaterThan = 3,
		LessThanOrEquals = 4,
		GreaterThanOrEquals = 5,
		Contains = 6,
		NotContains = 7
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum IdTypes
	{
		Graph = 0,
		Explict = 1
	}

	public FilterEntityTypes FilterEntityType;

	public OperationTypes Operation;

	public IdTypes IdType;

	public string StringValue;

	public EntityRoles RoleValue;

	public int IntValue;

	public MapEntityStates StateValue;

	public EntityContextKeys DistanceEntityKey;

	public LocationContextKeys DistanceLocationKey;

	public float FloatValue;

	public bool BoolValue;

	public bool Execute(MapEntity entity, StateNode.NodeExecutionState state)
	{
		return false;
	}
}
