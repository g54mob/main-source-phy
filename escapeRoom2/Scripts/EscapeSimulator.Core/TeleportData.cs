using System;
using System.Text;

[Serializable]
public class TeleportData : IReadWrite
{
	public bool teleportAll;

	public bool changeRotation;

	public float walkingSpeed;

	public float runningSpeed;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in teleportAll, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in changeRotation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in walkingSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in runningSpeed, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		teleportAll = reader.ReadBoolean();
		changeRotation = reader.ReadBoolean();
		walkingSpeed = reader.ReadSingle();
		runningSpeed = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("teleportAll: " + $"{teleportAll}");
		stringBuilder.AppendLine("changeRotation: " + $"{changeRotation}");
		stringBuilder.AppendLine("walkingSpeed: " + $"{walkingSpeed}");
		stringBuilder.Append("runningSpeed: " + $"{runningSpeed}");
		return stringBuilder.ToString();
	}
}
