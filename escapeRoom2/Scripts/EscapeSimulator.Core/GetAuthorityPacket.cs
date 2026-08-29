using System.Text;

public sealed class GetAuthorityPacket : Packet
{
	public Interactive target;

	public override byte getTypeId()
	{
		return 22;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(target);
	}

	public override void readData(FastBinaryReader reader)
	{
		target = reader.ReadComponent<Interactive>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("target: " + $"{target}");
		return stringBuilder.ToString();
	}
}
