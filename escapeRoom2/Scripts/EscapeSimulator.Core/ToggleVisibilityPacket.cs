using System.Text;
using UnityEngine;

public sealed class ToggleVisibilityPacket : Packet
{
	public GameObject targetObject;

	public bool isVisible;

	public Vector3 positionWhenVisible;

	public Quaternion rotationWhenVisible;

	public override byte getTypeId()
	{
		return 93;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteGameObject(targetObject);
		writer.Write(in isVisible, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in positionWhenVisible);
		writer.WriteQuaternion(in rotationWhenVisible);
	}

	public override void readData(FastBinaryReader reader)
	{
		targetObject = reader.ReadGameObject();
		isVisible = reader.ReadBoolean();
		positionWhenVisible = reader.ReadVector3();
		rotationWhenVisible = reader.ReadQuaternion();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("targetObject: " + $"{targetObject}");
		stringBuilder.AppendLine("isVisible: " + $"{isVisible}");
		stringBuilder.AppendLine("positionWhenVisible: " + $"{positionWhenVisible}");
		stringBuilder.Append("rotationWhenVisible: " + $"{rotationWhenVisible}");
		return stringBuilder.ToString();
	}
}
