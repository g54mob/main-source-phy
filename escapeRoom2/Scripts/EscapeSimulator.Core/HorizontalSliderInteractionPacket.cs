using System.Text;
using UnityEngine;

public sealed class HorizontalSliderInteractionPacket : Packet
{
	public HorizontalSlider horizontalSlider;

	public Vector3 offset;

	public float change;

	public override byte getTypeId()
	{
		return 51;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(horizontalSlider);
		writer.WriteVector3(in offset);
		writer.Write(in change, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		horizontalSlider = reader.ReadComponent<HorizontalSlider>();
		offset = reader.ReadVector3();
		change = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("horizontalSlider: " + $"{horizontalSlider}");
		stringBuilder.AppendLine("offset: " + $"{offset}");
		stringBuilder.Append("change: " + $"{change}");
		return stringBuilder.ToString();
	}
}
