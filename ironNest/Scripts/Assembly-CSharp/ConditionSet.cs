using System;
using System.Collections.Generic;
using SleepyNodes;

[Serializable]
public class ConditionSet
{
	[Serializable]
	public class ConditionPair
	{
		public enum Operation
		{
			Base = 0,
			And = 1,
			Or = 2
		}

		public Operation operation;

		public Condition Condition;
	}

	public ConditionPair[] Conditions;

	public bool Resolve(StateNode.NodeExecutionState state, List<MapEntity> filteredEntities)
	{
		return false;
	}
}
