using System;

[Serializable]
public class MedalNumberExpression
{
	public MedalExpressionMode Mode;

	public MedalValueSource Source;

	public float InlineValue;

	public MedalTrackedValue Variable;

	public string CustomVariableKey;

	public MedalNumberOperand A;

	public MedalMathOperator MathOperator;

	public MedalNumberOperand B;

	public float Resolve(MedalTrackedValues values)
	{
		return 0f;
	}

	private float ResolveValue(MedalTrackedValues values)
	{
		return 0f;
	}
}
