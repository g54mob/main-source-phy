using System;

[Serializable]
public class MedalConditionSet
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

		public MedalCondition Condition;
	}

	public ConditionPair[] Conditions;

	public bool Resolve(MedalTrackedValues values)
	{
		return false;
	}
}
