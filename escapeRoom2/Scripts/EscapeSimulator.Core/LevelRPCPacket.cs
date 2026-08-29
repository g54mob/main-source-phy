using System.Text;

public sealed class LevelRPCPacket : Packet
{
	public int rpcType;

	public override byte getTypeId()
	{
		return 101;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in rpcType, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		rpcType = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("rpcType: " + $"{rpcType}");
		return stringBuilder.ToString();
	}
}
