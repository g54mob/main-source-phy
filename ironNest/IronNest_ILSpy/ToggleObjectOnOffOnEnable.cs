using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public sealed class ToggleObjectOnOffOnEnable : MonoBehaviour
{
	private sealed class _003CToggleLoop_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ToggleObjectOnOffOnEnable _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CToggleLoop_003Ed__9(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0085: Expected I4, but got I8
			//IL_0310: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0071: Expected I4, but got I8
			//IL_005d: Expected I4, but got I8
			//IL_01d2: Invalid comparison between I4 and F4
			//IL_0132: Invalid comparison between I4 and F4
			//IL_01f4: Invalid comparison between F4 and I4
			//IL_0154: Invalid comparison between F4 and I4
			ToggleObjectOnOffOnEnable toggleObjectOnOffOnEnable = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						return false;
					}
					_003C_003E1__state = -1;
					goto IL_0168;
				}
				_003C_003E1__state = -1;
				goto IL_0208;
			}
			_003C_003E1__state = -1;
			if ((object)_003C_003E4__this != null && (object)toggleObjectOnOffOnEnable._target != null)
			{
				toggleObjectOnOffOnEnable._target.SetActive(toggleObjectOnOffOnEnable._startOn);
				goto IL_00e2;
			}
			goto IL_0302;
			IL_033e:
			GameObject target;
			bool active;
			target.SetActive(active);
			if (toggleObjectOnOffOnEnable._target == null)
			{
				return false;
			}
			goto IL_00e2;
			IL_00e2:
			if ((object)toggleObjectOnOffOnEnable._target != null)
			{
				if (!toggleObjectOnOffOnEnable._target.activeSelf)
				{
					if (0f > toggleObjectOnOffOnEnable._offDuration || !(toggleObjectOnOffOnEnable._offDuration > 0f))
					{
						goto IL_0168;
					}
					WaitForSeconds waitForSeconds = new WaitForSeconds(toggleObjectOnOffOnEnable._offDuration);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 2;
				}
				else
				{
					if (0f > toggleObjectOnOffOnEnable._onDuration || !(toggleObjectOnOffOnEnable._onDuration > 0f))
					{
						goto IL_0208;
					}
					WaitForSeconds waitForSeconds2 = new WaitForSeconds(toggleObjectOnOffOnEnable._onDuration);
					_003C_003E2__current = waitForSeconds2;
					_003C_003E1__state = 1;
				}
				return true;
			}
			goto IL_0302;
			IL_0208:
			if ((object)_003C_003E4__this != null)
			{
				target = toggleObjectOnOffOnEnable._target;
				if ((object)toggleObjectOnOffOnEnable._target != null)
				{
					active = false;
					goto IL_033e;
				}
			}
			goto IL_0302;
			IL_0168:
			if ((object)_003C_003E4__this != null)
			{
				target = toggleObjectOnOffOnEnable._target;
				if ((object)toggleObjectOnOffOnEnable._target != null)
				{
					active = true;
					goto IL_033e;
				}
			}
			goto IL_0302;
			IL_0302:
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

	private GameObject _target;

	private float _onDuration = 0.5f;

	private float _offDuration = 0.5f;

	private bool _startOn = true;

	private bool _restoreOriginalStateOnDisable;

	private bool _originalTargetActive;

	private Coroutine _routine;

	private void OnEnable()
	{
		if (_target != null)
		{
			bool activeSelf = _target.activeSelf;
			bool flag = _routine == null;
			_originalTargetActive = activeSelf;
			if (!flag)
			{
				StopCoroutine(_routine);
			}
			_003CToggleLoop_003Ed__9 obj = new _003CToggleLoop_003Ed__9(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine routine = StartCoroutine(obj);
			_routine = routine;
		}
	}

	private void OnDisable()
	{
		if (_routine != null)
		{
			StopCoroutine(_routine);
			_routine = null;
		}
		if (_target != null && _restoreOriginalStateOnDisable)
		{
			_target.SetActive(_originalTargetActive);
		}
	}

	private IEnumerator ToggleLoop()
	{
		_003CToggleLoop_003Ed__9 obj = new _003CToggleLoop_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
