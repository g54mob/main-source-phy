using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class DelayData : IReadWrite
{
	public List<InstanceID> targets;

	public float delay = 1f;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteList(targets, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		writer.Write(in delay, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		targets = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		delay = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("targets: " + ToStringHelper.Stringify(targets, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.Append("delay: " + $"{delay}");
		return stringBuilder.ToString();
	}
}
