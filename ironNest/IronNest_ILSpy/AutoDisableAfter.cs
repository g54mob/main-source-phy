using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class AutoDisableAfter : MonoBehaviour
{
	private sealed class _003CDisableAfterCoroutine_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float startSeconds;

		public AutoDisableAfter _003C_003E4__this;

		private float _003Cremaining_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDisableAfterCoroutine_003Ed__12(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_006e: Expected I4, but got I8
			//IL_0079: Invalid comparison between F4 and I4
			//IL_0173: Expected I4, but got O
			AutoDisableAfter autoDisableAfter = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003Cremaining_003E5__2 = startSeconds;
			}
			else if (_003C_003E1__state != 1)
			{
				return false;
			}
			_003C_003E1__state = -1;
			if (_003Cremaining_003E5__2 > 0f)
			{
				if ((object)_003C_003E4__this != null)
				{
					float num = ((!autoDisableAfter.useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
					float num2 = _003Cremaining_003E5__2 - num;
					_003Cremaining_003E5__2 = num2;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else if ((object)_003C_003E4__this != null)
			{
				_003C_003E4__this.ApplyEndOfTimerAction();
				autoDisableAfter.runningCoroutine = null;
				return false;
			}
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

	private float seconds = 5f;

	private bool useUnscaledTime;

	private GameObject target;

	private bool restartOnEnable = true;

	private bool destroyInsteadOfDisable;

	private Coroutine runningCoroutine;

	private GameObject RuntimeTarget
	{
		get
		{
			if (target != null)
			{
				return target;
			}
			return base.gameObject;
		}
	}

	private void OnEnable()
	{
		//IL_000b: Invalid comparison between I4 and F4
		if (0f < seconds)
		{
			float startSeconds;
			if (!restartOnEnable)
			{
				if (runningCoroutine != null)
				{
					return;
				}
				startSeconds = seconds;
			}
			else
			{
				if (runningCoroutine != null)
				{
					StopCoroutine(runningCoroutine);
					runningCoroutine = null;
				}
				startSeconds = seconds;
			}
			IEnumerator routine = DisableAfterCoroutine(startSeconds);
			Coroutine coroutine = StartCoroutine(routine);
			runningCoroutine = coroutine;
		}
		else
		{
			ApplyEndOfTimerAction();
		}
	}

	private void OnDisable()
	{
		if (runningCoroutine != null)
		{
			StopCoroutine(runningCoroutine);
			runningCoroutine = null;
		}
	}

	private void StartTimerFromEnable()
	{
		//IL_000b: Invalid comparison between I4 and F4
		if (0f < seconds)
		{
			float startSeconds;
			if (!restartOnEnable)
			{
				if (runningCoroutine != null)
				{
					return;
				}
				startSeconds = seconds;
			}
			else
			{
				if (runningCoroutine != null)
				{
					StopCoroutine(runningCoroutine);
					runningCoroutine = null;
				}
				startSeconds = seconds;
			}
			IEnumerator routine = DisableAfterCoroutine(startSeconds);
			Coroutine coroutine = StartCoroutine(routine);
			runningCoroutine = coroutine;
		}
		else
		{
			ApplyEndOfTimerAction();
		}
	}

	private void StopTimer()
	{
		if (runningCoroutine != null)
		{
			StopCoroutine(runningCoroutine);
			runningCoroutine = null;
		}
	}

	private IEnumerator DisableAfterCoroutine(float startSeconds)
	{
		_003CDisableAfterCoroutine_003Ed__12 obj = new _003CDisableAfterCoroutine_003Ed__12(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.startSeconds = startSeconds;
		return obj;
	}

	private void ApplyEndOfTimerAction()
	{
		GameObject gameObject;
		if (target != null)
		{
			gameObject = target;
		}
		else
		{
			GameObject gameObject2 = base.gameObject;
			gameObject = gameObject2;
		}
		bool flag = gameObject == null;
		if (!flag)
		{
			if (destroyInsteadOfDisable == flag)
			{
				gameObject.SetActive(value: false);
			}
			else
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}
}
