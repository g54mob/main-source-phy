using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class ArtilleryComputer : MonoBehaviour
{
	[Serializable]
	public class CalculationSuccessEvent : UnityEvent<float, float, int, bool>
	{
	}

	[Serializable]
	public class CalculationErrorEvent : UnityEvent<float, int, string>
	{
	}

	[CompilerGenerated]
	private sealed class _003CInvokeSuccessWithDelay_003Ed__44 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delaySeconds;

		public ArtilleryComputer _003C_003E4__this;

		public float elevation;

		public float clampedRange;

		public int inputCharge;

		public bool wasClamped;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CInvokeSuccessWithDelay_003Ed__44(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[Header("References")]
	[Tooltip("Shell / ballistics data source. Must implement GetRangeForCharge(int charge, out float minRange, out float maxRange). If missing, calculations will fail and OnCalculationError is invoked with reason 'NoBlueprint'.")]
	public ShellBlueprint shellBlueprint;

	[Tooltip("Dial providing the desired impact range (units typically meters). Its AccumulatedValue is read live each frame. Changing this re-enables the Calculate button if a previous calculation succeeded.")]
	public DialInteractable rangeDial;

	[Tooltip("Odometer-style numeric display mirroring the live desired range (raw user input, not clamped).")]
	public OdometerDisplay rangeOdometer;

	[Tooltip("Dial selecting the powder charge (integer 1–6 inclusive). Value is auto-snapped & clamped. Changing this re-enables the Calculate button if a previous calculation succeeded.")]
	public DialInteractable powderChargeDial;

	[Tooltip("Odometer-style numeric display for the computed gun elevation (degrees). Updated only on successful calculation OR (optionally) continuously randomized during an error state.")]
	public OdometerDisplay elevationOdometer;

	[Tooltip("Interactive 'Calculate' control (e.g., a LookAtTarget button). Disabled after a successful calculation until inputs change again. If 'Gate Calculate By Minimum Range' is enabled, this is only set active when Desired Range >= 'Min Desired Range To Enable'.")]
	public LookAtTarget calculateButton;

	[Header("Gun Elevation (Degrees)")]
	[Tooltip("Minimum possible elevation angle output (degrees) when desired range equals the minimum reachable range for the selected charge).")]
	public float minElevation;

	[Tooltip("Maximum possible elevation angle output (degrees) when desired range equals the maximum reachable range for the selected charge).")]
	public float maxElevation;

	[Header("Validation")]
	[Tooltip("Absolute tolerance when deciding if the user-entered range is outside the valid band for the selected charge. If DesiredRange < (MinRange - Tolerance) or DesiredRange > (MaxRange + Tolerance) => error (reason 'OutOfRange'). If within tolerance but still outside the core band, the value is clamped and calculation counts as success with wasClamped = true.")]
	public float rangeTolerance;

	[Header("Calculate Button Activation")]
	[Tooltip("If true, the 'Calculate' control is only set active when the live Desired Range (from 'rangeDial.AccumulatedValue') is >= 'Min Desired Range To Enable'. Useful to suppress accidental presses at near-zero values.\n- If 'rangeDial' is unassigned, the Calculate control will remain inactive while this is enabled.")]
	public bool gateCalculateByMinimumRange;

	[Tooltip("Minimum desired range required for the 'Calculate' control to be set active. Units match your range dial. Exact threshold is inclusive (i.e., Calculate becomes active at this value or higher). Default: 0.01")]
	public float minDesiredRangeToEnableCalculate;

	[Header("Error Display Randomization")]
	[Tooltip("If true, after a calculation error the elevation display will continuously show changing random values until a successful calculation occurs.")]
	public bool randomizeElevationOnError;

	[Tooltip("Number of discrete random base elevation values produced per second while in error state. Higher values = more flicker. Set to 0 to disable base stepping (will still apply jitter if enabled).")]
	public float errorRandomUpdatesPerSecond;

	[Tooltip("If NaN, uses Min Elevation as the lower bound for random elevation while in error. Otherwise this explicit value is used and then clamped into [minElevation, maxElevation].")]
	public float errorRandomMinElevation;

	[Tooltip("If NaN, uses Max Elevation as the upper bound for random elevation while in error. Otherwise this explicit value is used and then clamped into [minElevation, maxElevation].")]
	public float errorRandomMaxElevation;

	[Tooltip("Optional smooth sinusoidal jitter amplitude (degrees) layered over the stepped random elevations while in error. Set to 0 for no smooth jitter.")]
	public float errorRandomJitterAmplitude;

	[Tooltip("Frequency (Hz) of the smooth jitter oscillation while in error, if jitter amplitude > 0.")]
	public float errorRandomJitterFrequency;

	[Tooltip("If true, the very first frame of entering an error state forces an immediate random elevation update (no wait for interval).")]
	public bool errorRandomImmediateFirstTick;

	[Tooltip("Clamp the randomized elevation output to the global [Min Elevation, Max Elevation] after jitter. If false, the custom random bounds (pre-clamp) can allow slight overshoot via jitter before final clamp by the display (if any).")]
	public bool hardClampRandomizedElevation;

	[Header("Events")]
	[Tooltip("Invoked after a successful calculation.\nParameter order:\n1) elevationDegrees (float) - Final mapped elevation.\n2) clampedRange (float) - Range actually used (possibly clamped into [minRange,maxRange]).\n3) powderCharge (int) - Charge used.\n4) wasClamped (bool) - True if user-entered range was outside the allowed band but within tolerance and got clamped.")]
	public CalculationSuccessEvent OnCalculationSuccess;

	[Tooltip("Invoked when calculation fails (no blueprint, invalid band, or range truly outside).\nParameter order:\n1) attemptedRange (float) - User-entered range.\n2) powderCharge (int) - Charge attempted.\n3) reason (string) - Reason code.\nPossible reason codes:\n - NoBlueprint: shellBlueprint not assigned.\n - InvalidRangeBand: blueprint returned maxRange <= minRange.\n - OutOfRange: desired range beyond band beyond tolerance.")]
	public CalculationErrorEvent OnCalculationError;

	[Header("Success Delay")]
	[Tooltip("Delay (seconds, uses game Time.time scale) between a successful calculation and invoking 'OnCalculationSuccessWithDelay'. Set to 0 for immediate invocation alongside 'OnCalculationSuccess'. Safe example values: 0, 0.5, 1.0")]
	public float successDelaySeconds;

	[Tooltip("Invoked after a successful calculation, but only after waiting 'Success Delay Seconds'. Parameters match 'OnCalculationSuccess'.\nParameter order:\n1) elevationDegrees (float) - Final mapped elevation.\n2) clampedRange (float) - Range actually used (possibly clamped).\n3) powderCharge (int) - Charge used.\n4) wasClamped (bool) - True if the input range was clamped.")]
	public CalculationSuccessEvent OnCalculationSuccessWithDelay;

	private float lastInputRange;

	private int lastInputCharge;

	private bool waitingForCalculation;

	private bool errorActive;

	private float nextErrorRandomTime;

	private float currentErrorBaseElevation;

	private float lastValidElevation;

	private float errorSeed;

	private Coroutine successDelayRoutine;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
	}

	private void OnRangeInputChanged(float value)
	{
	}

	private void OnPowderChargeChanged(float value)
	{
	}

	private void UpdateRangeOdometer(float value)
	{
	}

	private void UpdateCalculateButtonState(bool requestedActive)
	{
	}

	private void OnCalculateButtonPressed()
	{
	}

	public void ResetCalculationGate()
	{
	}

	private void InvokeCalculationError(float attemptedRange, int charge, string reason)
	{
	}

	[IteratorStateMachine(typeof(_003CInvokeSuccessWithDelay_003Ed__44))]
	private IEnumerator InvokeSuccessWithDelay(float elevation, float clampedRange, int inputCharge, bool wasClamped, float delaySeconds)
	{
		return null;
	}

	private void CancelSuccessDelayIfRunning()
	{
	}
}
