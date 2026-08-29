using System.Text;

public sealed class JigsawPieceRotationPacket : Packet
{
	public JigsawPiece piece;

	public float angle;

	public override byte getTypeId()
	{
		return 43;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(piece);
		writer.Write(in angle, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		piece = reader.ReadComponent<JigsawPiece>();
		angle = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("piece: " + $"{piece}");
		stringBuilder.Append("angle: " + $"{angle}");
		return stringBuilder.ToString();
	}
}
