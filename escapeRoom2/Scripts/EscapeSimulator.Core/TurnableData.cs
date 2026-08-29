using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class TurnableData : IReadWrite
{
	public Vector3 worldAxis;

	public Vector2 screenAxis;

	public int steps;

	public bool useHotspots;

	public bool useAngleLimits;

	public float angleLimit1;

	public float angleLimit2;

	public int clickDirection;

	public TurnableType type;

	public float speed;

	public List<LockArgData> locks;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteVector3(in worldAxis);
		writer.WriteVector2(in screenAxis);
		writer.Write(in steps, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useHotspots, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useAngleLimits, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in angleLimit1, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in angleLimit2, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in clickDirection, default(FastBinaryWriter.ForPrimitives));
		int value = (int)type;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in speed, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(locks, delegate(FastBinaryWriter w, LockArgData e)
		{
			w.WriteIReadWrite(e);
		});
	}

	public virtual void Read(FastBinaryReader reader)
	{
		worldAxis = reader.ReadVector3();
		screenAxis = reader.ReadVector2();
		steps = reader.ReadInt32();
		useHotspots = reader.ReadBoolean();
		useAngleLimits = reader.ReadBoolean();
		angleLimit1 = reader.ReadSingle();
		angleLimit2 = reader.ReadSingle();
		clickDirection = reader.ReadInt32();
		type = (TurnableType)reader.ReadInt32();
		speed = reader.ReadSingle();
		locks = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<LockArgData>());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("worldAxis: " + $"{worldAxis}");
		stringBuilder.AppendLine("screenAxis: " + $"{screenAxis}");
		stringBuilder.AppendLine("steps: " + $"{steps}");
		stringBuilder.AppendLine("useHotspots: " + $"{useHotspots}");
		stringBuilder.AppendLine("useAngleLimits: " + $"{useAngleLimits}");
		stringBuilder.AppendLine("angleLimit1: " + $"{angleLimit1}");
		stringBuilder.AppendLine("angleLimit2: " + $"{angleLimit2}");
		stringBuilder.AppendLine("clickDirection: " + $"{clickDirection}");
		stringBuilder.AppendLine("type: " + $"{type}");
		stringBuilder.AppendLine("speed: " + $"{speed}");
		stringBuilder.Append("locks: " + ToStringHelper.Stringify(locks, (LockArgData e) => ToStringHelper.Stringify(e)));
		return stringBuilder.ToString();
	}
}
