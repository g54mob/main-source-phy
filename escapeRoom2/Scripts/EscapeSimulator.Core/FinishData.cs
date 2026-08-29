using System;
using System.Text;
using UnityEngine;

[Serializable]
public class FinishData : IReadWrite
{
	public Vector3 eyePosition;

	public Vector3 eyeRotation;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteVector3(in eyePosition);
		writer.WriteVector3(in eyeRotation);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		eyePosition = reader.ReadVector3();
		eyeRotation = reader.ReadVector3();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("eyePosition: " + $"{eyePosition}");
		stringBuilder.Append("eyeRotation: " + $"{eyeRotation}");
		return stringBuilder.ToString();
	}
}
