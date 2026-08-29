using System.Text;

public sealed class TurnableFinalizePacket : Packet
{
	public Turnable turnable;

	public float rotation;

	public override byte getTypeId()
	{
		return 56;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(turnable);
		writer.Write(in rotation, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		turnable = reader.ReadComponent<Turnable>();
		rotation = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("turnable: " + $"{turnable}");
		stringBuilder.Append("rotation: " + $"{rotation}");
		return stringBuilder.ToString();
	}
}
