using System;

public class ExtraOption : ICanBeReset
{
	public Action<object> apply;

	public string OptionName;

	public object[] arguments;

	public string[] argumentNames;

	public int resetIndex;

	public string currentValue;

	public object parsedValue;

	public string currentName;

	public ExtraOption(string OptionName, object[] arguments, string[] argumentNames, Action<object> apply)
	{
		this.apply = apply;
		this.OptionName = OptionName;
		this.arguments = arguments;
		this.argumentNames = argumentNames;
		if (ExtraOptions.SavedOptions.ContainsKey(OptionName))
		{
			currentValue = ExtraOptions.SavedOptions[OptionName];
		}
		else
		{
			currentValue = arguments[0].ToString();
			ExtraOptions.SavedOptions.Add(OptionName, currentValue);
		}
		parsedValue = Parse(currentValue);
		Apply();
	}

	private object Parse(string myValue)
	{
		for (int i = 0; i < arguments.Length; i++)
		{
			if (myValue == arguments[i].ToString())
			{
				currentName = argumentNames[i];
				return arguments[i];
			}
		}
		currentName = argumentNames[0];
		currentValue = arguments[0].ToString();
		return arguments[0];
	}

	public void SetValue(string newValue)
	{
		currentValue = newValue;
		parsedValue = Parse(currentValue);
		ExtraOptions.SavedOptions[OptionName] = currentValue;
		Apply();
	}

	public void Reset()
	{
		ExtraOptions.SavedOptions[OptionName] = (currentValue = arguments[resetIndex].ToString());
		parsedValue = arguments[resetIndex];
		currentName = argumentNames[resetIndex];
		ExtraOptions.SavedOptions[OptionName] = currentValue;
		Apply();
	}

	public void Apply()
	{
		try
		{
			apply(parsedValue);
		}
		catch
		{
		}
	}
}
