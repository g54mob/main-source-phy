using System.Text;
using UnityEngine;

public sealed class StealingPacket : Packet
{
	public GameObject item;

	public override byte getTypeId()
	{
		return 80;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteGameObject(item);
	}

	public override void readData(FastBinaryReader reader)
	{
		item = reader.ReadGameObject();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("item: " + $"{item}");
		return stringBuilder.ToString();
	}
}
