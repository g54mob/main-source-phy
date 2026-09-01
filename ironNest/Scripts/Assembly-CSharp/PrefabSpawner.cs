using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
	[Header("Prefab Settings")]
	[Tooltip("Pool of prefabs to choose from. One is picked at random each time Spawn() is called.\nNull entries in the list are skipped automatically.")]
	[SerializeField]
	private GameObject[] prefabs;

	[Tooltip("Where the prefab will be spawned.\n• None / Self  →  uses this GameObject's position and rotation.\n• Assign any Transform to override the spawn location.")]
	[SerializeField]
	private Transform spawnPoint;

	[Tooltip("Optional parent Transform assigned to the spawned instance.\nLeave empty to spawn at the scene root.")]
	[SerializeField]
	private Transform spawnParent;

	[Header("Behaviour")]
	[Tooltip("When enabled, the spawned instance inherits the spawn point's world position and rotation even if it is re-parented (worldPositionStays = true). When disabled, the local-space position/rotation is preserved instead.")]
	[SerializeField]
	private bool worldPositionStays;

	[Tooltip("Maximum number of instances this spawner may create during its lifetime.\nSet to 0 for unlimited spawns.")]
	[SerializeField]
	private int maxSpawnCount;

	[Header("Random Position Offset")]
	[Tooltip("When enabled, a random offset is added to the spawn position on every Spawn() call. The offset is generated independently per axis (X, Y, Z) using the Min/Max ranges below, and is applied in the spawn point's LOCAL space — i.e. the offset axes follow the spawn point's own rotation (its local right/up/forward), not the world's fixed X/Y/Z axes.\nWhen disabled, prefabs spawn exactly at the spawn point as before (default behaviour, unchanged).")]
	[SerializeField]
	private bool enableRandomPositionOffset;

	[Tooltip("Minimum value (inclusive) of the random offset range for each local axis, in local units relative to the spawn point.\n• X = spawn point's local right axis\n• Y = spawn point's local up axis\n• Z = spawn point's local forward axis\nOnly used when 'Enable Random Position Offset' is on.\nEach axis is picked independently via Random.Range(Min, Max) for that axis, so Min may be negative (e.g. -0.5) to allow offsets on both sides of the spawn point. Min does not need to be less than Max — Unity's Random.Range handles either order — but keeping Min ≤ Max per axis is recommended for clarity.\nExample: Min = (-1, 0, -1), Max = (1, 0, 1) → random horizontal scatter with no vertical offset.")]
	[SerializeField]
	private Vector3 randomOffsetMin;

	[Tooltip("Maximum value (inclusive) of the random offset range for each local axis, in local units relative to the spawn point.\n• X = spawn point's local right axis\n• Y = spawn point's local up axis\n• Z = spawn point's local forward axis\nOnly used when 'Enable Random Position Offset' is on.\nEach axis is picked independently via Random.Range(Min, Max) for that axis.\nExample: Min = (-1, 0, -1), Max = (1, 0, 1) → random horizontal scatter with no vertical offset.")]
	[SerializeField]
	private Vector3 randomOffsetMax;

	[Header("Debug")]
	[Tooltip("EDITOR-ONLY debug trigger. Tick this box in the Inspector while in Play Mode to immediately call Spawn() exactly once, following all the rules configured above (prefab pool, spawn point, offsets, max spawn count, etc.). The box automatically un-ticks itself right after the spawn fires, so it behaves like a momentary button rather than a persistent flag.\n• Only works in Play Mode — ticking it in Edit Mode does nothing, since Instantiate() outside Play Mode has no clear ownership/undo behaviour and could pollute the scene.\n• This field is not read anywhere except this debug hook — it has no effect on gameplay or builds, and is compiled out entirely outside the Unity Editor.")]
	[SerializeField]
	private bool debugTriggerSpawn;

	private int _spawnedCount;

	public int SpawnedCount => 0;

	public void Spawn()
	{
	}

	public void ResetSpawnCount()
	{
	}

	private Vector3 GetRandomLocalOffset()
	{
		return default(Vector3);
	}
}
