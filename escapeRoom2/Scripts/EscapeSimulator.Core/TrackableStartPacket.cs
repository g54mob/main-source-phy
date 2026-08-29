using System.Text;
using UnityEngine;

public sealed class TrackableStartPacket : Packet
{
	public Trackable trackable;

	public Vector3 value;

	public override byte getTypeId()
	{
		return 57;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(trackable);
		writer.WriteVector3(in value);
	}

	public override void readData(FastBinaryReader reader)
	{
		trackable = reader.ReadComponent<Trackable>();
		value = reader.ReadVector3();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("trackable: " + $"{trackable}");
		stringBuilder.Append("value: " + $"{value}");
		return stringBuilder.ToString();
	}
}
