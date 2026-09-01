using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Artillery/Gun Stopwatch")]
public class GunStopwatch : MonoBehaviour
{
	public enum RotationAxis
	{
		Z = 0,
		X = 1,
		Y = 2,
		NegativeZ = 3,
		NegativeX = 4,
		NegativeY = 5
	}

	public enum LocalComputationMode
	{
		MirrorGunRangeModel = 0,
		UISpacePathLength = 1
	}

	private enum WatchState
	{
		Predicting = 0,
		AwaitFireDelay = 1,
		CountingDown = 2,
		ImpactHold = 3
	}

	[Header("References")]
	[Tooltip("GunController to observe. Required.\nThe watch listens for firing and predicted impact time events, and optionally reads elevation, range, and shell speed for local computations.")]
	public GunController watchedGun;

	[Tooltip("Transform used as the watch 'hand' (needle) to rotate for the countdown/prediction.\nTypically a child RectTransform or Transform on the dial UI.")]
	public Transform handTransform;

	[Header("Display Settings")]
	[Tooltip("How many seconds a full 360° hand rotation represents.\nExample: 60 means one full rotation per minute.\nMust be > 0 for the hand to animate.")]
	public float secondsPerFullRotation;

	[Tooltip("Angle (in degrees) where 0 seconds is drawn. Applied relative to the hand's initial local rotation.\nUse this to align the 'zero' mark on your dial artwork.")]
	public float zeroAngle;

	[Tooltip("Axis around which the hand rotates.\nUse Z for most 2D/UI dials. Use a Negative variant to invert the rotation direction.")]
	public RotationAxis rotationAxis;

	[Header("Hand Motion")]
	[Tooltip("Animation curve for the hand's motion within each second (0..1).\n- Linear: smooth sweep\n- Stepped: ticking hand (e.g., floor-like steps)\nThe X-axis is the fractional part of seconds, the Y-axis is the interpolation within that second.")]
	public AnimationCurve tickCurve;

	[Header("Prediction Source")]
	[Tooltip("If enabled, the stopwatch uses GunController's OnPredictedImpactTimeChanged event (recommended).\nThis matches the game's firing model: time = range / adjustedSpeed (ignores per-shot randomness).\nIf disabled, the stopwatch computes the time locally (see 'Local Computation').")]
	public bool useGunPredictions;

	[Tooltip("If true and no shell is loaded, display 0 seconds.\nIf false, keep showing the last known prediction value even when the chamber is empty.")]
	public bool zeroWhenNoShellLoaded;

	[Header("Local Computation")]
	[Tooltip("When Use Gun Predictions is false, choose how to compute travel time locally:\n- MirrorGunRangeModel: time = MapElevationToRange(CurrentElevation) / GetAdjustedShellSpeed(). Matches GunController predictions without needing UI context.\n- UISpacePathLength: time = Distance(muzzle.localPosition, baseImpact.localPosition) / GetAdjustedShellSpeed(). More spatially accurate on-screen, requires ImpactMarkerManager.")]
	public LocalComputationMode localComputationMode;

	[Tooltip("If true and UISpacePathLength cannot resolve context, fallback to MirrorGunRangeModel.\nIf false and UISpacePathLength cannot resolve context, display 0.")]
	public bool fallbackToRangeDivSpeedIfNoUIContext;

	[Header("Impact UI Context (for UISpacePathLength)")]
	[Tooltip("If true, automatically find an ImpactMarkerManager in the scene.\nIf false, you must assign 'impactMarkerManagerOverride' below.")]
	public bool autoFindImpactMarkerManager;

	[Tooltip("Optional manual assignment of an ImpactMarkerManager if auto-find is disabled or multiple managers exist.\nREQUIRED for UISpacePathLength mode when auto-find is off.")]
	public ImpactMarkerManager impactMarkerManagerOverride;

	[Header("Events")]
	[Tooltip("Invoked exactly once when the stopwatch actually begins counting down (after any fire delay completes) and the state transitions to 'CountingDown'.\nUse this to trigger audio/visual cues or UI logic that should start at the moment the countdown begins.\nFires only at runtime; not called in Edit Mode.")]
	public UnityEvent onCountdownStarted;

	[Tooltip("Invoked exactly once when the countdown reaches 0 seconds (impact) and is about to enter 'ImpactHold'.\nUse this to trigger impact audio/visual cues or game logic.\nFires only at runtime; not called in Edit Mode.")]
	public UnityEvent onCountdownFinished;

	[Tooltip("Invoked exactly once per shot when the in-flight countdown (after firing + after any fireDelay) crosses from >= 5.0 seconds remaining to < 5.0 seconds remaining.\nImportant:\n- This event does NOT fire during Predicting (elevating/aiming without firing), AwaitFireDelay, or ImpactHold.\n- If the latched travel time is already < 5 seconds when CountingDown begins, this event will fire immediately on the first CountingDown Update.\n- Fires only at runtime; not called in Edit Mode.")]
	public UnityEvent onFiveSecondsRemaining;

	[Tooltip("Invoked every time the stopwatch's displayed whole-second value changes (a 'tick').\nThis stopwatch is configured to tick ONLY while a shell is in-flight (CountingDown state): after fireDelay completes until impact.\nNotes:\n- Ticks are based on the displayed seconds value, floored to an integer.\n- Predicting/AwaitFireDelay/ImpactHold do NOT tick.\nFires only at runtime; not called in Edit Mode.")]
	public UnityEvent onTick;

	[Tooltip("If true, immediately emits one tick when the countdown begins (when entering CountingDown), even if the whole-second value did not change yet.\nUseful for starting a metronome-like tick sound right as the shell begins its in-flight countdown.\nIf false, ticks only happen when the displayed whole-second value changes during CountingDown.\nNote: This setting does NOT cause ticks in Predicting/AwaitFireDelay/ImpactHold.")]
	public bool tickOnStateEnter;

	[Header("Debug (Read-Only)")]
	[Tooltip("Last predicted travel time in seconds (travel only; fireDelay is not included). For diagnostics.")]
	[SerializeField]
	private float lastPredictedTravelTime;

	[Tooltip("Current internal state of the watch. Possible values: Predicting, AwaitFireDelay, CountingDown, ImpactHold.")]
	[SerializeField]
	private string state;

	private WatchState currentState;

	private Quaternion initialHandRotation;

	private double countdownStartTime;

	private float latchedTravelTime;

	private int lastTickWholeSecond;

	private bool hasFiredFiveSecondsEventThisShot;

	private float previousCountingDownRemainingSeconds;

	private ImpactMarkerManager cachedImpactManager;

	private RectTransform cachedParentRect;

	private const float FiveSecondsThreshold = 5f;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	private void SubscribeToGun()
	{
	}

	private void UnsubscribeFromGun()
	{
	}

	private void HandlePredictedImpactTimeChanged(float predictedSeconds)
	{
	}

	private void HandleGunFired()
	{
	}

	private void HandleShellLaunched()
	{
	}

	private void RefreshLivePrediction()
	{
	}

	private float GetCurrentPredictedTravelTime()
	{
		return 0f;
	}

	private float ComputeTravelTimeLocally()
	{
		return 0f;
	}

	private bool ResolveImpactContext()
	{
		return false;
	}

	private bool HasLoadedShell()
	{
		return false;
	}

	private bool HasMeaningfulPrediction()
	{
		return false;
	}

	private float GetDisplaySeconds()
	{
		return 0f;
	}

	private void ApplyHandFromSeconds(float seconds)
	{
	}

	private void SetState(WatchState newState)
	{
	}

	private float GetCurrentlyDisplayedSeconds()
	{
		return 0f;
	}

	private void ResetTickTracking(float displayedSeconds)
	{
	}

	private int FloorToWholeSecond(float seconds)
	{
		return 0;
	}

	private void CheckAndEmitTick(float displayedSeconds)
	{
	}

	private void EmitTickNow()
	{
	}

	private void ResetFiveSecondsTracking()
	{
	}

	private void CheckAndEmitFiveSecondsRemaining(float displayedSeconds)
	{
	}

	[ContextMenu("Reset Stopwatch")]
	public void ResetStopwatch()
	{
	}

	private void CacheImpactContextIfNeeded()
	{
	}
}
