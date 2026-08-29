using System.Text;
using UnityEngine;

public sealed class PlayerPosePacket : Packet
{
	public Vector3 position;

	public Quaternion rotation;

	public Vector2 currentRotation;

	public bool isCrouching;

	public bool isTeleporting;

	public bool isClimbing;

	public bool isRunning;

	public Vector2 movementVector;

	public float turning;

	public sbyte climbDirection;

	public override byte getTypeId()
	{
		return 24;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteVector3(in position);
		writer.WriteQuaternion(in rotation);
		writer.WriteVector2(in currentRotation);
		writer.Write(in isCrouching, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isTeleporting, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isClimbing, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isRunning, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector2(in movementVector);
		writer.Write(in turning, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in climbDirection, default(FastBinaryWriter.ForPrimitives));
	}

	public override void readData(FastBinaryReader reader)
	{
		position = reader.ReadVector3();
		rotation = reader.ReadQuaternion();
		currentRotation = reader.ReadVector2();
		isCrouching = reader.ReadBoolean();
		isTeleporting = reader.ReadBoolean();
		isClimbing = reader.ReadBoolean();
		isRunning = reader.ReadBoolean();
		movementVector = reader.ReadVector2();
		turning = reader.ReadSingle();
		climbDirection = reader.ReadSByte();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("position: " + $"{position}");
		stringBuilder.AppendLine("rotation: " + $"{rotation}");
		stringBuilder.AppendLine("currentRotation: " + $"{currentRotation}");
		stringBuilder.AppendLine("isCrouching: " + $"{isCrouching}");
		stringBuilder.AppendLine("isTeleporting: " + $"{isTeleporting}");
		stringBuilder.AppendLine("isClimbing: " + $"{isClimbing}");
		stringBuilder.AppendLine("isRunning: " + $"{isRunning}");
		stringBuilder.AppendLine("movementVector: " + $"{movementVector}");
		stringBuilder.AppendLine("turning: " + $"{turning}");
		stringBuilder.Append("climbDirection: " + $"{climbDirection}");
		return stringBuilder.ToString();
	}
}
