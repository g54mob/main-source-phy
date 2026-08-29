using System;
using System.Text;

[Serializable]
public class RotatableData : IReadWrite
{
	public float speed;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in speed, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		speed = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("speed: " + $"{speed}");
		return stringBuilder.ToString();
	}
}
