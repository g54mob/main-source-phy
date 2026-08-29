using System.Text;
using UnityEngine;

public sealed class PlayerRayPacket : Packet
{
	public Vector3 rayPosition;

	public Vector3 rayDirection;

	public override byte getTypeId()
	{
		return 25;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteVector3(in rayPosition);
		writer.WriteVector3(in rayDirection);
	}

	public override void readData(FastBinaryReader reader)
	{
		rayPosition = reader.ReadVector3();
		rayDirection = reader.ReadVector3();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("rayPosition: " + $"{rayPosition}");
		stringBuilder.Append("rayDirection: " + $"{rayDirection}");
		return stringBuilder.ToString();
	}
}
