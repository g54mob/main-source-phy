using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class PuzzleData : IReadWrite
{
	public string puzzleName;

	public List<int> hints;

	public List<InstanceID> conditions;

	public bool muteSound;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(puzzleName);
		writer.WriteList(hints, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(conditions, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		writer.Write(in muteSound, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		puzzleName = reader.ReadString();
		hints = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		conditions = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		muteSound = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("puzzleName: " + ToStringHelper.Stringify(puzzleName));
		stringBuilder.AppendLine("hints: " + ToStringHelper.Stringify(hints, (int e) => $"{e}"));
		stringBuilder.AppendLine("conditions: " + ToStringHelper.Stringify(conditions, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.Append("muteSound: " + $"{muteSound}");
		return stringBuilder.ToString();
	}
}
