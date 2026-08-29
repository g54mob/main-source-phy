using System;
using System.Text;

[Serializable]
public class SpawnPointData : IReadWrite
{
	public float walkingSpeed;

	public float runningSpeed;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in walkingSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in runningSpeed, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		walkingSpeed = reader.ReadSingle();
		runningSpeed = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("walkingSpeed: " + $"{walkingSpeed}");
		stringBuilder.Append("runningSpeed: " + $"{runningSpeed}");
		return stringBuilder.ToString();
	}
}
