using UnityEngine;

public class OdometerDisplayWatcher : MonoBehaviour
{
	[Header("Reference")]
	[SerializeField]
	private OdometerDisplay odometer;

	[Header("Debug (Read Only)")]
	[SerializeField]
	private float currentValue;

	[SerializeField]
	private float valueChangeSpeed;

	[SerializeField]
	private float smoothedValueChangeSpeed;

	[Tooltip("0..1 where 0.5 = 50% of cap speed.")]
	[SerializeField]
	private float rollingCounterSpeedPercent;

	[Header("Options")]
	[Tooltip("If true, uses unscaled time (ignores Time.timeScale).")]
	[SerializeField]
	private bool useUnscaledTime;

	[Tooltip("If true, reports absolute speed (always positive).")]
	[SerializeField]
	private bool absoluteSpeed;

	[Header("Speed Smoothing (Optional)")]
	[Tooltip("Enable exponential smoothing on the computed valueChangeSpeed.")]
	[SerializeField]
	private bool enableSpeedSmoothing;

	[Tooltip("Time constant (seconds) for speed smoothing (tau). Smaller = more responsive, larger = smoother.")]
	[Min(0.0001f)]
	[SerializeField]
	private float speedSmoothingTimeConstant;

	[Tooltip("Optional clamp on per-frame change of the RAW speed before smoothing. <= 0 disables.")]
	[SerializeField]
	private float maxPerFrameSpeedDelta;

	[Header("Cap Speed")]
	[Tooltip("Cap speed used to compute Rolling_Counter_Speed (0..1). Set <= 0 to output 0.")]
	[SerializeField]
	private float Rolling_Counter_Speed_Cap;

	private bool initialized;

	private float lastValue;

	private bool speedSmoothingInitialized;

	private float speedSmoothed;

	public float Rolling_Counter_Speed => 0f;

	private void Reset()
	{
	}

	private void Update()
	{
	}

	private static float RoundTo2Decimals(float v)
	{
		return 0f;
	}
}
