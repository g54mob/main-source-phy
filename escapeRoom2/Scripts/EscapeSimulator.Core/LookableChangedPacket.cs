using System.Text;

public sealed class LookableChangedPacket : Packet
{
	public Lookable lookable;

	public bool newState;

	public override byte getTypeId()
	{
		return 98;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(lookable);
		writer.Write(in newState, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		lookable = reader.ReadComponent<Lookable>();
		newState = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("lookable: " + $"{lookable}");
		stringBuilder.Append("newState: " + $"{newState}");
		return stringBuilder.ToString();
	}
}
