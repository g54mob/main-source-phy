using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class LookableData : IReadWrite
{
	public List<InstanceID> onActivated;

	public List<InstanceID> onDeactivated;

	public float targetVisibilityPercent;

	public float targetScreenPercent;

	public bool activateOnlyOnce;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteList(onActivated, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(onDeactivated, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		writer.Write(in targetVisibilityPercent, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetScreenPercent, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in activateOnlyOnce, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		onActivated = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		onDeactivated = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		targetVisibilityPercent = reader.ReadSingle();
		targetScreenPercent = reader.ReadSingle();
		activateOnlyOnce = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("onActivated: " + ToStringHelper.Stringify(onActivated, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("onDeactivated: " + ToStringHelper.Stringify(onDeactivated, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("targetVisibilityPercent: " + $"{targetVisibilityPercent}");
		stringBuilder.AppendLine("targetScreenPercent: " + $"{targetScreenPercent}");
		stringBuilder.Append("activateOnlyOnce: " + $"{activateOnlyOnce}");
		return stringBuilder.ToString();
	}
}
