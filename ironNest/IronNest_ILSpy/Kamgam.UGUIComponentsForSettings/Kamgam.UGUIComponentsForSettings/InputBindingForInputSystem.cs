using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cpp2ILInjected;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Kamgam.UGUIComponentsForSettings;

[Serializable]
public class InputBindingForInputSystem : IInputBindingForGUI
{
	public enum LocalConfigBehaviours
	{
		OverrideGlobalIfLocalExists,
		AppendLocalToGlobal
	}

	public delegate bool CheckBindingPathDelegate(string previousPath, string path);

	public delegate Action OnBeforeRebindStartDelegate(InputActionRebindingExtensions.RebindingOperation rebindingOperation);

	private sealed class _003C_003Ec__DisplayClass31_0
	{
		public string[] abortControlPaths;

		public InputBindingForInputSystem _003C_003E4__this;

		internal void _003CStartListening_003Eb__0(InputActionRebindingExtensions.RebindingOperation operation)
		{
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Expected O, but got Unknown
			//IL_0102: Expected O, but got I4
			//IL_010b: Expected O, but got I4
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_0072: Expected O, but got I4
			//IL_007b: Expected O, but got I4
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Expected O, but got Unknown
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Expected O, but got Unknown
			//IL_020d: Expected O, but got I4
			//IL_0215: Unknown result type (might be due to invalid IL or missing references)
			//IL_021a: Expected O, but got Unknown
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_0227: Expected O, but got Unknown
			//IL_028d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0292: Expected O, but got Unknown
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Expected O, but got Unknown
			Version version = InputSystem.version;
			Version value = Version.Parse("1.4.1");
			int num = version.CompareTo(value);
			string[] array = abortControlPaths;
			InputBindingForInputSystem inputBindingForInputSystem;
			if (num >= 0)
			{
				object obj = abortControlPaths + 32;
				object obj2 = 0;
				object obj3 = 0;
				while (true)
				{
					if ((nint)obj3 < array.Length)
					{
						InputControl selectedControl = operation.selectedControl;
						if (InputControlPath.Matches((string)obj, selectedControl))
						{
							break;
						}
						obj2++;
						obj += 8;
						obj3 = obj2;
						continue;
					}
					return;
				}
				inputBindingForInputSystem = _003C_003E4__this;
			}
			else
			{
				object obj4 = abortControlPaths + 32;
				object obj5 = 0;
				object obj6 = 0;
				object obj10 = default(object);
				while (true)
				{
					if ((nint)obj6 >= array.Length)
					{
						return;
					}
					char c = ((string)obj4).get_Chars(0);
					bool flag = c != '<';
					string input = (string)obj4;
					if (!flag)
					{
						string text = "/" + (string)obj4;
						input = text;
					}
					string text2 = Regex.Replace(input, "[><{}*]+", "");
					string text3 = text2.ToLower();
					InputControl selectedControl2 = operation.selectedControl;
					string path = selectedControl2.path;
					string text4 = path.ToLower();
					int num2 = text4.IndexOf("/");
					int num3 = text3.IndexOf("/");
					object obj7 = text4._stringLength - text3._stringLength;
					object obj8 = obj7 - num2;
					object obj9 = num3 + obj8;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180439880");
					if ((nint)obj10 <= 1)
					{
						InputControl selectedControl3 = operation.selectedControl;
						if (InputControlPath.Matches((string)obj4, selectedControl3))
						{
							break;
						}
					}
					obj5++;
					obj4 += 8;
					obj6 = obj5;
				}
				inputBindingForInputSystem = _003C_003E4__this;
			}
			inputBindingForInputSystem._rebindingOperation.Cancel();
		}

		internal unsafe void _003CStartListening_003Eb__1(InputActionRebindingExtensions.RebindingOperation rebindingOp, string path)
		{
			//IL_05ad: Expected O, but got I4
			//IL_006b: Expected O, but got Ref
			//IL_0074: Expected O, but got I4
			//IL_0085: Expected O, but got Ref
			//IL_0614: Unknown result type (might be due to invalid IL or missing references)
			//IL_0619: Expected O, but got Unknown
			//IL_00c9: Expected O, but got I4
			//IL_0785: Expected O, but got I
			//IL_016a: Expected O, but got I4
			//IL_08ab: Expected I, but got O
			//IL_0117: Expected O, but got I
			//IL_0120: Expected O, but got I4
			//IL_017f: Expected I, but got O
			//IL_018d: Expected I, but got O
			//IL_019d: Expected O, but got I
			//IL_04ef: Expected O, but got I
			//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fd: Expected O, but got Unknown
			//IL_0227: Expected I, but got O
			//IL_0237: Expected O, but got I
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Expected O, but got Unknown
			//IL_02bb: Expected I, but got O
			//IL_02cb: Expected O, but got I
			//IL_02f7: Expected I, but got O
			//IL_01d9: Expected O, but got I
			//IL_020b: Expected I, but got O
			//IL_0273: Expected O, but got I
			//IL_029f: Expected O, but got I
			//IL_031d: Expected O, but got I
			//IL_034a: Expected I, but got O
			//IL_035a: Expected O, but got I
			//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e1: Expected O, but got Unknown
			//IL_03bd: Expected I, but got O
			//IL_0525: Expected I, but got O
			//IL_048e: Expected I, but got O
			//IL_04b7: Expected I, but got O
			InputBindingForInputSystem inputBindingForInputSystem = _003C_003E4__this;
			bool flag = _003C_003E4__this == null;
			_003C_003Ec__DisplayClass31_0 obj = this;
			nint num = default(nint);
			string text9 = default(string);
			string text10;
			_003C_003Ec__DisplayClass31_0 obj17;
			if (!flag)
			{
				if (inputBindingForInputSystem.AllowComposite)
				{
					bool flag2 = rebindingOp == null;
					obj = this;
					if (!flag2)
					{
						InputControlList<InputControl> candidates = rebindingOp.m_Candidates;
						InputControlList<InputControl> inputControlList = default(InputControlList<InputControl>);
						IEnumerator<InputControl> enumerator = inputControlList.GetEnumerator();
						string text = default(string);
						object obj2 = (object)(&text);
						object obj3 = 0;
						string text2 = "";
						InputControlList<InputControl> inputControlList2 = (InputControlList<InputControl>)(&inputControlList);
						object obj4 = default(object);
						InputControl inputControl = default(InputControl);
						string text6 = default(string);
						object obj16 = default(object);
						while (true)
						{
							object obj6;
							object obj13;
							if (text != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
								if (obj4 == null)
								{
									break;
								}
								bool flag3 = text == null;
								inputControlList2 = (InputControlList<InputControl>)0;
								if (!flag3)
								{
									object obj5 = text;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v9+12E]");
									if ((nint)0 >= (nint)0)
									{
										goto IL_0157;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v9+B0]");
									obj6 = 0;
									object obj7 = 0;
									while (true)
									{
										object obj8 = obj7 + obj7;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v16+v796 @ rax_v61*8]");
										if (0 == (nint)typeof(IEnumerator<InputControl>))
										{
											break;
										}
										obj7++;
										object obj9 = obj7;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v9+12E]");
										if ((nint)obj9 < 0)
										{
											continue;
										}
										goto IL_0157;
									}
									object obj10 = obj7 + obj7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v16+8+v872 @ rcx_v54*8]");
									object obj11 = (nint)0 << 4;
									object obj12 = obj11 + 312;
									obj13 = obj12 + obj5;
									goto IL_0884;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
							IL_0884:
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v877 @ rdx_v24] (should have been resolved before IL gen)");
							bool flag4 = inputControl == null;
							num = (nint)typeof(IEnumerator<InputControl>);
							inputControlList2 = (InputControlList<InputControl>)text;
							if (flag4)
							{
								continue;
							}
							nint num2 = (nint)inputControl;
							nint num3 = (nint)typeof(AnyKeyControl);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v433 @ rax_v37 (Il2CppClass<UnityEngine.InputSystem.Controls.AnyKeyControl>)+130]");
							InputControlList<InputControl> inputControlList3 = (InputControlList<InputControl>)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+130]");
							nint num4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v433 @ rax_v37 (Il2CppClass<UnityEngine.InputSystem.Controls.AnyKeyControl>)+130]");
							if (num4 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+C8]");
								inputControlList2 = (InputControlList<InputControl>)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v611 @ rcx_v4 (UnityEngine.InputSystem.InputControlList`1<UnityEngine.InputSystem.InputControl>)+FFFFFFF8+v442 @ rcx_v44 (UnityEngine.InputSystem.InputControlList`1<UnityEngine.InputSystem.InputControl>)*8]");
								bool flag5 = 0 == (nint)typeof(AnyKeyControl);
								inputControlList3 = inputControlList2;
								num = (nint)typeof(IEnumerator<InputControl>);
								if (flag5)
								{
									continue;
								}
							}
							nint num5 = (nint)typeof(KeyControl);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v919 @ rdx_v27 (Il2CppClass<UnityEngine.InputSystem.Controls.KeyControl>)+130]");
							InputControlList<InputControl> inputControlList4 = (InputControlList<InputControl>)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+130]");
							nint num6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v919 @ rdx_v27 (Il2CppClass<UnityEngine.InputSystem.Controls.KeyControl>)+130]");
							if (num6 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+C8]");
								object obj14 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rax_v56+FFFFFFF8+v920 @ rax_v38 (UnityEngine.InputSystem.InputControlList`1<UnityEngine.InputSystem.InputControl>)*8]");
								bool flag6 = 0 == (nint)typeof(KeyControl);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v919 @ rdx_v27 (Il2CppClass<UnityEngine.InputSystem.Controls.KeyControl>)+130]");
								inputControlList3 = (InputControlList<InputControl>)0;
								if (flag6)
								{
									goto IL_03d3;
								}
							}
							nint num7 = (nint)typeof(ButtonControl);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rdx_v36 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							InputControlList<InputControl> inputControlList5 = (InputControlList<InputControl>)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+130]");
							nint num8 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rdx_v36 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							bool flag7 = num8 < 0;
							num = (nint)typeof(IEnumerator<InputControl>);
							inputControlList2 = inputControlList3;
							if (flag7)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+C8]");
							object obj15 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v435 @ rax_v53+FFFFFFF8+v434 @ rax_v52 (UnityEngine.InputSystem.InputControlList`1<UnityEngine.InputSystem.InputControl>)*8]");
							bool flag8 = 0 != (nint)typeof(ButtonControl);
							num = (nint)typeof(IEnumerator<InputControl>);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rdx_v36 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							inputControlList2 = (InputControlList<InputControl>)0;
							if (flag8)
							{
								continue;
							}
							string path2 = inputControl.path;
							if (path2 != null)
							{
								bool flag9 = path2.Contains("Mouse");
								bool flag10 = !flag9;
								num = (nint)typeof(IEnumerator<InputControl>);
								inputControlList2 = (InputControlList<InputControl>)path2;
								if (flag10)
								{
									continue;
								}
								goto IL_03d3;
							}
							throw new NullReferenceException();
							IL_03d3:
							obj3++;
							string path3 = inputControl.path;
							if (path3 != null)
							{
								string text3 = path3.Replace("/Keyboard", "<Keyboard>");
								if (text3 != null)
								{
									string text4 = text3.Replace("/Mouse", "<Mouse>");
									if (!string.IsNullOrEmpty(text2))
									{
										nint num9 = (nint)typeof(InputBindingForInputSystem);
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
										string text5 = text2 + text6 + text4;
										num = unchecked((nint)null);
										text2 = text5;
										inputControlList2 = (InputControlList<InputControl>)text5;
									}
									else
									{
										string text7 = text2 + text4;
										num = unchecked((nint)null);
										text2 = text7;
										inputControlList2 = (InputControlList<InputControl>)text7;
									}
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
							IL_0157:
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
							obj6 = 0;
							obj13 = obj16;
							goto IL_0884;
						}
						bool flag11 = obj2 == null;
						string text8 = text;
						if (!flag11)
						{
							text8 = (string)obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						if (string.IsNullOrEmpty(text2))
						{
							text2 = text9;
						}
						bool flag12 = (nint)obj3 <= 1;
						text10 = text9;
						if (!flag12)
						{
							text10 = text2;
						}
						text9 = text8;
						obj17 = this;
						goto IL_05cd;
					}
				}
				else
				{
					bool flag13 = rebindingOp == null;
					InputControlList<InputControl> candidates = (InputControlList<InputControl>)0;
					obj17 = this;
					text10 = text9;
					obj = this;
					if (!flag13)
					{
						goto IL_05cd;
					}
				}
			}
			goto IL_07b9;
			IL_07b9:
			throw new NullReferenceException();
			IL_0746:
			obj = (_003C_003Ec__DisplayClass31_0)(object)obj17._003C_003E4__this;
			if (obj17._003C_003E4__this != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v7 (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem+<>c__DisplayClass31_0)+58]");
				object obj18 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v7 (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem+<>c__DisplayClass31_0)+58]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v825 @ rcx_v14+18] (should have been resolved before IL gen)");
				}
				return;
			}
			goto IL_07b9;
			IL_05cd:
			rebindingOp.Dispose();
			obj = (_003C_003Ec__DisplayClass31_0)(object)obj17._003C_003E4__this;
			if (obj17._003C_003E4__this != null)
			{
				_ = 0;
				obj = (_003C_003Ec__DisplayClass31_0)(obj + 104);
				InputBindingForInputSystem inputBindingForInputSystem2 = obj17._003C_003E4__this;
				if (obj17._003C_003E4__this != null)
				{
					string text11;
					nint num10;
					if (inputBindingForInputSystem2.CheckBindingPathFunc != null)
					{
						bool flag14 = text10 != inputBindingForInputSystem2._bindingPath;
						bool flag15 = !flag14;
						text9 = null;
						if (!flag15)
						{
							obj = (_003C_003Ec__DisplayClass31_0)(object)inputBindingForInputSystem2.CheckBindingPathFunc;
							if (inputBindingForInputSystem2.CheckBindingPathFunc == null)
							{
								goto IL_07b9;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v7 (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem+<>c__DisplayClass31_0)+28]");
							num = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v127.<>4__this (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem) (should have been resolved before IL gen)");
							object obj19 = default(object);
							bool flag16 = obj19 == null;
							text9 = text10;
							text11 = text10;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v7 (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem+<>c__DisplayClass31_0)+28]");
							num10 = 0;
							if (flag16)
							{
								goto IL_0746;
							}
						}
					}
					inputBindingForInputSystem2._bindingPath = text10;
					text11 = text9;
					num10 = num;
					goto IL_0746;
				}
			}
			goto IL_07b9;
		}

		internal void _003CStartListening_003Eb__2(InputActionRebindingExtensions.RebindingOperation rebindingOp)
		{
			rebindingOp.Dispose();
			InputBindingForInputSystem inputBindingForInputSystem = _003C_003E4__this;
			inputBindingForInputSystem._rebindingOperation = null;
			InputBindingForInputSystem inputBindingForInputSystem2 = _003C_003E4__this;
			Action onCanceled = inputBindingForInputSystem2.m_OnCanceled;
			if (inputBindingForInputSystem2.m_OnCanceled != null)
			{
				IntPtr invoke_impl = ((Delegate)onCanceled).invoke_impl;
				IntPtr method = ((Delegate)onCanceled).method;
				IntPtr method_code = ((Delegate)onCanceled).method_code;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v93 @ rax_v6 (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	public static char CompositeControlSeparator = '+';

	public static float WaitForKeyComboDuration = 0.3f;

	public static string[] GlobalIgnoreControlPaths = new string[4] { "<Pointer>/position", "<Pointer>/delta", "<Pointer>/{PrimaryAction}", "<Mouse>/clickCount" };

	public static string[] GlobalAbortControlPaths = new string[2] { "<Keyboard>/escape", "<Gamepad>/start" };

	public static string GlobalControlsHavingToMatchPath;

	public LocalConfigBehaviours LocalConfigBehaviour = LocalConfigBehaviours.AppendLocalToGlobal;

	public string[] IgnoreControlPaths;

	public string[] AbortControlPaths;

	public string ControlsHavingToMatchPath;

	public string[] MatchControlPaths;

	protected string _bindingPath;

	[NonSerialized]
	public bool AllowComposite;

	public CheckBindingPathDelegate CheckBindingPathFunc;

	public OnBeforeRebindStartDelegate OnBeforeRebindStart;

	private Action m_OnComplete;

	private Action m_OnCanceled;

	protected InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

	public event Action OnComplete
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 88;
			Delegate obj2 = this.m_OnComplete;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 88;
			Delegate obj2 = this.m_OnComplete;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action OnCanceled
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 96;
			Delegate obj2 = this.m_OnCanceled;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 96;
			Delegate obj2 = this.m_OnCanceled;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public void CopyFrom(InputBindingForInputSystem other)
	{
		LocalConfigBehaviour = other.LocalConfigBehaviour;
		string[] ignoreControlPaths = other.IgnoreControlPaths;
		Array.Copy(other.IgnoreControlPaths, IgnoreControlPaths, ignoreControlPaths.Length);
		string[] abortControlPaths = other.AbortControlPaths;
		Array.Copy(other.AbortControlPaths, AbortControlPaths, abortControlPaths.Length);
		string[] abortControlPaths2 = other.AbortControlPaths;
		Array.Copy(other.AbortControlPaths, AbortControlPaths, abortControlPaths2.Length);
		string[] matchControlPaths = other.MatchControlPaths;
		Array.Copy(other.MatchControlPaths, MatchControlPaths, matchControlPaths.Length);
		ControlsHavingToMatchPath = other.ControlsHavingToMatchPath;
		_bindingPath = other._bindingPath;
		AllowComposite = other.AllowComposite;
		CheckBindingPathFunc = other.CheckBindingPathFunc;
		OnBeforeRebindStart = other.OnBeforeRebindStart;
		this.m_OnComplete = other.m_OnComplete;
		this.m_OnCanceled = other.m_OnCanceled;
	}

	public string GetBindingPath()
	{
		return _bindingPath;
	}

	public void SetBindingPath(string path)
	{
		if (CheckBindingPathFunc != null && path != _bindingPath)
		{
			CheckBindingPathDelegate checkBindingPathFunc = CheckBindingPathFunc;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v85.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			object obj = default(object);
			if (obj == null)
			{
				return;
			}
		}
		_bindingPath = path;
	}

	public void AddOnCompleteCallback(Action callback)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		object obj = this + 88;
		Delegate obj2 = this.m_OnComplete;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj3 = Delegate.Combine(obj2, callback);
			bool flag = (object)obj3 == null;
			Delegate obj4 = null;
			if (!flag)
			{
				bool flag2 = (object)obj3.GetType() != typeof(Action);
				obj4 = null;
				if (!flag2)
				{
					obj4 = obj3;
				}
				if ((object)obj4 == null)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj5 != obj2;
			obj2 = obj5;
			if (!flag3)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public void RemoveOnCompleteCallback(Action callback)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		object obj = this + 88;
		Delegate obj2 = this.m_OnComplete;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj3 = Delegate.Remove(obj2, callback);
			bool flag = (object)obj3 == null;
			Delegate obj4 = null;
			if (!flag)
			{
				bool flag2 = (object)obj3.GetType() != typeof(Action);
				obj4 = null;
				if (!flag2)
				{
					obj4 = obj3;
				}
				if ((object)obj4 == null)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj5 != obj2;
			obj2 = obj5;
			if (!flag3)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public void AddOnCanceledCallback(Action callback)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		object obj = this + 96;
		Delegate obj2 = this.m_OnCanceled;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj3 = Delegate.Combine(obj2, callback);
			bool flag = (object)obj3 == null;
			Delegate obj4 = null;
			if (!flag)
			{
				bool flag2 = (object)obj3.GetType() != typeof(Action);
				obj4 = null;
				if (!flag2)
				{
					obj4 = obj3;
				}
				if ((object)obj4 == null)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj5 != obj2;
			obj2 = obj5;
			if (!flag3)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public void RemoveOnCanceledCallback(Action callback)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		object obj = this + 96;
		Delegate obj2 = this.m_OnCanceled;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj3 = Delegate.Remove(obj2, callback);
			bool flag = (object)obj3 == null;
			Delegate obj4 = null;
			if (!flag)
			{
				bool flag2 = (object)obj3.GetType() != typeof(Action);
				obj4 = null;
				if (!flag2)
				{
					obj4 = obj3;
				}
				if ((object)obj4 == null)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag3 = (object)obj5 != obj2;
			obj2 = obj5;
			if (!flag3)
			{
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public unsafe void StartListening()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		_003C_003Ec__DisplayClass31_0 CS_0024_003C_003E8__locals17 = new _003C_003Ec__DisplayClass31_0();
		CS_0024_003C_003E8__locals17._003C_003E4__this = this;
		InputUtils.ResetStuckKeyStates();
		InputActionRebindingExtensions.RebindingOperation rebindingOperation = new InputActionRebindingExtensions.RebindingOperation();
		_rebindingOperation = rebindingOperation;
		string[] array = resolveConfigStrings(GlobalIgnoreControlPaths, IgnoreControlPaths);
		object obj = array + 32;
		string text = null;
		string text2 = null;
		while ((nint)text2 < array.Length)
		{
			InputActionRebindingExtensions.RebindingOperation rebindingOperation2 = _rebindingOperation.WithControlsExcluding((string)obj);
			text++;
			obj += 8;
			text2 = text;
		}
		string[] abortControlPaths = resolveConfigStrings(GlobalAbortControlPaths, AbortControlPaths);
		CS_0024_003C_003E8__locals17.abortControlPaths = abortControlPaths;
		Action<InputActionRebindingExtensions.RebindingOperation> action = delegate(InputActionRebindingExtensions.RebindingOperation operation)
		{
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Expected O, but got Unknown
			//IL_0102: Expected O, but got I4
			//IL_010b: Expected O, but got I4
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_0072: Expected O, but got I4
			//IL_007b: Expected O, but got I4
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Expected O, but got Unknown
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Expected O, but got Unknown
			//IL_020d: Expected O, but got I4
			//IL_0215: Unknown result type (might be due to invalid IL or missing references)
			//IL_021a: Expected O, but got Unknown
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_0227: Expected O, but got Unknown
			//IL_028d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0292: Expected O, but got Unknown
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Expected O, but got Unknown
			Version version = InputSystem.version;
			Version value = Version.Parse("1.4.1");
			int num = version.CompareTo(value);
			string[] abortControlPaths2 = CS_0024_003C_003E8__locals17.abortControlPaths;
			InputBindingForInputSystem inputBindingForInputSystem;
			if (num >= 0)
			{
				object obj3 = CS_0024_003C_003E8__locals17.abortControlPaths + 32;
				object obj4 = 0;
				object obj5 = 0;
				while (true)
				{
					if ((nint)obj5 >= abortControlPaths2.Length)
					{
						return;
					}
					InputControl selectedControl = operation.selectedControl;
					if (InputControlPath.Matches((string)obj3, selectedControl))
					{
						break;
					}
					obj4++;
					obj3 += 8;
					obj5 = obj4;
				}
				inputBindingForInputSystem = CS_0024_003C_003E8__locals17._003C_003E4__this;
			}
			else
			{
				object obj6 = CS_0024_003C_003E8__locals17.abortControlPaths + 32;
				object obj7 = 0;
				object obj8 = 0;
				object obj12 = default(object);
				while (true)
				{
					if ((nint)obj8 >= abortControlPaths2.Length)
					{
						return;
					}
					char c = ((string)obj6).get_Chars(0);
					bool flag5 = c != '<';
					string input = (string)obj6;
					if (!flag5)
					{
						string text7 = "/" + (string)obj6;
						input = text7;
					}
					string text8 = Regex.Replace(input, "[><{}*]+", "");
					string text9 = text8.ToLower();
					InputControl selectedControl2 = operation.selectedControl;
					string path = selectedControl2.path;
					string text10 = path.ToLower();
					int num2 = text10.IndexOf("/");
					int num3 = text9.IndexOf("/");
					object obj9 = text10._stringLength - text9._stringLength;
					object obj10 = obj9 - num2;
					object obj11 = num3 + obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180439880");
					if ((nint)obj12 <= 1)
					{
						InputControl selectedControl3 = operation.selectedControl;
						if (InputControlPath.Matches((string)obj6, selectedControl3))
						{
							break;
						}
					}
					obj7++;
					obj6 += 8;
					obj8 = obj7;
				}
				inputBindingForInputSystem = CS_0024_003C_003E8__locals17._003C_003E4__this;
			}
			inputBindingForInputSystem._rebindingOperation.Cancel();
		};
		action._002Ector((object)CS_0024_003C_003E8__locals17, (IntPtr)(nint)__ldftn(_003C_003Ec__DisplayClass31_0._003CStartListening_003Eb__0));
		InputActionRebindingExtensions.RebindingOperation rebindingOperation3 = _rebindingOperation.OnPotentialMatch(action);
		string[] matchControlPaths = MatchControlPaths;
		object obj2 = MatchControlPaths + 32;
		string text3 = null;
		string text4 = null;
		while ((nint)text4 < matchControlPaths.Length)
		{
			if (!string.IsNullOrEmpty((string)obj2))
			{
				InputActionRebindingExtensions.RebindingOperation rebindingOperation4 = _rebindingOperation.WithControlsHavingToMatchPath((string)obj2);
			}
			text3++;
			obj2 += 8;
			text4 = text3;
		}
		string text5;
		if (LocalConfigBehaviour != LocalConfigBehaviours.OverrideGlobalIfLocalExists)
		{
			bool flag = LocalConfigBehaviour != LocalConfigBehaviours.AppendLocalToGlobal;
			text5 = null;
			if (!flag)
			{
				if (GlobalControlsHavingToMatchPath != null)
				{
					bool flag2 = string.IsNullOrEmpty(ControlsHavingToMatchPath);
					text5 = GlobalControlsHavingToMatchPath;
					if (!flag2)
					{
						string text6 = GlobalControlsHavingToMatchPath + ControlsHavingToMatchPath;
						text5 = text6;
					}
				}
				else
				{
					text5 = ControlsHavingToMatchPath;
				}
			}
		}
		else
		{
			bool flag3 = string.IsNullOrEmpty(ControlsHavingToMatchPath);
			bool flag4 = !flag3;
			text5 = ControlsHavingToMatchPath;
			if (!flag4)
			{
				text5 = GlobalControlsHavingToMatchPath;
			}
		}
		if (!string.IsNullOrEmpty(text5))
		{
			InputActionRebindingExtensions.RebindingOperation rebindingOperation5 = _rebindingOperation.WithControlsHavingToMatchPath(text5);
		}
		float seconds = (AllowComposite ? WaitForKeyComboDuration : 0.1f);
		InputActionRebindingExtensions.RebindingOperation rebindingOperation6 = _rebindingOperation.OnMatchWaitForAnother(seconds);
		Action<InputActionRebindingExtensions.RebindingOperation, string> callback = delegate(InputActionRebindingExtensions.RebindingOperation rebindingOp, string path)
		{
			//IL_05ad: Expected O, but got I4
			//IL_006b: Expected O, but got Ref
			//IL_0074: Expected O, but got I4
			//IL_0085: Expected O, but got Ref
			//IL_0614: Unknown result type (might be due to invalid IL or missing references)
			//IL_0619: Expected O, but got Unknown
			//IL_00c9: Expected O, but got I4
			//IL_0785: Expected O, but got I
			//IL_016a: Expected O, but got I4
			//IL_08ab: Expected I, but got O
			//IL_0117: Expected O, but got I
			//IL_0120: Expected O, but got I4
			//IL_017f: Expected I, but got O
			//IL_018d: Expected I, but got O
			//IL_019d: Expected O, but got I
			//IL_04ef: Expected O, but got I
			//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fd: Expected O, but got Unknown
			//IL_0227: Expected I, but got O
			//IL_0237: Expected O, but got I
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Expected O, but got Unknown
			//IL_02bb: Expected I, but got O
			//IL_02cb: Expected O, but got I
			//IL_02f7: Expected I, but got O
			//IL_01d9: Expected O, but got I
			//IL_020b: Expected I, but got O
			//IL_0273: Expected O, but got I
			//IL_029f: Expected O, but got I
			//IL_031d: Expected O, but got I
			//IL_034a: Expected I, but got O
			//IL_035a: Expected O, but got I
			//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e1: Expected O, but got Unknown
			//IL_03bd: Expected I, but got O
			//IL_0525: Expected I, but got O
			//IL_048e: Expected I, but got O
			//IL_04b7: Expected I, but got O
			InputBindingForInputSystem inputBindingForInputSystem = CS_0024_003C_003E8__locals17._003C_003E4__this;
			bool flag5 = CS_0024_003C_003E8__locals17._003C_003E4__this == null;
			_003C_003Ec__DisplayClass31_0 obj3 = CS_0024_003C_003E8__locals17;
			nint num = default(nint);
			string text15 = default(string);
			string text16;
			_003C_003Ec__DisplayClass31_0 obj19;
			if (!flag5)
			{
				if (inputBindingForInputSystem.AllowComposite)
				{
					bool flag6 = rebindingOp == null;
					obj3 = CS_0024_003C_003E8__locals17;
					if (!flag6)
					{
						InputControlList<InputControl> candidates = rebindingOp.m_Candidates;
						InputControlList<InputControl> inputControlList = default(InputControlList<InputControl>);
						IEnumerator<InputControl> enumerator = inputControlList.GetEnumerator();
						string text7 = default(string);
						object obj4 = (object)(&text7);
						object obj5 = 0;
						string text8 = "";
						InputControlList<InputControl> inputControlList2 = (InputControlList<InputControl>)(&inputControlList);
						object obj6 = default(object);
						InputControl inputControl = default(InputControl);
						string text12 = default(string);
						object obj18 = default(object);
						while (true)
						{
							if (text7 == null)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							if (obj6 == null)
							{
								break;
							}
							bool flag7 = text7 == null;
							inputControlList2 = (InputControlList<InputControl>)0;
							if (flag7)
							{
								throw new NullReferenceException();
							}
							object obj7 = text7;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v9+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_0157;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v9+B0]");
							object obj8 = 0;
							object obj9 = 0;
							while (true)
							{
								object obj10 = obj9 + obj9;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v16+v796 @ rax_v61*8]");
								if (0 == (nint)typeof(IEnumerator<InputControl>))
								{
									break;
								}
								obj9++;
								object obj11 = obj9;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v9+12E]");
								if ((nint)obj11 < 0)
								{
									continue;
								}
								goto IL_0157;
							}
							object obj12 = obj9 + obj9;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v16+8+v872 @ rcx_v54*8]");
							object obj13 = (nint)0 << 4;
							object obj14 = obj13 + 312;
							object obj15 = obj14 + obj7;
							goto IL_0884;
							IL_0884:
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v877 @ rdx_v24] (should have been resolved before IL gen)");
							bool flag8 = inputControl == null;
							num = (nint)typeof(IEnumerator<InputControl>);
							inputControlList2 = (InputControlList<InputControl>)text7;
							if (flag8)
							{
								continue;
							}
							nint num2 = (nint)inputControl;
							nint num3 = (nint)typeof(AnyKeyControl);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v433 @ rax_v37 (Il2CppClass<UnityEngine.InputSystem.Controls.AnyKeyControl>)+130]");
							InputControlList<InputControl> inputControlList3 = (InputControlList<InputControl>)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+130]");
							nint num4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v433 @ rax_v37 (Il2CppClass<UnityEngine.InputSystem.Controls.AnyKeyControl>)+130]");
							if (num4 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+C8]");
								inputControlList2 = (InputControlList<InputControl>)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v611 @ rcx_v4 (UnityEngine.InputSystem.InputControlList`1<UnityEngine.InputSystem.InputControl>)+FFFFFFF8+v442 @ rcx_v44 (UnityEngine.InputSystem.InputControlList`1<UnityEngine.InputSystem.InputControl>)*8]");
								bool flag9 = 0 == (nint)typeof(AnyKeyControl);
								inputControlList3 = inputControlList2;
								num = (nint)typeof(IEnumerator<InputControl>);
								if (flag9)
								{
									continue;
								}
							}
							nint num5 = (nint)typeof(KeyControl);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v919 @ rdx_v27 (Il2CppClass<UnityEngine.InputSystem.Controls.KeyControl>)+130]");
							InputControlList<InputControl> inputControlList4 = (InputControlList<InputControl>)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+130]");
							nint num6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v919 @ rdx_v27 (Il2CppClass<UnityEngine.InputSystem.Controls.KeyControl>)+130]");
							if (num6 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+C8]");
								object obj16 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ rax_v56+FFFFFFF8+v920 @ rax_v38 (UnityEngine.InputSystem.InputControlList`1<UnityEngine.InputSystem.InputControl>)*8]");
								bool flag10 = 0 == (nint)typeof(KeyControl);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v919 @ rdx_v27 (Il2CppClass<UnityEngine.InputSystem.Controls.KeyControl>)+130]");
								inputControlList3 = (InputControlList<InputControl>)0;
								if (flag10)
								{
									goto IL_03d3;
								}
							}
							nint num7 = (nint)typeof(ButtonControl);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rdx_v36 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							InputControlList<InputControl> inputControlList5 = (InputControlList<InputControl>)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+130]");
							nint num8 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rdx_v36 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							bool flag11 = num8 < 0;
							num = (nint)typeof(IEnumerator<InputControl>);
							inputControlList2 = inputControlList3;
							if (flag11)
							{
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ r8_v17 (Il2CppClass<UnityEngine.InputSystem.InputControl>)+C8]");
							object obj17 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v435 @ rax_v53+FFFFFFF8+v434 @ rax_v52 (UnityEngine.InputSystem.InputControlList`1<UnityEngine.InputSystem.InputControl>)*8]");
							bool flag12 = 0 != (nint)typeof(ButtonControl);
							num = (nint)typeof(IEnumerator<InputControl>);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rdx_v36 (Il2CppClass<UnityEngine.InputSystem.Controls.ButtonControl>)+130]");
							inputControlList2 = (InputControlList<InputControl>)0;
							if (flag12)
							{
								continue;
							}
							string path2 = inputControl.path;
							if (path2 == null)
							{
								throw new NullReferenceException();
							}
							bool flag13 = path2.Contains("Mouse");
							bool flag14 = !flag13;
							num = (nint)typeof(IEnumerator<InputControl>);
							inputControlList2 = (InputControlList<InputControl>)path2;
							if (flag14)
							{
								continue;
							}
							goto IL_03d3;
							IL_03d3:
							obj5++;
							string path3 = inputControl.path;
							if (path3 == null)
							{
								throw new NullReferenceException();
							}
							string text9 = path3.Replace("/Keyboard", "<Keyboard>");
							if (text9 == null)
							{
								throw new NullReferenceException();
							}
							string text10 = text9.Replace("/Mouse", "<Mouse>");
							if (!string.IsNullOrEmpty(text8))
							{
								nint num9 = (nint)typeof(InputBindingForInputSystem);
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
								string text11 = text8 + text12 + text10;
								num = unchecked((nint)null);
								text8 = text11;
								inputControlList2 = (InputControlList<InputControl>)text11;
							}
							else
							{
								string text13 = text8 + text10;
								num = unchecked((nint)null);
								text8 = text13;
								inputControlList2 = (InputControlList<InputControl>)text13;
							}
							continue;
							IL_0157:
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
							obj8 = 0;
							obj15 = obj18;
							goto IL_0884;
						}
						bool flag15 = obj4 == null;
						string text14 = text7;
						if (!flag15)
						{
							text14 = (string)obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						if (string.IsNullOrEmpty(text8))
						{
							text8 = text15;
						}
						bool flag16 = (nint)obj5 <= 1;
						text16 = text15;
						if (!flag16)
						{
							text16 = text8;
						}
						text15 = text14;
						obj19 = CS_0024_003C_003E8__locals17;
						goto IL_05cd;
					}
				}
				else
				{
					bool flag17 = rebindingOp == null;
					InputControlList<InputControl> candidates = (InputControlList<InputControl>)0;
					obj19 = CS_0024_003C_003E8__locals17;
					text16 = text15;
					obj3 = CS_0024_003C_003E8__locals17;
					if (!flag17)
					{
						goto IL_05cd;
					}
				}
			}
			goto IL_07b9;
			IL_07b9:
			throw new NullReferenceException();
			IL_0746:
			obj3 = (_003C_003Ec__DisplayClass31_0)(object)obj19._003C_003E4__this;
			if (obj19._003C_003E4__this != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v7 (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem+<>c__DisplayClass31_0)+58]");
				object obj20 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v7 (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem+<>c__DisplayClass31_0)+58]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v825 @ rcx_v14+18] (should have been resolved before IL gen)");
				}
				return;
			}
			goto IL_07b9;
			IL_05cd:
			rebindingOp.Dispose();
			obj3 = (_003C_003Ec__DisplayClass31_0)(object)obj19._003C_003E4__this;
			if (obj19._003C_003E4__this != null)
			{
				_ = 0;
				obj3 = (_003C_003Ec__DisplayClass31_0)(obj3 + 104);
				InputBindingForInputSystem inputBindingForInputSystem2 = obj19._003C_003E4__this;
				if (obj19._003C_003E4__this != null)
				{
					string text17;
					nint num10;
					if (inputBindingForInputSystem2.CheckBindingPathFunc != null)
					{
						bool flag18 = text16 != inputBindingForInputSystem2._bindingPath;
						bool flag19 = !flag18;
						text15 = null;
						if (!flag19)
						{
							obj3 = (_003C_003Ec__DisplayClass31_0)(object)inputBindingForInputSystem2.CheckBindingPathFunc;
							if (inputBindingForInputSystem2.CheckBindingPathFunc == null)
							{
								goto IL_07b9;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v7 (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem+<>c__DisplayClass31_0)+28]");
							num = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v127.<>4__this (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem) (should have been resolved before IL gen)");
							object obj21 = default(object);
							bool flag20 = obj21 == null;
							text15 = text16;
							text17 = text16;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v7 (Kamgam.UGUIComponentsForSettings.InputBindingForInputSystem+<>c__DisplayClass31_0)+28]");
							num10 = 0;
							if (flag20)
							{
								goto IL_0746;
							}
						}
					}
					inputBindingForInputSystem2._bindingPath = text16;
					text17 = text15;
					num10 = num;
					goto IL_0746;
				}
			}
			goto IL_07b9;
		};
		InputActionRebindingExtensions.RebindingOperation rebindingOperation7 = _rebindingOperation.OnApplyBinding(callback);
		Action<InputActionRebindingExtensions.RebindingOperation> callback2 = delegate(InputActionRebindingExtensions.RebindingOperation rebindingOp)
		{
			rebindingOp.Dispose();
			InputBindingForInputSystem inputBindingForInputSystem = CS_0024_003C_003E8__locals17._003C_003E4__this;
			inputBindingForInputSystem._rebindingOperation = null;
			InputBindingForInputSystem inputBindingForInputSystem2 = CS_0024_003C_003E8__locals17._003C_003E4__this;
			Action onCanceled = inputBindingForInputSystem2.m_OnCanceled;
			if (inputBindingForInputSystem2.m_OnCanceled != null)
			{
				IntPtr invoke_impl = ((Delegate)onCanceled).invoke_impl;
				IntPtr method = ((Delegate)onCanceled).method;
				IntPtr method_code = ((Delegate)onCanceled).method_code;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v93 @ rax_v6 (System.IntPtr) (should have been resolved before IL gen)");
			}
		};
		InputActionRebindingExtensions.RebindingOperation rebindingOperation8 = _rebindingOperation.OnCancel(callback2);
		OnBeforeRebindStartDelegate onBeforeRebindStart = OnBeforeRebindStart;
		if (OnBeforeRebindStart != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v688.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		InputActionRebindingExtensions.RebindingOperation rebindingOperation9 = _rebindingOperation.Start();
	}

	protected string[] resolveConfigStrings(string[] globals, string[] locals)
	{
		string[] result;
		if (LocalConfigBehaviour != LocalConfigBehaviours.OverrideGlobalIfLocalExists)
		{
			if (LocalConfigBehaviour != LocalConfigBehaviours.AppendLocalToGlobal)
			{
				return new string[0];
			}
			bool flag = globals == null;
			result = locals;
			if (!flag)
			{
				List<string> list = new List<string>(globals);
				if (locals != null && locals.Length != 0)
				{
					if (list != null)
					{
						list.AddRange(locals);
						return list.ToArray();
					}
				}
				else if (list != null)
				{
					return list.ToArray();
				}
				return (string[])(object)new NullReferenceException();
			}
		}
		else
		{
			if (locals == null)
			{
				return globals;
			}
			bool flag2 = locals.Length != 0;
			result = locals;
			if (!flag2)
			{
				result = globals;
			}
		}
		return result;
	}

	protected string resolveConfigString(string global, string local)
	{
		string result;
		if (LocalConfigBehaviour != LocalConfigBehaviours.OverrideGlobalIfLocalExists)
		{
			if (LocalConfigBehaviour != LocalConfigBehaviours.AppendLocalToGlobal)
			{
				return null;
			}
			bool flag = global == null;
			result = local;
			if (!flag)
			{
				bool flag2 = string.IsNullOrEmpty(local);
				string result2 = global;
				if (!flag2)
				{
					string text = global + local;
					result2 = text;
				}
				return result2;
			}
		}
		else
		{
			bool flag3 = string.IsNullOrEmpty(local);
			bool flag4 = !flag3;
			result = local;
			if (!flag4)
			{
				result = global;
			}
		}
		return result;
	}

	public void OnEnable()
	{
	}

	public void OnDisable()
	{
		if (_rebindingOperation != null)
		{
			_rebindingOperation.Cancel();
			if (_rebindingOperation != null)
			{
				_rebindingOperation.Dispose();
				_rebindingOperation = null;
			}
		}
	}

	public InputBindingForInputSystem()
	{
		string[] ignoreControlPaths = new string[0];
		IgnoreControlPaths = ignoreControlPaths;
		string[] abortControlPaths = new string[0];
		AbortControlPaths = abortControlPaths;
		string[] matchControlPaths = new string[0];
		MatchControlPaths = matchControlPaths;
		_bindingPath = "<Keyboard>/space";
		OnEnable();
	}
}
