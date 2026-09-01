using UnityEngine;
using UnityEngine.Events;

public class PlayerSpawnTrigger : MonoBehaviour
{
	[Header("Target Spawn Point")]
	[Tooltip("Direct reference to the PlayerSpawnPoint to trigger.\nWorks across scenes when both are loaded simultaneously.\nIf left null, the script falls back to a tag search using Spawn Point Tag.")]
	public PlayerSpawnPoint target;

	[Tooltip("Tag assigned to the GameObject that holds the target PlayerSpawnPoint.\nUsed only when Target is null. Uses FindGameObjectsWithTag, which is a fast\nhash lookup across all loaded scenes — more efficient than a name or type search.\nThe tag must exist in the project's Tag list and be assigned to the spawn point.\nIf multiple objects share the tag, the first one found with a PlayerSpawnPoint is used.\nExample: 'SpawnPoint'")]
	public string spawnPointTag;

	[Header("Trigger Mode")]
	[Tooltip("OnEnable — triggers the target PlayerSpawnPoint automatically when this component/GameObject becomes active (i.e. when the additive scene loads).\n\nManual   — does nothing on its own; call Trigger() from code or wire a UnityEvent to Trigger() for explicit control.")]
	public bool triggerOnEnable;

	[Header("Events")]
	[Tooltip("Fired after this trigger has successfully called Trigger() on the target spawn point.\nNote: this fires when the call is dispatched, not after the teleport completes (teleport is synchronous, so in practice they are the same frame).")]
	public UnityEvent onTriggered;

	private void OnEnable()
	{
	}

	public void Trigger()
	{
	}

	private PlayerSpawnPoint ResolveTarget()
	{
		return null;
	}
}
