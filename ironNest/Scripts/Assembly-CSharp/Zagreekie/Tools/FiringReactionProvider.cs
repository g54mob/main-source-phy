using UnityEngine;

namespace Zagreekie.Tools
{
	[AddComponentMenu("Zagreekie/Artillery/Firing Reaction Provider")]
	public sealed class FiringReactionProvider : MonoBehaviour
	{
		[Header("References")]
		[Tooltip("The single ArmedFireRelayOneShot shared by both guns.\n\nPolled each frame via IsAnyArmed():\n  Any side armed   → armed ramp begins.\n  All sides disarm → armed ramp releases back to 0.\nLeft/Right distinction is ignored; only the aggregate armed state matters.")]
		[SerializeField]
		private ArmedFireRelayOneShot _relay;

		[Tooltip("The SliderEnergyMomentumSpinner shared by both guns.\n\nReads EnergyNormalized (0–1) each frame while the relay is armed.\nAdditively lifts the armed channel from ValueOnArmed toward\nValueOnArmed + ValueAtFullSpinner (clamped to 1).\n\nLeave empty if there is no spinner.")]
		[SerializeField]
		private SliderEnergyMomentumSpinner _spinner;

		[Tooltip("GunController for the LEFT gun.\n\nSubscribed to OnGunFired (to start the in-flight decay) and\nOnPredictedImpactTimeChanged (to keep a latched travel time\nthat is always fresh by the time the fire event arrives).\nThis mirrors the approach used by GunStopwatch.")]
		[SerializeField]
		private GunController _gunLeft;

		[Tooltip("GunController for the RIGHT gun.\n\nSubscribed to OnGunFired and OnPredictedImpactTimeChanged.\n\nLeave empty if there is only one gun.")]
		[SerializeField]
		private GunController _gunRight;

		[Header("Armed Channel  (0 = no effect · 1 = full output)")]
		[Tooltip("Output level the armed channel ramps toward while the relay is\narmed and the spinner is at rest (EnergyNormalized = 0).\n\nThis is the baseline pre-fire tension value.\nThe spinner then lifts it further toward\nValueOnArmed + ValueAtFullSpinner.\n\nSuggested starting range: 0.10 – 0.30")]
		[SerializeField]
		[Range(0f, 1f)]
		private float _valueOnArmed;

		[Tooltip("Additional output added on top of ValueOnArmed when the spinner\nreaches full energy (EnergyNormalized = 1).\n\nArmed channel ceiling = ValueOnArmed + ValueAtFullSpinner (clamped to 1).\n\nSuggested starting range: 0.20 – 0.40")]
		[SerializeField]
		[Range(0f, 1f)]
		private float _valueAtFullSpinner;

		[Header("Output")]
		[Tooltip("When enabled, the output is remapped so that 'no effect' = 1\nand 'full reaction' = 0.  Output becomes: 1 - Value.\n\nUse this when your downstream system treats 1 as 'normal'\nand 0 as 'maximum effect' (e.g. a volume parameter where\n1 = full volume and 0 = fully muffled).\n\nWhen disabled (default): 0 = no effect, 1 = full reaction.")]
		[SerializeField]
		private bool _invertOutput;

		[Header("Smoothing  (all values are exponential time-constants τ in seconds)")]
		[Tooltip("How quickly the armed channel rises toward its target.\n\nτ is the time for ~63 % of the change to complete (RC-filter style).\nLower = snappier tension build.  Higher = slow, creeping build.\n\nSuggested starting range: 0.5 – 2.0 s")]
		[SerializeField]
		[Min(0.01f)]
		private float _armedRampUpTau;

		[Tooltip("How quickly the armed channel falls back to zero when the\nrelay fully disarms without a shot being fired.\n\nSuggested starting range: 0.5 – 3.0 s")]
		[SerializeField]
		[Min(0.01f)]
		private float _disarmReleaseTau;

		[Header("In-Flight Decay Curve")]
		[Tooltip("Shape of each gun's post-fire decay from the moment of firing\nto the end of the post-impact tail.\n\nX axis: normalised progress across the full decay window\n        (0 = the frame the gun fires · 1 = end of post-impact tail)\nY axis: multiplier applied to Curve Peak Scale\n        (1 = full peak · 0 = silent)\n\nThe contribution is driven directly by this curve each frame —\nno additional smoothing is applied.\n\nDefault: linear fade from 1 → 0.\n\nAlternatives worth trying:\n  Ease-out  – fast initial drop then slow tail.\n  Hold-then-fade – flat at 1 for 20–30 % of flight, then drop.\n  S-curve   – gentle start, fast middle, gentle end.")]
		[SerializeField]
		private AnimationCurve _inFlightDecayCurve;

		[Tooltip("Multiplier applied to the in-flight decay curve's Y output.\n\nThis scales the peak intensity of the post-fire reaction without\nchanging the curve's shape.\n\n  1.0  = curve Y drives the full 0–1 range.\n  0.75 = curve peak reaches 0.75 even when Y = 1.\n  0.0  = post-fire channel is disabled entirely.\n\nSuggested starting range: 0.80 – 1.00")]
		[SerializeField]
		[Range(0f, 1f)]
		private float _curvePeakScale;

		[Tooltip("Fallback in-flight duration (seconds) used when a gun's\nlatched predicted travel time is zero at the moment of firing.\n\nPrevents the post-fire decay from becoming permanent.\nThis is a safety net; in normal operation the latched value\nshould always be valid (see OnPredictedImpactTimeChanged).\n\nSuggested value: 3 – 8 s depending on typical range.")]
		[SerializeField]
		[Min(0.1f)]
		private float _fallbackFlightDuration;

		[Tooltip("Extra seconds appended to the decay window after the shell's\nlatched travel time (or the fallback duration).\n\nTotal decay window = latched flight time + Post-Impact Tail.\n\nThe decay curve's X axis runs 0 → 1 across this full window,\nso the tail is simply an extension of the curve beyond impact.\nSet to 0 to disable (decay ends exactly at predicted impact).\n\nSuggested value: 1 – 3 s.")]
		[SerializeField]
		[Min(0f)]
		private float _postImpactTailDuration;

		[Header("Debug  (Live Read-only)")]
		[Tooltip("Current armed-ramp channel contribution (0–1). Read-only.")]
		[SerializeField]
		private float _debugArmedContribution;

		[Tooltip("Gun Left in-flight decay channel contribution (0–1). 0 when idle. Read-only.")]
		[SerializeField]
		private float _debugGunLeftContribution;

		[Tooltip("Latched travel time used for Gun Left's current or next decay window (seconds). Read-only.")]
		[SerializeField]
		private float _debugGunLeftLatchedTime;

		[Tooltip("Gun Right in-flight decay channel contribution (0–1). 0 when idle. Read-only.")]
		[SerializeField]
		private float _debugGunRightContribution;

		[Tooltip("Latched travel time used for Gun Right's current or next decay window (seconds). Read-only.")]
		[SerializeField]
		private float _debugGunRightLatchedTime;

		[Tooltip("Final output sent to downstream consumers.\n\nInvert Output OFF: 0 = no effect, 1 = full reaction.\nInvert Output ON:  1 = no effect, 0 = full reaction.\n\nWire to FMODParameterSetter:\n  floatValueProvider   = this FiringReactionProvider\n  providerPropertyName = \"Value\"\nRead-only.")]
		[SerializeField]
		private float _debugOutput;

		private float _armedContribution;

		private float _gunLeftLatchedTravelTime;

		private float _gunRightLatchedTravelTime;

		private float _gunLeftContribution;

		private float _gunLeftFlightTotal;

		private float _gunLeftFlightRemaining;

		private float _gunRightContribution;

		private float _gunRightFlightTotal;

		private float _gunRightFlightRemaining;

		public float Value { get; private set; }

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
		}

		private void OnGunLeftImpactTimeChanged(float predictedSeconds)
		{
		}

		private void OnGunRightImpactTimeChanged(float predictedSeconds)
		{
		}

		private void UpdateArmedRamp(float dt)
		{
		}

		private void UpdateGunDecay(ref float contribution, ref float flightRemaining, float flightTotal, float dt)
		{
		}

		private void OnGunLeftFired()
		{
		}

		private void OnGunRightFired()
		{
		}

		private void HandleFire(float latchedTravelTime, ref float contribution, ref float flightTotal, ref float flightRemaining)
		{
		}

		private static float ComputeTravelTime(GunController gun)
		{
			return 0f;
		}

		private static float SmoothToward(float current, float target, float tau, float dt)
		{
			return 0f;
		}
	}
}
