using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Pine/ItemRespawnVolume")]
public class ItemRespawnVolume : MonoBehaviour
{
	public enum RespawnMode
	{
		ToPlayerInventory = 0,
		ToOriginalPositionAndRotation = 1,
		ToAnObject = 2
	}

	public RespawnMode respawnMode;

	public bool respawnWithPhysics = true;

	public Transform objectToRespawnTo;
}
