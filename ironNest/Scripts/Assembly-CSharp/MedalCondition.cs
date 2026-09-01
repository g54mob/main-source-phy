using System;

[Serializable]
public class MedalCondition
{
	public MedalNumberExpression Left;

	public MedalCompareOperator Operator;

	public MedalNumberExpression Right;

	public bool Resolve(MedalTrackedValues values)
	{
		return false;
	}
}
