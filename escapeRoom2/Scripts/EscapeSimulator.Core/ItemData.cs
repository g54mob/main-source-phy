using System;
using System.Text;

[Serializable]
public class ItemData : IReadWrite
{
	public ItemType itemType;

	public bool carriable;

	public bool hasRigidbody;

	public virtual void Write(FastBinaryWriter writer)
	{
		int value = (int)itemType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in carriable, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hasRigidbody, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		itemType = (ItemType)reader.ReadInt32();
		carriable = reader.ReadBoolean();
		hasRigidbody = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("itemType: " + $"{itemType}");
		stringBuilder.AppendLine("carriable: " + $"{carriable}");
		stringBuilder.Append("hasRigidbody: " + $"{hasRigidbody}");
		return stringBuilder.ToString();
	}
}
