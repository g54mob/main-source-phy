using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class RoomEditorLinksData : IReadWrite
{
	public List<LockArgData> locks = new List<LockArgData>();

	public List<InstanceID> targets = new List<InstanceID>();

	public int output = 1;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteList(locks, delegate(FastBinaryWriter w, LockArgData e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(targets, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		writer.Write(in output, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		locks = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<LockArgData>());
		targets = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		output = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("locks: " + ToStringHelper.Stringify(locks, (LockArgData e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("targets: " + ToStringHelper.Stringify(targets, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.Append("output: " + $"{output}");
		return stringBuilder.ToString();
	}
}
