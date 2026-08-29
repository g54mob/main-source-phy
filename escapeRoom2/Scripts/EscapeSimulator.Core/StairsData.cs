using System;
using System.Text;

[Serializable]
public class StairsData : IReadWrite
{
	public Stairs.Direction direction;

	public virtual void Write(FastBinaryWriter writer)
	{
		int value = (int)direction;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		direction = (Stairs.Direction)reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("direction: " + $"{direction}");
		return stringBuilder.ToString();
	}
}
