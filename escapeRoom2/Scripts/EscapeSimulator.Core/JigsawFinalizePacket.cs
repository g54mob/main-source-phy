using System.Text;

public sealed class JigsawFinalizePacket : Packet
{
	public JigsawPiece piece;

	public override byte getTypeId()
	{
		return 44;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(piece);
	}

	public override void readData(FastBinaryReader reader)
	{
		piece = reader.ReadComponent<JigsawPiece>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("piece: " + $"{piece}");
		return stringBuilder.ToString();
	}
}
