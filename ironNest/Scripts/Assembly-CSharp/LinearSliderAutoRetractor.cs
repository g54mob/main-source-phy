using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("Gameplay/Linear Slider Auto Retractor")]
public class LinearSliderAutoRetractor : MonoBehaviour
{
	public enum RetractMode
	{
		SmoothDamp = 0,
		AccelLimited = 1
	}

	[CompilerGenerated]
	private sealed class _003CRetractCoroutine_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LinearSliderAutoRetractor _003C_003E4__this;

		public float restValue;

		private float _003Ct_003E5__2;

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
		public _003CRetractCoroutine_003Ed__30(int _003C_003E1__state)
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
	[Tooltip("The LinearSliderInteractable to retract.\nIf left empty, this component will try to find one on the same GameObject first, then in children.\nThis script never reads input; it only listens to the slider's DragStarted/DragEnded events.")]
	[SerializeField]
	private LinearSliderInteractable slider;

	[Header("Retract Target")]
	[Tooltip("If true, the retract target is the slider's minimum output value (as produced by slider.ResetToMinimum()).\nIf false, retracts to 'Custom Rest Value' instead.\n\nNote: Because LinearSliderInteractable does not expose minOutputValue, the 'true' option temporarily calls\nResetToMinimum() to discover the minimum, then restores the previous value immediately. This may invoke\nOnValueChanged twice very briefly (min then restore). If that is undesirable, set this to false and specify\nCustom Rest Value manually.")]
	[SerializeField]
	private bool retractToSliderMinimum;

	[Tooltip("Custom rest value to retract to when 'Retract To Slider Minimum' is false.\nThis value is clamped by the slider's SetSliderValue() to its allowed output range.\nSafe example (0..100 slider): 0.")]
	[SerializeField]
	private float customRestValue;

	[Header("Timing")]
	[Tooltip("Unscaled seconds to wait after the player releases the slider before retracting begins.\n0 = start retract immediately.\nUses unscaled time so it still works if Time.timeScale is 0 (pause menus, slowmo setups).")]
	[SerializeField]
	[Min(0f)]
	private float startDelaySeconds;

	[Header("Retract Motion")]
	[Tooltip("How the slider returns to rest.\n\nSmoothDamp:\n- Classic smoothing toward target (ease-out).\n- Simple and stable.\n\nAccelLimited (recommended for 'heavy object'):\n- Maintains a velocity and applies an acceleration limit.\n- Feels like mass/inertia: starts slower, accelerates, then brakes near the target.\n- Uses a PD-like controller (spring + damping) with explicit max acceleration and max speed caps.")]
	[SerializeField]
	private RetractMode retractMode;

	[Tooltip("If enabled, the retract motion uses UNscaled delta time (recommended).\nIf disabled, uses scaled delta time (affected by Time.timeScale).")]
	[SerializeField]
	private bool useUnscaledTime;

	[Header("SmoothDamp Settings")]
	[Tooltip("Only used when Retract Mode is SmoothDamp.\nApproximate smoothing time (seconds) to ease toward the rest value.\nLower = snappier. Higher = more floaty.\nTime source depends on 'Use Unscaled Time'.")]
	[SerializeField]
	[Range(0.01f, 2f)]
	private float smoothTimeSeconds;

	[Header("AccelLimited (Heavy) Settings")]
	[Tooltip("Only used when Retract Mode is AccelLimited.\nMaximum retract speed in OUTPUT VALUE units per second.\nExample (0..100 slider): 120 means it could traverse the whole range in under 1 second once up to speed.\nSet lower for heavier feel.")]
	[SerializeField]
	[Min(0.0001f)]
	private float maxSpeedValuePerSecond;

	[Tooltip("Only used when Retract Mode is AccelLimited.\nMaximum acceleration in OUTPUT VALUE units per second squared.\nLower values feel heavier (slow to start/stop). Higher values feel snappier.\nExample (0..100 slider): 250..800 depending on desired heaviness.")]
	[SerializeField]
	[Min(0.0001f)]
	private float maxAccelerationValuePerSecondSq;

	[Tooltip("Only used when Retract Mode is AccelLimited.\nSpring strength (proportional gain). Higher pulls harder toward the rest value.\nIf too high relative to damping/maxAcceleration, can cause oscillation (which may be desirable for 'spring').\nTypical range: 10..80 for a 0..100 output slider.")]
	[SerializeField]
	[Min(0f)]
	private float springStrength;

	[Tooltip("Only used when Retract Mode is AccelLimited.\nDamping (derivative gain). Higher resists velocity more and reduces overshoot.\nTypical range: 5..30 for a 0..100 output slider.\nIf you want it to feel like a heavy object on a rope/winch, use moderate spring with higher damping.")]
	[SerializeField]
	[Min(0f)]
	private float damping;

	[Tooltip("Only used when Retract Mode is AccelLimited.\nIf true, velocity is set to 0 when the player begins dragging (so it doesn't 'carry momentum' into the next pull).\nIf false, velocity is preserved when interrupted, which can feel more physical but sometimes surprising.")]
	[SerializeField]
	private bool zeroVelocityWhenGrabbed;

	[Header("Finish / Snap")]
	[Tooltip("When the slider is within this absolute distance (in OUTPUT VALUE units) from rest, the retract completes and snaps to rest.\nExample (0..100 slider): 0.02..0.2 depending on how precise you want it.")]
	[SerializeField]
	[Min(0f)]
	private float snapEpsilonValue;

	[Header("Optional: Retract Only If Pulled")]
	[Tooltip("If true, retract only starts if the slider was moved at least 'Minimum Pull Amount' away from rest.\nThis can prevent tiny accidental releases from causing a visible retract.")]
	[SerializeField]
	private bool requireMinimumPull;

	[Tooltip("Only used when Require Minimum Pull is true.\nMinimum absolute difference (in OUTPUT VALUE units) between current value and rest value required to start retracting.\nExample (0..100 slider): 1 or 2.")]
	[SerializeField]
	[Min(0f)]
	private float minimumPullAmountValue;

	private Coroutine _retractRoutine;

	private float _smoothVelocity;

	private float _velocityValuePerSecond;

	private float Dt => 0f;

	private void Reset()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void HandleDragStarted()
	{
	}

	private void HandleDragEnded()
	{
	}

	private float GetRestValue()
	{
		return 0f;
	}

	private void StartRetract(float restValue)
	{
	}

	private void StopRetract()
	{
	}

	[IteratorStateMachine(typeof(_003CRetractCoroutine_003Ed__30))]
	private IEnumerator RetractCoroutine(float restValue)
	{
		return null;
	}
}
