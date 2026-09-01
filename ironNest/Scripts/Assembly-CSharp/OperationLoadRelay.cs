using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[AddComponentMenu("Missions/Operation Load Relay")]
public class OperationLoadRelay : MonoBehaviour
{
	public enum StartupAction
	{
		None = 0,
		StartAssignedOperation = 1,
		ReturnToMainMenu = 2
	}

	[CompilerGenerated]
	private sealed class _003CCoReturnToMainMenu_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public OperationLoadRelay _003C_003E4__this;

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
		public _003CCoReturnToMainMenu_003Ed__19(int _003C_003E1__state)
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
	private sealed class _003CCoStartOperation_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public OperationLoadRelay _003C_003E4__this;

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
		public _003CCoStartOperation_003Ed__18(int _003C_003E1__state)
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

	[Header("Assigned Operation")]
	[Tooltip("Operation asset to start when invoked. Its missions will replace the MissionManager's active list.\nScenes referenced in the asset must be in Build Settings.")]
	public OperationGraph operation;

	[Header("Invocation")]
	[Tooltip("If true, automatically triggers the selected action On Start after an optional delay.\nUseful for auto-boot flows or testing. Leave OFF for manual (UnityEvent/UI/Animator) control.")]
	public bool invokeOnStart;

	[Tooltip("Delay (in seconds) before executing an action when auto-invoked or when using the delayed methods.\nSet to 0 for immediate execution.")]
	[Min(0f)]
	public float delaySeconds;

	[Tooltip("If true, prevents multiple concurrent or rapid re-invocations while an action is running (e.g., during delay).\nRecommended when wired to buttons to avoid double-clicks causing multiple loads.")]
	public bool preventReentry;

	[Tooltip("If true, disables this component after the first successful invocation to avoid repeated triggers.\nGood for one-shot selection buttons.")]
	public bool disableAfterSuccess;

	[Header("Default Action (used only when Invoke On Start is enabled)")]
	[Tooltip("Which action to automatically perform On Start (only used when Invoke On Start is enabled).\n- None: do nothing.\n- StartAssignedOperation: uses the 'operation' field.\n- ReturnToMainMenu: calls MissionManager.Instance.LoadMainMenu().")]
	public StartupAction startupAction;

	[Header("Start Options")]
	[Tooltip("Mission index to start at within the assigned operation. 0 = first mission.\nClamped to valid range at runtime.")]
	[Min(0f)]
	public int startMissionIndex;

	[Header("Events")]
	[Tooltip("Invoked right before executing an action (after any delay completes but before calling MissionManager).")]
	public UnityEvent onBeforeAction;

	[Tooltip("Invoked immediately after a successful call to MissionManager.")]
	public UnityEvent onAfterAction;

	[Tooltip("Invoked if the action cannot be performed (e.g., MissionManager.Instance or Operation is missing).")]
	public UnityEvent onActionFailed;

	[Header("Debug")]
	[Tooltip("If true, prints detailed logs to the Console for troubleshooting.")]
	public bool verbose;

	private bool _busy;

	private void Start()
	{
	}

	[ContextMenu("Start Assigned Operation (Immediate)")]
	[Tooltip("Starts the assigned operation immediately via MissionManager.Instance.StartOperation(operation, startIndex).")]
	public void StartAssignedOperation()
	{
	}

	[Tooltip("Starts the assigned operation after a specified delay (seconds).")]
	public void StartAssignedOperationWithDelay(float delay)
	{
	}

	[ContextMenu("Return To Main Menu (Immediate)")]
	[Tooltip("Loads the Main Menu scene additively (and unloads any active mission) via MissionManager.Instance.LoadMainMenu().")]
	public void ReturnToMainMenu()
	{
	}

	[Tooltip("Loads the Main Menu scene additively (and unloads any active mission) after a specified delay (seconds).")]
	public void ReturnToMainMenuWithDelay(float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CCoStartOperation_003Ed__18))]
	private IEnumerator CoStartOperation(float delay)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CCoReturnToMainMenu_003Ed__19))]
	private IEnumerator CoReturnToMainMenu(float delay)
	{
		return null;
	}

	private bool TryBegin()
	{
		return false;
	}

	private void Succeed()
	{
	}

	private void End()
	{
	}
}
