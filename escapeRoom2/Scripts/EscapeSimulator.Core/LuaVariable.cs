using System;
using MoonSharp.Interpreter;

public struct LuaVariable : IEquatable<LuaVariable>
{
	public string name;

	public DataType type;

	public bool Equals(LuaVariable other)
	{
		return name == other.name;
	}

	public override bool Equals(object obj)
	{
		if (obj is LuaVariable other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		if (name == null)
		{
			return 0;
		}
		return name.GetHashCode();
	}
}
