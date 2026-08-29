using System.Text;
using UnityEngine;

public sealed class SharedZoomRotationPacket : Packet
{
	public Quaternion rotation;

	public override byte getTypeId()
	{
		return 85;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteQuaternion(in rotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		rotation = reader.ReadQuaternion();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("rotation: " + $"{rotation}");
		return stringBuilder.ToString();
	}
}
