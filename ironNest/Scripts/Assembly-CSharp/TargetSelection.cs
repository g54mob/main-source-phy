using System;
using System.Collections.Generic;
using SleepyNodes;

[Serializable]
public class TargetSelection
{
	public enum SourceTypes
	{
		FromContext = 0,
		FromFilter = 1
	}

	public enum CountTypes
	{
		All = 0,
		Count = 1
	}

	public enum SortTypes
	{
		First = 0,
		Random = 1,
		DistanceFromEntity = 2,
		DistanceFromLocation = 3
	}

	public SourceTypes SourceType;

	public EntityContextKeys ContextKey;

	public FilterEntitySet Filter;

	public CountTypes CountType;

	public int Count;

	public SortTypes SortType;

	public EntityContextKeys DistanceEntityKey;

	public LocationContextKeys DistanceLocationKey;

	public List<MapEntity> Resolve(FireMission fireMission, StateNode.NodeExecutionState state)
	{
		return null;
	}
}
