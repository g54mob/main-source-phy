using System;

[Serializable]
public class ContextKey_Location
{
	public enum SelectionTypes
	{
		Preset = 0,
		Custom = 1
	}

	public SelectionTypes SelectionType;

	public LocationContextKeys Value;

	public string ContextKey;

	public string Get()
	{
		return null;
	}

	public static implicit operator string(ContextKey_Location key)
	{
		return null;
	}
}
