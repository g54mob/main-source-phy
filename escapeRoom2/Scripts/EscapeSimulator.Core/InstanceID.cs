using System;

[Serializable]
public struct InstanceID : IReadWrite, IEquatable<InstanceID>
{
	public static readonly InstanceID None = new InstanceID
	{
		value = 0
	};

	public int value;

	public static bool operator ==(InstanceID a, InstanceID b)
	{
		return a.value == b.value;
	}

	public static bool operator !=(InstanceID a, InstanceID b)
	{
		return !(a == b);
	}

	public bool Equals(InstanceID other)
	{
		return value == other.value;
	}

	public override bool Equals(object obj)
	{
		if (obj is InstanceID other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return value;
	}

	public override string ToString()
	{
		return value.ToString();
	}

	public void Write(FastBinaryWriter writer)
	{
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public void Read(FastBinaryReader reader)
	{
		value = reader.ReadInt32();
	}
}
