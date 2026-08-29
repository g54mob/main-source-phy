using System.Text;
using UnityEngine;

public sealed class VRHeldItemPosePacket : Packet
{
	public int handId;

	public Vector3 heldObjectPos;

	public Quaternion heldObjectRot;

	public override byte getTypeId()
	{
		return 109;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in handId, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in heldObjectPos);
		writer.WriteQuaternion(in heldObjectRot);
	}

	public override void readData(FastBinaryReader reader)
	{
		handId = reader.ReadInt32();
		heldObjectPos = reader.ReadVector3();
		heldObjectRot = reader.ReadQuaternion();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("handId: " + $"{handId}");
		stringBuilder.AppendLine("heldObjectPos: " + $"{heldObjectPos}");
		stringBuilder.Append("heldObjectRot: " + $"{heldObjectRot}");
		return stringBuilder.ToString();
	}
}
