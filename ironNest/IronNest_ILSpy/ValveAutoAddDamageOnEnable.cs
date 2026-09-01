using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class ValveAutoAddDamageOnEnable : MonoBehaviour
{
	public enum TargetScope
	{
		SpecificSystem,
		AnySystem
	}

	private sealed class _003CBurstRoutine_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ValveAutoAddDamageOnEnable _003C_003E4__this;

		public bool ignoreProbability;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CBurstRoutine_003Ed__19(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0008: Expected O, but got Ref
			//IL_08bd: Expected I4, but got I8
			//IL_001d: Expected O, but got I4
			//IL_0bc6: Expected F4, but got I
			//IL_08a9: Expected I4, but got I8
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_0076: Expected I4, but got I8
			//IL_0a05: Invalid comparison between I and F4
			//IL_008b: Expected O, but got I
			//IL_081d: Expected O, but got Ref
			//IL_0833: Expected I4, but got O
			//IL_0851: Expected O, but got I
			//IL_0a49: Expected O, but got Ref
			//IL_0a66: Expected O, but got Ref
			//IL_09e7: Expected O, but got I4
			//IL_0abe: Expected O, but got I4
			//IL_01f3: Expected O, but got Ref
			//IL_0326: Expected F4, but got I
			//IL_0227: Expected O, but got Ref
			//IL_0249: Expected O, but got Ref
			//IL_0382: Expected F4, but got I4
			//IL_0cce: Expected O, but got Ref
			//IL_0ce6: Expected O, but got Ref
			//IL_0d05: Expected F4, but got I
			//IL_02c6: Expected O, but got I
			//IL_02c6: Expected O, but got I
			//IL_02e7: Expected O, but got I
			//IL_02e7: Expected O, but got I
			//IL_03be: Expected F4, but got I4
			//IL_0ec9: Expected O, but got I4
			//IL_0daf: Expected O, but got Ref
			//IL_0dc9: Expected O, but got I
			//IL_03ea: Expected O, but got I
			//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ba: Expected O, but got Unknown
			//IL_0622: Invalid comparison between I4 and F4
			//IL_0431: Expected O, but got I
			//IL_066d: Expected F4, but got I4
			//IL_0d74: Expected O, but got I
			//IL_067b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0680: Expected O, but got Unknown
			//IL_068e: Expected O, but got Ref
			//IL_06ab: Expected O, but got Ref
			//IL_06d0: Expected O, but got I
			//IL_0701: Expected O, but got Ref
			//IL_071e: Expected O, but got Ref
			//IL_0d5f: Expected I4, but got O
			//IL_0748: Expected O, but got Ref
			//IL_04ff: Expected O, but got I
			object obj2 = default(object);
			object obj = (object)(&obj2);
			UnityEngine.Object obj3 = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			bool flag2;
			if (!flag)
			{
				object obj4 = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj5 = obj4 - 1;
					if (flag || (nint)obj5 == 1)
					{
						_003C_003E1__state = -1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+58]");
						List<ValveController> list = (List<ValveController>)0;
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+58]");
						if ((nint)0 == 0 || list._size == 0)
						{
							object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+30]");
							_ = 0;
							object arg = (TargetScope)obj6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+28]");
							string text = $"(TargetScope={arg}, systemId='{0}'). Burst aborted.";
							string message = "[ValveAutoAddDamageOnEnable] No allowed valves found " + text;
							Debug.LogWarning(message, obj3);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+54]");
							flag2 = (nint)0 == 0;
							goto IL_0c21;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+56]");
						if ((nint)0 != 0 && ValveBreakBlocker.IsBlocked)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+55]");
							if ((nint)0 != 0)
							{
								Debug.Log("[ValveAutoAddDamageOnEnable] Aborted before apply: global blocker became active during wait.", obj3);
							}
							goto IL_0161;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+34]");
						int num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+34]");
						if ((nint)0 < (nint)1)
						{
							num = 1;
						}
						int num2 = num;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+38]");
						bool flag3 = (nint)num2 > (nint)0;
						int num3 = num;
						if (!flag3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+38]");
							num3 = 0;
						}
						int maxExclusive = num3 + 1;
						int num4 = UnityEngine.Random.Range(num, maxExclusive);
						int num5 = list._size;
						if (num4 < list._size)
						{
							num5 = num4;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+55]");
						if ((nint)0 != 0)
						{
							object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg2 = default(object);
							string text2 = $"[ValveAutoAddDamageOnEnable] Burst: requested={arg2}, ";
							object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
							_ = list._size;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 127));
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg3 = default(object);
							object arg4 = default(object);
							string text3 = $"pool={arg3}, actual={arg4}.";
							string message2 = text2 + text3;
							Debug.Log(message2, obj3);
						}
						bool flag4 = num5 <= 0;
						int num6 = 0;
						if (!flag4)
						{
							do
							{
								int index = UnityEngine.Random.Range(num6, list._size);
								object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+58]");
								nint num7 = 0;
								int index2 = num6;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+67]");
								((List<ValveController>)num7).set_Item(index2, (ValveController)0);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+58]");
								nint num8 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+77]");
								((List<ValveController>)num8).set_Item(index, (ValveController)0);
								num6++;
							}
							while (num6 < num5);
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+4C]");
						float num9 = 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+4C]");
						if ((nint)0 <= (nint)0)
						{
							if (num9 > 1f)
							{
								num9 = 1f;
							}
						}
						else
						{
							num9 = 0f;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+50]");
						float num10 = 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+50]");
						if ((nint)0 <= (nint)0)
						{
							if (num10 > 1f)
							{
								num10 = 1f;
							}
						}
						else
						{
							num10 = 0f;
						}
						bool flag5 = !(num9 > num10);
						float minInclusive = num9;
						if (!flag5)
						{
							minInclusive = num10;
							num10 = num9;
						}
						bool flag6 = num5 <= 0;
						object obj12 = 0;
						if (!flag6)
						{
							object arg5 = default(object);
							object arg6 = default(object);
							object arg7 = default(object);
							object arg8 = default(object);
							object arg9 = default(object);
							do
							{
								object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 81));
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-51]");
								UnityEngine.Object obj14 = (UnityEngine.Object)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-51]");
								bool flag7 = (UnityEngine.Object)0 == null;
								string message3;
								if (!flag7)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+56]");
									if ((nint)0 != (flag7 ? 1 : 0))
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-51]");
										string text4 = TryGetValveSystemId((ValveController)0);
										if (ValveBreakBlocker.IsBlocked || ValveBreakBlocker.IsSystemBlocked(text4))
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+55]");
											if ((nint)0 != 0)
											{
												string[] array = new string[5];
												if (array.Length > 0)
												{
													array[0] = "[ValveAutoAddDamageOnEnable] Skipping valve '";
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-51]");
													string name = ((UnityEngine.Object)0).name;
													if (array.Length > 1)
													{
														array[1] = name;
														if (array.Length > 2)
														{
															array[2] = "': system '";
															if (array.Length > 3)
															{
																array[3] = text4;
																if (array.Length > 4)
																{
																	array[4] = "' or global blocker active.";
																	message3 = string.Concat(array);
																	goto IL_079a;
																}
															}
														}
													}
												}
												IndexOutOfRangeException ex = new IndexOutOfRangeException();
												return (byte)(int)ex != 0;
											}
											goto IL_07ac;
										}
									}
									num9 = UnityEngine.Random.Range(minInclusive, num10);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rsi_v11 (UnityEngine.Object)+E0]");
									float num11 = 0f + num9;
									if (!(0f > num11))
									{
										if (num11 > 1f)
										{
											num11 = 1f;
										}
									}
									else
									{
										num11 = 0f;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-51]");
									((ValveController)0).SetDamage01(num11);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+55]");
									bool flag8 = (nint)0 == 0;
									float num12 = num11;
									if (!flag8)
									{
										object obj15 = obj12 + 1;
										object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-51]");
										string name2 = ((UnityEngine.Object)0).name;
										string text5 = $"[ValveAutoAddDamageOnEnable] Valve [{arg5}/{arg6}] '{name2}': ";
										object obj18 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 127));
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										object obj19 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rsi_v11 (UnityEngine.Object)+E0]");
										_ = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										object obj20 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 85));
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										string text6 = $"+{arg7:0.###} damage (was {arg8:0.###}, now {arg9:0.###}).";
										message3 = text5 + text6;
										num12 = num11;
										goto IL_079a;
									}
								}
								goto IL_07ac;
								IL_07ac:
								obj12++;
								continue;
								IL_079a:
								Debug.Log(message3, obj3);
								goto IL_07ac;
							}
							while ((nint)obj12 < num5);
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+54]");
						if ((nint)0 != 0)
						{
							_ = 1;
							return false;
						}
					}
					goto IL_0c13;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+40]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+40]");
					WaitForSeconds waitForSeconds = new WaitForSeconds(0f);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					goto IL_0e50;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+56]");
			if ((nint)0 != 0 && ValveBreakBlocker.IsBlocked)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+55]");
				if ((nint)0 == 0)
				{
					goto IL_0161;
				}
				Debug.Log("[ValveAutoAddDamageOnEnable] Aborted: ValveBreakBlocker.IsBlocked == true.", obj3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+54]");
				flag2 = (nint)0 == 0;
				goto IL_0c21;
			}
			if (ignoreProbability)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+55]");
				if ((nint)0 != 0)
				{
					Debug.Log("[ValveAutoAddDamageOnEnable] Probability roll skipped (TriggerNowIgnoreProbability).", obj3);
					object obj21 = 0;
				}
			}
			else
			{
				float value = UnityEngine.Random.value;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+3C]");
				bool flag9 = 0f < value;
				bool flag10 = !flag9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+55]");
				if ((nint)0 != 0)
				{
					object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+3C]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg10 = "PASS";
					if (!flag10)
					{
						arg10 = "FAIL";
					}
					object arg11 = default(object);
					object arg12 = default(object);
					string message4 = $"[ValveAutoAddDamageOnEnable] Probability roll {arg11:0.###} <= {arg12:0.###} => {arg10}.";
					Debug.Log(message4, obj3);
					object obj21 = 0;
				}
				if (!flag10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+54]");
					flag2 = (nint)0 == 0;
					goto IL_0c21;
				}
			}
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+30]");
			if ((nint)0 == 1)
			{
				_003CWaitForAnySystemPool_003Ed__21 obj24 = new _003CWaitForAnySystemPool_003Ed__21(0);
				obj24._003C_003E1__state = 0;
				obj24._003C_003E4__this = (ValveAutoAddDamageOnEnable)obj3;
				obj24.waitStart = realtimeSinceStartup;
				Coroutine coroutine = ((MonoBehaviour)obj3).StartCoroutine((IEnumerator)obj24);
				_003C_003E2__current = coroutine;
				_003C_003E1__state = 2;
			}
			else
			{
				_003CWaitForSpecificSystemPool_003Ed__22 obj25 = new _003CWaitForSpecificSystemPool_003Ed__22(0);
				obj25._003C_003E1__state = 0;
				obj25._003C_003E4__this = (ValveAutoAddDamageOnEnable)obj3;
				obj25.waitStart = realtimeSinceStartup;
				Coroutine coroutine2 = ((MonoBehaviour)obj3).StartCoroutine((IEnumerator)obj25);
				_003C_003E2__current = coroutine2;
				_003C_003E1__state = 3;
			}
			goto IL_0e50;
			IL_0e50:
			return true;
			IL_0161:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ r14_v1 (UnityEngine.Object)+54]");
			flag2 = (nint)0 == 0;
			goto IL_0c21;
			IL_0c21:
			if (!flag2)
			{
				_ = 1;
			}
			goto IL_0c13;
			IL_0c13:
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

	private sealed class _003CWaitForAnySystemPool_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ValveAutoAddDamageOnEnable _003C_003E4__this;

		public float waitStart;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CWaitForAnySystemPool_003Ed__21(int _003C_003E1__state)
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
			//IL_008b: Expected I4, but got I8
			//IL_02d1: Invalid comparison between I4 and F4
			ValveAutoAddDamageOnEnable valveAutoAddDamageOnEnable = _003C_003E4__this;
			float realtimeSinceStartup;
			float num;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					valveAutoAddDamageOnEnable._pendingPool = null;
					realtimeSinceStartup = Time.realtimeSinceStartup;
					num = waitStart;
					goto IL_00be;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_02b5;
				}
				_003C_003E1__state = -1;
				realtimeSinceStartup = Time.realtimeSinceStartup;
				num = waitStart;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00be;
				}
			}
			goto IL_0323;
			IL_02c3:
			if (0f < valveAutoAddDamageOnEnable.waitForManagerSeconds)
			{
				WaitForSeconds waitForSeconds = new WaitForSeconds(valveAutoAddDamageOnEnable.managerPollInterval);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_02b5;
			IL_0323:
			throw new NullReferenceException();
			IL_00be:
			float num2 = realtimeSinceStartup - num;
			if (valveAutoAddDamageOnEnable.waitForManagerSeconds < num2)
			{
				goto IL_02b5;
			}
			List<ValveController> allRegisteredValves = HighPressureSystemManager.GetAllRegisteredValves();
			if (allRegisteredValves == null || allRegisteredValves._size <= 0)
			{
				goto IL_02c3;
			}
			List<ValveController> list = new List<ValveController>(allRegisteredValves._size);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ValveController>.Enumerator enumerator = default(List<ValveController>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			string text2 = default(string);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				if (flag)
				{
					continue;
				}
				if ((object)_003C_003E4__this != null)
				{
					bool flag2 = valveAutoAddDamageOnEnable.respectGlobalBlocker == flag;
					string text = text2;
					if (!flag2)
					{
						string text3 = TryGetValveSystemId((ValveController)obj);
						bool flag3 = ValveBreakBlocker.IsSystemBlocked(text3);
						text = text3;
						text2 = text3;
						if (flag3)
						{
							continue;
						}
					}
					if (list != null)
					{
						list.Add((ValveController)obj);
						text2 = text;
						continue;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (list != null)
			{
				if (list._size <= 0)
				{
					goto IL_02c3;
				}
				if ((object)_003C_003E4__this != null)
				{
					valveAutoAddDamageOnEnable._pendingPool = list;
					goto IL_02b5;
				}
			}
			goto IL_0323;
			IL_02b5:
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

	private sealed class _003CWaitForSpecificSystemPool_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ValveAutoAddDamageOnEnable _003C_003E4__this;

		public float waitStart;

		private HighPressureSystemManager _003Cmgr_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CWaitForSpecificSystemPool_003Ed__22(int _003C_003E1__state)
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
			//IL_0111: Expected I4, but got I8
			//IL_0545: Expected I4, but got O
			//IL_004b: Expected O, but got I
			//IL_0137: Invalid comparison between I and F4
			//IL_008f: Expected O, but got I
			//IL_018a: Expected O, but got I
			//IL_00b7: Expected O, but got I
			//IL_01cb: Expected O, but got I
			//IL_050a: Expected F4, but got I
			//IL_01b2: Expected O, but got I
			//IL_03c5: Expected O, but got I4
			//IL_047b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0480: Expected O, but got Unknown
			UnityEngine.Object obj = _003C_003E4__this;
			float num;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_0537;
				}
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+20]");
				_003Cmgr_003E5__2 = (HighPressureSystemManager)0;
				if (_003Cmgr_003E5__2 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+28]");
					if (!string.IsNullOrEmpty((string)0))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+28]");
						HighPressureSystemManager highPressureSystemManager = HighPressureSystemManager.FindBySystemId((string)0);
						_003Cmgr_003E5__2 = highPressureSystemManager;
						float realtimeSinceStartup = Time.realtimeSinceStartup;
						num = realtimeSinceStartup;
						goto IL_0116;
					}
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_030a;
				}
				_003C_003E1__state = -1;
			}
			float realtimeSinceStartup2 = Time.realtimeSinceStartup;
			bool flag = (object)_003C_003E4__this == null;
			num = realtimeSinceStartup2;
			if (!flag)
			{
				goto IL_0116;
			}
			goto IL_0537;
			IL_0537:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_030a:
			return false;
			IL_0116:
			float num2 = num - waitStart;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+44]");
			if (!(0f < num2))
			{
				if (_003Cmgr_003E5__2 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+28]");
					HighPressureSystemManager highPressureSystemManager2;
					if (!string.IsNullOrEmpty((string)0))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+28]");
						highPressureSystemManager2 = HighPressureSystemManager.FindBySystemId((string)0);
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+20]");
						highPressureSystemManager2 = (HighPressureSystemManager)0;
					}
					_003Cmgr_003E5__2 = highPressureSystemManager2;
				}
				if (_003Cmgr_003E5__2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+56]");
					if ((nint)0 != 0)
					{
						HighPressureSystemManager highPressureSystemManager3 = _003Cmgr_003E5__2;
						if ((object)_003Cmgr_003E5__2 == null)
						{
							goto IL_0537;
						}
						if (ValveBreakBlocker.IsSystemBlocked(highPressureSystemManager3.systemId))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+55]");
							if ((nint)0 != 0)
							{
								HighPressureSystemManager highPressureSystemManager4 = _003Cmgr_003E5__2;
								if ((object)_003Cmgr_003E5__2 == null)
								{
									goto IL_0537;
								}
								string message = "[ValveAutoAddDamageOnEnable] Target system '" + highPressureSystemManager4.systemId + "' is blocked. Aborting.";
								Debug.Log(message, _003C_003E4__this);
							}
							_ = 0;
							goto IL_030a;
						}
					}
					HighPressureSystemManager highPressureSystemManager5 = _003Cmgr_003E5__2;
					if ((object)_003Cmgr_003E5__2 == null || highPressureSystemManager5.valves == null)
					{
						goto IL_0537;
					}
					ReadOnlyCollection<ValveController> readOnlyCollection = highPressureSystemManager5.valves.AsReadOnly();
					if (readOnlyCollection != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						object obj2 = default(object);
						if ((nint)obj2 > 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							int capacity = default(int);
							List<ValveController> list = new List<ValveController>(capacity);
							object obj3 = 0;
							object obj4 = default(object);
							UnityEngine.Object obj5 = default(UnityEngine.Object);
							ValveController item = default(ValveController);
							while (true)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
								if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
								bool flag2 = obj5 != null;
								bool flag3 = !flag2;
								object obj6 = obj3;
								if (flag3)
								{
									goto IL_0472;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
								if (list != null)
								{
									list.Add(item);
									obj6 = obj3;
									goto IL_0472;
								}
								goto IL_0537;
								IL_0472:
								obj3++;
							}
							if (list == null)
							{
								goto IL_0537;
							}
							if (list._size > 0)
							{
								goto IL_030a;
							}
						}
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+44]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rdi_v1 (UnityEngine.Object)+48]");
					WaitForSeconds waitForSeconds = new WaitForSeconds(0f);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
			}
			goto IL_030a;
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

	private HighPressureSystemManager explicitManager;

	private string systemId;

	private TargetScope targetScope;

	private int burstCountMin;

	private int burstCountMax;

	private float probability;

	private float delaySeconds;

	private float waitForManagerSeconds;

	private float managerPollInterval;

	private float addDamageMin;

	private float addDamageMax;

	private bool onlyOnce;

	private bool logAttempts;

	private bool respectGlobalBlocker;

	private bool alreadyTriggered;

	private List<ValveController> _pendingPool;

	private void OnEnable()
	{
		if (!onlyOnce || !alreadyTriggered)
		{
			IEnumerator routine = BurstRoutine(ignoreProbability: false);
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	public void TriggerNow()
	{
		if (!onlyOnce || !alreadyTriggered)
		{
			IEnumerator routine = BurstRoutine(ignoreProbability: false);
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	public void TriggerNowIgnoreProbability()
	{
		if (!onlyOnce || !alreadyTriggered)
		{
			IEnumerator routine = BurstRoutine(ignoreProbability: true);
			Coroutine coroutine = StartCoroutine(routine);
		}
	}

	private IEnumerator BurstRoutine(bool ignoreProbability)
	{
		_003CBurstRoutine_003Ed__19 obj = new _003CBurstRoutine_003Ed__19(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.ignoreProbability = ignoreProbability;
		return obj;
	}

	private IEnumerator WaitForAnySystemPool(float waitStart)
	{
		_003CWaitForAnySystemPool_003Ed__21 obj = new _003CWaitForAnySystemPool_003Ed__21(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.waitStart = waitStart;
		return obj;
	}

	private IEnumerator WaitForSpecificSystemPool(float waitStart)
	{
		_003CWaitForSpecificSystemPool_003Ed__22 obj = new _003CWaitForSpecificSystemPool_003Ed__22(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.waitStart = waitStart;
		return obj;
	}

	private static void ShufflePartial(List<ValveController> list, int count)
	{
		if (count > 0)
		{
			int num = 0;
			ValveController value = default(ValveController);
			ValveController value2 = default(ValveController);
			do
			{
				int index = UnityEngine.Random.Range(num, list._size);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				list.set_Item(num, value);
				list.set_Item(index, value2);
				num++;
			}
			while (num < count);
		}
	}

	private static string TryGetValveSystemId(ValveController valve)
	{
		//IL_029e: Expected O, but got I
		//IL_02ae: Expected O, but got I
		//IL_00d3: Expected I, but got O
		//IL_0162: Expected I4, but got O
		//IL_010b: Expected O, but got I
		//IL_02e0: Expected O, but got I4
		//IL_022b: Expected O, but got I4
		//IL_0241: Expected O, but got I
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Expected O, but got Unknown
		if (HighPressureSystemManager.registry != null)
		{
			Dictionary<string, HighPressureSystemManager>.ValueCollection values = HighPressureSystemManager.registry.Values;
			List<HighPressureSystemManager> list = new List<HighPressureSystemManager>(values);
			if (list != null)
			{
				ReadOnlyCollection<HighPressureSystemManager> readOnlyCollection = list.AsReadOnly();
				bool flag = readOnlyCollection == null;
				int num = 0;
				int num2 = 0;
				if (!flag)
				{
					object obj = default(object);
					object obj7 = default(object);
					object obj9 = default(object);
					UnityEngine.Object obj10 = default(UnityEngine.Object);
					while (true)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (num2 < (nint)obj)
						{
							nint num3 = (nint)readOnlyCollection;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v4 (Il2CppClass<System.Collections.ObjectModel.ReadOnlyCollection`1<HighPressureSystemManager>>)+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_014b;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v4 (Il2CppClass<System.Collections.ObjectModel.ReadOnlyCollection`1<HighPressureSystemManager>>)+B0]");
							object obj2 = 0;
							int num4 = 0;
							while (true)
							{
								object obj3 = num4 + num4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v346 @ r8_v14+v351 @ rcx_v28*8]");
								if (0 == (nint)typeof(IReadOnlyList<HighPressureSystemManager>))
								{
									break;
								}
								num4++;
								int num5 = num4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v4 (Il2CppClass<System.Collections.ObjectModel.ReadOnlyCollection`1<HighPressureSystemManager>>)+12E]");
								if ((nint)num5 < (nint)0)
								{
									continue;
								}
								goto IL_014b;
							}
							object obj4 = num4 + num4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v346 @ r8_v14+8+v429 @ rcx_v30*8]");
							object obj5 = (nint)0 << 4;
							object obj6 = obj5 + 312;
							obj7 = obj6 + num3;
							goto IL_015a;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
						object obj8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ rax_v15+B8]");
						return (string)0;
						IL_015a:
						int num6 = (int)obj7;
						HighPressureSystemManager highPressureSystemManager = ((IReadOnlyList<HighPressureSystemManager>)readOnlyCollection).get_Item(num);
						if ((object)highPressureSystemManager == null || highPressureSystemManager.valves == null)
						{
							break;
						}
						ReadOnlyCollection<ValveController> readOnlyCollection2 = highPressureSystemManager.valves.AsReadOnly();
						bool flag2 = readOnlyCollection2 == null;
						int num7 = 0;
						if (flag2)
						{
							break;
						}
						while (true)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							if (num7 >= (nint)obj9)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
							if (obj10 != valve)
							{
								int num8 = num7 + 1;
								num6 = num7;
								num7 = num8;
								continue;
							}
							return highPressureSystemManager.systemId;
						}
						num++;
						num2 = num;
						continue;
						IL_014b:
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
						goto IL_015a;
					}
				}
			}
		}
		return (string)(object)new NullReferenceException();
	}

	private void MarkTriggeredIfOnce()
	{
		if (onlyOnce)
		{
			alreadyTriggered = true;
		}
	}

	public ValveAutoAddDamageOnEnable()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A2A8]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		systemId = "Default";
		targetScope = TargetScope.AnySystem;
		burstCountMin = 1;
		burstCountMax = 1;
		probability = 1f;
		waitForManagerSeconds = 5f;
		managerPollInterval = 0.25f;
		addDamageMin = 0.1f;
		addDamageMax = 0.5f;
		onlyOnce = true;
		respectGlobalBlocker = true;
		base._002Ector();
	}
}
