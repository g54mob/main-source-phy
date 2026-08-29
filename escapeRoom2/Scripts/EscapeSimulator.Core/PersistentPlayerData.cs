using System.Text;
using UnityEngine;

public class PersistentPlayerData
{
	public NetPlayerId id;

	public Vector3 position;

	public Vector2 rotation;

	public bool isCrouching;

	public CharacterPoseContext characterPoseContext;

	public Game.LadderMovementRuntimeData ladderData;

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"ID: {id}");
		stringBuilder.AppendLine($"Position: {position}");
		stringBuilder.AppendLine($"Rotation: {rotation}");
		stringBuilder.AppendLine($"Is Crouching: {isCrouching}");
		stringBuilder.AppendLine($"Character Pose: {characterPoseContext}");
		stringBuilder.AppendLine($"Ladder Data: {ladderData}");
		return stringBuilder.ToString();
	}
}
