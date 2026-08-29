using System.Text;

public sealed class NpcZoomEnterPacket : Packet
{
	public Npc npc;

	public override byte getTypeId()
	{
		return 102;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(npc);
	}

	public override void readData(FastBinaryReader reader)
	{
		npc = reader.ReadComponent<Npc>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("npc: " + $"{npc}");
		return stringBuilder.ToString();
	}
}
