using UnityEngine;

public class ControllerSensitivitySetter : MonoBehaviour
{
	public float CurrentSensitivity = 2f;

	public VirtualCursor VirtualCursor;

	public FirstPersonController FirstPersonController;

	public void ChangeSensitivity(float value)
	{
		VirtualCursor virtualCursor = VirtualCursor;
		CurrentSensitivity = value;
		virtualCursor.ControllerSensitivity = value;
		FirstPersonController firstPersonController = FirstPersonController;
		firstPersonController.controllerSensitivity = value;
	}
}
