using System;
using UnityEngine;

[AddComponentMenu("Audio/Bridges/Time To Impact (Seconds) Bridge")]
public class TimeToImpactSecondsBridge : MonoBehaviour
{
	public enum PredictionMode
	{
		UseGunControllerPredictedImpactTime = 0,
		ComputeFromGunAtFireTime = 1
	}

	[Serializable]
	private class ShotCountdown
	{
		[SerializeField]
		private double startTime;

		[SerializeField]
		private float durationSeconds;

		public void Begin(double now, float duration)
		{
		}

		public float GetRemaining(double now)
		{
			return 0f;
		}

		public bool IsActive(double now)
		{
			return false;
		}

		public void Clear()
		{
		}
	}

	[Header("Gun Sources")]
	[Tooltip("First GunController to observe.\n\nWhen this gun launches a shell, the bridge latches the predicted flight time and begins a countdown.\n\nLeave null if you only want to use Gun B.")]
	[SerializeField]
	private GunController gunA;

	[Tooltip("Second GunController to observe (optional).\n\nWhen this gun launches a shell, the bridge latches the predicted flight time and begins a countdown.\n\nIf both guns have shots in flight, the bridge output is always the LOWEST remaining time (next impact).")]
	[SerializeField]
	private GunController gunB;

	[Header("Prediction Source")]
	[Tooltip("How the bridge obtains the travel time (flight-only) at the instant of firing.\n\n- UseGunControllerPredictedImpactTime: uses GunController.PredictedImpactTime (seconds).\n- ComputeFromGunAtFireTime: computes range/speed locally using the gun state at fire time.\n\nSafe default: UseGunControllerPredictedImpactTime.")]
	[SerializeField]
	private PredictionMode predictionMode;

	[Header("No In-Flight Fallback")]
	[Tooltip("When neither gun currently has a shot in flight, the bridge outputs this fallback value in seconds.\n\nUse this to drive FMOD into a stable 'idle' state when no shell is flying.\n\nExample safe values:\n- 999 (treat as 'no impact pending')\n- 0 (treat as 'impact now' / silence)")]
	[Min(0f)]
	[SerializeField]
	private float noInFlightFallbackSeconds;

	[Header("Output Conditioning")]
	[Tooltip("If true, clamps the bridge output to the range [outputMinSeconds .. outputMaxSeconds].\n\nEnable this if your FMOD parameter expects a bounded range.\nIf disabled, the fallback value and real countdown values are passed through as-is.")]
	[SerializeField]
	private bool clampOutput;

	[Tooltip("Minimum seconds output when Clamp Output is enabled.\n\nSafe default: 0 (never output negative).")]
	[SerializeField]
	private float outputMinSeconds;

	[Tooltip("Maximum seconds output when Clamp Output is enabled.\n\nSet this to the largest value your FMOD parameter mapping expects.\nExample: 60 if you only care about up to one minute remaining.")]
	[SerializeField]
	private float outputMaxSeconds;

	[Tooltip("If > 0, rounds the output to the specified number of decimal places.\n\nThis can reduce tiny jitter in parameter updates.\nExamples:\n- 0 => whole seconds (5, 4, 3...)\n- 1 => tenths (5.2, 5.1...)\n- 2 => hundredths (5.23...)")]
	[Range(0f, 4f)]
	[SerializeField]
	private int roundToDecimals;

	[Header("Debug (Read-Only)")]
	[Tooltip("The current bridge output in seconds.\n\nThis is the value you should read via reflection from FMODParameterSetter:\nproviderPropertyName = \"CurrentTimeToImpactSeconds\".")]
	[SerializeField]
	private float inspectorCurrentOutputSeconds;

	[Tooltip("True while Gun A currently has an active in-flight countdown.")]
	[SerializeField]
	private bool inspectorAActive;

	[Tooltip("True while Gun B currently has an active in-flight countdown.")]
	[SerializeField]
	private bool inspectorBActive;

	[Tooltip("Remaining seconds for Gun A's in-flight shot (flight-only). 0 means no active shot or impact reached.")]
	[SerializeField]
	private float inspectorARemainingSeconds;

	[Tooltip("Remaining seconds for Gun B's in-flight shot (flight-only). 0 means no active shot or impact reached.")]
	[SerializeField]
	private float inspectorBRemainingSeconds;

	private ShotCountdown shotA;

	private ShotCountdown shotB;

	public float CurrentTimeToImpactSeconds => 0f;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	public void ClearShots()
	{
	}

	private void HandleGunAFired()
	{
	}

	private void HandleGunBFired()
	{
	}

	private void StartCountdownForGun(GunController gun, ShotCountdown shot)
	{
	}

	private float GetTravelTimeAtFireTimeSeconds(GunController gun)
	{
		return 0f;
	}

	private float ComputeLowestRemainingOrFallback(double now)
	{
		return 0f;
	}

	private static float Round(float value, int decimals)
	{
		return 0f;
	}

	private static void Subscribe(GunController gun, Action handler)
	{
	}

	private static void Unsubscribe(GunController gun, Action handler)
	{
	}
}
