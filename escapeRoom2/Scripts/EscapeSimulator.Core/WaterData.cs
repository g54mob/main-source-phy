using System;
using System.Text;
using UnityEngine;

[Serializable]
public class WaterData : IReadWrite
{
	public float timeScale = 1f;

	public float localWindSpeed = 1f;

	public float chaos = 0.8f;

	public float rippleSpeed = 1f;

	public Color refractionColor = Color.white;

	public Color waterColor = Color.white;

	public float ambientIntensity;

	public EditorWater.WaterGeometryType geometryType;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in timeScale, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in localWindSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in chaos, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in rippleSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in refractionColor);
		writer.WriteColor(in waterColor);
		writer.Write(in ambientIntensity, default(FastBinaryWriter.ForPrimitives));
		int value = (int)geometryType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		timeScale = reader.ReadSingle();
		localWindSpeed = reader.ReadSingle();
		chaos = reader.ReadSingle();
		rippleSpeed = reader.ReadSingle();
		refractionColor = reader.ReadColor();
		waterColor = reader.ReadColor();
		ambientIntensity = reader.ReadSingle();
		geometryType = (EditorWater.WaterGeometryType)reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("timeScale: " + $"{timeScale}");
		stringBuilder.AppendLine("localWindSpeed: " + $"{localWindSpeed}");
		stringBuilder.AppendLine("chaos: " + $"{chaos}");
		stringBuilder.AppendLine("rippleSpeed: " + $"{rippleSpeed}");
		stringBuilder.AppendLine("refractionColor: " + $"{refractionColor}");
		stringBuilder.AppendLine("waterColor: " + $"{waterColor}");
		stringBuilder.AppendLine("ambientIntensity: " + $"{ambientIntensity}");
		stringBuilder.Append("geometryType: " + $"{geometryType}");
		return stringBuilder.ToString();
	}
}
