using System.Text;

public sealed class SyncTransformPacket : Packet
{
	public TransformSnapshot snapshot;

	public override void writeData(FastBinaryWriter writer)
	{
		bool value = snapshot != null && snapshot.targetObject != null && snapshot.targetObject.GetComponent<TransformSync>() != null;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		if (value)
		{
			writer.WriteTransformSnapshot(snapshot);
		}
	}

	public override void readData(FastBinaryReader reader)
	{
		if (reader.ReadBoolean())
		{
			snapshot = reader.ReadTransformSnapshot();
		}
	}

	public override byte getTypeId()
	{
		return 23;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("snapshot: " + $"{snapshot}");
		return stringBuilder.ToString();
	}
}
