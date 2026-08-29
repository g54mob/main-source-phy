using System.Text;
using UnityEngine;

public sealed class VRPlayerTransformPacket : Packet
{
	public ushort usedFieldsBitMask;

	public Vector3 headPosition;

	public Quaternion headRotation;

	public Quaternion torsoRotation;

	public bool forceTorsoRotation;

	public Vector3 leftHandPosition;

	public Quaternion leftHandRotation;

	public byte leftHandPackedGripValue;

	public byte leftHandPackedTriggerValue;

	public Vector3 rightHandPosition;

	public Quaternion rightHandRotation;

	public byte rightHandPackedGripValue;

	public byte rightHandPackedTriggerValue;

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(in usedFieldsBitMask, default(FastBinaryWriter.ForPrimitives));
		if ((usedFieldsBitMask & 1) != 0)
		{
			writer.WriteVector3(in headPosition);
		}
		if ((usedFieldsBitMask & 2) != 0)
		{
			writer.WriteQuaternion(in headRotation);
		}
		if ((usedFieldsBitMask & 4) != 0)
		{
			writer.WriteQuaternion(in torsoRotation);
		}
		if ((usedFieldsBitMask & 8) != 0)
		{
			writer.Write(in forceTorsoRotation, default(FastBinaryWriter.ForPrimitives));
		}
		if ((usedFieldsBitMask & 0x10) != 0)
		{
			writer.WriteVector3(in leftHandPosition);
		}
		if ((usedFieldsBitMask & 0x20) != 0)
		{
			writer.WriteQuaternion(in leftHandRotation);
		}
		if ((usedFieldsBitMask & 0x40) != 0)
		{
			writer.Write(leftHandPackedGripValue);
		}
		if ((usedFieldsBitMask & 0x80) != 0)
		{
			writer.Write(leftHandPackedTriggerValue);
		}
		if ((usedFieldsBitMask & 0x100) != 0)
		{
			writer.WriteVector3(in rightHandPosition);
		}
		if ((usedFieldsBitMask & 0x200) != 0)
		{
			writer.WriteQuaternion(in rightHandRotation);
		}
		if ((usedFieldsBitMask & 0x400) != 0)
		{
			writer.Write(rightHandPackedGripValue);
		}
		if ((usedFieldsBitMask & 0x800) != 0)
		{
			writer.Write(rightHandPackedTriggerValue);
		}
	}

	public override void readData(FastBinaryReader reader)
	{
		usedFieldsBitMask = reader.ReadUInt16();
		if ((usedFieldsBitMask & 1) != 0)
		{
			headPosition = reader.ReadVector3();
		}
		if ((usedFieldsBitMask & 2) != 0)
		{
			headRotation = reader.ReadQuaternion();
		}
		if ((usedFieldsBitMask & 4) != 0)
		{
			torsoRotation = reader.ReadQuaternion();
		}
		if ((usedFieldsBitMask & 8) != 0)
		{
			forceTorsoRotation = reader.ReadBoolean();
		}
		if ((usedFieldsBitMask & 0x10) != 0)
		{
			leftHandPosition = reader.ReadVector3();
		}
		if ((usedFieldsBitMask & 0x20) != 0)
		{
			leftHandRotation = reader.ReadQuaternion();
		}
		if ((usedFieldsBitMask & 0x40) != 0)
		{
			leftHandPackedGripValue = reader.ReadByte();
		}
		if ((usedFieldsBitMask & 0x80) != 0)
		{
			leftHandPackedTriggerValue = reader.ReadByte();
		}
		if ((usedFieldsBitMask & 0x100) != 0)
		{
			rightHandPosition = reader.ReadVector3();
		}
		if ((usedFieldsBitMask & 0x200) != 0)
		{
			rightHandRotation = reader.ReadQuaternion();
		}
		if ((usedFieldsBitMask & 0x400) != 0)
		{
			rightHandPackedGripValue = reader.ReadByte();
		}
		if ((usedFieldsBitMask & 0x800) != 0)
		{
			rightHandPackedTriggerValue = reader.ReadByte();
		}
	}

	public override byte getTypeId()
	{
		return 108;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("usedFieldsBitMask: " + $"{usedFieldsBitMask}");
		stringBuilder.AppendLine("headPosition: " + $"{headPosition}");
		stringBuilder.AppendLine("headRotation: " + $"{headRotation}");
		stringBuilder.AppendLine("torsoRotation: " + $"{torsoRotation}");
		stringBuilder.AppendLine("forceTorsoRotation: " + $"{forceTorsoRotation}");
		stringBuilder.AppendLine("leftHandPosition: " + $"{leftHandPosition}");
		stringBuilder.AppendLine("leftHandRotation: " + $"{leftHandRotation}");
		stringBuilder.AppendLine("leftHandPackedGripValue: " + $"{leftHandPackedGripValue}");
		stringBuilder.AppendLine("leftHandPackedTriggerValue: " + $"{leftHandPackedTriggerValue}");
		stringBuilder.AppendLine("rightHandPosition: " + $"{rightHandPosition}");
		stringBuilder.AppendLine("rightHandRotation: " + $"{rightHandRotation}");
		stringBuilder.AppendLine("rightHandPackedGripValue: " + $"{rightHandPackedGripValue}");
		stringBuilder.Append("rightHandPackedTriggerValue: " + $"{rightHandPackedTriggerValue}");
		return stringBuilder.ToString();
	}
}
