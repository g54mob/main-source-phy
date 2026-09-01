using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
	private sealed class _003CDelayRoutine_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DelayedEvent _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDelayRoutine_003Ed__6(int _003C_003E1__state)
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
			//IL_0109: Expected I4, but got O
			DelayedEvent delayedEvent = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(delayedEvent.delay);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_00fb;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_00fb;
				}
				delayedEvent._activeCoroutine = null;
				if (delayedEvent.onDelayComplete != null)
				{
					delayedEvent.onDelayComplete.Invoke();
				}
			}
			return false;
			IL_00fb:
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

	private float delay = 1f;

	private bool restartIfRunning = true;

	private UnityEvent onDelayComplete;

	private Coroutine _activeCoroutine;

	public void TriggerDelay()
	{
		if (_activeCoroutine != null)
		{
			if (!restartIfRunning)
			{
				return;
			}
			StopCoroutine(_activeCoroutine);
		}
		_003CDelayRoutine_003Ed__6 obj = new _003CDelayRoutine_003Ed__6(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine activeCoroutine = StartCoroutine(obj);
		_activeCoroutine = activeCoroutine;
	}

	public void CancelDelay()
	{
		if (_activeCoroutine != null)
		{
			StopCoroutine(_activeCoroutine);
			_activeCoroutine = null;
		}
	}

	private IEnumerator DelayRoutine()
	{
		_003CDelayRoutine_003Ed__6 obj = new _003CDelayRoutine_003Ed__6(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
