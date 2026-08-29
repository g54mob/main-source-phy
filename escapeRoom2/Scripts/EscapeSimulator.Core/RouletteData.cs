using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class RouletteData : IReadWrite
{
	public List<InstanceID> targets;

	public bool removeTargetOnTrigger;

	public int testingTarget;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteList(targets, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		writer.Write(in removeTargetOnTrigger, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in testingTarget, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		targets = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		removeTargetOnTrigger = reader.ReadBoolean();
		testingTarget = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("targets: " + ToStringHelper.Stringify(targets, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("removeTargetOnTrigger: " + $"{removeTargetOnTrigger}");
		stringBuilder.Append("testingTarget: " + $"{testingTarget}");
		return stringBuilder.ToString();
	}
}
