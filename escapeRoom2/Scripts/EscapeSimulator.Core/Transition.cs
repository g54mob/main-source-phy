using System.Text;
using UnityEngine;

public abstract class Transition : Timer
{
	public Transform transform;

	public bool isGlobal;

	public Interpolation interpolation;

	public Vector3 originalPosition;

	public Quaternion originalRotation;

	public Vector3 originalScale;

	public Vector3 targetPosition;

	public Quaternion targetRotation;

	public Vector3 targetScale;

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(transform);
		writer.Write(in isGlobal, default(FastBinaryWriter.ForPrimitives));
		writer.Write((byte)interpolation);
		writer.Write(in originalPosition, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in originalRotation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in originalScale, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetPosition, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetRotation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in targetScale, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		transform = reader.ReadComponent<Transform>();
		isGlobal = reader.ReadBoolean();
		interpolation = (Interpolation)reader.ReadByte();
		originalPosition = reader.ReadVector3();
		originalRotation = reader.ReadQuaternion();
		originalScale = reader.ReadVector3();
		targetPosition = reader.ReadVector3();
		targetRotation = reader.ReadQuaternion();
		targetScale = reader.ReadVector3();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine(string.Format("{0}: {1}", "transform", transform));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "isGlobal", isGlobal));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "interpolation", interpolation));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "originalPosition", originalPosition));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "originalRotation", originalRotation));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "originalScale", originalScale));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "targetPosition", targetPosition));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "targetRotation", targetRotation));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "targetScale", targetScale));
		return stringBuilder.ToString();
	}
}
