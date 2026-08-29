using System.Text;

public sealed class SlidableGraphFinalizePacket : Packet
{
	public SlidableGraphPiece slidableGraphPiece;

	public float pieceEdgePercentage;

	public short startNodeIndex;

	public short endNodeIndex;

	public override byte getTypeId()
	{
		return 47;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(slidableGraphPiece);
		writer.Write(in pieceEdgePercentage, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in startNodeIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in endNodeIndex, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		slidableGraphPiece = reader.ReadComponent<SlidableGraphPiece>();
		pieceEdgePercentage = reader.ReadSingle();
		startNodeIndex = reader.ReadInt16();
		endNodeIndex = reader.ReadInt16();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("slidableGraphPiece: " + $"{slidableGraphPiece}");
		stringBuilder.AppendLine("pieceEdgePercentage: " + $"{pieceEdgePercentage}");
		stringBuilder.AppendLine("startNodeIndex: " + $"{startNodeIndex}");
		stringBuilder.Append("endNodeIndex: " + $"{endNodeIndex}");
		return stringBuilder.ToString();
	}
}
