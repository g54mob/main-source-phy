using System.Text;
using UnityEngine;

public sealed class TakeCameraPicturePacket : Packet
{
	public Vector3 cameraPosition;

	public Quaternion cameraRotation;

	public override byte getTypeId()
	{
		return 114;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteVector3(in cameraPosition);
		writer.WriteQuaternion(in cameraRotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		cameraPosition = reader.ReadVector3();
		cameraRotation = reader.ReadQuaternion();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("cameraPosition: " + $"{cameraPosition}");
		stringBuilder.Append("cameraRotation: " + $"{cameraRotation}");
		return stringBuilder.ToString();
	}
}
