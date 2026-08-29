using System;
using System.Text;
using UnityEngine;

[Serializable]
public class EditorPostProcessingData : IReadWrite
{
	public float exposure;

	public float ssaoIntensity;

	public float ssaoRadius;

	public float bloomThreshold;

	public float bloomIntensity;

	public float colorAdjustmentContrast;

	public Color colorAdjustmentColorFilter;

	public float colorAdjustmentHue;

	public float colorAdjustmentSaturation;

	public float chromaticAberrationIntensity;

	public float vignetteIntensity;

	public float grainIntensity;

	public float motionBlurIntensity;

	public bool activeOnStart;

	public bool localOnly;

	public float transitionTime;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in exposure, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ssaoIntensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ssaoRadius, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in bloomThreshold, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in bloomIntensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in colorAdjustmentContrast, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in colorAdjustmentColorFilter);
		writer.Write(in colorAdjustmentHue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in colorAdjustmentSaturation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in chromaticAberrationIntensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in vignetteIntensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in grainIntensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in motionBlurIntensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in activeOnStart, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in localOnly, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in transitionTime, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		exposure = reader.ReadSingle();
		ssaoIntensity = reader.ReadSingle();
		ssaoRadius = reader.ReadSingle();
		bloomThreshold = reader.ReadSingle();
		bloomIntensity = reader.ReadSingle();
		colorAdjustmentContrast = reader.ReadSingle();
		colorAdjustmentColorFilter = reader.ReadColor();
		colorAdjustmentHue = reader.ReadSingle();
		colorAdjustmentSaturation = reader.ReadSingle();
		chromaticAberrationIntensity = reader.ReadSingle();
		vignetteIntensity = reader.ReadSingle();
		grainIntensity = reader.ReadSingle();
		motionBlurIntensity = reader.ReadSingle();
		activeOnStart = reader.ReadBoolean();
		localOnly = reader.ReadBoolean();
		transitionTime = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("exposure: " + $"{exposure}");
		stringBuilder.AppendLine("ssaoIntensity: " + $"{ssaoIntensity}");
		stringBuilder.AppendLine("ssaoRadius: " + $"{ssaoRadius}");
		stringBuilder.AppendLine("bloomThreshold: " + $"{bloomThreshold}");
		stringBuilder.AppendLine("bloomIntensity: " + $"{bloomIntensity}");
		stringBuilder.AppendLine("colorAdjustmentContrast: " + $"{colorAdjustmentContrast}");
		stringBuilder.AppendLine("colorAdjustmentColorFilter: " + $"{colorAdjustmentColorFilter}");
		stringBuilder.AppendLine("colorAdjustmentHue: " + $"{colorAdjustmentHue}");
		stringBuilder.AppendLine("colorAdjustmentSaturation: " + $"{colorAdjustmentSaturation}");
		stringBuilder.AppendLine("chromaticAberrationIntensity: " + $"{chromaticAberrationIntensity}");
		stringBuilder.AppendLine("vignetteIntensity: " + $"{vignetteIntensity}");
		stringBuilder.AppendLine("grainIntensity: " + $"{grainIntensity}");
		stringBuilder.AppendLine("motionBlurIntensity: " + $"{motionBlurIntensity}");
		stringBuilder.AppendLine("activeOnStart: " + $"{activeOnStart}");
		stringBuilder.AppendLine("localOnly: " + $"{localOnly}");
		stringBuilder.Append("transitionTime: " + $"{transitionTime}");
		return stringBuilder.ToString();
	}
}
