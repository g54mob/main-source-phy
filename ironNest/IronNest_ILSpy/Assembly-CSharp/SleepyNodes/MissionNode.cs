using System.Collections.Generic;

namespace SleepyNodes;

public class MissionNode : Node
{
	public enum NextUnlockConditions
	{
		All,
		Exclusive
	}

	public enum UnlockConditions
	{
		Any,
		All
	}

	public MissionNode UnlockedBy;

	public MissionNode Unlocks;

	public MissionGraph Mission;

	public UnlockConditions UnlockCondition;

	public NextUnlockConditions NextUnlockCondition;

	public List<MissionNode> GetUnlockedBy()
	{
		return GetConnectedNodes<MissionNode>("UnlockedBy");
	}

	public List<MissionNode> GetUnlocks()
	{
		return GetConnectedNodes<MissionNode>("Unlocks");
	}
}
