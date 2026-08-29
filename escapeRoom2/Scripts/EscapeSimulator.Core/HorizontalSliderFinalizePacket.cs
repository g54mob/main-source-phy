using System.Text;
using UnityEngine;

public sealed class HorizontalSliderFinalizePacket : Packet
{
	public HorizontalSlider horizontalSlider;

	public Vector3 offset;

	public override byte getTypeId()
	{
		return 52;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(horizontalSlider);
		writer.WriteVector3(in offset);
	}

	public override void readData(FastBinaryReader reader)
	{
		horizontalSlider = reader.ReadComponent<HorizontalSlider>();
		offset = reader.ReadVector3();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("horizontalSlider: " + $"{horizontalSlider}");
		stringBuilder.Append("offset: " + $"{offset}");
		return stringBuilder.ToString();
	}
}
