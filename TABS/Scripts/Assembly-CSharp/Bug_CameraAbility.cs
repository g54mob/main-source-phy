using UnityEngine;

public abstract class Bug_CameraAbility : MonoBehaviour
{
	public bool IsActive;

	public abstract void Enable();

	public abstract void Disable();
}
