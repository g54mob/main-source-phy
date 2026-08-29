using System;
using System.Text;

[Serializable]
public class MaterialSwapData : IReadWrite
{
	public string asset;

	public string path;

	public int index;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(asset);
		writer.Write(path);
		writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		asset = reader.ReadString();
		path = reader.ReadString();
		index = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("asset: " + ToStringHelper.Stringify(asset));
		stringBuilder.AppendLine("path: " + ToStringHelper.Stringify(path));
		stringBuilder.Append("index: " + $"{index}");
		return stringBuilder.ToString();
	}
}
