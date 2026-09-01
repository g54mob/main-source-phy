using System;
using SleepyNodes;
using UnityEngine;

[Serializable]
public class LocationFilter
{
	public enum LocationTypes
	{
		Any = 0,
		GridLocation = 1,
		Zone = 2,
		Relative = 3
	}

	public enum RelativeReferenceTypes
	{
		Self = 0,
		EntityFromFilter = 1,
		AllEntitiesFromFilter = 2,
		GridLocation = 3,
		Zone = 4,
		ContextLocation = 5,
		ContextEntity = 6,
		Turret = 7
	}

	public enum RelativeDirections
	{
		Distance = 0
	}

	public LocationTypes LocationType;

	public ContextVariableOrInline_GridRefence GridLocation;

	public string ZoneID;

	public FilterEntitySet TargetFilter;

	public ContextKey_Location ContextLocationKey;

	public ContextKey_Entity ContextEntityKey;

	public RelativeReferenceTypes RelativeTo;

	public RelativeDirections RelativeDirection;

	public float Distance;

	public bool Resolve(GridReference location, FireMission fireMission, MapEntity self, StateNode.NodeExecutionState state, MissionGraph missionGraph, Vector3[] gridBounds)
	{
		return false;
	}

	private GridReference ResolveRelative(FireMission fireMission, MapEntity self, StateNode.NodeExecutionState state, MissionGraph missionGraph, Vector3[] gridBounds)
	{
		return null;
	}
}
