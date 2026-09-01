using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace TutorialCutaways
{
	[DisallowMultipleComponent]
	public class TutorialCutawayCue : MonoBehaviour
	{
		public enum OverlapMode
		{
			[Tooltip("If another cue is active: deny immediately (onCutawayDenied invoked).")]
			Ignore = 0,
			[Tooltip("If another cue is active: enqueue. When active cue ends, the highest-priority pending cue (among queued) activates next.")]
			Queue = 1,
			[Tooltip("If another cue is active: attempt to preempt. Succeeds only if this cue's priority is strictly greater than the active cue's priority; otherwise denied.")]
			Preempt = 2
		}

		public enum DenialReason
		{
			None = 0,
			UnknownKey = 1,
			KeyUsageExceeded = 2,
			ActiveIgnoreOverlap = 3,
			PreemptPriorityInsufficient = 4
		}

		[CompilerGenerated]
		private sealed class _003CCoro_ActivatedDelay_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public TutorialCutawayCue _003C_003E4__this;

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
			public _003CCoro_ActivatedDelay_003Ed__50(int _003C_003E1__state)
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
		private sealed class _003CCoro_Duration_003Ed__51 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public TutorialCutawayCue _003C_003E4__this;

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
			public _003CCoro_Duration_003Ed__51(int _003C_003E1__state)
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

		[Header("Service Discovery")]
		[Tooltip("Optional explicit reference to the TutorialCutawayService.\n- Assign to skip search.\n- Leave null to allow automatic resolution by tag or singleton.\nSearch Order when requesting activation:\n 1) serviceReference (if set)\n 2) TutorialCutawayService.Instance\n 3) GameObject.FindWithTag(serviceTag)\n 4) FindObjectOfType<TutorialCutawayService>(true)")]
		public TutorialCutawayService serviceReference;

		[Tooltip("Unity Tag used for locating the service if 'serviceReference' isn't provided and no singleton Instance exists.\nMust match the tag on the TutorialCutawayService GameObject.\nExample: 'TutorialCutawayService'")]
		public string serviceTag;

		[Tooltip("Enable detailed debug logs for this cue (resolution attempts, trigger detection, activation lifecycle).\nUseful for diagnosing why a toggle may not cause activation.")]
		public bool debugLogging;

		[Header("Identification & Selection")]
		[Tooltip("Key (channel) this cue belongs to. MUST match a key declared on the TutorialCutawayService.\nRules:\n- Non-empty string.\n- Trimmed.\n- Case-sensitive (\"Default\" != \"default\").\nExamples: 'Default', 'Intro', 'BossReveal'")]
		public string key;

		[Tooltip("Selection & preemption priority.\nHigher number = more important.\nUsed to pick among queued cues and to decide preemption (must be strictly greater than active cue's priority).")]
		public int priority;

		[Tooltip("Overlap behavior if another cue is active:\n- Ignore: Denied immediately.\n- Queue: Stored until current finishes; highest priority wins.\n- Preempt: Interrupts the active if this priority is strictly higher.")]
		public OverlapMode overlapMode;

		[Header("Activation Trigger")]
		[Tooltip("If true at OnEnable, DOES NOT auto-activate (edge required). For automatic start use 'autoRequestOnEnable'.\nToggle from false→true (via Inspector in Play Mode or Animator) to request activation.\nIf 'autoResetTrigger' is true, it resets back to false after being processed so you can trigger again without manual reset.")]
		public bool ActivationTrigger;

		[Tooltip("If true, ActivationTrigger resets to false immediately after a rising-edge activation request is processed.\nBehaves like a one-frame trigger when driven by animation or manual inspector toggling.")]
		public bool autoResetTrigger;

		[Tooltip("Automatically request activation when this component becomes enabled.\nUseful for scripted/prefab one-shot cues (e.g., intro sequence). Independent of ActivationTrigger.")]
		public bool autoRequestOnEnable;

		[Header("Duration")]
		[Tooltip("If true, the duration timer does NOT start automatically on activation.\nYou must call 'StartDurationCountdown()' (e.g., via UnityEvent) to begin the timer.\nUseful for cues that wait for a specific user action before timing out.")]
		public bool manualDurationTrigger;

		[Tooltip("Fixed active duration in seconds (unscaled time). After this time the cue auto-completes (onCutawayCompleted).\n0 = Activate and complete on the following frame (still fires both events).\nMust be >= 0.")]
		public float durationSeconds;

		[Header("Events")]
		[Tooltip("Invoked once when activation is granted.")]
		public UnityEvent onCutawayActivated;

		[Tooltip("Invoked after normal completion (duration elapsed).")]
		public UnityEvent onCutawayCompleted;

		[Tooltip("Invoked when activation fails.\nPossible reasons (see lastDenialReason):\n- UnknownKey\n- KeyUsageExceeded\n- ActiveIgnoreOverlap\n- PreemptPriorityInsufficient")]
		public UnityEvent onCutawayDenied;

		[Tooltip("Invoked if this cue is interrupted (e.g., preempted by a higher priority or disabled mid-activation).")]
		public UnityEvent onCutawayInterrupted;

		[Header("Delayed Activation")]
		[Tooltip("Delay (unscaled seconds) after activation before invoking 'onCutawayActivatedDelayed'.\n0 = invoke immediately on activation.\nOnly fires if the cue is still active when the delay elapses.")]
		public float activatedDelaySeconds;

		[Tooltip("Invoked once after activation plus 'activatedDelaySeconds'.\nWill NOT fire if the cue has completed or been interrupted before the delay elapses.")]
		public UnityEvent onCutawayActivatedDelayed;

		private Coroutine _lifecycleCoro;

		private Coroutine _delayedActivatedCoro;

		private bool _lastActivationTrigger;

		private bool _idInitialized;

		private bool _durationCountdownTriggered;

		[SerializeField]
		[HideInInspector]
		private string _persistentId;

		private DenialReason _lastDenialReason;

		private string _lastDenialExtra;

		public string PersistentId => null;

		public bool IsActive { get; private set; }

		public DenialReason lastDenialReason => default(DenialReason);

		public string lastDenialExtra => null;

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

		private void TryRegisterWithService()
		{
		}

		private TutorialCutawayService ResolveService()
		{
			return null;
		}

		public bool RequestActivate()
		{
			return false;
		}

		public void StartDurationCountdown()
		{
		}

		public void CompleteEarly()
		{
		}

		public void Cancel()
		{
		}

		internal void Internal_Begin()
		{
		}

		internal void Internal_Denied(DenialReason reason, string reasonExtra)
		{
		}

		internal void Internal_End(bool interrupted)
		{
		}

		[IteratorStateMachine(typeof(_003CCoro_ActivatedDelay_003Ed__50))]
		private IEnumerator Coro_ActivatedDelay()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CCoro_Duration_003Ed__51))]
		private IEnumerator Coro_Duration()
		{
			return null;
		}

		[ContextMenu("Request Activate (Test)")]
		private void Context_RequestActivate()
		{
		}

		[ContextMenu("Start Duration Countdown (Test)")]
		private void Context_StartDurationCountdown()
		{
		}

		[ContextMenu("Complete Early (Test)")]
		private void Context_CompleteEarly()
		{
		}

		[ContextMenu("Cancel (Test)")]
		private void Context_Cancel()
		{
		}

		[ContextMenu("Log Service Resolution (Test)")]
		private void Context_LogServiceResolution()
		{
		}
	}
}
