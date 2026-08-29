using System.Text;

public sealed class NpcDialoguePacket : Packet
{
	public Npc npc;

	public int orderIndex;

	public int choiceIndex;

	public override byte getTypeId()
	{
		return 103;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(npc);
		writer.Write(in orderIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in choiceIndex, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		npc = reader.ReadComponent<Npc>();
		orderIndex = reader.ReadInt32();
		choiceIndex = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("npc: " + $"{npc}");
		stringBuilder.AppendLine("orderIndex: " + $"{orderIndex}");
		stringBuilder.Append("choiceIndex: " + $"{choiceIndex}");
		return stringBuilder.ToString();
	}
}
