using System.Text;

public sealed class InteractiveBackgroundPacket : Packet
{
	public Interactive interactive;

	public float dt;

	public override byte getTypeId()
	{
		return 129;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(interactive);
		writer.Write(in dt, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		interactive = reader.ReadComponent<Interactive>();
		dt = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("interactive: " + $"{interactive}");
		stringBuilder.Append("dt: " + $"{dt}");
		return stringBuilder.ToString();
	}
}
