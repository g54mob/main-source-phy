using Unity.Cinemachine;
using UnityEngine;

[DisallowMultipleComponent]
[DefaultExecutionOrder(10000)]
public sealed class CinemachineImpulseListenerToSwingBridge : MonoBehaviour
{
	public enum QueryPositionMode
	{
		UseCameraTransformPosition = 0,
		UseManualPosition = 1
	}

	public enum DirectionMode
	{
		RandomXZ = 0,
		FixedXZ = 1,
		FromImpulsePositionXZ = 2
	}

	[Header("References")]
	[SerializeField]
	[Tooltip("The CinemachineImpulseListener that is already on your camera/virtual camera.\nThis bridge will READ its settings (ChannelMask, Gain, Use2DDistance, UseCameraSpace, SignalCombinationMode, ReactionSettings)\nand query CinemachineImpulseManager in the same way to estimate current shake intensity.\n\nNo modifications are made to the listener. This is read-only.\n\nIf left unassigned, the bridge will try to find one on this GameObject.")]
	private CinemachineImpulseListener impulseListener;

	[SerializeField]
	[Tooltip("The SwingController that will receive impulses based on active Cinemachine screen shake.\n\nThis bridge calls:\n  SwingController.TriggerExternalImpulse(worldXZ, worldTwistImpulse)\n\nSo SwingController must implement that public method.")]
	private SwingController swingController;

	[Header("Sampling")]
	[SerializeField]
	[Tooltip("If true, this bridge uses the listener's SignalCombinationMode to decide whether to query:\n- GetImpulseAt (Additive)\n- GetStrongestImpulseAt (UseLargest)\n\nIf false, it always uses GetImpulseAt (Additive).")]
	private bool mirrorListenerCombinationMode;

	[SerializeField]
	[Tooltip("If true, includes the listener's ReactionSettings (secondary vibration) in the computed shake.\nThis makes the swing respond to both the primary impulse and the secondary reaction noise.\n\nNote:\nCalling ReactionSettings.GetReaction() updates internal state in the ReactionSettings struct.\nIn practice this is usually OK, but if you want to avoid any chance of interacting with the listener's internal reaction state,\nset this to false and the bridge will use only the primary impulse from CinemachineImpulseManager.")]
	private bool includeReactionSettings;

	[SerializeField]
	[Tooltip("The world position used when querying CinemachineImpulseManager.\n\nOptions:\n- UseCameraTransformPosition: uses this GameObject's transform.position (typically the camera)\n- UseManualPosition: uses ManualQueryPosition\n\nRecommendation:\nIf this component is on the same object as the camera, leave at UseCameraTransformPosition.")]
	private QueryPositionMode queryPositionMode;

	[SerializeField]
	[Tooltip("Only used if Query Position Mode = UseManualPosition.\nThis world position will be used to query impulses.")]
	private Vector3 manualQueryPosition;

	[Header("Intensity Measurement")]
	[SerializeField]
	[Min(0f)]
	[Tooltip("Minimum computed shake intensity required before the bridge triggers swing impulses.\nThis filters out tiny micro-shakes and numerical noise.\n\nIntensity is computed from:\n- impulsePos magnitude\n- plus (optional) impulseRot angle contribution scaled by RotationAngleToIntensity\n\nTune this with LogDebug enabled.")]
	private float intensityThreshold;

	[SerializeField]
	[Min(0f)]
	[Tooltip("How much rotational shake contributes to the final intensity.\n\nWe compute rotation angle in degrees as:\n  angleDeg = Quaternion.Angle(identity, impulseRot)\n\nThen intensity = impulsePos.magnitude + angleDeg * RotationAngleToIntensity.\n\nSet to 0 to ignore rotation completely.")]
	private float rotationAngleToIntensity;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("Optional smoothing applied to intensity to prevent flicker/noise.\n0 = no smoothing.\nHigher values = smoother but slower response.\nRecommended: 0.05 to 0.2.")]
	private float intensitySmoothing;

	[Header("Mapping: Intensity -> Swing Impulse Strength")]
	[SerializeField]
	[Tooltip("AnimationCurve mapping measured shake intensity (X) to swing impulse strength (Y).\n\nNo clamping or remapping is performed. Curve Evaluate(intensity) is used directly.\n\nSafe example keys:\n- (0, 0)\n- (0.25, 0.5)\n- (1.0, 2.0)")]
	private AnimationCurve intensityToSwingStrength;

	[SerializeField]
	[Tooltip("Global multiplier applied after curve evaluation.\nUse this to tune the overall effect without editing the curve.")]
	private float strengthMultiplier;

	[Header("Impulse Output (World Space)")]
	[SerializeField]
	[Tooltip("How the bridge chooses the world-space impulse direction for swinging objects.\n\nOptions:\n- RandomXZ: random world XZ direction\n- FixedXZ: fixed direction from FixedDirectionWorldXZ\n- FromImpulsePositionXZ: uses the impulse position delta projected into XZ (direction of camera positional shake)\n\nIf the chosen direction has near-zero magnitude, the bridge falls back to FixedDirectionWorldXZ.")]
	private DirectionMode directionMode;

	[SerializeField]
	[Tooltip("Used when Direction Mode is FixedXZ, and also as a fallback when other direction modes produce a near-zero vector.\nX = world +X, Y = world +Z.\n\nExamples:\n- (1, 0): push toward world +X\n- (0, 1): push toward world +Z")]
	private Vector2 fixedDirectionWorldXZ;

	[SerializeField]
	[Tooltip("Random twist impulse range around WORLD Y applied each time a swing impulse is triggered.\nSet both to 0 to disable twist.\n\nThis is passed to SwingController.TriggerExternalImpulse as the 'worldTwistImpulse'.")]
	private Vector2 randomTwistImpulseWorldYMinMax;

	[Header("Rate Limiting")]
	[SerializeField]
	[Min(0f)]
	[Tooltip("Minimum time between swing impulses while shaking is active.\nThis prevents spamming impulses every frame during sustained shake.\n\nRecommended:\n- 0.00 for very reactive props\n- 0.03 to 0.12 for controlled impulses\n\nIf set to 0, an impulse may be triggered every frame while intensity is above threshold.")]
	private float minSecondsBetweenImpulses;

	[Header("Debug")]
	[SerializeField]
	[Tooltip("If true, logs the computed intensity occasionally (Play Mode only) to help tuning.\nDisable in production.")]
	private bool logDebug;

	[SerializeField]
	[Min(0.01f)]
	[Tooltip("Minimum seconds between debug log prints when Log Debug is enabled.")]
	private float debugLogInterval;

	private float _smoothedIntensity;

	private float _nextImpulseTime;

	private float _nextLogTime;

	private void Reset()
	{
	}

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private Vector2 GetDirectionXZFromMode(Vector3 impulsePos)
	{
		return default(Vector2);
	}
}
