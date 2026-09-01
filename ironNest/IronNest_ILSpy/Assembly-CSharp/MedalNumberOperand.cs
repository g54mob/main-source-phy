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
		//IL_002f: Expected O, but got I4
		//IL_0060: Expected F4, but got I4
		bool flag = Source == MedalValueSource.Inline;
		if (!flag)
		{
			object obj = Source - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					return 0f;
				}
				return values.GetCustomValue(CustomVariableKey);
			}
			return values.GetValue(Variable);
		}
		return InlineValue;
	}
}
