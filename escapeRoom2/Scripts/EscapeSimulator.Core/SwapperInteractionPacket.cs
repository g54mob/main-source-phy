using System.Text;

public sealed class SwapperInteractionPacket : Packet
{
	public Swapper swapper;

	public SwapperPiece currentPiece;

	public override byte getTypeId()
	{
		return 105;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(swapper);
		writer.WriteComponent(currentPiece);
	}

	public override void readData(FastBinaryReader reader)
	{
		swapper = reader.ReadComponent<Swapper>();
		currentPiece = reader.ReadComponent<SwapperPiece>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("swapper: " + $"{swapper}");
		stringBuilder.Append("currentPiece: " + $"{currentPiece}");
		return stringBuilder.ToString();
	}
}
