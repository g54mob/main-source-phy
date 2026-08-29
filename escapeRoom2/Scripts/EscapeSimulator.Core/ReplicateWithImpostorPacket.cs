using System.Text;
using UnityEngine;

public sealed class ReplicateWithImpostorPacket : Packet
{
	public GameObject originalObject;

	public ReplicateWithImpostorType replicateType;

	public Vector3 impostorPosition;

	public Quaternion impostorRotation;

	public override byte getTypeId()
	{
		return 92;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteGameObject(originalObject);
		int value = (int)replicateType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in impostorPosition);
		writer.WriteQuaternion(in impostorRotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		originalObject = reader.ReadGameObject();
		replicateType = (ReplicateWithImpostorType)reader.ReadInt32();
		impostorPosition = reader.ReadVector3();
		impostorRotation = reader.ReadQuaternion();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("originalObject: " + $"{originalObject}");
		stringBuilder.AppendLine("replicateType: " + $"{replicateType}");
		stringBuilder.AppendLine("impostorPosition: " + $"{impostorPosition}");
		stringBuilder.Append("impostorRotation: " + $"{impostorRotation}");
		return stringBuilder.ToString();
	}
}
