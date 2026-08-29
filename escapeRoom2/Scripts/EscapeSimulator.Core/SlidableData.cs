using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class SlidableData : IReadWrite
{
	public TransformData endNode;

	public Slidable.SnapMode snapMode;

	public int additionalSnappingPoints;

	public float snapAnimationDuration;

	public int startingIndex;

	public List<LockArgData> locks;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteIReadWrite(endNode);
		int value = (int)snapMode;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in additionalSnappingPoints, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in snapAnimationDuration, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in startingIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(locks, delegate(FastBinaryWriter w, LockArgData e)
		{
			w.WriteIReadWrite(e);
		});
	}

	public virtual void Read(FastBinaryReader reader)
	{
		endNode = reader.ReadIReadWrite<TransformData>();
		snapMode = (Slidable.SnapMode)reader.ReadInt32();
		additionalSnappingPoints = reader.ReadInt32();
		snapAnimationDuration = reader.ReadSingle();
		startingIndex = reader.ReadInt32();
		locks = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<LockArgData>());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("endNode: " + ToStringHelper.Stringify(endNode));
		stringBuilder.AppendLine("snapMode: " + $"{snapMode}");
		stringBuilder.AppendLine("additionalSnappingPoints: " + $"{additionalSnappingPoints}");
		stringBuilder.AppendLine("snapAnimationDuration: " + $"{snapAnimationDuration}");
		stringBuilder.AppendLine("startingIndex: " + $"{startingIndex}");
		stringBuilder.Append("locks: " + ToStringHelper.Stringify(locks, (LockArgData e) => ToStringHelper.Stringify(e)));
		return stringBuilder.ToString();
	}
}
