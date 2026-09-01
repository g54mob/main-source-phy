using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SleepyNodes;

[Serializable]
public class Condition
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ConditionTypes
	{
		Context = 0,
		Entity = 1,
		Filter = 2,
		Comparison = 3
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
	public enum ContextConditions
	{
		RequisitionPoints = 0,
		PowederCharges = 1,
		TimerExists = 2,
		TimerRunning = 3,
		TimerTimeRemaining = 4,
		MissionIsCompleteThisRun = 5,
		MissionIsCompletePreviously = 6,
		MedalsEarnedThisRun = 7,
		MedalsEarnedBest = 8,
		TimeSinceMissionStart = 9,
		MissionIsFailedThisRun = 10,
		TimerExpired = 11,
		GenericTimerValue = 12,
		TurretMoving = 13
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum EnittyConditions
	{
		ID = 0,
		Role = 1,
		Health = 2,
		Armour = 3,
		State = 4,
		Stars = 5,
		DistanceFromEntity = 6,
		DistanceFromLocation = 7,
		DistanceFromTurret = 8
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum FilterConditions
	{
		Count = 0
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ComparisonConditions
	{
		Distance = 0,
		Medals = 1
	}

	[Serializable]
	public class EntityIDLookup
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public enum IdTypes
		{
			Graph = 0,
			Explict = 1,
			Context = 2,
			Filter = 3
		}

		public IdTypes IdType;

		public string Value;

		public EntityContextKeys ContextKey;

		public int FilterIndex;

		public MapEntity Resolve(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
		{
			return null;
		}
	}

	public ConditionTypes ConditionType;

	public OperationTypes Operation;

	public ContextConditions ContextCondition;

	public EnittyConditions EnittyCondition;

	public EntityRoles RoleValue;

	public MapEntityStates StateValue;

	public LocationContextKeys DistanceLocationKey;

	public EntityIDLookup Entity1;

	public EntityIDLookup Entity2;

	public FilterConditions FilterCondition;

	public ComparisonConditions ComparisonCondition;

	public string StringValue;

	public int IntValue;

	public float FloatValue;

	public bool BoolValue;

	public bool Execute(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		return false;
	}

	public bool Resolve_Context(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		return false;
	}

	public bool Resolve_Entity(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		return false;
	}

	public bool Resolve_Filter(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		return false;
	}

	public bool Resolve_Comparison(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		return false;
	}
}
