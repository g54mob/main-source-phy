using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace YourGame.Input;

public sealed class InputActionUnityEventRelay : MonoBehaviour
{
	private InputActionReference action;

	private bool autoEnableAction = true;

	private bool requireNonDefaultValue;

	private bool fireOnce;

	private UnityEvent onStarted;

	private UnityEvent onPerformed;

	private UnityEvent onCanceled;

	private Action<InputAction.CallbackContext> _startedHandler;

	private Action<InputAction.CallbackContext> _performedHandler;

	private Action<InputAction.CallbackContext> _canceledHandler;

	private bool _startedFired;

	private bool _performedFired;

	private bool _canceledFired;

	private InputAction BoundAction
	{
		get
		{
			if (action != null)
			{
				if ((object)action != null)
				{
					return action.action;
				}
				return (InputAction)(object)new NullReferenceException();
			}
			return null;
		}
	}

	private unsafe void Awake()
	{
		Action<InputAction.CallbackContext> startedHandler = delegate
		{
			//IL_004b: Expected O, but got Ref
			object obj = default(object);
			if ((!fireOnce || !_startedFired) && ShouldFire((InputAction.CallbackContext)(&obj)))
			{
				if (onStarted != null)
				{
					onStarted.Invoke();
				}
				if (fireOnce)
				{
					_startedFired = true;
					UnsubscribeIfDone();
				}
			}
		};
		_startedHandler = startedHandler;
		Action<InputAction.CallbackContext> performedHandler = delegate
		{
			//IL_004b: Expected O, but got Ref
			object obj = default(object);
			if ((!fireOnce || !_performedFired) && ShouldFire((InputAction.CallbackContext)(&obj)))
			{
				if (onPerformed != null)
				{
					onPerformed.Invoke();
				}
				if (fireOnce)
				{
					_performedFired = true;
					UnsubscribeIfDone();
				}
			}
		};
		_performedHandler = performedHandler;
		Action<InputAction.CallbackContext> canceledHandler = delegate
		{
			//IL_004b: Expected O, but got Ref
			object obj = default(object);
			if ((!fireOnce || !_canceledFired) && ShouldFire((InputAction.CallbackContext)(&obj)))
			{
				if (onCanceled != null)
				{
					onCanceled.Invoke();
				}
				if (fireOnce)
				{
					_canceledFired = true;
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 74 Invalid \"Jump target not found in method: 0x1804ED840\"");
				}
			}
		};
		_canceledHandler = canceledHandler;
	}

	private void OnEnable()
	{
		InputAction boundAction = BoundAction;
		if (boundAction != null)
		{
			boundAction.started += _startedHandler;
			boundAction.performed += _performedHandler;
			boundAction.canceled += _canceledHandler;
			if (autoEnableAction && !boundAction.enabled)
			{
				boundAction.Enable();
			}
		}
	}

	private void OnDisable()
	{
		InputAction boundAction = BoundAction;
		if (boundAction != null)
		{
			if (autoEnableAction && boundAction.enabled)
			{
				boundAction.Disable();
			}
			boundAction.started -= _startedHandler;
			boundAction.performed -= _performedHandler;
			boundAction.canceled -= _canceledHandler;
		}
	}

	private void UnsubscribeIfDone()
	{
		//IL_01f9: Expected O, but got I4
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		bool flag;
		if (_startedFired)
		{
			flag = true;
		}
		else if (onStarted != null)
		{
			int persistentEventCount = onStarted.GetPersistentEventCount();
			bool flag2 = persistentEventCount == 0;
			flag = flag2;
		}
		else
		{
			flag = false;
		}
		bool flag3;
		if (_performedFired)
		{
			flag3 = true;
		}
		else if (onPerformed != null)
		{
			int persistentEventCount2 = onPerformed.GetPersistentEventCount();
			bool flag4 = persistentEventCount2 == 0;
			flag3 = flag4;
		}
		else
		{
			flag3 = false;
		}
		bool flag5 = _canceledFired;
		bool flag6 = true;
		if (!flag5)
		{
			if (onCanceled != null)
			{
				int persistentEventCount3 = onCanceled.GetPersistentEventCount();
				bool flag7 = persistentEventCount3 == 0;
				flag6 = flag7;
			}
			else
			{
				flag6 = false;
			}
		}
		object obj = flag6 & flag3;
		object obj2 = flag & obj;
		if (obj2 != null)
		{
			InputAction boundAction = BoundAction;
			if (boundAction != null)
			{
				boundAction.started -= _startedHandler;
				boundAction.performed -= _performedHandler;
				boundAction.canceled -= _canceledHandler;
			}
		}
	}

	private unsafe bool ShouldFire(InputAction.CallbackContext ctx)
	{
		//IL_0464: Expected I4, but got O
		//IL_03ee: Expected I4, but got O
		//IL_033a: Expected I, but got O
		//IL_00d2: Expected I, but got O
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Expected O, but got Unknown
		//IL_0399: Invalid comparison between O and F4
		//IL_03b8: Invalid comparison between F4 and I4
		//IL_0256: Expected I, but got O
		//IL_0285: Expected I, but got O
		//IL_011c: Expected I, but got O
		//IL_02c2: Expected O, but got I
		//IL_02e5: Invalid comparison between O and F4
		//IL_0304: Invalid comparison between F4 and I4
		//IL_016e: Expected I, but got O
		//IL_042b: Expected I, but got O
		//IL_01c4: Expected O, but got I
		//IL_0201: Invalid comparison between O and F4
		//IL_0220: Invalid comparison between F4 and I4
		if (requireNonDefaultValue)
		{
			InputAction inputAction = ((InputAction.CallbackContext*)ctx)->action;
			if (inputAction != null)
			{
				if (inputAction.m_Type == InputActionType.Button)
				{
					return ((InputAction.CallbackContext*)ctx)->ReadValueAsButton();
				}
				object obj = ((InputAction.CallbackContext*)ctx)->ReadValueAsObject();
				if (obj == null)
				{
					return (byte)(int)obj != 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
				bool flag = obj != null;
				object obj2 = null;
				if (!flag)
				{
					obj2 = obj;
				}
				if (obj2 == null)
				{
					nint num2 = (nint)typeof(Vector2);
					bool flag2 = (object)obj.GetType() != typeof(Vector2);
					object obj3 = null;
					if (!flag2)
					{
						obj3 = obj;
					}
					if (obj3 == null)
					{
						nint num3 = (nint)typeof(Vector3);
						bool flag3 = (object)obj.GetType() != typeof(Vector3);
						object obj4 = null;
						if (!flag3)
						{
							obj4 = obj;
						}
						if (obj4 == null)
						{
							return true;
						}
						nint num4 = (nint)obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v361 @ rcx_v14 (Il2CppClass<System.Object>)+40]");
						nint num5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ r9_v7 (Il2CppClass<UnityEngine.Vector3>)+40]");
						if (num5 == 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v367 @ rax_v25+8]");
							nint num6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v367 @ rax_v25+8]");
							object obj5 = num6 * 0;
							object obj7 = default(object);
							object obj6 = obj7 * obj7;
							object obj9 = default(object);
							object obj8 = obj9 * obj9;
							object obj10 = obj8 + obj6;
							object obj11 = obj10 + obj5;
							bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f);
							float num7 = (float)obj11 - 1E-06f;
							bool flag5 = num7 == 0f;
							bool flag6 = !flag4;
							bool flag7 = !flag5;
							return flag7 & flag6;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						num = (nint)typeof(Vector3);
						object obj12 = obj;
					}
					else
					{
						nint num8 = (nint)obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v329 @ rcx_v12 (Il2CppClass<System.Object>)+40]");
						nint num9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r9_v5 (Il2CppClass<UnityEngine.Vector2>)+40]");
						bool flag8 = num9 != 0;
						num = (nint)typeof(Vector2);
						object obj12 = obj;
						if (!flag8)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v20+4]");
							nint num10 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v20+4]");
							object obj13 = num10 * 0;
							object obj15 = default(object);
							object obj14 = obj15 * obj15;
							object obj16 = obj14 + obj13;
							bool flag9 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj16) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f);
							float num11 = (float)obj16 - 1E-06f;
							bool flag10 = num11 == 0f;
							bool flag11 = !flag9;
							bool flag12 = !flag10;
							return flag12 & flag11;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				}
				else
				{
					nint num12 = (nint)obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v274 @ rcx_v9 (Il2CppClass<System.Object>)+40]");
					nint num13 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r9_v4 (Il2CppClass<UnityEngine.Vector3>)+40]");
					bool flag13 = num13 != 0;
					object obj12 = obj;
					if (!flag13)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						object obj18 = default(object);
						object obj17 = obj18 & 0;
						bool flag14 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f);
						float num14 = (float)obj17 - 0.0001f;
						bool flag15 = num14 == 0f;
						bool flag16 = !flag14;
						bool flag17 = !flag15;
						return flag17 & flag16;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return true;
	}

	private unsafe void _003CAwake_003Eb__15_0(InputAction.CallbackContext ctx)
	{
		//IL_004b: Expected O, but got Ref
		object obj = default(object);
		if ((!fireOnce || !_startedFired) && ShouldFire((InputAction.CallbackContext)(&obj)))
		{
			if (onStarted != null)
			{
				onStarted.Invoke();
			}
			if (fireOnce)
			{
				_startedFired = true;
				UnsubscribeIfDone();
			}
		}
	}

	private unsafe void _003CAwake_003Eb__15_1(InputAction.CallbackContext ctx)
	{
		//IL_004b: Expected O, but got Ref
		object obj = default(object);
		if ((!fireOnce || !_performedFired) && ShouldFire((InputAction.CallbackContext)(&obj)))
		{
			if (onPerformed != null)
			{
				onPerformed.Invoke();
			}
			if (fireOnce)
			{
				_performedFired = true;
				UnsubscribeIfDone();
			}
		}
	}

	private unsafe void _003CAwake_003Eb__15_2(InputAction.CallbackContext ctx)
	{
		//IL_004b: Expected O, but got Ref
		object obj = default(object);
		if ((!fireOnce || !_canceledFired) && ShouldFire((InputAction.CallbackContext)(&obj)))
		{
			if (onCanceled != null)
			{
				onCanceled.Invoke();
			}
			if (fireOnce)
			{
				_canceledFired = true;
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 74 Invalid \"Jump target not found in method: 0x1804ED840\"");
			}
		}
	}
}
