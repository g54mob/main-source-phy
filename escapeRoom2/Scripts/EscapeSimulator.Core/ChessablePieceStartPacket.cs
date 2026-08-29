using System.Text;

public sealed class ChessablePieceStartPacket : Packet
{
	public ChessablePiece piece;

	public override byte getTypeId()
	{
		return 48;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(piece);
	}

	public override void readData(FastBinaryReader reader)
	{
		piece = reader.ReadComponent<ChessablePiece>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("piece: " + $"{piece}");
		return stringBuilder.ToString();
	}
}
