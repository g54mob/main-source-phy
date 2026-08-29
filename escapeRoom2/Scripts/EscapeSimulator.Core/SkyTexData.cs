using System;
using System.Text;

[Serializable]
public class SkyTexData : IReadWrite
{
	public string fileName;

	public bool custom;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(fileName);
		writer.Write(in custom, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		fileName = reader.ReadString();
		custom = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("fileName: " + ToStringHelper.Stringify(fileName));
		stringBuilder.Append("custom: " + $"{custom}");
		return stringBuilder.ToString();
	}
}
