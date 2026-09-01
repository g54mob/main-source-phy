using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class EventReceiver : MonoBehaviour
{
	private sealed class _003CDelayedInvoke_003Ed__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EventReceiver _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDelayedInvoke_003Ed__3(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0097: Expected I4, but got I8
			//IL_00ff: Expected I4, but got O
			EventReceiver eventReceiver = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(eventReceiver.delay);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_00f1;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null || eventReceiver.onReceive == null)
				{
					goto IL_00f1;
				}
				eventReceiver.onReceive.Invoke();
			}
			return false;
			IL_00f1:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public float delay;

	public UnityEvent onReceive;

	public void Receive()
	{
		//IL_000b: Invalid comparison between I4 and F4
		if (0f < delay)
		{
			_003CDelayedInvoke_003Ed__3 obj = new _003CDelayedInvoke_003Ed__3(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
		}
		else
		{
			onReceive.Invoke();
		}
	}

	private IEnumerator DelayedInvoke()
	{
		_003CDelayedInvoke_003Ed__3 obj = new _003CDelayedInvoke_003Ed__3(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public EventReceiver()
	{
		UnityEvent unityEvent = new UnityEvent();
		onReceive = unityEvent;
		base._002Ector();
	}
}
