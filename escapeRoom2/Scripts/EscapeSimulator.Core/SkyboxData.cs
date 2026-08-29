using System;
using System.Text;
using UnityEngine;

[Serializable]
public class SkyboxData : IReadWrite
{
	public SkyboxType type;

	public bool activeOnStart;

	public bool localOnly;

	public float transitionTime;

	[Header("Procedural")]
	public Color topColor;

	public Color midColor;

	public Color botColor;

	public float horizonSize;

	public float exposure;

	[Header("6 sided")]
	public float exposure2;

	public int rotation;

	public SkyTexData textureData;

	[DontSave]
	public Cubemap tempTexture;

	[Header("Physically based")]
	public float aersolDensity;

	public float alphaMultiplier;

	public virtual void Write(FastBinaryWriter writer)
	{
		int value = (int)type;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in activeOnStart, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in localOnly, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in transitionTime, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in topColor);
		writer.WriteColor(in midColor);
		writer.WriteColor(in botColor);
		writer.Write(in horizonSize, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in exposure, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in exposure2, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in rotation, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(textureData);
		writer.Write(in aersolDensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in alphaMultiplier, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		type = (SkyboxType)reader.ReadInt32();
		activeOnStart = reader.ReadBoolean();
		localOnly = reader.ReadBoolean();
		transitionTime = reader.ReadSingle();
		topColor = reader.ReadColor();
		midColor = reader.ReadColor();
		botColor = reader.ReadColor();
		horizonSize = reader.ReadSingle();
		exposure = reader.ReadSingle();
		exposure2 = reader.ReadSingle();
		rotation = reader.ReadInt32();
		textureData = reader.ReadIReadWrite<SkyTexData>();
		aersolDensity = reader.ReadSingle();
		alphaMultiplier = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("type: " + $"{type}");
		stringBuilder.AppendLine("activeOnStart: " + $"{activeOnStart}");
		stringBuilder.AppendLine("localOnly: " + $"{localOnly}");
		stringBuilder.AppendLine("transitionTime: " + $"{transitionTime}");
		stringBuilder.AppendLine("topColor: " + $"{topColor}");
		stringBuilder.AppendLine("midColor: " + $"{midColor}");
		stringBuilder.AppendLine("botColor: " + $"{botColor}");
		stringBuilder.AppendLine("horizonSize: " + $"{horizonSize}");
		stringBuilder.AppendLine("exposure: " + $"{exposure}");
		stringBuilder.AppendLine("exposure2: " + $"{exposure2}");
		stringBuilder.AppendLine("rotation: " + $"{rotation}");
		stringBuilder.AppendLine("textureData: " + ToStringHelper.Stringify(textureData));
		stringBuilder.AppendLine("aersolDensity: " + $"{aersolDensity}");
		stringBuilder.Append("alphaMultiplier: " + $"{alphaMultiplier}");
		return stringBuilder.ToString();
	}
}
