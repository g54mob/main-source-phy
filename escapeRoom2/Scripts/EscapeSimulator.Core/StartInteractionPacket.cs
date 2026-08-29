using System.Text;

public sealed class StartInteractionPacket : Packet
{
	public Interactive interactable;

	public int requestedInteractionId;

	public byte[] initialState;

	public override byte getTypeId()
	{
		return 71;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(interactable);
		writer.Write(in requestedInteractionId, default(FastBinaryWriter.ForPrimitives));
		writer.WriteByteArray(initialState);
	}

	public override void readData(FastBinaryReader reader)
	{
		interactable = reader.ReadComponent<Interactive>();
		requestedInteractionId = reader.ReadInt32();
		initialState = reader.ReadByteArray();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("interactable: " + $"{interactable}");
		stringBuilder.AppendLine("requestedInteractionId: " + $"{requestedInteractionId}");
		stringBuilder.Append("initialState: " + ToStringHelper.Stringify(initialState, (byte e) => $"{e}"));
		return stringBuilder.ToString();
	}
}
