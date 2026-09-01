using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class AnimatorBoolToggler : MonoBehaviour
{
	private sealed class _003CSetBoolCoroutine_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AnimatorBoolToggler _003C_003E4__this;

		public bool value;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CSetBoolCoroutine_003Ed__9(int _003C_003E1__state)
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
			//IL_00bc: Expected I4, but got I8
			//IL_0105: Expected I4, but got O
			//IL_0041: Invalid comparison between F4 and I4
			AnimatorBoolToggler animatorBoolToggler = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (animatorBoolToggler.delay > 0f)
					{
						WaitForSeconds waitForSeconds = new WaitForSeconds(animatorBoolToggler.delay);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 1;
						return true;
					}
					goto IL_00db;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_00f1;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00db;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00db:
			_003C_003E4__this.SetBool(value);
			goto IL_00f1;
			IL_00f1:
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

	private sealed class _003CToggleBoolCoroutine_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AnimatorBoolToggler _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CToggleBoolCoroutine_003Ed__6(int _003C_003E1__state)
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
			//IL_00bc: Expected I4, but got I8
			//IL_00ff: Expected I4, but got O
			//IL_0041: Invalid comparison between F4 and I4
			AnimatorBoolToggler animatorBoolToggler = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (animatorBoolToggler.delay > 0f)
					{
						WaitForSeconds waitForSeconds = new WaitForSeconds(animatorBoolToggler.delay);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 1;
						return true;
					}
					goto IL_00db;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_00eb;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00db;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00db:
			_003C_003E4__this.ToggleBool();
			goto IL_00eb;
			IL_00eb:
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

	public Animator animator;

	public string parameterName;

	public float delay;

	public void ToggleBool()
	{
		if (animator != null && !string.IsNullOrEmpty(parameterName))
		{
			bool flag = animator.GetBool(parameterName);
			bool value = (byte)((flag ? 1u : 0u) ^ 1u) != 0;
			animator.SetBool(parameterName, value);
		}
	}

	public void SetBool(bool value)
	{
		if (animator != null && !string.IsNullOrEmpty(parameterName))
		{
			animator.SetBool(parameterName, value);
		}
	}

	public void ToggleBoolDelayed()
	{
		_003CToggleBoolCoroutine_003Ed__6 obj = new _003CToggleBoolCoroutine_003Ed__6(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator ToggleBoolCoroutine()
	{
		_003CToggleBoolCoroutine_003Ed__6 obj = new _003CToggleBoolCoroutine_003Ed__6(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public void SetBoolDelayed(bool value)
	{
		_003CSetBoolCoroutine_003Ed__9 obj = new _003CSetBoolCoroutine_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.value = value;
		Coroutine coroutine = StartCoroutine(obj);
	}

	public bool GetBool()
	{
		//IL_008d: Expected I4, but got O
		if (animator != null && !string.IsNullOrEmpty(parameterName))
		{
			if ((object)animator != null)
			{
				return animator.GetBool(parameterName);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private IEnumerator SetBoolCoroutine(bool value)
	{
		_003CSetBoolCoroutine_003Ed__9 obj = new _003CSetBoolCoroutine_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.value = value;
		return obj;
	}
}
