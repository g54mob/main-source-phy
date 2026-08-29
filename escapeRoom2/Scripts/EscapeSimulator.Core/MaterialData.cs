using System;
using System.Text;
using UnityEngine;

[Serializable]
public class MaterialData : IReadWrite
{
	public enum BlendingMode
	{
		Alpha = 0,
		Additive = 1,
		Multiply = 2
	}

	public string sourcePrefabId;

	public string sourceMaterialPath;

	public int sourceMaterialIndex;

	public string baseMapTexture;

	public Color baseMapColor = Color.white;

	public Vector2 textureScale = new Vector2(1f, 1f);

	public Vector2 textureOffset = new Vector2(0f, 0f);

	public string normalMapTexture;

	public float normalMapScale = 1f;

	public string maskMapTexture;

	public float metallicRemapMin;

	public float metallicRemapMax = 1f;

	public float metallic;

	public float smoothnessRemapMin;

	public float smoothnessRemapMax = 1f;

	public float smoothness = 0.5f;

	public string emissiveMapTexture;

	public Color emissiveMapColor = Color.black;

	public float emissiveMapScale = 1f;

	public int transparencyRenderOffset;

	public BlendingMode transparencyBlendingMode;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(sourcePrefabId);
		writer.Write(sourceMaterialPath);
		writer.Write(in sourceMaterialIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(baseMapTexture);
		writer.WriteColor(in baseMapColor);
		writer.WriteVector2(in textureScale);
		writer.WriteVector2(in textureOffset);
		writer.Write(normalMapTexture);
		writer.Write(in normalMapScale, default(FastBinaryWriter.ForPrimitives));
		writer.Write(maskMapTexture);
		writer.Write(in metallicRemapMin, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in metallicRemapMax, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in metallic, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in smoothnessRemapMin, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in smoothnessRemapMax, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in smoothness, default(FastBinaryWriter.ForPrimitives));
		writer.Write(emissiveMapTexture);
		writer.WriteColor(in emissiveMapColor);
		writer.Write(in emissiveMapScale, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in transparencyRenderOffset, default(FastBinaryWriter.ForPrimitives));
		int value = (int)transparencyBlendingMode;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		sourcePrefabId = reader.ReadString();
		sourceMaterialPath = reader.ReadString();
		sourceMaterialIndex = reader.ReadInt32();
		baseMapTexture = reader.ReadString();
		baseMapColor = reader.ReadColor();
		textureScale = reader.ReadVector2();
		textureOffset = reader.ReadVector2();
		normalMapTexture = reader.ReadString();
		normalMapScale = reader.ReadSingle();
		maskMapTexture = reader.ReadString();
		metallicRemapMin = reader.ReadSingle();
		metallicRemapMax = reader.ReadSingle();
		metallic = reader.ReadSingle();
		smoothnessRemapMin = reader.ReadSingle();
		smoothnessRemapMax = reader.ReadSingle();
		smoothness = reader.ReadSingle();
		emissiveMapTexture = reader.ReadString();
		emissiveMapColor = reader.ReadColor();
		emissiveMapScale = reader.ReadSingle();
		transparencyRenderOffset = reader.ReadInt32();
		transparencyBlendingMode = (BlendingMode)reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("sourcePrefabId: " + ToStringHelper.Stringify(sourcePrefabId));
		stringBuilder.AppendLine("sourceMaterialPath: " + ToStringHelper.Stringify(sourceMaterialPath));
		stringBuilder.AppendLine("sourceMaterialIndex: " + $"{sourceMaterialIndex}");
		stringBuilder.AppendLine("baseMapTexture: " + ToStringHelper.Stringify(baseMapTexture));
		stringBuilder.AppendLine("baseMapColor: " + $"{baseMapColor}");
		stringBuilder.AppendLine("textureScale: " + $"{textureScale}");
		stringBuilder.AppendLine("textureOffset: " + $"{textureOffset}");
		stringBuilder.AppendLine("normalMapTexture: " + ToStringHelper.Stringify(normalMapTexture));
		stringBuilder.AppendLine("normalMapScale: " + $"{normalMapScale}");
		stringBuilder.AppendLine("maskMapTexture: " + ToStringHelper.Stringify(maskMapTexture));
		stringBuilder.AppendLine("metallicRemapMin: " + $"{metallicRemapMin}");
		stringBuilder.AppendLine("metallicRemapMax: " + $"{metallicRemapMax}");
		stringBuilder.AppendLine("metallic: " + $"{metallic}");
		stringBuilder.AppendLine("smoothnessRemapMin: " + $"{smoothnessRemapMin}");
		stringBuilder.AppendLine("smoothnessRemapMax: " + $"{smoothnessRemapMax}");
		stringBuilder.AppendLine("smoothness: " + $"{smoothness}");
		stringBuilder.AppendLine("emissiveMapTexture: " + ToStringHelper.Stringify(emissiveMapTexture));
		stringBuilder.AppendLine("emissiveMapColor: " + $"{emissiveMapColor}");
		stringBuilder.AppendLine("emissiveMapScale: " + $"{emissiveMapScale}");
		stringBuilder.AppendLine("transparencyRenderOffset: " + $"{transparencyRenderOffset}");
		stringBuilder.Append("transparencyBlendingMode: " + $"{transparencyBlendingMode}");
		return stringBuilder.ToString();
	}
}
