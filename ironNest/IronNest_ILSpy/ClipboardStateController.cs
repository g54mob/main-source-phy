using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClipboardStateController : MonoBehaviour
{
	public struct ClipboardState(bool raised, bool focused, bool hidden) : IEquatable<ClipboardState>
	{
		public bool raised = raised;

		public bool focused = focused;

		public bool hidden = hidden;

		public bool Equals(ClipboardState other)
		{
			//IL_0060: Expected O, but got I4
			if (raised == other.raised && focused == other.focused)
			{
				object obj = (hidden ? 1 : 0) - (other.hidden ? 1 : 0);
				return obj == null;
			}
			return false;
		}

		public override string ToString()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A1EA]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			object arg3 = default(object);
			return $"(Raised={arg}, Focused={arg2}, Hidden={arg3})";
		}
	}

	private struct OverrideEntry
	{
		public uint token;

		public ClipboardState state;

		public Coroutine routine;
	}

	private struct SoftOverrideEntry
	{
		public uint token;

		public ClipboardState snapshot;

		public uint beginRevision;

		public bool interrupted;

		public Coroutine routine;
	}

	private sealed class _003CEndSoftOverrideAfter_003Ed__88 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float durationSeconds;

		public ClipboardStateController _003C_003E4__this;

		public uint token;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CEndSoftOverrideAfter_003Ed__88(int _003C_003E1__state)
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
			//IL_00be: Expected I4, but got O
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSeconds waitForSeconds = new WaitForSeconds(durationSeconds);
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
				_003C_003E4__this.EndSoftOverride(token);
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

	private sealed class _003CPopOverrideAfter_003Ed__87 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float durationSeconds;

		public ClipboardStateController _003C_003E4__this;

		public uint token;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CPopOverrideAfter_003Ed__87(int _003C_003E1__state)
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
			//IL_0231: Expected I4, but got O
			//IL_00a4: Expected O, but got I
			//IL_020e: Expected O, but got I
			UnityEngine.Object obj = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSeconds waitForSeconds = new WaitForSeconds(durationSeconds);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rsi_v1 (UnityEngine.Object)+98]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rsi_v1 (UnityEngine.Object)+98]");
					bool flag = (nint)0 < (nint)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rsi_v1 (UnityEngine.Object)+98]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rdi_v4+18]");
						int num = (int)(-_003C_003E1__state);
						if (flag)
						{
							goto IL_015c;
						}
						object obj3 = default(object);
						object arg = default(object);
						while (true)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rsi_v1 (UnityEngine.Object)+98]");
							if ((nint)0 == 0)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if ((nint)obj3 != (int)token)
							{
								num--;
								if ((nint)obj3 >= (int)token)
								{
									continue;
								}
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rsi_v1 (UnityEngine.Object)+56]");
								if ((nint)0 != 0)
								{
									string name = _003C_003E4__this.name;
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									string message = $"{name}: PopOverride token={arg}";
									Debug.Log(message, _003C_003E4__this);
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rsi_v1 (UnityEngine.Object)+98]");
								if ((nint)0 == 0)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rsi_v1 (UnityEngine.Object)+98]");
								((List<OverrideEntry>)0).RemoveAt(num);
								_003C_003E4__this.ApplyEffectiveStateToAnimator();
							}
							goto IL_015c;
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_015c;
			IL_015c:
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

	private Animator animator;

	private string isRaisedParam = "IsRaised";

	private string isFocusedParam = "IsFocused";

	private string isHiddenParam = "IsHidden";

	private InputActionReference raiseToggleAction;

	private InputActionReference focusHoldAction;

	private bool manageActionEnable = true;

	private bool readInitialStateFromAnimator;

	private bool initialRaised;

	private bool initialFocused;

	private bool initialHidden;

	private bool logWarnings = true;

	private bool verboseLogging;

	private DynamicCursorManager cursorManager;

	private UnityEvent onRaised = new UnityEvent();

	private UnityEvent onLowered = new UnityEvent();

	private UnityEvent onFocused = new UnityEvent();

	private UnityEvent onUnfocused = new UnityEvent();

	private UnityEvent onHidden = new UnityEvent();

	private UnityEvent onUnhidden = new UnityEvent();

	private ClipboardState _baseState;

	private readonly List<OverrideEntry> _overrides = new List<OverrideEntry>(8);

	private uint _nextToken = 1u;

	private readonly List<SoftOverrideEntry> _softOverrides = new List<SoftOverrideEntry>(4);

	private uint _stateRevision;

	private int _raisedHash;

	private int _focusedHash;

	private int _hiddenHash;

	private bool _raisedValid;

	private bool _focusedValid;

	private bool _hiddenValid;

	private InputAction _raiseAction;

	private InputAction _focusAction;

	private ClipboardState _lastAppliedState;

	public bool IsRaised
	{
		get
		{
			//IL_0083: Expected I4, but got O
			//IL_0031: Expected I4, but got O
			//IL_0066: Expected O, but got I
			List<OverrideEntry> overrides = _overrides;
			if (_overrides != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
				if ((nint)0 <= (nint)0)
				{
					return (byte)(int)_baseState != 0;
				}
				if (_overrides != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
					object obj = -1;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool result = default(bool);
					return result;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public bool IsFocused
	{
		get
		{
			//IL_0081: Expected I4, but got O
			//IL_0064: Expected O, but got I
			List<OverrideEntry> overrides = _overrides;
			if (_overrides != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
				bool result = default(bool);
				if ((nint)0 <= (nint)0)
				{
					return result;
				}
				if (_overrides != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
					object obj = -1;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					return result;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public bool IsHidden
	{
		get
		{
			//IL_0089: Expected I4, but got O
			//IL_006c: Expected O, but got I
			List<OverrideEntry> overrides = _overrides;
			if (_overrides != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+92]");
					return false;
				}
				if (_overrides != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
					object obj = -1;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool result = default(bool);
					return result;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public UnityEvent OnRaised => onRaised;

	public UnityEvent OnLowered => onLowered;

	public UnityEvent OnFocused => onFocused;

	public UnityEvent OnUnfocused => onUnfocused;

	public UnityEvent OnHidden => onHidden;

	public UnityEvent OnUnhidden => onUnhidden;

	public uint StateRevision => _stateRevision;

	private void Awake()
	{
		//IL_0160: Expected O, but got I4
		//IL_014a: Expected O, but got I4
		if (this.animator == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
			Animator animator = default(Animator);
			this.animator = animator;
		}
		ValidateAnimatorParams();
		if (readInitialStateFromAnimator && this.animator != null && _raisedValid && _focusedValid && _hiddenValid)
		{
			bool flag = this.animator.GetBool(_raisedHash);
			bool flag2 = this.animator.GetBool(_focusedHash);
			bool flag3 = this.animator.GetBool(_hiddenHash);
			_baseState = (ClipboardState)flag;
		}
		else
		{
			_baseState = (ClipboardState)initialRaised;
			_ = initialHidden;
		}
		_lastAppliedState = _baseState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+92]");
		_ = 0;
		ApplyEffectiveStateToAnimator();
	}

	private void OnEnable()
	{
		BindInputActions();
	}

	private void OnDisable()
	{
		UnbindInputActions();
	}

	private void OnValidate()
	{
		if (!string.IsNullOrEmpty(isRaisedParam))
		{
			int raisedHash = Animator.StringToHash(isRaisedParam);
			_raisedHash = raisedHash;
		}
		if (!string.IsNullOrEmpty(isFocusedParam))
		{
			int focusedHash = Animator.StringToHash(isFocusedParam);
			_focusedHash = focusedHash;
		}
		if (!string.IsNullOrEmpty(isHiddenParam))
		{
			int hiddenHash = Animator.StringToHash(isHiddenParam);
			_hiddenHash = hiddenHash;
		}
	}

	private void BindInputActions()
	{
		if (raiseToggleAction != null)
		{
			InputAction action = raiseToggleAction.action;
			if (action != null)
			{
				InputAction action2 = raiseToggleAction.action;
				_raiseAction = action2;
				Action<InputAction.CallbackContext> value = OnRaisePerformed;
				_raiseAction.performed += value;
				if (manageActionEnable && !_raiseAction.enabled)
				{
					_raiseAction.Enable();
				}
				goto IL_00fa;
			}
		}
		_raiseAction = null;
		goto IL_00fa;
		IL_00fa:
		if (focusHoldAction != null)
		{
			InputAction action3 = focusHoldAction.action;
			if (action3 != null)
			{
				InputAction action4 = focusHoldAction.action;
				_focusAction = action4;
				Action<InputAction.CallbackContext> value2 = OnFocusPerformed;
				_focusAction.performed += value2;
				Action<InputAction.CallbackContext> value3 = OnFocusCanceled;
				_focusAction.canceled += value3;
				if (manageActionEnable && !_focusAction.enabled)
				{
					_focusAction.Enable();
				}
				return;
			}
		}
		_focusAction = null;
	}

	private void UnbindInputActions()
	{
		if (_raiseAction != null)
		{
			Action<InputAction.CallbackContext> value = OnRaisePerformed;
			_raiseAction.performed -= value;
			if (manageActionEnable && _raiseAction.enabled)
			{
				_raiseAction.Disable();
			}
		}
		if (_focusAction != null)
		{
			Action<InputAction.CallbackContext> value2 = OnFocusPerformed;
			_focusAction.performed -= value2;
			Action<InputAction.CallbackContext> value3 = OnFocusCanceled;
			_focusAction.canceled -= value3;
			if (manageActionEnable && _focusAction.enabled)
			{
				_focusAction.Disable();
			}
		}
		_raiseAction = null;
		_focusAction = null;
	}

	private void OnRaisePerformed(InputAction.CallbackContext ctx)
	{
		//IL_00d6: Invalid comparison between F4 and I4
		float timeScale = Time.timeScale;
		bool flag = timeScale == 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018043B776h\"");
		if (!flag)
		{
			if (verboseLogging)
			{
				string text = base.name;
				string message = text + ": Input RaiseToggle performed";
				Debug.Log(message, this);
			}
			if (cursorManager.IsCurrentDeviceGamepad() && IsFocused)
			{
				_ = 0;
				NoteBaseStateChanged();
				ApplyEffectiveStateToAnimator();
			}
			else
			{
				ToggleRaised();
			}
		}
	}

	private void OnFocusPerformed(InputAction.CallbackContext ctx)
	{
		//IL_00cd: Invalid comparison between F4 and I4
		float timeScale = Time.timeScale;
		bool flag = timeScale == 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018043B686h\"");
		if (!flag)
		{
			if (verboseLogging)
			{
				string text = base.name;
				string message = text + ": Input FocusHold performed -> Focus(true)";
				Debug.Log(message, this);
			}
			if (cursorManager.IsCurrentDeviceGamepad() && IsFocused)
			{
				_ = 0;
			}
			else
			{
				_ = 1;
			}
			NoteBaseStateChanged();
			ApplyEffectiveStateToAnimator();
		}
	}

	private void OnFocusCanceled(InputAction.CallbackContext ctx)
	{
		//IL_0088: Invalid comparison between F4 and I4
		float timeScale = Time.timeScale;
		bool flag = timeScale == 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018043B5A6h\"");
		if (flag)
		{
			return;
		}
		bool flag2 = cursorManager.IsCurrentDeviceGamepad();
		if (!flag2)
		{
			if (verboseLogging != flag2)
			{
				string text = base.name;
				string message = text + ": Input FocusHold canceled -> Focus(false)";
				Debug.Log(message, this);
			}
			_ = 0;
			NoteBaseStateChanged();
			ApplyEffectiveStateToAnimator();
		}
	}

	public void Raise()
	{
		//IL_000b: Expected O, but got I4
		_baseState = (ClipboardState)1;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void Lower()
	{
		//IL_000b: Expected O, but got I4
		_baseState = (ClipboardState)0;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void ToggleRaised()
	{
		//IL_004f: Expected O, but got I
		//IL_008e: Expected O, but got I4
		List<OverrideEntry> overrides = _overrides;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
		ClipboardState clipboardState;
		if ((nint)0 <= (nint)0)
		{
			clipboardState = _baseState;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
			object obj = -1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			ClipboardState clipboardState2 = default(ClipboardState);
			clipboardState = clipboardState2;
		}
		bool flag = (object)clipboardState == null;
		_baseState = (ClipboardState)flag;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void Focus()
	{
		_ = 1;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void Unfocus()
	{
		_ = 0;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void ToggleFocused()
	{
		//IL_0045: Expected O, but got I
		List<OverrideEntry> overrides = _overrides;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
			object obj = -1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		}
		object obj2 = default(object);
		bool flag = obj2 == null;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void Hide()
	{
		_ = 1;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void Unhide()
	{
		_ = 0;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void ToggleHidden()
	{
		//IL_0055: Expected O, but got I
		//IL_003a: Expected O, but got I
		List<OverrideEntry> overrides = _overrides;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
		object obj;
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+92]");
			obj = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
			object obj2 = -1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj3 = default(object);
			obj = obj3;
		}
		bool flag = obj == null;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void SetRaised(bool raised)
	{
		//IL_000a: Expected O, but got I4
		_baseState = (ClipboardState)raised;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void SetFocused(bool focused)
	{
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public void SetHidden(bool hidden)
	{
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
	}

	public unsafe uint PushOverrideState(bool raised, bool focused, bool hidden, float durationSeconds)
	{
		//IL_0087: Invalid comparison between I4 and F4
		//IL_0013: Expected O, but got Ref
		float num = default(float);
		if (0f < num)
		{
			return PushOverride((ClipboardState)(&num), num);
		}
		if (logWarnings)
		{
			string text = base.name;
			string message = text + ": PushOverrideState called with durationSeconds <= 0. Ignored.";
			Debug.LogWarning(message, this);
		}
		return 0u;
	}

	public unsafe uint PushOverrideRaised(bool raised, float durationSeconds)
	{
		//IL_008e: Invalid comparison between I4 and F4
		//IL_001d: Expected O, but got Ref
		if (0f < durationSeconds)
		{
			ClipboardState effectiveState = GetEffectiveState();
			object obj = default(object);
			return PushOverride((ClipboardState)(&obj), durationSeconds);
		}
		if (logWarnings)
		{
			string text = base.name;
			string message = text + ": PushOverrideRaised called with durationSeconds <= 0. Ignored.";
			Debug.LogWarning(message, this);
		}
		return 0u;
	}

	public unsafe uint PushOverrideFocused(bool focused, float durationSeconds)
	{
		//IL_008e: Invalid comparison between I4 and F4
		//IL_001d: Expected O, but got Ref
		if (0f < durationSeconds)
		{
			ClipboardState effectiveState = GetEffectiveState();
			object obj = default(object);
			return PushOverride((ClipboardState)(&obj), durationSeconds);
		}
		if (logWarnings)
		{
			string text = base.name;
			string message = text + ": PushOverrideFocused called with durationSeconds <= 0. Ignored.";
			Debug.LogWarning(message, this);
		}
		return 0u;
	}

	public unsafe uint PushOverrideHidden(bool hidden, float durationSeconds)
	{
		//IL_008e: Invalid comparison between I4 and F4
		//IL_001d: Expected O, but got Ref
		if (0f < durationSeconds)
		{
			ClipboardState effectiveState = GetEffectiveState();
			object obj = default(object);
			return PushOverride((ClipboardState)(&obj), durationSeconds);
		}
		if (logWarnings)
		{
			string text = base.name;
			string message = text + ": PushOverrideHidden called with durationSeconds <= 0. Ignored.";
			Debug.LogWarning(message, this);
		}
		return 0u;
	}

	public void CancelOverride(uint token)
	{
		if (token == 0)
		{
			return;
		}
		List<OverrideEntry> overrides = _overrides;
		bool flag = (nint)_overrides < 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rbx_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
		int num = (int)(-1);
		if (flag)
		{
			return;
		}
		object obj = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if ((nint)obj == (int)token)
			{
				break;
			}
			num--;
			if ((nint)obj < (int)token)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Coroutine coroutine = default(Coroutine);
		if (coroutine != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			StopCoroutine(coroutine);
		}
		_overrides.RemoveAt(num);
		ApplyEffectiveStateToAnimator();
	}

	public unsafe uint BeginSoftOverrideState(bool raised, bool focused, bool hidden, float durationSeconds)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0832: Expected O, but got I4
		//IL_0072: Expected O, but got I
		//IL_0082: Expected O, but got I
		//IL_00c7: Expected O, but got I4
		//IL_015a: Expected O, but got Ref
		//IL_00ef: Expected F4, but got I
		//IL_0888: Expected I4, but got O
		//IL_0126: Expected O, but got I
		//IL_0136: Expected O, but got I
		//IL_0243: Expected O, but got Ref
		//IL_01f1: Expected I, but got O
		//IL_0206: Expected O, but got I
		//IL_02ee: Expected O, but got Ref
		//IL_027e: Expected I, but got O
		//IL_028e: Expected O, but got I
		//IL_02a3: Expected O, but got I
		//IL_039f: Expected O, but got Ref
		//IL_032f: Expected I, but got O
		//IL_033f: Expected O, but got I
		//IL_0354: Expected O, but got I
		//IL_0448: Expected O, but got Ref
		//IL_03d8: Expected I, but got O
		//IL_03e8: Expected O, but got I
		//IL_03fd: Expected O, but got I
		//IL_04f1: Expected O, but got Ref
		//IL_0481: Expected I, but got O
		//IL_0491: Expected O, but got I
		//IL_04a6: Expected O, but got I
		//IL_059a: Expected O, but got Ref
		//IL_052a: Expected I, but got O
		//IL_053a: Expected O, but got I
		//IL_054f: Expected O, but got I
		//IL_065b: Expected O, but got Ref
		//IL_05eb: Expected I, but got O
		//IL_05fb: Expected O, but got I
		//IL_0610: Expected O, but got I
		//IL_070c: Expected O, but got Ref
		//IL_069c: Expected I, but got O
		//IL_06ac: Expected O, but got I
		//IL_06c1: Expected O, but got I
		//IL_0747: Expected I, but got O
		//IL_0757: Expected O, but got I
		//IL_076c: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		uint nextToken = _nextToken + 1;
		_ = _baseState;
		_nextToken = nextToken;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+92]");
		_ = 0;
		_ = 0;
		_ = _stateRevision;
		_ = 0;
		_baseState = (ClipboardState)raised;
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
		NoteBaseStateChanged();
		ApplyEffectiveStateToAnimator();
		_ = 0;
		_ = _baseState;
		_ = 0;
		_ = _nextToken;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+5F]");
		_ = 0;
		_ = _stateRevision;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-71]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-61]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-71]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-61]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+77]");
		bool flag = (nint)0 <= (nint)0;
		bool flag2 = focused;
		SoftOverrideEntry softOverrideEntry = (SoftOverrideEntry)0;
		if (!flag)
		{
			_003CEndSoftOverrideAfter_003Ed__88 obj5 = new _003CEndSoftOverrideAfter_003Ed__88(0);
			obj5._003C_003E1__state = 0;
			obj5._003C_003E4__this = this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+77]");
			obj5.durationSeconds = 0f;
			obj5.token = _nextToken;
			Coroutine coroutine = StartCoroutine(obj5);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-31]");
			obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-41]");
			obj3 = 0;
			flag2 = false;
			softOverrideEntry = (SoftOverrideEntry)coroutine;
		}
		SoftOverrideEntry item = (SoftOverrideEntry)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		_softOverrides.Add(item);
		if (verboseLogging)
		{
			object[] array = new object[9];
			string text = base.name;
			if (array == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (uint)(int)ex;
			}
			if (text != null)
			{
				nint num = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rdx_v77 (Il2CppClass<System.Object[]>)+40]");
				((List<SoftOverrideEntry>)(object)text).Add((SoftOverrideEntry)0);
				object obj6 = default(object);
				if (obj6 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					List<SoftOverrideEntry> list = default(List<SoftOverrideEntry>);
					throw list;
				}
			}
			array[0] = text;
			object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
			_ = _nextToken;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			List<SoftOverrideEntry> list2 = default(List<SoftOverrideEntry>);
			if (list2 != null)
			{
				nint num2 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v593 @ rdx_v75 (Il2CppClass<System.Object[]>)+40]");
				SoftOverrideEntry softOverrideEntry2 = (SoftOverrideEntry)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v593 @ rdx_v75 (Il2CppClass<System.Object[]>)+40]");
				list2.Add((SoftOverrideEntry)0);
				object obj8 = default(object);
				bool flag3 = obj8 == null;
				flag2 = false;
				List<SoftOverrideEntry> list3 = list2;
				if (flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					List<SoftOverrideEntry> list4 = default(List<SoftOverrideEntry>);
					throw list4;
				}
			}
			array[1] = list2;
			object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 85));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+77]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			List<SoftOverrideEntry> list5 = default(List<SoftOverrideEntry>);
			if (list5 != null)
			{
				nint num3 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v655 @ rdx_v73 (Il2CppClass<System.Object[]>)+40]");
				SoftOverrideEntry softOverrideEntry3 = (SoftOverrideEntry)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v655 @ rdx_v73 (Il2CppClass<System.Object[]>)+40]");
				list5.Add((SoftOverrideEntry)0);
				object obj10 = default(object);
				bool flag4 = obj10 == null;
				flag2 = false;
				List<SoftOverrideEntry> list6 = list5;
				if (flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					List<SoftOverrideEntry> list7 = default(List<SoftOverrideEntry>);
					throw list7;
				}
			}
			array[2] = list5;
			object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			List<SoftOverrideEntry> list8 = default(List<SoftOverrideEntry>);
			if (list8 != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v748 @ rdx_v71 (Il2CppClass<System.Object[]>)+40]");
				SoftOverrideEntry softOverrideEntry4 = (SoftOverrideEntry)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v748 @ rdx_v71 (Il2CppClass<System.Object[]>)+40]");
				list8.Add((SoftOverrideEntry)0);
				object obj12 = default(object);
				bool flag5 = obj12 == null;
				flag2 = false;
				List<SoftOverrideEntry> list9 = list8;
				if (flag5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					List<SoftOverrideEntry> list10 = default(List<SoftOverrideEntry>);
					throw list10;
				}
			}
			array[3] = list8;
			object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			List<SoftOverrideEntry> list11 = default(List<SoftOverrideEntry>);
			if (list11 != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v813 @ rdx_v69 (Il2CppClass<System.Object[]>)+40]");
				SoftOverrideEntry softOverrideEntry5 = (SoftOverrideEntry)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v813 @ rdx_v69 (Il2CppClass<System.Object[]>)+40]");
				list11.Add((SoftOverrideEntry)0);
				object obj14 = default(object);
				bool flag6 = obj14 == null;
				flag2 = false;
				List<SoftOverrideEntry> list12 = list11;
				if (flag6)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					List<SoftOverrideEntry> list13 = default(List<SoftOverrideEntry>);
					throw list13;
				}
			}
			array[4] = list11;
			object obj15 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			List<SoftOverrideEntry> list14 = default(List<SoftOverrideEntry>);
			if (list14 != null)
			{
				nint num6 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v875 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
				SoftOverrideEntry softOverrideEntry6 = (SoftOverrideEntry)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v875 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
				list14.Add((SoftOverrideEntry)0);
				object obj16 = default(object);
				bool flag7 = obj16 == null;
				flag2 = false;
				List<SoftOverrideEntry> list15 = list14;
				if (flag7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					List<SoftOverrideEntry> list16 = default(List<SoftOverrideEntry>);
					throw list16;
				}
			}
			array[5] = list14;
			object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+57]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+5F]");
			_ = 0;
			List<SoftOverrideEntry> list17 = (List<SoftOverrideEntry>)(object)(ClipboardState)obj17;
			if (list17 != null)
			{
				nint num7 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v922 @ rdx_v65 (Il2CppClass<System.Object[]>)+40]");
				SoftOverrideEntry softOverrideEntry7 = (SoftOverrideEntry)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v922 @ rdx_v65 (Il2CppClass<System.Object[]>)+40]");
				list17.Add((SoftOverrideEntry)0);
				object obj18 = default(object);
				bool flag8 = obj18 == null;
				flag2 = false;
				List<SoftOverrideEntry> list18 = list17;
				if (flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					List<SoftOverrideEntry> list19 = default(List<SoftOverrideEntry>);
					throw list19;
				}
			}
			array[6] = list17;
			object obj19 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 77));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-51]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			List<SoftOverrideEntry> list20 = default(List<SoftOverrideEntry>);
			if (list20 != null)
			{
				nint num8 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rdx_v63 (Il2CppClass<System.Object[]>)+40]");
				SoftOverrideEntry softOverrideEntry8 = (SoftOverrideEntry)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rdx_v63 (Il2CppClass<System.Object[]>)+40]");
				list20.Add((SoftOverrideEntry)0);
				object obj20 = default(object);
				bool flag9 = obj20 == null;
				flag2 = false;
				List<SoftOverrideEntry> list21 = list20;
				if (flag9)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					List<SoftOverrideEntry> list22 = default(List<SoftOverrideEntry>);
					throw list22;
				}
			}
			array[7] = list20;
			object obj21 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
			_ = _stateRevision;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			List<SoftOverrideEntry> list23 = default(List<SoftOverrideEntry>);
			if (list23 != null)
			{
				nint num9 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v980 @ rdx_v61 (Il2CppClass<System.Object[]>)+40]");
				SoftOverrideEntry softOverrideEntry9 = (SoftOverrideEntry)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v980 @ rdx_v61 (Il2CppClass<System.Object[]>)+40]");
				list23.Add((SoftOverrideEntry)0);
				object obj22 = default(object);
				bool flag10 = obj22 == null;
				flag2 = false;
				List<SoftOverrideEntry> list24 = list23;
				if (flag10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj23 = default(object);
					throw obj23;
				}
			}
			array[8] = list23;
			string message = string.Format("{0}: BeginSoftOverride token={1} duration={2:0.###}s target=(Raised={3}, Focused={4}, Hidden={5}) snapshot={6} beginRev={7} nowRev={8}", array);
			Debug.Log(message, this);
		}
		return _nextToken;
	}

	public uint BeginSoftOverrideRaised(bool raised, float durationSeconds)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+91]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+92]");
		float durationSeconds2 = default(float);
		return BeginSoftOverrideState(raised, (byte)num != 0, hidden: false, durationSeconds2);
	}

	public uint BeginSoftOverrideFocused(bool focused, float durationSeconds)
	{
		//IL_0020: Expected I4, but got O
		ClipboardState baseState = _baseState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+92]");
		float durationSeconds2 = default(float);
		return BeginSoftOverrideState((byte)(int)baseState != 0, focused, hidden: false, durationSeconds2);
	}

	public uint BeginSoftOverrideHidden(bool hidden, float durationSeconds)
	{
		//IL_0020: Expected I4, but got O
		ClipboardState baseState = _baseState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+91]");
		float durationSeconds2 = default(float);
		return BeginSoftOverrideState((byte)(int)baseState != 0, focused: false, hidden, durationSeconds2);
	}

	public unsafe void EndSoftOverride(uint token)
	{
		//IL_016c: Expected I4, but got O
		//IL_014e: Expected I, but got O
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_0241: Expected I, but got O
		//IL_0254: Expected O, but got I
		//IL_0607: Expected O, but got I4
		//IL_02d2: Expected I, but got O
		//IL_02e2: Expected O, but got I
		//IL_02f5: Expected O, but got I
		//IL_0363: Expected I, but got O
		//IL_0373: Expected O, but got I
		//IL_0386: Expected O, but got I
		//IL_03f4: Expected I, but got O
		//IL_0404: Expected O, but got I
		//IL_0417: Expected O, but got I
		//IL_0485: Expected I, but got O
		//IL_0495: Expected O, but got I
		//IL_04a8: Expected O, but got I
		if (token == 0)
		{
			return;
		}
		List<SoftOverrideEntry> softOverrides = _softOverrides;
		bool flag = (nint)_softOverrides < 0;
		bool flag2 = _softOverrides == null;
		uint num = token;
		if (!flag2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rbx_v2 (System.Collections.Generic.List`1<ClipboardStateController+SoftOverrideEntry>)+18]");
			uint num2 = (uint)(-1);
			num = token;
			if (flag)
			{
				return;
			}
			object obj = default(object);
			Coroutine coroutine = default(Coroutine);
			object obj3 = default(object);
			SoftOverrideEntry softOverrideEntry3 = default(SoftOverrideEntry);
			SoftOverrideEntry softOverrideEntry6 = default(SoftOverrideEntry);
			SoftOverrideEntry softOverrideEntry9 = default(SoftOverrideEntry);
			SoftOverrideEntry softOverrideEntry12 = default(SoftOverrideEntry);
			while (_softOverrides != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				nint num4;
				if ((nint)obj != (int)token)
				{
					uint num3 = num2 - 1;
					bool flag3 = (nint)obj >= (int)token;
					num = num2;
					num4 = (nint)(&obj);
					num2 = num3;
					if (!flag3)
					{
						return;
					}
					continue;
				}
				bool flag4 = _softOverrides == null;
				num = num2;
				num4 = (nint)(&obj);
				if (flag4)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool flag5 = coroutine == null;
				num4 = (nint)(&obj);
				if (!flag5)
				{
					StopCoroutine(coroutine);
					num4 = unchecked((nint)null);
				}
				bool flag6 = _softOverrides == null;
				num = (uint)(int)coroutine;
				if (flag6)
				{
					break;
				}
				_softOverrides.RemoveAt((int)num2);
				object obj2 = obj3 >> 32;
				bool flag7;
				if (obj2 != null)
				{
					flag7 = true;
				}
				else
				{
					object obj4 = obj3 + 3;
					object obj5 = _stateRevision - obj4;
					bool flag8 = obj5 == null;
					flag7 = !flag8;
				}
				if (verboseLogging)
				{
					object[] array = new object[5];
					string text = base.name;
					if (text != null)
					{
						nint num5 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v726 @ rdx_v54 (Il2CppClass<System.Object[]>)+40]");
						SoftOverrideEntry softOverrideEntry = ((List<SoftOverrideEntry>)0).get_Item(0);
						bool flag9 = (object)softOverrideEntry == null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v726 @ rdx_v54 (Il2CppClass<System.Object[]>)+40]");
						num = 0u;
						num4 = 0;
						if (flag9)
						{
							SoftOverrideEntry softOverrideEntry2 = ((List<SoftOverrideEntry>)num).get_Item((int)num4);
							throw softOverrideEntry2;
						}
					}
					array[0] = text;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if ((object)softOverrideEntry3 != null)
					{
						nint num6 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v825 @ rdx_v52 (Il2CppClass<System.Object[]>)+40]");
						List<SoftOverrideEntry> list = (List<SoftOverrideEntry>)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v825 @ rdx_v52 (Il2CppClass<System.Object[]>)+40]");
						SoftOverrideEntry softOverrideEntry4 = ((List<SoftOverrideEntry>)0).get_Item(0);
						bool flag10 = (object)softOverrideEntry4 == null;
						num4 = 0;
						if (flag10)
						{
							SoftOverrideEntry softOverrideEntry5 = list.get_Item((int)num4);
							throw softOverrideEntry5;
						}
					}
					array[1] = softOverrideEntry3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if ((object)softOverrideEntry6 != null)
					{
						nint num7 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v853 @ rdx_v50 (Il2CppClass<System.Object[]>)+40]");
						List<SoftOverrideEntry> list2 = (List<SoftOverrideEntry>)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v853 @ rdx_v50 (Il2CppClass<System.Object[]>)+40]");
						SoftOverrideEntry softOverrideEntry7 = ((List<SoftOverrideEntry>)0).get_Item(0);
						bool flag11 = (object)softOverrideEntry7 == null;
						num4 = 0;
						if (flag11)
						{
							SoftOverrideEntry softOverrideEntry8 = list2.get_Item((int)num4);
							throw softOverrideEntry8;
						}
					}
					array[2] = softOverrideEntry6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if ((object)softOverrideEntry9 != null)
					{
						nint num8 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v881 @ rdx_v48 (Il2CppClass<System.Object[]>)+40]");
						List<SoftOverrideEntry> list3 = (List<SoftOverrideEntry>)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v881 @ rdx_v48 (Il2CppClass<System.Object[]>)+40]");
						SoftOverrideEntry softOverrideEntry10 = ((List<SoftOverrideEntry>)0).get_Item(0);
						bool flag12 = (object)softOverrideEntry10 == null;
						num4 = 0;
						if (flag12)
						{
							SoftOverrideEntry softOverrideEntry11 = list3.get_Item((int)num4);
							throw softOverrideEntry11;
						}
					}
					array[3] = softOverrideEntry9;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if ((object)softOverrideEntry12 != null)
					{
						nint num9 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v910 @ rdx_v46 (Il2CppClass<System.Object[]>)+40]");
						List<SoftOverrideEntry> list4 = (List<SoftOverrideEntry>)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v910 @ rdx_v46 (Il2CppClass<System.Object[]>)+40]");
						SoftOverrideEntry softOverrideEntry13 = ((List<SoftOverrideEntry>)0).get_Item(0);
						bool flag13 = (object)softOverrideEntry13 == null;
						num4 = 0;
						if (flag13)
						{
							SoftOverrideEntry softOverrideEntry14 = list4.get_Item((int)num4);
							throw softOverrideEntry14;
						}
					}
					array[4] = softOverrideEntry12;
					string message = string.Format("{0}: EndSoftOverride token={1} interrupted={2} beginRev={3} nowRev={4}", array);
					Debug.Log(message, this);
				}
				if (!flag7)
				{
					ClipboardState baseState = (ClipboardState)(obj >> 32);
					_baseState = baseState;
					NoteBaseStateChanged();
					ApplyEffectiveStateToAnimator();
					object obj6 = obj >> 40;
					NoteBaseStateChanged();
					ApplyEffectiveStateToAnimator();
					object obj7 = obj >> 48;
					NoteBaseStateChanged();
					ApplyEffectiveStateToAnimator();
				}
				return;
			}
		}
		throw new NullReferenceException();
	}

	public unsafe ClipboardState GetEffectiveState()
	{
		//IL_0039: Expected I4, but got O
		//IL_0034: Expected native int or pointer, but got O
		//IL_0049: Expected native int or pointer, but got O
		//IL_0088: Expected O, but got I
		//IL_009a: Expected native int or pointer, but got O
		//IL_00a7: Expected native int or pointer, but got O
		List<OverrideEntry> overrides = _overrides;
		if (_overrides != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
			ClipboardState clipboardState = default(ClipboardState);
			if ((nint)0 <= (nint)0)
			{
				((ClipboardState*)(nint)clipboardState)->raised = (byte)(int)_baseState != 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (ClipboardStateController)+92]");
				((ClipboardState*)(nint)clipboardState)->hidden = false;
				return clipboardState;
			}
			if (_overrides != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rax_v2 (System.Collections.Generic.List`1<ClipboardStateController+OverrideEntry>)+18]");
				object obj = -1;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool raised = default(bool);
				((ClipboardState*)(nint)clipboardState)->raised = raised;
				bool hidden = default(bool);
				((ClipboardState*)(nint)clipboardState)->hidden = hidden;
				return clipboardState;
			}
		}
		return (ClipboardState)new NullReferenceException();
	}

	private unsafe uint PushOverride(ClipboardState target, float durationSeconds)
	{
		//IL_0043: Expected O, but got Ref
		//IL_032f: Expected I4, but got O
		//IL_00c1: Expected I, but got O
		//IL_00e8: Expected I, but got O
		//IL_013c: Expected I, but got O
		//IL_014c: Expected O, but got I
		//IL_01d1: Expected I, but got O
		//IL_01e1: Expected O, but got I
		//IL_0269: Expected I, but got O
		//IL_0279: Expected O, but got I
		uint nextToken = _nextToken + 1;
		_nextToken = nextToken;
		_003CPopOverrideAfter_003Ed__87 obj = new _003CPopOverrideAfter_003Ed__87(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.durationSeconds = durationSeconds;
		obj.token = _nextToken;
		Coroutine coroutine = StartCoroutine(obj);
		object obj2 = default(object);
		_overrides.Add((OverrideEntry)(&obj2));
		if (verboseLogging)
		{
			object[] array = new object[4];
			string text = base.name;
			if (array == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (uint)(int)ex;
			}
			if (text != null)
			{
				nint num = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				bool flag = obj3 == null;
				uint num2 = 0u;
				nint num3 = unchecked((nint)null);
				if (flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj4 = default(object);
					throw obj4;
				}
			}
			array[0] = text;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj5 = default(object);
			if (obj5 != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v513 @ rdx_v37 (Il2CppClass<System.Object[]>)+40]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj7 = default(object);
				bool flag2 = obj7 == null;
				uint num2 = _nextToken;
				nint num3 = 0;
				object obj8 = obj5;
				if (flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj9 = default(object);
					throw obj9;
				}
			}
			array[1] = obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj10 = default(object);
			if (obj10 != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v558 @ rdx_v35 (Il2CppClass<System.Object[]>)+40]");
				object obj11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj12 = default(object);
				bool flag3 = obj12 == null;
				uint num2 = _nextToken;
				nint num3 = 0;
				object obj13 = obj10;
				if (flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj14 = default(object);
					throw obj14;
				}
			}
			array[2] = obj10;
			object obj16 = default(object);
			object obj15 = (ClipboardState)obj16;
			if (obj15 != null)
			{
				nint num6 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v588 @ rdx_v33 (Il2CppClass<System.Object[]>)+40]");
				object obj17 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj18 = default(object);
				bool flag4 = obj18 == null;
				uint num2 = _nextToken;
				nint num3 = 0;
				object obj19 = obj15;
				if (flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj20 = default(object);
					throw obj20;
				}
			}
			array[3] = obj15;
			string message = string.Format("{0}: PushOverride token={1} duration={2:0.###}s target={3}", array);
			Debug.Log(message, this);
		}
		ApplyEffectiveStateToAnimator();
		return _nextToken;
	}

	private IEnumerator PopOverrideAfter(uint token, float durationSeconds)
	{
		_003CPopOverrideAfter_003Ed__87 obj = new _003CPopOverrideAfter_003Ed__87(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.durationSeconds = durationSeconds;
		obj.token = token;
		return obj;
	}

	private IEnumerator EndSoftOverrideAfter(uint token, float durationSeconds)
	{
		_003CEndSoftOverrideAfter_003Ed__88 obj = new _003CEndSoftOverrideAfter_003Ed__88(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.durationSeconds = durationSeconds;
		obj.token = token;
		return obj;
	}

	private void ApplyEffectiveStateToAnimator()
	{
		//IL_0281: Expected O, but got I4
		bool flag = animator == null;
		if (!flag)
		{
			if (_raisedValid == flag || _focusedValid == flag || _hiddenValid == flag)
			{
				ValidateAnimatorParams();
			}
			if (_raisedValid && _focusedValid && _hiddenValid)
			{
				ClipboardState effectiveState = GetEffectiveState();
				animator.SetBool(_raisedHash, effectiveState.raised);
				bool flag2 = default(bool);
				animator.SetBool(_focusedHash, flag2);
				animator.SetBool(_hiddenHash, effectiveState.hidden);
				if ((effectiveState.raised ? 1 : 0) != (nint)_lastAppliedState)
				{
					UnityEvent unityEvent = (effectiveState.raised ? onRaised : onLowered);
					unityEvent.Invoke();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+D9]");
				if ((nint)(flag2 ? 1 : 0) != 0)
				{
					UnityEvent unityEvent2 = (flag2 ? onFocused : onUnfocused);
					unityEvent2.Invoke();
				}
				bool hidden = effectiveState.hidden;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardStateController)+DA]");
				if ((nint)(hidden ? 1 : 0) != 0)
				{
					UnityEvent unityEvent3 = (effectiveState.hidden ? onHidden : onUnhidden);
					unityEvent3.Invoke();
				}
				_lastAppliedState = (ClipboardState)effectiveState.raised;
				_ = effectiveState.hidden;
			}
		}
		else if (logWarnings)
		{
			string text = base.name;
			string message = text + ": ClipboardStateController has no Animator assigned/found.";
			Debug.LogWarning(message, this);
		}
	}

	private void ValidateAnimatorParams()
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00c5: Expected O, but got I4
		//IL_00cf: Expected O, but got I4
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Expected O, but got Unknown
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Expected O, but got Unknown
		_raisedValid = false;
		_hiddenValid = false;
		if (!(animator != null))
		{
			return;
		}
		if (!string.IsNullOrEmpty(isRaisedParam))
		{
			int raisedHash = Animator.StringToHash(isRaisedParam);
			_raisedHash = raisedHash;
		}
		if (!string.IsNullOrEmpty(isFocusedParam))
		{
			int focusedHash = Animator.StringToHash(isFocusedParam);
			_focusedHash = focusedHash;
		}
		if (!string.IsNullOrEmpty(isHiddenParam))
		{
			int hiddenHash = Animator.StringToHash(isHiddenParam);
			_hiddenHash = hiddenHash;
		}
		AnimatorControllerParameter[] parameters = animator.parameters;
		object obj = parameters + 32;
		object obj2 = 0;
		object obj3 = 0;
		string text = default(string);
		object obj4 = default(object);
		string text2 = default(string);
		object obj5 = default(object);
		string text3 = default(string);
		object obj6 = default(object);
		while ((nint)obj3 < parameters.Length)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746E0");
			if (text == isRaisedParam)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CF520");
				if ((nint)obj4 == 4)
				{
					_raisedValid = true;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746E0");
			if (text2 == isFocusedParam)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CF520");
				if ((nint)obj5 == 4)
				{
					_focusedValid = true;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746E0");
			if (text3 == isHiddenParam)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CF520");
				if ((nint)obj6 == 4)
				{
					_hiddenValid = true;
				}
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		if (logWarnings)
		{
			if (!_raisedValid)
			{
				string text4 = base.name;
				string text5 = animator.name;
				string message = text4 + ": Missing Bool parameter '" + isRaisedParam + "' on Animator '" + text5 + "'.";
				Debug.LogWarning(message, this);
			}
			if (!_focusedValid)
			{
				string text6 = base.name;
				string text7 = animator.name;
				string message2 = text6 + ": Missing Bool parameter '" + isFocusedParam + "' on Animator '" + text7 + "'.";
				Debug.LogWarning(message2, this);
			}
			if (!_hiddenValid)
			{
				string text8 = base.name;
				string text9 = animator.name;
				string message3 = text8 + ": Missing Bool parameter '" + isHiddenParam + "' on Animator '" + text9 + "'.";
				Debug.LogWarning(message3, this);
			}
		}
	}

	private unsafe void NoteBaseStateChanged()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0087: Expected O, but got Ref
		uint stateRevision = _stateRevision + 1;
		_stateRevision = stateRevision;
		List<SoftOverrideEntry> softOverrides = _softOverrides;
		int num = 0;
		int num2 = 0;
		object obj2 = default(object);
		object obj3 = default(object);
		while (true)
		{
			int num3 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rax_v7 (System.Collections.Generic.List`1<ClipboardStateController+SoftOverrideEntry>)+18]");
			if ((nint)num3 < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj = obj2 + 3;
				if ((int)_stateRevision > (nint)obj)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					_softOverrides.set_Item(num, (SoftOverrideEntry)(&obj3));
				}
				softOverrides = _softOverrides;
				num++;
				num2 = num;
				continue;
			}
			break;
		}
	}
}
