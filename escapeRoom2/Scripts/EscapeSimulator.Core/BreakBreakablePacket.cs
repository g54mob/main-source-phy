using System.Text;
using UnityEngine;

public sealed class BreakBreakablePacket : Packet
{
	public GameObject breakableObject;

	public override byte getTypeId()
	{
		return 81;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteGameObject(breakableObject);
	}

	public override void readData(FastBinaryReader reader)
	{
		breakableObject = reader.ReadGameObject();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("breakableObject: " + $"{breakableObject}");
		return stringBuilder.ToString();
	}
}
