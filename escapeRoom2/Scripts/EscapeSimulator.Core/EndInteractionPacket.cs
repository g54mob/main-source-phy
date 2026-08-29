using System.Text;

public sealed class EndInteractionPacket : Packet
{
	public Interactive interactable;

	public override byte getTypeId()
	{
		return 73;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(interactable);
	}

	public override void readData(FastBinaryReader reader)
	{
		interactable = reader.ReadComponent<Interactive>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("interactable: " + $"{interactable}");
		return stringBuilder.ToString();
	}
}
