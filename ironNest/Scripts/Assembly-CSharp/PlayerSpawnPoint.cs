using UnityEngine;
using UnityEngine.Events;

public class PlayerSpawnPoint : MonoBehaviour
{
	public enum SpawnTriggerMode
	{
		OnEnable = 0,
		Manual = 1
	}

	[Header("Trigger Mode")]
	[Tooltip("OnEnable  — teleports the player automatically when this component/GameObject becomes active.\n            Best for spawn points that live inside the additive mission scene.\n\nManual    — does nothing on its own; waits to be triggered by:\n              • Calling Trigger() from code\n              • A UnityEvent wired to Trigger()\n              • A PlayerSpawnTrigger companion (in any loaded scene)\n            Best for spawn points that live in the master scene (e.g. rotating platforms).")]
	public SpawnTriggerMode triggerMode;

	[Header("Target")]
	[Tooltip("The tag used to locate the FirstPersonController GameObject across all loaded scenes.\nThe tag must exist in the project's Tag list and be assigned to the player root.\nDefault: 'Player'")]
	public string playerTag;

	[Header("Spawn Options")]
	[Tooltip("When enabled, the player's Y (yaw) rotation is set to match this transform's Y rotation, giving the player a directed facing angle at spawn.\nPitch and roll are always left untouched to avoid disorienting the camera.")]
	public bool applyYawRotation;

	[Header("Events")]
	[Tooltip("Fired after the player has been successfully teleported to this spawn point.\nWire any post-spawn logic here (e.g. UI transitions, cutscene starts).")]
	public UnityEvent onSpawned;

	private void OnEnable()
	{
	}

	public void Trigger()
	{
	}

	public void Respawn()
	{
	}

	private void TeleportPlayer()
	{
	}

	private void ApplyYaw(Transform playerTransform)
	{
	}

	private void ApplyPitch(Transform playerTransform)
	{
	}
}
