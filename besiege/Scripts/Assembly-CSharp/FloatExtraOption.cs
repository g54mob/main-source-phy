using System;

public class FloatExtraOption : ExtraOption
{
	public FloatExtraOption(string OptionName, float defaultFloat, string displayName, Action<object> apply)
		: base(OptionName, new object[1] { defaultFloat }, new string[1] { displayName }, apply)
	{
	}

	public float Parse(string myValue)
	{
		try
		{
			return float.Parse(myValue);
		}
		catch
		{
			currentValue = arguments[0].ToString();
			return (float)arguments[0];
		}
	}

	public void SetValue(float newValue)
	{
		parsedValue = newValue;
		currentValue = parsedValue.ToString();
		ExtraOptions.SavedOptions[OptionName] = currentValue;
		try
		{
			apply(parsedValue);
		}
		catch
		{
		}
	}
}
