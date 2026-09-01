using UnityEngine;

public class MouseSensitivitySetter : MonoBehaviour
{
	public float CurrentSensitivity = 1f;

	public VirtualCursor VirtualCursor;

	public FirstPersonController FirstPersonController;

	public void ChangeSensitivity(float value)
	{
		FirstPersonController firstPersonController = FirstPersonController;
		CurrentSensitivity = value;
		firstPersonController.mouseSensitivityMultiplier = value;
	}
}
