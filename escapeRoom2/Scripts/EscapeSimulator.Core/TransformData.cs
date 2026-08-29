using System;
using System.Text;
using UnityEngine;

[Serializable]
public class TransformData : IReadWrite
{
	public bool isLocal;

	public Vector3 position;

	public Vector3 rotation;

	public Vector3 scale;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in isLocal, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in position);
		writer.WriteVector3(in rotation);
		writer.WriteVector3(in scale);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		isLocal = reader.ReadBoolean();
		position = reader.ReadVector3();
		rotation = reader.ReadVector3();
		scale = reader.ReadVector3();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("isLocal: " + $"{isLocal}");
		stringBuilder.AppendLine("position: " + $"{position}");
		stringBuilder.AppendLine("rotation: " + $"{rotation}");
		stringBuilder.Append("scale: " + $"{scale}");
		return stringBuilder.ToString();
	}
}
