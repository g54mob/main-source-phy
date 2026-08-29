using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class DialData : IReadWrite
{
	public Vector3 rotationAxis;

	public int valueCount;

	public bool snapToPosition;

	public int clickDirection;

	public bool useAngleLimits;

	public float angleLimit1;

	public float angleLimit2;

	public List<LockArgData> locks;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteVector3(in rotationAxis);
		writer.Write(in valueCount, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in snapToPosition, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in clickDirection, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useAngleLimits, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in angleLimit1, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in angleLimit2, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(locks, delegate(FastBinaryWriter w, LockArgData e)
		{
			w.WriteIReadWrite(e);
		});
	}

	public virtual void Read(FastBinaryReader reader)
	{
		rotationAxis = reader.ReadVector3();
		valueCount = reader.ReadInt32();
		snapToPosition = reader.ReadBoolean();
		clickDirection = reader.ReadInt32();
		useAngleLimits = reader.ReadBoolean();
		angleLimit1 = reader.ReadSingle();
		angleLimit2 = reader.ReadSingle();
		locks = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<LockArgData>());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("rotationAxis: " + $"{rotationAxis}");
		stringBuilder.AppendLine("valueCount: " + $"{valueCount}");
		stringBuilder.AppendLine("snapToPosition: " + $"{snapToPosition}");
		stringBuilder.AppendLine("clickDirection: " + $"{clickDirection}");
		stringBuilder.AppendLine("useAngleLimits: " + $"{useAngleLimits}");
		stringBuilder.AppendLine("angleLimit1: " + $"{angleLimit1}");
		stringBuilder.AppendLine("angleLimit2: " + $"{angleLimit2}");
		stringBuilder.Append("locks: " + ToStringHelper.Stringify(locks, (LockArgData e) => ToStringHelper.Stringify(e)));
		return stringBuilder.ToString();
	}
}
