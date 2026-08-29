using System.Text;
using UnityEngine;

public sealed class JigsawAddPiecePacket : Packet
{
	public JigsawPiece piece;

	public Vector3 position;

	public override byte getTypeId()
	{
		return 40;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(piece);
		writer.WriteVector3(in position);
	}

	public override void readData(FastBinaryReader reader)
	{
		piece = reader.ReadComponent<JigsawPiece>();
		position = reader.ReadVector3();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("piece: " + $"{piece}");
		stringBuilder.Append("position: " + $"{position}");
		return stringBuilder.ToString();
	}
}
