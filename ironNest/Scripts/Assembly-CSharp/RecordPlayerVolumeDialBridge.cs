using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Gameplay/Record Player Volume Dial Bridge")]
public class RecordPlayerVolumeDialBridge : MonoBehaviour
{
	[Header("References")]
	[Tooltip("The RecordPlayerController whose master volume this dial controls.\n\nRequired. The bridge will log an error and disable itself if not assigned.")]
	[SerializeField]
	private RecordPlayerController recordPlayerController;

	[Tooltip("The DialInteractable that the player rotates to change volume.\n\nRequired. Must be configured in Limited mode with:\n  Min Output Value = 0\n  Max Output Value = 1\n\nMin/Max Rotation Angle can be set to any diegetic range (e.g. 0–270 degrees).\nThe bridge will log an error and disable itself if not assigned.")]
	[SerializeField]
	private DialInteractable volumeDial;

	[Header("Debug")]
	[Tooltip("Logs volume changes and initial sync events to the Console.\n\nSafe default: false.")]
	[SerializeField]
	private bool debugLogs;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void HandleDialValueChanged(float value)
	{
	}
}
