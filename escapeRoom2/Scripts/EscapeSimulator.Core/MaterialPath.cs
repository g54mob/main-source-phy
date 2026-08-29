using System;
using System.Text;

[Serializable]
[Obsolete("Use MaterialSwapData instead.")]
public class MaterialPath : IReadWrite
{
	public string path;

	public int index;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(path);
		writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		path = reader.ReadString();
		index = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("path: " + ToStringHelper.Stringify(path));
		stringBuilder.Append("index: " + $"{index}");
		return stringBuilder.ToString();
	}
}
