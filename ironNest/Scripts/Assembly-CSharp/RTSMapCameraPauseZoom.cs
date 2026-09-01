using UnityEngine;

[RequireComponent(typeof(RTSMapCameraController))]
public class RTSMapCameraPauseZoom : MonoBehaviour
{
	[Header("Pause Zoom Range")]
	[Tooltip("Minimum zoom distance allowed while the game is paused.\nIf the camera is closer than this when paused, it will zoom out to this value.\nMust be >= RTSMapCameraController.minZoom and <= pauseMaxZoom.")]
	public float pauseMinZoom;

	[Tooltip("Maximum zoom distance allowed while the game is paused.\nIf the camera is further than this when paused, it will zoom in to this value.\nMust be <= RTSMapCameraController.maxZoom and >= pauseMinZoom.")]
	public float pauseMaxZoom;

	[Header("Transition Speed")]
	[Tooltip("Exponential smoothing time constant (seconds) for the pause zoom-adjust transition.\nSmaller = snappier. Uses unscaled time so it works while the game is paused.\nRecommended range: 0.05 – 0.5.")]
	public float pauseTransitionTau;

	[Tooltip("Exponential smoothing time constant (seconds) for the restore-to-pre-pause-zoom transition.\nSmaller = snappier. Uses scaled time (game is running again at this point).\nRecommended range: 0.05 – 0.5.")]
	public float restoreTransitionTau;

	[Tooltip("Zoom distance is considered 'arrived' when it is within this threshold of the target.\nPrevents the lerp running indefinitely on sub-pixel differences.")]
	public float arrivalThreshold;

	private RTSMapCameraController controller;

	private bool wasPaused;

	private bool didAdjust;

	private float preAdjustZoom;

	private float pauseTargetZoom;

	private bool pauseAnimDone;

	private bool restoring;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void OnBecamePaused()
	{
	}

	private void OnBecameUnpaused()
	{
	}

	private void TickPauseAnimation()
	{
	}

	private void TickRestoreAnimation()
	{
	}
}
