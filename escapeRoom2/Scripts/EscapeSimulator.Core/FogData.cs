using System;
using System.Text;
using UnityEngine;

[Serializable]
public class FogData : IReadWrite
{
	public bool activeOnStart;

	public bool localOnly;

	public float transitionTime;

	public float attenuation;

	public float distance;

	public float maxHeight;

	public Color color;

	public bool isVolumetric;

	public Color volumetricColor;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in activeOnStart, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in localOnly, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in transitionTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in attenuation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in distance, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in maxHeight, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in color);
		writer.Write(in isVolumetric, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in volumetricColor);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		activeOnStart = reader.ReadBoolean();
		localOnly = reader.ReadBoolean();
		transitionTime = reader.ReadSingle();
		attenuation = reader.ReadSingle();
		distance = reader.ReadSingle();
		maxHeight = reader.ReadSingle();
		color = reader.ReadColor();
		isVolumetric = reader.ReadBoolean();
		volumetricColor = reader.ReadColor();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("activeOnStart: " + $"{activeOnStart}");
		stringBuilder.AppendLine("localOnly: " + $"{localOnly}");
		stringBuilder.AppendLine("transitionTime: " + $"{transitionTime}");
		stringBuilder.AppendLine("attenuation: " + $"{attenuation}");
		stringBuilder.AppendLine("distance: " + $"{distance}");
		stringBuilder.AppendLine("maxHeight: " + $"{maxHeight}");
		stringBuilder.AppendLine("color: " + $"{color}");
		stringBuilder.AppendLine("isVolumetric: " + $"{isVolumetric}");
		stringBuilder.Append("volumetricColor: " + $"{volumetricColor}");
		return stringBuilder.ToString();
	}
}
