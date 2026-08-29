using System;
using System.Text;
using UnityEngine;

[Serializable]
public class OceanData : IReadWrite
{
	public bool activeOnStart;

	public bool localOnly;

	public float transitionTime;

	public float distantWindSize = 30f;

	public float chaos = 0.8f;

	public float currentSpeed;

	public float currentDirection;

	public float amplitudeModifier = 1f;

	public float rippleSpeed = 8f;

	public Color refractionColor = Color.white;

	public Color waterColor = Color.white;

	public float ambientIntensity;

	public float height;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in activeOnStart, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in localOnly, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in transitionTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in distantWindSize, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in chaos, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentDirection, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in amplitudeModifier, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in rippleSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in refractionColor);
		writer.WriteColor(in waterColor);
		writer.Write(in ambientIntensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in height, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		activeOnStart = reader.ReadBoolean();
		localOnly = reader.ReadBoolean();
		transitionTime = reader.ReadSingle();
		distantWindSize = reader.ReadSingle();
		chaos = reader.ReadSingle();
		currentSpeed = reader.ReadSingle();
		currentDirection = reader.ReadSingle();
		amplitudeModifier = reader.ReadSingle();
		rippleSpeed = reader.ReadSingle();
		refractionColor = reader.ReadColor();
		waterColor = reader.ReadColor();
		ambientIntensity = reader.ReadSingle();
		height = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("activeOnStart: " + $"{activeOnStart}");
		stringBuilder.AppendLine("localOnly: " + $"{localOnly}");
		stringBuilder.AppendLine("transitionTime: " + $"{transitionTime}");
		stringBuilder.AppendLine("distantWindSize: " + $"{distantWindSize}");
		stringBuilder.AppendLine("chaos: " + $"{chaos}");
		stringBuilder.AppendLine("currentSpeed: " + $"{currentSpeed}");
		stringBuilder.AppendLine("currentDirection: " + $"{currentDirection}");
		stringBuilder.AppendLine("amplitudeModifier: " + $"{amplitudeModifier}");
		stringBuilder.AppendLine("rippleSpeed: " + $"{rippleSpeed}");
		stringBuilder.AppendLine("refractionColor: " + $"{refractionColor}");
		stringBuilder.AppendLine("waterColor: " + $"{waterColor}");
		stringBuilder.AppendLine("ambientIntensity: " + $"{ambientIntensity}");
		stringBuilder.Append("height: " + $"{height}");
		return stringBuilder.ToString();
	}
}
