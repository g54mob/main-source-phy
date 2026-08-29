using System.Text;
using UnityEngine;

public sealed class LocationPingPacket : Packet
{
	public Vector3 pingPosition;

	public Quaternion pingRotation;

	public Interactive pingedInteractable;

	public override byte getTypeId()
	{
		return 87;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteVector3(in pingPosition);
		writer.WriteQuaternion(in pingRotation);
		writer.WriteComponent(pingedInteractable);
	}

	public override void readData(FastBinaryReader reader)
	{
		pingPosition = reader.ReadVector3();
		pingRotation = reader.ReadQuaternion();
		pingedInteractable = reader.ReadComponent<Interactive>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("pingPosition: " + $"{pingPosition}");
		stringBuilder.AppendLine("pingRotation: " + $"{pingRotation}");
		stringBuilder.Append("pingedInteractable: " + $"{pingedInteractable}");
		return stringBuilder.ToString();
	}
}
