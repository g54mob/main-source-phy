using System.Text;

public sealed class SlidableGraphInteractionPacket : Packet
{
	public SlidableGraphPiece slidableGraphPiece;

	public float pieceEdgePercentage;

	public short startNodeIndex;

	public short endNodeIndex;

	public SlidableGraph.Event graphEvent;

	public override byte getTypeId()
	{
		return 46;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(slidableGraphPiece);
		writer.Write(in pieceEdgePercentage, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in startNodeIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in endNodeIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write((byte)graphEvent);
	}

	public override void readData(FastBinaryReader reader)
	{
		slidableGraphPiece = reader.ReadComponent<SlidableGraphPiece>();
		pieceEdgePercentage = reader.ReadSingle();
		startNodeIndex = reader.ReadInt16();
		endNodeIndex = reader.ReadInt16();
		graphEvent = (SlidableGraph.Event)reader.ReadByte();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("slidableGraphPiece: " + $"{slidableGraphPiece}");
		stringBuilder.AppendLine("pieceEdgePercentage: " + $"{pieceEdgePercentage}");
		stringBuilder.AppendLine("startNodeIndex: " + $"{startNodeIndex}");
		stringBuilder.AppendLine("endNodeIndex: " + $"{endNodeIndex}");
		stringBuilder.Append("graphEvent: " + $"{graphEvent}");
		return stringBuilder.ToString();
	}
}
