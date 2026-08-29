using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
[Obsolete("Use MaterialSwapData instead.")]
public class SwapMaterialData : IReadWrite
{
	public string materialAssetFileName;

	public List<MaterialPath> materialPaths;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(materialAssetFileName);
		writer.WriteList(materialPaths, delegate(FastBinaryWriter w, MaterialPath e)
		{
			w.WriteIReadWrite(e);
		});
	}

	public virtual void Read(FastBinaryReader reader)
	{
		materialAssetFileName = reader.ReadString();
		materialPaths = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<MaterialPath>());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("materialAssetFileName: " + ToStringHelper.Stringify(materialAssetFileName));
		stringBuilder.Append("materialPaths: " + ToStringHelper.Stringify(materialPaths, (MaterialPath e) => ToStringHelper.Stringify(e)));
		return stringBuilder.ToString();
	}
}
