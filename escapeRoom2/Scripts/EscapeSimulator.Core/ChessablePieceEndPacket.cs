using System.Text;

public sealed class ChessablePieceEndPacket : Packet
{
	public ChessableTile tile;

	public override byte getTypeId()
	{
		return 49;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(tile);
	}

	public override void readData(FastBinaryReader reader)
	{
		tile = reader.ReadComponent<ChessableTile>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("tile: " + $"{tile}");
		return stringBuilder.ToString();
	}
}
