using System.Text;
using UnityEngine;

public sealed class PlaceItemInWorldPacket : Packet
{
	public GameObject item;

	public Vector3 position;

	public Quaternion rotation;

	public override byte getTypeId()
	{
		return 30;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteGameObject(item);
		writer.WriteVector3(in position);
		writer.WriteQuaternion(in rotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		item = reader.ReadGameObject();
		position = reader.ReadVector3();
		rotation = reader.ReadQuaternion();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("item: " + $"{item}");
		stringBuilder.AppendLine("position: " + $"{position}");
		stringBuilder.Append("rotation: " + $"{rotation}");
		return stringBuilder.ToString();
	}
}
