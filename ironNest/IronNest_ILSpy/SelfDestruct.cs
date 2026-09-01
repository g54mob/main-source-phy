using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
	private sealed class _003CCountdownAndDestroy_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SelfDestruct _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCountdownAndDestroy_003Ed__11(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_00a9: Expected I4, but got I8
			//IL_01a6: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0052: Expected I4, but got I8
			//IL_015e: Expected F4, but got I
			//IL_0129: Expected F4, but got I
			Component component = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag && (nint)obj != 1)
				{
					goto IL_0192;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					_ = 0;
					GameObject gameObject = _003C_003E4__this.gameObject;
					UnityEngine.Object.Destroy(gameObject);
					return false;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (UnityEngine.Component)+20]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (UnityEngine.Component)+25]");
						if ((nint)0 == 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (UnityEngine.Component)+20]");
							WaitForSeconds waitForSeconds = new WaitForSeconds(0f);
							_003C_003E2__current = waitForSeconds;
							_003C_003E1__state = 2;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (UnityEngine.Component)+20]");
							WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(0f);
							_003C_003E2__current = waitForSecondsRealtime;
							_003C_003E1__state = 1;
						}
						return true;
					}
					_003C_003E4__this.DestroyImmediateNow();
					goto IL_0192;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0192:
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

	public float lifetime = 1f;

	private bool startTimerOnStart = true;

	private bool useUnscaledTime;

	private bool countdownActive;

	private Coroutine countdownRoutine;

	private void Start()
	{
		if (startTimerOnStart)
		{
			StartTimer();
		}
	}

	public void TriggerDestroyImmediate()
	{
		DestroyImmediateNow();
	}

	public void TriggerStartTimer()
	{
		StartTimer();
	}

	public void StartTimer()
	{
		if (countdownRoutine != null)
		{
			StopCoroutine(countdownRoutine);
		}
		_003CCountdownAndDestroy_003Ed__11 obj = new _003CCountdownAndDestroy_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
		countdownRoutine = coroutine;
		countdownActive = true;
	}

	public void CancelTimer()
	{
		if (countdownRoutine != null)
		{
			StopCoroutine(countdownRoutine);
			countdownRoutine = null;
			countdownActive = false;
		}
		else
		{
			countdownActive = false;
		}
	}

	public void DestroyImmediateNow()
	{
		if (countdownRoutine != null)
		{
			StopCoroutine(countdownRoutine);
			countdownRoutine = null;
		}
		countdownActive = false;
		GameObject obj = base.gameObject;
		UnityEngine.Object.Destroy(obj);
	}

	private IEnumerator CountdownAndDestroy()
	{
		_003CCountdownAndDestroy_003Ed__11 obj = new _003CCountdownAndDestroy_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
