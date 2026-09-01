using System;

[Serializable]
public class ContextKey_Entity
{
	public enum SelectionTypes
	{
		Preset = 0,
		Custom = 1
	}

	public SelectionTypes SelectionType;

	public EntityContextKeys Value;

	public string ContextKey;

	public string Get()
	{
		return null;
	}

	public static implicit operator string(ContextKey_Entity key)
	{
		return null;
	}
}
