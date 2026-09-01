using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Gun Elevation Dial Binding")]
public class GunElevationDialBinding : MonoBehaviour
{
	public enum BackdriveSource
	{
		CurrentElevation = 0,
		DesiredElevation = 1
	}

	[Header("Gun")]
	[Tooltip("GunController controlled by this binding.\n- Dragging sets desired elevation via Gun.SetDesiredElevationFromDial (immediate).\n- If 'Ignore Dial While Reloading' is enabled, input is ignored while reloading.")]
	[SerializeField]
	private GunController gun;

	[Header("Elevation Dial (Interactive)")]
	[Tooltip("DialInteractable used to control elevation by absolute position.\n- Unlimited mode (recommended): uses 'Dial Degrees Per Elevation Degree' and 'Elevation Offset'.\n- Limited mode: dial output is treated as elevation degrees directly.")]
	[SerializeField]
	private DialInteractable elevationDial;

	[Header("Optional UI Sync & Slider Override Detection")]
	[Tooltip("Optional GunElevationSliderBinding for the SAME gun.\n- While dragging the dial, this binding calls SliderBinding.SetInteractiveSliderVisualOnly(deg) to keep the interactive Desired slider in sync WITHOUT firing OnValueChanged.\n- Also used to detect when the Dial overrides the Slider and to end any active slider drag cleanly.")]
	[SerializeField]
	private GunElevationSliderBinding sliderBindingForVisualSync;

	[Header("Dial Overrides Slider (Events)")]
	[Tooltip("If true, this binding will detect when the Dial takes control over the slider input and raise begin/end events.\nOverride BEGINS at Dial-drag start IF detection conditions are met.\nOverride ENDS at Dial-drag end.")]
	[SerializeField]
	private bool detectAndSignalSliderOverride;

	[Tooltip("Invoked once when the Dial begins overriding the Slider (at Dial drag start when detection conditions are met).")]
	public UnityEvent OnDialOverrideSliderBegan;

	[Tooltip("Invoked once when the Dial stops overriding the Slider (at Dial drag end if an override began).")]
	public UnityEvent OnDialOverrideSliderEnded;

	[Header("Override Detection Tuning")]
	[Tooltip("If true, the Dial will only signal that it overrides the Slider when either:\n- The slider is currently being dragged by the user, OR\n- The last command source was the Slider AND EITHER of these is true:\n  • The gun's absolute elevation speed is >= 'Speed Threshold (deg/s)'.\n  • The absolute |Desired - Current| elevation error is >= 'Delta Threshold (deg)'.\nIf false, legacy behavior is used (override begins whenever the last command source was the Slider, even if stationary).")]
	[SerializeField]
	private bool requireMovementOrDeltaForSliderOverride;

	[Tooltip("Minimum absolute elevation speed (deg/second) considered 'moving quickly' for override detection.\nUsed only when 'Require Movement Or Delta For Slider Override' is true.\nTypical: 0.3–1.0 deg/s. Default: 0.5 deg/s.")]
	[SerializeField]
	[Min(0f)]
	private float sliderOverrideSpeedThresholdDegPerSec;

	[Tooltip("Minimum absolute elevation error |Desired - Current| (degrees) considered a 'notable amount' for override detection when not moving fast.\nUsed only when 'Require Movement Or Delta For Slider Override' is true.\nTypical: 0.3–1.0 deg. Default: 0.5 deg.")]
	[SerializeField]
	[Min(0f)]
	private float sliderOverrideDeltaThresholdDeg;

	[Header("Optional Output-Only Ghost")]
	[Tooltip("Optional output-only LinearSliderInteractable showing DESIRED elevation (ghost) for the SAME gun.\n- While dragging the dial, this ghost slider is also updated for visual sync.")]
	[SerializeField]
	private LinearSliderInteractable desiredSliderGhost;

	[Header("Limits Source (Source of Truth)")]
	[Tooltip("If true, auto-finds a TurretController in parents for min/max elevation clamp (degrees). If none is found, falls back to Gun.Min +45° (warns).")]
	[SerializeField]
	private bool autoFindTurretController;

	[Tooltip("Optional TurretController for clamping elevation values (degrees). Read-only; the turret is not driven by this binding.")]
	[SerializeField]
	private TurretController turretController;

	[Tooltip("If true, values sent to the gun and UI are clamped to [Min..Max] elevation limits derived from the TurretController (or the Gun fallback).")]
	[SerializeField]
	private bool clampValuesToLimits;

	[Header("Dial <-> Elevation Mapping (Unlimited Mode)")]
	[Tooltip("Dial degrees per 1 elevation degree (Unlimited mode). Example: 4 => 4° dial = 1° elevation.")]
	[SerializeField]
	private float dialDegreesPerElevationDegree;

	[Tooltip("Constant offset added after mapping dial degrees to elevation degrees (Unlimited mode).\nUse to calibrate neutral.")]
	[SerializeField]
	private float elevationOffset;

	[Header("Backdrive (Dial Follows Gun)")]
	[Tooltip("If true, the elevation dial is backdriven to follow the gun while NOT dragging.")]
	[SerializeField]
	private bool backdriveDial;

	[Tooltip("When backdriving, follow either the gun's Current or Desired elevation.")]
	[SerializeField]
	private BackdriveSource backdriveSource;

	[Tooltip("If true (Unlimited mode), use dial's smoothing when backdriven. If false, snap instantly.")]
	[SerializeField]
	private bool backdriveUseDialSmoothing;

	[Header("Drag Override Options (Legacy: Speed Dial)")]
	[Tooltip("If true, when user begins dragging and Elevation Speed Dial has non-zero value, reset that dial to 0 (drag takes priority).")]
	[SerializeField]
	private bool dragOverridesElevationSpeedDial;

	[Tooltip("Optional Elevation Speed Dial to override (reset to 0 on drag begin if non-zero).")]
	[SerializeField]
	private DialInteractable elevationSpeedDialToOverride;

	[Header("Reload Integration")]
	[Tooltip("If true, dial input is ignored while the gun is reloading (Gun.IsReloading).")]
	[SerializeField]
	private bool ignoreDialWhileReloading;

	[Header("Events (Legacy: Speed Dial Override)")]
	[Tooltip("Invoked once when the Elevation Speed Dial is reset to neutral at Dial drag begin (legacy override).")]
	public UnityEvent OnElevationDragOverrideSpeedDial;

	[Tooltip("Invoked once when a legacy speed-dial override begins at Dial drag start.")]
	public UnityEvent OnElevationOverrideBegan;

	[Tooltip("Invoked once when a legacy speed-dial override ends at Dial drag end.")]
	public UnityEvent OnElevationOverrideEnded;

	private float minDeg;

	private float maxDeg;

	private bool dialDragActive;

	private float dialBaseElevationDeg;

	private bool overrideActiveThisDrag_SpeedDial;

	private bool overrideActiveThisDrag_Slider;

	[SerializeField]
	[Tooltip("If true, logs helpful warnings (e.g., missing references or missing TurretController for limits).")]
	private bool logWarnings;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
	}

	private void OnBeginDialDrag()
	{
	}

	private void OnEndDialDrag()
	{
	}

	private void ResolveLimits()
	{
	}

	private float MapDialToElevation(float dialDegrees)
	{
		return 0f;
	}

	private float MapElevationToDialDegrees(float elevationDeg)
	{
		return 0f;
	}
}
