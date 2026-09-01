using System;
using SleepyNodes;
using UnityEngine;

[Serializable]
public class LocationSelection
{
	public enum LocationTypes
	{
		GridLocation = 0,
		Zone = 1,
		Entity = 2,
		ContextLocation = 3,
		ContextEntity = 4,
		Relative = 5,
		Turret = 6
	}

	public enum RelativeReferenceTypes
	{
		Self = 0,
		EntityFromFilter = 1,
		GridLocation = 2,
		ContextLocation = 3,
		ContextEntity = 4,
		Turret = 5
	}

	public enum RelativeDirections
	{
		Offset = 0,
		Towards = 1,
		Away = 2,
		RandomInRadius = 3,
		BearingDistance = 4
	}

	public LocationTypes LocationType;

	public ContextVariableOrInline_GridRefence GridLocation;

	public string ZoneID;

	public FilterEntitySet TargetFilter;

	public ContextKey_Location ContextLocationKey;

	public ContextKey_Entity ContextEntityKey;

	public RelativeReferenceTypes RelativeTo;

	public RelativeDirections RelativeDirection;

	public Vector2Int OffsetMin;

	public Vector2Int OffsetMax;

	public ContextVariableOrInline_Float DistanceMin;

	public ContextVariableOrInline_Float DistanceMax;

	public ContextVariableOrInline_Float Bearing;

	public bool FuzzyLocation;

	public bool RandomiseSubgrid;

	[NonSerialized]
	public bool DidClampToMap;

	public GridReference Resolve(FireMission fireMission, MapEntity self, StateNode.NodeExecutionState state, MissionGraph missionGraph, Vector3[] gridBounds)
	{
		return null;
	}

	private GridReference ResolveRelative(FireMission fireMission, MapEntity self, StateNode.NodeExecutionState state, MissionGraph missionGraph, Vector3[] gridBounds)
	{
		return null;
	}
}
