using UnityEngine;

public class CameraZoneRTSTransitionHelper : MonoBehaviour
{
	[Header("References")]
	[Tooltip("Assign your CameraZoneTrigger (transition script).")]
	public CameraZoneTrigger transitionScript;

	[Tooltip("Assign your RTSMapCameraController.")]
	public RTSMapCameraController rtsCameraController;

	[Tooltip("Assign the tactical map's canvas GameObject.")]
	public Canvas mapCanvas;

	private void Start()
	{
	}

	public void OnRTSCameraActivated()
	{
	}

	private Vector3? GetCanvasLookAtLocalPoint()
	{
		return null;
	}
}
