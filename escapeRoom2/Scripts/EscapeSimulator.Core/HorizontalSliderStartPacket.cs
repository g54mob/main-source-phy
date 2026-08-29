using System.Text;

public sealed class HorizontalSliderStartPacket : Packet
{
	public HorizontalSlider horizontalSlider;

	public override byte getTypeId()
	{
		return 50;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(horizontalSlider);
	}

	public override void readData(FastBinaryReader reader)
	{
		horizontalSlider = reader.ReadComponent<HorizontalSlider>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("horizontalSlider: " + $"{horizontalSlider}");
		return stringBuilder.ToString();
	}
}
