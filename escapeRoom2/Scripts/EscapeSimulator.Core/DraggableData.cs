using System;
using System.Text;
using UnityEngine;

[Serializable]
public class DraggableData : IReadWrite
{
	public RigidbodyConstraints dragConstraintFlags;

	public Draggable.ManipulationType manipulationType;

	public Draggable.ForcePoint forcePoint;

	public virtual void Write(FastBinaryWriter writer)
	{
		int value = (int)dragConstraintFlags;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)manipulationType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)forcePoint;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		dragConstraintFlags = (RigidbodyConstraints)reader.ReadInt32();
		manipulationType = (Draggable.ManipulationType)reader.ReadInt32();
		forcePoint = (Draggable.ForcePoint)reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("dragConstraintFlags: " + $"{dragConstraintFlags}");
		stringBuilder.AppendLine("manipulationType: " + $"{manipulationType}");
		stringBuilder.Append("forcePoint: " + $"{forcePoint}");
		return stringBuilder.ToString();
	}
}
