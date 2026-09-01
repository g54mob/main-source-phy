using System;

[Serializable]
public class MedalNumberOperand
{
	public MedalValueSource Source;

	public float InlineValue;

	public MedalTrackedValue Variable;

	public string CustomVariableKey;

	public float Resolve(MedalTrackedValues values)
	{
		return 0f;
	}
}
