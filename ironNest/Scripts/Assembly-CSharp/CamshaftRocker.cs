using UnityEngine;

public sealed class CamshaftRocker : MonoBehaviour
{
	[Header("Source")]
	[Tooltip("Reference to the EnginePowerController that provides the normalized power (0–1).\nBehavior:\n- Power scales the animation playback speed by selecting a cycle duration between the Min and Max power durations.\n- Power = 0 -> paused (no motion).\n- Power = 1 -> uses Cycle Duration at Max Power.")]
	public EnginePowerController EnginePower;

	[Header("Target & Axis")]
	[Tooltip("The Transform to rotate.\nIf left null, this component will rotate its own Transform.\nSafe examples: assign a rocker arm Transform.")]
	public Transform Target;

	[Tooltip("Axis of rotation in local space.\nAccepted values: any non-zero vector. Will be normalized at runtime.\nSafe examples: (1,0,0) for local X, (0,1,0) for local Y, (0,0,1) for local Z.")]
	public Vector3 LocalAxis;

	[Header("Animation")]
	[Tooltip("Angular offset curve (in degrees) over a single normalized cycle [0, 1].\nEvaluation:\n- The curve's X (time) must span 0..1.\n- The curve's Y (value) is applied as degrees about the chosen axis.\nSafe examples:\n- A sine-like curve from 0 -> +30 -> 0 -> -30 -> 0.\n- A constant 0 curve for no motion.")]
	public AnimationCurve AngleCurve;

	[Header("Cycle Durations")]
	[Tooltip("Cycle duration (seconds) when EnginePower = 0 (minimum power) — if power is exactly 0, playback is paused.\nUsed for interpolation when power is > 0 but near zero.\nAccepted range: > 0.\nSafe examples: 2.0 (slow at low power), 1.0 (moderate).")]
	[Min(0.0001f)]
	public float CycleDurationAtMinPower;

	[Tooltip("Cycle duration (seconds) when EnginePower = 1 (maximum power).\nAccepted range: > 0.\nSafe examples: 0.5 (fast), 1.0 (moderate).")]
	[Min(0.0001f)]
	public float CycleDurationAtMaxPower;

	[Tooltip("Minimum power threshold used to avoid extremely slow stepping when power is very small (but > 0).\nBehavior:\n- Only used when EnginePower > 0 to compute time scaling.\n- Does not create motion if power is 0 (still paused).\nAccepted range: 0..1.\nSafe examples: 0.05 to prevent crawling speeds.")]
	[Range(0f, 1f)]
	public float MinPowerForMotion;

	[Header("Offsets")]
	[Tooltip("Static base rotation (degrees) about the axis applied before the animated offset.\nUse this to set the neutral/rest angle.\nAccepted range: any float.\nSafe examples: 0 (neutral), 15 (offset forward).")]
	public float BaseAngleDegrees;

	[Tooltip("Optional additional rotation applied after the axis rotation, expressed as Euler angles in local space.\nUseful for fine alignment without changing the axis.\nAccepted range: any Vector3.\nSafe examples: (0,0,0), (0,10,0).")]
	public Vector3 PostEulerOffset;

	[Header("Playback")]
	[Tooltip("Starting normalized position in the animation cycle [0, 1].\nAccepted range: 0..1. Values outside will be wrapped.\nSafe examples: 0.0 (start), 0.25 (quarter cycle).")]
	[Range(0f, 1f)]
	public float StartCyclePosition;

	[Tooltip("If enabled, the animation will continue from the last internal cycle position when the component is toggled on/off.\nIf disabled, it resets to StartCyclePosition on enable.")]
	public bool PreserveCycleOnEnable;

	private float _cyclePos;

	private Quaternion _initialLocalRotation;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private static AnimationCurve DefaultSineLikeCurve()
	{
		return null;
	}
}
