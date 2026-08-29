using System.Text;
using UnityEngine;

public sealed class RotatableInteractionPacket : Packet
{
	public Rotatable rotatable;

	public Quaternion rotation;

	public override byte getTypeId()
	{
		return 66;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(rotatable);
		writer.WriteQuaternion(in rotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		rotatable = reader.ReadComponent<Rotatable>();
		rotation = reader.ReadQuaternion();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("rotatable: " + $"{rotatable}");
		stringBuilder.Append("rotation: " + $"{rotation}");
		return stringBuilder.ToString();
	}
}
