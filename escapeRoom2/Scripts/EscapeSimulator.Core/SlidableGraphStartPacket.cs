using System.Text;

public sealed class SlidableGraphStartPacket : Packet
{
	public SlidableGraphPiece slidableGraphPiece;

	public override byte getTypeId()
	{
		return 45;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(slidableGraphPiece);
	}

	public override void readData(FastBinaryReader reader)
	{
		slidableGraphPiece = reader.ReadComponent<SlidableGraphPiece>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("slidableGraphPiece: " + $"{slidableGraphPiece}");
		return stringBuilder.ToString();
	}
}
