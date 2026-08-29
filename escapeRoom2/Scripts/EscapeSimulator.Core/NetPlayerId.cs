using System;

public class NetPlayerId : IEquatable<NetPlayerId>, IComparable<NetPlayerId>
{
	public static readonly NetPlayerId Empty = new NetPlayerId(string.Empty);

	public readonly string value;

	public NetPlayerId(string value)
	{
		this.value = value ?? throw new NullReferenceException("value");
	}

	public override string ToString()
	{
		return value;
	}

	public override int GetHashCode()
	{
		return value.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		if (obj is NetPlayerId other)
		{
			return Equals(other);
		}
		return false;
	}

	public bool Equals(NetPlayerId other)
	{
		return compare(this, other) == 0;
	}

	public int CompareTo(NetPlayerId other)
	{
		return compare(this, other);
	}

	public static bool operator ==(NetPlayerId lhs, NetPlayerId rhs)
	{
		return compare(lhs, rhs) == 0;
	}

	public static bool operator !=(NetPlayerId lhs, NetPlayerId rhs)
	{
		return compare(lhs, rhs) != 0;
	}

	public static bool operator >(NetPlayerId lhs, NetPlayerId rhs)
	{
		return compare(lhs, rhs) > 0;
	}

	public static bool operator <(NetPlayerId lhs, NetPlayerId rhs)
	{
		return compare(lhs, rhs) < 0;
	}

	public static bool operator >=(NetPlayerId lhs, NetPlayerId rhs)
	{
		return compare(lhs, rhs) >= 0;
	}

	public static bool operator <=(NetPlayerId lhs, NetPlayerId rhs)
	{
		return compare(lhs, rhs) <= 0;
	}

	private static int compare(NetPlayerId lhs, NetPlayerId rhs)
	{
		if ((object)lhs == rhs)
		{
			return 0;
		}
		if ((object)rhs == null)
		{
			return 1;
		}
		if ((object)lhs == null)
		{
			return -1;
		}
		return string.Compare(lhs.value, rhs.value, StringComparison.Ordinal);
	}

	public int getSizeInBytes()
	{
		if (value == string.Empty)
		{
			return 1;
		}
		if (!ulong.TryParse(value, out var _))
		{
			return 5 + value.Length * 2;
		}
		return 9;
	}
}
