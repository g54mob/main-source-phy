using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class TweenStateRecordRoomEditor : IReadWrite
{
	public string name;

	public List<ObjectStateRoomEditor> states = new List<ObjectStateRoomEditor>();

	public float weight;

	public float targetWeight;

	public float speed = 1f;

	public float delay;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(name);
		writer.WriteList(states, delegate(FastBinaryWriter w, ObjectStateRoomEditor e)
		{
			w.WriteIReadWrite(e);
		});
		writer.Write(in weight, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetWeight, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in speed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in delay, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		name = reader.ReadString();
		states = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ObjectStateRoomEditor>());
		weight = reader.ReadSingle();
		targetWeight = reader.ReadSingle();
		speed = reader.ReadSingle();
		delay = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("name: " + ToStringHelper.Stringify(name));
		stringBuilder.AppendLine("states: " + ToStringHelper.Stringify(states, (ObjectStateRoomEditor e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("weight: " + $"{weight}");
		stringBuilder.AppendLine("targetWeight: " + $"{targetWeight}");
		stringBuilder.AppendLine("speed: " + $"{speed}");
		stringBuilder.Append("delay: " + $"{delay}");
		return stringBuilder.ToString();
	}
}
