using System.Collections.Generic;

namespace SleepyNodes
{
	[CreateNodeMenu("Mission")]
	[NodeName("Mission")]
	[NodeWidth(400)]
	public class MissionNode : Node
	{
		public enum NextUnlockConditions
		{
			All = 0,
			Exclusive = 1
		}

		public enum UnlockConditions
		{
			Any = 0,
			All = 1
		}

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public MissionNode UnlockedBy;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)]
		public MissionNode Unlocks;

		public MissionGraph Mission;

		public UnlockConditions UnlockCondition;

		public NextUnlockConditions NextUnlockCondition;

		public List<MissionNode> GetUnlockedBy()
		{
			return null;
		}

		public List<MissionNode> GetUnlocks()
		{
			return null;
		}
	}
}
