using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayRoutine_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DelayedEvent _003C_003E4__this;

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
		public _003CDelayRoutine_003Ed__6(int _003C_003E1__state)
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

	[Tooltip("How many seconds to wait before firing OnDelayComplete.\n\nExamples:\n  0.5  → half a second\n  2.0  → two seconds\n\nSet to 0 to fire on the next frame.")]
	[Min(0f)]
	[SerializeField]
	private float delay;

	[Tooltip("Whether a new TriggerDelay() call cancels and restarts the timer while it is already running.\n\n  true  → calling TriggerDelay() mid-countdown resets the timer.\n  false → additional calls are ignored while the timer is active.")]
	[SerializeField]
	private bool restartIfRunning;

	[Tooltip("Fired when the delay has fully elapsed after TriggerDelay() was called.")]
	[SerializeField]
	private UnityEvent onDelayComplete;

	private Coroutine _activeCoroutine;

	public void TriggerDelay()
	{
	}

	public void CancelDelay()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayRoutine_003Ed__6))]
	private IEnumerator DelayRoutine()
	{
		return null;
	}
}
