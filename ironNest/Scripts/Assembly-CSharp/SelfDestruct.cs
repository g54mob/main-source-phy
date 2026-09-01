using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public class SelfDestruct : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CCountdownAndDestroy_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SelfDestruct _003C_003E4__this;

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
		public _003CCountdownAndDestroy_003Ed__11(int _003C_003E1__state)
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

	[Header("Timer")]
	[Tooltip("Time in seconds before this object destroys itself when the timer is started.\nSet to 0 for immediate destruction when the timer starts.")]
	[Min(0f)]
	public float lifetime;

	[Tooltip("If enabled, the destruction timer automatically starts in Start().\nDisable this if you only want to start the timer via a UnityEvent or script call.")]
	[SerializeField]
	private bool startTimerOnStart;

	[Tooltip("If enabled, the timer uses unscaled time (ignores Time.timeScale).\nUseful if you pause the game via Time.timeScale = 0 but still want destruction to proceed.")]
	[SerializeField]
	private bool useUnscaledTime;

	[Header("Runtime State (Read-only)")]
	[Tooltip("Indicates whether a destruction countdown is currently active.\nNote: calling 'StartTimer' again will restart the countdown from full lifetime.")]
	[SerializeField]
	private bool countdownActive;

	private Coroutine countdownRoutine;

	private void Start()
	{
	}

	public void TriggerDestroyImmediate()
	{
	}

	public void TriggerStartTimer()
	{
	}

	public void StartTimer()
	{
	}

	public void CancelTimer()
	{
	}

	public void DestroyImmediateNow()
	{
	}

	[IteratorStateMachine(typeof(_003CCountdownAndDestroy_003Ed__11))]
	private IEnumerator CountdownAndDestroy()
	{
		return null;
	}
}
