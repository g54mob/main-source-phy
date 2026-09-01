using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class EventReceiver : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayedInvoke_003Ed__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EventReceiver _003C_003E4__this;

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
		public _003CDelayedInvoke_003Ed__3(int _003C_003E1__state)
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

	[Tooltip("Seconds to wait before invoking the response event after a signal is received.\n\n• 0  — fires immediately (no delay).\n• >0 — each signal starts its own timer; multiple signals stack and\n       each one fires independently after this duration.\n\nExample: 0.5 fires the event half a second after each received signal.")]
	[Min(0f)]
	public float delay;

	[Tooltip("UnityEvent invoked when this receiver is signalled (after any delay).\nWire up any methods here — works identically to a Button's OnClick event.\n\nTip: the sender can reach this receiver as long as this GameObject's tag\nmatches the sender's Target Tag field.")]
	public UnityEvent onReceive;

	public void Receive()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayedInvoke_003Ed__3))]
	private IEnumerator DelayedInvoke()
	{
		return null;
	}
}
