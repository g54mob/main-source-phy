using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Audio/FMODOps/Counter Battery Timer – FMOD Bridge")]
public class CounterBatteryTimerFMODBridge : MonoBehaviour, IFloatValueProvider
{
	[Header("Timer Source")]
	[Tooltip("The CounterBatteryTimer whose TimeRemaining drives this bridge.\n\nIf left empty, the bridge will attempt to use CounterBatteryTimer.Instance at runtime (scene singleton). Assign explicitly when multiple timers exist in the scene or to guarantee a specific reference.")]
	[SerializeField]
	private CounterBatteryTimer timerSource;

	[Header("Response Curve")]
	[Tooltip("Maps normalised time-remaining (X axis) to the output float value (Y axis) that FMODParameterSetter will read.\n\nX axis — normalised time remaining:\n  • X = 1.0  →  timer is at full duration (e.g. 300 s remaining)\n  • X = 0.0  →  timer has expired (0 s remaining)\n\nY axis — output value forwarded to FMODParameterSetter:\n  • Typical default: (1, 0) → (0, 1)  — full time = 0, no time = 1.\n  • Curve is sampled via AnimationCurve.Evaluate, so keyframes outside [0,1] on X are extrapolated using the curve's wrap mode.\n\nSafe examples:\n  • Flat urgency ramp: start flat at Y=0, curve steeply up near X=0.\n  • Inverse linear: straight line from (1,0) to (0,1).")]
	[SerializeField]
	private AnimationCurve responseCurve;

	[Header("Behaviour")]
	[Tooltip("When true, the bridge continues to output the last evaluated value even after the timer has permanently stopped (IsPermanentlyStopped == true). When false, the output is held at the curve value for 0 s remaining once the timer stops, regardless of actual remaining time.")]
	[SerializeField]
	private bool outputLastValueWhenStopped;

	[Tooltip("When true, the bridge outputs 0 while the timer has not yet started (IsRunning == false and not expired). When false, the curve is evaluated normally using the current TimeRemaining even before the timer has been started.")]
	[SerializeField]
	private bool zeroOutputBeforeTimerStarts;

	[Header("Debug")]
	[Tooltip("If true, prints diagnostic messages when the timer source is resolved at runtime or when the timer reference is missing.")]
	[SerializeField]
	private bool verbose;

	[Header("Inspector (Live Read-only)")]
	[Tooltip("Normalised time-remaining value used as the curve X input this frame. Range: [0..1]. 1 = full duration remaining, 0 = expired.")]
	[SerializeField]
	private float inspectorNormalisedTimeRemaining;

	[Tooltip("Output value after evaluating the response curve. This is what FMODParameterSetter reads via IFloatValueProvider.")]
	[SerializeField]
	private float inspectorCurveOutput;

	private float _lastOutput;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	public float GetFloatValue()
	{
		return 0f;
	}

	private void ResolveTimerSource()
	{
	}

	private float EvaluateCurve()
	{
		return 0f;
	}
}
