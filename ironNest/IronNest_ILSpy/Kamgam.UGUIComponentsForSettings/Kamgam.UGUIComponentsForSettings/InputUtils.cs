using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace Kamgam.UGUIComponentsForSettings;

public static class InputUtils
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyControl, bool> _003C_003E9__13_0;

		public static Func<KeyControl, Key> _003C_003E9__13_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CbuildKeyCache_003Eb__13_0(KeyControl k)
		{
			bool flag = k == null;
			return !flag;
		}

		internal Key _003CbuildKeyCache_003Eb__13_1(KeyControl k)
		{
			//IL_0035: Expected I4, but got O
			if (k != null)
			{
				return k._003CkeyCode_003Ek__BackingField;
			}
			NullReferenceException ex = new NullReferenceException();
			return (Key)ex;
		}
	}

	private static int _lastMouseWheelChangeFrame;

	private static Vector2 _lastMouseWheelValue;

	public static Key[] Keys;

	private static List<UniversalKeyCode> _tmpUniversalKeyResults;

	private static Dictionary<UniversalKeyCode, string> keyNameDictionary;

	private static List<(Key, UniversalKeyCode)> _inputSystemKeyMap;

	public static string BindingPathToDisplayName(string path, Func<string, string> localizeFunc = null, string localizedSeparator = " + ")
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00c3: Expected O, but got I4
		//IL_00cf: Expected O, but got I4
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Expected O, but got Unknown
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Expected O, but got Unknown
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Expected O, but got Unknown
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Expected O, but got Unknown
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Expected O, but got Unknown
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Expected O, but got Unknown
		int num = path.IndexOf(InputBindingForInputSystem.CompositeControlSeparator);
		string text;
		if (num < 0)
		{
			text = Regex.Replace(path, "<[^>]*>/", "");
			if (text._stringLength < 6)
			{
				text = text.ToUpper();
			}
		}
		else
		{
			string[] array = path.Split(InputBindingForInputSystem.CompositeControlSeparator);
			bool flag = string.IsNullOrEmpty(localizedSeparator);
			bool flag2 = !flag;
			string text2 = localizedSeparator;
			if (!flag2)
			{
				text2 = " + ";
			}
			object obj = array + 32;
			object obj2 = 0;
			object obj3 = 0;
			string text3 = "";
			string text5 = default(string);
			string text7 = default(string);
			while ((nint)obj3 < array.Length)
			{
				if (obj2 != null)
				{
					if ((nint)obj2 < array.Length)
					{
						string text4;
						if (obj != null)
						{
							if (localizeFunc == null)
							{
								text4 = Regex.Replace((string)obj, "<[^>]*>/", "");
								if (text4._stringLength < 6)
								{
									text4 = text4.ToUpper();
								}
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [localizeFunc @ rdx (System.Func`2<System.String, System.String>)+18] (should have been resolved before IL gen)");
								text4 = text5;
							}
						}
						else
						{
							text4 = "";
						}
						string text6 = text3 + text2 + text4;
						obj2++;
						obj += 8;
						obj3 = obj2;
						text3 = text6;
						continue;
					}
				}
				else if (array.Length != 0)
				{
					if (obj != null)
					{
						if (localizeFunc != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [localizeFunc @ rdx (System.Func`2<System.String, System.String>)+18] (should have been resolved before IL gen)");
							obj2++;
							obj += 8;
							obj3 = obj2;
							text3 = text7;
							continue;
						}
						string text8 = Regex.Replace((string)obj, "<[^>]*>/", "");
						bool flag3 = text8._stringLength >= 6;
						text3 = text8;
						if (!flag3)
						{
							string text9 = text8.ToUpper();
							obj2++;
							obj += 8;
							obj3 = obj2;
							text3 = text9;
							continue;
						}
					}
					else
					{
						text3 = "";
					}
					obj2++;
					obj += 8;
					obj3 = obj2;
					continue;
				}
				return (string)(object)new IndexOutOfRangeException();
			}
			text = text3;
		}
		return text;
	}

	public static string SingleBindingPathToDisplayName(string path, Func<string, string> localizeFunc = null)
	{
		string text;
		if (path != null)
		{
			if (localizeFunc != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [localizeFunc @ rdx (System.Func`2<System.String, System.String>)+18] (should have been resolved before IL gen)");
				string result = default(string);
				return result;
			}
			text = Regex.Replace(path, "<[^>]*>/", "");
			if (text == null)
			{
				return (string)(object)new NullReferenceException();
			}
			if (text._stringLength < 6)
			{
				return text.ToUpper();
			}
		}
		else
		{
			text = "";
		}
		return text;
	}

	public static bool IsGamePadKey(UniversalKeyCode keyCode)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 15 Invalid \"Jump target not found in method: 0x180A639A1\"");
		return (byte)(keyCode - 501) != 0;
	}

	public static bool IsMouseWheel(UniversalKeyCode keyCode)
	{
		//IL_0034: Expected O, but got I4
		if (keyCode == UniversalKeyCode.MouseWheelUp)
		{
			return true;
		}
		object obj = keyCode - 18;
		return obj == null;
	}

	public static UniversalKeyCode KeyCodeToUniversalKeyCode(KeyCode keyCode)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 39 Invalid \"Jump target not found in method: 0x180A63A70\"");
		UniversalKeyCode result = default(UniversalKeyCode);
		return result;
	}

	public static UniversalKeyCode KeyCodeToUniversalKeyCode(KeyCode keyCode, bool convertJoyStickToGamePad)
	{
		//IL_000d: Expected O, but got I8
		//IL_006c: Expected O, but got I4
		//IL_00cd: Expected O, but got I8
		//IL_003a: Expected O, but got I
		//IL_0054: Expected O, but got I8
		//IL_0097: Expected O, but got I4
		//IL_00b1: Expected O, but got I8
		//IL_0136: Expected O, but got I8
		//IL_0112: Expected O, but got I8
		object obj = 6442450944L;
		if (keyCode <= KeyCode.Delete)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r10_v1+A63F1C+keyCode @ rcx (UnityEngine.KeyCode)]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r10_v1+A63E34+v16 @ rax_v7*4]");
			object obj3 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v18 @ r8_v2 (should have been resolved before IL gen)");
		}
		object obj4 = keyCode - 256;
		if ((nint)obj4 <= 73)
		{
			object obj5 = keyCode - 256;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r10_v1+A63F9C+v83 @ rax_v4*4]");
			object obj6 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v45 @ rax_v6 (should have been resolved before IL gen)");
		}
		object obj7 = (long)keyCode + 4294966966L;
		if ((nint)obj7 <= 10)
		{
			if (!convertJoyStickToGamePad)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r10_v1+A640C4+v82 @ r9_v2*4]");
				object obj8 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v40 @ rcx_v4 (should have been resolved before IL gen)");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ r10_v1+A640F0+v82 @ r9_v2*4]");
			object obj9 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v41 @ rcx_v2 (should have been resolved before IL gen)");
		}
		return UniversalKeyCode.Unknown;
	}

	public static KeyCode UniversalKeyCodeToKeyCode(UniversalKeyCode universalKeyCode)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 15 Invalid \"Jump target not found in method: 0x180A650CB\"");
		KeyCode result = default(KeyCode);
		return result;
	}

	private static void Init()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180387260");
		Action<InputEventPtr, InputDevice> action = OnInputEvent;
		InputEventListener inputEventListener2 = default(InputEventListener);
		InputEventListener inputEventListener = inputEventListener2 + action;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	private static void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
	{
		//IL_0097: Expected I, but got O
		InputEventPtr eventPtr2 = default(InputEventPtr);
		if (device == Mouse._003Ccurrent_003Ek__BackingField && (eventPtr2.IsA<StateEvent>() || eventPtr2.IsA<DeltaStateEvent>()))
		{
			Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
			if (InputControlExtensions.HasValueChangeInEvent(mouse._003Cscroll_003Ek__BackingField, eventPtr2))
			{
				Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807036B0");
				nint num = (nint)typeof(InputUtils);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ rax_v16 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputUtils>)+B8]");
				nint num2 = 0;
				Vector2 lastMouseWheelValue = default(Vector2);
				_lastMouseWheelValue = lastMouseWheelValue;
				int frameCount = Time.frameCount;
				_lastMouseWheelChangeFrame = frameCount;
			}
		}
	}

	private unsafe static void buildKeyCache()
	{
		//IL_0061: Expected O, but got Ref
		if (Keys != null)
		{
			return;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			Keys = System.EmptyArray<Key>.Value;
			return;
		}
		object obj = default(object);
		ReadOnlyArray<KeyControl> allKeys = ((Keyboard)(&obj)).allKeys;
		object obj2 = default(object);
		IEnumerable<KeyControl> source = (ReadOnlyArray<KeyControl>)obj2;
		Func<KeyControl, bool> predicate = _003C_003Ec._003C_003E9__13_0;
		if (_003C_003Ec._003C_003E9__13_0 == null)
		{
			predicate = (_003C_003Ec._003C_003E9__13_0 = delegate(KeyControl k)
			{
				bool flag = k == null;
				return !flag;
			});
		}
		IEnumerable<KeyControl> source2 = Enumerable.Where(source, predicate);
		Func<KeyControl, Key> selector = _003C_003Ec._003C_003E9__13_1;
		if (_003C_003Ec._003C_003E9__13_1 == null)
		{
			selector = (_003C_003Ec._003C_003E9__13_1 = delegate(KeyControl k)
			{
				//IL_0035: Expected I4, but got O
				if (k == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (Key)ex;
				}
				return k._003CkeyCode_003Ek__BackingField;
			});
		}
		IEnumerable<Key> source3 = Enumerable.Select(source2, selector);
		Key[] keys = Enumerable.ToArray(source3);
		Keys = keys;
	}

	public unsafe static void ResetStuckKeyStates()
	{
		//IL_0029: Expected F4, but got Ref
		//IL_0057: Expected F4, but got Ref
		KeyControl leftAltKey = Keyboard._003Ccurrent_003Ek__BackingField.leftAltKey;
		object obj = default(object);
		InputControlExtensions.QueueValueChange(leftAltKey, (nint)(&obj));
		KeyControl tabKey = Keyboard._003Ccurrent_003Ek__BackingField.tabKey;
		InputControlExtensions.QueueValueChange(tabKey, (nint)(&obj));
	}

	public unsafe static bool AnyKey()
	{
		//IL_1053: Expected I4, but got O
		//IL_0f03: Expected O, but got Ref
		//IL_0f3a: Expected O, but got I4
		//IL_1018: Unknown result type (might be due to invalid IL or missing references)
		//IL_101d: Expected O, but got Unknown
		//IL_0f56: Expected I, but got O
		//IL_0f5e: Expected I, but got O
		//IL_0f6e: Expected O, but got I
		//IL_0faa: Expected O, but got I
		//IL_02f9: Expected O, but got I
		//IL_0137: Expected O, but got I
		//IL_0368: Expected O, but got I
		//IL_01a6: Expected O, but got I
		//IL_03d7: Expected O, but got I
		//IL_0446: Expected O, but got I
		//IL_04b5: Expected O, but got I
		//IL_0524: Expected O, but got I
		//IL_0593: Expected O, but got I
		//IL_05dc: Expected O, but got I
		//IL_0637: Expected O, but got I
		//IL_0680: Expected O, but got I
		//IL_06db: Expected O, but got I
		//IL_0724: Expected O, but got I
		//IL_077f: Expected O, but got I
		//IL_07c8: Expected O, but got I
		//IL_0823: Expected O, but got I
		//IL_0892: Expected O, but got I
		//IL_08db: Expected O, but got I
		//IL_0936: Expected O, but got I
		//IL_097f: Expected O, but got I
		//IL_09da: Expected O, but got I
		//IL_0a23: Expected O, but got I
		//IL_0a7e: Expected O, but got I
		//IL_0ac7: Expected O, but got I
		//IL_0b22: Expected O, but got I
		//IL_0b91: Expected O, but got I
		//IL_0c00: Expected O, but got I
		//IL_0c49: Expected O, but got I
		//IL_0ca4: Expected O, but got I
		//IL_0ced: Expected O, but got I
		//IL_0d48: Expected O, but got I
		//IL_0d91: Expected O, but got I
		//IL_0dec: Expected O, but got I
		//IL_0e35: Expected O, but got I
		//IL_0e90: Expected O, but got I
		if (Mouse._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_10f3;
		}
		Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
		if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse._003CleftButton_003Ek__BackingField != null)
		{
			if (mouse._003CleftButton_003Ek__BackingField.isPressed)
			{
				goto IL_103f;
			}
			Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
			if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse2._003CmiddleButton_003Ek__BackingField != null)
			{
				if (mouse2._003CmiddleButton_003Ek__BackingField.isPressed)
				{
					goto IL_103f;
				}
				Mouse mouse3 = Mouse._003Ccurrent_003Ek__BackingField;
				if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse3._003CrightButton_003Ek__BackingField != null)
				{
					if (mouse3._003CrightButton_003Ek__BackingField.isPressed)
					{
						goto IL_103f;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3FA0");
					object obj = default(object);
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rax_v104+1E8]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rax_v104+1E8]");
							if (((ButtonControl)0).isPressed)
							{
								goto IL_103f;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3FA0");
							object obj2 = default(object);
							if (obj2 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ rax_v106+1E0]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ rax_v106+1E0]");
									if (!((ButtonControl)0).isPressed)
									{
										int frameCount = Time.frameCount;
										if (_lastMouseWheelChangeFrame != frameCount)
										{
											goto IL_10f3;
										}
									}
									goto IL_103f;
								}
							}
						}
					}
				}
			}
		}
		goto IL_1045;
		IL_11a4:
		if (Joystick._003Ccurrent_003Ek__BackingField != null)
		{
			if (Joystick._003Ccurrent_003Ek__BackingField == null)
			{
				goto IL_1045;
			}
			if (Joystick._003Ccurrent_003Ek__BackingField.wasUpdatedThisFrame)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A57A30");
				object obj3 = default(object);
				if (obj3 == null)
				{
					goto IL_1045;
				}
				object obj4 = default(object);
				ReadOnlyArray<InputControl> allControls = ((InputDevice)(&obj4)).allControls;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
				object obj5 = (object)allControls >> 32;
				bool flag = (nint)obj5 <= 0;
				object obj6 = 0;
				if (!flag)
				{
					ButtonControl buttonControl = default(ButtonControl);
					object obj9 = default(object);
					while (true)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
						if (buttonControl != null)
						{
							nint num = (nint)typeof(ButtonControl);
							nint num2 = (nint)buttonControl;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1749 @ rdx_v17 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							object obj7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1271 @ r9_v6 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1749 @ rdx_v17 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1271 @ r9_v6 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+C8]");
								object obj8 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1763 @ rax_v30+FFFFFFF8+v1750 @ rax_v29*8]");
								if (0 == (nint)typeof(ButtonControl) && buttonControl != null && buttonControl.isPressed)
								{
									break;
								}
							}
						}
						obj6++;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9))
						{
							continue;
						}
						goto IL_1039;
					}
					goto IL_103f;
				}
			}
		}
		goto IL_1039;
		IL_10f3:
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			Keyboard keyboard = Keyboard._003Ccurrent_003Ek__BackingField;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null || keyboard._003CanyKey_003Ek__BackingField == null)
			{
				goto IL_1045;
			}
			if (keyboard._003CanyKey_003Ek__BackingField.isPressed)
			{
				goto IL_103f;
			}
		}
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_11a4;
		}
		Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
		if (Gamepad._003Ccurrent_003Ek__BackingField != null && gamepad._003CbuttonSouth_003Ek__BackingField != null)
		{
			if (gamepad._003CbuttonSouth_003Ek__BackingField.isPressed)
			{
				goto IL_103f;
			}
			Gamepad gamepad2 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (Gamepad._003Ccurrent_003Ek__BackingField != null && gamepad2._003CbuttonEast_003Ek__BackingField != null)
			{
				if (gamepad2._003CbuttonEast_003Ek__BackingField.isPressed)
				{
					goto IL_103f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
				object obj10 = default(object);
				if (obj10 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ rax_v41+190]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ rax_v41+190]");
						if (((ButtonControl)0).isPressed)
						{
							goto IL_103f;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
						object obj11 = default(object);
						if (obj11 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rax_v43+198]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rax_v43+198]");
								if (((ButtonControl)0).isPressed)
								{
									goto IL_103f;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
								object obj12 = default(object);
								if (obj12 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ rax_v45+1D8]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ rax_v45+1D8]");
										if (((ButtonControl)0).isPressed)
										{
											goto IL_103f;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
										object obj13 = default(object);
										if (obj13 != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ rax_v47+1E0]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ rax_v47+1E0]");
												if (((ButtonControl)0).isPressed)
												{
													goto IL_103f;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
												object obj14 = default(object);
												if (obj14 != null)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ rax_v49+1C8]");
													if ((nint)0 != 0)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ rax_v49+1C8]");
														if (((ButtonControl)0).isPressed)
														{
															goto IL_103f;
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
														object obj15 = default(object);
														if (obj15 != null)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ rax_v51+1C0]");
															if ((nint)0 != 0)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ rax_v51+1C0]");
																if (((ButtonControl)0).isPressed)
																{
																	goto IL_103f;
																}
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																object obj16 = default(object);
																if (obj16 != null)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rax_v53+1B0]");
																	if ((nint)0 != 0)
																	{
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rax_v53+1B0]");
																		if (((ButtonControl)0).isPressed)
																		{
																			goto IL_103f;
																		}
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																		object obj17 = default(object);
																		if (obj17 != null)
																		{
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rax_v55+1E8]");
																			object obj18 = 0;
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rax_v55+1E8]");
																			if ((nint)0 != 0)
																			{
																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v254 @ rcx_v49+130]");
																				if ((nint)0 != 0)
																				{
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v254 @ rcx_v49+130]");
																					if (((ButtonControl)0).isPressed)
																					{
																						goto IL_103f;
																					}
																					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																					object obj19 = default(object);
																					if (obj19 != null)
																					{
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rax_v57+1E8]");
																						object obj20 = 0;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rax_v57+1E8]");
																						if ((nint)0 != 0)
																						{
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v257 @ rcx_v52+138]");
																							if ((nint)0 != 0)
																							{
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v257 @ rcx_v52+138]");
																								if (((ButtonControl)0).isPressed)
																								{
																									goto IL_103f;
																								}
																								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																								object obj21 = default(object);
																								if (obj21 != null)
																								{
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rax_v59+1E8]");
																									object obj22 = 0;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rax_v59+1E8]");
																									if ((nint)0 != 0)
																									{
																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v260 @ rcx_v55+120]");
																										if ((nint)0 != 0)
																										{
																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v260 @ rcx_v55+120]");
																											if (((ButtonControl)0).isPressed)
																											{
																												goto IL_103f;
																											}
																											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																											object obj23 = default(object);
																											if (obj23 != null)
																											{
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ rax_v61+1E8]");
																												object obj24 = 0;
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ rax_v61+1E8]");
																												if ((nint)0 != 0)
																												{
																													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rcx_v58+128]");
																													if ((nint)0 != 0)
																													{
																														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rcx_v58+128]");
																														if (((ButtonControl)0).isPressed)
																														{
																															goto IL_103f;
																														}
																														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																														object obj25 = default(object);
																														if (obj25 != null)
																														{
																															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ rax_v63+1B8]");
																															if ((nint)0 != 0)
																															{
																																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ rax_v63+1B8]");
																																if (((ButtonControl)0).isPressed)
																																{
																																	goto IL_103f;
																																}
																																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																object obj26 = default(object);
																																if (obj26 != null)
																																{
																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rax_v65+1F0]");
																																	object obj27 = 0;
																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rax_v65+1F0]");
																																	if ((nint)0 != 0)
																																	{
																																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rcx_v63+130]");
																																		if ((nint)0 != 0)
																																		{
																																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rcx_v63+130]");
																																			if (((ButtonControl)0).isPressed)
																																			{
																																				goto IL_103f;
																																			}
																																			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																			object obj28 = default(object);
																																			if (obj28 != null)
																																			{
																																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ rax_v67+1F0]");
																																				object obj29 = 0;
																																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ rax_v67+1F0]");
																																				if ((nint)0 != 0)
																																				{
																																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ rcx_v66+138]");
																																					if ((nint)0 != 0)
																																					{
																																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ rcx_v66+138]");
																																						if (((ButtonControl)0).isPressed)
																																						{
																																							goto IL_103f;
																																						}
																																						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																						object obj30 = default(object);
																																						if (obj30 != null)
																																						{
																																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rax_v69+1F0]");
																																							object obj31 = 0;
																																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rax_v69+1F0]");
																																							if ((nint)0 != 0)
																																							{
																																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v274 @ rcx_v69+120]");
																																								if ((nint)0 != 0)
																																								{
																																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v274 @ rcx_v69+120]");
																																									if (((ButtonControl)0).isPressed)
																																									{
																																										goto IL_103f;
																																									}
																																									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																									object obj32 = default(object);
																																									if (obj32 != null)
																																									{
																																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rax_v71+1F0]");
																																										object obj33 = 0;
																																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rax_v71+1F0]");
																																										if ((nint)0 != 0)
																																										{
																																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rcx_v72+128]");
																																											if ((nint)0 != 0)
																																											{
																																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rcx_v72+128]");
																																												if (((ButtonControl)0).isPressed)
																																												{
																																													goto IL_103f;
																																												}
																																												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																												object obj34 = default(object);
																																												if (obj34 != null)
																																												{
																																													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rax_v73+1F8]");
																																													if ((nint)0 != 0)
																																													{
																																														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rax_v73+1F8]");
																																														if (((ButtonControl)0).isPressed)
																																														{
																																															goto IL_103f;
																																														}
																																														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																														object obj35 = default(object);
																																														if (obj35 != null)
																																														{
																																															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v75+200]");
																																															if ((nint)0 != 0)
																																															{
																																																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v75+200]");
																																																if (((ButtonControl)0).isPressed)
																																																{
																																																	goto IL_103f;
																																																}
																																																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																																object obj36 = default(object);
																																																if (obj36 != null)
																																																{
																																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rax_v77+1D0]");
																																																	object obj37 = 0;
																																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rax_v77+1D0]");
																																																	if ((nint)0 != 0)
																																																	{
																																																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v284 @ rcx_v79+130]");
																																																		if ((nint)0 != 0)
																																																		{
																																																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v284 @ rcx_v79+130]");
																																																			if (((ButtonControl)0).isPressed)
																																																			{
																																																				goto IL_103f;
																																																			}
																																																			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																																			object obj38 = default(object);
																																																			if (obj38 != null)
																																																			{
																																																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ rax_v79+1D0]");
																																																				object obj39 = 0;
																																																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ rax_v79+1D0]");
																																																				if ((nint)0 != 0)
																																																				{
																																																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ rcx_v82+138]");
																																																					if ((nint)0 != 0)
																																																					{
																																																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ rcx_v82+138]");
																																																						if (((ButtonControl)0).isPressed)
																																																						{
																																																							goto IL_103f;
																																																						}
																																																						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																																						object obj40 = default(object);
																																																						if (obj40 != null)
																																																						{
																																																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rax_v81+1D0]");
																																																							object obj41 = 0;
																																																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rax_v81+1D0]");
																																																							if ((nint)0 != 0)
																																																							{
																																																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rcx_v85+120]");
																																																								if ((nint)0 != 0)
																																																								{
																																																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rcx_v85+120]");
																																																									if (((ButtonControl)0).isPressed)
																																																									{
																																																										goto IL_103f;
																																																									}
																																																									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																																									object obj42 = default(object);
																																																									if (obj42 != null)
																																																									{
																																																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rax_v83+1D0]");
																																																										object obj43 = 0;
																																																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rax_v83+1D0]");
																																																										if ((nint)0 != 0)
																																																										{
																																																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v293 @ rcx_v88+128]");
																																																											if ((nint)0 != 0)
																																																											{
																																																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v293 @ rcx_v88+128]");
																																																												if (((ButtonControl)0).isPressed)
																																																												{
																																																													goto IL_103f;
																																																												}
																																																												goto IL_11a4;
																																																											}
																																																										}
																																																									}
																																																								}
																																																							}
																																																						}
																																																					}
																																																				}
																																																			}
																																																		}
																																																	}
																																																}
																																															}
																																														}
																																													}
																																												}
																																											}
																																										}
																																									}
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_1045;
		IL_1045:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_1039:
		return false;
		IL_103f:
		return true;
	}

	public unsafe static bool AnyKeyDown()
	{
		//IL_1053: Expected I4, but got O
		//IL_0f03: Expected O, but got Ref
		//IL_0f3a: Expected O, but got I4
		//IL_1018: Unknown result type (might be due to invalid IL or missing references)
		//IL_101d: Expected O, but got Unknown
		//IL_0f56: Expected I, but got O
		//IL_0f5e: Expected I, but got O
		//IL_0f6e: Expected O, but got I
		//IL_0faa: Expected O, but got I
		//IL_02f9: Expected O, but got I
		//IL_0137: Expected O, but got I
		//IL_0368: Expected O, but got I
		//IL_01a6: Expected O, but got I
		//IL_03d7: Expected O, but got I
		//IL_0446: Expected O, but got I
		//IL_04b5: Expected O, but got I
		//IL_0524: Expected O, but got I
		//IL_0593: Expected O, but got I
		//IL_05dc: Expected O, but got I
		//IL_0637: Expected O, but got I
		//IL_0680: Expected O, but got I
		//IL_06db: Expected O, but got I
		//IL_0724: Expected O, but got I
		//IL_077f: Expected O, but got I
		//IL_07c8: Expected O, but got I
		//IL_0823: Expected O, but got I
		//IL_0892: Expected O, but got I
		//IL_08db: Expected O, but got I
		//IL_0936: Expected O, but got I
		//IL_097f: Expected O, but got I
		//IL_09da: Expected O, but got I
		//IL_0a23: Expected O, but got I
		//IL_0a7e: Expected O, but got I
		//IL_0ac7: Expected O, but got I
		//IL_0b22: Expected O, but got I
		//IL_0b91: Expected O, but got I
		//IL_0c00: Expected O, but got I
		//IL_0c49: Expected O, but got I
		//IL_0ca4: Expected O, but got I
		//IL_0ced: Expected O, but got I
		//IL_0d48: Expected O, but got I
		//IL_0d91: Expected O, but got I
		//IL_0dec: Expected O, but got I
		//IL_0e35: Expected O, but got I
		//IL_0e90: Expected O, but got I
		if (Mouse._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_10f3;
		}
		Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
		if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse._003CleftButton_003Ek__BackingField != null)
		{
			if (mouse._003CleftButton_003Ek__BackingField.wasPressedThisFrame)
			{
				goto IL_103f;
			}
			Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
			if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse2._003CmiddleButton_003Ek__BackingField != null)
			{
				if (mouse2._003CmiddleButton_003Ek__BackingField.wasPressedThisFrame)
				{
					goto IL_103f;
				}
				Mouse mouse3 = Mouse._003Ccurrent_003Ek__BackingField;
				if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse3._003CrightButton_003Ek__BackingField != null)
				{
					if (mouse3._003CrightButton_003Ek__BackingField.wasPressedThisFrame)
					{
						goto IL_103f;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3FA0");
					object obj = default(object);
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rax_v104+1E8]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rax_v104+1E8]");
							if (((ButtonControl)0).wasPressedThisFrame)
							{
								goto IL_103f;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3FA0");
							object obj2 = default(object);
							if (obj2 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ rax_v106+1E0]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ rax_v106+1E0]");
									if (!((ButtonControl)0).wasPressedThisFrame)
									{
										int frameCount = Time.frameCount;
										if (_lastMouseWheelChangeFrame != frameCount)
										{
											goto IL_10f3;
										}
									}
									goto IL_103f;
								}
							}
						}
					}
				}
			}
		}
		goto IL_1045;
		IL_11a4:
		if (Joystick._003Ccurrent_003Ek__BackingField != null)
		{
			if (Joystick._003Ccurrent_003Ek__BackingField == null)
			{
				goto IL_1045;
			}
			if (Joystick._003Ccurrent_003Ek__BackingField.wasUpdatedThisFrame)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A57A30");
				object obj3 = default(object);
				if (obj3 == null)
				{
					goto IL_1045;
				}
				object obj4 = default(object);
				ReadOnlyArray<InputControl> allControls = ((InputDevice)(&obj4)).allControls;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
				object obj5 = (object)allControls >> 32;
				bool flag = (nint)obj5 <= 0;
				object obj6 = 0;
				if (!flag)
				{
					ButtonControl buttonControl = default(ButtonControl);
					object obj9 = default(object);
					while (true)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
						if (buttonControl != null)
						{
							nint num = (nint)typeof(ButtonControl);
							nint num2 = (nint)buttonControl;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1749 @ rdx_v17 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							object obj7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1271 @ r9_v6 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1749 @ rdx_v17 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1271 @ r9_v6 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+C8]");
								object obj8 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1763 @ rax_v30+FFFFFFF8+v1750 @ rax_v29*8]");
								if (0 == (nint)typeof(ButtonControl) && buttonControl != null && buttonControl.wasPressedThisFrame)
								{
									break;
								}
							}
						}
						obj6++;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9))
						{
							continue;
						}
						goto IL_1039;
					}
					goto IL_103f;
				}
			}
		}
		goto IL_1039;
		IL_10f3:
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			Keyboard keyboard = Keyboard._003Ccurrent_003Ek__BackingField;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null || keyboard._003CanyKey_003Ek__BackingField == null)
			{
				goto IL_1045;
			}
			if (keyboard._003CanyKey_003Ek__BackingField.wasPressedThisFrame)
			{
				goto IL_103f;
			}
		}
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_11a4;
		}
		Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
		if (Gamepad._003Ccurrent_003Ek__BackingField != null && gamepad._003CbuttonSouth_003Ek__BackingField != null)
		{
			if (gamepad._003CbuttonSouth_003Ek__BackingField.wasPressedThisFrame)
			{
				goto IL_103f;
			}
			Gamepad gamepad2 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (Gamepad._003Ccurrent_003Ek__BackingField != null && gamepad2._003CbuttonEast_003Ek__BackingField != null)
			{
				if (gamepad2._003CbuttonEast_003Ek__BackingField.wasPressedThisFrame)
				{
					goto IL_103f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
				object obj10 = default(object);
				if (obj10 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ rax_v41+190]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ rax_v41+190]");
						if (((ButtonControl)0).wasPressedThisFrame)
						{
							goto IL_103f;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
						object obj11 = default(object);
						if (obj11 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rax_v43+198]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rax_v43+198]");
								if (((ButtonControl)0).wasPressedThisFrame)
								{
									goto IL_103f;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
								object obj12 = default(object);
								if (obj12 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ rax_v45+1D8]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ rax_v45+1D8]");
										if (((ButtonControl)0).wasPressedThisFrame)
										{
											goto IL_103f;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
										object obj13 = default(object);
										if (obj13 != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ rax_v47+1E0]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ rax_v47+1E0]");
												if (((ButtonControl)0).wasPressedThisFrame)
												{
													goto IL_103f;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
												object obj14 = default(object);
												if (obj14 != null)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ rax_v49+1C8]");
													if ((nint)0 != 0)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ rax_v49+1C8]");
														if (((ButtonControl)0).wasPressedThisFrame)
														{
															goto IL_103f;
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
														object obj15 = default(object);
														if (obj15 != null)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ rax_v51+1C0]");
															if ((nint)0 != 0)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ rax_v51+1C0]");
																if (((ButtonControl)0).wasPressedThisFrame)
																{
																	goto IL_103f;
																}
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																object obj16 = default(object);
																if (obj16 != null)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rax_v53+1B0]");
																	if ((nint)0 != 0)
																	{
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rax_v53+1B0]");
																		if (((ButtonControl)0).wasPressedThisFrame)
																		{
																			goto IL_103f;
																		}
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																		object obj17 = default(object);
																		if (obj17 != null)
																		{
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rax_v55+1E8]");
																			object obj18 = 0;
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rax_v55+1E8]");
																			if ((nint)0 != 0)
																			{
																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v254 @ rcx_v49+130]");
																				if ((nint)0 != 0)
																				{
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v254 @ rcx_v49+130]");
																					if (((ButtonControl)0).wasPressedThisFrame)
																					{
																						goto IL_103f;
																					}
																					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																					object obj19 = default(object);
																					if (obj19 != null)
																					{
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rax_v57+1E8]");
																						object obj20 = 0;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rax_v57+1E8]");
																						if ((nint)0 != 0)
																						{
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v257 @ rcx_v52+138]");
																							if ((nint)0 != 0)
																							{
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v257 @ rcx_v52+138]");
																								if (((ButtonControl)0).wasPressedThisFrame)
																								{
																									goto IL_103f;
																								}
																								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																								object obj21 = default(object);
																								if (obj21 != null)
																								{
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rax_v59+1E8]");
																									object obj22 = 0;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rax_v59+1E8]");
																									if ((nint)0 != 0)
																									{
																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v260 @ rcx_v55+120]");
																										if ((nint)0 != 0)
																										{
																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v260 @ rcx_v55+120]");
																											if (((ButtonControl)0).wasPressedThisFrame)
																											{
																												goto IL_103f;
																											}
																											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																											object obj23 = default(object);
																											if (obj23 != null)
																											{
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ rax_v61+1E8]");
																												object obj24 = 0;
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ rax_v61+1E8]");
																												if ((nint)0 != 0)
																												{
																													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rcx_v58+128]");
																													if ((nint)0 != 0)
																													{
																														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rcx_v58+128]");
																														if (((ButtonControl)0).wasPressedThisFrame)
																														{
																															goto IL_103f;
																														}
																														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																														object obj25 = default(object);
																														if (obj25 != null)
																														{
																															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ rax_v63+1B8]");
																															if ((nint)0 != 0)
																															{
																																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ rax_v63+1B8]");
																																if (((ButtonControl)0).wasPressedThisFrame)
																																{
																																	goto IL_103f;
																																}
																																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																object obj26 = default(object);
																																if (obj26 != null)
																																{
																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rax_v65+1F0]");
																																	object obj27 = 0;
																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rax_v65+1F0]");
																																	if ((nint)0 != 0)
																																	{
																																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rcx_v63+130]");
																																		if ((nint)0 != 0)
																																		{
																																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rcx_v63+130]");
																																			if (((ButtonControl)0).wasPressedThisFrame)
																																			{
																																				goto IL_103f;
																																			}
																																			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																			object obj28 = default(object);
																																			if (obj28 != null)
																																			{
																																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ rax_v67+1F0]");
																																				object obj29 = 0;
																																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ rax_v67+1F0]");
																																				if ((nint)0 != 0)
																																				{
																																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ rcx_v66+138]");
																																					if ((nint)0 != 0)
																																					{
																																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ rcx_v66+138]");
																																						if (((ButtonControl)0).wasPressedThisFrame)
																																						{
																																							goto IL_103f;
																																						}
																																						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																						object obj30 = default(object);
																																						if (obj30 != null)
																																						{
																																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rax_v69+1F0]");
																																							object obj31 = 0;
																																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rax_v69+1F0]");
																																							if ((nint)0 != 0)
																																							{
																																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v274 @ rcx_v69+120]");
																																								if ((nint)0 != 0)
																																								{
																																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v274 @ rcx_v69+120]");
																																									if (((ButtonControl)0).wasPressedThisFrame)
																																									{
																																										goto IL_103f;
																																									}
																																									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																									object obj32 = default(object);
																																									if (obj32 != null)
																																									{
																																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rax_v71+1F0]");
																																										object obj33 = 0;
																																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rax_v71+1F0]");
																																										if ((nint)0 != 0)
																																										{
																																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rcx_v72+128]");
																																											if ((nint)0 != 0)
																																											{
																																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rcx_v72+128]");
																																												if (((ButtonControl)0).wasPressedThisFrame)
																																												{
																																													goto IL_103f;
																																												}
																																												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																												object obj34 = default(object);
																																												if (obj34 != null)
																																												{
																																													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rax_v73+1F8]");
																																													if ((nint)0 != 0)
																																													{
																																														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rax_v73+1F8]");
																																														if (((ButtonControl)0).wasPressedThisFrame)
																																														{
																																															goto IL_103f;
																																														}
																																														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																														object obj35 = default(object);
																																														if (obj35 != null)
																																														{
																																															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v75+200]");
																																															if ((nint)0 != 0)
																																															{
																																																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v75+200]");
																																																if (((ButtonControl)0).wasPressedThisFrame)
																																																{
																																																	goto IL_103f;
																																																}
																																																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																																object obj36 = default(object);
																																																if (obj36 != null)
																																																{
																																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rax_v77+1D0]");
																																																	object obj37 = 0;
																																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rax_v77+1D0]");
																																																	if ((nint)0 != 0)
																																																	{
																																																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v284 @ rcx_v79+130]");
																																																		if ((nint)0 != 0)
																																																		{
																																																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v284 @ rcx_v79+130]");
																																																			if (((ButtonControl)0).wasPressedThisFrame)
																																																			{
																																																				goto IL_103f;
																																																			}
																																																			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																																			object obj38 = default(object);
																																																			if (obj38 != null)
																																																			{
																																																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ rax_v79+1D0]");
																																																				object obj39 = 0;
																																																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ rax_v79+1D0]");
																																																				if ((nint)0 != 0)
																																																				{
																																																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ rcx_v82+138]");
																																																					if ((nint)0 != 0)
																																																					{
																																																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ rcx_v82+138]");
																																																						if (((ButtonControl)0).wasPressedThisFrame)
																																																						{
																																																							goto IL_103f;
																																																						}
																																																						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																																						object obj40 = default(object);
																																																						if (obj40 != null)
																																																						{
																																																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rax_v81+1D0]");
																																																							object obj41 = 0;
																																																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rax_v81+1D0]");
																																																							if ((nint)0 != 0)
																																																							{
																																																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rcx_v85+120]");
																																																								if ((nint)0 != 0)
																																																								{
																																																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rcx_v85+120]");
																																																									if (((ButtonControl)0).wasPressedThisFrame)
																																																									{
																																																										goto IL_103f;
																																																									}
																																																									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																																																									object obj42 = default(object);
																																																									if (obj42 != null)
																																																									{
																																																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rax_v83+1D0]");
																																																										object obj43 = 0;
																																																										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rax_v83+1D0]");
																																																										if ((nint)0 != 0)
																																																										{
																																																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v293 @ rcx_v88+128]");
																																																											if ((nint)0 != 0)
																																																											{
																																																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v293 @ rcx_v88+128]");
																																																												if (((ButtonControl)0).wasPressedThisFrame)
																																																												{
																																																													goto IL_103f;
																																																												}
																																																												goto IL_11a4;
																																																											}
																																																										}
																																																									}
																																																								}
																																																							}
																																																						}
																																																					}
																																																				}
																																																			}
																																																		}
																																																	}
																																																}
																																															}
																																														}
																																													}
																																												}
																																											}
																																										}
																																									}
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_1045;
		IL_1045:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_1039:
		return false;
		IL_103f:
		return true;
	}

	public static bool MouseUp()
	{
		//IL_01c9: Expected I4, but got O
		//IL_013d: Expected O, but got I
		//IL_01ac: Expected O, but got I
		if (Mouse._003Ccurrent_003Ek__BackingField == null)
		{
			return false;
		}
		Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
		if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse._003CleftButton_003Ek__BackingField != null)
		{
			if (mouse._003CleftButton_003Ek__BackingField.wasReleasedThisFrame)
			{
				goto IL_01b5;
			}
			Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
			if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse2._003CmiddleButton_003Ek__BackingField != null)
			{
				if (mouse2._003CmiddleButton_003Ek__BackingField.wasReleasedThisFrame)
				{
					goto IL_01b5;
				}
				Mouse mouse3 = Mouse._003Ccurrent_003Ek__BackingField;
				if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse3._003CrightButton_003Ek__BackingField != null)
				{
					if (mouse3._003CrightButton_003Ek__BackingField.wasReleasedThisFrame)
					{
						goto IL_01b5;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3FA0");
					object obj = default(object);
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ rax_v18+1E8]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v142 @ rax_v18+1E8]");
							if (((ButtonControl)0).wasReleasedThisFrame)
							{
								goto IL_01b5;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3FA0");
							object obj2 = default(object);
							if (obj2 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rax_v20+1E0]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rax_v20+1E0]");
									return ((ButtonControl)0).wasReleasedThisFrame;
								}
							}
						}
					}
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_01b5:
		return true;
	}

	public static bool MouseWheelUsed()
	{
		//IL_0021: Expected O, but got I4
		int frameCount = Time.frameCount;
		object obj = _lastMouseWheelChangeFrame - frameCount;
		return obj == null;
	}

	public static bool SubmitDown()
	{
		//IL_00c1: Expected I4, but got O
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_00fd;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl enterKey = Keyboard._003Ccurrent_003Ek__BackingField.enterKey;
			if (enterKey != null)
			{
				if (enterKey.wasPressedThisFrame)
				{
					goto IL_00a7;
				}
				goto IL_00fd;
			}
		}
		goto IL_00b3;
		IL_00fd:
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
			if (Gamepad._003Ccurrent_003Ek__BackingField == null || gamepad._003CbuttonSouth_003Ek__BackingField == null)
			{
				goto IL_00b3;
			}
			if (gamepad._003CbuttonSouth_003Ek__BackingField.wasPressedThisFrame)
			{
				goto IL_00a7;
			}
		}
		return false;
		IL_00a7:
		return true;
		IL_00b3:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static bool SubmitUp()
	{
		//IL_00c1: Expected I4, but got O
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_00fd;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl enterKey = Keyboard._003Ccurrent_003Ek__BackingField.enterKey;
			if (enterKey != null)
			{
				if (enterKey.wasReleasedThisFrame)
				{
					goto IL_00a7;
				}
				goto IL_00fd;
			}
		}
		goto IL_00b3;
		IL_00fd:
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
			if (Gamepad._003Ccurrent_003Ek__BackingField == null || gamepad._003CbuttonSouth_003Ek__BackingField == null)
			{
				goto IL_00b3;
			}
			if (gamepad._003CbuttonSouth_003Ek__BackingField.wasReleasedThisFrame)
			{
				goto IL_00a7;
			}
		}
		return false;
		IL_00a7:
		return true;
		IL_00b3:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static bool CancelDown()
	{
		//IL_00c1: Expected I4, but got O
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_00fd;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl escapeKey = Keyboard._003Ccurrent_003Ek__BackingField.escapeKey;
			if (escapeKey != null)
			{
				if (escapeKey.wasPressedThisFrame)
				{
					goto IL_00a7;
				}
				goto IL_00fd;
			}
		}
		goto IL_00b3;
		IL_00fd:
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
			if (Gamepad._003Ccurrent_003Ek__BackingField == null || gamepad._003CselectButton_003Ek__BackingField == null)
			{
				goto IL_00b3;
			}
			if (gamepad._003CselectButton_003Ek__BackingField.wasPressedThisFrame)
			{
				goto IL_00a7;
			}
		}
		return false;
		IL_00a7:
		return true;
		IL_00b3:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static bool CancelUp()
	{
		//IL_00c1: Expected I4, but got O
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_00fd;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl escapeKey = Keyboard._003Ccurrent_003Ek__BackingField.escapeKey;
			if (escapeKey != null)
			{
				if (escapeKey.wasReleasedThisFrame)
				{
					goto IL_00a7;
				}
				goto IL_00fd;
			}
		}
		goto IL_00b3;
		IL_00fd:
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
			if (Gamepad._003Ccurrent_003Ek__BackingField == null || gamepad._003CselectButton_003Ek__BackingField == null)
			{
				goto IL_00b3;
			}
			if (gamepad._003CselectButton_003Ek__BackingField.wasReleasedThisFrame)
			{
				goto IL_00a7;
			}
		}
		return false;
		IL_00a7:
		return true;
		IL_00b3:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static bool AnyDirection()
	{
		//IL_0640: Expected I4, but got O
		//IL_027a: Expected O, but got I
		//IL_02d5: Expected O, but got I
		//IL_031e: Expected O, but got I
		//IL_0379: Expected O, but got I
		//IL_03c2: Expected O, but got I
		//IL_041d: Expected O, but got I
		//IL_0466: Expected O, but got I
		//IL_04c1: Expected O, but got I
		//IL_050a: Expected O, but got I
		//IL_0565: Expected O, but got I
		//IL_05ae: Expected O, but got I
		//IL_0609: Expected O, but got I
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_06b8;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl downArrowKey = Keyboard._003Ccurrent_003Ek__BackingField.downArrowKey;
			if (downArrowKey != null)
			{
				if (downArrowKey.isPressed)
				{
					goto IL_0626;
				}
				if (Keyboard._003Ccurrent_003Ek__BackingField != null)
				{
					KeyControl upArrowKey = Keyboard._003Ccurrent_003Ek__BackingField.upArrowKey;
					if (upArrowKey != null)
					{
						if (upArrowKey.isPressed)
						{
							goto IL_0626;
						}
						if (Keyboard._003Ccurrent_003Ek__BackingField != null)
						{
							KeyControl leftArrowKey = Keyboard._003Ccurrent_003Ek__BackingField.leftArrowKey;
							if (leftArrowKey != null)
							{
								if (leftArrowKey.isPressed)
								{
									goto IL_0626;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
								Keyboard keyboard = default(Keyboard);
								if (keyboard != null)
								{
									KeyControl rightArrowKey = keyboard.rightArrowKey;
									if (rightArrowKey != null)
									{
										if (rightArrowKey.isPressed)
										{
											goto IL_0626;
										}
										goto IL_06b8;
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0632;
		IL_06b8:
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_062c;
		}
		Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			StickControl stickControl = gamepad._003CleftStick_003Ek__BackingField;
			if (gamepad._003CleftStick_003Ek__BackingField != null && stickControl._003Cleft_003Ek__BackingField != null)
			{
				if (stickControl._003Cleft_003Ek__BackingField.isPressed)
				{
					goto IL_0626;
				}
				Gamepad gamepad2 = Gamepad._003Ccurrent_003Ek__BackingField;
				if (Gamepad._003Ccurrent_003Ek__BackingField != null)
				{
					StickControl stickControl2 = gamepad2._003CleftStick_003Ek__BackingField;
					if (gamepad2._003CleftStick_003Ek__BackingField != null && stickControl2._003Cright_003Ek__BackingField != null)
					{
						if (stickControl2._003Cright_003Ek__BackingField.isPressed)
						{
							goto IL_0626;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
						object obj = default(object);
						if (obj != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rax_v22+1E8]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rax_v22+1E8]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v499 @ rcx_v15+120]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v499 @ rcx_v15+120]");
									if (((ButtonControl)0).isPressed)
									{
										goto IL_0626;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
									object obj3 = default(object);
									if (obj3 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rax_v24+1E8]");
										object obj4 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rax_v24+1E8]");
										if ((nint)0 != 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v502 @ rcx_v18+128]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v502 @ rcx_v18+128]");
												if (((ButtonControl)0).isPressed)
												{
													goto IL_0626;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
												object obj5 = default(object);
												if (obj5 != null)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v26+1D0]");
													object obj6 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v26+1D0]");
													if ((nint)0 != 0)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ rcx_v21+130]");
														if ((nint)0 != 0)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ rcx_v21+130]");
															if (((ButtonControl)0).isPressed)
															{
																goto IL_0626;
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
															object obj7 = default(object);
															if (obj7 != null)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rax_v28+1D0]");
																object obj8 = 0;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rax_v28+1D0]");
																if ((nint)0 != 0)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v508 @ rcx_v24+138]");
																	if ((nint)0 != 0)
																	{
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v508 @ rcx_v24+138]");
																		if (((ButtonControl)0).isPressed)
																		{
																			goto IL_0626;
																		}
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																		object obj9 = default(object);
																		if (obj9 != null)
																		{
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rax_v30+1D0]");
																			object obj10 = 0;
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rax_v30+1D0]");
																			if ((nint)0 != 0)
																			{
																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v511 @ rcx_v27+120]");
																				if ((nint)0 != 0)
																				{
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v511 @ rcx_v27+120]");
																					if (((ButtonControl)0).isPressed)
																					{
																						goto IL_0626;
																					}
																					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180511390");
																					object obj11 = default(object);
																					if (obj11 != null)
																					{
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rax_v32+1D0]");
																						object obj12 = 0;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rax_v32+1D0]");
																						if ((nint)0 != 0)
																						{
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v514 @ rcx_v30+128]");
																							if ((nint)0 != 0)
																							{
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v514 @ rcx_v30+128]");
																								if (((ButtonControl)0).isPressed)
																								{
																									goto IL_0626;
																								}
																								goto IL_062c;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0632;
		IL_062c:
		return false;
		IL_0632:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0626:
		return true;
	}

	public static bool UpPressed()
	{
		//IL_00f0: Expected I4, but got O
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_012c;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl upArrowKey = Keyboard._003Ccurrent_003Ek__BackingField.upArrowKey;
			if (upArrowKey != null)
			{
				if (upArrowKey.wasPressedThisFrame)
				{
					goto IL_00d6;
				}
				goto IL_012c;
			}
		}
		goto IL_00e2;
		IL_00e2:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_00d6:
		return true;
		IL_00dc:
		return false;
		IL_012c:
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_00dc;
		}
		Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			StickControl stickControl = gamepad._003CleftStick_003Ek__BackingField;
			if (gamepad._003CleftStick_003Ek__BackingField != null && stickControl._003Cup_003Ek__BackingField != null)
			{
				if (stickControl._003Cup_003Ek__BackingField.wasPressedThisFrame)
				{
					goto IL_00d6;
				}
				goto IL_00dc;
			}
		}
		goto IL_00e2;
	}

	public static bool DownPressed()
	{
		//IL_00f0: Expected I4, but got O
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_012c;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl downArrowKey = Keyboard._003Ccurrent_003Ek__BackingField.downArrowKey;
			if (downArrowKey != null)
			{
				if (downArrowKey.wasPressedThisFrame)
				{
					goto IL_00d6;
				}
				goto IL_012c;
			}
		}
		goto IL_00e2;
		IL_00e2:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_00d6:
		return true;
		IL_00dc:
		return false;
		IL_012c:
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_00dc;
		}
		Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			StickControl stickControl = gamepad._003CleftStick_003Ek__BackingField;
			if (gamepad._003CleftStick_003Ek__BackingField != null && stickControl._003Cdown_003Ek__BackingField != null)
			{
				if (stickControl._003Cdown_003Ek__BackingField.wasPressedThisFrame)
				{
					goto IL_00d6;
				}
				goto IL_00dc;
			}
		}
		goto IL_00e2;
	}

	public static bool LeftPressed()
	{
		//IL_00f0: Expected I4, but got O
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_012c;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl leftArrowKey = Keyboard._003Ccurrent_003Ek__BackingField.leftArrowKey;
			if (leftArrowKey != null)
			{
				if (leftArrowKey.wasPressedThisFrame)
				{
					goto IL_00d6;
				}
				goto IL_012c;
			}
		}
		goto IL_00e2;
		IL_00e2:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_00d6:
		return true;
		IL_00dc:
		return false;
		IL_012c:
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_00dc;
		}
		Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			StickControl stickControl = gamepad._003CleftStick_003Ek__BackingField;
			if (gamepad._003CleftStick_003Ek__BackingField != null && stickControl._003Cleft_003Ek__BackingField != null)
			{
				if (stickControl._003Cleft_003Ek__BackingField.wasPressedThisFrame)
				{
					goto IL_00d6;
				}
				goto IL_00dc;
			}
		}
		goto IL_00e2;
	}

	public static bool RightPressed()
	{
		//IL_00f0: Expected I4, but got O
		if (Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_012c;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl rightArrowKey = Keyboard._003Ccurrent_003Ek__BackingField.rightArrowKey;
			if (rightArrowKey != null)
			{
				if (rightArrowKey.wasPressedThisFrame)
				{
					goto IL_00d6;
				}
				goto IL_012c;
			}
		}
		goto IL_00e2;
		IL_00e2:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_00d6:
		return true;
		IL_00dc:
		return false;
		IL_012c:
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_00dc;
		}
		Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			StickControl stickControl = gamepad._003CleftStick_003Ek__BackingField;
			if (gamepad._003CleftStick_003Ek__BackingField != null && stickControl._003Cright_003Ek__BackingField != null)
			{
				if (stickControl._003Cright_003Ek__BackingField.wasPressedThisFrame)
				{
					goto IL_00d6;
				}
				goto IL_00dc;
			}
		}
		goto IL_00e2;
	}

	public static bool LeftMouse()
	{
		//IL_00eb: Expected I4, but got O
		if (Mouse._003Ccurrent_003Ek__BackingField == null)
		{
			return false;
		}
		Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
		if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse._003CleftButton_003Ek__BackingField != null)
		{
			if (mouse._003CleftButton_003Ek__BackingField.wasReleasedThisFrame)
			{
				goto IL_00d7;
			}
			Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
			if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse2._003CleftButton_003Ek__BackingField != null)
			{
				if (mouse2._003CleftButton_003Ek__BackingField.wasPressedThisFrame)
				{
					goto IL_00d7;
				}
				Mouse mouse3 = Mouse._003Ccurrent_003Ek__BackingField;
				if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse3._003CleftButton_003Ek__BackingField != null)
				{
					return mouse3._003CleftButton_003Ek__BackingField.isPressed;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_00d7:
		return true;
	}

	public unsafe static bool GetModifierKeyDown(UniversalKeyCode universalKeyCode)
	{
		//IL_012c: Expected I4, but got O
		//IL_009d: Expected O, but got I
		List<UniversalKeyCode> tmpUniversalKeyResults = _tmpUniversalKeyResults;
		if (_tmpUniversalKeyResults != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				_ = 0;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
					Array.Clear((Array)num, 0, 0);
				}
			}
			List<UniversalKeyCode> modifierKeysDown = GetModifierKeysDown(_tmpUniversalKeyResults);
			List<UniversalKeyCode> tmpUniversalKeyResults2 = _tmpUniversalKeyResults;
			if (_tmpUniversalKeyResults != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rdx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 == 0)
				{
					return false;
				}
				object obj2 = default(object);
				if (_tmpUniversalKeyResults != null)
				{
					return _tmpUniversalKeyResults.Contains((UniversalKeyCode)(int)(&obj2));
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static UniversalKeyCode GetModifierKeyDown()
	{
		//IL_0111: Expected I4, but got O
		//IL_008a: Expected O, but got I
		List<UniversalKeyCode> tmpUniversalKeyResults = _tmpUniversalKeyResults;
		if (_tmpUniversalKeyResults != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
			_ = (nint)0 + (nint)1;
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<UniversalKeyCode>())
			{
				_ = 0;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
					Array.Clear((Array)num, 0, 0);
				}
			}
			List<UniversalKeyCode> modifierKeysDown = GetModifierKeysDown(_tmpUniversalKeyResults);
			List<UniversalKeyCode> tmpUniversalKeyResults2 = _tmpUniversalKeyResults;
			if (_tmpUniversalKeyResults != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rdx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 == 0)
				{
					return UniversalKeyCode.None;
				}
				if (_tmpUniversalKeyResults != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UniversalKeyCode result = default(UniversalKeyCode);
					return result;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (UniversalKeyCode)ex;
	}

	public unsafe static List<UniversalKeyCode> GetModifierKeysDown(List<UniversalKeyCode> results = null)
	{
		//IL_00aa: Expected O, but got I4
		//IL_013d: Expected O, but got I4
		//IL_01d0: Expected O, but got I4
		//IL_0263: Expected O, but got I4
		//IL_02f6: Expected O, but got I4
		//IL_0389: Expected O, but got I4
		//IL_041c: Expected O, but got I4
		//IL_04af: Expected O, but got I4
		bool flag = results != null;
		List<UniversalKeyCode> list = results;
		if (!flag)
		{
			List<UniversalKeyCode> list2 = new List<UniversalKeyCode>();
			list = list2;
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl keyControl = Keyboard._003Ccurrent_003Ek__BackingField.get_Item(Key.LeftShift);
			if (keyControl != null)
			{
				object obj = default(object);
				if (keyControl.wasPressedThisFrame)
				{
					if (list == null)
					{
						goto IL_0543;
					}
					list.Add((UniversalKeyCode)(int)(&obj));
					obj = 218;
				}
				if (Keyboard._003Ccurrent_003Ek__BackingField != null)
				{
					KeyControl keyControl2 = Keyboard._003Ccurrent_003Ek__BackingField.get_Item(Key.RightShift);
					if (keyControl2 != null)
					{
						if (keyControl2.wasPressedThisFrame)
						{
							if (list == null)
							{
								goto IL_0543;
							}
							list.Add((UniversalKeyCode)(int)(&obj));
							obj = 217;
						}
						if (Keyboard._003Ccurrent_003Ek__BackingField != null)
						{
							KeyControl keyControl3 = Keyboard._003Ccurrent_003Ek__BackingField.get_Item(Key.Tab);
							if (keyControl3 != null)
							{
								if (keyControl3.wasPressedThisFrame)
								{
									if (list == null)
									{
										goto IL_0543;
									}
									list.Add((UniversalKeyCode)(int)(&obj));
									obj = 101;
								}
								if (Keyboard._003Ccurrent_003Ek__BackingField != null)
								{
									KeyControl keyControl4 = Keyboard._003Ccurrent_003Ek__BackingField.get_Item(Key.LeftCtrl);
									if (keyControl4 != null)
									{
										if (keyControl4.wasPressedThisFrame)
										{
											if (list == null)
											{
												goto IL_0543;
											}
											list.Add((UniversalKeyCode)(int)(&obj));
											obj = 220;
										}
										if (Keyboard._003Ccurrent_003Ek__BackingField != null)
										{
											KeyControl keyControl5 = Keyboard._003Ccurrent_003Ek__BackingField.get_Item(Key.RightCtrl);
											if (keyControl5 != null)
											{
												if (keyControl5.wasPressedThisFrame)
												{
													if (list == null)
													{
														goto IL_0543;
													}
													list.Add((UniversalKeyCode)(int)(&obj));
													obj = 219;
												}
												if (Keyboard._003Ccurrent_003Ek__BackingField != null)
												{
													KeyControl keyControl6 = Keyboard._003Ccurrent_003Ek__BackingField.get_Item(Key.LeftMeta);
													if (keyControl6 != null)
													{
														if (keyControl6.wasPressedThisFrame)
														{
															if (list == null)
															{
																goto IL_0543;
															}
															list.Add((UniversalKeyCode)(int)(&obj));
															obj = 224;
														}
														if (Keyboard._003Ccurrent_003Ek__BackingField != null)
														{
															KeyControl keyControl7 = Keyboard._003Ccurrent_003Ek__BackingField.get_Item(Key.RightMeta);
															if (keyControl7 != null)
															{
																if (keyControl7.wasPressedThisFrame)
																{
																	if (list == null)
																	{
																		goto IL_0543;
																	}
																	list.Add((UniversalKeyCode)(int)(&obj));
																	obj = 223;
																}
																if (Keyboard._003Ccurrent_003Ek__BackingField != null)
																{
																	KeyControl keyControl8 = Keyboard._003Ccurrent_003Ek__BackingField.get_Item(Key.LeftAlt);
																	if (keyControl8 != null)
																	{
																		if (keyControl8.wasPressedThisFrame)
																		{
																			if (list == null)
																			{
																				goto IL_0543;
																			}
																			list.Add((UniversalKeyCode)(int)(&obj));
																			obj = 222;
																		}
																		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
																		{
																			KeyControl keyControl9 = Keyboard._003Ccurrent_003Ek__BackingField.get_Item(Key.RightAlt);
																			if (keyControl9 != null)
																			{
																				if (keyControl9.wasPressedThisFrame)
																				{
																					if (list == null)
																					{
																						goto IL_0543;
																					}
																					list.Add((UniversalKeyCode)(int)(&obj));
																				}
																				return list;
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0543;
		IL_0543:
		return (List<UniversalKeyCode>)(object)new NullReferenceException();
	}

	public unsafe static bool GetUniversalKeyDown(UniversalKeyCode universalKeyCode)
	{
		//IL_0136: Expected I4, but got O
		//IL_009d: Expected O, but got I
		List<UniversalKeyCode> tmpUniversalKeyResults = _tmpUniversalKeyResults;
		if (_tmpUniversalKeyResults != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				_ = 0;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
					Array.Clear((Array)num, 0, 0);
				}
			}
			List<UniversalKeyCode> universalKeysDown = GetUniversalKeysDown(excludeModifierKeys: false, excludeMouseButtons: false, _tmpUniversalKeyResults);
			List<UniversalKeyCode> tmpUniversalKeyResults2 = _tmpUniversalKeyResults;
			if (_tmpUniversalKeyResults != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rdx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 == 0)
				{
					return false;
				}
				object obj2 = default(object);
				if (_tmpUniversalKeyResults != null)
				{
					return _tmpUniversalKeyResults.Contains((UniversalKeyCode)(int)(&obj2));
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static UniversalKeyCode GetUniversalKeyDown(bool excludeModifierKeys, bool excludeMouseButtons)
	{
		//IL_012c: Expected I4, but got O
		//IL_009d: Expected O, but got I
		List<UniversalKeyCode> tmpUniversalKeyResults = _tmpUniversalKeyResults;
		if (_tmpUniversalKeyResults != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				_ = 0;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
					Array.Clear((Array)num, 0, 0);
				}
			}
			List<UniversalKeyCode> universalKeysDown = GetUniversalKeysDown(excludeModifierKeys, excludeMouseButtons, _tmpUniversalKeyResults);
			List<UniversalKeyCode> tmpUniversalKeyResults2 = _tmpUniversalKeyResults;
			if (_tmpUniversalKeyResults != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rdx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 == 0)
				{
					return UniversalKeyCode.None;
				}
				if (_tmpUniversalKeyResults != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UniversalKeyCode result = default(UniversalKeyCode);
					return result;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (UniversalKeyCode)ex;
	}

	public unsafe static List<UniversalKeyCode> GetUniversalKeysDown(bool excludeModifierKeys, bool excludeMouseButtons, List<UniversalKeyCode> results)
	{
		//IL_003b: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_1ee7: Expected I, but got O
		//IL_1f0f: Expected O, but got I
		//IL_1d03: Expected I, but got O
		//IL_1d34: Expected O, but got I
		//IL_1f8d: Expected I, but got O
		//IL_1fbe: Expected O, but got I
		//IL_0479: Expected O, but got Ref
		//IL_270a: Expected I, but got O
		//IL_2732: Expected O, but got I
		//IL_17e2: Expected O, but got Ref
		//IL_1d50: Expected I, but got O
		//IL_1d81: Expected O, but got I
		//IL_1fda: Expected I, but got O
		//IL_200b: Expected O, but got I
		//IL_1c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c57: Expected O, but got Unknown
		//IL_182e: Expected I, but got O
		//IL_183c: Expected I, but got O
		//IL_184c: Expected O, but got I
		//IL_1888: Expected O, but got I
		//IL_1d9d: Expected I, but got O
		//IL_1dce: Expected O, but got I
		//IL_2027: Expected I, but got O
		//IL_2058: Expected O, but got I
		//IL_1dea: Expected I, but got O
		//IL_1e1b: Expected O, but got I
		//IL_2074: Expected I, but got O
		//IL_20a5: Expected O, but got I
		//IL_20c1: Expected I, but got O
		//IL_20f2: Expected O, but got I
		//IL_033a: Expected I, but got O
		//IL_035b: Invalid comparison between I and F4
		//IL_03ee: Expected I, but got O
		//IL_1e73: Invalid comparison between F4 and I
		//IL_0389: Expected O, but got I
		//IL_210e: Expected I, but got O
		//IL_213f: Expected O, but got I
		//IL_215b: Expected I, but got O
		//IL_218c: Expected O, but got I
		//IL_21a8: Expected I, but got O
		//IL_21d9: Expected O, but got I
		//IL_21f5: Expected I, but got O
		//IL_2226: Expected O, but got I
		//IL_2242: Expected I, but got O
		//IL_2273: Expected O, but got I
		//IL_0c64: Expected O, but got I
		//IL_228f: Expected I, but got O
		//IL_22c0: Expected O, but got I
		//IL_0d32: Expected O, but got I
		//IL_22dc: Expected I, but got O
		//IL_230d: Expected O, but got I
		//IL_0e00: Expected O, but got I
		//IL_2329: Expected I, but got O
		//IL_235a: Expected O, but got I
		//IL_0ece: Expected O, but got I
		//IL_2376: Expected I, but got O
		//IL_23a7: Expected O, but got I
		//IL_23c3: Expected I, but got O
		//IL_23f4: Expected O, but got I
		//IL_1033: Expected O, but got I
		//IL_2410: Expected I, but got O
		//IL_2441: Expected O, but got I
		//IL_1101: Expected O, but got I
		//IL_245d: Expected I, but got O
		//IL_248e: Expected O, but got I
		//IL_11cf: Expected O, but got I
		//IL_24aa: Expected I, but got O
		//IL_24db: Expected O, but got I
		//IL_129d: Expected O, but got I
		//IL_24f7: Expected I, but got O
		//IL_2528: Expected O, but got I
		//IL_2544: Expected I, but got O
		//IL_2575: Expected O, but got I
		//IL_2591: Expected I, but got O
		//IL_25c2: Expected O, but got I
		//IL_1499: Expected O, but got I
		//IL_25de: Expected I, but got O
		//IL_260f: Expected O, but got I
		//IL_1567: Expected O, but got I
		//IL_262b: Expected I, but got O
		//IL_265c: Expected O, but got I
		//IL_1635: Expected O, but got I
		//IL_2678: Expected I, but got O
		//IL_26a9: Expected O, but got I
		//IL_1703: Expected O, but got I
		bool flag = results != null;
		List<UniversalKeyCode> list = results;
		if (!flag)
		{
			List<UniversalKeyCode> list2 = new List<UniversalKeyCode>();
			list = list2;
		}
		if (excludeMouseButtons || Mouse._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_1e8a;
		}
		ButtonControl buttonControl = (ButtonControl)(object)Mouse._003Ccurrent_003Ek__BackingField;
		UniversalKeyCode universalKeyCode = default(UniversalKeyCode);
		if (Mouse._003Ccurrent_003Ek__BackingField != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v600 @ rcx_v4 (UnityEngine.InputSystem.Controls.ButtonControl)+1C8]");
			buttonControl = (ButtonControl)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v600 @ rcx_v4 (UnityEngine.InputSystem.Controls.ButtonControl)+1C8]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v600 @ rcx_v4 (UnityEngine.InputSystem.Controls.ButtonControl)+1C8]");
				if (((ButtonControl)0).wasPressedThisFrame)
				{
					if (list == null)
					{
						goto IL_1c78;
					}
					list.Add((UniversalKeyCode)(int)(&universalKeyCode));
					universalKeyCode = UniversalKeyCode.MouseLeft;
				}
				nint num = (nint)typeof(Mouse);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2153 @ rax_v252 (Il2CppClass<UnityEngine.InputSystem.Mouse>)+B8]");
				nint num2 = 0;
				Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
				bool flag2 = Mouse._003Ccurrent_003Ek__BackingField == null;
				buttonControl = (ButtonControl)num2;
				if (!flag2)
				{
					buttonControl = mouse._003CmiddleButton_003Ek__BackingField;
					if (mouse._003CmiddleButton_003Ek__BackingField != null)
					{
						if (mouse._003CmiddleButton_003Ek__BackingField.wasPressedThisFrame)
						{
							if (list == null)
							{
								goto IL_1c78;
							}
							list.Add((UniversalKeyCode)(int)(&universalKeyCode));
							universalKeyCode = UniversalKeyCode.MouseMiddle;
						}
						nint num3 = (nint)typeof(Mouse);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2291 @ rax_v257 (Il2CppClass<UnityEngine.InputSystem.Mouse>)+B8]");
						nint num4 = 0;
						Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
						bool flag3 = Mouse._003Ccurrent_003Ek__BackingField == null;
						buttonControl = (ButtonControl)num4;
						if (!flag3)
						{
							buttonControl = mouse2._003CrightButton_003Ek__BackingField;
							if (mouse2._003CrightButton_003Ek__BackingField != null)
							{
								if (mouse2._003CrightButton_003Ek__BackingField.wasPressedThisFrame)
								{
									if (list == null)
									{
										goto IL_1c78;
									}
									list.Add((UniversalKeyCode)(int)(&universalKeyCode));
									universalKeyCode = UniversalKeyCode.MouseRight;
								}
								nint num5 = (nint)typeof(Mouse);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2648 @ rax_v262 (Il2CppClass<UnityEngine.InputSystem.Mouse>)+B8]");
								nint num6 = 0;
								Mouse mouse3 = Mouse._003Ccurrent_003Ek__BackingField;
								bool flag4 = Mouse._003Ccurrent_003Ek__BackingField == null;
								buttonControl = (ButtonControl)num6;
								if (!flag4)
								{
									buttonControl = mouse3._003CbackButton_003Ek__BackingField;
									if (mouse3._003CbackButton_003Ek__BackingField != null)
									{
										if (mouse3._003CbackButton_003Ek__BackingField.wasPressedThisFrame)
										{
											if (list == null)
											{
												goto IL_1c78;
											}
											list.Add((UniversalKeyCode)(int)(&universalKeyCode));
											universalKeyCode = UniversalKeyCode.MouseBack;
										}
										nint num7 = (nint)typeof(Mouse);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2767 @ rax_v267 (Il2CppClass<UnityEngine.InputSystem.Mouse>)+B8]");
										nint num8 = 0;
										Mouse mouse4 = Mouse._003Ccurrent_003Ek__BackingField;
										bool flag5 = Mouse._003Ccurrent_003Ek__BackingField == null;
										buttonControl = (ButtonControl)num8;
										if (!flag5)
										{
											buttonControl = mouse4._003CforwardButton_003Ek__BackingField;
											if (mouse4._003CforwardButton_003Ek__BackingField != null)
											{
												if (mouse4._003CforwardButton_003Ek__BackingField.wasPressedThisFrame)
												{
													if (list == null)
													{
														goto IL_1c78;
													}
													list.Add((UniversalKeyCode)(int)(&universalKeyCode));
													universalKeyCode = UniversalKeyCode.MouseForward;
												}
												int frameCount = Time.frameCount;
												if (_lastMouseWheelChangeFrame == frameCount)
												{
													nint num9 = (nint)typeof(InputUtils);
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v534 @ rax_v287 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputUtils>)+B8]");
													nint num10 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v605 @ rcx_v233 (Il2CppStaticFields<Kamgam.UGUIComponentsForSettings.InputUtils>)+8]");
													if (0f > 0.01f)
													{
														bool flag6 = list == null;
														buttonControl = (ButtonControl)num10;
														if (flag6)
														{
															goto IL_1c78;
														}
														list.Add((UniversalKeyCode)(int)(&universalKeyCode));
														universalKeyCode = UniversalKeyCode.MouseWheelUp;
													}
												}
												int frameCount2 = Time.frameCount;
												if (_lastMouseWheelChangeFrame == frameCount2)
												{
													nint num11 = (nint)typeof(InputUtils);
													buttonControl = null;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3105 @ rax_v280 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputUtils>)+B8]");
													nint num12 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v281 (Il2CppStaticFields<Kamgam.UGUIComponentsForSettings.InputUtils>)+8]");
													if (-0.01f > 0f)
													{
														if (list == null)
														{
															goto IL_1c78;
														}
														list.Add((UniversalKeyCode)(int)(&universalKeyCode));
														universalKeyCode = UniversalKeyCode.MouseWheelDown;
													}
												}
												goto IL_1e8a;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_1c78;
		IL_1ecf:
		ButtonControl buttonControl2 = null;
		goto IL_27a4;
		IL_1c78:
		throw new NullReferenceException();
		IL_1c73:
		return list;
		IL_26b7:
		if (Joystick._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_1c73;
		}
		bool flag7 = Joystick._003Ccurrent_003Ek__BackingField == null;
		buttonControl = (ButtonControl)(object)Joystick._003Ccurrent_003Ek__BackingField;
		ButtonControl buttonControl3 = default(ButtonControl);
		if (!flag7)
		{
			if (!Joystick._003Ccurrent_003Ek__BackingField.wasUpdatedThisFrame)
			{
				goto IL_1c73;
			}
			nint num13 = (nint)typeof(Joystick);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v599 @ rax_v24 (Il2CppClass<UnityEngine.InputSystem.Joystick>)+B8]");
			nint num14 = 0;
			bool flag8 = Joystick._003Ccurrent_003Ek__BackingField == null;
			buttonControl = (ButtonControl)num14;
			if (!flag8)
			{
				ReadOnlyArray<InputControl> allControls = ((InputDevice)(&buttonControl3)).allControls;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
				object obj = (object)allControls >> 32;
				bool flag9 = (nint)obj <= 0;
				ButtonControl buttonControl4 = buttonControl2;
				if (flag9)
				{
					goto IL_1c73;
				}
				ButtonControl buttonControl5 = default(ButtonControl);
				object obj4 = default(object);
				while (true)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
					if (buttonControl5 != null)
					{
						nint num15 = (nint)buttonControl5;
						nint num16 = (nint)typeof(ButtonControl);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2557 @ rdx_v17 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v383 @ r9_v7 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
						nint num17 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2557 @ rdx_v17 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
						if (num17 >= 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v383 @ r9_v7 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+C8]");
							object obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2575 @ rax_v32+FFFFFFF8+v2558 @ rax_v31*8]");
							if (0 == (nint)typeof(ButtonControl))
							{
								bool flag10 = buttonControl2 != null;
								buttonControl = buttonControl2;
								if (!flag10)
								{
									buttonControl = buttonControl5;
								}
								if (buttonControl != null && buttonControl.wasPressedThisFrame)
								{
									if (buttonControl4 == null)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton0;
									}
									else if ((nint)buttonControl4 == 1)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton1;
									}
									else if ((nint)buttonControl4 == 2)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton2;
									}
									else if ((nint)buttonControl4 == 3)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton3;
									}
									else if ((nint)buttonControl4 == 4)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton4;
									}
									else if ((nint)buttonControl4 == 5)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton5;
									}
									else if ((nint)buttonControl4 == 6)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton6;
									}
									else if ((nint)buttonControl4 == 7)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton7;
									}
									else if ((nint)buttonControl4 == 8)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton8;
									}
									else if ((nint)buttonControl4 == 9)
									{
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton9;
									}
									else
									{
										if ((nint)buttonControl4 != 10)
										{
											goto IL_1c49;
										}
										if (list == null)
										{
											break;
										}
										universalKeyCode = UniversalKeyCode.JoystickButton10;
									}
									list.Add((UniversalKeyCode)(int)(&universalKeyCode));
								}
							}
						}
					}
					goto IL_1c49;
					IL_1c49:
					buttonControl4 = (ButtonControl)(buttonControl4 + 1);
					if (System.Runtime.CompilerServices.Unsafe.As<ButtonControl, UIntPtr>(ref buttonControl4) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
					{
						continue;
					}
					goto IL_1c73;
				}
			}
		}
		goto IL_1c78;
		IL_27a4:
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			goto IL_26b7;
		}
		bool flag11 = Gamepad._003Ccurrent_003Ek__BackingField == null;
		buttonControl = (ButtonControl)(object)Gamepad._003Ccurrent_003Ek__BackingField;
		if (!flag11)
		{
			if (!Gamepad._003Ccurrent_003Ek__BackingField.wasUpdatedThisFrame)
			{
				goto IL_26b7;
			}
			nint num18 = (nint)typeof(Gamepad);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2228 @ rax_v42 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
			nint num19 = 0;
			Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
			bool flag12 = Gamepad._003Ccurrent_003Ek__BackingField == null;
			buttonControl = (ButtonControl)num19;
			if (!flag12)
			{
				buttonControl = gamepad._003CbuttonSouth_003Ek__BackingField;
				if (gamepad._003CbuttonSouth_003Ek__BackingField != null)
				{
					if (gamepad._003CbuttonSouth_003Ek__BackingField.wasPressedThisFrame)
					{
						if (list == null)
						{
							goto IL_1c78;
						}
						list.Add((UniversalKeyCode)(int)(&universalKeyCode));
						universalKeyCode = UniversalKeyCode.GamePadSouth;
					}
					nint num20 = (nint)typeof(Gamepad);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2477 @ rax_v47 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
					nint num21 = 0;
					Gamepad gamepad2 = Gamepad._003Ccurrent_003Ek__BackingField;
					bool flag13 = Gamepad._003Ccurrent_003Ek__BackingField == null;
					buttonControl = (ButtonControl)num21;
					if (!flag13)
					{
						buttonControl = gamepad2._003CbuttonEast_003Ek__BackingField;
						if (gamepad2._003CbuttonEast_003Ek__BackingField != null)
						{
							if (gamepad2._003CbuttonEast_003Ek__BackingField.wasPressedThisFrame)
							{
								if (list == null)
								{
									goto IL_1c78;
								}
								list.Add((UniversalKeyCode)(int)(&universalKeyCode));
								universalKeyCode = UniversalKeyCode.GamePadEast;
							}
							nint num22 = (nint)typeof(Gamepad);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2722 @ rax_v52 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
							nint num23 = 0;
							Gamepad gamepad3 = Gamepad._003Ccurrent_003Ek__BackingField;
							bool flag14 = Gamepad._003Ccurrent_003Ek__BackingField == null;
							buttonControl = (ButtonControl)num23;
							if (!flag14)
							{
								buttonControl = gamepad3._003CbuttonWest_003Ek__BackingField;
								if (gamepad3._003CbuttonWest_003Ek__BackingField != null)
								{
									if (gamepad3._003CbuttonWest_003Ek__BackingField.wasPressedThisFrame)
									{
										if (list == null)
										{
											goto IL_1c78;
										}
										list.Add((UniversalKeyCode)(int)(&universalKeyCode));
										universalKeyCode = UniversalKeyCode.GamePadWest;
									}
									nint num24 = (nint)typeof(Gamepad);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2829 @ rax_v57 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
									nint num25 = 0;
									Gamepad gamepad4 = Gamepad._003Ccurrent_003Ek__BackingField;
									bool flag15 = Gamepad._003Ccurrent_003Ek__BackingField == null;
									buttonControl = (ButtonControl)num25;
									if (!flag15)
									{
										buttonControl = gamepad4._003CbuttonNorth_003Ek__BackingField;
										if (gamepad4._003CbuttonNorth_003Ek__BackingField != null)
										{
											if (gamepad4._003CbuttonNorth_003Ek__BackingField.wasPressedThisFrame)
											{
												if (list == null)
												{
													goto IL_1c78;
												}
												list.Add((UniversalKeyCode)(int)(&universalKeyCode));
												universalKeyCode = UniversalKeyCode.GamePadNorth;
											}
											nint num26 = (nint)typeof(Gamepad);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3042 @ rax_v62 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
											nint num27 = 0;
											Gamepad gamepad5 = Gamepad._003Ccurrent_003Ek__BackingField;
											bool flag16 = Gamepad._003Ccurrent_003Ek__BackingField == null;
											buttonControl = (ButtonControl)num27;
											if (!flag16)
											{
												buttonControl = gamepad5._003CleftShoulder_003Ek__BackingField;
												if (gamepad5._003CleftShoulder_003Ek__BackingField != null)
												{
													if (gamepad5._003CleftShoulder_003Ek__BackingField.wasPressedThisFrame)
													{
														if (list == null)
														{
															goto IL_1c78;
														}
														list.Add((UniversalKeyCode)(int)(&universalKeyCode));
														universalKeyCode = UniversalKeyCode.GamePadLeftShoulder;
													}
													nint num28 = (nint)typeof(Gamepad);
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3140 @ rax_v67 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
													nint num29 = 0;
													Gamepad gamepad6 = Gamepad._003Ccurrent_003Ek__BackingField;
													bool flag17 = Gamepad._003Ccurrent_003Ek__BackingField == null;
													buttonControl = (ButtonControl)num29;
													if (!flag17)
													{
														buttonControl = gamepad6._003CrightShoulder_003Ek__BackingField;
														if (gamepad6._003CrightShoulder_003Ek__BackingField != null)
														{
															if (gamepad6._003CrightShoulder_003Ek__BackingField.wasPressedThisFrame)
															{
																if (list == null)
																{
																	goto IL_1c78;
																}
																list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																universalKeyCode = UniversalKeyCode.GamePadRightShoulder;
															}
															nint num30 = (nint)typeof(Gamepad);
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3182 @ rax_v72 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
															nint num31 = 0;
															Gamepad gamepad7 = Gamepad._003Ccurrent_003Ek__BackingField;
															bool flag18 = Gamepad._003Ccurrent_003Ek__BackingField == null;
															buttonControl = (ButtonControl)num31;
															if (!flag18)
															{
																buttonControl = gamepad7._003CselectButton_003Ek__BackingField;
																if (gamepad7._003CselectButton_003Ek__BackingField != null)
																{
																	if (gamepad7._003CselectButton_003Ek__BackingField.wasPressedThisFrame)
																	{
																		if (list == null)
																		{
																			goto IL_1c78;
																		}
																		list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																		universalKeyCode = UniversalKeyCode.GamePadSelect;
																	}
																	nint num32 = (nint)typeof(Gamepad);
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3224 @ rax_v77 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																	nint num33 = 0;
																	Gamepad gamepad8 = Gamepad._003Ccurrent_003Ek__BackingField;
																	bool flag19 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																	buttonControl = (ButtonControl)num33;
																	if (!flag19)
																	{
																		buttonControl = gamepad8._003CstartButton_003Ek__BackingField;
																		if (gamepad8._003CstartButton_003Ek__BackingField != null)
																		{
																			if (gamepad8._003CstartButton_003Ek__BackingField.wasPressedThisFrame)
																			{
																				if (list == null)
																				{
																					goto IL_1c78;
																				}
																				list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																				universalKeyCode = UniversalKeyCode.GamePadStart;
																			}
																			nint num34 = (nint)typeof(Gamepad);
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3266 @ rax_v82 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																			nint num35 = 0;
																			Gamepad gamepad9 = Gamepad._003Ccurrent_003Ek__BackingField;
																			bool flag20 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																			buttonControl = (ButtonControl)num35;
																			if (!flag20)
																			{
																				buttonControl = gamepad9._003CleftStickButton_003Ek__BackingField;
																				if (gamepad9._003CleftStickButton_003Ek__BackingField != null)
																				{
																					if (gamepad9._003CleftStickButton_003Ek__BackingField.wasPressedThisFrame)
																					{
																						if (list == null)
																						{
																							goto IL_1c78;
																						}
																						list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																						universalKeyCode = UniversalKeyCode.GamePadLeftStickButton;
																					}
																					nint num36 = (nint)typeof(Gamepad);
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3308 @ rax_v87 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																					nint num37 = 0;
																					Gamepad gamepad10 = Gamepad._003Ccurrent_003Ek__BackingField;
																					bool flag21 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																					buttonControl = (ButtonControl)num37;
																					if (!flag21)
																					{
																						StickControl stickControl = gamepad10._003CleftStick_003Ek__BackingField;
																						bool flag22 = gamepad10._003CleftStick_003Ek__BackingField == null;
																						buttonControl = (ButtonControl)num37;
																						if (!flag22)
																						{
																							buttonControl = stickControl._003Cup_003Ek__BackingField;
																							if (stickControl._003Cup_003Ek__BackingField != null)
																							{
																								if (stickControl._003Cup_003Ek__BackingField.wasPressedThisFrame)
																								{
																									if (list == null)
																									{
																										goto IL_1c78;
																									}
																									list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																									universalKeyCode = UniversalKeyCode.GamePadLeftStickUp;
																								}
																								nint num38 = (nint)typeof(Gamepad);
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3350 @ rax_v93 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																								nint num39 = 0;
																								Gamepad gamepad11 = Gamepad._003Ccurrent_003Ek__BackingField;
																								bool flag23 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																								buttonControl = (ButtonControl)num39;
																								if (!flag23)
																								{
																									StickControl stickControl2 = gamepad11._003CleftStick_003Ek__BackingField;
																									bool flag24 = gamepad11._003CleftStick_003Ek__BackingField == null;
																									buttonControl = (ButtonControl)num39;
																									if (!flag24)
																									{
																										buttonControl = stickControl2._003Cdown_003Ek__BackingField;
																										if (stickControl2._003Cdown_003Ek__BackingField != null)
																										{
																											if (stickControl2._003Cdown_003Ek__BackingField.wasPressedThisFrame)
																											{
																												if (list == null)
																												{
																													goto IL_1c78;
																												}
																												list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																												universalKeyCode = UniversalKeyCode.GamePadLeftStickDown;
																											}
																											nint num40 = (nint)typeof(Gamepad);
																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3392 @ rax_v99 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																											nint num41 = 0;
																											Gamepad gamepad12 = Gamepad._003Ccurrent_003Ek__BackingField;
																											bool flag25 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																											buttonControl = (ButtonControl)num41;
																											if (!flag25)
																											{
																												StickControl stickControl3 = gamepad12._003CleftStick_003Ek__BackingField;
																												bool flag26 = gamepad12._003CleftStick_003Ek__BackingField == null;
																												buttonControl = (ButtonControl)num41;
																												if (!flag26)
																												{
																													buttonControl = stickControl3._003Cleft_003Ek__BackingField;
																													if (stickControl3._003Cleft_003Ek__BackingField != null)
																													{
																														if (stickControl3._003Cleft_003Ek__BackingField.wasPressedThisFrame)
																														{
																															if (list == null)
																															{
																																goto IL_1c78;
																															}
																															list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																															universalKeyCode = UniversalKeyCode.GamePadLeftStickLeft;
																														}
																														nint num42 = (nint)typeof(Gamepad);
																														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3434 @ rax_v105 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																														nint num43 = 0;
																														Gamepad gamepad13 = Gamepad._003Ccurrent_003Ek__BackingField;
																														bool flag27 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																														buttonControl = (ButtonControl)num43;
																														if (!flag27)
																														{
																															StickControl stickControl4 = gamepad13._003CleftStick_003Ek__BackingField;
																															bool flag28 = gamepad13._003CleftStick_003Ek__BackingField == null;
																															buttonControl = (ButtonControl)num43;
																															if (!flag28)
																															{
																																buttonControl = stickControl4._003Cright_003Ek__BackingField;
																																if (stickControl4._003Cright_003Ek__BackingField != null)
																																{
																																	if (stickControl4._003Cright_003Ek__BackingField.wasPressedThisFrame)
																																	{
																																		if (list == null)
																																		{
																																			goto IL_1c78;
																																		}
																																		list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																		universalKeyCode = UniversalKeyCode.GamePadLeftStickRight;
																																	}
																																	nint num44 = (nint)typeof(Gamepad);
																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3476 @ rax_v111 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																	nint num45 = 0;
																																	Gamepad gamepad14 = Gamepad._003Ccurrent_003Ek__BackingField;
																																	bool flag29 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																	buttonControl = (ButtonControl)num45;
																																	if (!flag29)
																																	{
																																		buttonControl = gamepad14._003CrightStickButton_003Ek__BackingField;
																																		if (gamepad14._003CrightStickButton_003Ek__BackingField != null)
																																		{
																																			if (gamepad14._003CrightStickButton_003Ek__BackingField.wasPressedThisFrame)
																																			{
																																				if (list == null)
																																				{
																																					goto IL_1c78;
																																				}
																																				list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																				universalKeyCode = UniversalKeyCode.GamePadRightStickButton;
																																			}
																																			nint num46 = (nint)typeof(Gamepad);
																																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3518 @ rax_v116 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																			nint num47 = 0;
																																			Gamepad gamepad15 = Gamepad._003Ccurrent_003Ek__BackingField;
																																			bool flag30 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																			buttonControl = (ButtonControl)num47;
																																			if (!flag30)
																																			{
																																				StickControl stickControl5 = gamepad15._003CrightStick_003Ek__BackingField;
																																				bool flag31 = gamepad15._003CrightStick_003Ek__BackingField == null;
																																				buttonControl = (ButtonControl)num47;
																																				if (!flag31)
																																				{
																																					buttonControl = stickControl5._003Cup_003Ek__BackingField;
																																					if (stickControl5._003Cup_003Ek__BackingField != null)
																																					{
																																						if (stickControl5._003Cup_003Ek__BackingField.wasPressedThisFrame)
																																						{
																																							if (list == null)
																																							{
																																								goto IL_1c78;
																																							}
																																							list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																							universalKeyCode = UniversalKeyCode.GamePadRightStickUp;
																																						}
																																						nint num48 = (nint)typeof(Gamepad);
																																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3560 @ rax_v122 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																						nint num49 = 0;
																																						Gamepad gamepad16 = Gamepad._003Ccurrent_003Ek__BackingField;
																																						bool flag32 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																						buttonControl = (ButtonControl)num49;
																																						if (!flag32)
																																						{
																																							StickControl stickControl6 = gamepad16._003CrightStick_003Ek__BackingField;
																																							bool flag33 = gamepad16._003CrightStick_003Ek__BackingField == null;
																																							buttonControl = (ButtonControl)num49;
																																							if (!flag33)
																																							{
																																								buttonControl = stickControl6._003Cdown_003Ek__BackingField;
																																								if (stickControl6._003Cdown_003Ek__BackingField != null)
																																								{
																																									if (stickControl6._003Cdown_003Ek__BackingField.wasPressedThisFrame)
																																									{
																																										if (list == null)
																																										{
																																											goto IL_1c78;
																																										}
																																										list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																										universalKeyCode = UniversalKeyCode.GamePadRightStickDown;
																																									}
																																									nint num50 = (nint)typeof(Gamepad);
																																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3602 @ rax_v128 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																									nint num51 = 0;
																																									Gamepad gamepad17 = Gamepad._003Ccurrent_003Ek__BackingField;
																																									bool flag34 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																									buttonControl = (ButtonControl)num51;
																																									if (!flag34)
																																									{
																																										StickControl stickControl7 = gamepad17._003CrightStick_003Ek__BackingField;
																																										bool flag35 = gamepad17._003CrightStick_003Ek__BackingField == null;
																																										buttonControl = (ButtonControl)num51;
																																										if (!flag35)
																																										{
																																											buttonControl = stickControl7._003Cleft_003Ek__BackingField;
																																											if (stickControl7._003Cleft_003Ek__BackingField != null)
																																											{
																																												if (stickControl7._003Cleft_003Ek__BackingField.wasPressedThisFrame)
																																												{
																																													if (list == null)
																																													{
																																														goto IL_1c78;
																																													}
																																													list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																													universalKeyCode = UniversalKeyCode.GamePadRightStickLeft;
																																												}
																																												nint num52 = (nint)typeof(Gamepad);
																																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3644 @ rax_v134 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																												nint num53 = 0;
																																												Gamepad gamepad18 = Gamepad._003Ccurrent_003Ek__BackingField;
																																												bool flag36 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																												buttonControl = (ButtonControl)num53;
																																												if (!flag36)
																																												{
																																													StickControl stickControl8 = gamepad18._003CrightStick_003Ek__BackingField;
																																													bool flag37 = gamepad18._003CrightStick_003Ek__BackingField == null;
																																													buttonControl = (ButtonControl)num53;
																																													if (!flag37)
																																													{
																																														buttonControl = stickControl8._003Cright_003Ek__BackingField;
																																														if (stickControl8._003Cright_003Ek__BackingField != null)
																																														{
																																															if (stickControl8._003Cright_003Ek__BackingField.wasPressedThisFrame)
																																															{
																																																if (list == null)
																																																{
																																																	goto IL_1c78;
																																																}
																																																list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																																universalKeyCode = UniversalKeyCode.GamePadRightStickRight;
																																															}
																																															nint num54 = (nint)typeof(Gamepad);
																																															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3686 @ rax_v140 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																															nint num55 = 0;
																																															Gamepad gamepad19 = Gamepad._003Ccurrent_003Ek__BackingField;
																																															bool flag38 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																															buttonControl = (ButtonControl)num55;
																																															if (!flag38)
																																															{
																																																buttonControl = gamepad19._003CleftTrigger_003Ek__BackingField;
																																																if (gamepad19._003CleftTrigger_003Ek__BackingField != null)
																																																{
																																																	if (gamepad19._003CleftTrigger_003Ek__BackingField.wasPressedThisFrame)
																																																	{
																																																		if (list == null)
																																																		{
																																																			goto IL_1c78;
																																																		}
																																																		list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																																		universalKeyCode = UniversalKeyCode.GamePadLeftTrigger;
																																																	}
																																																	nint num56 = (nint)typeof(Gamepad);
																																																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3728 @ rax_v145 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																																	nint num57 = 0;
																																																	Gamepad gamepad20 = Gamepad._003Ccurrent_003Ek__BackingField;
																																																	bool flag39 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																																	buttonControl = (ButtonControl)num57;
																																																	if (!flag39)
																																																	{
																																																		buttonControl = gamepad20._003CrightTrigger_003Ek__BackingField;
																																																		if (gamepad20._003CrightTrigger_003Ek__BackingField != null)
																																																		{
																																																			if (gamepad20._003CrightTrigger_003Ek__BackingField.wasPressedThisFrame)
																																																			{
																																																				if (list == null)
																																																				{
																																																					goto IL_1c78;
																																																				}
																																																				list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																																				universalKeyCode = UniversalKeyCode.GamePadRightTrigger;
																																																			}
																																																			nint num58 = (nint)typeof(Gamepad);
																																																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3770 @ rax_v150 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																																			nint num59 = 0;
																																																			Gamepad gamepad21 = Gamepad._003Ccurrent_003Ek__BackingField;
																																																			bool flag40 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																																			buttonControl = (ButtonControl)num59;
																																																			if (!flag40)
																																																			{
																																																				DpadControl dpadControl = gamepad21._003Cdpad_003Ek__BackingField;
																																																				bool flag41 = gamepad21._003Cdpad_003Ek__BackingField == null;
																																																				buttonControl = (ButtonControl)num59;
																																																				if (!flag41)
																																																				{
																																																					buttonControl = dpadControl._003Cleft_003Ek__BackingField;
																																																					if (dpadControl._003Cleft_003Ek__BackingField != null)
																																																					{
																																																						if (dpadControl._003Cleft_003Ek__BackingField.wasPressedThisFrame)
																																																						{
																																																							if (list == null)
																																																							{
																																																								goto IL_1c78;
																																																							}
																																																							list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																																							universalKeyCode = UniversalKeyCode.GamePadDPadLeft;
																																																						}
																																																						nint num60 = (nint)typeof(Gamepad);
																																																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3812 @ rax_v156 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																																						nint num61 = 0;
																																																						Gamepad gamepad22 = Gamepad._003Ccurrent_003Ek__BackingField;
																																																						bool flag42 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																																						buttonControl = (ButtonControl)num61;
																																																						if (!flag42)
																																																						{
																																																							DpadControl dpadControl2 = gamepad22._003Cdpad_003Ek__BackingField;
																																																							bool flag43 = gamepad22._003Cdpad_003Ek__BackingField == null;
																																																							buttonControl = (ButtonControl)num61;
																																																							if (!flag43)
																																																							{
																																																								buttonControl = dpadControl2._003Cright_003Ek__BackingField;
																																																								if (dpadControl2._003Cright_003Ek__BackingField != null)
																																																								{
																																																									if (dpadControl2._003Cright_003Ek__BackingField.wasPressedThisFrame)
																																																									{
																																																										if (list == null)
																																																										{
																																																											goto IL_1c78;
																																																										}
																																																										list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																																										universalKeyCode = UniversalKeyCode.GamePadDPadRight;
																																																									}
																																																									nint num62 = (nint)typeof(Gamepad);
																																																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3854 @ rax_v162 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																																									nint num63 = 0;
																																																									Gamepad gamepad23 = Gamepad._003Ccurrent_003Ek__BackingField;
																																																									bool flag44 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																																									buttonControl = (ButtonControl)num63;
																																																									if (!flag44)
																																																									{
																																																										DpadControl dpadControl3 = gamepad23._003Cdpad_003Ek__BackingField;
																																																										bool flag45 = gamepad23._003Cdpad_003Ek__BackingField == null;
																																																										buttonControl = (ButtonControl)num63;
																																																										if (!flag45)
																																																										{
																																																											buttonControl = dpadControl3._003Cup_003Ek__BackingField;
																																																											if (dpadControl3._003Cup_003Ek__BackingField != null)
																																																											{
																																																												if (dpadControl3._003Cup_003Ek__BackingField.wasPressedThisFrame)
																																																												{
																																																													if (list == null)
																																																													{
																																																														goto IL_1c78;
																																																													}
																																																													list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																																													universalKeyCode = UniversalKeyCode.GamePadDPadUp;
																																																												}
																																																												nint num64 = (nint)typeof(Gamepad);
																																																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3896 @ rax_v168 (Il2CppClass<UnityEngine.InputSystem.Gamepad>)+B8]");
																																																												nint num65 = 0;
																																																												Gamepad gamepad24 = Gamepad._003Ccurrent_003Ek__BackingField;
																																																												bool flag46 = Gamepad._003Ccurrent_003Ek__BackingField == null;
																																																												buttonControl = (ButtonControl)num65;
																																																												if (!flag46)
																																																												{
																																																													DpadControl dpadControl4 = gamepad24._003Cdpad_003Ek__BackingField;
																																																													bool flag47 = gamepad24._003Cdpad_003Ek__BackingField == null;
																																																													buttonControl = (ButtonControl)num65;
																																																													if (!flag47)
																																																													{
																																																														buttonControl = dpadControl4._003Cdown_003Ek__BackingField;
																																																														if (dpadControl4._003Cdown_003Ek__BackingField != null)
																																																														{
																																																															if (dpadControl4._003Cdown_003Ek__BackingField.wasPressedThisFrame)
																																																															{
																																																																if (list == null)
																																																																{
																																																																	goto IL_1c78;
																																																																}
																																																																list.Add((UniversalKeyCode)(int)(&universalKeyCode));
																																																																universalKeyCode = UniversalKeyCode.GamePadDPadDown;
																																																															}
																																																															goto IL_26b7;
																																																														}
																																																													}
																																																												}
																																																											}
																																																										}
																																																									}
																																																								}
																																																							}
																																																						}
																																																					}
																																																				}
																																																			}
																																																		}
																																																	}
																																																}
																																															}
																																														}
																																													}
																																												}
																																											}
																																										}
																																									}
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_1c78;
		IL_1e8a:
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			bool flag48 = Keyboard._003Ccurrent_003Ek__BackingField == null;
			buttonControl = (ButtonControl)(object)Keyboard._003Ccurrent_003Ek__BackingField;
			if (!flag48)
			{
				if (!Keyboard._003Ccurrent_003Ek__BackingField.wasUpdatedThisFrame)
				{
					goto IL_1ecf;
				}
				buildKeyCache();
				nint num66 = (nint)typeof(Keyboard);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v584 @ rax_v229 (Il2CppClass<UnityEngine.InputSystem.Keyboard>)+B8]");
				nint num67 = 0;
				bool flag49 = Keyboard._003Ccurrent_003Ek__BackingField == null;
				buttonControl = (ButtonControl)num67;
				if (!flag49)
				{
					ReadOnlyArray<KeyControl> allKeys = ((Keyboard)(&buttonControl3)).allKeys;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA390");
					ReadOnlyArray<KeyControl>.Enumerator enumerator = default(ReadOnlyArray<KeyControl>.Enumerator);
					ButtonControl buttonControl6 = default(ButtonControl);
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18084D310");
						if (buttonControl6 == null)
						{
							continue;
						}
						if (excludeModifierKeys)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							if ((nint)0 == 51)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							if ((nint)0 == 52)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							if ((nint)0 == 3)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							if ((nint)0 == 55)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							if ((nint)0 == 56)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							if ((nint)0 == 57)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							if ((nint)0 == 58)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							if ((nint)0 == 53)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							if ((nint)0 == 54)
							{
								continue;
							}
						}
						if (buttonControl6.wasPressedThisFrame)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ stack_-78 (UnityEngine.InputSystem.Controls.ButtonControl)+148]");
							UniversalKeyCode universalKeyCode2 = KeyToUniversalKeyCode(Key.None);
							if (list == null)
							{
								throw new NullReferenceException();
							}
							list.Add((UniversalKeyCode)(int)(&universalKeyCode));
							universalKeyCode = universalKeyCode2;
						}
					}
					enumerator.Dispose();
					buttonControl2 = null;
					buttonControl3 = null;
					goto IL_27a4;
				}
			}
			goto IL_1c78;
		}
		goto IL_1ecf;
	}

	public unsafe static bool GetUniversalKeyUp(UniversalKeyCode universalKeyCode)
	{
		//IL_0136: Expected I4, but got O
		//IL_009d: Expected O, but got I
		List<UniversalKeyCode> tmpUniversalKeyResults = _tmpUniversalKeyResults;
		if (_tmpUniversalKeyResults != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				_ = 0;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
					Array.Clear((Array)num, 0, 0);
				}
			}
			List<UniversalKeyCode> universalKeysUp = GetUniversalKeysUp(excludeModifierKeys: false, excludeMouseButtons: false, _tmpUniversalKeyResults);
			List<UniversalKeyCode> tmpUniversalKeyResults2 = _tmpUniversalKeyResults;
			if (_tmpUniversalKeyResults != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rdx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 == 0)
				{
					return false;
				}
				object obj2 = default(object);
				if (_tmpUniversalKeyResults != null)
				{
					return _tmpUniversalKeyResults.Contains((UniversalKeyCode)(int)(&obj2));
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static UniversalKeyCode GetUniversalKeyUp(bool excludeModifierKeys, bool excludeMouseButtons)
	{
		//IL_012c: Expected I4, but got O
		//IL_009d: Expected O, but got I
		List<UniversalKeyCode> tmpUniversalKeyResults = _tmpUniversalKeyResults;
		if (_tmpUniversalKeyResults != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				_ = 0;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
					Array.Clear((Array)num, 0, 0);
				}
			}
			List<UniversalKeyCode> universalKeysUp = GetUniversalKeysUp(excludeModifierKeys, excludeMouseButtons, _tmpUniversalKeyResults);
			List<UniversalKeyCode> tmpUniversalKeyResults2 = _tmpUniversalKeyResults;
			if (_tmpUniversalKeyResults != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rdx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 == 0)
				{
					return UniversalKeyCode.None;
				}
				if (_tmpUniversalKeyResults != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UniversalKeyCode result = default(UniversalKeyCode);
					return result;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (UniversalKeyCode)ex;
	}

	public static List<UniversalKeyCode> GetUniversalKeysUp(bool excludeModifierKeys, bool excludeMouseButtons, List<UniversalKeyCode> results)
	{
		//IL_0025: Expected F4, but got I4
		//IL_002d: Expected I, but got O
		//IL_1482: Expected F4, but got I4
		//IL_148a: Expected I, but got O
		//IL_006a: Expected I, but got O
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Expected O, but got Unknown
		//IL_03a8: Expected O, but got I4
		//IL_03b1: Expected O, but got I4
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected I4, but got Unknown
		//IL_10f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f6: Expected O, but got Unknown
		//IL_113b: Expected O, but got I4
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected I4, but got Unknown
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Expected I4, but got Unknown
		//IL_175b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1760: Expected O, but got Unknown
		//IL_1769: Unknown result type (might be due to invalid IL or missing references)
		//IL_176e: Expected O, but got Unknown
		//IL_1788: Expected O, but got I
		//IL_140d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1412: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected I4, but got Unknown
		//IL_1157: Expected I, but got O
		//IL_115f: Expected I, but got O
		//IL_116f: Expected O, but got I
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Expected I4, but got Unknown
		//IL_06e1: Expected I4, but got O
		//IL_0707: Expected I, but got O
		//IL_11ab: Expected O, but got I
		//IL_0769: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Expected O, but got Unknown
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Expected O, but got Unknown
		//IL_0428: Expected I, but got O
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected I4, but got Unknown
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Expected I4, but got Unknown
		//IL_072c: Expected I4, but got O
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Expected I4, but got Unknown
		//IL_1502: Expected F4, but got I4
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected I4, but got Unknown
		//IL_120e: Expected O, but got I
		//IL_0905: Unknown result type (might be due to invalid IL or missing references)
		//IL_090a: Expected I4, but got Unknown
		//IL_0477: Expected I, but got O
		//IL_024c: Expected I, but got O
		//IL_026c: Expected F4, but got I
		//IL_027d: Invalid comparison between I and F4
		//IL_095b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0960: Expected I4, but got Unknown
		//IL_0304: Expected I, but got O
		//IL_04c6: Expected I, but got O
		//IL_1539: Invalid comparison between F4 and I
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Expected I4, but got Unknown
		//IL_173b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1740: Expected I4, but got Unknown
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b6: Expected I4, but got Unknown
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Expected I4, but got Unknown
		//IL_0515: Expected I, but got O
		//IL_0a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0c: Expected I4, but got Unknown
		//IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a62: Expected I4, but got Unknown
		//IL_0564: Expected I, but got O
		//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab8: Expected I4, but got Unknown
		//IL_05b3: Expected I, but got O
		//IL_0b1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b20: Expected I4, but got Unknown
		//IL_0602: Expected I, but got O
		//IL_0b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b88: Expected I4, but got Unknown
		//IL_0651: Expected I, but got O
		//IL_0beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf0: Expected I4, but got Unknown
		//IL_06a0: Expected I, but got O
		//IL_06ae: Expected I, but got O
		//IL_0c53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c58: Expected I4, but got Unknown
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cae: Expected I4, but got Unknown
		//IL_0d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d16: Expected I4, but got Unknown
		//IL_0d79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7e: Expected I4, but got Unknown
		//IL_0de1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de6: Expected I4, but got Unknown
		//IL_0e49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4e: Expected I4, but got Unknown
		//IL_0e9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea4: Expected I4, but got Unknown
		//IL_0ef5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efa: Expected I4, but got Unknown
		//IL_0f5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f62: Expected I4, but got Unknown
		//IL_0fc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fca: Expected I4, but got Unknown
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1032: Expected I4, but got Unknown
		//IL_1095: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Expected I4, but got Unknown
		_ = 0;
		bool flag = results != null;
		List<UniversalKeyCode> list = results;
		if (!flag)
		{
			List<UniversalKeyCode> list2 = new List<UniversalKeyCode>();
			list = list2;
		}
		float num = 0f;
		nint num2 = (nint)results;
		object obj = default(object);
		if (!excludeMouseButtons)
		{
			bool flag2 = Mouse._003Ccurrent_003Ek__BackingField == null;
			num = 0f;
			num2 = (nint)results;
			if (!flag2)
			{
				Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
				bool wasReleasedThisFrame = mouse._003CleftButton_003Ek__BackingField.wasReleasedThisFrame;
				bool flag3 = !wasReleasedThisFrame;
				nint num3 = (nint)results;
				if (!flag3)
				{
					UniversalKeyCode item = (UniversalKeyCode)(obj + 48);
					_ = 10;
					list.Add(item);
					num3 = 0;
				}
				Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
				bool wasReleasedThisFrame2 = mouse2._003CmiddleButton_003Ek__BackingField.wasReleasedThisFrame;
				bool flag4 = !wasReleasedThisFrame2;
				nint num4 = num3;
				if (!flag4)
				{
					UniversalKeyCode item2 = (UniversalKeyCode)(obj + 48);
					_ = 11;
					list.Add(item2);
					num4 = 0;
				}
				Mouse mouse3 = Mouse._003Ccurrent_003Ek__BackingField;
				bool wasReleasedThisFrame3 = mouse3._003CrightButton_003Ek__BackingField.wasReleasedThisFrame;
				bool flag5 = !wasReleasedThisFrame3;
				nint num5 = num4;
				if (!flag5)
				{
					UniversalKeyCode item3 = (UniversalKeyCode)(obj + 48);
					_ = 12;
					list.Add(item3);
					num5 = 0;
				}
				Mouse mouse4 = Mouse._003Ccurrent_003Ek__BackingField;
				bool wasReleasedThisFrame4 = mouse4._003CbackButton_003Ek__BackingField.wasReleasedThisFrame;
				bool flag6 = !wasReleasedThisFrame4;
				nint num6 = num5;
				if (!flag6)
				{
					UniversalKeyCode item4 = (UniversalKeyCode)(obj + 48);
					_ = 14;
					list.Add(item4);
					num6 = 0;
				}
				Mouse mouse5 = Mouse._003Ccurrent_003Ek__BackingField;
				bool wasReleasedThisFrame5 = mouse5._003CforwardButton_003Ek__BackingField.wasReleasedThisFrame;
				bool flag7 = !wasReleasedThisFrame5;
				nint num7 = num6;
				if (!flag7)
				{
					UniversalKeyCode item5 = (UniversalKeyCode)(obj + 48);
					_ = 13;
					list.Add(item5);
					num7 = 0;
				}
				int frameCount = Time.frameCount;
				bool flag8 = _lastMouseWheelChangeFrame != frameCount;
				num = 0f;
				num2 = num7;
				if (!flag8)
				{
					nint num8 = (nint)typeof(InputUtils);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v504 @ rax_v310 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputUtils>)+B8]");
					nint num9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v596 @ rcx_v254 (Il2CppStaticFields<Kamgam.UGUIComponentsForSettings.InputUtils>)+8]");
					num = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v596 @ rcx_v254 (Il2CppStaticFields<Kamgam.UGUIComponentsForSettings.InputUtils>)+8]");
					bool flag9 = !(0f > 0.01f);
					num2 = num7;
					if (!flag9)
					{
						UniversalKeyCode item6 = (UniversalKeyCode)(obj + 48);
						_ = 17;
						list.Add(item6);
						num2 = 0;
					}
				}
				int frameCount2 = Time.frameCount;
				if (_lastMouseWheelChangeFrame == frameCount2)
				{
					nint num10 = (nint)typeof(InputUtils);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3262 @ rax_v303 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputUtils>)+B8]");
					nint num11 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rax_v304 (Il2CppStaticFields<Kamgam.UGUIComponentsForSettings.InputUtils>)+8]");
					bool flag10 = !(-0.01f > 0f);
					num = -0.01f;
					if (!flag10)
					{
						UniversalKeyCode item7 = (UniversalKeyCode)(obj + 48);
						_ = 18;
						list.Add(item7);
						num = -0.01f;
						num2 = 0;
					}
				}
			}
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null && Keyboard._003Ccurrent_003Ek__BackingField.wasUpdatedThisFrame)
		{
			buildKeyCache();
			Key[] keys = Keys;
			object obj2 = Keys + 32;
			UniversalKeyCode universalKeyCode = UniversalKeyCode.None;
			object obj3 = 0;
			nint num12;
			UniversalKeyCode universalKeyCode2;
			Keyboard keyboard = default(Keyboard);
			Keyboard keyboard2 = default(Keyboard);
			Keyboard keyboard3 = default(Keyboard);
			Keyboard keyboard4 = default(Keyboard);
			Keyboard keyboard5 = default(Keyboard);
			Keyboard keyboard6 = default(Keyboard);
			Keyboard keyboard7 = default(Keyboard);
			Keyboard keyboard8 = default(Keyboard);
			Keyboard keyboard9 = default(Keyboard);
			Keyboard keyboard10 = default(Keyboard);
			for (object obj4 = 0; (nint)obj3 < keys.Length; obj4++, obj2 += 4, num2 = num12, universalKeyCode = universalKeyCode2, obj3 = obj4)
			{
				if ((nint)obj4 < keys.Length)
				{
					if (excludeModifierKeys)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl = keyboard.get_Item(Key.LeftShift);
						bool wasReleasedThisFrame6 = keyControl.wasReleasedThisFrame;
						num12 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (wasReleasedThisFrame6)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl2 = keyboard2.get_Item(Key.RightShift);
						bool wasReleasedThisFrame7 = keyControl2.wasReleasedThisFrame;
						num12 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (wasReleasedThisFrame7)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl3 = keyboard3.get_Item(Key.Tab);
						bool wasReleasedThisFrame8 = keyControl3.wasReleasedThisFrame;
						num12 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (wasReleasedThisFrame8)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl4 = keyboard4.get_Item(Key.LeftCtrl);
						bool wasReleasedThisFrame9 = keyControl4.wasReleasedThisFrame;
						num12 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (wasReleasedThisFrame9)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl5 = keyboard5.get_Item(Key.RightCtrl);
						bool wasReleasedThisFrame10 = keyControl5.wasReleasedThisFrame;
						num12 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (wasReleasedThisFrame10)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl6 = keyboard6.get_Item(Key.LeftMeta);
						bool wasReleasedThisFrame11 = keyControl6.wasReleasedThisFrame;
						num12 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (wasReleasedThisFrame11)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl7 = keyboard7.get_Item(Key.RightMeta);
						bool wasReleasedThisFrame12 = keyControl7.wasReleasedThisFrame;
						num12 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (wasReleasedThisFrame12)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl8 = keyboard8.get_Item(Key.LeftAlt);
						bool wasReleasedThisFrame13 = keyControl8.wasReleasedThisFrame;
						num12 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (wasReleasedThisFrame13)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl9 = keyboard9.get_Item(Key.RightAlt);
						bool wasReleasedThisFrame14 = keyControl9.wasReleasedThisFrame;
						num2 = unchecked((nint)null);
						universalKeyCode = UniversalKeyCode.None;
						num12 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (wasReleasedThisFrame14)
						{
							continue;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
					KeyControl keyControl10 = keyboard10.get_Item((Key)obj2);
					bool wasReleasedThisFrame15 = keyControl10.wasReleasedThisFrame;
					bool flag11 = !wasReleasedThisFrame15;
					num12 = unchecked((nint)null);
					universalKeyCode2 = UniversalKeyCode.None;
					if (!flag11)
					{
						UniversalKeyCode universalKeyCode3 = KeyToUniversalKeyCode((Key)obj2);
						universalKeyCode2 = (UniversalKeyCode)(obj + 48);
						list.Add(universalKeyCode2);
						num12 = 0;
					}
					continue;
				}
				return (List<UniversalKeyCode>)(object)new IndexOutOfRangeException();
			}
		}
		if (Gamepad._003Ccurrent_003Ek__BackingField != null && Gamepad._003Ccurrent_003Ek__BackingField.wasUpdatedThisFrame)
		{
			Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad._003CbuttonSouth_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item8 = (UniversalKeyCode)(obj + 48);
				_ = 702;
				list.Add(item8);
			}
			Gamepad gamepad2 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad2._003CbuttonEast_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item9 = (UniversalKeyCode)(obj + 48);
				_ = 704;
				list.Add(item9);
			}
			Gamepad gamepad3 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad3._003CbuttonWest_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item10 = (UniversalKeyCode)(obj + 48);
				_ = 703;
				list.Add(item10);
			}
			Gamepad gamepad4 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad4._003CbuttonNorth_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item11 = (UniversalKeyCode)(obj + 48);
				_ = 701;
				list.Add(item11);
			}
			Gamepad gamepad5 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad5._003CleftShoulder_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item12 = (UniversalKeyCode)(obj + 48);
				_ = 707;
				list.Add(item12);
			}
			Gamepad gamepad6 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad6._003CrightShoulder_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item13 = (UniversalKeyCode)(obj + 48);
				_ = 708;
				list.Add(item13);
			}
			Gamepad gamepad7 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad7._003CselectButton_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item14 = (UniversalKeyCode)(obj + 48);
				_ = 706;
				list.Add(item14);
			}
			Gamepad gamepad8 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad8._003CstartButton_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item15 = (UniversalKeyCode)(obj + 48);
				_ = 705;
				list.Add(item15);
			}
			Gamepad gamepad9 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad9._003CleftStickButton_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item16 = (UniversalKeyCode)(obj + 48);
				_ = 715;
				list.Add(item16);
			}
			Gamepad gamepad10 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl = gamepad10._003CleftStick_003Ek__BackingField;
			if (stickControl._003Cup_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item17 = (UniversalKeyCode)(obj + 48);
				_ = 717;
				list.Add(item17);
			}
			Gamepad gamepad11 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl2 = gamepad11._003CleftStick_003Ek__BackingField;
			if (stickControl2._003Cdown_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item18 = (UniversalKeyCode)(obj + 48);
				_ = 718;
				list.Add(item18);
			}
			Gamepad gamepad12 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl3 = gamepad12._003CleftStick_003Ek__BackingField;
			if (stickControl3._003Cleft_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item19 = (UniversalKeyCode)(obj + 48);
				_ = 719;
				list.Add(item19);
			}
			Gamepad gamepad13 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl4 = gamepad13._003CleftStick_003Ek__BackingField;
			if (stickControl4._003Cright_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item20 = (UniversalKeyCode)(obj + 48);
				_ = 720;
				list.Add(item20);
			}
			Gamepad gamepad14 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad14._003CrightStickButton_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item21 = (UniversalKeyCode)(obj + 48);
				_ = 716;
				list.Add(item21);
			}
			Gamepad gamepad15 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl5 = gamepad15._003CrightStick_003Ek__BackingField;
			if (stickControl5._003Cup_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item22 = (UniversalKeyCode)(obj + 48);
				_ = 721;
				list.Add(item22);
			}
			Gamepad gamepad16 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl6 = gamepad16._003CrightStick_003Ek__BackingField;
			if (stickControl6._003Cdown_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item23 = (UniversalKeyCode)(obj + 48);
				_ = 722;
				list.Add(item23);
			}
			Gamepad gamepad17 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl7 = gamepad17._003CrightStick_003Ek__BackingField;
			if (stickControl7._003Cleft_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item24 = (UniversalKeyCode)(obj + 48);
				_ = 723;
				list.Add(item24);
			}
			Gamepad gamepad18 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl8 = gamepad18._003CrightStick_003Ek__BackingField;
			if (stickControl8._003Cright_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item25 = (UniversalKeyCode)(obj + 48);
				_ = 724;
				list.Add(item25);
			}
			Gamepad gamepad19 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad19._003CleftTrigger_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item26 = (UniversalKeyCode)(obj + 48);
				_ = 709;
				list.Add(item26);
			}
			Gamepad gamepad20 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad20._003CrightTrigger_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item27 = (UniversalKeyCode)(obj + 48);
				_ = 710;
				list.Add(item27);
			}
			Gamepad gamepad21 = Gamepad._003Ccurrent_003Ek__BackingField;
			DpadControl dpadControl = gamepad21._003Cdpad_003Ek__BackingField;
			if (dpadControl._003Cleft_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item28 = (UniversalKeyCode)(obj + 48);
				_ = 713;
				list.Add(item28);
			}
			Gamepad gamepad22 = Gamepad._003Ccurrent_003Ek__BackingField;
			DpadControl dpadControl2 = gamepad22._003Cdpad_003Ek__BackingField;
			if (dpadControl2._003Cright_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item29 = (UniversalKeyCode)(obj + 48);
				_ = 714;
				list.Add(item29);
			}
			Gamepad gamepad23 = Gamepad._003Ccurrent_003Ek__BackingField;
			DpadControl dpadControl3 = gamepad23._003Cdpad_003Ek__BackingField;
			if (dpadControl3._003Cup_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item30 = (UniversalKeyCode)(obj + 48);
				_ = 711;
				list.Add(item30);
			}
			Gamepad gamepad24 = Gamepad._003Ccurrent_003Ek__BackingField;
			DpadControl dpadControl4 = gamepad24._003Cdpad_003Ek__BackingField;
			if (dpadControl4._003Cdown_003Ek__BackingField.wasReleasedThisFrame)
			{
				UniversalKeyCode item31 = (UniversalKeyCode)(obj + 48);
				_ = 712;
				list.Add(item31);
			}
		}
		if (Joystick._003Ccurrent_003Ek__BackingField != null && Joystick._003Ccurrent_003Ek__BackingField.wasUpdatedThisFrame)
		{
			InputDevice inputDevice = (InputDevice)(obj - 40);
			ReadOnlyArray<InputControl> allControls = inputDevice.allControls;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
			object obj5 = (object)allControls >> 32;
			bool flag12 = (nint)obj5 <= 0;
			object obj6 = 0;
			if (!flag12)
			{
				object obj11;
				do
				{
					object obj7 = obj + 56;
					object obj8 = obj - 56;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+38]");
					ButtonControl buttonControl = (ButtonControl)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+38]");
					if ((nint)0 != 0)
					{
						nint num13 = (nint)typeof(ButtonControl);
						nint num14 = (nint)buttonControl;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2590 @ rdx_v17 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
						object obj9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v361 @ r9_v7 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
						nint num15 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2590 @ rdx_v17 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
						if (num15 >= 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v361 @ r9_v7 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+C8]");
							object obj10 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2607 @ rax_v30+FFFFFFF8+v2591 @ rax_v29*8]");
							if (0 == (nint)typeof(ButtonControl))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+38]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+38]");
									if (((ButtonControl)0).wasReleasedThisFrame)
									{
										if (obj6 == null)
										{
											_ = 501;
										}
										else if ((nint)obj6 == 1)
										{
											_ = 502;
										}
										else if ((nint)obj6 == 2)
										{
											_ = 503;
										}
										else if ((nint)obj6 == 3)
										{
											_ = 504;
										}
										else if ((nint)obj6 == 4)
										{
											_ = 505;
										}
										else if ((nint)obj6 == 5)
										{
											_ = 506;
										}
										else if ((nint)obj6 == 6)
										{
											_ = 507;
										}
										else if ((nint)obj6 == 7)
										{
											_ = 508;
										}
										else if ((nint)obj6 == 8)
										{
											_ = 509;
										}
										else if ((nint)obj6 == 9)
										{
											_ = 510;
										}
										else
										{
											if ((nint)obj6 != 10)
											{
												goto IL_1404;
											}
											_ = 511;
										}
										UniversalKeyCode item32 = (UniversalKeyCode)(obj + 48);
										list.Add(item32);
									}
								}
							}
						}
					}
					goto IL_1404;
					IL_1404:
					obj6++;
					obj11 = obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-2C]");
				}
				while ((nint)obj11 < 0);
			}
		}
		return list;
	}

	public unsafe static bool GetUniversalKey(UniversalKeyCode universalKeyCode)
	{
		//IL_0136: Expected I4, but got O
		//IL_009d: Expected O, but got I
		List<UniversalKeyCode> tmpUniversalKeyResults = _tmpUniversalKeyResults;
		if (_tmpUniversalKeyResults != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				_ = 0;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
					Array.Clear((Array)num, 0, 0);
				}
			}
			List<UniversalKeyCode> pressedUniversalKeys = GetPressedUniversalKeys(excludeModifierKeys: false, excludeMouseButtons: false, _tmpUniversalKeyResults);
			List<UniversalKeyCode> tmpUniversalKeyResults2 = _tmpUniversalKeyResults;
			if (_tmpUniversalKeyResults != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rdx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 == 0)
				{
					return false;
				}
				object obj2 = default(object);
				if (_tmpUniversalKeyResults != null)
				{
					return _tmpUniversalKeyResults.Contains((UniversalKeyCode)(int)(&obj2));
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static UniversalKeyCode GetPressedUniversalKey(bool excludeModifierKeys, bool excludeMouseButtons)
	{
		//IL_012e: Expected I4, but got O
		//IL_009d: Expected O, but got I
		List<UniversalKeyCode> tmpUniversalKeyResults = _tmpUniversalKeyResults;
		if (_tmpUniversalKeyResults != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				_ = 0;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rbx_v1 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
					Array.Clear((Array)num, 0, 0);
				}
			}
			List<UniversalKeyCode> pressedUniversalKeys = GetPressedUniversalKeys(excludeModifierKeys: false, excludeMouseButtons: false, _tmpUniversalKeyResults);
			List<UniversalKeyCode> tmpUniversalKeyResults2 = _tmpUniversalKeyResults;
			if (_tmpUniversalKeyResults != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rdx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				if ((nint)0 == 0)
				{
					return UniversalKeyCode.None;
				}
				if (_tmpUniversalKeyResults != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UniversalKeyCode result = default(UniversalKeyCode);
					return result;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (UniversalKeyCode)ex;
	}

	public static List<UniversalKeyCode> GetPressedUniversalKeys(bool excludeModifierKeys, bool excludeMouseButtons, List<UniversalKeyCode> results = null)
	{
		//IL_002b: Expected F4, but got I4
		//IL_0033: Expected I, but got O
		//IL_14b0: Expected F4, but got I4
		//IL_14b8: Expected I, but got O
		//IL_14c6: Expected I4, but got O
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Expected O, but got Unknown
		//IL_03cb: Expected O, but got I4
		//IL_03d4: Expected O, but got I4
		//IL_0078: Expected I, but got O
		//IL_10bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c1: Expected O, but got Unknown
		//IL_1106: Expected O, but got I4
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected I4, but got Unknown
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Expected I4, but got Unknown
		//IL_111d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1122: Expected O, but got Unknown
		//IL_112b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1130: Expected O, but got Unknown
		//IL_114a: Expected O, but got I
		//IL_1433: Unknown result type (might be due to invalid IL or missing references)
		//IL_1438: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected I4, but got Unknown
		//IL_117d: Expected I, but got O
		//IL_1185: Expected I, but got O
		//IL_1195: Expected O, but got I
		//IL_0850: Unknown result type (might be due to invalid IL or missing references)
		//IL_0855: Expected I4, but got Unknown
		//IL_0704: Expected I4, but got O
		//IL_072a: Expected I, but got O
		//IL_11d1: Expected O, but got I
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Expected O, but got Unknown
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Expected O, but got Unknown
		//IL_044b: Expected I, but got O
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected I4, but got Unknown
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Expected I4, but got Unknown
		//IL_074f: Expected I4, but got O
		//IL_0761: Unknown result type (might be due to invalid IL or missing references)
		//IL_0766: Expected I4, but got Unknown
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected I4, but got Unknown
		//IL_1234: Expected O, but got I
		//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Expected I4, but got Unknown
		//IL_049a: Expected I, but got O
		//IL_153e: Expected F4, but got I4
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected I4, but got Unknown
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Expected I4, but got Unknown
		//IL_04e9: Expected I, but got O
		//IL_1736: Unknown result type (might be due to invalid IL or missing references)
		//IL_173b: Expected I4, but got Unknown
		//IL_0263: Expected I, but got O
		//IL_0283: Expected F4, but got I
		//IL_0294: Invalid comparison between I and F4
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ad: Expected I4, but got Unknown
		//IL_0320: Expected I, but got O
		//IL_0341: Invalid comparison between F4 and I
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Expected I4, but got Unknown
		//IL_0538: Expected I, but got O
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Expected I4, but got Unknown
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected I4, but got Unknown
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Expected I4, but got Unknown
		//IL_0587: Expected I, but got O
		//IL_0aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aaf: Expected I4, but got Unknown
		//IL_05d6: Expected I, but got O
		//IL_0b12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b17: Expected I4, but got Unknown
		//IL_0625: Expected I, but got O
		//IL_0b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7f: Expected I4, but got Unknown
		//IL_0674: Expected I, but got O
		//IL_0be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be7: Expected I4, but got Unknown
		//IL_06c3: Expected I, but got O
		//IL_06d1: Expected I, but got O
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Expected I4, but got Unknown
		//IL_0ca0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca5: Expected I4, but got Unknown
		//IL_0d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Expected I4, but got Unknown
		//IL_0d70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Expected I4, but got Unknown
		//IL_0dd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddd: Expected I4, but got Unknown
		//IL_0e40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e45: Expected I4, but got Unknown
		//IL_0e96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9b: Expected I4, but got Unknown
		//IL_0eec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef1: Expected I4, but got Unknown
		//IL_0f54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f59: Expected I4, but got Unknown
		//IL_0fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc1: Expected I4, but got Unknown
		//IL_1024: Unknown result type (might be due to invalid IL or missing references)
		//IL_1029: Expected I4, but got Unknown
		//IL_108c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1091: Expected I4, but got Unknown
		_ = 0;
		bool flag = results != null;
		nint num = (excludeMouseButtons ? 1 : 0);
		List<UniversalKeyCode> list = results;
		if (!flag)
		{
			List<UniversalKeyCode> list2 = new List<UniversalKeyCode>();
			num = 0;
			list = list2;
		}
		float num2 = 0f;
		nint num3 = (nint)results;
		UniversalKeyCode universalKeyCode = (UniversalKeyCode)num;
		object obj = default(object);
		if (!excludeMouseButtons)
		{
			bool flag2 = Mouse._003Ccurrent_003Ek__BackingField == null;
			num2 = 0f;
			num3 = (nint)results;
			universalKeyCode = (UniversalKeyCode)typeof(Mouse);
			if (!flag2)
			{
				Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
				bool isPressed = mouse._003CleftButton_003Ek__BackingField.isPressed;
				bool flag3 = !isPressed;
				nint num4 = (nint)results;
				if (!flag3)
				{
					UniversalKeyCode item = (UniversalKeyCode)(obj + 48);
					_ = 10;
					list.Add(item);
					num4 = 0;
				}
				Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
				bool isPressed2 = mouse2._003CmiddleButton_003Ek__BackingField.isPressed;
				bool flag4 = !isPressed2;
				nint num5 = num4;
				if (!flag4)
				{
					UniversalKeyCode item2 = (UniversalKeyCode)(obj + 48);
					_ = 11;
					list.Add(item2);
					num5 = 0;
				}
				Mouse mouse3 = Mouse._003Ccurrent_003Ek__BackingField;
				bool isPressed3 = mouse3._003CrightButton_003Ek__BackingField.isPressed;
				bool flag5 = !isPressed3;
				nint num6 = num5;
				if (!flag5)
				{
					UniversalKeyCode item3 = (UniversalKeyCode)(obj + 48);
					_ = 12;
					list.Add(item3);
					num6 = 0;
				}
				Mouse mouse4 = Mouse._003Ccurrent_003Ek__BackingField;
				bool isPressed4 = mouse4._003CbackButton_003Ek__BackingField.isPressed;
				bool flag6 = !isPressed4;
				nint num7 = num6;
				if (!flag6)
				{
					UniversalKeyCode item4 = (UniversalKeyCode)(obj + 48);
					_ = 14;
					list.Add(item4);
					num7 = 0;
				}
				Mouse mouse5 = Mouse._003Ccurrent_003Ek__BackingField;
				bool isPressed5 = mouse5._003CforwardButton_003Ek__BackingField.isPressed;
				bool flag7 = !isPressed5;
				nint num8 = num7;
				universalKeyCode = UniversalKeyCode.None;
				if (!flag7)
				{
					universalKeyCode = (UniversalKeyCode)(obj + 48);
					_ = 13;
					list.Add(universalKeyCode);
					num8 = 0;
				}
				int frameCount = Time.frameCount;
				bool flag8 = _lastMouseWheelChangeFrame != frameCount;
				num2 = 0f;
				num3 = num8;
				if (!flag8)
				{
					nint num9 = (nint)typeof(InputUtils);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v474 @ rax_v295 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputUtils>)+B8]");
					nint num10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rcx_v243 (Il2CppStaticFields<Kamgam.UGUIComponentsForSettings.InputUtils>)+8]");
					num2 = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v564 @ rcx_v243 (Il2CppStaticFields<Kamgam.UGUIComponentsForSettings.InputUtils>)+8]");
					bool flag9 = !(0f > 0.01f);
					num3 = num8;
					if (!flag9)
					{
						universalKeyCode = (UniversalKeyCode)(obj + 48);
						_ = 17;
						list.Add(universalKeyCode);
						num3 = 0;
					}
				}
				int frameCount2 = Time.frameCount;
				if (_lastMouseWheelChangeFrame == frameCount2)
				{
					nint num11 = (nint)typeof(InputUtils);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v290 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputUtils>)+B8]");
					nint num12 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ rcx_v240 (Il2CppStaticFields<Kamgam.UGUIComponentsForSettings.InputUtils>)+8]");
					bool flag10 = !(-0.01f > 0f);
					num2 = -0.01f;
					if (!flag10)
					{
						universalKeyCode = (UniversalKeyCode)(obj + 48);
						_ = 18;
						list.Add(universalKeyCode);
						num2 = -0.01f;
						num3 = 0;
					}
				}
			}
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			buildKeyCache();
			Key[] keys = Keys;
			object obj2 = Keys + 32;
			object obj3 = 0;
			nint num13;
			UniversalKeyCode universalKeyCode2;
			Keyboard keyboard = default(Keyboard);
			Keyboard keyboard2 = default(Keyboard);
			Keyboard keyboard3 = default(Keyboard);
			Keyboard keyboard4 = default(Keyboard);
			Keyboard keyboard5 = default(Keyboard);
			Keyboard keyboard6 = default(Keyboard);
			Keyboard keyboard7 = default(Keyboard);
			Keyboard keyboard8 = default(Keyboard);
			Keyboard keyboard9 = default(Keyboard);
			Keyboard keyboard10 = default(Keyboard);
			for (object obj4 = 0; (nint)obj3 < keys.Length; obj4++, obj2 += 4, num3 = num13, universalKeyCode = universalKeyCode2, obj3 = obj4)
			{
				if ((nint)obj4 < keys.Length)
				{
					if (excludeModifierKeys)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl = keyboard.get_Item(Key.LeftShift);
						bool isPressed6 = keyControl.isPressed;
						num13 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (isPressed6)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl2 = keyboard2.get_Item(Key.RightShift);
						bool isPressed7 = keyControl2.isPressed;
						num13 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (isPressed7)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl3 = keyboard3.get_Item(Key.Tab);
						bool isPressed8 = keyControl3.isPressed;
						num13 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (isPressed8)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl4 = keyboard4.get_Item(Key.LeftCtrl);
						bool isPressed9 = keyControl4.isPressed;
						num13 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (isPressed9)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl5 = keyboard5.get_Item(Key.RightCtrl);
						bool isPressed10 = keyControl5.isPressed;
						num13 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (isPressed10)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl6 = keyboard6.get_Item(Key.LeftMeta);
						bool isPressed11 = keyControl6.isPressed;
						num13 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (isPressed11)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl7 = keyboard7.get_Item(Key.RightMeta);
						bool isPressed12 = keyControl7.isPressed;
						num13 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (isPressed12)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl8 = keyboard8.get_Item(Key.LeftAlt);
						bool isPressed13 = keyControl8.isPressed;
						num13 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (isPressed13)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
						KeyControl keyControl9 = keyboard9.get_Item(Key.RightAlt);
						bool isPressed14 = keyControl9.isPressed;
						num3 = unchecked((nint)null);
						universalKeyCode = UniversalKeyCode.None;
						num13 = unchecked((nint)null);
						universalKeyCode2 = UniversalKeyCode.None;
						if (isPressed14)
						{
							continue;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
					KeyControl keyControl10 = keyboard10.get_Item((Key)obj2);
					bool isPressed15 = keyControl10.isPressed;
					bool flag11 = !isPressed15;
					num13 = unchecked((nint)null);
					universalKeyCode2 = UniversalKeyCode.None;
					if (!flag11)
					{
						UniversalKeyCode universalKeyCode3 = KeyToUniversalKeyCode((Key)obj2);
						universalKeyCode2 = (UniversalKeyCode)(obj + 48);
						list.Add(universalKeyCode2);
						num13 = 0;
					}
					continue;
				}
				return (List<UniversalKeyCode>)(object)new IndexOutOfRangeException();
			}
		}
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad._003CbuttonSouth_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item5 = (UniversalKeyCode)(obj + 48);
				_ = 702;
				list.Add(item5);
			}
			Gamepad gamepad2 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad2._003CbuttonEast_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item6 = (UniversalKeyCode)(obj + 48);
				_ = 704;
				list.Add(item6);
			}
			Gamepad gamepad3 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad3._003CbuttonWest_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item7 = (UniversalKeyCode)(obj + 48);
				_ = 703;
				list.Add(item7);
			}
			Gamepad gamepad4 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad4._003CbuttonNorth_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item8 = (UniversalKeyCode)(obj + 48);
				_ = 701;
				list.Add(item8);
			}
			Gamepad gamepad5 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad5._003CleftShoulder_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item9 = (UniversalKeyCode)(obj + 48);
				_ = 707;
				list.Add(item9);
			}
			Gamepad gamepad6 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad6._003CrightShoulder_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item10 = (UniversalKeyCode)(obj + 48);
				_ = 708;
				list.Add(item10);
			}
			Gamepad gamepad7 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad7._003CselectButton_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item11 = (UniversalKeyCode)(obj + 48);
				_ = 706;
				list.Add(item11);
			}
			Gamepad gamepad8 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad8._003CstartButton_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item12 = (UniversalKeyCode)(obj + 48);
				_ = 705;
				list.Add(item12);
			}
			Gamepad gamepad9 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad9._003CleftStickButton_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item13 = (UniversalKeyCode)(obj + 48);
				_ = 715;
				list.Add(item13);
			}
			Gamepad gamepad10 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl = gamepad10._003CleftStick_003Ek__BackingField;
			if (stickControl._003Cup_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item14 = (UniversalKeyCode)(obj + 48);
				_ = 717;
				list.Add(item14);
			}
			Gamepad gamepad11 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl2 = gamepad11._003CleftStick_003Ek__BackingField;
			if (stickControl2._003Cdown_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item15 = (UniversalKeyCode)(obj + 48);
				_ = 718;
				list.Add(item15);
			}
			Gamepad gamepad12 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl3 = gamepad12._003CleftStick_003Ek__BackingField;
			if (stickControl3._003Cleft_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item16 = (UniversalKeyCode)(obj + 48);
				_ = 719;
				list.Add(item16);
			}
			Gamepad gamepad13 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl4 = gamepad13._003CleftStick_003Ek__BackingField;
			if (stickControl4._003Cright_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item17 = (UniversalKeyCode)(obj + 48);
				_ = 720;
				list.Add(item17);
			}
			Gamepad gamepad14 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad14._003CrightStickButton_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item18 = (UniversalKeyCode)(obj + 48);
				_ = 716;
				list.Add(item18);
			}
			Gamepad gamepad15 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl5 = gamepad15._003CrightStick_003Ek__BackingField;
			if (stickControl5._003Cup_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item19 = (UniversalKeyCode)(obj + 48);
				_ = 721;
				list.Add(item19);
			}
			Gamepad gamepad16 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl6 = gamepad16._003CrightStick_003Ek__BackingField;
			if (stickControl6._003Cdown_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item20 = (UniversalKeyCode)(obj + 48);
				_ = 722;
				list.Add(item20);
			}
			Gamepad gamepad17 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl7 = gamepad17._003CrightStick_003Ek__BackingField;
			if (stickControl7._003Cleft_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item21 = (UniversalKeyCode)(obj + 48);
				_ = 723;
				list.Add(item21);
			}
			Gamepad gamepad18 = Gamepad._003Ccurrent_003Ek__BackingField;
			StickControl stickControl8 = gamepad18._003CrightStick_003Ek__BackingField;
			if (stickControl8._003Cright_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item22 = (UniversalKeyCode)(obj + 48);
				_ = 724;
				list.Add(item22);
			}
			Gamepad gamepad19 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad19._003CleftTrigger_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item23 = (UniversalKeyCode)(obj + 48);
				_ = 709;
				list.Add(item23);
			}
			Gamepad gamepad20 = Gamepad._003Ccurrent_003Ek__BackingField;
			if (gamepad20._003CrightTrigger_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item24 = (UniversalKeyCode)(obj + 48);
				_ = 710;
				list.Add(item24);
			}
			Gamepad gamepad21 = Gamepad._003Ccurrent_003Ek__BackingField;
			DpadControl dpadControl = gamepad21._003Cdpad_003Ek__BackingField;
			if (dpadControl._003Cleft_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item25 = (UniversalKeyCode)(obj + 48);
				_ = 713;
				list.Add(item25);
			}
			Gamepad gamepad22 = Gamepad._003Ccurrent_003Ek__BackingField;
			DpadControl dpadControl2 = gamepad22._003Cdpad_003Ek__BackingField;
			if (dpadControl2._003Cright_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item26 = (UniversalKeyCode)(obj + 48);
				_ = 714;
				list.Add(item26);
			}
			Gamepad gamepad23 = Gamepad._003Ccurrent_003Ek__BackingField;
			DpadControl dpadControl3 = gamepad23._003Cdpad_003Ek__BackingField;
			if (dpadControl3._003Cup_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item27 = (UniversalKeyCode)(obj + 48);
				_ = 711;
				list.Add(item27);
			}
			Gamepad gamepad24 = Gamepad._003Ccurrent_003Ek__BackingField;
			DpadControl dpadControl4 = gamepad24._003Cdpad_003Ek__BackingField;
			if (dpadControl4._003Cdown_003Ek__BackingField.isPressed)
			{
				UniversalKeyCode item28 = (UniversalKeyCode)(obj + 48);
				_ = 712;
				list.Add(item28);
			}
		}
		if (Joystick._003Ccurrent_003Ek__BackingField != null)
		{
			InputDevice inputDevice = (InputDevice)(obj - 40);
			ReadOnlyArray<InputControl> allControls = inputDevice.allControls;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
			object obj5 = (object)allControls >> 32;
			bool flag12 = (nint)obj5 <= 0;
			object obj6 = 0;
			if (!flag12)
			{
				object obj11;
				do
				{
					object obj7 = obj + 56;
					object obj8 = obj - 56;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+38]");
					ButtonControl buttonControl = (ButtonControl)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+38]");
					if ((nint)0 != 0)
					{
						nint num14 = (nint)typeof(ButtonControl);
						nint num15 = (nint)buttonControl;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2452 @ rdx_v15 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
						object obj9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ r9_v7 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
						nint num16 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2452 @ rdx_v15 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
						if (num16 >= 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ r9_v7 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+C8]");
							object obj10 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2469 @ rax_v27+FFFFFFF8+v2453 @ rax_v26*8]");
							if (0 == (nint)typeof(ButtonControl))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+38]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp+38]");
									if (((ButtonControl)0).isPressed)
									{
										if (obj6 == null)
										{
											_ = 501;
										}
										else if ((nint)obj6 == 1)
										{
											_ = 502;
										}
										else if ((nint)obj6 == 2)
										{
											_ = 503;
										}
										else if ((nint)obj6 == 3)
										{
											_ = 504;
										}
										else if ((nint)obj6 == 4)
										{
											_ = 505;
										}
										else if ((nint)obj6 == 5)
										{
											_ = 506;
										}
										else if ((nint)obj6 == 6)
										{
											_ = 507;
										}
										else if ((nint)obj6 == 7)
										{
											_ = 508;
										}
										else if ((nint)obj6 == 8)
										{
											_ = 509;
										}
										else if ((nint)obj6 == 9)
										{
											_ = 510;
										}
										else
										{
											if ((nint)obj6 != 10)
											{
												goto IL_142a;
											}
											_ = 511;
										}
										UniversalKeyCode item29 = (UniversalKeyCode)(obj + 48);
										list.Add(item29);
									}
								}
							}
						}
					}
					goto IL_142a;
					IL_142a:
					obj6++;
					obj11 = obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-2C]");
				}
				while ((nint)obj11 < 0);
			}
		}
		return list;
	}

	public static string UniversalKeyName(UniversalKeyCode keyCode)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected I4, but got Unknown
		//IL_57da: Unknown result type (might be due to invalid IL or missing references)
		//IL_57df: Expected I4, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected I4, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected I4, but got Unknown
		//IL_580d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5812: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected I4, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected I4, but got Unknown
		//IL_5848: Unknown result type (might be due to invalid IL or missing references)
		//IL_584d: Expected O, but got Unknown
		//IL_5856: Unknown result type (might be due to invalid IL or missing references)
		//IL_585b: Expected O, but got Unknown
		//IL_5877: Expected O, but got I
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected I4, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected I4, but got Unknown
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected I4, but got Unknown
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected I4, but got Unknown
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected I4, but got Unknown
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected I4, but got Unknown
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected I4, but got Unknown
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected I4, but got Unknown
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected I4, but got Unknown
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Expected I4, but got Unknown
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Expected I4, but got Unknown
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Expected I4, but got Unknown
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Expected I4, but got Unknown
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Expected I4, but got Unknown
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Expected I4, but got Unknown
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Expected I4, but got Unknown
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Expected I4, but got Unknown
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Expected I4, but got Unknown
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Expected I4, but got Unknown
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Expected I4, but got Unknown
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Expected I4, but got Unknown
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Expected I4, but got Unknown
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Expected I4, but got Unknown
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Expected I4, but got Unknown
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Expected I4, but got Unknown
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Expected I4, but got Unknown
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Expected I4, but got Unknown
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Expected I4, but got Unknown
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Expected I4, but got Unknown
		//IL_08be: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Expected I4, but got Unknown
		//IL_0940: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Expected I4, but got Unknown
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c7: Expected I4, but got Unknown
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Expected I4, but got Unknown
		//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Expected I4, but got Unknown
		//IL_0b7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b83: Expected I4, but got Unknown
		//IL_0c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Expected I4, but got Unknown
		//IL_0d4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d53: Expected I4, but got Unknown
		//IL_0d79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7e: Expected I4, but got Unknown
		//IL_0e63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e68: Expected I4, but got Unknown
		//IL_0e8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e93: Expected I4, but got Unknown
		//IL_0f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7d: Expected I4, but got Unknown
		//IL_0fa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa8: Expected I4, but got Unknown
		//IL_0fe8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fed: Expected I4, but got Unknown
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1032: Expected I4, but got Unknown
		//IL_1117: Unknown result type (might be due to invalid IL or missing references)
		//IL_111c: Expected I4, but got Unknown
		//IL_11ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1204: Expected I4, but got Unknown
		//IL_12e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ec: Expected I4, but got Unknown
		//IL_1312: Unknown result type (might be due to invalid IL or missing references)
		//IL_1317: Expected I4, but got Unknown
		//IL_1357: Unknown result type (might be due to invalid IL or missing references)
		//IL_135c: Expected I4, but got Unknown
		//IL_1417: Unknown result type (might be due to invalid IL or missing references)
		//IL_141c: Expected I4, but got Unknown
		//IL_14ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1504: Expected I4, but got Unknown
		//IL_15e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ec: Expected I4, but got Unknown
		//IL_16cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d4: Expected I4, but got Unknown
		//IL_17b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bc: Expected I4, but got Unknown
		//IL_189f: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a4: Expected I4, but got Unknown
		//IL_1987: Unknown result type (might be due to invalid IL or missing references)
		//IL_198c: Expected I4, but got Unknown
		//IL_1a6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a74: Expected I4, but got Unknown
		//IL_1b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5c: Expected I4, but got Unknown
		//IL_1c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c44: Expected I4, but got Unknown
		//IL_1d27: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2c: Expected I4, but got Unknown
		//IL_1e0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e14: Expected I4, but got Unknown
		//IL_1ef7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efc: Expected I4, but got Unknown
		//IL_1fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe4: Expected I4, but got Unknown
		//IL_20c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_20cc: Expected I4, but got Unknown
		//IL_21af: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b4: Expected I4, but got Unknown
		//IL_2297: Unknown result type (might be due to invalid IL or missing references)
		//IL_229c: Expected I4, but got Unknown
		//IL_237f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2384: Expected I4, but got Unknown
		//IL_2467: Unknown result type (might be due to invalid IL or missing references)
		//IL_246c: Expected I4, but got Unknown
		//IL_254f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2554: Expected I4, but got Unknown
		//IL_2637: Unknown result type (might be due to invalid IL or missing references)
		//IL_263c: Expected I4, but got Unknown
		//IL_271f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2724: Expected I4, but got Unknown
		//IL_2807: Unknown result type (might be due to invalid IL or missing references)
		//IL_280c: Expected I4, but got Unknown
		//IL_28ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_28f4: Expected I4, but got Unknown
		//IL_29d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_29dc: Expected I4, but got Unknown
		//IL_2abf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ac4: Expected I4, but got Unknown
		//IL_2ba7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bac: Expected I4, but got Unknown
		//IL_2bd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bd7: Expected I4, but got Unknown
		//IL_2c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c1c: Expected I4, but got Unknown
		//IL_2c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c61: Expected I4, but got Unknown
		//IL_2ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ca6: Expected I4, but got Unknown
		//IL_2d8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d90: Expected I4, but got Unknown
		//IL_2db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dbb: Expected I4, but got Unknown
		//IL_2dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e00: Expected I4, but got Unknown
		//IL_2e40: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e45: Expected I4, but got Unknown
		//IL_2e85: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e8a: Expected I4, but got Unknown
		//IL_2eca: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ecf: Expected I4, but got Unknown
		//IL_2f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f14: Expected I4, but got Unknown
		//IL_2f54: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f59: Expected I4, but got Unknown
		//IL_2f99: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f9e: Expected I4, but got Unknown
		//IL_2fde: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fe3: Expected I4, but got Unknown
		//IL_3023: Unknown result type (might be due to invalid IL or missing references)
		//IL_3028: Expected I4, but got Unknown
		//IL_3068: Unknown result type (might be due to invalid IL or missing references)
		//IL_306d: Expected I4, but got Unknown
		//IL_30ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_30b2: Expected I4, but got Unknown
		//IL_30f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_30f7: Expected I4, but got Unknown
		//IL_3137: Unknown result type (might be due to invalid IL or missing references)
		//IL_313c: Expected I4, but got Unknown
		//IL_317c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3181: Expected I4, but got Unknown
		//IL_31c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_31c6: Expected I4, but got Unknown
		//IL_3206: Unknown result type (might be due to invalid IL or missing references)
		//IL_320b: Expected I4, but got Unknown
		//IL_324b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3250: Expected I4, but got Unknown
		//IL_3290: Unknown result type (might be due to invalid IL or missing references)
		//IL_3295: Expected I4, but got Unknown
		//IL_32d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_32da: Expected I4, but got Unknown
		//IL_331a: Unknown result type (might be due to invalid IL or missing references)
		//IL_331f: Expected I4, but got Unknown
		//IL_3404: Unknown result type (might be due to invalid IL or missing references)
		//IL_3409: Expected I4, but got Unknown
		//IL_342f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3434: Expected I4, but got Unknown
		//IL_3474: Unknown result type (might be due to invalid IL or missing references)
		//IL_3479: Expected I4, but got Unknown
		//IL_355e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3563: Expected I4, but got Unknown
		//IL_3646: Unknown result type (might be due to invalid IL or missing references)
		//IL_364b: Expected I4, but got Unknown
		//IL_372e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3733: Expected I4, but got Unknown
		//IL_3816: Unknown result type (might be due to invalid IL or missing references)
		//IL_381b: Expected I4, but got Unknown
		//IL_38fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_3903: Expected I4, but got Unknown
		//IL_39e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_39eb: Expected I4, but got Unknown
		//IL_3ace: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ad3: Expected I4, but got Unknown
		//IL_3bb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bbb: Expected I4, but got Unknown
		//IL_3c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ca3: Expected I4, but got Unknown
		//IL_3d86: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d8b: Expected I4, but got Unknown
		//IL_3e6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e73: Expected I4, but got Unknown
		//IL_3f56: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f5b: Expected I4, but got Unknown
		//IL_403e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4043: Expected I4, but got Unknown
		//IL_4126: Unknown result type (might be due to invalid IL or missing references)
		//IL_412b: Expected I4, but got Unknown
		//IL_4151: Unknown result type (might be due to invalid IL or missing references)
		//IL_4156: Expected I4, but got Unknown
		//IL_4196: Unknown result type (might be due to invalid IL or missing references)
		//IL_419b: Expected I4, but got Unknown
		//IL_41db: Unknown result type (might be due to invalid IL or missing references)
		//IL_41e0: Expected I4, but got Unknown
		//IL_4220: Unknown result type (might be due to invalid IL or missing references)
		//IL_4225: Expected I4, but got Unknown
		//IL_4265: Unknown result type (might be due to invalid IL or missing references)
		//IL_426a: Expected I4, but got Unknown
		//IL_42aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_42af: Expected I4, but got Unknown
		//IL_42ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_42f4: Expected I4, but got Unknown
		//IL_4334: Unknown result type (might be due to invalid IL or missing references)
		//IL_4339: Expected I4, but got Unknown
		//IL_4379: Unknown result type (might be due to invalid IL or missing references)
		//IL_437e: Expected I4, but got Unknown
		//IL_43be: Unknown result type (might be due to invalid IL or missing references)
		//IL_43c3: Expected I4, but got Unknown
		//IL_4403: Unknown result type (might be due to invalid IL or missing references)
		//IL_4408: Expected I4, but got Unknown
		//IL_4448: Unknown result type (might be due to invalid IL or missing references)
		//IL_444d: Expected I4, but got Unknown
		//IL_4474: Expected I, but got O
		//IL_4549: Unknown result type (might be due to invalid IL or missing references)
		//IL_454e: Expected I4, but got Unknown
		//IL_4574: Unknown result type (might be due to invalid IL or missing references)
		//IL_4579: Expected I4, but got Unknown
		//IL_45b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_45be: Expected I4, but got Unknown
		//IL_45fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_4603: Expected I4, but got Unknown
		//IL_4643: Unknown result type (might be due to invalid IL or missing references)
		//IL_4648: Expected I4, but got Unknown
		//IL_4688: Unknown result type (might be due to invalid IL or missing references)
		//IL_468d: Expected I4, but got Unknown
		//IL_46cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_46d2: Expected I4, but got Unknown
		//IL_4712: Unknown result type (might be due to invalid IL or missing references)
		//IL_4717: Expected I4, but got Unknown
		//IL_4757: Unknown result type (might be due to invalid IL or missing references)
		//IL_475c: Expected I4, but got Unknown
		//IL_479c: Unknown result type (might be due to invalid IL or missing references)
		//IL_47a1: Expected I4, but got Unknown
		//IL_47e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_47e6: Expected I4, but got Unknown
		//IL_4826: Unknown result type (might be due to invalid IL or missing references)
		//IL_482b: Expected I4, but got Unknown
		//IL_486b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4870: Expected I4, but got Unknown
		//IL_48b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_48b5: Expected I4, but got Unknown
		//IL_48f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_48fa: Expected I4, but got Unknown
		//IL_493a: Unknown result type (might be due to invalid IL or missing references)
		//IL_493f: Expected I4, but got Unknown
		//IL_497f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4984: Expected I4, but got Unknown
		//IL_49c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_49c9: Expected I4, but got Unknown
		//IL_4a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a0e: Expected I4, but got Unknown
		//IL_4a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a53: Expected I4, but got Unknown
		//IL_4a93: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a98: Expected I4, but got Unknown
		//IL_4ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_4add: Expected I4, but got Unknown
		//IL_4b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b22: Expected I4, but got Unknown
		//IL_4b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b67: Expected I4, but got Unknown
		//IL_4ba7: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bac: Expected I4, but got Unknown
		//IL_4bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bf1: Expected I4, but got Unknown
		//IL_4c31: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c36: Expected I4, but got Unknown
		//IL_4cc4: Expected O, but got I
		//IL_4d1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d23: Expected I4, but got Unknown
		//IL_4daf: Expected O, but got I
		//IL_4e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_4e0e: Expected I4, but got Unknown
		//IL_4e9a: Expected O, but got I
		//IL_4ef4: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ef9: Expected I4, but got Unknown
		//IL_4f85: Expected O, but got I
		//IL_4fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fe4: Expected I4, but got Unknown
		//IL_500a: Unknown result type (might be due to invalid IL or missing references)
		//IL_500f: Expected I4, but got Unknown
		//IL_504f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5054: Expected I4, but got Unknown
		//IL_50e2: Expected O, but got I
		//IL_513c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5141: Expected I4, but got Unknown
		//IL_51cd: Expected O, but got I
		//IL_5227: Unknown result type (might be due to invalid IL or missing references)
		//IL_522c: Expected I4, but got Unknown
		//IL_52b8: Expected O, but got I
		//IL_5312: Unknown result type (might be due to invalid IL or missing references)
		//IL_5317: Expected I4, but got Unknown
		//IL_53a3: Expected O, but got I
		//IL_53fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_5402: Expected I4, but got Unknown
		//IL_5428: Unknown result type (might be due to invalid IL or missing references)
		//IL_542d: Expected I4, but got Unknown
		//IL_546d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5472: Expected I4, but got Unknown
		//IL_54b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_54b7: Expected I4, but got Unknown
		//IL_54f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_54fc: Expected I4, but got Unknown
		//IL_553c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5541: Expected I4, but got Unknown
		//IL_5581: Unknown result type (might be due to invalid IL or missing references)
		//IL_5586: Expected I4, but got Unknown
		//IL_55c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_55cb: Expected I4, but got Unknown
		//IL_560b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5610: Expected I4, but got Unknown
		//IL_5650: Unknown result type (might be due to invalid IL or missing references)
		//IL_5655: Expected I4, but got Unknown
		//IL_5695: Unknown result type (might be due to invalid IL or missing references)
		//IL_569a: Expected I4, but got Unknown
		//IL_56da: Unknown result type (might be due to invalid IL or missing references)
		//IL_56df: Expected I4, but got Unknown
		//IL_571f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5724: Expected I4, but got Unknown
		//IL_5764: Unknown result type (might be due to invalid IL or missing references)
		//IL_5769: Expected I4, but got Unknown
		//IL_57a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_57ae: Expected I4, but got Unknown
		object obj = default(object);
		string value14;
		if (keyNameDictionary.Count == 0)
		{
			UniversalKeyCode key = (UniversalKeyCode)(obj + 32);
			_ = 0;
			keyNameDictionary.Add(key, "-");
			UniversalKeyCode key2 = (UniversalKeyCode)(obj + 32);
			_ = 10;
			keyNameDictionary.Add(key2, "Left Mouse");
			UniversalKeyCode key3 = (UniversalKeyCode)(obj + 32);
			_ = 12;
			keyNameDictionary.Add(key3, "Right Mouse");
			UniversalKeyCode key4 = (UniversalKeyCode)(obj + 32);
			_ = 11;
			keyNameDictionary.Add(key4, "Middle Mouse");
			UniversalKeyCode key5 = (UniversalKeyCode)(obj + 32);
			_ = 14;
			keyNameDictionary.Add(key5, "Back Mouse");
			UniversalKeyCode key6 = (UniversalKeyCode)(obj + 32);
			_ = 13;
			keyNameDictionary.Add(key6, "Forward Mouse");
			UniversalKeyCode key7 = (UniversalKeyCode)(obj + 32);
			_ = 15;
			keyNameDictionary.Add(key7, "Mouse 6");
			UniversalKeyCode key8 = (UniversalKeyCode)(obj + 32);
			_ = 16;
			keyNameDictionary.Add(key8, "Mouse 7");
			UniversalKeyCode key9 = (UniversalKeyCode)(obj + 32);
			_ = 17;
			keyNameDictionary.Add(key9, "Mouse Wheel Up");
			UniversalKeyCode key10 = (UniversalKeyCode)(obj + 32);
			_ = 18;
			keyNameDictionary.Add(key10, "Mouse Wheel Down");
			UniversalKeyCode key11 = (UniversalKeyCode)(obj + 32);
			_ = 100;
			keyNameDictionary.Add(key11, "Backspace");
			UniversalKeyCode key12 = (UniversalKeyCode)(obj + 32);
			_ = 101;
			keyNameDictionary.Add(key12, "Tab");
			UniversalKeyCode key13 = (UniversalKeyCode)(obj + 32);
			_ = 102;
			keyNameDictionary.Add(key13, "Clear");
			UniversalKeyCode key14 = (UniversalKeyCode)(obj + 32);
			_ = 103;
			keyNameDictionary.Add(key14, "Return");
			UniversalKeyCode key15 = (UniversalKeyCode)(obj + 32);
			_ = 104;
			keyNameDictionary.Add(key15, "Pause");
			UniversalKeyCode key16 = (UniversalKeyCode)(obj + 32);
			_ = 105;
			keyNameDictionary.Add(key16, "Esc");
			UniversalKeyCode key17 = (UniversalKeyCode)(obj + 32);
			_ = 106;
			keyNameDictionary.Add(key17, "Space");
			UniversalKeyCode key18 = (UniversalKeyCode)(obj + 32);
			_ = 107;
			keyNameDictionary.Add(key18, "!");
			UniversalKeyCode key19 = (UniversalKeyCode)(obj + 32);
			_ = 108;
			keyNameDictionary.Add(key19, "\"");
			UniversalKeyCode key20 = (UniversalKeyCode)(obj + 32);
			_ = 109;
			keyNameDictionary.Add(key20, "#");
			UniversalKeyCode key21 = (UniversalKeyCode)(obj + 32);
			_ = 110;
			keyNameDictionary.Add(key21, "$");
			UniversalKeyCode key22 = (UniversalKeyCode)(obj + 32);
			_ = 111;
			keyNameDictionary.Add(key22, "%");
			UniversalKeyCode key23 = (UniversalKeyCode)(obj + 32);
			_ = 112;
			keyNameDictionary.Add(key23, "&");
			string value;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value = "\"";
			}
			else
			{
				KeyControl quoteKey = Keyboard._003Ccurrent_003Ek__BackingField.quoteKey;
				string displayName = quoteKey.displayName;
				value = displayName.ToUpper();
			}
			UniversalKeyCode key24 = (UniversalKeyCode)(obj + 32);
			_ = 113;
			keyNameDictionary.Add(key24, value);
			UniversalKeyCode key25 = (UniversalKeyCode)(obj + 32);
			_ = 114;
			keyNameDictionary.Add(key25, "(");
			UniversalKeyCode key26 = (UniversalKeyCode)(obj + 32);
			_ = 115;
			keyNameDictionary.Add(key26, ")");
			UniversalKeyCode key27 = (UniversalKeyCode)(obj + 32);
			_ = 116;
			keyNameDictionary.Add(key27, "*");
			UniversalKeyCode key28 = (UniversalKeyCode)(obj + 32);
			_ = 117;
			keyNameDictionary.Add(key28, "+");
			string value2;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value2 = ",";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key28, "+");
				Keyboard keyboard = default(Keyboard);
				KeyControl commaKey = keyboard.commaKey;
				string displayName2 = commaKey.displayName;
				value2 = displayName2.ToUpper();
			}
			UniversalKeyCode key29 = (UniversalKeyCode)(obj + 32);
			_ = 118;
			keyNameDictionary.Add(key29, value2);
			string value3;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value3 = "-";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key29, value2);
				Keyboard keyboard2 = default(Keyboard);
				KeyControl minusKey = keyboard2.minusKey;
				string displayName3 = minusKey.displayName;
				value3 = displayName3.ToUpper();
			}
			UniversalKeyCode key30 = (UniversalKeyCode)(obj + 32);
			_ = 119;
			keyNameDictionary.Add(key30, value3);
			string value4;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value4 = ".";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key30, value3);
				Keyboard keyboard3 = default(Keyboard);
				KeyControl periodKey = keyboard3.periodKey;
				string displayName4 = periodKey.displayName;
				value4 = displayName4.ToUpper();
			}
			UniversalKeyCode key31 = (UniversalKeyCode)(obj + 32);
			_ = 120;
			keyNameDictionary.Add(key31, value4);
			string value5;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value5 = "/";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key31, value4);
				Keyboard keyboard4 = default(Keyboard);
				KeyControl slashKey = keyboard4.slashKey;
				string displayName5 = slashKey.displayName;
				value5 = displayName5.ToUpper();
			}
			UniversalKeyCode key32 = (UniversalKeyCode)(obj + 32);
			_ = 121;
			keyNameDictionary.Add(key32, value5);
			string value6;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value6 = "0";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key32, value5);
				Keyboard keyboard5 = default(Keyboard);
				KeyControl digit0Key = keyboard5.digit0Key;
				string displayName6 = digit0Key.displayName;
				value6 = displayName6.ToUpper();
			}
			UniversalKeyCode key33 = (UniversalKeyCode)(obj + 32);
			_ = 122;
			keyNameDictionary.Add(key33, value6);
			string value7;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value7 = "1";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key33, value6);
				Keyboard keyboard6 = default(Keyboard);
				KeyControl digit1Key = keyboard6.digit1Key;
				string displayName7 = digit1Key.displayName;
				value7 = displayName7.ToUpper();
			}
			UniversalKeyCode key34 = (UniversalKeyCode)(obj + 32);
			_ = 123;
			keyNameDictionary.Add(key34, value7);
			string value8;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value8 = "2";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key34, value7);
				Keyboard keyboard7 = default(Keyboard);
				KeyControl digit2Key = keyboard7.digit2Key;
				string displayName8 = digit2Key.displayName;
				value8 = displayName8.ToUpper();
			}
			UniversalKeyCode key35 = (UniversalKeyCode)(obj + 32);
			_ = 124;
			keyNameDictionary.Add(key35, value8);
			string value9;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value9 = "3";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key35, value8);
				Keyboard keyboard8 = default(Keyboard);
				KeyControl digit3Key = keyboard8.digit3Key;
				string displayName9 = digit3Key.displayName;
				value9 = displayName9.ToUpper();
			}
			UniversalKeyCode key36 = (UniversalKeyCode)(obj + 32);
			_ = 125;
			keyNameDictionary.Add(key36, value9);
			string value10;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value10 = "4";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key36, value9);
				Keyboard keyboard9 = default(Keyboard);
				KeyControl digit4Key = keyboard9.digit4Key;
				string displayName10 = digit4Key.displayName;
				value10 = displayName10.ToUpper();
			}
			UniversalKeyCode key37 = (UniversalKeyCode)(obj + 32);
			_ = 126;
			keyNameDictionary.Add(key37, value10);
			string value11;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value11 = "5";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key37, value10);
				Keyboard keyboard10 = default(Keyboard);
				KeyControl digit5Key = keyboard10.digit5Key;
				string displayName11 = digit5Key.displayName;
				value11 = displayName11.ToUpper();
			}
			UniversalKeyCode key38 = (UniversalKeyCode)(obj + 32);
			_ = 127;
			keyNameDictionary.Add(key38, value11);
			string value12;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value12 = "6";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key38, value11);
				Keyboard keyboard11 = default(Keyboard);
				KeyControl digit6Key = keyboard11.digit6Key;
				string displayName12 = digit6Key.displayName;
				value12 = displayName12.ToUpper();
			}
			UniversalKeyCode key39 = (UniversalKeyCode)(obj + 32);
			_ = 128;
			keyNameDictionary.Add(key39, value12);
			string value13;
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value13 = "7";
			}
			else
			{
				((Dictionary<UniversalKeyCode, string>)null).Add(key39, value12);
				Keyboard keyboard12 = default(Keyboard);
				KeyControl digit7Key = keyboard12.digit7Key;
				string displayName13 = digit7Key.displayName;
				if (displayName13 == null)
				{
					goto IL_58f5;
				}
				value13 = displayName13.ToUpper();
			}
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key40 = (UniversalKeyCode)(obj + 32);
				_ = 129;
				keyNameDictionary.Add(key40, value13);
				if (Keyboard._003Ccurrent_003Ek__BackingField == null)
				{
					value14 = "8";
					goto IL_0c3f;
				}
				((Dictionary<UniversalKeyCode, string>)null).Add(key40, value13);
				Keyboard keyboard13 = default(Keyboard);
				if (keyboard13 != null)
				{
					KeyControl digit8Key = keyboard13.digit8Key;
					if (digit8Key != null)
					{
						string displayName14 = digit8Key.displayName;
						if (displayName14 != null)
						{
							value14 = displayName14.ToUpper();
							goto IL_0c3f;
						}
					}
				}
			}
			goto IL_58f5;
		}
		goto IL_5c8c;
		IL_15c0:
		string value15;
		string value16;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key41 = (UniversalKeyCode)(obj + 32);
			_ = 146;
			keyNameDictionary.Add(key41, value15);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value16 = "C";
				goto IL_16a8;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key41, value15);
			Keyboard keyboard14 = default(Keyboard);
			if (keyboard14 != null)
			{
				KeyControl cKey = keyboard14.cKey;
				if (cKey != null)
				{
					string displayName15 = cKey.displayName;
					if (displayName15 != null)
					{
						value16 = displayName15.ToUpper();
						goto IL_16a8;
					}
				}
			}
		}
		goto IL_58f5;
		IL_2270:
		string value17;
		string value18;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key42 = (UniversalKeyCode)(obj + 32);
			_ = 160;
			keyNameDictionary.Add(key42, value17);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value18 = "Q";
				goto IL_2358;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key42, value17);
			Keyboard keyboard15 = default(Keyboard);
			if (keyboard15 != null)
			{
				KeyControl qKey = keyboard15.qKey;
				if (qKey != null)
				{
					string displayName16 = qKey.displayName;
					if (displayName16 != null)
					{
						value18 = displayName16.ToUpper();
						goto IL_2358;
					}
				}
			}
		}
		goto IL_58f5;
		IL_3e47:
		string value19;
		string value20;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key43 = (UniversalKeyCode)(obj + 32);
			_ = 210;
			keyNameDictionary.Add(key43, value19);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value20 = "F10";
				goto IL_3f2f;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key43, value19);
			Keyboard keyboard16 = default(Keyboard);
			if (keyboard16 != null)
			{
				KeyControl f10Key = keyboard16.f10Key;
				if (f10Key != null)
				{
					string displayName17 = f10Key.displayName;
					if (displayName17 != null)
					{
						value20 = displayName17.ToUpper();
						goto IL_3f2f;
					}
				}
			}
		}
		goto IL_58f5;
		IL_0e3c:
		string value21;
		string value22;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key44 = (UniversalKeyCode)(obj + 32);
			_ = 133;
			keyNameDictionary.Add(key44, value21);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key45 = (UniversalKeyCode)(obj + 32);
				_ = 134;
				keyNameDictionary.Add(key45, "<");
				if (Keyboard._003Ccurrent_003Ek__BackingField == null)
				{
					value22 = "=";
					goto IL_0f51;
				}
				((Dictionary<UniversalKeyCode, string>)null).Add(key45, "<");
				Keyboard keyboard17 = default(Keyboard);
				if (keyboard17 != null)
				{
					KeyControl equalsKey = keyboard17.equalsKey;
					if (equalsKey != null)
					{
						string displayName18 = equalsKey.displayName;
						if (displayName18 != null)
						{
							value22 = displayName18.ToUpper();
							goto IL_0f51;
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_2358:
		string value23;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key46 = (UniversalKeyCode)(obj + 32);
			_ = 161;
			keyNameDictionary.Add(key46, value18);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value23 = "R";
				goto IL_2440;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key46, value18);
			Keyboard keyboard18 = default(Keyboard);
			if (keyboard18 != null)
			{
				KeyControl rKey = keyboard18.rKey;
				if (rKey != null)
				{
					string displayName19 = rKey.displayName;
					if (displayName19 != null)
					{
						value23 = displayName19.ToUpper();
						goto IL_2440;
					}
				}
			}
		}
		goto IL_58f5;
		IL_361f:
		string value24;
		string value25;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key47 = (UniversalKeyCode)(obj + 32);
			_ = 201;
			keyNameDictionary.Add(key47, value24);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value25 = "F1";
				goto IL_3707;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key47, value24);
			Keyboard keyboard19 = default(Keyboard);
			if (keyboard19 != null)
			{
				KeyControl f1Key = keyboard19.f1Key;
				if (f1Key != null)
				{
					string displayName20 = f1Key.displayName;
					if (displayName20 != null)
					{
						value25 = displayName20.ToUpper();
						goto IL_3707;
					}
				}
			}
		}
		goto IL_58f5;
		IL_27e0:
		string value26;
		string value27;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key48 = (UniversalKeyCode)(obj + 32);
			_ = 166;
			keyNameDictionary.Add(key48, value26);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value27 = "W";
				goto IL_28c8;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key48, value26);
			Keyboard keyboard20 = default(Keyboard);
			if (keyboard20 != null)
			{
				KeyControl wKey = keyboard20.wKey;
				if (wKey != null)
				{
					string displayName21 = wKey.displayName;
					if (displayName21 != null)
					{
						value27 = displayName21.ToUpper();
						goto IL_28c8;
					}
				}
			}
		}
		goto IL_58f5;
		IL_0f51:
		string value28;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key49 = (UniversalKeyCode)(obj + 32);
			_ = 135;
			keyNameDictionary.Add(key49, value22);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key50 = (UniversalKeyCode)(obj + 32);
				_ = 136;
				keyNameDictionary.Add(key50, ">");
				if (keyNameDictionary != null)
				{
					UniversalKeyCode key51 = (UniversalKeyCode)(obj + 32);
					_ = 137;
					keyNameDictionary.Add(key51, "?");
					if (keyNameDictionary != null)
					{
						UniversalKeyCode key52 = (UniversalKeyCode)(obj + 32);
						_ = 138;
						keyNameDictionary.Add(key52, "@");
						if (Keyboard._003Ccurrent_003Ek__BackingField == null)
						{
							value28 = "(";
							goto IL_10f0;
						}
						((Dictionary<UniversalKeyCode, string>)null).Add(key52, "@");
						Keyboard keyboard21 = default(Keyboard);
						if (keyboard21 != null)
						{
							KeyControl leftBracketKey = keyboard21.leftBracketKey;
							if (leftBracketKey != null)
							{
								string displayName22 = leftBracketKey.displayName;
								if (displayName22 != null)
								{
									value28 = displayName22.ToUpper();
									goto IL_10f0;
								}
							}
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_16a8:
		string value29;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key53 = (UniversalKeyCode)(obj + 32);
			_ = 147;
			keyNameDictionary.Add(key53, value16);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value29 = "D";
				goto IL_1790;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key53, value16);
			Keyboard keyboard22 = default(Keyboard);
			if (keyboard22 != null)
			{
				KeyControl dKey = keyboard22.dKey;
				if (dKey != null)
				{
					string displayName23 = dKey.displayName;
					if (displayName23 != null)
					{
						value29 = displayName23.ToUpper();
						goto IL_1790;
					}
				}
			}
		}
		goto IL_58f5;
		IL_4de2:
		string value30;
		string value31;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key54 = (UniversalKeyCode)(obj + 32);
			_ = 702;
			keyNameDictionary.Add(key54, value30);
			if (Gamepad._003Ccurrent_003Ek__BackingField == null)
			{
				value31 = "X";
				goto IL_4ecd;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key54, value30);
			object obj2 = default(object);
			if (obj2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2216 @ rax_v799+190]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2216 @ rax_v799+190]");
					string shortDisplayName = ((InputControl)0).shortDisplayName;
					if (shortDisplayName != null)
					{
						value31 = shortDisplayName.ToUpper();
						goto IL_4ecd;
					}
				}
			}
		}
		goto IL_58f5;
		IL_1790:
		string value32;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key55 = (UniversalKeyCode)(obj + 32);
			_ = 148;
			keyNameDictionary.Add(key55, value29);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value32 = "E";
				goto IL_1878;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key55, value29);
			Keyboard keyboard23 = default(Keyboard);
			if (keyboard23 != null)
			{
				KeyControl eKey = keyboard23.eKey;
				if (eKey != null)
				{
					string displayName24 = eKey.displayName;
					if (displayName24 != null)
					{
						value32 = displayName24.ToUpper();
						goto IL_1878;
					}
				}
			}
		}
		goto IL_58f5;
		IL_52eb:
		string value33;
		string value34;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key56 = (UniversalKeyCode)(obj + 32);
			_ = 709;
			keyNameDictionary.Add(key56, value33);
			if (Gamepad._003Ccurrent_003Ek__BackingField == null)
			{
				value34 = "RT";
				goto IL_53d6;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key56, value33);
			object obj3 = default(object);
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2232 @ rax_v764+200]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2232 @ rax_v764+200]");
					string shortDisplayName2 = ((InputControl)0).shortDisplayName;
					if (shortDisplayName2 != null)
					{
						value34 = shortDisplayName2.ToUpper();
						goto IL_53d6;
					}
				}
			}
		}
		goto IL_58f5;
		IL_1de8:
		string value35;
		string value36;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key57 = (UniversalKeyCode)(obj + 32);
			_ = 155;
			keyNameDictionary.Add(key57, value35);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value36 = "L";
				goto IL_1ed0;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key57, value35);
			Keyboard keyboard24 = default(Keyboard);
			if (keyboard24 != null)
			{
				KeyControl lKey = keyboard24.lKey;
				if (lKey != null)
				{
					string displayName25 = lKey.displayName;
					if (displayName25 != null)
					{
						value36 = displayName25.ToUpper();
						goto IL_1ed0;
					}
				}
			}
		}
		goto IL_58f5;
		IL_3707:
		string value37;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key58 = (UniversalKeyCode)(obj + 32);
			_ = 202;
			keyNameDictionary.Add(key58, value25);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value37 = "F2";
				goto IL_37ef;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key58, value25);
			Keyboard keyboard25 = default(Keyboard);
			if (keyboard25 != null)
			{
				KeyControl f2Key = keyboard25.f2Key;
				if (f2Key != null)
				{
					string displayName26 = f2Key.displayName;
					if (displayName26 != null)
					{
						value37 = displayName26.ToUpper();
						goto IL_37ef;
					}
				}
			}
		}
		goto IL_58f5;
		IL_28c8:
		string value38;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key59 = (UniversalKeyCode)(obj + 32);
			_ = 167;
			keyNameDictionary.Add(key59, value27);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value38 = "X";
				goto IL_29b0;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key59, value27);
			Keyboard keyboard26 = default(Keyboard);
			if (keyboard26 != null)
			{
				KeyControl xKey = keyboard26.xKey;
				if (xKey != null)
				{
					string displayName27 = xKey.displayName;
					if (displayName27 != null)
					{
						value38 = displayName27.ToUpper();
						goto IL_29b0;
					}
				}
			}
		}
		goto IL_58f5;
		IL_5115:
		string value39;
		string value40;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key60 = (UniversalKeyCode)(obj + 32);
			_ = 707;
			keyNameDictionary.Add(key60, value39);
			if (Gamepad._003Ccurrent_003Ek__BackingField == null)
			{
				value40 = "RB";
				goto IL_5200;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key60, value39);
			object obj4 = default(object);
			if (obj4 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2226 @ rax_v778+1E0]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2226 @ rax_v778+1E0]");
					string shortDisplayName3 = ((InputControl)0).shortDisplayName;
					if (shortDisplayName3 != null)
					{
						value40 = shortDisplayName3.ToUpper();
						goto IL_5200;
					}
				}
			}
		}
		goto IL_58f5;
		IL_58f5:
		return (string)(object)new NullReferenceException();
		IL_4522:
		string value41;
		string value42;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key61 = (UniversalKeyCode)(obj + 32);
			_ = 226;
			keyNameDictionary.Add(key61, value41);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key62 = (UniversalKeyCode)(obj + 32);
				_ = 227;
				keyNameDictionary.Add(key62, "AltGr");
				if (keyNameDictionary != null)
				{
					UniversalKeyCode key63 = (UniversalKeyCode)(obj + 32);
					_ = 228;
					keyNameDictionary.Add(key63, "Help");
					if (keyNameDictionary != null)
					{
						UniversalKeyCode key64 = (UniversalKeyCode)(obj + 32);
						_ = 229;
						keyNameDictionary.Add(key64, "Print");
						if (keyNameDictionary != null)
						{
							UniversalKeyCode key65 = (UniversalKeyCode)(obj + 32);
							_ = 230;
							keyNameDictionary.Add(key65, "SysReq");
							if (keyNameDictionary != null)
							{
								UniversalKeyCode key66 = (UniversalKeyCode)(obj + 32);
								_ = 231;
								keyNameDictionary.Add(key66, "Break");
								if (keyNameDictionary != null)
								{
									UniversalKeyCode key67 = (UniversalKeyCode)(obj + 32);
									_ = 232;
									keyNameDictionary.Add(key67, "Menu");
									if (keyNameDictionary != null)
									{
										UniversalKeyCode key68 = (UniversalKeyCode)(obj + 32);
										_ = 501;
										keyNameDictionary.Add(key68, "Joy0");
										if (keyNameDictionary != null)
										{
											UniversalKeyCode key69 = (UniversalKeyCode)(obj + 32);
											_ = 502;
											keyNameDictionary.Add(key69, "Joy1");
											if (keyNameDictionary != null)
											{
												UniversalKeyCode key70 = (UniversalKeyCode)(obj + 32);
												_ = 503;
												keyNameDictionary.Add(key70, "Joy2");
												if (keyNameDictionary != null)
												{
													UniversalKeyCode key71 = (UniversalKeyCode)(obj + 32);
													_ = 504;
													keyNameDictionary.Add(key71, "Joy3");
													if (keyNameDictionary != null)
													{
														UniversalKeyCode key72 = (UniversalKeyCode)(obj + 32);
														_ = 505;
														keyNameDictionary.Add(key72, "Joy4");
														if (keyNameDictionary != null)
														{
															UniversalKeyCode key73 = (UniversalKeyCode)(obj + 32);
															_ = 506;
															keyNameDictionary.Add(key73, "Joy5");
															if (keyNameDictionary != null)
															{
																UniversalKeyCode key74 = (UniversalKeyCode)(obj + 32);
																_ = 507;
																keyNameDictionary.Add(key74, "Joy6");
																if (keyNameDictionary != null)
																{
																	UniversalKeyCode key75 = (UniversalKeyCode)(obj + 32);
																	_ = 508;
																	keyNameDictionary.Add(key75, "Joy7");
																	if (keyNameDictionary != null)
																	{
																		UniversalKeyCode key76 = (UniversalKeyCode)(obj + 32);
																		_ = 509;
																		keyNameDictionary.Add(key76, "Joy8");
																		if (keyNameDictionary != null)
																		{
																			UniversalKeyCode key77 = (UniversalKeyCode)(obj + 32);
																			_ = 510;
																			keyNameDictionary.Add(key77, "Joy9");
																			if (keyNameDictionary != null)
																			{
																				UniversalKeyCode key78 = (UniversalKeyCode)(obj + 32);
																				_ = 511;
																				keyNameDictionary.Add(key78, "Joy10");
																				if (keyNameDictionary != null)
																				{
																					UniversalKeyCode key79 = (UniversalKeyCode)(obj + 32);
																					_ = 512;
																					keyNameDictionary.Add(key79, "Joy11");
																					if (keyNameDictionary != null)
																					{
																						UniversalKeyCode key80 = (UniversalKeyCode)(obj + 32);
																						_ = 513;
																						keyNameDictionary.Add(key80, "Joy12");
																						if (keyNameDictionary != null)
																						{
																							UniversalKeyCode key81 = (UniversalKeyCode)(obj + 32);
																							_ = 514;
																							keyNameDictionary.Add(key81, "Joy13");
																							if (keyNameDictionary != null)
																							{
																								UniversalKeyCode key82 = (UniversalKeyCode)(obj + 32);
																								_ = 515;
																								keyNameDictionary.Add(key82, "Joy14");
																								if (keyNameDictionary != null)
																								{
																									UniversalKeyCode key83 = (UniversalKeyCode)(obj + 32);
																									_ = 516;
																									keyNameDictionary.Add(key83, "Joy15");
																									if (keyNameDictionary != null)
																									{
																										UniversalKeyCode key84 = (UniversalKeyCode)(obj + 32);
																										_ = 517;
																										keyNameDictionary.Add(key84, "Joy16");
																										if (keyNameDictionary != null)
																										{
																											UniversalKeyCode key85 = (UniversalKeyCode)(obj + 32);
																											_ = 518;
																											keyNameDictionary.Add(key85, "Joy17");
																											if (keyNameDictionary != null)
																											{
																												UniversalKeyCode key86 = (UniversalKeyCode)(obj + 32);
																												_ = 519;
																												keyNameDictionary.Add(key86, "Joy18");
																												if (keyNameDictionary != null)
																												{
																													UniversalKeyCode key87 = (UniversalKeyCode)(obj + 32);
																													_ = 520;
																													keyNameDictionary.Add(key87, "Joy19");
																													if (Gamepad._003Ccurrent_003Ek__BackingField == null)
																													{
																														value42 = "Y";
																														goto IL_4cf7;
																													}
																													((Dictionary<UniversalKeyCode, string>)null).Add(key87, "Joy19");
																													object obj5 = default(object);
																													if (obj5 != null)
																													{
																														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2210 @ rax_v813+198]");
																														if ((nint)0 != 0)
																														{
																															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2210 @ rax_v813+198]");
																															string shortDisplayName4 = ((InputControl)0).shortDisplayName;
																															if (shortDisplayName4 != null)
																															{
																																value42 = shortDisplayName4.ToUpper();
																																goto IL_4cf7;
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_5c8c:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key88 = (UniversalKeyCode)(obj + 32);
			if (!keyNameDictionary.ContainsKey(key88))
			{
				Enum obj6 = (Enum)(obj - 32);
				_ = typeof(UniversalKeyCode);
				_ = -1;
				return obj6.ToString();
			}
			if (keyNameDictionary != null)
			{
				object obj7 = obj + 40;
				object obj8 = obj + 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+28]");
				return (string)0;
			}
		}
		goto IL_58f5;
		IL_33dd:
		string value43;
		string value44;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key89 = (UniversalKeyCode)(obj + 32);
			_ = 197;
			keyNameDictionary.Add(key89, value43);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key90 = (UniversalKeyCode)(obj + 32);
				_ = 198;
				keyNameDictionary.Add(key90, "Home");
				if (keyNameDictionary != null)
				{
					UniversalKeyCode key91 = (UniversalKeyCode)(obj + 32);
					_ = 199;
					keyNameDictionary.Add(key91, "End");
					if (Keyboard._003Ccurrent_003Ek__BackingField == null)
					{
						value44 = "PageUp";
						goto IL_3537;
					}
					((Dictionary<UniversalKeyCode, string>)null).Add(key91, "End");
					Keyboard keyboard27 = default(Keyboard);
					if (keyboard27 != null)
					{
						KeyControl pageUpKey = keyboard27.pageUpKey;
						if (pageUpKey != null)
						{
							string displayName28 = pageUpKey.displayName;
							if (displayName28 != null)
							{
								value44 = displayName28.ToUpper();
								goto IL_3537;
							}
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_1878:
		string value45;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key92 = (UniversalKeyCode)(obj + 32);
			_ = 149;
			keyNameDictionary.Add(key92, value32);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value45 = "F";
				goto IL_1960;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key92, value32);
			Keyboard keyboard28 = default(Keyboard);
			if (keyboard28 != null)
			{
				KeyControl fKey = keyboard28.fKey;
				if (fKey != null)
				{
					string displayName29 = fKey.displayName;
					if (displayName29 != null)
					{
						value45 = displayName29.ToUpper();
						goto IL_1960;
					}
				}
			}
		}
		goto IL_58f5;
		IL_10f0:
		string value46;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key93 = (UniversalKeyCode)(obj + 32);
			_ = 139;
			keyNameDictionary.Add(key93, value28);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value46 = "\\";
				goto IL_11d8;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key93, value28);
			Keyboard keyboard29 = default(Keyboard);
			if (keyboard29 != null)
			{
				KeyControl backslashKey = keyboard29.backslashKey;
				if (backslashKey != null)
				{
					string displayName30 = backslashKey.displayName;
					if (displayName30 != null)
					{
						value46 = displayName30.ToUpper();
						goto IL_11d8;
					}
				}
			}
		}
		goto IL_58f5;
		IL_2d64:
		string value47;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key94 = (UniversalKeyCode)(obj + 32);
			_ = 175;
			keyNameDictionary.Add(key94, value47);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key95 = (UniversalKeyCode)(obj + 32);
				_ = 176;
				keyNameDictionary.Add(key95, "0");
				if (keyNameDictionary != null)
				{
					UniversalKeyCode key96 = (UniversalKeyCode)(obj + 32);
					_ = 177;
					keyNameDictionary.Add(key96, "1");
					if (keyNameDictionary != null)
					{
						UniversalKeyCode key97 = (UniversalKeyCode)(obj + 32);
						_ = 178;
						keyNameDictionary.Add(key97, "2");
						if (keyNameDictionary != null)
						{
							UniversalKeyCode key98 = (UniversalKeyCode)(obj + 32);
							_ = 179;
							keyNameDictionary.Add(key98, "3");
							if (keyNameDictionary != null)
							{
								UniversalKeyCode key99 = (UniversalKeyCode)(obj + 32);
								_ = 180;
								keyNameDictionary.Add(key99, "4");
								if (keyNameDictionary != null)
								{
									UniversalKeyCode key100 = (UniversalKeyCode)(obj + 32);
									_ = 181;
									keyNameDictionary.Add(key100, "5");
									if (keyNameDictionary != null)
									{
										UniversalKeyCode key101 = (UniversalKeyCode)(obj + 32);
										_ = 182;
										keyNameDictionary.Add(key101, "6");
										if (keyNameDictionary != null)
										{
											UniversalKeyCode key102 = (UniversalKeyCode)(obj + 32);
											_ = 183;
											keyNameDictionary.Add(key102, "7");
											if (keyNameDictionary != null)
											{
												UniversalKeyCode key103 = (UniversalKeyCode)(obj + 32);
												_ = 184;
												keyNameDictionary.Add(key103, "8");
												if (keyNameDictionary != null)
												{
													UniversalKeyCode key104 = (UniversalKeyCode)(obj + 32);
													_ = 185;
													keyNameDictionary.Add(key104, "9");
													if (keyNameDictionary != null)
													{
														UniversalKeyCode key105 = (UniversalKeyCode)(obj + 32);
														_ = 186;
														keyNameDictionary.Add(key105, ".");
														if (keyNameDictionary != null)
														{
															UniversalKeyCode key106 = (UniversalKeyCode)(obj + 32);
															_ = 187;
															keyNameDictionary.Add(key106, "/");
															if (keyNameDictionary != null)
															{
																UniversalKeyCode key107 = (UniversalKeyCode)(obj + 32);
																_ = 188;
																keyNameDictionary.Add(key107, "*");
																if (keyNameDictionary != null)
																{
																	UniversalKeyCode key108 = (UniversalKeyCode)(obj + 32);
																	_ = 189;
																	keyNameDictionary.Add(key108, "-");
																	if (keyNameDictionary != null)
																	{
																		UniversalKeyCode key109 = (UniversalKeyCode)(obj + 32);
																		_ = 190;
																		keyNameDictionary.Add(key109, "+");
																		if (keyNameDictionary != null)
																		{
																			UniversalKeyCode key110 = (UniversalKeyCode)(obj + 32);
																			_ = 191;
																			keyNameDictionary.Add(key110, "Enter");
																			if (keyNameDictionary != null)
																			{
																				UniversalKeyCode key111 = (UniversalKeyCode)(obj + 32);
																				_ = 192;
																				keyNameDictionary.Add(key111, "=");
																				if (keyNameDictionary != null)
																				{
																					UniversalKeyCode key112 = (UniversalKeyCode)(obj + 32);
																					_ = 193;
																					keyNameDictionary.Add(key112, "Up Arrow");
																					if (keyNameDictionary != null)
																					{
																						UniversalKeyCode key113 = (UniversalKeyCode)(obj + 32);
																						_ = 194;
																						keyNameDictionary.Add(key113, "Down Arrow");
																						if (keyNameDictionary != null)
																						{
																							UniversalKeyCode key114 = (UniversalKeyCode)(obj + 32);
																							_ = 195;
																							keyNameDictionary.Add(key114, "Right Arrow");
																							if (keyNameDictionary != null)
																							{
																								UniversalKeyCode key115 = (UniversalKeyCode)(obj + 32);
																								_ = 196;
																								keyNameDictionary.Add(key115, "Left Arrow");
																								if (Keyboard._003Ccurrent_003Ek__BackingField == null)
																								{
																									value43 = "Ins";
																									goto IL_33dd;
																								}
																								((Dictionary<UniversalKeyCode, string>)null).Add(key115, "Left Arrow");
																								Keyboard keyboard30 = default(Keyboard);
																								if (keyboard30 != null)
																								{
																									KeyControl insertKey = keyboard30.insertKey;
																									if (insertKey != null)
																									{
																										string displayName31 = insertKey.displayName;
																										if (displayName31 != null)
																										{
																											value43 = displayName31.ToUpper();
																											goto IL_33dd;
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_3c77:
		string value48;
		string value49;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key116 = (UniversalKeyCode)(obj + 32);
			_ = 208;
			keyNameDictionary.Add(key116, value48);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value49 = "F8";
				goto IL_3d5f;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key116, value48);
			Keyboard keyboard31 = default(Keyboard);
			if (keyboard31 != null)
			{
				KeyControl f8Key = keyboard31.f8Key;
				if (f8Key != null)
				{
					string displayName32 = f8Key.displayName;
					if (displayName32 != null)
					{
						value49 = displayName32.ToUpper();
						goto IL_3d5f;
					}
				}
			}
		}
		goto IL_58f5;
		IL_3f2f:
		string value50;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key117 = (UniversalKeyCode)(obj + 32);
			_ = 211;
			keyNameDictionary.Add(key117, value20);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value50 = "F11";
				goto IL_4017;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key117, value20);
			Keyboard keyboard32 = default(Keyboard);
			if (keyboard32 != null)
			{
				KeyControl f11Key = keyboard32.f11Key;
				if (f11Key != null)
				{
					string displayName33 = f11Key.displayName;
					if (displayName33 != null)
					{
						value50 = displayName33.ToUpper();
						goto IL_4017;
					}
				}
			}
		}
		goto IL_58f5;
		IL_2440:
		string value51;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key118 = (UniversalKeyCode)(obj + 32);
			_ = 162;
			keyNameDictionary.Add(key118, value23);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value51 = "S";
				goto IL_2528;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key118, value23);
			Keyboard keyboard33 = default(Keyboard);
			if (keyboard33 != null)
			{
				KeyControl sKey = keyboard33.sKey;
				if (sKey != null)
				{
					string displayName34 = sKey.displayName;
					if (displayName34 != null)
					{
						value51 = displayName34.ToUpper();
						goto IL_2528;
					}
				}
			}
		}
		goto IL_58f5;
		IL_11d8:
		string value52;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key119 = (UniversalKeyCode)(obj + 32);
			_ = 140;
			keyNameDictionary.Add(key119, value46);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value52 = ")";
				goto IL_12c0;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key119, value46);
			Keyboard keyboard34 = default(Keyboard);
			if (keyboard34 != null)
			{
				KeyControl rightBracketKey = keyboard34.rightBracketKey;
				if (rightBracketKey != null)
				{
					string displayName35 = rightBracketKey.displayName;
					if (displayName35 != null)
					{
						value52 = displayName35.ToUpper();
						goto IL_12c0;
					}
				}
			}
		}
		goto IL_58f5;
		IL_1ed0:
		string value53;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key120 = (UniversalKeyCode)(obj + 32);
			_ = 156;
			keyNameDictionary.Add(key120, value36);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value53 = "M";
				goto IL_1fb8;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key120, value36);
			Keyboard keyboard35 = default(Keyboard);
			if (keyboard35 != null)
			{
				KeyControl mKey = keyboard35.mKey;
				if (mKey != null)
				{
					string displayName36 = mKey.displayName;
					if (displayName36 != null)
					{
						value53 = displayName36.ToUpper();
						goto IL_1fb8;
					}
				}
			}
		}
		goto IL_58f5;
		IL_29b0:
		string value54;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key121 = (UniversalKeyCode)(obj + 32);
			_ = 168;
			keyNameDictionary.Add(key121, value38);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value54 = "Y";
				goto IL_2a98;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key121, value38);
			Keyboard keyboard36 = default(Keyboard);
			if (keyboard36 != null)
			{
				KeyControl yKey = keyboard36.yKey;
				if (yKey != null)
				{
					string displayName37 = yKey.displayName;
					if (displayName37 != null)
					{
						value54 = displayName37.ToUpper();
						goto IL_2a98;
					}
				}
			}
		}
		goto IL_58f5;
		IL_4017:
		string value55;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key122 = (UniversalKeyCode)(obj + 32);
			_ = 212;
			keyNameDictionary.Add(key122, value50);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value55 = "F12";
				goto IL_40ff;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key122, value50);
			Keyboard keyboard37 = default(Keyboard);
			if (keyboard37 != null)
			{
				KeyControl f12Key = keyboard37.f12Key;
				if (f12Key != null)
				{
					string displayName38 = f12Key.displayName;
					if (displayName38 != null)
					{
						value55 = displayName38.ToUpper();
						goto IL_40ff;
					}
				}
			}
		}
		goto IL_58f5;
		IL_39bf:
		string value56;
		string value57;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key123 = (UniversalKeyCode)(obj + 32);
			_ = 205;
			keyNameDictionary.Add(key123, value56);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value57 = "F5";
				goto IL_3aa7;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key123, value56);
			Keyboard keyboard38 = default(Keyboard);
			if (keyboard38 != null)
			{
				KeyControl f5Key = keyboard38.f5Key;
				if (f5Key != null)
				{
					string displayName39 = f5Key.displayName;
					if (displayName39 != null)
					{
						value57 = displayName39.ToUpper();
						goto IL_3aa7;
					}
				}
			}
		}
		goto IL_58f5;
		IL_12c0:
		string value58;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key124 = (UniversalKeyCode)(obj + 32);
			_ = 141;
			keyNameDictionary.Add(key124, value52);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key125 = (UniversalKeyCode)(obj + 32);
				_ = 142;
				keyNameDictionary.Add(key125, "^");
				if (keyNameDictionary != null)
				{
					UniversalKeyCode key126 = (UniversalKeyCode)(obj + 32);
					_ = 143;
					keyNameDictionary.Add(key126, "_");
					if (Keyboard._003Ccurrent_003Ek__BackingField == null)
					{
						value58 = "\"";
						goto IL_13f0;
					}
					((Dictionary<UniversalKeyCode, string>)null).Add(key126, "_");
					Keyboard keyboard39 = default(Keyboard);
					if (keyboard39 != null)
					{
						KeyControl backquoteKey = keyboard39.backquoteKey;
						if (backquoteKey != null)
						{
							value58 = backquoteKey.displayName;
							goto IL_13f0;
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_1960:
		string value59;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key127 = (UniversalKeyCode)(obj + 32);
			_ = 150;
			keyNameDictionary.Add(key127, value45);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value59 = "G";
				goto IL_1a48;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key127, value45);
			Keyboard keyboard40 = default(Keyboard);
			if (keyboard40 != null)
			{
				KeyControl gKey = keyboard40.gKey;
				if (gKey != null)
				{
					string displayName40 = gKey.displayName;
					if (displayName40 != null)
					{
						value59 = displayName40.ToUpper();
						goto IL_1a48;
					}
				}
			}
		}
		goto IL_58f5;
		IL_40ff:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key128 = (UniversalKeyCode)(obj + 32);
			_ = 213;
			keyNameDictionary.Add(key128, value55);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key129 = (UniversalKeyCode)(obj + 32);
				_ = 214;
				keyNameDictionary.Add(key129, "NumLock");
				if (keyNameDictionary != null)
				{
					UniversalKeyCode key130 = (UniversalKeyCode)(obj + 32);
					_ = 215;
					keyNameDictionary.Add(key130, "CapsLock");
					if (keyNameDictionary != null)
					{
						UniversalKeyCode key131 = (UniversalKeyCode)(obj + 32);
						_ = 216;
						keyNameDictionary.Add(key131, "ScrollLock");
						if (keyNameDictionary != null)
						{
							UniversalKeyCode key132 = (UniversalKeyCode)(obj + 32);
							_ = 217;
							keyNameDictionary.Add(key132, "Shift");
							if (keyNameDictionary != null)
							{
								UniversalKeyCode key133 = (UniversalKeyCode)(obj + 32);
								_ = 218;
								keyNameDictionary.Add(key133, "Shift");
								if (keyNameDictionary != null)
								{
									UniversalKeyCode key134 = (UniversalKeyCode)(obj + 32);
									_ = 219;
									keyNameDictionary.Add(key134, "Ctrl");
									if (keyNameDictionary != null)
									{
										UniversalKeyCode key135 = (UniversalKeyCode)(obj + 32);
										_ = 220;
										keyNameDictionary.Add(key135, "Ctrl");
										if (keyNameDictionary != null)
										{
											UniversalKeyCode key136 = (UniversalKeyCode)(obj + 32);
											_ = 221;
											keyNameDictionary.Add(key136, "Alt");
											if (keyNameDictionary != null)
											{
												UniversalKeyCode key137 = (UniversalKeyCode)(obj + 32);
												_ = 222;
												keyNameDictionary.Add(key137, "Alt");
												if (keyNameDictionary != null)
												{
													UniversalKeyCode key138 = (UniversalKeyCode)(obj + 32);
													_ = 223;
													keyNameDictionary.Add(key138, "Cmd");
													if (keyNameDictionary != null)
													{
														UniversalKeyCode key139 = (UniversalKeyCode)(obj + 32);
														_ = 224;
														keyNameDictionary.Add(key139, "Cmd");
														if (keyNameDictionary != null)
														{
															UniversalKeyCode key140 = (UniversalKeyCode)(obj + 32);
															_ = 225;
															keyNameDictionary.Add(key140, "Win");
															nint num = (nint)typeof(InputUtils);
															if (Keyboard._003Ccurrent_003Ek__BackingField == null)
															{
																value41 = "Win";
																goto IL_4522;
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10179 @ rax_v615 (Il2CppClass<Kamgam.UGUIComponentsForSettings.InputUtils>)+B8]");
															((Dictionary<UniversalKeyCode, string>)null).Add(UniversalKeyCode.None, "Win");
															Keyboard keyboard41 = default(Keyboard);
															if (keyboard41 != null)
															{
																KeyControl rightMetaKey = keyboard41.rightMetaKey;
																if (rightMetaKey != null)
																{
																	string displayName41 = rightMetaKey.displayName;
																	if (displayName41 != null)
																	{
																		value41 = displayName41.ToUpper();
																		goto IL_4522;
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_1a48:
		string value60;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key141 = (UniversalKeyCode)(obj + 32);
			_ = 151;
			keyNameDictionary.Add(key141, value59);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value60 = "H";
				goto IL_1b30;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key141, value59);
			Keyboard keyboard42 = default(Keyboard);
			if (keyboard42 != null)
			{
				KeyControl hKey = keyboard42.hKey;
				if (hKey != null)
				{
					string displayName42 = hKey.displayName;
					if (displayName42 != null)
					{
						value60 = displayName42.ToUpper();
						goto IL_1b30;
					}
				}
			}
		}
		goto IL_58f5;
		IL_1fb8:
		string value61;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key142 = (UniversalKeyCode)(obj + 32);
			_ = 157;
			keyNameDictionary.Add(key142, value53);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value61 = "N";
				goto IL_20a0;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key142, value53);
			Keyboard keyboard43 = default(Keyboard);
			if (keyboard43 != null)
			{
				KeyControl nKey = keyboard43.nKey;
				if (nKey != null)
				{
					string displayName43 = nKey.displayName;
					if (displayName43 != null)
					{
						value61 = displayName43.ToUpper();
						goto IL_20a0;
					}
				}
			}
		}
		goto IL_58f5;
		IL_4fb8:
		string value62;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key143 = (UniversalKeyCode)(obj + 32);
			_ = 704;
			keyNameDictionary.Add(key143, value62);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key144 = (UniversalKeyCode)(obj + 32);
				_ = 705;
				keyNameDictionary.Add(key144, "Start");
				if (keyNameDictionary != null)
				{
					UniversalKeyCode key145 = (UniversalKeyCode)(obj + 32);
					_ = 706;
					keyNameDictionary.Add(key145, "Select");
					if (Gamepad._003Ccurrent_003Ek__BackingField == null)
					{
						value39 = "LB";
						goto IL_5115;
					}
					((Dictionary<UniversalKeyCode, string>)null).Add(key145, "Select");
					object obj9 = default(object);
					if (obj9 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2223 @ rax_v785+1D8]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2223 @ rax_v785+1D8]");
							string shortDisplayName5 = ((InputControl)0).shortDisplayName;
							if (shortDisplayName5 != null)
							{
								value39 = shortDisplayName5.ToUpper();
								goto IL_5115;
							}
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_2610:
		string value63;
		string value64;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key146 = (UniversalKeyCode)(obj + 32);
			_ = 164;
			keyNameDictionary.Add(key146, value63);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value64 = "U";
				goto IL_26f8;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key146, value63);
			Keyboard keyboard44 = default(Keyboard);
			if (keyboard44 != null)
			{
				KeyControl uKey = keyboard44.uKey;
				if (uKey != null)
				{
					string displayName44 = uKey.displayName;
					if (displayName44 != null)
					{
						value64 = displayName44.ToUpper();
						goto IL_26f8;
					}
				}
			}
		}
		goto IL_58f5;
		IL_5200:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key147 = (UniversalKeyCode)(obj + 32);
			_ = 708;
			keyNameDictionary.Add(key147, value40);
			if (Gamepad._003Ccurrent_003Ek__BackingField == null)
			{
				value33 = "LT";
				goto IL_52eb;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key147, value40);
			object obj10 = default(object);
			if (obj10 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2229 @ rax_v771+1F8]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2229 @ rax_v771+1F8]");
					string shortDisplayName6 = ((InputControl)0).shortDisplayName;
					if (shortDisplayName6 != null)
					{
						value33 = shortDisplayName6.ToUpper();
						goto IL_52eb;
					}
				}
			}
		}
		goto IL_58f5;
		IL_53d6:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key148 = (UniversalKeyCode)(obj + 32);
			_ = 710;
			keyNameDictionary.Add(key148, value34);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key149 = (UniversalKeyCode)(obj + 32);
				_ = 711;
				keyNameDictionary.Add(key149, "DPad Up");
				if (keyNameDictionary != null)
				{
					UniversalKeyCode key150 = (UniversalKeyCode)(obj + 32);
					_ = 712;
					keyNameDictionary.Add(key150, "DPad Down");
					if (keyNameDictionary != null)
					{
						UniversalKeyCode key151 = (UniversalKeyCode)(obj + 32);
						_ = 713;
						keyNameDictionary.Add(key151, "DPad Left");
						if (keyNameDictionary != null)
						{
							UniversalKeyCode key152 = (UniversalKeyCode)(obj + 32);
							_ = 714;
							keyNameDictionary.Add(key152, "DPad Right");
							if (keyNameDictionary != null)
							{
								UniversalKeyCode key153 = (UniversalKeyCode)(obj + 32);
								_ = 715;
								keyNameDictionary.Add(key153, "Left Stick");
								if (keyNameDictionary != null)
								{
									UniversalKeyCode key154 = (UniversalKeyCode)(obj + 32);
									_ = 716;
									keyNameDictionary.Add(key154, "Left Stick");
									if (keyNameDictionary != null)
									{
										UniversalKeyCode key155 = (UniversalKeyCode)(obj + 32);
										_ = 717;
										keyNameDictionary.Add(key155, "L-Stick Up");
										if (keyNameDictionary != null)
										{
											UniversalKeyCode key156 = (UniversalKeyCode)(obj + 32);
											_ = 718;
											keyNameDictionary.Add(key156, "L-Stick Down");
											if (keyNameDictionary != null)
											{
												UniversalKeyCode key157 = (UniversalKeyCode)(obj + 32);
												_ = 719;
												keyNameDictionary.Add(key157, "L-Stick Left");
												if (keyNameDictionary != null)
												{
													UniversalKeyCode key158 = (UniversalKeyCode)(obj + 32);
													_ = 720;
													keyNameDictionary.Add(key158, "L-Stick Right");
													if (keyNameDictionary != null)
													{
														UniversalKeyCode key159 = (UniversalKeyCode)(obj + 32);
														_ = 721;
														keyNameDictionary.Add(key159, "R-Stick Up");
														if (keyNameDictionary != null)
														{
															UniversalKeyCode key160 = (UniversalKeyCode)(obj + 32);
															_ = 722;
															keyNameDictionary.Add(key160, "R-Stick Down");
															if (keyNameDictionary != null)
															{
																UniversalKeyCode key161 = (UniversalKeyCode)(obj + 32);
																_ = 723;
																keyNameDictionary.Add(key161, "R-Stick Left");
																if (keyNameDictionary != null)
																{
																	UniversalKeyCode key162 = (UniversalKeyCode)(obj + 32);
																	_ = 724;
																	keyNameDictionary.Add(key162, "R-Stick Right");
																	goto IL_5c8c;
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_20a0:
		string value65;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key163 = (UniversalKeyCode)(obj + 32);
			_ = 158;
			keyNameDictionary.Add(key163, value61);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value65 = "O";
				goto IL_2188;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key163, value61);
			Keyboard keyboard45 = default(Keyboard);
			if (keyboard45 != null)
			{
				KeyControl oKey = keyboard45.oKey;
				if (oKey != null)
				{
					string displayName45 = oKey.displayName;
					if (displayName45 != null)
					{
						value65 = displayName45.ToUpper();
						goto IL_2188;
					}
				}
			}
		}
		goto IL_58f5;
		IL_13f0:
		string value66;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key164 = (UniversalKeyCode)(obj + 32);
			_ = 144;
			keyNameDictionary.Add(key164, value58);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value66 = "A";
				goto IL_14d8;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key164, value58);
			Keyboard keyboard46 = default(Keyboard);
			if (keyboard46 != null)
			{
				KeyControl aKey = keyboard46.aKey;
				if (aKey != null)
				{
					string name = aKey.name;
					if (name != null)
					{
						value66 = name.ToUpper();
						goto IL_14d8;
					}
				}
			}
		}
		goto IL_58f5;
		IL_1b30:
		string value67;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key165 = (UniversalKeyCode)(obj + 32);
			_ = 152;
			keyNameDictionary.Add(key165, value60);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value67 = "I";
				goto IL_1c18;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key165, value60);
			Keyboard keyboard47 = default(Keyboard);
			if (keyboard47 != null)
			{
				KeyControl iKey = keyboard47.iKey;
				if (iKey != null)
				{
					string displayName46 = iKey.displayName;
					if (displayName46 != null)
					{
						value67 = displayName46.ToUpper();
						goto IL_1c18;
					}
				}
			}
		}
		goto IL_58f5;
		IL_3537:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key166 = (UniversalKeyCode)(obj + 32);
			_ = 200;
			keyNameDictionary.Add(key166, value44);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value24 = "PageDown";
				goto IL_361f;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key166, value44);
			Keyboard keyboard48 = default(Keyboard);
			if (keyboard48 != null)
			{
				KeyControl pageDownKey = keyboard48.pageDownKey;
				if (pageDownKey != null)
				{
					string displayName47 = pageDownKey.displayName;
					if (displayName47 != null)
					{
						value24 = displayName47.ToUpper();
						goto IL_361f;
					}
				}
			}
		}
		goto IL_58f5;
		IL_3aa7:
		string value68;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key167 = (UniversalKeyCode)(obj + 32);
			_ = 206;
			keyNameDictionary.Add(key167, value57);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value68 = "F6";
				goto IL_3b8f;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key167, value57);
			Keyboard keyboard49 = default(Keyboard);
			if (keyboard49 != null)
			{
				KeyControl f6Key = keyboard49.f6Key;
				if (f6Key != null)
				{
					string displayName48 = f6Key.displayName;
					if (displayName48 != null)
					{
						value68 = displayName48.ToUpper();
						goto IL_3b8f;
					}
				}
			}
		}
		goto IL_58f5;
		IL_2188:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key168 = (UniversalKeyCode)(obj + 32);
			_ = 159;
			keyNameDictionary.Add(key168, value65);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value17 = "P";
				goto IL_2270;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key168, value65);
			Keyboard keyboard50 = default(Keyboard);
			if (keyboard50 != null)
			{
				KeyControl pKey = keyboard50.pKey;
				if (pKey != null)
				{
					string displayName49 = pKey.displayName;
					if (displayName49 != null)
					{
						value17 = displayName49.ToUpper();
						goto IL_2270;
					}
				}
			}
		}
		goto IL_58f5;
		IL_37ef:
		string value69;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key169 = (UniversalKeyCode)(obj + 32);
			_ = 203;
			keyNameDictionary.Add(key169, value37);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value69 = "F3";
				goto IL_38d7;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key169, value37);
			Keyboard keyboard51 = default(Keyboard);
			if (keyboard51 != null)
			{
				KeyControl f3Key = keyboard51.f3Key;
				if (f3Key != null)
				{
					string displayName50 = f3Key.displayName;
					if (displayName50 != null)
					{
						value69 = displayName50.ToUpper();
						goto IL_38d7;
					}
				}
			}
		}
		goto IL_58f5;
		IL_0c3f:
		string value70;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key170 = (UniversalKeyCode)(obj + 32);
			_ = 130;
			keyNameDictionary.Add(key170, value14);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value70 = "9";
				goto IL_0d27;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key170, value14);
			Keyboard keyboard52 = default(Keyboard);
			if (keyboard52 != null)
			{
				KeyControl digit9Key = keyboard52.digit9Key;
				if (digit9Key != null)
				{
					string displayName51 = digit9Key.displayName;
					if (displayName51 != null)
					{
						value70 = displayName51.ToUpper();
						goto IL_0d27;
					}
				}
			}
		}
		goto IL_58f5;
		IL_2528:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key171 = (UniversalKeyCode)(obj + 32);
			_ = 163;
			keyNameDictionary.Add(key171, value51);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value63 = "T";
				goto IL_2610;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key171, value51);
			Keyboard keyboard53 = default(Keyboard);
			if (keyboard53 != null)
			{
				KeyControl tKey = keyboard53.tKey;
				if (tKey != null)
				{
					string displayName52 = tKey.displayName;
					if (displayName52 != null)
					{
						value63 = displayName52.ToUpper();
						goto IL_2610;
					}
				}
			}
		}
		goto IL_58f5;
		IL_38d7:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key172 = (UniversalKeyCode)(obj + 32);
			_ = 204;
			keyNameDictionary.Add(key172, value69);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value56 = "F4";
				goto IL_39bf;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key172, value69);
			Keyboard keyboard54 = default(Keyboard);
			if (keyboard54 != null)
			{
				KeyControl f4Key = keyboard54.f4Key;
				if (f4Key != null)
				{
					string displayName53 = f4Key.displayName;
					if (displayName53 != null)
					{
						value56 = displayName53.ToUpper();
						goto IL_39bf;
					}
				}
			}
		}
		goto IL_58f5;
		IL_3d5f:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key173 = (UniversalKeyCode)(obj + 32);
			_ = 209;
			keyNameDictionary.Add(key173, value49);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value19 = "F9";
				goto IL_3e47;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key173, value49);
			Keyboard keyboard55 = default(Keyboard);
			if (keyboard55 != null)
			{
				KeyControl f9Key = keyboard55.f9Key;
				if (f9Key != null)
				{
					string displayName54 = f9Key.displayName;
					if (displayName54 != null)
					{
						value19 = displayName54.ToUpper();
						goto IL_3e47;
					}
				}
			}
		}
		goto IL_58f5;
		IL_1c18:
		string value71;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key174 = (UniversalKeyCode)(obj + 32);
			_ = 153;
			keyNameDictionary.Add(key174, value67);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value71 = "J";
				goto IL_1d00;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key174, value67);
			Keyboard keyboard56 = default(Keyboard);
			if (keyboard56 != null)
			{
				KeyControl jKey = keyboard56.jKey;
				if (jKey != null)
				{
					string displayName55 = jKey.displayName;
					if (displayName55 != null)
					{
						value71 = displayName55.ToUpper();
						goto IL_1d00;
					}
				}
			}
		}
		goto IL_58f5;
		IL_26f8:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key175 = (UniversalKeyCode)(obj + 32);
			_ = 165;
			keyNameDictionary.Add(key175, value64);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value26 = "V";
				goto IL_27e0;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key175, value64);
			Keyboard keyboard57 = default(Keyboard);
			if (keyboard57 != null)
			{
				KeyControl vKey = keyboard57.vKey;
				if (vKey != null)
				{
					string displayName56 = vKey.displayName;
					if (displayName56 != null)
					{
						value26 = displayName56.ToUpper();
						goto IL_27e0;
					}
				}
			}
		}
		goto IL_58f5;
		IL_14d8:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key176 = (UniversalKeyCode)(obj + 32);
			_ = 145;
			keyNameDictionary.Add(key176, value66);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value15 = "B";
				goto IL_15c0;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key176, value66);
			Keyboard keyboard58 = default(Keyboard);
			if (keyboard58 != null)
			{
				KeyControl bKey = keyboard58.bKey;
				if (bKey != null)
				{
					string displayName57 = bKey.displayName;
					if (displayName57 != null)
					{
						value15 = displayName57.ToUpper();
						goto IL_15c0;
					}
				}
			}
		}
		goto IL_58f5;
		IL_0d27:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key177 = (UniversalKeyCode)(obj + 32);
			_ = 131;
			keyNameDictionary.Add(key177, value70);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key178 = (UniversalKeyCode)(obj + 32);
				_ = 132;
				keyNameDictionary.Add(key178, ":");
				if (Keyboard._003Ccurrent_003Ek__BackingField == null)
				{
					value21 = ";";
					goto IL_0e3c;
				}
				((Dictionary<UniversalKeyCode, string>)null).Add(key178, ":");
				Keyboard keyboard59 = default(Keyboard);
				if (keyboard59 != null)
				{
					KeyControl semicolonKey = keyboard59.semicolonKey;
					if (semicolonKey != null)
					{
						string displayName58 = semicolonKey.displayName;
						if (displayName58 != null)
						{
							value21 = displayName58.ToUpper();
							goto IL_0e3c;
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_2b80:
		string value72;
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key179 = (UniversalKeyCode)(obj + 32);
			_ = 170;
			keyNameDictionary.Add(key179, value72);
			if (keyNameDictionary != null)
			{
				UniversalKeyCode key180 = (UniversalKeyCode)(obj + 32);
				_ = 171;
				keyNameDictionary.Add(key180, "{");
				if (keyNameDictionary != null)
				{
					UniversalKeyCode key181 = (UniversalKeyCode)(obj + 32);
					_ = 172;
					keyNameDictionary.Add(key181, "|");
					if (keyNameDictionary != null)
					{
						UniversalKeyCode key182 = (UniversalKeyCode)(obj + 32);
						_ = 173;
						keyNameDictionary.Add(key182, "}");
						if (keyNameDictionary != null)
						{
							UniversalKeyCode key183 = (UniversalKeyCode)(obj + 32);
							_ = 174;
							keyNameDictionary.Add(key183, "~");
							if (Keyboard._003Ccurrent_003Ek__BackingField == null)
							{
								value47 = "Del";
								goto IL_2d64;
							}
							((Dictionary<UniversalKeyCode, string>)null).Add(key183, "~");
							Keyboard keyboard60 = default(Keyboard);
							if (keyboard60 != null)
							{
								KeyControl deleteKey = keyboard60.deleteKey;
								if (deleteKey != null)
								{
									string displayName59 = deleteKey.displayName;
									if (displayName59 != null)
									{
										value47 = displayName59.ToUpper();
										goto IL_2d64;
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_58f5;
		IL_2a98:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key184 = (UniversalKeyCode)(obj + 32);
			_ = 169;
			keyNameDictionary.Add(key184, value54);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value72 = "Z";
				goto IL_2b80;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key184, value54);
			Keyboard keyboard61 = default(Keyboard);
			if (keyboard61 != null)
			{
				KeyControl zKey = keyboard61.zKey;
				if (zKey != null)
				{
					string displayName60 = zKey.displayName;
					if (displayName60 != null)
					{
						value72 = displayName60.ToUpper();
						goto IL_2b80;
					}
				}
			}
		}
		goto IL_58f5;
		IL_3b8f:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key185 = (UniversalKeyCode)(obj + 32);
			_ = 207;
			keyNameDictionary.Add(key185, value68);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value48 = "F7";
				goto IL_3c77;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key185, value68);
			Keyboard keyboard62 = default(Keyboard);
			if (keyboard62 != null)
			{
				KeyControl f7Key = keyboard62.f7Key;
				if (f7Key != null)
				{
					string displayName61 = f7Key.displayName;
					if (displayName61 != null)
					{
						value48 = displayName61.ToUpper();
						goto IL_3c77;
					}
				}
			}
		}
		goto IL_58f5;
		IL_4cf7:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key186 = (UniversalKeyCode)(obj + 32);
			_ = 701;
			keyNameDictionary.Add(key186, value42);
			if (Gamepad._003Ccurrent_003Ek__BackingField == null)
			{
				value30 = "A";
				goto IL_4de2;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key186, value42);
			object obj11 = default(object);
			if (obj11 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2213 @ rax_v806+1A0]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2213 @ rax_v806+1A0]");
					string shortDisplayName7 = ((InputControl)0).shortDisplayName;
					if (shortDisplayName7 != null)
					{
						value30 = shortDisplayName7.ToUpper();
						goto IL_4de2;
					}
				}
			}
		}
		goto IL_58f5;
		IL_4ecd:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key187 = (UniversalKeyCode)(obj + 32);
			_ = 703;
			keyNameDictionary.Add(key187, value31);
			if (Gamepad._003Ccurrent_003Ek__BackingField == null)
			{
				value62 = "B";
				goto IL_4fb8;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key187, value31);
			object obj12 = default(object);
			if (obj12 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2219 @ rax_v792+1A8]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2219 @ rax_v792+1A8]");
					string shortDisplayName8 = ((InputControl)0).shortDisplayName;
					if (shortDisplayName8 != null)
					{
						value62 = shortDisplayName8.ToUpper();
						goto IL_4fb8;
					}
				}
			}
		}
		goto IL_58f5;
		IL_1d00:
		if (keyNameDictionary != null)
		{
			UniversalKeyCode key188 = (UniversalKeyCode)(obj + 32);
			_ = 154;
			keyNameDictionary.Add(key188, value71);
			if (Keyboard._003Ccurrent_003Ek__BackingField == null)
			{
				value35 = "K";
				goto IL_1de8;
			}
			((Dictionary<UniversalKeyCode, string>)null).Add(key188, value71);
			Keyboard keyboard63 = default(Keyboard);
			if (keyboard63 != null)
			{
				KeyControl kKey = keyboard63.kKey;
				if (kKey != null)
				{
					string displayName62 = kKey.displayName;
					if (displayName62 != null)
					{
						value35 = displayName62.ToUpper();
						goto IL_1de8;
					}
				}
			}
		}
		goto IL_58f5;
	}

	public static UniversalKeyCode KeyToUniversalKeyCode(Key key)
	{
		//IL_004d: Expected I4, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<(Key, UniversalKeyCode)>.Enumerator enumerator = default(List<(Key, UniversalKeyCode)>.Enumerator);
		object obj = default(object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if ((nint)obj == (nint)key)
			{
				UniversalKeyCode result = (UniversalKeyCode)(obj >> 32);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
				return result;
			}
		}
		enumerator.Dispose();
		return UniversalKeyCode.Unknown;
	}

	public unsafe static Key? UniversalKeyCodeToKey(UniversalKeyCode universalKeyCode)
	{
		//IL_0082: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<(Key, UniversalKeyCode)>.Enumerator enumerator = default(List<(Key, UniversalKeyCode)>.Enumerator);
		object obj2 = default(object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			object obj = obj2 >> 32;
			if ((nint)obj == (nint)universalKeyCode)
			{
				Key? result = (Key)(int)(&obj2);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
				return result;
			}
		}
		enumerator.Dispose();
		return (Key?)(object)0;
	}

	unsafe static InputUtils()
	{
		//IL_0008: Expected O, but got Ref
		//IL_344e: Expected I4, but got I8
		//IL_005e: Expected O, but got Ref
		//IL_007c: Expected native int or pointer, but got O
		//IL_0094: Expected O, but got Ref
		//IL_00fa: Expected O, but got Ref
		//IL_0158: Expected O, but got Ref
		//IL_01b6: Expected O, but got Ref
		//IL_0214: Expected O, but got Ref
		//IL_0272: Expected O, but got Ref
		//IL_02d0: Expected O, but got Ref
		//IL_032e: Expected O, but got Ref
		//IL_038c: Expected O, but got Ref
		//IL_03ea: Expected O, but got Ref
		//IL_0448: Expected O, but got Ref
		//IL_04a6: Expected O, but got Ref
		//IL_0504: Expected O, but got Ref
		//IL_0546: Expected O, but got Ref
		//IL_055e: Expected native int or pointer, but got O
		//IL_0576: Expected O, but got Ref
		//IL_05c0: Expected O, but got Ref
		//IL_05d8: Expected native int or pointer, but got O
		//IL_05f0: Expected O, but got Ref
		//IL_063a: Expected O, but got Ref
		//IL_0652: Expected native int or pointer, but got O
		//IL_066a: Expected O, but got Ref
		//IL_06b4: Expected O, but got Ref
		//IL_06cc: Expected native int or pointer, but got O
		//IL_06e4: Expected O, but got Ref
		//IL_072e: Expected O, but got Ref
		//IL_0746: Expected native int or pointer, but got O
		//IL_075e: Expected O, but got Ref
		//IL_07a8: Expected O, but got Ref
		//IL_07c0: Expected native int or pointer, but got O
		//IL_07d8: Expected O, but got Ref
		//IL_0822: Expected O, but got Ref
		//IL_083a: Expected native int or pointer, but got O
		//IL_0852: Expected O, but got Ref
		//IL_089c: Expected O, but got Ref
		//IL_08b4: Expected native int or pointer, but got O
		//IL_08cc: Expected O, but got Ref
		//IL_0916: Expected O, but got Ref
		//IL_092e: Expected native int or pointer, but got O
		//IL_0946: Expected O, but got Ref
		//IL_0990: Expected O, but got Ref
		//IL_09a8: Expected native int or pointer, but got O
		//IL_09c0: Expected O, but got Ref
		//IL_0a0a: Expected O, but got Ref
		//IL_0a22: Expected native int or pointer, but got O
		//IL_0a47: Expected O, but got Ref
		//IL_0a84: Expected O, but got Ref
		//IL_0a9c: Expected native int or pointer, but got O
		//IL_0ab4: Expected O, but got Ref
		//IL_0afe: Expected O, but got Ref
		//IL_0b16: Expected native int or pointer, but got O
		//IL_0b2e: Expected O, but got Ref
		//IL_0b78: Expected O, but got Ref
		//IL_0b90: Expected native int or pointer, but got O
		//IL_0ba8: Expected O, but got Ref
		//IL_0bf2: Expected O, but got Ref
		//IL_0c0a: Expected native int or pointer, but got O
		//IL_0c22: Expected O, but got Ref
		//IL_0c6c: Expected O, but got Ref
		//IL_0c84: Expected native int or pointer, but got O
		//IL_0c9c: Expected O, but got Ref
		//IL_0cdb: Expected O, but got I4
		//IL_0d0b: Expected O, but got Ref
		//IL_0d4d: Expected O, but got Ref
		//IL_0d65: Expected native int or pointer, but got O
		//IL_0d7d: Expected O, but got Ref
		//IL_0dc7: Expected O, but got Ref
		//IL_0ddf: Expected native int or pointer, but got O
		//IL_0df7: Expected O, but got Ref
		//IL_0e41: Expected O, but got Ref
		//IL_0e59: Expected native int or pointer, but got O
		//IL_0e71: Expected O, but got Ref
		//IL_0ebb: Expected O, but got Ref
		//IL_0ed3: Expected native int or pointer, but got O
		//IL_0eeb: Expected O, but got Ref
		//IL_0f35: Expected O, but got Ref
		//IL_0f4d: Expected native int or pointer, but got O
		//IL_0f65: Expected O, but got Ref
		//IL_0fbb: Expected O, but got Ref
		//IL_0fc7: Expected native int or pointer, but got O
		//IL_0fdf: Expected O, but got Ref
		//IL_1029: Expected O, but got Ref
		//IL_1041: Expected native int or pointer, but got O
		//IL_1059: Expected O, but got Ref
		//IL_10a3: Expected O, but got Ref
		//IL_10bb: Expected native int or pointer, but got O
		//IL_10d3: Expected O, but got Ref
		//IL_111d: Expected O, but got Ref
		//IL_1135: Expected native int or pointer, but got O
		//IL_114d: Expected O, but got Ref
		//IL_1197: Expected O, but got Ref
		//IL_11af: Expected native int or pointer, but got O
		//IL_11c7: Expected O, but got Ref
		//IL_1211: Expected O, but got Ref
		//IL_1229: Expected native int or pointer, but got O
		//IL_1241: Expected O, but got Ref
		//IL_1291: Expected O, but got Ref
		//IL_12a3: Expected native int or pointer, but got O
		//IL_12bb: Expected O, but got Ref
		//IL_1305: Expected O, but got Ref
		//IL_131d: Expected native int or pointer, but got O
		//IL_1335: Expected O, but got Ref
		//IL_137f: Expected O, but got Ref
		//IL_1397: Expected native int or pointer, but got O
		//IL_13af: Expected O, but got Ref
		//IL_13f9: Expected O, but got Ref
		//IL_1411: Expected native int or pointer, but got O
		//IL_1429: Expected O, but got Ref
		//IL_1473: Expected O, but got Ref
		//IL_148b: Expected native int or pointer, but got O
		//IL_14a3: Expected O, but got Ref
		//IL_14ed: Expected O, but got Ref
		//IL_1505: Expected native int or pointer, but got O
		//IL_151d: Expected O, but got Ref
		//IL_1567: Expected O, but got Ref
		//IL_157f: Expected native int or pointer, but got O
		//IL_1597: Expected O, but got Ref
		//IL_15e1: Expected O, but got Ref
		//IL_15f9: Expected native int or pointer, but got O
		//IL_1611: Expected O, but got Ref
		//IL_165b: Expected O, but got Ref
		//IL_1673: Expected native int or pointer, but got O
		//IL_168b: Expected O, but got Ref
		//IL_16d5: Expected O, but got Ref
		//IL_16ed: Expected native int or pointer, but got O
		//IL_1705: Expected O, but got Ref
		//IL_174f: Expected O, but got Ref
		//IL_1767: Expected native int or pointer, but got O
		//IL_177f: Expected O, but got Ref
		//IL_17c9: Expected O, but got Ref
		//IL_17e1: Expected native int or pointer, but got O
		//IL_17f9: Expected O, but got Ref
		//IL_1843: Expected O, but got Ref
		//IL_185b: Expected native int or pointer, but got O
		//IL_1873: Expected O, but got Ref
		//IL_18bd: Expected O, but got Ref
		//IL_18d5: Expected native int or pointer, but got O
		//IL_18ed: Expected O, but got Ref
		//IL_1937: Expected O, but got Ref
		//IL_194f: Expected native int or pointer, but got O
		//IL_1967: Expected O, but got Ref
		//IL_19b1: Expected O, but got Ref
		//IL_19c9: Expected native int or pointer, but got O
		//IL_19e1: Expected O, but got Ref
		//IL_1a2b: Expected O, but got Ref
		//IL_1a43: Expected native int or pointer, but got O
		//IL_1a5b: Expected O, but got Ref
		//IL_1ab1: Expected O, but got Ref
		//IL_1abd: Expected native int or pointer, but got O
		//IL_1ad5: Expected O, but got Ref
		//IL_1b1f: Expected O, but got Ref
		//IL_1b37: Expected native int or pointer, but got O
		//IL_1b4f: Expected O, but got Ref
		//IL_1b99: Expected O, but got Ref
		//IL_1bb1: Expected native int or pointer, but got O
		//IL_1bc9: Expected O, but got Ref
		//IL_1c13: Expected O, but got Ref
		//IL_1c2b: Expected native int or pointer, but got O
		//IL_1c43: Expected O, but got Ref
		//IL_1c8d: Expected O, but got Ref
		//IL_1ca5: Expected native int or pointer, but got O
		//IL_1cbd: Expected O, but got Ref
		//IL_1d07: Expected O, but got Ref
		//IL_1d1f: Expected native int or pointer, but got O
		//IL_1d37: Expected O, but got Ref
		//IL_1d8d: Expected O, but got Ref
		//IL_1d99: Expected native int or pointer, but got O
		//IL_1db1: Expected O, but got Ref
		//IL_1dfb: Expected O, but got Ref
		//IL_1e13: Expected native int or pointer, but got O
		//IL_1e2b: Expected O, but got Ref
		//IL_1e75: Expected O, but got Ref
		//IL_1e8d: Expected native int or pointer, but got O
		//IL_1ea5: Expected O, but got Ref
		//IL_1eef: Expected O, but got Ref
		//IL_1f07: Expected native int or pointer, but got O
		//IL_1f1f: Expected O, but got Ref
		//IL_1f69: Expected O, but got Ref
		//IL_1f81: Expected native int or pointer, but got O
		//IL_1f99: Expected O, but got Ref
		//IL_1fe3: Expected O, but got Ref
		//IL_1ffb: Expected native int or pointer, but got O
		//IL_2013: Expected O, but got Ref
		//IL_205d: Expected O, but got Ref
		//IL_2075: Expected native int or pointer, but got O
		//IL_208d: Expected O, but got Ref
		//IL_20d7: Expected O, but got Ref
		//IL_20ef: Expected native int or pointer, but got O
		//IL_2107: Expected O, but got Ref
		//IL_2151: Expected O, but got Ref
		//IL_2169: Expected native int or pointer, but got O
		//IL_2181: Expected O, but got Ref
		//IL_21cb: Expected O, but got Ref
		//IL_21e3: Expected native int or pointer, but got O
		//IL_21fb: Expected O, but got Ref
		//IL_2245: Expected O, but got Ref
		//IL_225d: Expected native int or pointer, but got O
		//IL_2275: Expected O, but got Ref
		//IL_22bf: Expected O, but got Ref
		//IL_22d7: Expected native int or pointer, but got O
		//IL_22fc: Expected O, but got Ref
		//IL_2339: Expected O, but got Ref
		//IL_2351: Expected native int or pointer, but got O
		//IL_2369: Expected O, but got Ref
		//IL_23b3: Expected O, but got Ref
		//IL_23cb: Expected native int or pointer, but got O
		//IL_23e3: Expected O, but got Ref
		//IL_242d: Expected O, but got Ref
		//IL_2445: Expected native int or pointer, but got O
		//IL_245d: Expected O, but got Ref
		//IL_24a7: Expected O, but got Ref
		//IL_24bf: Expected native int or pointer, but got O
		//IL_24d7: Expected O, but got Ref
		//IL_2521: Expected O, but got Ref
		//IL_2539: Expected native int or pointer, but got O
		//IL_2551: Expected O, but got Ref
		//IL_259b: Expected O, but got Ref
		//IL_25b3: Expected native int or pointer, but got O
		//IL_25cb: Expected O, but got Ref
		//IL_2615: Expected O, but got Ref
		//IL_262d: Expected native int or pointer, but got O
		//IL_2645: Expected O, but got Ref
		//IL_268f: Expected O, but got Ref
		//IL_26a7: Expected native int or pointer, but got O
		//IL_26bf: Expected O, but got Ref
		//IL_2709: Expected O, but got Ref
		//IL_2721: Expected native int or pointer, but got O
		//IL_2739: Expected O, but got Ref
		//IL_2783: Expected O, but got Ref
		//IL_279b: Expected native int or pointer, but got O
		//IL_27b3: Expected O, but got Ref
		//IL_27fd: Expected O, but got Ref
		//IL_2815: Expected native int or pointer, but got O
		//IL_282d: Expected O, but got Ref
		//IL_2883: Expected O, but got Ref
		//IL_288f: Expected native int or pointer, but got O
		//IL_28a7: Expected O, but got Ref
		//IL_28f1: Expected O, but got Ref
		//IL_2909: Expected native int or pointer, but got O
		//IL_2921: Expected O, but got Ref
		//IL_296b: Expected O, but got Ref
		//IL_2983: Expected native int or pointer, but got O
		//IL_299b: Expected O, but got Ref
		//IL_29e5: Expected O, but got Ref
		//IL_29fd: Expected native int or pointer, but got O
		//IL_2a15: Expected O, but got Ref
		//IL_2a5f: Expected O, but got Ref
		//IL_2a77: Expected native int or pointer, but got O
		//IL_2a8f: Expected O, but got Ref
		//IL_2ad9: Expected O, but got Ref
		//IL_2af1: Expected native int or pointer, but got O
		//IL_2b09: Expected O, but got Ref
		//IL_2b5f: Expected O, but got Ref
		//IL_2b6b: Expected native int or pointer, but got O
		//IL_2b83: Expected O, but got Ref
		//IL_2bcd: Expected O, but got Ref
		//IL_2be5: Expected native int or pointer, but got O
		//IL_2bfd: Expected O, but got Ref
		//IL_2c47: Expected O, but got Ref
		//IL_2c5f: Expected native int or pointer, but got O
		//IL_2c77: Expected O, but got Ref
		//IL_2cc1: Expected O, but got Ref
		//IL_2cd9: Expected native int or pointer, but got O
		//IL_2cf1: Expected O, but got Ref
		//IL_2d3b: Expected O, but got Ref
		//IL_2d53: Expected native int or pointer, but got O
		//IL_2d6b: Expected O, but got Ref
		//IL_2db5: Expected O, but got Ref
		//IL_2dcd: Expected native int or pointer, but got O
		//IL_2de5: Expected O, but got Ref
		//IL_2e2f: Expected O, but got Ref
		//IL_2e47: Expected native int or pointer, but got O
		//IL_2e5f: Expected O, but got Ref
		//IL_2ea9: Expected O, but got Ref
		//IL_2ec1: Expected native int or pointer, but got O
		//IL_2ed9: Expected O, but got Ref
		//IL_2f23: Expected O, but got Ref
		//IL_2f3b: Expected native int or pointer, but got O
		//IL_2f53: Expected O, but got Ref
		//IL_2f9d: Expected O, but got Ref
		//IL_2fb5: Expected native int or pointer, but got O
		//IL_2fcd: Expected O, but got Ref
		//IL_3017: Expected O, but got Ref
		//IL_302f: Expected native int or pointer, but got O
		//IL_3047: Expected O, but got Ref
		//IL_3091: Expected O, but got Ref
		//IL_30a9: Expected native int or pointer, but got O
		//IL_30ce: Expected O, but got Ref
		//IL_310b: Expected O, but got Ref
		//IL_3123: Expected native int or pointer, but got O
		//IL_313b: Expected O, but got Ref
		//IL_3185: Expected O, but got Ref
		//IL_319d: Expected native int or pointer, but got O
		//IL_31b5: Expected O, but got Ref
		//IL_31ff: Expected O, but got Ref
		//IL_3217: Expected native int or pointer, but got O
		//IL_322f: Expected O, but got Ref
		//IL_3279: Expected O, but got Ref
		//IL_3291: Expected native int or pointer, but got O
		//IL_32a9: Expected O, but got Ref
		//IL_32f3: Expected O, but got Ref
		//IL_330b: Expected native int or pointer, but got O
		//IL_3323: Expected O, but got Ref
		//IL_336d: Expected O, but got Ref
		//IL_3385: Expected native int or pointer, but got O
		//IL_339d: Expected O, but got Ref
		//IL_33e7: Expected O, but got Ref
		//IL_33ff: Expected native int or pointer, but got O
		//IL_3417: Expected O, but got Ref
		(Key, UniversalKeyCode) tuple2 = default((Key, UniversalKeyCode));
		(Key, UniversalKeyCode) tuple = ((Key, UniversalKeyCode))(&tuple2);
		_lastMouseWheelChangeFrame = -1;
		List<UniversalKeyCode> tmpUniversalKeyResults = new List<UniversalKeyCode>();
		_tmpUniversalKeyResults = tmpUniversalKeyResults;
		Dictionary<UniversalKeyCode, string> dictionary = new Dictionary<UniversalKeyCode, string>();
		keyNameDictionary = dictionary;
		List<(Key, UniversalKeyCode)> list = new List<(Key, UniversalKeyCode)>();
		UniversalKeyCode item = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item2 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		(Key, UniversalKeyCode) tuple3 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 728));
		_ = 0;
		_ = 0;
		_ = 0;
		*((Key, UniversalKeyCode)*)(nint)tuple3 = (item2, item);
		(Key, UniversalKeyCode) item3 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+2D8]");
		_ = 0;
		list.Add(item3);
		UniversalKeyCode item4 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item5 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 106;
		_ = 1;
		(Key, UniversalKeyCode) tuple4 = (item5, item4);
		(Key, UniversalKeyCode) item6 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item6);
		UniversalKeyCode item7 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item8 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 103;
		_ = 2;
		(Key, UniversalKeyCode) tuple5 = (item8, item7);
		(Key, UniversalKeyCode) item9 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item9);
		UniversalKeyCode item10 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item11 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 101;
		_ = 3;
		(Key, UniversalKeyCode) tuple6 = (item11, item10);
		(Key, UniversalKeyCode) item12 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item12);
		UniversalKeyCode item13 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item14 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 144;
		_ = 4;
		(Key, UniversalKeyCode) tuple7 = (item14, item13);
		(Key, UniversalKeyCode) item15 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item15);
		UniversalKeyCode item16 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item17 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 113;
		_ = 5;
		(Key, UniversalKeyCode) tuple8 = (item17, item16);
		(Key, UniversalKeyCode) item18 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item18);
		UniversalKeyCode item19 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		_ = 133;
		_ = 6;
		Key item20 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		(Key, UniversalKeyCode) tuple9 = (item20, item19);
		(Key, UniversalKeyCode) item21 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item21);
		UniversalKeyCode item22 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item23 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 118;
		_ = 7;
		(Key, UniversalKeyCode) tuple10 = (item23, item22);
		(Key, UniversalKeyCode) item24 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item24);
		UniversalKeyCode item25 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item26 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 120;
		_ = 8;
		(Key, UniversalKeyCode) tuple11 = (item26, item25);
		(Key, UniversalKeyCode) item27 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item27);
		UniversalKeyCode item28 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item29 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 121;
		_ = 9;
		(Key, UniversalKeyCode) tuple12 = (item29, item28);
		(Key, UniversalKeyCode) item30 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item30);
		UniversalKeyCode item31 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item32 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 140;
		_ = 10;
		(Key, UniversalKeyCode) tuple13 = (item32, item31);
		(Key, UniversalKeyCode) item33 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item33);
		UniversalKeyCode item34 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item35 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 139;
		_ = 11;
		(Key, UniversalKeyCode) tuple14 = (item35, item34);
		(Key, UniversalKeyCode) item36 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item36);
		_ = 141;
		UniversalKeyCode item37 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item38 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 12;
		(Key, UniversalKeyCode) tuple15 = (item38, item37);
		(Key, UniversalKeyCode) item39 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item39);
		UniversalKeyCode item40 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item41 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple16 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 128));
		_ = 119;
		_ = 13;
		*((Key, UniversalKeyCode)*)(nint)tuple16 = (item41, item40);
		(Key, UniversalKeyCode) item42 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-80]");
		_ = 0;
		list.Add(item42);
		UniversalKeyCode item43 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item44 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple17 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 120));
		_ = 135;
		_ = 14;
		*((Key, UniversalKeyCode)*)(nint)tuple17 = (item44, item43);
		(Key, UniversalKeyCode) item45 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-78]");
		_ = 0;
		list.Add(item45);
		UniversalKeyCode item46 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item47 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple18 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 112));
		_ = 145;
		_ = 15;
		*((Key, UniversalKeyCode)*)(nint)tuple18 = (item47, item46);
		(Key, UniversalKeyCode) item48 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-70]");
		_ = 0;
		list.Add(item48);
		UniversalKeyCode item49 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item50 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple19 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 104));
		_ = 146;
		_ = 16;
		*((Key, UniversalKeyCode)*)(nint)tuple19 = (item50, item49);
		(Key, UniversalKeyCode) item51 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-68]");
		_ = 0;
		list.Add(item51);
		UniversalKeyCode item52 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item53 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple20 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 96));
		_ = 147;
		_ = 17;
		*((Key, UniversalKeyCode)*)(nint)tuple20 = (item53, item52);
		(Key, UniversalKeyCode) item54 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-60]");
		_ = 0;
		list.Add(item54);
		UniversalKeyCode item55 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item56 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple21 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 88));
		_ = 148;
		_ = 18;
		*((Key, UniversalKeyCode)*)(nint)tuple21 = (item56, item55);
		(Key, UniversalKeyCode) item57 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-58]");
		_ = 0;
		list.Add(item57);
		UniversalKeyCode item58 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item59 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple22 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 80));
		_ = 149;
		_ = 19;
		*((Key, UniversalKeyCode)*)(nint)tuple22 = (item59, item58);
		(Key, UniversalKeyCode) item60 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-50]");
		_ = 0;
		list.Add(item60);
		UniversalKeyCode item61 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item62 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple23 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 72));
		_ = 150;
		_ = 20;
		*((Key, UniversalKeyCode)*)(nint)tuple23 = (item62, item61);
		(Key, UniversalKeyCode) item63 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-48]");
		_ = 0;
		list.Add(item63);
		UniversalKeyCode item64 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item65 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple24 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 64));
		_ = 151;
		_ = 21;
		*((Key, UniversalKeyCode)*)(nint)tuple24 = (item65, item64);
		(Key, UniversalKeyCode) item66 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-40]");
		_ = 0;
		list.Add(item66);
		UniversalKeyCode item67 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item68 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple25 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 56));
		_ = 152;
		_ = 22;
		*((Key, UniversalKeyCode)*)(nint)tuple25 = (item68, item67);
		(Key, UniversalKeyCode) item69 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-38]");
		_ = 0;
		list.Add(item69);
		UniversalKeyCode item70 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item71 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple26 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 48));
		_ = 153;
		_ = 23;
		*((Key, UniversalKeyCode)*)(nint)tuple26 = (item71, item70);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-30]");
		_ = 0;
		(Key, UniversalKeyCode) item72 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item72);
		UniversalKeyCode item73 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item74 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple27 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 40));
		_ = 154;
		_ = 24;
		*((Key, UniversalKeyCode)*)(nint)tuple27 = (item74, item73);
		(Key, UniversalKeyCode) item75 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-28]");
		_ = 0;
		list.Add(item75);
		UniversalKeyCode item76 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item77 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple28 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 32));
		_ = 155;
		_ = 25;
		*((Key, UniversalKeyCode)*)(nint)tuple28 = (item77, item76);
		(Key, UniversalKeyCode) item78 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-20]");
		_ = 0;
		list.Add(item78);
		UniversalKeyCode item79 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item80 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple29 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 24));
		_ = 156;
		_ = 26;
		*((Key, UniversalKeyCode)*)(nint)tuple29 = (item80, item79);
		(Key, UniversalKeyCode) item81 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-18]");
		_ = 0;
		list.Add(item81);
		UniversalKeyCode item82 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item83 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple30 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 16));
		_ = 157;
		_ = 27;
		*((Key, UniversalKeyCode)*)(nint)tuple30 = (item83, item82);
		(Key, UniversalKeyCode) item84 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-10]");
		_ = 0;
		list.Add(item84);
		UniversalKeyCode item85 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item86 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple31 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref tuple2, 8));
		_ = 158;
		_ = 28;
		*((Key, UniversalKeyCode)*)(nint)tuple31 = (item86, item85);
		(Key, UniversalKeyCode) item87 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)-8]");
		_ = 0;
		list.Add(item87);
		UniversalKeyCode item88 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item89 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		tuple = ((Key, UniversalKeyCode))0;
		_ = 159;
		_ = 29;
		tuple2 = (item89, item88);
		(Key, UniversalKeyCode) item90 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item90);
		UniversalKeyCode item91 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item92 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple32 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 8));
		_ = 160;
		_ = 30;
		*((Key, UniversalKeyCode)*)(nint)tuple32 = (item92, item91);
		(Key, UniversalKeyCode) item93 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+8]");
		_ = 0;
		list.Add(item93);
		UniversalKeyCode item94 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item95 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple33 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 16));
		_ = 161;
		_ = 31;
		*((Key, UniversalKeyCode)*)(nint)tuple33 = (item95, item94);
		(Key, UniversalKeyCode) item96 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
		_ = 0;
		list.Add(item96);
		UniversalKeyCode item97 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item98 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple34 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 24));
		_ = 162;
		_ = 32;
		*((Key, UniversalKeyCode)*)(nint)tuple34 = (item98, item97);
		(Key, UniversalKeyCode) item99 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
		_ = 0;
		list.Add(item99);
		UniversalKeyCode item100 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item101 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple35 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 32));
		_ = 163;
		_ = 33;
		*((Key, UniversalKeyCode)*)(nint)tuple35 = (item101, item100);
		(Key, UniversalKeyCode) item102 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+20]");
		_ = 0;
		list.Add(item102);
		UniversalKeyCode item103 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item104 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple36 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 40));
		_ = 164;
		_ = 34;
		*((Key, UniversalKeyCode)*)(nint)tuple36 = (item104, item103);
		(Key, UniversalKeyCode) item105 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+28]");
		_ = 0;
		list.Add(item105);
		_ = 0;
		_ = 165;
		_ = 35;
		UniversalKeyCode item106 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item107 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		(Key, UniversalKeyCode) tuple37 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 48));
		*((Key, UniversalKeyCode)*)(nint)tuple37 = (item107, item106);
		(Key, UniversalKeyCode) item108 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+30]");
		_ = 0;
		list.Add(item108);
		UniversalKeyCode item109 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item110 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple38 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 56));
		_ = 166;
		_ = 36;
		*((Key, UniversalKeyCode)*)(nint)tuple38 = (item110, item109);
		(Key, UniversalKeyCode) item111 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+38]");
		_ = 0;
		list.Add(item111);
		UniversalKeyCode item112 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item113 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple39 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 64));
		_ = 167;
		_ = 37;
		*((Key, UniversalKeyCode)*)(nint)tuple39 = (item113, item112);
		(Key, UniversalKeyCode) item114 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+40]");
		_ = 0;
		list.Add(item114);
		UniversalKeyCode item115 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item116 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple40 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 72));
		_ = 168;
		_ = 38;
		*((Key, UniversalKeyCode)*)(nint)tuple40 = (item116, item115);
		(Key, UniversalKeyCode) item117 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+48]");
		_ = 0;
		list.Add(item117);
		UniversalKeyCode item118 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item119 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple41 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 80));
		_ = 169;
		_ = 39;
		*((Key, UniversalKeyCode)*)(nint)tuple41 = (item119, item118);
		(Key, UniversalKeyCode) item120 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+50]");
		_ = 0;
		list.Add(item120);
		UniversalKeyCode item121 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item122 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple42 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 88));
		_ = 170;
		_ = 40;
		*((Key, UniversalKeyCode)*)(nint)tuple42 = (item122, item121);
		(Key, UniversalKeyCode) item123 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+58]");
		_ = 0;
		list.Add(item123);
		_ = 0;
		UniversalKeyCode item124 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item125 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 122;
		(Key, UniversalKeyCode) tuple43 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 96));
		_ = 50;
		*((Key, UniversalKeyCode)*)(nint)tuple43 = (item125, item124);
		(Key, UniversalKeyCode) item126 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+60]");
		_ = 0;
		list.Add(item126);
		UniversalKeyCode item127 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item128 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple44 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 104));
		_ = 123;
		_ = 41;
		*((Key, UniversalKeyCode)*)(nint)tuple44 = (item128, item127);
		(Key, UniversalKeyCode) item129 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+68]");
		_ = 0;
		list.Add(item129);
		UniversalKeyCode item130 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item131 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple45 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 112));
		_ = 124;
		_ = 42;
		*((Key, UniversalKeyCode)*)(nint)tuple45 = (item131, item130);
		(Key, UniversalKeyCode) item132 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+70]");
		_ = 0;
		list.Add(item132);
		UniversalKeyCode item133 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item134 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple46 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 120));
		_ = 125;
		_ = 43;
		*((Key, UniversalKeyCode)*)(nint)tuple46 = (item134, item133);
		(Key, UniversalKeyCode) item135 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+78]");
		_ = 0;
		list.Add(item135);
		UniversalKeyCode item136 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item137 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple47 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 128));
		_ = 126;
		_ = 44;
		*((Key, UniversalKeyCode)*)(nint)tuple47 = (item137, item136);
		(Key, UniversalKeyCode) item138 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+80]");
		_ = 0;
		list.Add(item138);
		UniversalKeyCode item139 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item140 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple48 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 136));
		_ = 127;
		_ = 45;
		*((Key, UniversalKeyCode)*)(nint)tuple48 = (item140, item139);
		(Key, UniversalKeyCode) item141 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+88]");
		_ = 0;
		list.Add(item141);
		UniversalKeyCode item142 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item143 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple49 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 144));
		_ = 128;
		_ = 46;
		*((Key, UniversalKeyCode)*)(nint)tuple49 = (item143, item142);
		(Key, UniversalKeyCode) item144 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+90]");
		_ = 0;
		list.Add(item144);
		UniversalKeyCode item145 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item146 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple50 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 152));
		_ = 129;
		_ = 47;
		*((Key, UniversalKeyCode)*)(nint)tuple50 = (item146, item145);
		(Key, UniversalKeyCode) item147 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+98]");
		_ = 0;
		list.Add(item147);
		UniversalKeyCode item148 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item149 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple51 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 160));
		_ = 130;
		_ = 48;
		*((Key, UniversalKeyCode)*)(nint)tuple51 = (item149, item148);
		(Key, UniversalKeyCode) item150 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+A0]");
		_ = 0;
		list.Add(item150);
		UniversalKeyCode item151 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item152 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple52 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 168));
		_ = 131;
		_ = 49;
		*((Key, UniversalKeyCode)*)(nint)tuple52 = (item152, item151);
		(Key, UniversalKeyCode) item153 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+A8]");
		_ = 0;
		list.Add(item153);
		UniversalKeyCode item154 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item155 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple53 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 176));
		_ = 218;
		_ = 51;
		*((Key, UniversalKeyCode)*)(nint)tuple53 = (item155, item154);
		(Key, UniversalKeyCode) item156 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+B0]");
		_ = 0;
		list.Add(item156);
		UniversalKeyCode item157 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item158 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple54 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 184));
		_ = 217;
		_ = 52;
		*((Key, UniversalKeyCode)*)(nint)tuple54 = (item158, item157);
		(Key, UniversalKeyCode) item159 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+B8]");
		_ = 0;
		list.Add(item159);
		UniversalKeyCode item160 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item161 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple55 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 192));
		_ = 222;
		_ = 53;
		*((Key, UniversalKeyCode)*)(nint)tuple55 = (item161, item160);
		(Key, UniversalKeyCode) item162 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+C0]");
		_ = 0;
		list.Add(item162);
		UniversalKeyCode item163 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item164 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple56 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 200));
		_ = 221;
		_ = 54;
		*((Key, UniversalKeyCode)*)(nint)tuple56 = (item164, item163);
		(Key, UniversalKeyCode) item165 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+C8]");
		_ = 0;
		list.Add(item165);
		UniversalKeyCode item166 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item167 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple57 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 208));
		_ = 220;
		_ = 55;
		*((Key, UniversalKeyCode)*)(nint)tuple57 = (item167, item166);
		(Key, UniversalKeyCode) item168 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+D0]");
		_ = 0;
		list.Add(item168);
		UniversalKeyCode item169 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item170 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple58 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 216));
		_ = 219;
		_ = 56;
		*((Key, UniversalKeyCode)*)(nint)tuple58 = (item170, item169);
		(Key, UniversalKeyCode) item171 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+D8]");
		_ = 0;
		list.Add(item171);
		UniversalKeyCode item172 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item173 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple59 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 224));
		_ = 224;
		_ = 57;
		*((Key, UniversalKeyCode)*)(nint)tuple59 = (item173, item172);
		(Key, UniversalKeyCode) item174 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+E0]");
		_ = 0;
		list.Add(item174);
		UniversalKeyCode item175 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item176 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		_ = 223;
		_ = 58;
		(Key, UniversalKeyCode) tuple60 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 232));
		*((Key, UniversalKeyCode)*)(nint)tuple60 = (item176, item175);
		(Key, UniversalKeyCode) item177 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+E8]");
		_ = 0;
		list.Add(item177);
		UniversalKeyCode item178 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item179 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple61 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 240));
		_ = 232;
		_ = 59;
		*((Key, UniversalKeyCode)*)(nint)tuple61 = (item179, item178);
		(Key, UniversalKeyCode) item180 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+F0]");
		_ = 0;
		list.Add(item180);
		UniversalKeyCode item181 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item182 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple62 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 248));
		_ = 105;
		_ = 60;
		*((Key, UniversalKeyCode)*)(nint)tuple62 = (item182, item181);
		(Key, UniversalKeyCode) item183 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+F8]");
		_ = 0;
		list.Add(item183);
		UniversalKeyCode item184 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item185 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple63 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 256));
		_ = 196;
		_ = 61;
		*((Key, UniversalKeyCode)*)(nint)tuple63 = (item185, item184);
		(Key, UniversalKeyCode) item186 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+100]");
		_ = 0;
		list.Add(item186);
		UniversalKeyCode item187 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item188 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple64 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 264));
		_ = 195;
		_ = 62;
		*((Key, UniversalKeyCode)*)(nint)tuple64 = (item188, item187);
		(Key, UniversalKeyCode) item189 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+108]");
		_ = 0;
		list.Add(item189);
		UniversalKeyCode item190 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item191 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple65 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 272));
		_ = 193;
		_ = 63;
		*((Key, UniversalKeyCode)*)(nint)tuple65 = (item191, item190);
		(Key, UniversalKeyCode) item192 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+110]");
		_ = 0;
		list.Add(item192);
		_ = 0;
		_ = 194;
		_ = 64;
		UniversalKeyCode item193 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item194 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		(Key, UniversalKeyCode) tuple66 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 280));
		*((Key, UniversalKeyCode)*)(nint)tuple66 = (item194, item193);
		(Key, UniversalKeyCode) item195 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+118]");
		_ = 0;
		list.Add(item195);
		UniversalKeyCode item196 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item197 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple67 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 288));
		_ = 100;
		_ = 65;
		*((Key, UniversalKeyCode)*)(nint)tuple67 = (item197, item196);
		(Key, UniversalKeyCode) item198 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+120]");
		_ = 0;
		list.Add(item198);
		UniversalKeyCode item199 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item200 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple68 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 296));
		_ = 201;
		_ = 66;
		*((Key, UniversalKeyCode)*)(nint)tuple68 = (item200, item199);
		(Key, UniversalKeyCode) item201 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+128]");
		_ = 0;
		list.Add(item201);
		UniversalKeyCode item202 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item203 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple69 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 304));
		_ = 200;
		_ = 67;
		*((Key, UniversalKeyCode)*)(nint)tuple69 = (item203, item202);
		(Key, UniversalKeyCode) item204 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+130]");
		_ = 0;
		list.Add(item204);
		UniversalKeyCode item205 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item206 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple70 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 312));
		_ = 198;
		_ = 68;
		*((Key, UniversalKeyCode)*)(nint)tuple70 = (item206, item205);
		(Key, UniversalKeyCode) item207 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+138]");
		_ = 0;
		list.Add(item207);
		UniversalKeyCode item208 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item209 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple71 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 320));
		_ = 199;
		_ = 69;
		*((Key, UniversalKeyCode)*)(nint)tuple71 = (item209, item208);
		(Key, UniversalKeyCode) item210 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+140]");
		_ = 0;
		list.Add(item210);
		UniversalKeyCode item211 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item212 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple72 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 328));
		_ = 197;
		_ = 70;
		*((Key, UniversalKeyCode)*)(nint)tuple72 = (item212, item211);
		(Key, UniversalKeyCode) item213 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+148]");
		_ = 0;
		list.Add(item213);
		UniversalKeyCode item214 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item215 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple73 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 336));
		_ = 175;
		_ = 71;
		*((Key, UniversalKeyCode)*)(nint)tuple73 = (item215, item214);
		(Key, UniversalKeyCode) item216 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+150]");
		_ = 0;
		list.Add(item216);
		UniversalKeyCode item217 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item218 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple74 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 344));
		_ = 215;
		_ = 72;
		*((Key, UniversalKeyCode)*)(nint)tuple74 = (item218, item217);
		(Key, UniversalKeyCode) item219 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+158]");
		_ = 0;
		list.Add(item219);
		UniversalKeyCode item220 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item221 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple75 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 352));
		_ = 214;
		_ = 73;
		*((Key, UniversalKeyCode)*)(nint)tuple75 = (item221, item220);
		(Key, UniversalKeyCode) item222 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+160]");
		_ = 0;
		list.Add(item222);
		UniversalKeyCode item223 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item224 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple76 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 360));
		_ = 229;
		_ = 74;
		*((Key, UniversalKeyCode)*)(nint)tuple76 = (item224, item223);
		(Key, UniversalKeyCode) item225 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+168]");
		_ = 0;
		list.Add(item225);
		UniversalKeyCode item226 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item227 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple77 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 368));
		_ = 216;
		_ = 75;
		*((Key, UniversalKeyCode)*)(nint)tuple77 = (item227, item226);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+170]");
		_ = 0;
		(Key, UniversalKeyCode) item228 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item228);
		UniversalKeyCode item229 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item230 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple78 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 376));
		_ = 104;
		_ = 76;
		*((Key, UniversalKeyCode)*)(nint)tuple78 = (item230, item229);
		(Key, UniversalKeyCode) item231 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+178]");
		_ = 0;
		list.Add(item231);
		UniversalKeyCode item232 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item233 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple79 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 384));
		_ = 191;
		_ = 77;
		*((Key, UniversalKeyCode)*)(nint)tuple79 = (item233, item232);
		(Key, UniversalKeyCode) item234 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+180]");
		_ = 0;
		list.Add(item234);
		UniversalKeyCode item235 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item236 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple80 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 392));
		_ = 187;
		_ = 78;
		*((Key, UniversalKeyCode)*)(nint)tuple80 = (item236, item235);
		(Key, UniversalKeyCode) item237 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+188]");
		_ = 0;
		list.Add(item237);
		UniversalKeyCode item238 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item239 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple81 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 400));
		_ = 188;
		_ = 79;
		*((Key, UniversalKeyCode)*)(nint)tuple81 = (item239, item238);
		(Key, UniversalKeyCode) item240 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+190]");
		_ = 0;
		list.Add(item240);
		UniversalKeyCode item241 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item242 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple82 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 408));
		_ = 190;
		_ = 80;
		*((Key, UniversalKeyCode)*)(nint)tuple82 = (item242, item241);
		(Key, UniversalKeyCode) item243 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+198]");
		_ = 0;
		list.Add(item243);
		UniversalKeyCode item244 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item245 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple83 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 416));
		_ = 189;
		_ = 81;
		*((Key, UniversalKeyCode)*)(nint)tuple83 = (item245, item244);
		(Key, UniversalKeyCode) item246 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1A0]");
		_ = 0;
		list.Add(item246);
		UniversalKeyCode item247 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item248 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple84 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 424));
		_ = 186;
		_ = 82;
		*((Key, UniversalKeyCode)*)(nint)tuple84 = (item248, item247);
		(Key, UniversalKeyCode) item249 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1A8]");
		_ = 0;
		list.Add(item249);
		UniversalKeyCode item250 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item251 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple85 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 432));
		_ = 192;
		_ = 83;
		*((Key, UniversalKeyCode)*)(nint)tuple85 = (item251, item250);
		(Key, UniversalKeyCode) item252 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1B0]");
		_ = 0;
		list.Add(item252);
		UniversalKeyCode item253 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item254 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple86 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 440));
		_ = 176;
		_ = 84;
		*((Key, UniversalKeyCode)*)(nint)tuple86 = (item254, item253);
		(Key, UniversalKeyCode) item255 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1B8]");
		_ = 0;
		list.Add(item255);
		UniversalKeyCode item256 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item257 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple87 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 448));
		_ = 177;
		_ = 85;
		*((Key, UniversalKeyCode)*)(nint)tuple87 = (item257, item256);
		(Key, UniversalKeyCode) item258 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C0]");
		_ = 0;
		list.Add(item258);
		UniversalKeyCode item259 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item260 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple88 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 456));
		_ = 178;
		_ = 86;
		*((Key, UniversalKeyCode)*)(nint)tuple88 = (item260, item259);
		(Key, UniversalKeyCode) item261 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C8]");
		_ = 0;
		list.Add(item261);
		UniversalKeyCode item262 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		_ = 0;
		_ = 179;
		_ = 87;
		Key item263 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		(Key, UniversalKeyCode) tuple89 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 464));
		*((Key, UniversalKeyCode)*)(nint)tuple89 = (item263, item262);
		(Key, UniversalKeyCode) item264 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1D0]");
		_ = 0;
		list.Add(item264);
		UniversalKeyCode item265 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item266 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple90 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 472));
		_ = 180;
		_ = 88;
		*((Key, UniversalKeyCode)*)(nint)tuple90 = (item266, item265);
		(Key, UniversalKeyCode) item267 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1D8]");
		_ = 0;
		list.Add(item267);
		UniversalKeyCode item268 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item269 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple91 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 480));
		_ = 181;
		_ = 89;
		*((Key, UniversalKeyCode)*)(nint)tuple91 = (item269, item268);
		(Key, UniversalKeyCode) item270 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1E0]");
		_ = 0;
		list.Add(item270);
		UniversalKeyCode item271 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item272 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple92 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 488));
		_ = 182;
		_ = 90;
		*((Key, UniversalKeyCode)*)(nint)tuple92 = (item272, item271);
		(Key, UniversalKeyCode) item273 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1E8]");
		_ = 0;
		list.Add(item273);
		UniversalKeyCode item274 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item275 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple93 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 496));
		_ = 183;
		_ = 91;
		*((Key, UniversalKeyCode)*)(nint)tuple93 = (item275, item274);
		(Key, UniversalKeyCode) item276 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1F0]");
		_ = 0;
		list.Add(item276);
		UniversalKeyCode item277 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item278 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple94 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 504));
		_ = 184;
		_ = 92;
		*((Key, UniversalKeyCode)*)(nint)tuple94 = (item278, item277);
		(Key, UniversalKeyCode) item279 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1F8]");
		_ = 0;
		list.Add(item279);
		_ = 0;
		_ = 185;
		UniversalKeyCode item280 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item281 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 93;
		(Key, UniversalKeyCode) tuple95 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 512));
		*((Key, UniversalKeyCode)*)(nint)tuple95 = (item281, item280);
		(Key, UniversalKeyCode) item282 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+200]");
		_ = 0;
		list.Add(item282);
		UniversalKeyCode item283 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item284 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple96 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 520));
		_ = 202;
		_ = 94;
		*((Key, UniversalKeyCode)*)(nint)tuple96 = (item284, item283);
		(Key, UniversalKeyCode) item285 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+208]");
		_ = 0;
		list.Add(item285);
		UniversalKeyCode item286 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item287 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple97 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 528));
		_ = 203;
		_ = 95;
		*((Key, UniversalKeyCode)*)(nint)tuple97 = (item287, item286);
		(Key, UniversalKeyCode) item288 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+210]");
		_ = 0;
		list.Add(item288);
		UniversalKeyCode item289 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item290 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple98 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 536));
		_ = 204;
		_ = 96;
		*((Key, UniversalKeyCode)*)(nint)tuple98 = (item290, item289);
		(Key, UniversalKeyCode) item291 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+218]");
		_ = 0;
		list.Add(item291);
		UniversalKeyCode item292 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item293 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple99 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 544));
		_ = 205;
		_ = 97;
		*((Key, UniversalKeyCode)*)(nint)tuple99 = (item293, item292);
		(Key, UniversalKeyCode) item294 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+220]");
		_ = 0;
		list.Add(item294);
		UniversalKeyCode item295 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item296 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple100 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 552));
		_ = 206;
		_ = 98;
		*((Key, UniversalKeyCode)*)(nint)tuple100 = (item296, item295);
		(Key, UniversalKeyCode) item297 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+228]");
		_ = 0;
		list.Add(item297);
		UniversalKeyCode item298 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item299 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple101 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 560));
		_ = 207;
		_ = 99;
		*((Key, UniversalKeyCode)*)(nint)tuple101 = (item299, item298);
		(Key, UniversalKeyCode) item300 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+230]");
		_ = 0;
		list.Add(item300);
		UniversalKeyCode item301 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item302 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple102 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 568));
		_ = 208;
		_ = 100;
		*((Key, UniversalKeyCode)*)(nint)tuple102 = (item302, item301);
		(Key, UniversalKeyCode) item303 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+238]");
		_ = 0;
		list.Add(item303);
		UniversalKeyCode item304 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item305 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple103 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 576));
		_ = 209;
		_ = 101;
		*((Key, UniversalKeyCode)*)(nint)tuple103 = (item305, item304);
		(Key, UniversalKeyCode) item306 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+240]");
		_ = 0;
		list.Add(item306);
		UniversalKeyCode item307 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item308 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple104 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 584));
		_ = 210;
		_ = 102;
		*((Key, UniversalKeyCode)*)(nint)tuple104 = (item308, item307);
		(Key, UniversalKeyCode) item309 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+248]");
		_ = 0;
		list.Add(item309);
		UniversalKeyCode item310 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item311 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple105 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 592));
		_ = 211;
		_ = 103;
		*((Key, UniversalKeyCode)*)(nint)tuple105 = (item311, item310);
		(Key, UniversalKeyCode) item312 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+250]");
		_ = 0;
		list.Add(item312);
		UniversalKeyCode item313 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item314 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple106 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 600));
		_ = 212;
		_ = 104;
		*((Key, UniversalKeyCode)*)(nint)tuple106 = (item314, item313);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+258]");
		_ = 0;
		(Key, UniversalKeyCode) item315 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		list.Add(item315);
		UniversalKeyCode item316 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item317 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple107 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 608));
		_ = 213;
		_ = 105;
		*((Key, UniversalKeyCode)*)(nint)tuple107 = (item317, item316);
		(Key, UniversalKeyCode) item318 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+260]");
		_ = 0;
		list.Add(item318);
		UniversalKeyCode item319 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item320 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple108 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 616));
		_ = 1;
		_ = 106;
		*((Key, UniversalKeyCode)*)(nint)tuple108 = (item320, item319);
		(Key, UniversalKeyCode) item321 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+268]");
		_ = 0;
		list.Add(item321);
		UniversalKeyCode item322 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item323 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple109 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 624));
		_ = 1;
		_ = 107;
		*((Key, UniversalKeyCode)*)(nint)tuple109 = (item323, item322);
		(Key, UniversalKeyCode) item324 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+270]");
		_ = 0;
		list.Add(item324);
		UniversalKeyCode item325 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item326 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple110 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 632));
		_ = 1;
		_ = 108;
		*((Key, UniversalKeyCode)*)(nint)tuple110 = (item326, item325);
		(Key, UniversalKeyCode) item327 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+278]");
		_ = 0;
		list.Add(item327);
		UniversalKeyCode item328 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item329 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple111 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 640));
		_ = 1;
		_ = 109;
		*((Key, UniversalKeyCode)*)(nint)tuple111 = (item329, item328);
		(Key, UniversalKeyCode) item330 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+280]");
		_ = 0;
		list.Add(item330);
		UniversalKeyCode item331 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item332 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple112 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 648));
		_ = 1;
		_ = 110;
		*((Key, UniversalKeyCode)*)(nint)tuple112 = (item332, item331);
		(Key, UniversalKeyCode) item333 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+288]");
		_ = 0;
		list.Add(item333);
		UniversalKeyCode item334 = (UniversalKeyCode)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Key item335 = (Key)(int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 720));
		_ = 0;
		(Key, UniversalKeyCode) tuple113 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 656));
		_ = 1;
		_ = 111;
		*((Key, UniversalKeyCode)*)(nint)tuple113 = (item335, item334);
		(Key, UniversalKeyCode) item336 = ((Key, UniversalKeyCode))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref tuple2, 712));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (System.ValueTuple`2<UnityEngine.InputSystem.Key, Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+290]");
		_ = 0;
		list.Add(item336);
		_inputSystemKeyMap = list;
	}
}
