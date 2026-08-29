using System.Text;

public sealed class ConfirmStartInteractionPacket : Packet
{
	public Interactive interactable;

	public int confirmedInteractionId;

	public override byte getTypeId()
	{
		return 72;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(interactable);
		writer.Write(in confirmedInteractionId, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		interactable = reader.ReadComponent<Interactive>();
		confirmedInteractionId = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("interactable: " + $"{interactable}");
		stringBuilder.Append("confirmedInteractionId: " + $"{confirmedInteractionId}");
		return stringBuilder.ToString();
	}
}
