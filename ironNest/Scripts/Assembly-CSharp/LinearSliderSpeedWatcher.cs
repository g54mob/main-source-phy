using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Linear Slider Speed Watcher")]
public class LinearSliderSpeedWatcher : MonoBehaviour
{
	[Header("Target")]
	[Tooltip("The LinearSliderInteractable to watch.\nIf left empty, the watcher will try to find one on this GameObject first, then in children, then in parents.\nSafe default: leave empty if this watcher sits next to the slider in the prefab.")]
	[SerializeField]
	private LinearSliderInteractable slider;

	[Header("Speed Calculation")]
	[Tooltip("If true, speed is computed based on slider.Value delta per second (units: ValueUnits/second).\nIf false, speed is computed based on slider.CurrentDistance delta per second (units: LocalUnits/second).\nMost debugging/gameplay uses Value-based speed because it matches what your systems read from the slider.")]
	[SerializeField]
	private bool speedBasedOnSliderValue;

	[Tooltip("If true, speed is updated even when the slider is not being dragged.\nIf false, speed is forced to 0 when slider.IsDragging is false.\nUseful if the slider can be animated or set via code and you still want to track motion.")]
	[SerializeField]
	private bool trackWhenNotDragging;

	[Tooltip("Minimum deltaTime used to compute speed (seconds).\nPrevents extreme spikes if Time.deltaTime becomes very small (e.g., hitch recovery or editor quirks).\nRecommended safe range: 0.005 to 0.02.")]
	[SerializeField]
	[Min(0.0001f)]
	private float minDeltaTime;

	[Tooltip("Optional smoothing for the displayed/computed speed.\n0 = no smoothing (raw speed).\nHigher values = more smoothing (slower response).\nThis is an exponential smoothing time constant in seconds, applied with unscaled time.")]
	[SerializeField]
	[Min(0f)]
	private float smoothingTime;

	[Tooltip("If true, uses unscaled time (Time.unscaledDeltaTime) for speed computation.\nIf false, uses scaled time (Time.deltaTime).\nUse unscaled when you want speed debugging/events to remain consistent during slow-mo or paused timescale.")]
	[SerializeField]
	private bool useUnscaledTime;

	[Header("Hold-in-Range Trigger")]
	[Tooltip("If true, the hold timer can accumulate while the slider is not being dragged.\nIf false, the hold timer only accumulates while slider.IsDragging is true.\nUseful to require active user interaction for the hold condition.")]
	[SerializeField]
	private bool requireDraggingForHold;

	[Tooltip("Inclusive minimum speed to be considered 'in range'.\nUnits depend on Speed Based On Slider Value:\n- ON  => ValueUnits/second (based on slider.Value)\n- OFF => LocalUnits/second (based on slider.CurrentDistance)\nSpeed is absolute and always >= 0.")]
	[SerializeField]
	[Min(0f)]
	private float holdSpeedMinInclusive;

	[Tooltip("Inclusive maximum speed to be considered 'in range'.\nUnits depend on Speed Based On Slider Value:\n- ON  => ValueUnits/second (based on slider.Value)\n- OFF => LocalUnits/second (based on slider.CurrentDistance)\nIf Max < Min at runtime, the script automatically swaps them to keep it safe.")]
	[SerializeField]
	[Min(0f)]
	private float holdSpeedMaxInclusive;

	[Tooltip("How long (seconds) the speed must remain continuously within [Min..Max] to trigger OnSpeedHeldInRange.\nUses unscaled time if Use Unscaled Time is enabled.\nSet to 0 to trigger immediately when entering the range.")]
	[SerializeField]
	[Min(0f)]
	private float holdDurationSeconds;

	[Tooltip("If true, OnSpeedHeldInRange fires only once per 'in-range session' (it won't spam every frame).\nIt will fire again only after speed leaves the range and then re-enters and satisfies the hold duration again.")]
	[SerializeField]
	private bool fireOncePerRangeEntry;

	[Tooltip("If true, when the speed leaves the range, the accumulated hold time resets to 0.\nIf false, the hold time pauses while out of range and resumes when it re-enters.\nMost 'hold' behaviors want reset = true.")]
	[SerializeField]
	private bool resetHoldTimerWhenOutOfRange;

	[Header("Debug (Read Only)")]
	[Tooltip("Current computed absolute speed (always >= 0).\nUnits depend on Speed Based On Slider Value:\n- ON  => ValueUnits/second (based on slider.Value)\n- OFF => LocalUnits/second (based on slider.CurrentDistance)\nThis is the smoothed speed if Smoothing Time > 0.")]
	[SerializeField]
	private float currentSpeed;

	[Tooltip("Current accumulated time (seconds) that speed has continuously remained within the configured range.\nThis value is affected by Require Dragging For Hold and Reset Hold Timer When Out Of Range.")]
	[SerializeField]
	private float heldInRangeTime;

	[Tooltip("True when the current speed is within [Hold Speed Min Inclusive .. Hold Speed Max Inclusive] (inclusive).")]
	[SerializeField]
	private bool isSpeedInRange;

	[Tooltip("True after OnSpeedHeldInRange has fired for the current in-range session (used when Fire Once Per Range Entry is enabled).")]
	[SerializeField]
	private bool hasFiredThisSession;

	[Header("Events")]
	[Tooltip("Invoked when speed has been continuously held within the configured inclusive range for Hold Duration Seconds.\nParameter: currentSpeed at the time of firing (smoothed).")]
	public UnityEvent<float> OnSpeedHeldInRange;

	private float _prevSample;

	private bool _hasPrevSample;

	private float _smoothVelocity;

	private float _rawSpeed;

	public float CurrentSpeed => 0f;

	public float HeldInRangeTime => 0f;

	private void Reset()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void AutoAssignSliderIfNeeded()
	{
	}

	private void InitializeSampling()
	{
	}

	private void Update()
	{
	}

	private float GetCurrentSample()
	{
		return 0f;
	}

	private void UpdateHoldState(float dt, float speed)
	{
	}
}
