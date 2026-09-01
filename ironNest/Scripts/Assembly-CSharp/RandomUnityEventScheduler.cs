using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[AddComponentMenu("Gameplay/Random Unity Event Scheduler")]
public sealed class RandomUnityEventScheduler : MonoBehaviour
{
	public enum IntervalMode
	{
		FixedInterval = 0,
		RandomRangePerAttempt = 1
	}

	public enum Clock
	{
		ScaledTime = 0,
		UnscaledTime = 1
	}

	[CompilerGenerated]
	private sealed class _003CRun_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RandomUnityEventScheduler _003C_003E4__this;

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
		public _003CRun_003Ed__38(int _003C_003E1__state)
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

	[Header("Start & Lifecycle")]
	[SerializeField]
	[Tooltip("If enabled, the scheduler automatically starts when this component is enabled.\nIf disabled, you must call StartSchedule() (e.g., via another UnityEvent) to begin.\nDesigner tip: Leave ON for fire-and-forget behavior; turn OFF to coordinate with other systems.")]
	private bool autoStartOnEnable;

	[SerializeField]
	[Tooltip("Seconds to wait before the FIRST attempt after starting (or restarting via ResetSchedule when 'Restart After Reset' is ON).\nRespects the selected Clock (Scaled or Unscaled).\nSafe examples: 0 (immediate), 2.5 (start attempts after 2.5 seconds).")]
	private float startDelaySeconds;

	[SerializeField]
	[Tooltip("Time source for all waits:\n- ScaledTime: Pauses if Time.timeScale = 0 (e.g., game paused).\n- UnscaledTime: Continues regardless of Time.timeScale.\nChoose Unscaled for UI / meta systems; Scaled for gameplay pacing.")]
	private Clock clock;

	[SerializeField]
	[Tooltip("If enabled, calling ResetSchedule() will clear state and immediately start again (honoring Start Delay).\nIf disabled, ResetSchedule() leaves the scheduler stopped until StartSchedule() is called.")]
	private bool restartAfterReset;

	[Header("Attempt Cadence")]
	[SerializeField]
	[Tooltip("How time between attempts is chosen:\n- FixedInterval: Constant spacing using 'Fixed Interval Seconds'.\n- RandomRangePerAttempt: Each gap is a new random value between 'Random Interval Min Seconds' and 'Random Interval Max Seconds'.\nUse RandomRangePerAttempt for less predictable timing.")]
	private IntervalMode intervalMode;

	[SerializeField]
	[Tooltip("Used only when Interval Mode = FixedInterval.\nSeconds between attempts (>= 0). Values < 0 act as 0 (attempts every frame).\nExample: 5 = one attempt every 5 seconds.")]
	private float fixedIntervalSeconds;

	[SerializeField]
	[Tooltip("Used only when Interval Mode = RandomRangePerAttempt.\nLower bound (inclusive) for random seconds between attempts. NOT auto-corrected in Inspector.\nRuntime rules:\n- If negative, treated as 0.\n- If Min > Max, a warning logs once and Min is used as a fixed interval until corrected.\n- If Min == Max, behaves like a fixed interval.\nExamples: 1 (with Max 3) = 1–3s; 0 (with Max 4) = 0–4s.")]
	private float randomIntervalMinSeconds;

	[SerializeField]
	[Tooltip("Used only when Interval Mode = RandomRangePerAttempt.\nUpper bound (inclusive) for random seconds between attempts. NOT auto-corrected.\nRuntime rules mirror Min field:\n- Negative treated as 0.\n- If Max < Min, a warning logs once and Min acts as fixed interval.\n- If equal to Min, acts fixed.\nExamples: 7 (with Min 3) = 3–7s; 5 (with Min 5) = fixed 5s.")]
	private float randomIntervalMaxSeconds;

	[Header("Trigger Logic")]
	[SerializeField]
	[Range(0f, 100f)]
	[Tooltip("Chance (percent) that an attempt triggers the main event.\nEvaluated independently each attempt.\n0 = never, 100 = always, 50 = ~half.\nExamples: 25 (rare), 75 (frequent).")]
	private float triggerChancePercent;

	[SerializeField]
	[Tooltip("If enabled, triggers can occur unlimited times (ignores Max Triggers).\nIf disabled, scheduler stops after reaching Max Triggers.")]
	private bool unlimitedTriggers;

	[SerializeField]
	[Tooltip("Maximum number of successful triggers before stopping.\nUsed only when Unlimited Triggers is OFF.\nMust be >= 1. Example: 3 = stop after three successful triggers.")]
	private int maxTriggers;

	[Header("Events")]
	[SerializeField]
	[Tooltip("Invoked once whenever scheduling starts (auto or manual).")]
	private UnityEvent onStarted;

	[SerializeField]
	[Tooltip("Invoked at the start of EVERY attempt before chance evaluation.\nUse for telemetry or secondary effects that should happen even if not triggered.")]
	private UnityEvent onAttempt;

	[SerializeField]
	[Tooltip("Invoked when an attempt passes the chance roll.\nConnect gameplay actions, audio, VFX, spawning, etc.")]
	private UnityEvent onTriggered;

	[SerializeField]
	[Tooltip("Invoked once when the scheduler reaches its max successful triggers (only if Unlimited Triggers is OFF).")]
	private UnityEvent onMaxedOut;

	[SerializeField]
	[Tooltip("Invoked after ResetSchedule() clears internal state. Fires before optional auto-restart.")]
	private UnityEvent onReset;

	private Coroutine _runner;

	private int _successfulTriggers;

	private int _attempts;

	private bool _hasNotifiedMax;

	private bool _rangeWarningLogged;

	public bool IsRunning => false;

	public int SuccessfulTriggers => 0;

	public int Attempts => 0;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnValidate()
	{
	}

	[ContextMenu("Start Schedule")]
	public void StartSchedule()
	{
	}

	[ContextMenu("Stop Schedule")]
	public void StopSchedule()
	{
	}

	[ContextMenu("Reset Schedule")]
	public void ResetSchedule()
	{
	}

	[ContextMenu("Force Attempt (Apply Chance)")]
	public void ForceAttempt()
	{
	}

	[ContextMenu("Force Trigger (Count As Trigger = true)")]
	public void ForceTrigger()
	{
	}

	public void ForceTrigger(bool countAsTrigger)
	{
	}

	[IteratorStateMachine(typeof(_003CRun_003Ed__38))]
	private IEnumerator Run()
	{
		return null;
	}

	private void InternalAttempt()
	{
	}

	private bool HasReachedMax()
	{
		return false;
	}

	private void NotifyMaxedOutOnce()
	{
	}

	private float NextIntervalSeconds()
	{
		return 0f;
	}

	private bool TryPassChance(float percent)
	{
		return false;
	}

	private object WaitForSecondsDynamic(float seconds)
	{
		return null;
	}
}
