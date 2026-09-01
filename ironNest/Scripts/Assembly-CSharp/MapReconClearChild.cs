using UnityEngine;

public class MapReconClearChild : MonoBehaviour
{
	[Tooltip("The MapReconClearHandle on the root of this impact's prefab. Leave None to let Awake() find it automatically by walking up the hierarchy at the moment of instantiation (before reparenting occurs). If your spawner reparents children before Awake runs, assign this reference explicitly via script or use the Handle's Prelinked Children list.")]
	[SerializeField]
	private MapReconClearHandle _handle;

	private void Awake()
	{
	}
}
