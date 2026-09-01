using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace Minimal.Generic;

public sealed class DelayedUnityEvent : MonoBehaviour
{
	private sealed class _003CTimerRoutine_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DelayedUnityEvent _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CTimerRoutine_003Ed__12(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_00e1: Expected I4, but got I8
			//IL_01d4: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_010e: Invalid comparison between I4 and F4
			//IL_006e: Expected I4, but got I8
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			DelayedUnityEvent delayedUnityEvent = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag && (nint)obj2 != 1)
					{
						goto IL_00cc;
					}
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					delayedUnityEvent.running = null;
					if (delayedUnityEvent.onTimerComplete != null)
					{
						delayedUnityEvent.onTimerComplete.Invoke();
					}
					goto IL_00cc;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (0f < delayedUnityEvent.delaySeconds)
					{
						if (!delayedUnityEvent.useUnscaledTime)
						{
							WaitForSeconds waitForSeconds = new WaitForSeconds(delayedUnityEvent.delaySeconds);
							_003C_003E2__current = waitForSeconds;
							_003C_003E1__state = 3;
						}
						else
						{
							WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(delayedUnityEvent.delaySeconds);
							_003C_003E2__current = waitForSecondsRealtime;
							_003C_003E1__state = 2;
						}
						return true;
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00cc:
			return false;
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

	private bool startOnEnable = true;

	private bool startOnStart;

	private float delaySeconds = 1f;

	private bool useUnscaledTime;

	private bool restartIfAlreadyRunning = true;

	private UnityEvent onTimerComplete;

	private Coroutine running;

	private void OnEnable()
	{
		if (startOnEnable)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 12 Invalid \"Jump target not found in method: 0x1804EADF0\"");
		}
	}

	private void Start()
	{
		if (startOnStart)
		{
			StartTimer();
		}
	}

	private void OnDisable()
	{
		if (running != null)
		{
			StopCoroutine(running);
			running = null;
		}
	}

	public void StartTimer()
	{
		if (running != null)
		{
			if (!restartIfAlreadyRunning)
			{
				return;
			}
			StopCoroutine(running);
			running = null;
		}
		_003CTimerRoutine_003Ed__12 obj = new _003CTimerRoutine_003Ed__12(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
		running = coroutine;
	}

	public void StopTimer()
	{
		if (running != null)
		{
			StopCoroutine(running);
			running = null;
		}
	}

	private IEnumerator TimerRoutine()
	{
		_003CTimerRoutine_003Ed__12 obj = new _003CTimerRoutine_003Ed__12(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
