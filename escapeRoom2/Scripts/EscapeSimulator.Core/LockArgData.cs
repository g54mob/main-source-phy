using System;
using System.Text;

[Serializable]
public class LockArgData : IReadWrite
{
	public InstanceID instanceID;

	public int passwordIndex;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteIReadWrite(instanceID);
		writer.Write(in passwordIndex, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		instanceID = reader.ReadIReadWrite<InstanceID>();
		passwordIndex = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("instanceID: " + ToStringHelper.Stringify(instanceID));
		stringBuilder.Append("passwordIndex: " + $"{passwordIndex}");
		return stringBuilder.ToString();
	}
}
