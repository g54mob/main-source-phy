using UnityEngine;

[AddComponentMenu("Audio/FMODOps/Gun Measured Elevation Speed")]
public class GunMeasuredElevationSpeed : MonoBehaviour
{
	[Header("Target")]
	[Tooltip("GunController whose CurrentElevation will be observed.\nIf left null, the component will attempt to auto-find a GunController on the same GameObject in Awake().")]
	[SerializeField]
	private GunController gun;

	[Header("Measurement Settings")]
	[Tooltip("If true, the reported speed is smoothed using exponential smoothing (good for UI/audio).\nIf false, the raw measured speed (delta elevation / delta time) is reported each frame.")]
	[SerializeField]
	private bool useSmoothing;

	[Tooltip("Exponential smoothing factor in the range [0..0.95].\n0 = no smoothing (raw speed).\nHigher values = smoother but more latency.\nRecommended: 0.10–0.25 for audio parameters.")]
	[Range(0f, 0.95f)]
	[SerializeField]
	private float smoothing;

	[Tooltip("If true, the component uses unscaled delta time (Time.unscaledDeltaTime) for measurement.\nUse this if your audio should respond the same during timeScale changes.\nIf false, uses Time.deltaTime.")]
	[SerializeField]
	private bool useUnscaledTime;

	[Tooltip("If true, the speed is measured in LateUpdate instead of Update.\nEnable this if something else updates GunController elevation later in the frame and you want to sample after it.\nDefault false is usually fine.")]
	[SerializeField]
	private bool sampleInLateUpdate;

	[Header("Diagnostics (Read-only)")]
	[Tooltip("Last observed elevation angle in degrees (GunController.CurrentElevation) used to compute the next delta.")]
	[SerializeField]
	private float lastElevationDeg;

	[Tooltip("Raw measured elevation speed in degrees/second for the most recent sample (signed).")]
	[SerializeField]
	private float rawMeasuredSpeed;

	[Tooltip("Smoothed measured elevation speed in degrees/second for the most recent sample (signed).")]
	[SerializeField]
	private float smoothedMeasuredSpeed;

	[Tooltip("True after the first valid sample has been taken.")]
	[SerializeField]
	private bool hasSample;

	public float MeasuredElevationSpeed => 0f;

	public float MeasuredElevationSpeedAbs => 0f;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	private void LateUpdate()
	{
	}

	[ContextMenu("Reset Sampling")]
	private void ResetSampling()
	{
	}

	private void Sample()
	{
	}
}
