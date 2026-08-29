using System.Text;
using UnityEngine;

public class CharacterPoseContext : IReadWrite
{
	public static readonly CharacterPoseContext None = new CharacterPoseContext();

	public CharacterPose pose;

	public CharacterPoseState poseState;

	public Vector3 transitionInPosition;

	public Quaternion transitionInRotation;

	public Quaternion pivotRotation;

	public Vector2 currentRotationOffset;

	public bool transitionIn;

	public Vector3 lastPlayerPosition;

	public Vector2 lastPlayerRotation;

	public Vector3 transitionOutStartPosition;

	public Quaternion transitionOutStartRotation;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteComponent(pose);
		int value = (int)poseState;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in transitionInPosition);
		writer.WriteQuaternion(in transitionInRotation);
		writer.WriteQuaternion(in pivotRotation);
		writer.WriteVector2(in currentRotationOffset);
		writer.Write(in transitionIn, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in lastPlayerPosition);
		writer.WriteVector2(in lastPlayerRotation);
		writer.WriteVector3(in transitionOutStartPosition);
		writer.WriteQuaternion(in transitionOutStartRotation);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		pose = reader.ReadComponent<CharacterPose>();
		poseState = (CharacterPoseState)reader.ReadInt32();
		transitionInPosition = reader.ReadVector3();
		transitionInRotation = reader.ReadQuaternion();
		pivotRotation = reader.ReadQuaternion();
		currentRotationOffset = reader.ReadVector2();
		transitionIn = reader.ReadBoolean();
		lastPlayerPosition = reader.ReadVector3();
		lastPlayerRotation = reader.ReadVector2();
		transitionOutStartPosition = reader.ReadVector3();
		transitionOutStartRotation = reader.ReadQuaternion();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("pose: " + $"{pose}");
		stringBuilder.AppendLine("poseState: " + $"{poseState}");
		stringBuilder.AppendLine("transitionInPosition: " + $"{transitionInPosition}");
		stringBuilder.AppendLine("transitionInRotation: " + $"{transitionInRotation}");
		stringBuilder.AppendLine("pivotRotation: " + $"{pivotRotation}");
		stringBuilder.AppendLine("currentRotationOffset: " + $"{currentRotationOffset}");
		stringBuilder.AppendLine("transitionIn: " + $"{transitionIn}");
		stringBuilder.AppendLine("lastPlayerPosition: " + $"{lastPlayerPosition}");
		stringBuilder.AppendLine("lastPlayerRotation: " + $"{lastPlayerRotation}");
		stringBuilder.AppendLine("transitionOutStartPosition: " + $"{transitionOutStartPosition}");
		stringBuilder.Append("transitionOutStartRotation: " + $"{transitionOutStartRotation}");
		return stringBuilder.ToString();
	}
}
