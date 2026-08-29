using System;
using System.Text;
using UnityEngine;

[Serializable]
public class CloudsData : IReadWrite
{
	public bool activeOnStart;

	public bool localOnly;

	public float transitionTime;

	public float opacity;

	public bool upperHemisphereOnly;

	public float opacityColorsR;

	public float opacityColorsG;

	public float opacityColorsB;

	public float opacityColorsA;

	public float rotation;

	public Color tint;

	public float exposure;

	public bool useWind;

	public float windSpeed;

	public float windDirection;

	public bool useWindShadows;

	public Color windShadowsColor;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in activeOnStart, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in localOnly, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in transitionTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in opacity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in upperHemisphereOnly, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in opacityColorsR, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in opacityColorsG, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in opacityColorsB, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in opacityColorsA, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in rotation, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in tint);
		writer.Write(in exposure, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useWind, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in windSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in windDirection, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useWindShadows, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in windShadowsColor);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		activeOnStart = reader.ReadBoolean();
		localOnly = reader.ReadBoolean();
		transitionTime = reader.ReadSingle();
		opacity = reader.ReadSingle();
		upperHemisphereOnly = reader.ReadBoolean();
		opacityColorsR = reader.ReadSingle();
		opacityColorsG = reader.ReadSingle();
		opacityColorsB = reader.ReadSingle();
		opacityColorsA = reader.ReadSingle();
		rotation = reader.ReadSingle();
		tint = reader.ReadColor();
		exposure = reader.ReadSingle();
		useWind = reader.ReadBoolean();
		windSpeed = reader.ReadSingle();
		windDirection = reader.ReadSingle();
		useWindShadows = reader.ReadBoolean();
		windShadowsColor = reader.ReadColor();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("activeOnStart: " + $"{activeOnStart}");
		stringBuilder.AppendLine("localOnly: " + $"{localOnly}");
		stringBuilder.AppendLine("transitionTime: " + $"{transitionTime}");
		stringBuilder.AppendLine("opacity: " + $"{opacity}");
		stringBuilder.AppendLine("upperHemisphereOnly: " + $"{upperHemisphereOnly}");
		stringBuilder.AppendLine("opacityColorsR: " + $"{opacityColorsR}");
		stringBuilder.AppendLine("opacityColorsG: " + $"{opacityColorsG}");
		stringBuilder.AppendLine("opacityColorsB: " + $"{opacityColorsB}");
		stringBuilder.AppendLine("opacityColorsA: " + $"{opacityColorsA}");
		stringBuilder.AppendLine("rotation: " + $"{rotation}");
		stringBuilder.AppendLine("tint: " + $"{tint}");
		stringBuilder.AppendLine("exposure: " + $"{exposure}");
		stringBuilder.AppendLine("useWind: " + $"{useWind}");
		stringBuilder.AppendLine("windSpeed: " + $"{windSpeed}");
		stringBuilder.AppendLine("windDirection: " + $"{windDirection}");
		stringBuilder.AppendLine("useWindShadows: " + $"{useWindShadows}");
		stringBuilder.Append("windShadowsColor: " + $"{windShadowsColor}");
		return stringBuilder.ToString();
	}
}
