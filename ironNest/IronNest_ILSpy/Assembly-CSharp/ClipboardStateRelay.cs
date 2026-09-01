using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class ClipboardStateRelay : MonoBehaviour
{
	public enum OverrideMode
	{
		RaisedOnly,
		FocusedOnly,
		HiddenOnly,
		BothRaisedFocused,
		AllThree
	}

	public enum OverrideStyle
	{
		HardUninterruptible,
		SoftInterruptible
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<ClipboardStateController> _003C_003E9__24_0;

		public static Action<ClipboardStateController> _003C_003E9__25_0;

		public static Action<ClipboardStateController> _003C_003E9__26_0;

		public static Action<ClipboardStateController> _003C_003E9__27_0;

		public static Action<ClipboardStateController> _003C_003E9__28_0;

		public static Action<ClipboardStateController> _003C_003E9__29_0;

		public static Action<ClipboardStateController> _003C_003E9__30_0;

		public static Action<ClipboardStateController> _003C_003E9__31_0;

		public static Action<ClipboardStateController> _003C_003E9__32_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003CRaise_003Eb__24_0(ClipboardStateController c)
		{
			//IL_000e: Expected O, but got I4
			c._baseState = (ClipboardStateController.ClipboardState)1;
			c.NoteBaseStateChanged();
			c.ApplyEffectiveStateToAnimator();
		}

		internal void _003CLower_003Eb__25_0(ClipboardStateController c)
		{
			//IL_000e: Expected O, but got I4
			c._baseState = (ClipboardStateController.ClipboardState)0;
			c.NoteBaseStateChanged();
			c.ApplyEffectiveStateToAnimator();
		}

		internal void _003CToggleRaised_003Eb__26_0(ClipboardStateController c)
		{
			c.ToggleRaised();
		}

		internal void _003CFocus_003Eb__27_0(ClipboardStateController c)
		{
			_ = 1;
			c.NoteBaseStateChanged();
			c.ApplyEffectiveStateToAnimator();
		}

		internal void _003CUnfocus_003Eb__28_0(ClipboardStateController c)
		{
			_ = 0;
			c.NoteBaseStateChanged();
			c.ApplyEffectiveStateToAnimator();
		}

		internal void _003CToggleFocused_003Eb__29_0(ClipboardStateController c)
		{
			//IL_0045: Expected O, but got I
			List<ClipboardStateController.OverrideEntry> overrides = c._overrides;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
				object obj = -1;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			}
			object obj2 = default(object);
			bool flag = obj2 == null;
			c.NoteBaseStateChanged();
			c.ApplyEffectiveStateToAnimator();
		}

		internal void _003CHide_003Eb__30_0(ClipboardStateController c)
		{
			_ = 1;
			c.NoteBaseStateChanged();
			c.ApplyEffectiveStateToAnimator();
		}

		internal void _003CUnhide_003Eb__31_0(ClipboardStateController c)
		{
			_ = 0;
			c.NoteBaseStateChanged();
			c.ApplyEffectiveStateToAnimator();
		}

		internal void _003CToggleHidden_003Eb__32_0(ClipboardStateController c)
		{
			//IL_0055: Expected O, but got I
			//IL_003a: Expected O, but got I
			List<ClipboardStateController.OverrideEntry> overrides = c._overrides;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
			object obj;
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [c @ rdx (ClipboardStateController)+92]");
				obj = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
				object obj2 = -1;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj3 = default(object);
				obj = obj3;
			}
			bool flag = obj == null;
			c.NoteBaseStateChanged();
			c.ApplyEffectiveStateToAnimator();
		}
	}

	private sealed class _003CAutoEndAfter_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float seconds;

		public ClipboardStateRelay _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAutoEndAfter_003Ed__40(int _003C_003E1__state)
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
			//IL_0075: Expected I4, but got I8
			//IL_00b8: Expected I4, but got O
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSeconds waitForSeconds = new WaitForSeconds(seconds);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				_003C_003E4__this.EndOverride();
			}
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

	private sealed class _003CForwardAfterDelay_003Ed__37 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float seconds;

		public ClipboardStateRelay _003C_003E4__this;

		public Action<ClipboardStateController> action;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CForwardAfterDelay_003Ed__37(int _003C_003E1__state)
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
			//IL_0075: Expected I4, but got I8
			//IL_01a8: Expected I4, but got O
			//IL_0177: Expected O, but got I
			Behaviour behaviour = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSeconds waitForSeconds = new WaitForSeconds(seconds);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				if (_003C_003E4__this.isActiveAndEnabled)
				{
					ClipboardStateController controller = _003C_003E4__this.GetController();
					if (!(controller == null))
					{
						Action<ClipboardStateController> action = this.action;
						if (this.action != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v217 @ rcx_v12 (System.Action`1<ClipboardStateController>)+18] (should have been resolved before IL gen)");
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Behaviour)+4D]");
						if ((nint)0 != 0)
						{
							string name = _003C_003E4__this.name;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Behaviour)+20]");
							string message = name + ": ClipboardStateRelay could not find ClipboardStateController (tag='" + (string)0 + "').";
							Debug.LogWarning(message, _003C_003E4__this);
							return false;
						}
					}
				}
			}
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

	private string controllerTag;

	private ClipboardStateController explicitController;

	private bool autoFindController;

	private float forwardDelaySeconds;

	private bool beginOverrideOnEnable;

	private bool endOverrideOnDisable;

	private OverrideStyle overrideStyle;

	private OverrideMode overrideMode;

	private bool targetRaised;

	private bool targetFocused;

	private bool targetHidden;

	private float overrideDurationSeconds;

	private bool beginOverrideRestartsIfActive;

	private bool logWarnings;

	private UnityEvent onControllerFound;

	private ClipboardStateController _controller;

	private bool _hadController;

	private uint _token;

	private Coroutine _timer;

	private Coroutine _forwardRoutine;

	private void OnEnable()
	{
		TryResolveController();
		if (beginOverrideOnEnable)
		{
			Action<ClipboardStateController> action = delegate
			{
				BeginOverrideInternal(timed: false, 0f);
			};
			Forward(action);
		}
	}

	private void OnDisable()
	{
		if (_forwardRoutine != null)
		{
			StopCoroutine(_forwardRoutine);
		}
		_forwardRoutine = null;
		if (endOverrideOnDisable)
		{
			EndOverride();
		}
	}

	public void Raise()
	{
		Action<ClipboardStateController> action = _003C_003Ec._003C_003E9__24_0;
		if (_003C_003Ec._003C_003E9__24_0 == null)
		{
			action = (_003C_003Ec._003C_003E9__24_0 = delegate(ClipboardStateController c)
			{
				//IL_000e: Expected O, but got I4
				c._baseState = (ClipboardStateController.ClipboardState)1;
				c.NoteBaseStateChanged();
				c.ApplyEffectiveStateToAnimator();
			});
		}
		Forward(action);
	}

	public void Lower()
	{
		Action<ClipboardStateController> action = _003C_003Ec._003C_003E9__25_0;
		if (_003C_003Ec._003C_003E9__25_0 == null)
		{
			action = (_003C_003Ec._003C_003E9__25_0 = delegate(ClipboardStateController c)
			{
				//IL_000e: Expected O, but got I4
				c._baseState = (ClipboardStateController.ClipboardState)0;
				c.NoteBaseStateChanged();
				c.ApplyEffectiveStateToAnimator();
			});
		}
		Forward(action);
	}

	public void ToggleRaised()
	{
		Action<ClipboardStateController> action = _003C_003Ec._003C_003E9__26_0;
		if (_003C_003Ec._003C_003E9__26_0 == null)
		{
			action = (_003C_003Ec._003C_003E9__26_0 = delegate(ClipboardStateController c)
			{
				c.ToggleRaised();
			});
		}
		Forward(action);
	}

	public void Focus()
	{
		if (_003C_003Ec._003C_003E9__27_0 == null)
		{
			Action<ClipboardStateController> action = delegate(ClipboardStateController c)
			{
				_ = 1;
				c.NoteBaseStateChanged();
				c.ApplyEffectiveStateToAnimator();
			};
			_003C_003Ec._003C_003E9__27_0 = action;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 105 Invalid \"Jump target not found in method: 0x18043D990\"");
		throw new NullReferenceException();
	}

	public void Unfocus()
	{
		Action<ClipboardStateController> action = _003C_003Ec._003C_003E9__28_0;
		if (_003C_003Ec._003C_003E9__28_0 == null)
		{
			action = (_003C_003Ec._003C_003E9__28_0 = delegate(ClipboardStateController c)
			{
				_ = 0;
				c.NoteBaseStateChanged();
				c.ApplyEffectiveStateToAnimator();
			});
		}
		Forward(action);
	}

	public void ToggleFocused()
	{
		Action<ClipboardStateController> action = _003C_003Ec._003C_003E9__29_0;
		if (_003C_003Ec._003C_003E9__29_0 == null)
		{
			action = (_003C_003Ec._003C_003E9__29_0 = delegate(ClipboardStateController c)
			{
				//IL_0045: Expected O, but got I
				List<ClipboardStateController.OverrideEntry> overrides = c._overrides;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
					object obj = -1;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				}
				object obj2 = default(object);
				bool flag = obj2 == null;
				c.NoteBaseStateChanged();
				c.ApplyEffectiveStateToAnimator();
			});
		}
		Forward(action);
	}

	public void Hide()
	{
		Action<ClipboardStateController> action = _003C_003Ec._003C_003E9__30_0;
		if (_003C_003Ec._003C_003E9__30_0 == null)
		{
			action = (_003C_003Ec._003C_003E9__30_0 = delegate(ClipboardStateController c)
			{
				_ = 1;
				c.NoteBaseStateChanged();
				c.ApplyEffectiveStateToAnimator();
			});
		}
		Forward(action);
	}

	public void Unhide()
	{
		Action<ClipboardStateController> action = _003C_003Ec._003C_003E9__31_0;
		if (_003C_003Ec._003C_003E9__31_0 == null)
		{
			action = (_003C_003Ec._003C_003E9__31_0 = delegate(ClipboardStateController c)
			{
				_ = 0;
				c.NoteBaseStateChanged();
				c.ApplyEffectiveStateToAnimator();
			});
		}
		Forward(action);
	}

	public void ToggleHidden()
	{
		Action<ClipboardStateController> action = _003C_003Ec._003C_003E9__32_0;
		if (_003C_003Ec._003C_003E9__32_0 == null)
		{
			action = (_003C_003Ec._003C_003E9__32_0 = delegate(ClipboardStateController c)
			{
				//IL_0055: Expected O, but got I
				//IL_003a: Expected O, but got I
				List<ClipboardStateController.OverrideEntry> overrides = c._overrides;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
				object obj;
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [c @ rdx (ClipboardStateController)+92]");
					obj = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v4 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
					object obj2 = -1;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					object obj3 = default(object);
					obj = obj3;
				}
				bool flag = obj == null;
				c.NoteBaseStateChanged();
				c.ApplyEffectiveStateToAnimator();
			});
		}
		Forward(action);
	}

	public void BeginOverride()
	{
		Action<ClipboardStateController> action = delegate
		{
			BeginOverrideInternal(timed: false, 0f);
		};
		Forward(action);
	}

	public void BeginOverrideTimed()
	{
		Action<ClipboardStateController> action = delegate
		{
			BeginOverrideInternal(timed: true, overrideDurationSeconds);
		};
		Forward(action);
	}

	public void EndOverride()
	{
		if (_timer != null)
		{
			StopCoroutine(_timer);
		}
		_timer = null;
		if (_token == 0)
		{
			return;
		}
		ClipboardStateController controller = GetController();
		if (controller != null)
		{
			if (overrideStyle != OverrideStyle.HardUninterruptible)
			{
				controller.EndSoftOverride(_token);
			}
			else if (_token != 0)
			{
				List<ClipboardStateController.OverrideEntry> overrides = controller._overrides;
				bool flag = (nint)controller._overrides < 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rbx_v5 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
				int num = (int)(-1);
				if (!flag)
				{
					object obj = default(object);
					Coroutine coroutine = default(Coroutine);
					do
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if ((nint)obj != (int)_token)
						{
							num--;
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (coroutine != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							controller.StopCoroutine(coroutine);
						}
						controller._overrides.RemoveAt(num);
						controller.ApplyEffectiveStateToAnimator();
						break;
					}
					while ((nint)obj >= (int)_token);
				}
			}
		}
		_token = 0u;
	}

	private void Forward(Action<ClipboardStateController> action)
	{
		//IL_0047: Invalid comparison between I4 and F4
		//IL_00f5: Expected O, but got I
		//IL_0105: Expected O, but got I
		//IL_0115: Expected O, but got I
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		if (_forwardRoutine != null)
		{
			StopCoroutine(_forwardRoutine);
		}
		_forwardRoutine = null;
		if (0f < forwardDelaySeconds)
		{
			_003CForwardAfterDelay_003Ed__37 obj = new _003CForwardAfterDelay_003Ed__37(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			obj.action = action;
			obj.seconds = forwardDelaySeconds;
			Coroutine forwardRoutine = StartCoroutine(obj);
			_forwardRoutine = forwardRoutine;
			return;
		}
		ClipboardStateController controller = GetController();
		if (!(controller == null))
		{
			if (action == null)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [action @ rdx (System.Action`1<ClipboardStateController>)+18]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [action @ rdx (System.Action`1<ClipboardStateController>)+28]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [action @ rdx (System.Action`1<ClipboardStateController>)+40]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v135 @ rax_v15 (should have been resolved before IL gen)");
		}
		if (logWarnings)
		{
			string text = base.name;
			string message = text + ": ClipboardStateRelay could not find ClipboardStateController (tag='" + controllerTag + "').";
			Debug.LogWarning(message, this);
		}
	}

	private IEnumerator ForwardAfterDelay(Action<ClipboardStateController> action, float seconds)
	{
		_003CForwardAfterDelay_003Ed__37 obj = new _003CForwardAfterDelay_003Ed__37(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.action = action;
		obj.seconds = seconds;
		return obj;
	}

	private void StopForwardRoutine()
	{
		if (_forwardRoutine != null)
		{
			StopCoroutine(_forwardRoutine);
		}
		_forwardRoutine = null;
	}

	private unsafe void BeginOverrideInternal(bool timed, float durationSeconds)
	{
		//IL_00bd: Expected O, but got I4
		//IL_015a: Invalid comparison between F4 and I4
		//IL_0256: Expected O, but got I4
		//IL_04bc: Expected O, but got Ref
		//IL_0346: Expected I4, but got O
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		//IL_0312: Expected I4, but got O
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Expected O, but got Unknown
		ClipboardStateController controller = GetController();
		bool flag = controller == null;
		uint token;
		if (!flag)
		{
			if (_token != 0)
			{
				if (beginOverrideRestartsIfActive == flag)
				{
					return;
				}
				EndOverride();
			}
			if (timed)
			{
			}
			float durationSeconds2 = default(float);
			if (overrideStyle == OverrideStyle.HardUninterruptible)
			{
				bool flag2 = overrideMode == OverrideMode.RaisedOnly;
				if (!flag2)
				{
					object obj = overrideMode - 1;
					if (!flag2)
					{
						object obj2 = obj - 1;
						if (!flag2)
						{
							object obj3 = obj2 - 1;
							bool hidden;
							if (!flag2)
							{
								if ((nint)obj3 != 1)
								{
									goto IL_0419;
								}
								hidden = targetHidden;
							}
							else
							{
								bool isHidden = controller.IsHidden;
								hidden = isHidden;
							}
							token = controller.PushOverrideState(targetRaised, targetFocused, hidden, durationSeconds2);
							goto IL_0135;
						}
						ClipboardStateController.ClipboardState effectiveState = controller.GetEffectiveState();
					}
					else
					{
						ClipboardStateController.ClipboardState effectiveState2 = controller.GetEffectiveState();
					}
				}
				else
				{
					ClipboardStateController.ClipboardState effectiveState3 = controller.GetEffectiveState();
				}
				object obj4 = default(object);
				token = controller.PushOverride((ClipboardStateController.ClipboardState)(&obj4), 31536000f);
			}
			else
			{
				if (overrideStyle != OverrideStyle.SoftInterruptible)
				{
					goto IL_0419;
				}
				bool flag3 = overrideMode == OverrideMode.RaisedOnly;
				if (!flag3)
				{
					object obj5 = (int)overrideMode - (int)overrideStyle;
					if (!flag3)
					{
						object obj6 = obj5 - overrideStyle;
						if (!flag3)
						{
							object obj7 = obj6 - overrideStyle;
							bool hidden2;
							if (!flag3)
							{
								if ((nint)obj7 != (nint)overrideStyle)
								{
									goto IL_0419;
								}
								hidden2 = targetHidden;
							}
							else
							{
								bool isHidden2 = controller.IsHidden;
								hidden2 = isHidden2;
							}
							token = controller.BeginSoftOverrideState(targetRaised, targetFocused, hidden2, durationSeconds2);
						}
						else
						{
							ClipboardStateController.ClipboardState baseState = controller._baseState;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rax_v2 (ClipboardStateController)+91]");
							token = controller.BeginSoftOverrideState((byte)(int)baseState != 0, focused: false, targetHidden, durationSeconds2);
						}
					}
					else
					{
						ClipboardStateController.ClipboardState baseState2 = controller._baseState;
						bool focused = targetFocused;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rax_v2 (ClipboardStateController)+92]");
						token = controller.BeginSoftOverrideState((byte)(int)baseState2 != 0, focused, hidden: false, durationSeconds2);
					}
				}
				else
				{
					bool raised = targetRaised;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rax_v2 (ClipboardStateController)+91]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rax_v2 (ClipboardStateController)+92]");
					token = controller.BeginSoftOverrideState(raised, (byte)num != 0, hidden: false, durationSeconds2);
				}
			}
			goto IL_0135;
		}
		if (logWarnings)
		{
			string text = base.name;
			string message = text + ": ClipboardStateRelay could not find ClipboardStateController (tag='" + controllerTag + "').";
			Debug.LogWarning(message, this);
		}
		return;
		IL_0135:
		_token = token;
		goto IL_0419;
		IL_0419:
		bool flag4 = overrideStyle != OverrideStyle.HardUninterruptible;
		bool flag5 = false;
		if (!flag4)
		{
			flag5 = timed;
		}
		if (flag5 && durationSeconds > 0f)
		{
			if (_timer != null)
			{
				StopCoroutine(_timer);
			}
			_timer = null;
			_003CAutoEndAfter_003Ed__40 obj8 = new _003CAutoEndAfter_003Ed__40(0);
			obj8._003C_003E1__state = 0;
			obj8._003C_003E4__this = this;
			obj8.seconds = durationSeconds;
			Coroutine timer = StartCoroutine(obj8);
			_timer = timer;
		}
	}

	private IEnumerator AutoEndAfter(float seconds)
	{
		_003CAutoEndAfter_003Ed__40 obj = new _003CAutoEndAfter_003Ed__40(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.seconds = seconds;
		return obj;
	}

	private void StopTimer()
	{
		if (_timer != null)
		{
			StopCoroutine(_timer);
		}
		_timer = null;
	}

	private ClipboardStateController GetController()
	{
		if (_controller == null)
		{
			bool flag = explicitController != null;
			if (!flag)
			{
				if (autoFindController == flag)
				{
					return null;
				}
				TryResolveController();
				return _controller;
			}
			_controller = explicitController;
			NotifyFoundIfNeeded();
		}
		return _controller;
	}

	private void TryResolveController()
	{
		bool flag = explicitController != null;
		if (!flag)
		{
			if (autoFindController == flag || string.IsNullOrEmpty(controllerTag))
			{
				return;
			}
			GameObject gameObject = GameObject.FindGameObjectWithTag(controllerTag);
			if (!(gameObject != null))
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			ClipboardStateController controller = default(ClipboardStateController);
			_controller = controller;
			if (!(_controller != null))
			{
				return;
			}
		}
		else
		{
			_controller = explicitController;
		}
		NotifyFoundIfNeeded();
	}

	private void NotifyFoundIfNeeded()
	{
		bool flag = _controller == null;
		if (!flag && _hadController == flag)
		{
			_hadController = true;
			if (onControllerFound != null)
			{
				onControllerFound.Invoke();
			}
		}
	}

	public ClipboardStateRelay()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A202]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		controllerTag = "NotepadLogger";
		autoFindController = true;
		endOverrideOnDisable = true;
		overrideStyle = OverrideStyle.SoftInterruptible;
		overrideMode = OverrideMode.FocusedOnly;
		targetRaised = true;
		targetHidden = true;
		overrideDurationSeconds = 2f;
		beginOverrideRestartsIfActive = true;
		base._002Ector();
	}

	private void _003CBeginOverride_003Eb__33_0(ClipboardStateController _)
	{
		BeginOverrideInternal(timed: false, 0f);
	}

	private void _003CBeginOverrideTimed_003Eb__34_0(ClipboardStateController _)
	{
		BeginOverrideInternal(timed: true, overrideDurationSeconds);
	}
}
