using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("Animation/Animator Bool Pulse")]
public class AnimatorBoolPulse : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CRevertAfterDelay_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AnimatorBoolPulse _003C_003E4__this;

		public uint token;

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
		public _003CRevertAfterDelay_003Ed__17(int _003C_003E1__state)
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

	[Header("Animator Target")]
	[Tooltip("Animator that owns the target bool parameter.\nIf left null, the component attempts GetComponent<Animator>() in Awake.\nIf still null at runtime, pulses are ignored safely.")]
	[SerializeField]
	private Animator animator;

	[Tooltip("Exact name of the Animator BOOL parameter to pulse.\nMust exist and be of type Bool in the Animator Controller, otherwise calls are ignored.\nExample: isVisible")]
	[SerializeField]
	private string parameterName;

	[Header("States")]
	[Tooltip("The value considered 'Inactive'. A pulse only runs if the current value equals this.\nDefault (false): A pulse will only fire when the bool is false.\nSet to true if you want the opposite behavior (pulse only when currently true).")]
	[SerializeField]
	private bool inactiveState;

	[Tooltip("The value to set when a pulse activates.\nUsually the logical opposite of Inactive State. Default = true.")]
	[SerializeField]
	private bool activeState;

	[Header("Pulse Timing")]
	[Tooltip("How long (SECONDS) to keep the parameter in Active State before reverting.\nIf <= 0, the parameter is NOT auto-reverted (one-shot set only if it was inactive).\nExample: 2.0")]
	[SerializeField]
	private float pulseDuration;

	[Tooltip("If TRUE, the revert step only happens if the parameter STILL matches Active State AND\nthe pulse is still the most recent (not superseded by a new pulse).\nPrevents reverting a value the player or another system changed in the meantime.\nIf FALSE, the revert always runs after the delay (if duration > 0).")]
	[SerializeField]
	private bool revertOnlyIfUnchanged;

	[Header("Diagnostics")]
	[Tooltip("If TRUE, logs warnings for missing animator/parameter or mismatches.\nDisable in production for silence.")]
	[SerializeField]
	private bool logWarnings;

	[Tooltip("If TRUE, prints verbose debug messages about pulse lifecycle.\nUseful while integrating; disable afterward.")]
	[SerializeField]
	private bool verboseLogging;

	private int _paramHash;

	private bool _paramHashValid;

	private Coroutine _revertCoroutine;

	private uint _pulseToken;

	private void Awake()
	{
	}

	private void OnValidate()
	{
	}

	[ContextMenu("Trigger Pulse (If Inactive)")]
	public void TriggerPulse()
	{
	}

	[ContextMenu("Cancel Pending Revert")]
	public void CancelPendingRevert()
	{
	}

	[ContextMenu("Force Revert Now")]
	public void ForceRevert()
	{
	}

	[IteratorStateMachine(typeof(_003CRevertAfterDelay_003Ed__17))]
	private IEnumerator RevertAfterDelay(uint token)
	{
		return null;
	}

	private bool EnsureReady()
	{
		return false;
	}

	private void ValidateParameter()
	{
	}
}
