using System.Text;
using UnityEngine;

public sealed class SurfaceSlotInteractionPacket : Packet
{
	public SurfaceSlot surfaceSlot;

	public Item item;

	public Pose initialPose;

	public override byte getTypeId()
	{
		return 82;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(surfaceSlot);
		writer.WriteComponent(item);
		writer.WritePose(initialPose);
	}

	public override void readData(FastBinaryReader reader)
	{
		surfaceSlot = reader.ReadComponent<SurfaceSlot>();
		item = reader.ReadComponent<Item>();
		initialPose = reader.ReadPose();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("surfaceSlot: " + $"{surfaceSlot}");
		stringBuilder.AppendLine("item: " + $"{item}");
		stringBuilder.Append("initialPose: " + $"{initialPose}");
		return stringBuilder.ToString();
	}
}
