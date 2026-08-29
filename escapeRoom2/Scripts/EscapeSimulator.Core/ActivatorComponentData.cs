using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class ActivatorComponentData : IReadWrite
{
	public bool activeOnStart;

	public List<InstanceID> keys;

	public bool removeFromInventoryOnDisable;

	public List<InstanceID> targets;

	public ActivatorComponent.ActivatorType type;

	public bool targetObject;

	public bool targetRenderer;

	public bool targetCollider;

	public bool targetObstacle;

	public bool targetTargetable;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in activeOnStart, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(keys, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		writer.Write(in removeFromInventoryOnDisable, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(targets, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		int value = (int)type;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetObject, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetRenderer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetCollider, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetObstacle, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetTargetable, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		activeOnStart = reader.ReadBoolean();
		keys = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		removeFromInventoryOnDisable = reader.ReadBoolean();
		targets = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		type = (ActivatorComponent.ActivatorType)reader.ReadInt32();
		targetObject = reader.ReadBoolean();
		targetRenderer = reader.ReadBoolean();
		targetCollider = reader.ReadBoolean();
		targetObstacle = reader.ReadBoolean();
		targetTargetable = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("activeOnStart: " + $"{activeOnStart}");
		stringBuilder.AppendLine("keys: " + ToStringHelper.Stringify(keys, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("removeFromInventoryOnDisable: " + $"{removeFromInventoryOnDisable}");
		stringBuilder.AppendLine("targets: " + ToStringHelper.Stringify(targets, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("type: " + $"{type}");
		stringBuilder.AppendLine("targetObject: " + $"{targetObject}");
		stringBuilder.AppendLine("targetRenderer: " + $"{targetRenderer}");
		stringBuilder.AppendLine("targetCollider: " + $"{targetCollider}");
		stringBuilder.AppendLine("targetObstacle: " + $"{targetObstacle}");
		stringBuilder.Append("targetTargetable: " + $"{targetTargetable}");
		return stringBuilder.ToString();
	}
}
