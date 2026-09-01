using System;

public class ToggleExtraOption : ExtraOption
{
	public ToggleExtraOption(string OptionName, bool defaultBool, string displayName, Action<object> apply)
		: base(OptionName, new object[1] { defaultBool }, new string[1] { displayName }, apply)
	{
	}

	public bool Toggle()
	{
		parsedValue = !(bool)parsedValue;
		currentValue = parsedValue.ToString();
		ExtraOptions.SavedOptions[OptionName] = currentValue;
		try
		{
			apply(parsedValue);
		}
		catch
		{
		}
		return (bool)parsedValue;
	}

	public void SetValue(bool newValue)
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

	public bool Parse(string myValue)
	{
		try
		{
			return bool.Parse(myValue);
		}
		catch
		{
			currentValue = arguments[0].ToString();
			return (bool)arguments[0];
		}
	}
}
