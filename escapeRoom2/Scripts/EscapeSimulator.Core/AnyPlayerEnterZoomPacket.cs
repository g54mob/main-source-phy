using System.Text;

public sealed class AnyPlayerEnterZoomPacket : Packet
{
	public Interactive zoom;

	public override byte getTypeId()
	{
		return 126;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(zoom);
	}

	public override void readData(FastBinaryReader reader)
	{
		zoom = reader.ReadComponent<Interactive>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("zoom: " + $"{zoom}");
		return stringBuilder.ToString();
	}
}
