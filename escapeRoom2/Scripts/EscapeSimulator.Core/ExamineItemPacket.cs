using System.Text;
using UnityEngine;

public sealed class ExamineItemPacket : Packet
{
	public GameObject examinedItem;

	public bool isEnteringExamineMode;

	public bool isItemVisibleOnExit;

	public Vector3 cameraPosition;

	public Quaternion cameraRotation;

	public override byte getTypeId()
	{
		return 83;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteGameObject(examinedItem);
		writer.Write(in isEnteringExamineMode, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isItemVisibleOnExit, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in cameraPosition);
		writer.WriteQuaternion(in cameraRotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		examinedItem = reader.ReadGameObject();
		isEnteringExamineMode = reader.ReadBoolean();
		isItemVisibleOnExit = reader.ReadBoolean();
		cameraPosition = reader.ReadVector3();
		cameraRotation = reader.ReadQuaternion();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("examinedItem: " + $"{examinedItem}");
		stringBuilder.AppendLine("isEnteringExamineMode: " + $"{isEnteringExamineMode}");
		stringBuilder.AppendLine("isItemVisibleOnExit: " + $"{isItemVisibleOnExit}");
		stringBuilder.AppendLine("cameraPosition: " + $"{cameraPosition}");
		stringBuilder.Append("cameraRotation: " + $"{cameraRotation}");
		return stringBuilder.ToString();
	}
}
