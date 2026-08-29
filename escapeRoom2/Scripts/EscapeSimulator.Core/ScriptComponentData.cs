using System;
using System.Text;

[Serializable]
public class ScriptComponentData : IReadWrite
{
	public string scriptLocation;

	public string functionToCall;

	public bool canBeTriggered;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(scriptLocation);
		writer.Write(functionToCall);
		writer.Write(in canBeTriggered, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		scriptLocation = reader.ReadString();
		functionToCall = reader.ReadString();
		canBeTriggered = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("scriptLocation: " + ToStringHelper.Stringify(scriptLocation));
		stringBuilder.AppendLine("functionToCall: " + ToStringHelper.Stringify(functionToCall));
		stringBuilder.Append("canBeTriggered: " + $"{canBeTriggered}");
		return stringBuilder.ToString();
	}
}
