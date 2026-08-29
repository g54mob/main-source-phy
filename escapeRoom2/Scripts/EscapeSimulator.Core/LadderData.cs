using System;
using System.Text;
using UnityEngine;

[Serializable]
public class LadderData : IReadWrite
{
	public Vector3 upLocalPosition;

	public Vector3 downLocalPosition;

	public Vector3 upExitLocalPosition;

	public Vector3 downExitLocalPosition;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteVector3(in upLocalPosition);
		writer.WriteVector3(in downLocalPosition);
		writer.WriteVector3(in upExitLocalPosition);
		writer.WriteVector3(in downExitLocalPosition);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		upLocalPosition = reader.ReadVector3();
		downLocalPosition = reader.ReadVector3();
		upExitLocalPosition = reader.ReadVector3();
		downExitLocalPosition = reader.ReadVector3();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("upLocalPosition: " + $"{upLocalPosition}");
		stringBuilder.AppendLine("downLocalPosition: " + $"{downLocalPosition}");
		stringBuilder.AppendLine("upExitLocalPosition: " + $"{upExitLocalPosition}");
		stringBuilder.Append("downExitLocalPosition: " + $"{downExitLocalPosition}");
		return stringBuilder.ToString();
	}
}
