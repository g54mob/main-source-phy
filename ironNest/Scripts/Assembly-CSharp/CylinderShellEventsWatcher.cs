using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class CylinderShellEventsWatcher : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CPeriodicCheckLoop_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CylinderShellEventsWatcher _003C_003E4__this;

		private WaitForSeconds _003Cwait_003E5__2;

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
		public _003CPeriodicCheckLoop_003Ed__28(int _003C_003E1__state)
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
	private sealed class _003CWaitForRotationThenCheck_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CylinderShellEventsWatcher _003C_003E4__this;

		private int[] _003Cbefore_003E5__2;

		private float _003Cdeadline_003E5__3;

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
		public _003CWaitForRotationThenCheck_003Ed__32(int _003C_003E1__state)
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

	[Header("Selector Reference")]
	[Tooltip("Reference to the CylinderShellSelector. If null, tries GetComponent<CylinderShellSelector>().")]
	public CylinderShellSelector selector;

	[Header("Cross-Scene Tag Lookup")]
	[Tooltip("Enable tag-based lookup if the selector lives elsewhere or persists across scenes.")]
	public bool useTagLookup;

	[Tooltip("Unity tag assigned to the GameObject that has the CylinderShellSelector.")]
	public string selectorTag;

	[Header("Watched Shell Type")]
	[Tooltip("Drag the ShellBlueprint PREFAB (project asset) whose remaining count you want to watch.")]
	public ShellBlueprint watchedShellBlueprintPrefab;

	[Tooltip("Match type by shellVisualPrefab reference if both prefabs define one; otherwise fallback to name match.")]
	public bool matchByVisualPrefab;

	[Header("Events (Rotation-Based)")]
	[Tooltip("Invoked after rotation if the cylinder is now completely empty.")]
	public UnityEvent onRotatedAndEmpty;

	[Tooltip("Invoked after rotation if watched type count changed from >0 to 0.")]
	public UnityEvent onWatchedTypeDepleted;

	[Header("Rotation Completion Heuristic")]
	[Tooltip("Max time to wait for bullets list order to change after rotation is triggered. Used as a fallback when the cylinder is all empty (no order change detectable).")]
	public float rotationTimeoutSeconds;

	[Header("Optional Periodic Checks")]
	[Tooltip("Enable to run checks every 'periodicIntervalSeconds' in addition to rotation-based checks.")]
	public bool enablePeriodicChecks;

	[Tooltip("Seconds between periodic checks (used only if 'enablePeriodicChecks' is true).")]
	[Min(0.02f)]
	public float periodicIntervalSeconds;

	[Tooltip("Invoked when cylinder transitions to empty during periodic checks. This event is delayed by 'periodicEventsDelaySeconds' if a delay is set.")]
	public UnityEvent onPeriodicEmpty;

	[Tooltip("Invoked when watched type transitions from >0 to 0 during periodic checks. This event is delayed by 'periodicEventsDelaySeconds' if a delay is set.")]
	public UnityEvent onPeriodicWatchedTypeDepleted;

	[Header("Periodic Events Delay")]
	[Tooltip("Delay (in seconds) applied before invoking periodic events after their transition is first detected. If the condition is no longer true before the delay elapses, the pending event is canceled. Default: 6 seconds.")]
	[Min(0f)]
	public float periodicEventsDelaySeconds;

	private int _previousWatchedCount_Rotation;

	private Coroutine _rotationWaitRoutine;

	private Coroutine _periodicRoutine;

	private bool _lastEmpty_Periodic;

	private int _lastWatchedCount_Periodic;

	private bool _periodicInitialized;

	private bool _pendingPeriodicEmpty;

	private float _pendingPeriodicEmptyDeadline;

	private bool _pendingPeriodicWatchedDepleted;

	private float _pendingPeriodicWatchedDepletedDeadline;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Start()
	{
	}

	private void TryStartPeriodic()
	{
	}

	[IteratorStateMachine(typeof(_003CPeriodicCheckLoop_003Ed__28))]
	private IEnumerator PeriodicCheckLoop()
	{
		return null;
	}

	private void ResolveSelectorReference()
	{
	}

	private void SubscribeToSelectorMove()
	{
	}

	private void OnSelectorRotateRequested()
	{
	}

	[IteratorStateMachine(typeof(_003CWaitForRotationThenCheck_003Ed__32))]
	private IEnumerator WaitForRotationThenCheck()
	{
		return null;
	}

	private void DoRotationChecks()
	{
	}

	private static int[] SnapshotBullets(CylinderShellSelector sel)
	{
		return null;
	}

	private static bool SequenceEqual(int[] a, int[] b)
	{
		return false;
	}

	private bool IsCylinderEmpty()
	{
		return false;
	}

	private int CountWatchedTypeRemaining()
	{
		return 0;
	}

	private bool IsWatchedType(ShellBlueprint instanceBlueprint)
	{
		return false;
	}

	private static string StripClone(string name)
	{
		return null;
	}
}
