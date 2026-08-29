using System.Text;
using UnityEngine;

public sealed class JigsawInteractionPacket : Packet
{
	public JigsawPiece piece;

	public Vector3 piecePosition;

	public override byte getTypeId()
	{
		return 42;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(piece);
		writer.WriteVector3(in piecePosition);
	}

	public override void readData(FastBinaryReader reader)
	{
		piece = reader.ReadComponent<JigsawPiece>();
		piecePosition = reader.ReadVector3();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("piece: " + $"{piece}");
		stringBuilder.Append("piecePosition: " + $"{piecePosition}");
		return stringBuilder.ToString();
	}
}
