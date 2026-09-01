using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class GunElevationWatcher : MonoBehaviour
{
	[Serializable]
	public class GunControllerEvent : UnityEvent<GunController>
	{
	}

	[CompilerGenerated]
	private sealed class _003CDelayedFire_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public int tokenAtSchedule;

		public GunElevationWatcher _003C_003E4__this;

		public GunController gun;

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
		public _003CDelayedFire_003Ed__26(int _003C_003E1__state)
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
	private sealed class _003CEnumerateValidGuns_003Ed__30 : IEnumerable<GunController>, IEnumerable, IEnumerator<GunController>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private GunController _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public GunElevationWatcher _003C_003E4__this;

		private int _003Ci_003E5__2;

		GunController IEnumerator<GunController>.Current
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
		public _003CEnumerateValidGuns_003Ed__30(int _003C_003E1__state)
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

		[DebuggerHidden]
		IEnumerator<GunController> IEnumerable<GunController>.GetEnumerator()
		{
			return null;
		}

		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}

	[Header("Watched Guns")]
	[Tooltip("List of GunController instances to watch.\n\nRules:\n- Null entries are ignored.\n- If the same GunController appears multiple times, you can optionally deduplicate via 'De-Duplicate Gun References'.\n\nSetup tip:\nPopulate via the Inspector for prefab-friendly setup.")]
	[SerializeField]
	private List<GunController> guns;

	[Tooltip("If true, at runtime this component will add ALL GunController components found in the active scene (including inactive objects).\n\nRules:\n- Existing entries in 'Guns' are kept.\n- If 'De-Duplicate Gun References' is enabled, duplicates are removed.\n\nUse this if you don't want to manually wire references in the Inspector.")]
	[SerializeField]
	private bool autoFindGunsInScene;

	[Tooltip("If true, duplicates in the watched list are removed at runtime (reference equality).\n\nRecommended: true when using Auto-Find or when multiple sources may add the same gun.")]
	[SerializeField]
	private bool deduplicateGunReferences;

	[Header("Trigger Condition")]
	[Tooltip("Elevation threshold (degrees).\n\nEvent is scheduled when ANY watched gun crosses from <= this value to > this value.\nExample: Threshold=10 => 9.9 -> 10.1 schedules a trigger; 10.1 -> 10.2 does NOT reschedule.\n\nNote:\nThis uses GunController.CurrentElevation (the gun's current physical elevation), not DesiredElevationAngle.")]
	[SerializeField]
	private float triggerAboveDegrees;

	[Tooltip("If true, the watcher fires only once total (first time ANY gun crosses above the threshold), then stops evaluating.\n\nIf false, it can fire multiple times over the lifetime (e.g., if guns drop back down and cross up again).")]
	[SerializeField]
	private bool triggerOnlyOnce;

	[Tooltip("If true, the watcher starts in an 'armed' state and can schedule a trigger immediately.\n\nIf false, the watcher will snapshot current elevations on enable and will NOT schedule a trigger until a later upward crossing occurs.\n\nRecommended: false if guns might already be above the threshold when the scene loads and you don't want an immediate schedule.")]
	[SerializeField]
	private bool armImmediatelyOnEnable;

	[Header("Delay")]
	[Tooltip("Delay (seconds) applied AFTER an upward crossing is detected and BEFORE the UnityEvents are invoked.\n\nRules:\n- 0 => events fire immediately on crossing.\n- < 0 => treated as 0.\n\nNote:\nThis delay does NOT require the gun to remain above the threshold for the entire delay by default.\nUse 'Cancel If Gun Drops Below During Delay' if you want the condition to remain true during the delay.")]
	[SerializeField]
	private float triggerDelaySeconds;

	[Tooltip("If true, a scheduled trigger will be cancelled if the triggering gun drops back to <= threshold before the delay completes.\n\nBehavior:\n- When cancelled, the watcher can schedule again on a later upward crossing.\n- If false, once scheduled, the trigger will fire after the delay even if the gun dips back down.\n\nRecommended: true if you want a 'sustained above threshold for X seconds' style condition.")]
	[SerializeField]
	private bool cancelIfGunDropsBelowDuringDelay;

	[Tooltip("If true, only one delayed trigger can be pending at a time.\n\nBehavior:\n- If a trigger is already pending, additional crossings are ignored until the pending trigger either fires or is cancelled.\n- This helps prevent a burst of scheduled coroutines when many guns cross at nearly the same time.\n\nRecommended: true for most UI/mission logic.")]
	[SerializeField]
	private bool singlePendingTrigger;

	[Header("Events")]
	[Tooltip("Invoked when ANY watched gun crosses above the threshold (after the optional delay).\n\nNo parameters; use 'On Threshold Crossed (With Gun)' if you need to know which gun triggered.")]
	[SerializeField]
	private UnityEvent onAnyGunElevatedAboveThreshold;

	[Tooltip("Invoked when ANY watched gun crosses above the threshold (after the optional delay).\n\nParameter: the GunController that triggered the crossing.\nTip: Your listener can read gun.gunName, gun.CurrentElevation, etc.")]
	[SerializeField]
	private GunControllerEvent onThresholdCrossedWithGun;

	[Header("Diagnostics")]
	[Tooltip("If true, logs when the threshold is crossed, scheduled, cancelled, and fired.\n\nDisable in production to avoid log spam.")]
	[SerializeField]
	private bool logCrossings;

	private readonly Dictionary<GunController, float> _lastElevation;

	private bool _hasTriggered;

	private Coroutine _pendingCoroutine;

	private GunController _pendingGun;

	private int _pendingToken;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	public void AddGun(GunController gun)
	{
	}

	public void RemoveGun(GunController gun)
	{
	}

	public void ResetTriggerLatch()
	{
	}

	public void CancelPendingTrigger()
	{
	}

	private void TryScheduleTrigger(GunController gun)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayedFire_003Ed__26))]
	private IEnumerator DelayedFire(int tokenAtSchedule, GunController gun, float delay)
	{
		return null;
	}

	private void FireEventsNow(GunController gun)
	{
	}

	private void RefreshGunListIfConfigured()
	{
	}

	private void DeduplicateGunsInPlace()
	{
	}

	[IteratorStateMachine(typeof(_003CEnumerateValidGuns_003Ed__30))]
	private IEnumerable<GunController> EnumerateValidGuns()
	{
		return null;
	}
}
