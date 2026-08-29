using System;

[Serializable]
public struct PropID : IReadWrite, IEquatable<PropID>
{
	public static readonly PropID None = new PropID
	{
		value = null
	};

	public string value;

	public string getIconFilePath()
	{
		return "Assets/_RoomEditor/EditorPropIcons/" + getIconFileName() + ".png";
	}

	public string getIconFileName()
	{
		return value?.Replace("/", "-") ?? string.Empty;
	}

	public static bool operator ==(PropID a, PropID b)
	{
		return a.value == b.value;
	}

	public static bool operator !=(PropID a, PropID b)
	{
		return !(a == b);
	}

	public bool Equals(PropID other)
	{
		return value == other.value;
	}

	public override bool Equals(object obj)
	{
		if (obj is PropID other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		if (value == null)
		{
			return 0;
		}
		return value.GetHashCode();
	}

	public override string ToString()
	{
		return value;
	}

	public void Write(FastBinaryWriter writer)
	{
		writer.Write(value);
	}

	public void Read(FastBinaryReader reader)
	{
		value = reader.ReadString();
	}
}
