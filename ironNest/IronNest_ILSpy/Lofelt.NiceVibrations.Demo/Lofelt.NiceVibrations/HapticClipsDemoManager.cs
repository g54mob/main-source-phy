using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class HapticClipsDemoManager : DemoManager
{
	private sealed class _003CBackToIdle_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HapticClipsDemoManager _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CBackToIdle_003Ed__8(int _003C_003E1__state)
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
			//IL_0104: Expected I4, but got I8
			//IL_01c1: Expected I4, but got O
			//IL_01a8: Expected O, but got I
			HapticClipsDemoManager hapticClipsDemoManager = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					MMUIShaker logo = hapticClipsDemoManager.Logo;
					if ((object)hapticClipsDemoManager.Logo != null)
					{
						logo.Shaking = false;
						if ((object)hapticClipsDemoManager.IconImageAnimator != null)
						{
							hapticClipsDemoManager.IconImageAnimator.SetBool(hapticClipsDemoManager._idleAnimationParameter, value: true);
							_003C_003E2__current = hapticClipsDemoManager._iconChangeDelay;
							_003C_003E1__state = 1;
							return true;
						}
					}
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_01ad;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null && hapticClipsDemoManager.DemoItems != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					object obj = default(object);
					if (obj != null && (object)hapticClipsDemoManager.IconImage != null)
					{
						Image iconImage = hapticClipsDemoManager.IconImage;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ stack_8_v3+20]");
						iconImage.sprite = (Sprite)0;
						goto IL_01ad;
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_01ad:
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

	private sealed class _003CChangeIcon_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HapticClipsDemoManager _003C_003E4__this;

		public Sprite newSprite;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CChangeIcon_003Ed__7(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_003b: Expected I4, but got I8
			//IL_00ee: Expected I4, but got I8
			//IL_015c: Expected I4, but got O
			HapticClipsDemoManager hapticClipsDemoManager = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null && (object)hapticClipsDemoManager.IconImageAnimator != null)
				{
					hapticClipsDemoManager.IconImageAnimator.SetBool(hapticClipsDemoManager._idleAnimationParameter, value: false);
					_003C_003E2__current = hapticClipsDemoManager._iconChangeDelay;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_014e;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null || (object)hapticClipsDemoManager.IconImage == null)
				{
					goto IL_014e;
				}
				hapticClipsDemoManager.IconImage.sprite = newSprite;
			}
			return false;
			IL_014e:
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

	public Image IconImage;

	public Animator IconImageAnimator;

	public List<HapticClipsDemoItem> DemoItems;

	protected WaitForSeconds _iconChangeDelay;

	protected int _idleAnimationParameter;

	protected virtual void Awake()
	{
		WaitForSeconds iconChangeDelay = new WaitForSeconds(0.02f);
		_iconChangeDelay = iconChangeDelay;
		int id = (_idleAnimationParameter = Animator.StringToHash("Idle"));
		IconImageAnimator.SetBool(id, value: true);
	}

	public virtual void PlayHapticClip(int index)
	{
		//IL_004c: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_009d: Expected O, but got I
		MMUIShaker logo = Logo;
		logo.Shaking = true;
		HapticController._fallbackPreset = HapticPatterns.PresetType.LightImpact;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_8_v2+18]");
		HapticController.Play((HapticClip)0);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_8_v2+28]");
		((AudioSource)0).Play();
		StopAllCoroutines();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_8_v2+20]");
		IEnumerator routine = ChangeIcon((Sprite)0);
		Coroutine coroutine = StartCoroutine(routine);
	}

	protected virtual IEnumerator ChangeIcon(Sprite newSprite)
	{
		_003CChangeIcon_003Ed__7 obj = new _003CChangeIcon_003Ed__7(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.newSprite = newSprite;
		return obj;
	}

	protected virtual IEnumerator BackToIdle()
	{
		_003CBackToIdle_003Ed__8 obj = new _003CBackToIdle_003Ed__8(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void OnHapticsStopped()
	{
		IEnumerator routine = BackToIdle();
		Coroutine coroutine = StartCoroutine(routine);
	}

	private void OnDisable()
	{
		//IL_0137: Expected I, but got O
		Action value = OnHapticsStopped;
		Delegate obj = Delegate.Remove(HapticController.PlaybackStopped, value);
		if ((object)obj == null)
		{
			HapticController.PlaybackStopped = null;
			goto IL_00b1;
		}
		bool flag = (object)obj.GetType() != typeof(Action);
		Delegate obj2 = null;
		if (!flag)
		{
			obj2 = obj;
		}
		if ((object)obj2 != null)
		{
			HapticController.PlaybackStopped = (Action)obj2;
			bool flag2 = (object)obj.GetType() != typeof(Action);
			Delegate obj3 = null;
			if (!flag2)
			{
				obj3 = obj;
			}
			bool flag3 = (object)obj3 == null;
			nint num = (nint)typeof(Action);
			if (!flag3)
			{
				goto IL_00b1;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_00b1:
		if (HapticController.IsPlaying())
		{
			HapticController.Stop();
		}
	}

	private void OnEnable()
	{
		//IL_011e: Expected I, but got O
		Action b = OnHapticsStopped;
		Delegate obj = Delegate.Combine(HapticController.PlaybackStopped, b);
		if ((object)obj == null)
		{
			HapticController.PlaybackStopped = null;
			goto IL_00b1;
		}
		bool flag = (object)obj.GetType() != typeof(Action);
		Delegate obj2 = null;
		if (!flag)
		{
			obj2 = obj;
		}
		if ((object)obj2 != null)
		{
			HapticController.PlaybackStopped = (Action)obj2;
			bool flag2 = (object)obj.GetType() != typeof(Action);
			Delegate obj3 = null;
			if (!flag2)
			{
				obj3 = obj;
			}
			bool flag3 = (object)obj3 == null;
			nint num = (nint)typeof(Action);
			if (!flag3)
			{
				goto IL_00b1;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_00b1:
		IEnumerator routine = BackToIdle();
		Coroutine coroutine = StartCoroutine(routine);
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			IEnumerator routine = BackToIdle();
			Coroutine coroutine = StartCoroutine(routine);
		}
	}
}
