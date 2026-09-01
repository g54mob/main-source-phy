using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualCursor : MonoBehaviour
{
	private InputActionReference pointerDeltaAction;

	private InputActionReference pointerPositionAction;

	private InputActionReference cursorSlowingAction;

	private bool enableActionsOnEnable = true;

	private float deltaSpeed = 1200f;

	private AnimationCurve deltaAcceleration = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	private bool adoptAbsoluteFromPositionAction = true;

	private float absoluteAdoptionDeadzoneSqr = 25f;

	private float absoluteChangeThresholdPx = 2f;

	private float absoluteSuppressAfterDeltaSeconds = 0.15f;

	private bool clampToScreen = true;

	private float edgePadding = 2f;

	private DynamicCursorManager cursorManager;

	private bool lockToCenterWhenFPSLocked = true;

	private Vector2 _position;

	private bool _initialized;

	private float _deltaLastUsedTime = -1f / 0f;

	private Vector2 _lastAbsolutePosition;

	public float ControllerSensitivity = 2f;

	public Vector2 ScreenPosition
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
	}

	public void WarpTo(Vector2 screenPosition)
	{
		bool flag = !clampToScreen;
		_position = screenPosition;
		if (!flag)
		{
			Vector2 position = ClampToScreen(screenPosition, edgePadding);
			_position = position;
		}
		_lastAbsolutePosition = _position;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VirtualCursor)+70]");
		_ = 0;
	}

	private void OnEnable()
	{
		//IL_0053: Expected O, but got F4
		if (!_initialized)
		{
			int width = Screen.width;
			float num = (float)width * 0.5f;
			int height = Screen.height;
			_position = (Vector2)num;
			_initialized = true;
			float num2 = (float)height * 0.5f;
			Vector2 lastAbsolutePosition = default(Vector2);
			_lastAbsolutePosition = lastAbsolutePosition;
		}
		if (enableActionsOnEnable)
		{
			TryEnable(pointerDeltaAction);
			TryEnable(pointerPositionAction);
			TryEnable(cursorSlowingAction);
		}
	}

	private void Update()
	{
		//IL_0135: Invalid comparison between F4 and I4
		//IL_015e: Expected O, but got I4
		//IL_017e: Expected O, but got I4
		//IL_002e: Expected O, but got I4
		//IL_02c1: Invalid comparison between O and F4
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		//IL_0308: Expected O, but got I4
		//IL_0328: Expected O, but got I4
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Expected O, but got Unknown
		//IL_08c8: Invalid comparison between I4 and F4
		//IL_006b: Expected O, but got I4
		//IL_092a: Expected O, but got I4
		//IL_0233: Expected F4, but got I4
		//IL_0523: Expected O, but got I
		//IL_093d: Expected I, but got O
		//IL_0ae5: Expected O, but got F4
		//IL_00aa: Expected O, but got F4
		//IL_027c: Expected O, but got I4
		//IL_025c: Expected O, but got I4
		//IL_0593: Expected F8, but got I4
		//IL_099f: Expected O, but got F8
		//IL_05da: Expected F8, but got I4
		//IL_05e3: Expected F8, but got I4
		//IL_0757: Invalid comparison between O and F4
		Vector2 vector = default(Vector2);
		if (lockToCenterWhenFPSLocked)
		{
			bool flag = cursorManager != null;
			bool flag2 = !flag;
			object obj = 0;
			if (!flag2)
			{
				DynamicCursorManager dynamicCursorManager = cursorManager;
				bool flag3 = dynamicCursorManager._currentMode != DynamicCursorManager.PresentationMode.FPSLocked;
				obj = 0;
				if (!flag3)
				{
					int width = Screen.width;
					float num = (float)width * 0.5f;
					int height = Screen.height;
					_position = (Vector2)num;
					float num2 = (float)height * 0.5f;
					_lastAbsolutePosition = vector;
					return;
				}
			}
		}
		Vector2 vector2 = ReadVector2(pointerDeltaAction);
		object obj3 = default(object);
		object obj2 = obj3 * obj3;
		float num3 = (float)vector2 * (float)vector2;
		float num4 = (float)obj2 + num3;
		bool flag4 = num4 < 0.0001f;
		float num5 = num4 - 0.0001f;
		bool flag5 = num5 == 0f;
		bool flag6 = !flag4;
		bool flag7 = !flag5;
		object obj4 = flag7 & flag6;
		bool flag8 = obj4 == null;
		Vector2 vector3 = vector2;
		object obj5 = 0;
		if (!flag8)
		{
			float num6 = ControllerSensitivity * 0.25f;
			InputAction action = cursorSlowingAction.action;
			bool flag9 = action.IsPressed();
			bool flag10 = !flag9;
			float num7 = 1f;
			if (!flag10)
			{
				num7 = 0.5f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
			float num8;
			if (!(0f > num3))
			{
				bool flag11 = !(num3 > 1f);
				num8 = num3;
				if (!flag11)
				{
					num8 = 1f;
				}
			}
			else
			{
				num8 = 0f;
			}
			if (deltaAcceleration != null)
			{
				float num9 = deltaAcceleration.Evaluate(num8);
				num8 = num9;
				object obj = 0;
			}
			Vector2 value = default(Vector2);
			Vector2 vector4 = Vector2.Normalize(ref value);
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			float num10 = (float)obj3 * num8;
			float num11 = (float)vector4 * num8;
			float num12 = num10 * deltaSpeed;
			float num13 = num11 * deltaSpeed;
			float num14 = num12 * unscaledDeltaTime;
			float num15 = num13 * unscaledDeltaTime;
			float num16 = num14 * num6;
			float num17 = num15 * num6;
			float num18 = num16 * num7;
			float num19 = num17 * num7;
			float num20 = num18;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VirtualCursor)+70]");
			float num21 = num20 + 0f;
			float num22 = num19 + (float)_position;
			_position = (Vector2)num22;
			float unscaledTime = Time.unscaledTime;
			_deltaLastUsedTime = unscaledTime;
			vector3 = vector4;
			obj5 = 0;
		}
		if (adoptAbsoluteFromPositionAction)
		{
			Vector2 vector5 = ReadVector2(pointerPositionAction);
			object obj6 = obj3 * obj3;
			object obj7 = vector5 * vector5;
			object obj8 = obj6 + obj7;
			bool flag12 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)absoluteAdoptionDeadzoneSqr);
			object obj9 = obj8 - absoluteAdoptionDeadzoneSqr;
			bool flag13 = obj9 == null;
			bool flag14 = !flag12;
			bool flag15 = !flag13;
			object obj10 = flag15 & flag14;
			bool flag16 = obj10 == null;
			vector3 = vector5;
			obj5 = 0;
			if (!flag16)
			{
				float unscaledTime2 = Time.unscaledTime;
				float num23 = unscaledTime2 - _deltaLastUsedTime;
				object obj11 = vector5 - _lastAbsolutePosition;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VirtualCursor)+80]");
				object obj12 = obj3 - 0;
				object obj13 = obj11 * obj11;
				object obj14 = obj12 * obj12;
				object obj15 = obj14 + obj13;
				if (absoluteSuppressAfterDeltaSeconds != num23)
				{
					_position = vector5;
					_deltaLastUsedTime = -1f / 0f;
				}
				_lastAbsolutePosition = vector5;
				vector3 = vector5;
				obj5 = 0;
			}
		}
		DynamicCursorManager dynamicCursorManager2 = cursorManager;
		float num24;
		if (dynamicCursorManager2._003CIsClampedToValve_003Ek__BackingField)
		{
			int height2 = Screen.height;
			DynamicCursorManager dynamicCursorManager3 = cursorManager;
			num24 = (float)height2 * dynamicCursorManager3._003CCursorDistanceMultiplierFromCenter_003Ek__BackingField;
			if (obj4 != null || !dynamicCursorManager3._003CIsAngleConstrained_003Ek__BackingField || !dynamicCursorManager3._003CResetToDefault_003Ek__BackingField || dynamicCursorManager3._003CIsClampingMouse_003Ek__BackingField)
			{
				DynamicCursorManager dynamicCursorManager4 = cursorManager;
				object obj16 = _position - dynamicCursorManager4._003CValveScreenPosition_003Ek__BackingField;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VirtualCursor)+70]");
				nint num25 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v896 @ rax_v15 (DynamicCursorManager)+110]");
				object obj17 = num25 - 0;
				nint num26 = (nint)typeof(Math);
				object obj18 = obj17 * obj17;
				object obj19 = obj16 * obj16;
				double d = (double)obj18 + (double)obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v338 @ rcx_v13 (Il2CppClass<System.Math>)+E4]");
				double num27;
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm2\"");
					num27 = 0.0;
				}
				else
				{
					num27 = Math.Sqrt(d);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
				double num28;
				double num29;
				if (!(num27 > 9.999999747378752E-06))
				{
					num28 = 0.0;
					num29 = 0.0;
				}
				else
				{
					num29 = (double)obj16 / num27;
					num28 = (double)obj17 / num27;
				}
				DynamicCursorManager dynamicCursorManager5 = cursorManager;
				double num30 = num29 * (double)num24;
				double num31 = num28 * (double)num24;
				double num32 = num30 + (double)dynamicCursorManager4._003CValveScreenPosition_003Ek__BackingField;
				double num33 = num31;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v896 @ rax_v15 (DynamicCursorManager)+110]");
				double num34 = num33 + 0.0;
				_position = (Vector2)num32;
				double num35 = num34;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v327 @ rax_v18 (DynamicCursorManager)+110]");
				double num36 = num35 - 0.0;
				double num37 = num32 - (double)dynamicCursorManager5._003CValveScreenPosition_003Ek__BackingField;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
				DynamicCursorManager dynamicCursorManager6 = cursorManager;
				double num38 = num36 * 57.295780181884766;
				if (dynamicCursorManager6._003CIsAngleConstrained_003Ek__BackingField && !dynamicCursorManager6._003CIsClampingMouse_003Ek__BackingField)
				{
					if (!(dynamicCursorManager6._003CMinAngle_003Ek__BackingField > dynamicCursorManager6._003CMaxAngle_003Ek__BackingField))
					{
						if (num38 > (double)dynamicCursorManager6._003CMaxAngle_003Ek__BackingField)
						{
							goto IL_0744;
						}
					}
					else
					{
						if (!(num38 > (double)dynamicCursorManager6._003CMaxAngle_003Ek__BackingField))
						{
							goto IL_09a9;
						}
						dynamicCursorManager6 = cursorManager;
					}
					if ((double)dynamicCursorManager6._003CMinAngle_003Ek__BackingField > num38)
					{
						goto IL_0744;
					}
				}
				goto IL_09a9;
			}
			_position = (_lastAbsolutePosition = SnapToDefaultPosition(vector, vector, num24));
		}
		goto IL_0831;
		IL_09a9:
		DynamicCursorManager dynamicCursorManager7 = cursorManager;
		_lastAbsolutePosition = _position;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VirtualCursor)+70]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A951]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		string currentControlScheme = dynamicCursorManager7._playerInput.currentControlScheme;
		if (currentControlScheme != "Gamepad")
		{
			Mouse._003Ccurrent_003Ek__BackingField.WarpCursorPosition(vector);
			Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180708830");
		}
		goto IL_0831;
		IL_0831:
		if (clampToScreen)
		{
			Vector2 position = ClampToScreen(vector, edgePadding);
			_position = position;
		}
		return;
		IL_0744:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
		if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)200f))
		{
			vector3 = (_lastAbsolutePosition = SnapToDefaultPosition(vector, vector, num24));
		}
		_position = _lastAbsolutePosition;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VirtualCursor)+80]");
		_ = 0;
		goto IL_09a9;
	}

	private Vector2 ClampToCircle(Vector2 center, float radius, Vector2 positionOutsideCircle)
	{
		//IL_00db: Expected I, but got O
		//IL_008a: Expected F8, but got I4
		object obj = positionOutsideCircle - center;
		object obj3 = default(object);
		object obj4 = default(object);
		object obj2 = obj3 - obj4;
		nint num = (nint)typeof(Math);
		object obj5 = obj2 * obj2;
		object obj6 = obj * obj;
		double d = (double)obj5 + (double)obj6;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
		double num2;
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm2\"");
			num2 = 0.0;
		}
		else
		{
			num2 = Math.Sqrt(d);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
		if (!(num2 > 9.999999747378752E-06))
		{
		}
		Vector2 result = default(Vector2);
		return result;
	}

	private Vector2 SnapToDefaultPosition(Vector2 center, Vector2 defaultPos, float radius)
	{
		//IL_00db: Expected I, but got O
		//IL_008a: Expected F8, but got I4
		object obj = defaultPos - center;
		object obj3 = default(object);
		object obj4 = default(object);
		object obj2 = obj3 - obj4;
		nint num = (nint)typeof(Math);
		object obj5 = obj2 * obj2;
		object obj6 = obj * obj;
		double d = (double)obj5 + (double)obj6;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
		double num2;
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm2\"");
			num2 = 0.0;
		}
		else
		{
			num2 = Math.Sqrt(d);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
		if (!(num2 > 9.999999747378752E-06))
		{
		}
		Vector2 result = default(Vector2);
		return result;
	}

	private void TryEnable(InputActionReference actionRef)
	{
		if (!(actionRef != null))
		{
			return;
		}
		InputAction action = actionRef.action;
		if (action != null)
		{
			InputAction action2 = actionRef.action;
			if (!action2.enabled)
			{
				InputAction action3 = actionRef.action;
				action3.Enable();
			}
		}
	}

	private static Vector2 ClampToScreen(Vector2 p, float pad)
	{
		//IL_00a9: Invalid comparison between F4 and O
		//IL_0104: Invalid comparison between F4 and O
		//IL_0054: Invalid comparison between O and F4
		//IL_0085: Invalid comparison between O and F4
		int width = Screen.width;
		float num = (float)width - pad;
		bool flag = !(pad < num);
		float num2 = pad;
		if (!flag)
		{
			num2 = num;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)pad) > System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref p) || System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref p) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
		}
		int height = Screen.height;
		float num3 = (float)height - pad;
		bool flag2 = !(pad < num3);
		float num4 = pad;
		if (!flag2)
		{
			num4 = num3;
		}
		object obj = default(object);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)pad) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) || System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4))
		{
		}
		Vector2 result = default(Vector2);
		return result;
	}

	private static Vector2 ReadVector2(InputActionReference actionRef)
	{
		if (actionRef != null)
		{
			if ((object)actionRef == null)
			{
				goto IL_00e3;
			}
			InputAction action = actionRef.action;
			if (action != null)
			{
				InputAction action2 = actionRef.action;
				if (action2 == null)
				{
					goto IL_00e3;
				}
				if (action2.enabled)
				{
					InputAction action3 = actionRef.action;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
					Vector2 result = default(Vector2);
					return result;
				}
			}
		}
		Vector2 result2 = default(Vector2);
		return result2;
		IL_00e3:
		return (Vector2)new NullReferenceException();
	}
}
