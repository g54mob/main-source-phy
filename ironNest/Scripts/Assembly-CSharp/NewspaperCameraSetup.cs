using UnityEngine;

public class NewspaperCameraSetup : MonoBehaviour
{
	[Header("References")]
	[Tooltip("The RTSMapCameraController to reposition when this screen is shown.")]
	[SerializeField]
	private RTSMapCameraController cameraController;

	[Header("Camera Position")]
	[Tooltip("Local-space position the camera rig will snap to when this screen is enabled.\nUse the Context Menu → 'Capture Current Camera Position' button in Play Mode\nto copy the rig's current local position directly into this field.")]
	[SerializeField]
	private Vector3 cameraLocalPosition;

	private void OnEnable()
	{
	}
}
