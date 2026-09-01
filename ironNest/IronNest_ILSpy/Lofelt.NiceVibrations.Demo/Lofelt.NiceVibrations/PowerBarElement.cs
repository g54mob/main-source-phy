using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class PowerBarElement : MonoBehaviour
{
	private sealed class _003CColorBump_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PowerBarElement _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CColorBump_003Ed__11(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_003b: Expected I4, but got I8
			//IL_009f: Expected I4, but got I8
			//IL_021a: Expected I4, but got O
			//IL_0069: Expected F4, but got I4
			//IL_0207: Expected O, but got Ref
			//IL_0141: Invalid comparison between I4 and F4
			//IL_018c: Expected O, but got Ref
			PowerBarElement powerBarElement = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					powerBarElement._bumpDuration = _003C_003E1__state;
					goto IL_00be;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_021a;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00be;
				}
			}
			goto IL_020c;
			IL_00be:
			object obj = default(object);
			if (powerBarElement.BumpDuration > powerBarElement._bumpDuration)
			{
				if (powerBarElement.Curve != null)
				{
					float time = powerBarElement._bumpDuration / powerBarElement.BumpDuration;
					float num = powerBarElement.Curve.Evaluate(time);
					if (0f > num || num > 1f)
					{
					}
					if ((object)powerBarElement._image != null)
					{
						powerBarElement._image.color = (Color)(&obj);
						float deltaTime = Time.deltaTime;
						float bumpDuration = powerBarElement._bumpDuration + deltaTime;
						powerBarElement._bumpDuration = bumpDuration;
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
			}
			else if ((object)powerBarElement._image != null)
			{
				powerBarElement._image.color = (Color)(&obj);
				goto IL_021a;
			}
			goto IL_020c;
			IL_020c:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_021a:
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

	public float BumpDuration = 0.15f;

	public Color NormalColor;

	public Color InactiveColor;

	public AnimationCurve Curve;

	protected Image _image;

	protected float _bumpDuration;

	protected bool _active;

	protected bool _activeLastFrame;

	protected virtual void Awake()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		Image image = default(Image);
		_image = image;
	}

	public unsafe virtual void SetActive(bool status)
	{
		//IL_0037: Expected O, but got Ref
		_active = status;
		if (status)
		{
		}
		object obj = default(object);
		_image.color = (Color)(&obj);
	}

	protected virtual void Update()
	{
		if (_active && !_activeLastFrame)
		{
			IEnumerator routine = ColorBump();
			Coroutine coroutine = StartCoroutine(routine);
			_activeLastFrame = _active;
		}
		else
		{
			_activeLastFrame = _active;
		}
	}

	protected virtual IEnumerator ColorBump()
	{
		_003CColorBump_003Ed__11 obj = new _003CColorBump_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
