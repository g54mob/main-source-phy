using System.Text;
using UnityEngine;

public sealed class PinItemPacket : Packet
{
	public GameObject pinnedItem;

	public Vector3 pinPosition;

	public Quaternion pinRotation;

	public float pinTime;

	public override byte getTypeId()
	{
		return 115;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteGameObject(pinnedItem);
		writer.WriteVector3(in pinPosition);
		writer.WriteQuaternion(in pinRotation);
		writer.Write(in pinTime, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		pinnedItem = reader.ReadGameObject();
		pinPosition = reader.ReadVector3();
		pinRotation = reader.ReadQuaternion();
		pinTime = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("pinnedItem: " + $"{pinnedItem}");
		stringBuilder.AppendLine("pinPosition: " + $"{pinPosition}");
		stringBuilder.AppendLine("pinRotation: " + $"{pinRotation}");
		stringBuilder.Append("pinTime: " + $"{pinTime}");
		return stringBuilder.ToString();
	}
}
