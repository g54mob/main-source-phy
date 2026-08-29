using System.Text;
using UnityEngine;

public sealed class GrabVRPacket : Packet
{
	public GameObject grabbedObject;

	public bool isLeftHand;

	public override byte getTypeId()
	{
		return 113;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteGameObject(grabbedObject);
		writer.Write(in isLeftHand, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		grabbedObject = reader.ReadGameObject();
		isLeftHand = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("grabbedObject: " + $"{grabbedObject}");
		stringBuilder.Append("isLeftHand: " + $"{isLeftHand}");
		return stringBuilder.ToString();
	}
}
