using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class EditorSetupData : IReadWrite
{
	public List<InstanceID> targets;

	public EditorSetup.SetupDifficulty difficulty;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteList(targets, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		int value = (int)difficulty;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		targets = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		difficulty = (EditorSetup.SetupDifficulty)reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("targets: " + ToStringHelper.Stringify(targets, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.Append("difficulty: " + $"{difficulty}");
		return stringBuilder.ToString();
	}
}
