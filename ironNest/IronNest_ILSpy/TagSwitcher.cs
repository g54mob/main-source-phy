using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class TagSwitcher : MonoBehaviour
{
	private sealed class _003CDoChangeTagAfterDelay_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public TagSwitcher _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDoChangeTagAfterDelay_003Ed__13(int _003C_003E1__state)
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
			//IL_001f: Invalid comparison between F4 and I4
			//IL_0097: Expected I4, but got I8
			//IL_00ca: Expected I4, but got O
			TagSwitcher tagSwitcher = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (delay > 0f)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_00b6;
				}
				_003C_003E1__state = -1;
			}
			if ((object)_003C_003E4__this != null)
			{
				_003C_003E4__this.ApplyTagSafely();
				tagSwitcher._pending = null;
				goto IL_00b6;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00b6:
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

	private GameObject target;

	private string newTag;

	private float delaySeconds;

	private bool triggerOnAwake;

	private bool fallbackToUntaggedOnInvalidTag;

	private Coroutine _pending;

	private void Awake()
	{
		if (triggerOnAwake)
		{
			StartOrRestartCountdown(delaySeconds);
		}
	}

	public void Trigger()
	{
		StartOrRestartCountdown(delaySeconds);
	}

	public void TriggerWithDelay(float delay)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_001b: Expected F4, but got I4
		bool flag = !(0f < delay);
		float delay2 = 0f;
		if (!flag)
		{
			delay2 = delay;
		}
		StartOrRestartCountdown(delay2);
	}

	public void Cancel()
	{
		if (_pending != null)
		{
			StopCoroutine(_pending);
			_pending = null;
		}
	}

	private void ContextTrigger()
	{
		StartOrRestartCountdown(delaySeconds);
	}

	private void ContextCancel()
	{
		if (_pending != null)
		{
			StopCoroutine(_pending);
			_pending = null;
		}
	}

	private void StartOrRestartCountdown(float delay)
	{
		if (_pending != null)
		{
			StopCoroutine(_pending);
			_pending = null;
		}
		_003CDoChangeTagAfterDelay_003Ed__13 obj = new _003CDoChangeTagAfterDelay_003Ed__13(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.delay = delay;
		Coroutine pending = StartCoroutine(obj);
		_pending = pending;
	}

	private IEnumerator DoChangeTagAfterDelay(float delay)
	{
		_003CDoChangeTagAfterDelay_003Ed__13 obj = new _003CDoChangeTagAfterDelay_003Ed__13(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.delay = delay;
		return obj;
	}

	private void ApplyTagSafely()
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
		if (gameObject != null)
		{
			bool flag = string.IsNullOrWhiteSpace(newTag);
			string text = "Untagged";
			if (!flag)
			{
				text = newTag;
			}
			string text2 = gameObject.tag;
			if (!(text2 != text))
			{
				return;
			}
			bool flag2 = TrySetTag(gameObject, text);
			if (flag2)
			{
				return;
			}
			string text3;
			string text4;
			if (fallbackToUntaggedOnInvalidTag != flag2 && text != "Untagged")
			{
				if (TrySetTag(gameObject, "Untagged"))
				{
					return;
				}
				text3 = "', and fallback to 'Untagged' also failed.";
				text4 = "[TagSwitcher] Failed to set tag to '";
			}
			else
			{
				text3 = "' is not defined. No changes were made. Enable 'fallbackToUntaggedOnInvalidTag' to force 'Untagged' on failure.";
				text4 = "[TagSwitcher] Tag '";
			}
			string message = text4 + text + text3;
			Debug.LogWarning(message, gameObject);
		}
		else
		{
			Debug.LogWarning("[TagSwitcher] Target GameObject is null; cannot change tag.", this);
		}
	}

	private bool TrySetTag(GameObject go, string tagValue)
	{
		//IL_003d: Expected I4, but got O
		if ((object)go != null)
		{
			go.tag = tagValue;
			return true;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public TagSwitcher()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A02B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		newTag = "Untagged";
		fallbackToUntaggedOnInvalidTag = true;
		base._002Ector();
	}
}
