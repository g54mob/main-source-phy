using System.Text;

public sealed class Switch3DFinalizePacket : Packet
{
	public Switch3D switch3D;

	public override byte getTypeId()
	{
		return 36;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(switch3D);
	}

	public override void readData(FastBinaryReader reader)
	{
		switch3D = reader.ReadComponent<Switch3D>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("switch3D: " + $"{switch3D}");
		return stringBuilder.ToString();
	}
}
