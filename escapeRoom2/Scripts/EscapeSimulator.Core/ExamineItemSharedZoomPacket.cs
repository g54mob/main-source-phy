using System.Text;
using UnityEngine;

public sealed class ExamineItemSharedZoomPacket : Packet
{
	public GameObject itemBeingExamined;

	public bool isEnteringExamineMode;

	public override byte getTypeId()
	{
		return 84;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteGameObject(itemBeingExamined);
		writer.Write(in isEnteringExamineMode, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		itemBeingExamined = reader.ReadGameObject();
		isEnteringExamineMode = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("itemBeingExamined: " + $"{itemBeingExamined}");
		stringBuilder.Append("isEnteringExamineMode: " + $"{isEnteringExamineMode}");
		return stringBuilder.ToString();
	}
}
