using System;

[Serializable]
public class ContextKey_Location
{
	public enum SelectionTypes
	{
		Preset,
		Custom
	}

	public SelectionTypes SelectionType;

	public LocationContextKeys Value;

	public string ContextKey;

	public unsafe string Get()
	{
		//IL_000e: Expected O, but got Ref
		object obj = default(object);
		if (SelectionType == SelectionTypes.Preset)
		{
			return ((Enum)(&obj)).ToString();
		}
		return ContextKey;
	}

	public unsafe static implicit operator string(ContextKey_Location key)
	{
		//IL_002b: Expected O, but got Ref
		if (key != null)
		{
			object obj = default(object);
			if (key.SelectionType == SelectionTypes.Preset)
			{
				return ((Enum)(&obj)).ToString();
			}
			return key.ContextKey;
		}
		return null;
	}
}
