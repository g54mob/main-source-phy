using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public sealed class SwingLightFlicker : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CFlickerBurstRoutine_003Ed__54 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SwingLightFlicker _003C_003E4__this;

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
		public _003CFlickerBurstRoutine_003Ed__54(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CPassiveFlickerLoop_003Ed__52 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SwingLightFlicker _003C_003E4__this;

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
		public _003CPassiveFlickerLoop_003Ed__52(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CPowerRestoreRoutine_003Ed__53 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SwingLightFlicker _003C_003E4__this;

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
		public _003CPowerRestoreRoutine_003Ed__53(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CQuickFlickerRoutine_003Ed__55 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SwingLightFlicker _003C_003E4__this;

		public bool shorterRecovery;

		private int _003Ctoggles_003E5__2;

		private float _003CminI_003E5__3;

		private float _003CmaxI_003E5__4;

		private int _003Ci_003E5__5;

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
		public _003CQuickFlickerRoutine_003Ed__55(int _003C_003E1__state)
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
	[Header("Standalone Mode (Ignores Controller Entirely)")]
	[SerializeField]
	[Tooltip("If enabled, this light operates completely independently of the SwingLightFlickerController.\n\nWhile in standalone mode:\n- This light does NOT register with the controller (master power signals are ignored entirely).\n- SetMasterPowerState() calls have no effect.\n- The light starts ON by default (unless 'Start Manually Off When Enabled' overrides this).\n- Motion-driven flicker still works if a SwingReceiver is present.\n- Passive flicker still works if enabled.\n- Manual override still works.\n\nUse this for lights that should be permanently on and self-managed,\nwith no dependency on a scene-level power controller.\n\nDefault: OFF (light registers with the controller as normal).")]
	private bool standaloneMode;

	[SerializeField]
	[Tooltip("Optional SwingReceiver to observe for motion + spike data.\nIf unassigned, this component will search on this GameObject and then in parents.\n\nIf still not found, this light will:\n- Skip ALL motion-driven flicker/brownout behavior (no swinging required)\n- Still fully support master power OFF/ON and the power-restore sequence.\n\nThis makes the component safe to use on non-swinging lights.")]
	private SwingReceiver receiver;

	[SerializeField]
	[Tooltip("The GameObject to toggle active/inactive to create the flicker.\nTypically this is the Light GameObject (or a visual-only child) so turning it off does not disable other scripts.\nIf unassigned, this defaults to this GameObject.\n\nImportant:\n- If you assign the root object that also contains this script, turning it off will stop updates.\n- Prefer assigning a child object that contains your Light/mesh emissive/VFX.\n\nManual OFF behavior note:\n- Do NOT externally SetActive() this object to implement manual off.\n- Use ToggleManualOverride()/SetManualOverride() instead, so motion flicker will respect the manual state.")]
	private GameObject lightObjectToToggle;

	[Header("Events")]
	[SerializeField]
	[Tooltip("Invoked whenever this component actually changes the light object's active state.\nArgument: true = light is ON/active, false = light is OFF/inactive.\n\nNotes:\n- This event fires for toggles caused by:\n  - Master power OFF/ON and restore sequence\n  - Motion-driven flicker bursts (if SwingReceiver exists)\n  - Brownouts and recovery flicker\n  - Manual override ON/OFF (via ToggleManualOverride/SetManualOverride)\n  - Passive flicker (if enabled)\n- This event does NOT fire if SetActive is called with the same value (no actual state change).\n\nCommon uses:\n- Play a click/buzz sound on each flicker\n- Drive emissive materials / VFX\n- Notify gameplay logic of light state changes")]
	private UnityEvent<bool> onLightToggled;

	[Header("Startup / Enable Behavior")]
	[SerializeField]
	[Tooltip("If enabled, this component will start in the same state as if you had called SetManualOverride(true)\nas soon as it becomes enabled.\n\nWhile started manually OFF:\n- The light is forced OFF locally.\n- ALL motion-driven flicker/brownouts are blocked.\n- The light will NOT flicker ON for any local reason.\n\nHow it clears:\n- Per system rules, an OFF→ON master power transition clears manual override.\n\nImportant:\n- If 'Allow Manual Override' is disabled, this option has no effect.\n- If you also enable 'Play Restore Sequence On Enable', manual OFF will win (restore will NOT play).")]
	private bool startManuallyOffWhenEnabled;

	[SerializeField]
	[Tooltip("If enabled, when this component becomes enabled AND master power is ON, the light will \"flicker on\" by\nrunning the same restore sequence used for master power restoration:\nOFF → random stagger delay → ON → quick flicker → stable ON.\n\nNotes:\n- This runs only when master power is currently ON.\n- If master power is OFF, this does nothing (light remains OFF).\n- If manual override is ON (manual OFF), this does nothing (light remains OFF).\n- Works even if there is NO SwingReceiver.\n\nWarning:\n- If 'Start Manually Off When Enabled' is enabled, manual OFF blocks this restore-on-enable behavior.")]
	private bool playRestoreSequenceOnEnable;

	[Header("Manual Override (Blocks Motion Flicker)")]
	[SerializeField]
	[Tooltip("If enabled, calling ToggleManualOverride()/SetManualOverride(true) forces this light OFF locally.\nWhile manually OFF:\n- Motion/swing flicker and brownouts are completely disabled (the light will not flicker ON).\n- Passive flicker is also disabled (the light will not flicker ON).\n- Master power OFF still forces OFF (no change).\n- Master power ON (OFF→ON) clears the manual override (per system rules) and restores the light.\n\nSafe default: Enabled.")]
	private bool allowManualOverride;

	[Header("Passive Flicker (Optional Background Flicker)")]
	[SerializeField]
	[Tooltip("If enabled, this light can also flicker passively (background/ambient flicker), even when NOT swinging.\n\nRules / safety:\n- OFF by default.\n- Respects master power and manual override:\n  - Master power OFF forces OFF and blocks passive flicker.\n  - Manual override OFF forces OFF and blocks passive flicker.\n- Passive flicker will NOT start while another local flicker/restore routine is running.\n- Passive flicker uses the same Quick Flicker style settings as motion flicker, for consistency.\n\nUse this to make a \"slightly unreliable\" light that occasionally flickers even at rest.")]
	private bool enablePassiveFlicker;

	[SerializeField]
	[Tooltip("Passive flicker timing range (seconds) between passive flicker attempts.\n\nHow it works:\n- After waiting a random time in this range, the script will attempt a passive quick-flicker burst.\n- If conditions are not valid (e.g. master power OFF, manual override OFF, or another burst running),\n  it will skip and wait again.\n\nFormat:\n- X = minimum seconds between attempts\n- Y = maximum seconds between attempts\n\nSafe starting range: (4, 20).")]
	private Vector2 passiveAttemptIntervalMinMaxSeconds;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("Chance that a passive attempt actually plays a quick-flicker burst.\n\nExamples:\n- 0.10 = about 1 out of 10 attempts flickers\n- 1.00 = every attempt flickers\n\nSafe starting range: 0.05 to 0.35.")]
	private float passiveAttemptFlickerChance;

	[SerializeField]
	[Min(0f)]
	[Tooltip("Minimum time (seconds) between passive flicker bursts.\n\nThis is separate from the attempt interval and prevents rare cases where attempts line up too close together\nor where conditions cause immediate retries.\n\nSafe starting range: 1 to 10.")]
	private float minTimeBetweenPassiveBursts;

	[Header("Motion Detection (Only Used If SwingReceiver Exists)")]
	[SerializeField]
	[Min(0f)]
	[Tooltip("Minimum receiver MotionMagnitude (deg/sec) required before motion-driven flicker logic can trigger.\nBelow this, the script will not start new motion-driven flicker bursts.\n\nOnly used if a SwingReceiver is present.\n\nSafe starting range: 5 to 40.")]
	private float movingThresholdDegPerSec;

	[SerializeField]
	[Min(0f)]
	[Tooltip("Minimum receiver MotionSpikePerSecond (deg/sec^2 approx) considered a 'jolt'.\nAbove this value, flicker chance ramps up aggressively.\n\nOnly used if a SwingReceiver is present.\n\nSafe starting range: 50 to 400 depending on impulse strength and stiffness.")]
	private float spikeThresholdDegPerSec2;

	[Header("Flicker Scheduling (Only Used If SwingReceiver Exists)")]
	[SerializeField]
	[Min(0f)]
	[Tooltip("Base probability PER SECOND to start a flicker burst while the receiver is moving (above Moving Threshold).\nThis is the 'rare random flicker' even during normal motion.\n\nOnly used if a SwingReceiver is present.\n\nSafe starting range: 0.01 to 0.12.")]
	private float baseBurstChancePerSecond;

	[SerializeField]
	[Min(0f)]
	[Tooltip("Additional probability PER SECOND added when a motion spike exceeds Spike Threshold.\nThis is multiplied by a spike factor, so heavy jolts can trigger flickers frequently.\n\nOnly used if a SwingReceiver is present.\n\nSafe starting range: 0.2 to 2.")]
	private float spikeBurstChancePerSecond;

	[SerializeField]
	[Min(0f)]
	[Tooltip("How strongly spikes above Spike Threshold increase the spike-driven chance.\nSpike factor = (spike / SpikeThreshold) ^ exponent.\n\nOnly used if a SwingReceiver is present.\n\nSafe starting range: 1 to 3.")]
	private float spikeExponent;

	[SerializeField]
	[Min(0f)]
	[Tooltip("Minimum time (seconds) between starting motion-driven flicker bursts.\nPrevents rapid retriggering and keeps the effect from looking like a broken strobe.\n\nOnly used if a SwingReceiver is present.\n\nSafe starting range: 0.2 to 2.")]
	private float minTimeBetweenBursts;

	[Header("Burst Style (Only Used If SwingReceiver Exists)")]
	[SerializeField]
	[Tooltip("Random number of on/off toggles for a standard quick flicker burst.\nEach toggle flips active state with a random interval.\n\nUsed by:\n- Motion-driven quick flicker bursts (if SwingReceiver exists)\n- The master power restore sequence flicker (always, when enabled)\n- Passive flicker bursts (if enabled)\n\nSet both to 0 to disable quick flicker entirely (restore will still do OFF→wait→ON but no toggles).")]
	private Vector2Int flickerToggleCountMinMax;

	[SerializeField]
	[Tooltip("Random interval range (seconds) between toggles during a quick flicker burst.\nSmaller values = faster flicker.\n\nUsed by:\n- Motion-driven quick flicker bursts (if SwingReceiver exists)\n- The master power restore sequence flicker (always, when enabled)\n- Passive flicker bursts (if enabled)\n\nSafe starting range: 0.02 to 0.12.")]
	private Vector2 toggleIntervalMinMaxSeconds;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("Probability that a motion-driven burst becomes a 'brownout' (light stays OFF for a longer random duration)\ninstead of only quick toggles.\n\nOnly used if a SwingReceiver is present.\n\nSafe starting range: 0.05 to 0.35.")]
	private float brownoutChance;

	[SerializeField]
	[Tooltip("If a motion-driven brownout occurs, the light will remain OFF for a random duration (seconds) in this range.\nAfter the brownout, the light returns ON, optionally followed by a short quick flicker.\n\nOnly used if a SwingReceiver is present.\n\nSafe starting range: 0.2 to 4.")]
	private Vector2 brownoutDurationMinMaxSeconds;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("After a motion-driven brownout ends (light returns ON), chance to play a short quick-flicker 'recovery' burst.\n\nOnly used if a SwingReceiver is present.\n\nSafe starting range: 0.2 to 0.9.")]
	private float recoveryFlickerChance;

	[Header("Emergency Light")]
	[SerializeField]
	[Tooltip("If enabled, this light behaves as an emergency light — it reacts to master power in REVERSE:\n\n  Master power ON  → light is OFF (dormant)\n  Master power OFF → light turns ON (active)\n\nWhen master power is cut and this light activates, it uses the same restore sequence as a normal\nlight restoring (OFF → stagger delay → ON → flicker → stable ON), controlled by\n'Play Restore Sequence When Master Powered On' and the restore timing fields below.\n\nAll other behavior is respected normally:\n  - Manual override ON/OFF still works (blocks the emergency light's active state).\n  - Passive flicker still works while the emergency light is active (power OFF).\n  - Motion-driven flicker still works while the emergency light is active.\n  - PowerOffAll() turns this light ON (emergency activates).\n  - PowerOnAll() turns this light OFF (emergency deactivates).\n\nStarting state:\n  - If 'Start Powered On' is true on the controller, emergency lights begin OFF.\n  - If 'Start Powered On' is false on the controller, emergency lights begin ON.\n\nNote: 'Start Manually Off When Enabled' and 'Play Restore Sequence On Enable' still apply\nbased on the effective active state, not the master power state directly.")]
	private bool isEmergencyLight;

	[Header("Master Power Restore Sequence (OFF → wait → ON → flicker → stable)")]
	[SerializeField]
	[Tooltip("If enabled, when master power is restored ON (OFF→ON), the light will play a restore sequence:\nOFF → random stagger delay → ON → quick flicker → stable ON.\n\nDisable this only if you want master power restore to be instant/stable ON.\n\nNote:\nThis works even if there is NO SwingReceiver (non-swinging lights).\n\nManual override note:\nMaster power ON clears manual override (per system rules), so restore can bring the light back.")]
	private bool playRestoreSequenceWhenMasterPoweredOn;

	[SerializeField]
	[Tooltip("Additional random delay (seconds) applied before playing the power-restore sequence.\nThis creates a stagger so many lights don't restore in perfect unison.\n\nFormat:\n- X = minimum delay\n- Y = maximum delay\n\nSet to (0, 0) to disable staggering.\n\nSafe starting range: (0, 0.8).")]
	private Vector2 powerRestoreStartDelayMinMaxSeconds;

	[SerializeField]
	[Tooltip("Duration range (seconds) the light stays OFF at the start of the restore sequence.\nThis represents the \"power comes back\" brownout moment.\n\nFormat:\n- X = minimum OFF duration\n- Y = maximum OFF duration\n\nSet to (0, 0) if you want no OFF time (it will still do ON→flicker→stable when restoring).\n\nSafe starting range: (0.1, 1.5).")]
	private Vector2 powerRestoreOffDurationMinMaxSeconds;

	private bool _isBurstRunning;

	private float _nextAllowedBurstTime;

	private bool _masterPowerOn;

	private bool _lastMasterPowerOn;

	private bool _manualOverrideOff;

	private bool _emergencyPlayerOverride;

	private Coroutine _runningRoutine;

	private float _nextAllowedPassiveBurstTime;

	private Coroutine _passiveRoutine;

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

	public void ToggleManualOverride()
	{
	}

	public void SetManualOverride(bool manualOff)
	{
	}

	private void SetEmergencyManualOn(bool overrideActive)
	{
	}

	public void SetMasterPowerState(bool powerOn, bool playRestoreSequence)
	{
	}

	public void FlickerTurnOn()
	{
	}

	private bool IsManuallyOff()
	{
		return false;
	}

	private bool IsEffectivelyPowered()
	{
		return false;
	}

	private void ApplyDesiredLightState()
	{
	}

	private void StartLocalRoutine(IEnumerator routine)
	{
	}

	private void StopAllLocalRoutines()
	{
	}

	private void EnsurePassiveRoutineState()
	{
	}

	private void StartPassiveRoutineIfNeeded()
	{
	}

	private void StopPassiveRoutine()
	{
	}

	[IteratorStateMachine(typeof(_003CPassiveFlickerLoop_003Ed__52))]
	private IEnumerator PassiveFlickerLoop()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CPowerRestoreRoutine_003Ed__53))]
	private IEnumerator PowerRestoreRoutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CFlickerBurstRoutine_003Ed__54))]
	private IEnumerator FlickerBurstRoutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CQuickFlickerRoutine_003Ed__55))]
	private IEnumerator QuickFlickerRoutine(bool shorterRecovery)
	{
		return null;
	}

	private void SetLightActive(bool active)
	{
	}
}
