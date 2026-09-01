using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class NewTeleprinterStartTrigger : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CStartSequence_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NewTeleprinterStartTrigger _003C_003E4__this;

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
		public _003CStartSequence_003Ed__24(int _003C_003E1__state)
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

	[Header("Trigger Conditions")]
	public Teleprinter.Teleprinters PrinterType;

	[Header("Trigger Conditions")]
	[Tooltip("If > 0, a delay (in seconds) will occur AFTER the trigger is entered and BEFORE starting the typewriter.\nThis is separate from TeleprinterQueueTypewriter.initialStartDelay.\nSet to 0 for immediate start upon trigger.")]
	public float delayAfterTrigger;

	[Tooltip("If true, bypasses the TeleprinterQueueTypewriter.initialStartDelay logic and starts immediately (after delayAfterTrigger if any).\nIf false, the typewriter's own initialStartDelay is respected (it must be configured on the TeleprinterQueueTypewriter component).")]
	public bool bypassTypewriterInitialDelay;

	[Tooltip("If true and the trigger is activated when there are currently NO jobs queued, the typewriter will wait and auto-start as soon as jobs are enqueued.\nIf false, triggering while no jobs are present does nothing until you trigger again (if re-trigger allowed) or jobs already exist.")]
	public bool startOnNextJobIfEmpty;

	[Tooltip("If true, the trigger can only activate once. Further enters are ignored.\nIf you need to allow multiple independent runs (e.g., after ForceCompleteAll), uncheck this.")]
	public bool oneShot;

	[Header("Filtering")]
	[Tooltip("Optional list of allowed tags. If empty, ANY tag is accepted.\nExample: add 'Player' to restrict activation to objects tagged Player.")]
	public List<string> allowedTags;

	[Tooltip("Layer mask filter for triggering colliders. If set to 0 (Nothing), layer filtering is ignored and ANY layer is accepted.\nOtherwise, the other collider's layer must be included in this mask.")]
	public LayerMask allowedLayers;

	[Tooltip("If true, shows brief debug logs when the trigger arms or starts the typewriter.")]
	public bool debugLogging;

	[Tooltip("If true, subscribe to the queue manager's OnJobsEnqueued event (via the TeleprinterQueueTypewriter) only after the trigger fires and no jobs are present.\nIf you disable this, startOnNextJobIfEmpty will not function.")]
	public bool subscribeForDeferredStart;

	[Tooltip("Additional actions that can happen when triggered")]
	public UnityEvent OnTriggered;

	private bool _triggered;

	private bool _armedForNextJobs;

	private bool _deferredSubscribed;

	private Teleprinter Printer => null;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
	}

	private bool IsAllowed(Collider other)
	{
		return false;
	}

	private bool IsAllowed2D(Collider2D other)
	{
		return false;
	}

	private bool LayerAllowed(int layer)
	{
		return false;
	}

	private void HandleTrigger(GameObject activator)
	{
	}

	[IteratorStateMachine(typeof(_003CStartSequence_003Ed__24))]
	private IEnumerator StartSequence()
	{
		return null;
	}

	private void TrySubscribeDeferred()
	{
	}

	private void UnsubscribeDeferred()
	{
	}

	private void OnJobsEnqueuedDeferred()
	{
	}

	public void ResetTrigger()
	{
	}

	public void ArmProgrammatically()
	{
	}
}
