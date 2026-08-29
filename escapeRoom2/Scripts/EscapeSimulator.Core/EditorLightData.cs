using System;
using System.Text;
using UnityEngine;

[Serializable]
public class EditorLightData : IReadWrite
{
	public LightType type;

	public float range;

	public float spotAngle;

	public Color color;

	public float intensity;

	public bool castShadows;

	public float shadowDimmer = 1f;

	public string cookieTextureId;

	public virtual void Write(FastBinaryWriter writer)
	{
		int value = (int)type;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in range, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in spotAngle, default(FastBinaryWriter.ForPrimitives));
		writer.WriteColor(in color);
		writer.Write(in intensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in castShadows, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in shadowDimmer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(cookieTextureId);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		type = (LightType)reader.ReadInt32();
		range = reader.ReadSingle();
		spotAngle = reader.ReadSingle();
		color = reader.ReadColor();
		intensity = reader.ReadSingle();
		castShadows = reader.ReadBoolean();
		shadowDimmer = reader.ReadSingle();
		cookieTextureId = reader.ReadString();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("type: " + $"{type}");
		stringBuilder.AppendLine("range: " + $"{range}");
		stringBuilder.AppendLine("spotAngle: " + $"{spotAngle}");
		stringBuilder.AppendLine("color: " + $"{color}");
		stringBuilder.AppendLine("intensity: " + $"{intensity}");
		stringBuilder.AppendLine("castShadows: " + $"{castShadows}");
		stringBuilder.AppendLine("shadowDimmer: " + $"{shadowDimmer}");
		stringBuilder.Append("cookieTextureId: " + ToStringHelper.Stringify(cookieTextureId));
		return stringBuilder.ToString();
	}
}
