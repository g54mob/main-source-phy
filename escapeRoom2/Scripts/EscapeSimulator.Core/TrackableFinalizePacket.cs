using System.Text;

public sealed class TrackableFinalizePacket : Packet
{
	public Trackable trackable;

	public override byte getTypeId()
	{
		return 59;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(trackable);
	}

	public override void readData(FastBinaryReader reader)
	{
		trackable = reader.ReadComponent<Trackable>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("trackable: " + $"{trackable}");
		return stringBuilder.ToString();
	}
}
