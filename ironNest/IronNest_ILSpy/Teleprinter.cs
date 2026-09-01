using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Teleprinter : MonoBehaviour
{
	public enum Teleprinters
	{
		Primary,
		Secondary
	}

	public enum TeleprinterAlarmState
	{
		None,
		High,
		Low,
		Sucess
	}

	[Serializable]
	public class TeleprinterCharacterEvent : UnityEvent<char, int, int>
	{
	}

	[Serializable]
	public class TeleprinterLineTransitionEvent : UnityEvent<int, int>
	{
	}

	public enum CursorLockMode
	{
		None,
		LockYOnly,
		LockYAndZ,
		LockYAndX
	}

	public enum PrintingOrder
	{
		TopDown,
		BottomUp
	}

	private struct LineRange
	{
		public int lineNumber;

		public int firstCharIndex;

		public int lastCharIndex;
	}

	private sealed class _003CMoveCursor_003Ed__98 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		public Vector3 fromWorld;

		public Vector3 toWorld;

		private float _003Cduration_003E5__2;

		private float _003Ct_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CMoveCursor_003Ed__98(int _003C_003E1__state)
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
			//IL_0300: Expected I4, but got I8
			//IL_04ca: Expected I4, but got O
			//IL_0322: Expected O, but got I
			//IL_0419: Unknown result type (might be due to invalid IL or missing references)
			//IL_041e: Expected O, but got Unknown
			//IL_0071: Invalid comparison between I4 and F4
			//IL_038a: Unknown result type (might be due to invalid IL or missing references)
			//IL_038f: Expected O, but got Unknown
			//IL_0248: Unknown result type (might be due to invalid IL or missing references)
			//IL_024d: Expected O, but got Unknown
			//IL_046c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0471: Expected O, but got Unknown
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Expected O, but got Unknown
			//IL_00e9: Expected O, but got I
			//IL_0504: Expected I, but got O
			//IL_0521: Expected O, but got I
			//IL_053e: Expected O, but got I
			//IL_055b: Expected O, but got I
			//IL_01f0: Expected F8, but got I4
			Teleprinter teleprinter = _003C_003E4__this;
			object obj15 = default(object);
			object arg;
			string format;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_04bc;
				}
				if ((bool)teleprinter.typerCursor)
				{
					if (0f < teleprinter.cursorMaxSpeed && !teleprinter.skipAnimation)
					{
						_ = fromWorld;
						_ = toWorld;
						object obj = fromWorld - toWorld;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+30]");
						nint num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+3C]");
						object obj2 = num - 0;
						object obj4 = default(object);
						object obj3 = obj4 - obj4;
						object obj5 = obj3 * obj3;
						object obj6 = obj * obj;
						double num2 = (double)obj2 * (double)obj2;
						object obj7 = obj5 + obj6;
						double num3 = (double)obj7 + num2;
						if (!(9.999999439624929E-11 > num3))
						{
							_ = toWorld;
							_ = fromWorld;
							nint num4 = (nint)typeof(Math);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-50]");
							nint num5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-40]");
							object obj8 = num5 - 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-4C]");
							nint num6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-3C]");
							object obj9 = num6 - 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+30]");
							nint num7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+3C]");
							object obj10 = num7 - 0;
							object obj11 = obj9 * obj9;
							object obj12 = obj8 * obj8;
							object obj13 = obj10 * obj10;
							object obj14 = obj11 + obj12;
							double d = (double)obj14 + (double)obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rcx_v21 (Il2CppClass<System.Math>)+E4]");
							double num8;
							if ((nint)0 <= (nint)0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
								num8 = 0.0;
							}
							else
							{
								num8 = Math.Sqrt(d);
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
							double num9 = num8 / (double)teleprinter.cursorMaxSpeed;
							_003Ct_003E5__3 = 0f;
							_003Cduration_003E5__2 = (float)num9;
							goto IL_0560;
						}
					}
					Vector3 cursorPosition = (Vector3)(obj15 - 64);
					_ = toWorld;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+3C]");
					_ = 0;
					_003C_003E4__this.SetCursorPosition(cursorPosition);
					if (teleprinter.debugCursor)
					{
						object obj16 = obj15 - 64;
						_ = toWorld;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+3C]");
						_ = 0;
						arg = (Vector3)obj16;
						format = "[Teleprinter] Cursor snap to {0}";
						goto IL_0583;
					}
				}
			}
			else if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				goto IL_0560;
			}
			goto IL_04ae;
			IL_0560:
			if (_003Cduration_003E5__2 > _003Ct_003E5__3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+3C]");
				nint num10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+30]");
				object obj17 = num10 - 0;
				float num11 = _003Ct_003E5__3 / _003Cduration_003E5__2;
				_ = fromWorld;
				float num12 = (float)obj17 * num11;
				float num13 = num12;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+30]");
				float num14 = num13 + 0f;
				if ((object)_003C_003E4__this != null)
				{
					Vector3 cursorPosition2 = (Vector3)(obj15 - 64);
					_003C_003E4__this.SetCursorPosition(cursorPosition2);
					float deltaTime = Time.deltaTime;
					float num15 = deltaTime + _003Ct_003E5__3;
					_003C_003E2__current = null;
					_003Ct_003E5__3 = num15;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else if ((object)_003C_003E4__this != null)
			{
				Vector3 cursorPosition3 = (Vector3)(obj15 - 64);
				_ = toWorld;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+3C]");
				_ = 0;
				_003C_003E4__this.SetCursorPosition(cursorPosition3);
				if (!teleprinter.debugCursor)
				{
					goto IL_04ae;
				}
				object obj18 = obj15 - 64;
				_ = toWorld;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MoveCursor>d__98)+3C]");
				_ = 0;
				arg = (Vector3)obj18;
				format = "[Teleprinter] Cursor final {0}";
				goto IL_0583;
			}
			goto IL_04bc;
			IL_0583:
			string message = string.Format(format, arg);
			Debug.Log(message);
			goto IL_04ae;
			IL_04bc:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_04ae:
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

	private sealed class _003CMovePaper_003Ed__97 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		public float lineDeltaLocal;

		public int lineCount;

		public bool compensateCursorWorldPosition;

		private Vector3 _003CstartLocal_003E5__2;

		private Vector3 _003CendLocal_003E5__3;

		private float _003Cduration_003E5__4;

		private float _003Ct_003E5__5;

		private Vector3 _003CprevPaperLocal_003E5__6;

		private bool _003CdoRotate_003E5__7;

		private Quaternion _003CstartRotLocal_003E5__8;

		private Quaternion _003CendRotLocal_003E5__9;

		private Quaternion _003CstartRotWorld_003E5__10;

		private Quaternion _003CendRotWorld_003E5__11;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CMovePaper_003Ed__97(int _003C_003E1__state)
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
			//IL_001c: Expected I4, but got I8
			//IL_077b: Expected I4, but got I8
			//IL_110e: Expected I4, but got O
			//IL_07a4: Expected O, but got I
			//IL_0076: Invalid comparison between F4 and I4
			//IL_0cfb: Expected O, but got Ref
			//IL_00a2: Invalid comparison between I4 and F4
			//IL_0833: Expected O, but got Ref
			//IL_038e: Expected O, but got Ref
			//IL_0d9b: Expected O, but got I
			//IL_10ad: Expected O, but got Ref
			//IL_0121: Expected O, but got F4
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Expected O, but got Unknown
			//IL_018e: Expected O, but got F4
			//IL_105f: Expected O, but got Ref
			//IL_0c33: Expected O, but got Ref
			//IL_0bbf: Expected O, but got Ref
			//IL_025e: Expected F4, but got I4
			//IL_1262: Expected I4, but got F4
			//IL_1148: Expected I, but got O
			//IL_116f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1174: Expected O, but got Unknown
			//IL_1184: Unknown result type (might be due to invalid IL or missing references)
			//IL_1189: Expected O, but got Unknown
			//IL_11a6: Expected O, but got I
			//IL_11f0: Invalid comparison between F4 and O
			//IL_120f: Invalid comparison between F4 and I4
			//IL_1238: Expected O, but got I4
			//IL_0e40: Expected O, but got Ref
			//IL_0961: Expected O, but got Ref
			//IL_0212: Unknown result type (might be due to invalid IL or missing references)
			//IL_0217: Expected O, but got Unknown
			//IL_022f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0234: Expected O, but got Unknown
			//IL_0250: Expected F4, but got I4
			//IL_0fca: Expected O, but got Ref
			//IL_0ea2: Expected O, but got Ref
			//IL_026e: Invalid comparison between F4 and I4
			//IL_0aeb: Expected O, but got Ref
			//IL_09c3: Expected O, but got Ref
			//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b8: Expected O, but got Unknown
			//IL_04b8: Expected O, but got Ref
			//IL_12ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_12b1: Expected O, but got Unknown
			//IL_0642: Expected O, but got Ref
			//IL_0515: Expected O, but got Ref
			//IL_0338: Expected O, but got F4
			//IL_02e1: Expected O, but got F4
			object obj2 = default(object);
			object obj = (object)(&obj2);
			Teleprinter teleprinter = _003C_003E4__this;
			Vector3 vector = default(Vector3);
			float num7;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_1100;
				}
				if ((bool)teleprinter.paperTransform)
				{
					bool flag = lineDeltaLocal == 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180490EBEh\"");
					if (!flag)
					{
						if (0f < teleprinter.paperFeedSpeed && !teleprinter.skipAnimation)
						{
							if ((object)teleprinter.paperTransform != null)
							{
								Vector3 localPosition = teleprinter.paperTransform.localPosition;
								_003CstartLocal_003E5__2 = (Vector3)localPosition.x;
								_ = localPosition.z;
								_ = localPosition.x;
								_003CendLocal_003E5__3 = vector;
								float num = lineDeltaLocal;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
								object obj3 = num & 0;
								_ = localPosition.z;
								float num2 = (float)obj3 / teleprinter.paperFeedSpeed;
								_003Ct_003E5__5 = 0f;
								_003CprevPaperLocal_003E5__6 = (Vector3)localPosition.x;
								_003Cduration_003E5__4 = num2;
								_ = localPosition.z;
								if ((bool)teleprinter.rotateTransform && lineCount != 0)
								{
									_ = teleprinter.rotationAxis;
									nint num3 = (nint)typeof(Vector3);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1669 @ rax_v97 (Il2CppClass<UnityEngine.Vector3>)+B8]");
									nint num4 = 0;
									_ = Vector3.zeroVector;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-69]");
									object obj4 = 0 - Vector3.zeroVector;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-65]");
									object obj5 = 0 - vector;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rsi_v1 (Teleprinter)+80]");
									nint num5 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1589 @ rcx_v88 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
									object obj6 = num5 - 0;
									object obj7 = obj5 * obj5;
									object obj8 = obj4 * obj4;
									object obj9 = obj6 * obj6;
									object obj10 = obj7 + obj8;
									object obj11 = obj10 + obj9;
									bool flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11);
									float num6 = 9.9999994E-11f - (float)obj11;
									bool flag3 = num6 == 0f;
									bool flag4 = !flag2;
									bool flag5 = !flag3;
									object obj12 = flag5 & flag4;
									if (obj12 == null)
									{
										object obj13 = teleprinter.degreesPerLine & -2147483649L;
										bool flag6 = (nint)obj13 < 2139095040;
										object obj14 = obj13 - 2139095040;
										bool flag7 = obj14 == null;
										num7 = ((flag6 | flag7) ? 1 : 0);
										goto IL_1258;
									}
								}
								num7 = 0f;
								goto IL_1258;
							}
						}
						else if ((object)teleprinter.paperTransform != null)
						{
							Vector3 localPosition2 = teleprinter.paperTransform.localPosition;
							Vector3 localPosition3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
							_ = localPosition2.x;
							_ = localPosition2.z;
							teleprinter.paperTransform.localPosition = localPosition3;
							if (!compensateCursorWorldPosition || !teleprinter.typerCursor)
							{
								goto IL_066b;
							}
							if ((object)teleprinter.typerCursor != null)
							{
								Transform transform = teleprinter.typerCursor.transform;
								if ((object)transform != null)
								{
									if (!transform.IsChildOf(teleprinter.paperTransform))
									{
										if ((object)teleprinter.paperTransform != null)
										{
											_ = 0;
											Vector3 vector2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
											Vector3 vector3 = teleprinter.paperTransform.TransformVector(vector2);
											if ((object)teleprinter.typerCursor != null)
											{
												Vector3 position = teleprinter.typerCursor.position;
												Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
												_ = position.x;
												_ = vector3.x;
												float num8 = position.z - vector3.z;
												teleprinter.typerCursor.position = position2;
												goto IL_066b;
											}
										}
									}
									else if ((object)teleprinter.typerCursor != null)
									{
										Vector3 localPosition4 = teleprinter.typerCursor.localPosition;
										float z = localPosition4.z;
										object obj15 = vector - lineDeltaLocal;
										_ = localPosition4.x;
										if (teleprinter.preserveLocalZ)
										{
											if ((object)teleprinter.typerCursor == null)
											{
												goto IL_1100;
											}
											z = teleprinter.typerCursor.localPosition.z;
										}
										if ((object)teleprinter.typerCursor != null)
										{
											Vector3 localPosition5 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-69]");
											_ = 0;
											teleprinter.typerCursor.localPosition = localPosition5;
											goto IL_066b;
										}
									}
								}
							}
						}
						goto IL_1100;
					}
				}
				if ((bool)teleprinter.rotateTransform && lineCount != 0)
				{
					_003C_003E4__this.ApplyInstantRotation(lineDeltaLocal, lineCount);
				}
			}
			else if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				goto IL_133b;
			}
			goto IL_073c;
			IL_0feb:
			if (!_003CdoRotate_003E5__7)
			{
				goto IL_10cb;
			}
			if (!teleprinter.rotateInLocalSpace)
			{
				if ((object)teleprinter.rotateTransform != null)
				{
					Quaternion rotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
					_ = _003CendRotWorld_003E5__11;
					teleprinter.rotateTransform.rotation = rotation;
					goto IL_10cb;
				}
			}
			else if ((object)teleprinter.rotateTransform != null)
			{
				Quaternion localRotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
				_ = _003CendRotLocal_003E5__9;
				teleprinter.rotateTransform.localRotation = localRotation;
				goto IL_10cb;
			}
			goto IL_1100;
			IL_10cb:
			if (teleprinter.onLineFeed != null)
			{
				teleprinter.onLineFeed.Invoke();
			}
			goto IL_073c;
			IL_066b:
			if ((bool)teleprinter.rotateTransform && lineCount != 0)
			{
				_003C_003E4__this.ApplyInstantRotation(lineDeltaLocal, lineCount);
			}
			goto IL_10cb;
			IL_1100:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0b0c:
			if (!_003CdoRotate_003E5__7)
			{
				goto IL_0c54;
			}
			ref Quaternion b = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
			ref Quaternion a = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
			float num9;
			if (!teleprinter.rotateInLocalSpace)
			{
				_ = _003CendRotWorld_003E5__11;
				_ = _003CstartRotWorld_003E5__10;
				Quaternion quaternion = Quaternion.Internal_Slerp(ref a, ref b, num9);
				if ((object)teleprinter.rotateTransform != null)
				{
					Quaternion rotation2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
					_ = quaternion.x;
					teleprinter.rotateTransform.rotation = rotation2;
					goto IL_0c54;
				}
			}
			else
			{
				_ = _003CendRotLocal_003E5__9;
				_ = _003CstartRotLocal_003E5__8;
				Quaternion quaternion2 = Quaternion.Internal_Slerp(ref a, ref b, num9);
				if ((object)teleprinter.rotateTransform != null)
				{
					Quaternion localRotation2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
					_ = quaternion2.x;
					teleprinter.rotateTransform.localRotation = localRotation2;
					goto IL_0c54;
				}
			}
			goto IL_1100;
			IL_133b:
			if (_003Cduration_003E5__4 > _003Ct_003E5__5)
			{
				_ = _003CendLocal_003E5__3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MovePaper>d__97)+48]");
				nint num10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MovePaper>d__97)+3C]");
				object obj16 = num10 - 0;
				num9 = _003Ct_003E5__5 / _003Cduration_003E5__4;
				_ = _003CstartLocal_003E5__2;
				float num11 = (float)obj16 * num9;
				float num12 = num11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MovePaper>d__97)+3C]");
				float num13 = num12 + 0f;
				if ((object)_003C_003E4__this != null && (object)teleprinter.paperTransform != null)
				{
					Vector3 localPosition6 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
					teleprinter.paperTransform.localPosition = localPosition6;
					if (!compensateCursorWorldPosition || !teleprinter.typerCursor)
					{
						goto IL_0b0c;
					}
					_ = _003CprevPaperLocal_003E5__6;
					float num14 = num13;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MovePaper>d__97)+5C]");
					float num15 = num14 - 0f;
					if ((object)teleprinter.typerCursor != null)
					{
						Transform transform2 = teleprinter.typerCursor.transform;
						if ((object)transform2 != null)
						{
							if (!transform2.IsChildOf(teleprinter.paperTransform))
							{
								if ((object)teleprinter.paperTransform != null)
								{
									Vector3 vector4 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
									Vector3 vector5 = teleprinter.paperTransform.TransformVector(vector4);
									if ((object)teleprinter.typerCursor != null)
									{
										Vector3 position3 = teleprinter.typerCursor.position;
										Vector3 position4 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
										_ = position3.x;
										_ = vector5.x;
										float num16 = position3.z - vector5.z;
										teleprinter.typerCursor.position = position4;
										goto IL_0b0c;
									}
								}
							}
							else if ((object)teleprinter.typerCursor != null)
							{
								Vector3 localPosition7 = teleprinter.typerCursor.localPosition;
								float z2 = localPosition7.z;
								_ = localPosition7.x;
								_ = localPosition7.z;
								_ = localPosition7.x;
								if (teleprinter.preserveLocalZ)
								{
									if ((object)teleprinter.typerCursor == null)
									{
										goto IL_1100;
									}
									z2 = teleprinter.typerCursor.localPosition.z;
								}
								if ((object)teleprinter.typerCursor != null)
								{
									Vector3 localPosition8 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
									teleprinter.typerCursor.localPosition = localPosition8;
									goto IL_0b0c;
								}
							}
						}
					}
				}
			}
			else if ((object)_003C_003E4__this != null && (object)teleprinter.paperTransform != null)
			{
				Vector3 localPosition9 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
				_ = _003CendLocal_003E5__3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MovePaper>d__97)+48]");
				_ = 0;
				teleprinter.paperTransform.localPosition = localPosition9;
				if (!compensateCursorWorldPosition || !teleprinter.typerCursor)
				{
					goto IL_0feb;
				}
				_ = _003CendLocal_003E5__3;
				_ = _003CprevPaperLocal_003E5__6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MovePaper>d__97)+48]");
				nint num17 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<MovePaper>d__97)+5C]");
				object obj17 = num17 - 0;
				if ((object)teleprinter.typerCursor != null)
				{
					Transform transform3 = teleprinter.typerCursor.transform;
					if ((object)transform3 != null)
					{
						if (!transform3.IsChildOf(teleprinter.paperTransform))
						{
							if ((object)teleprinter.paperTransform != null)
							{
								Vector3 vector6 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
								Vector3 vector7 = teleprinter.paperTransform.TransformVector(vector6);
								if ((object)teleprinter.typerCursor != null)
								{
									Vector3 position5 = teleprinter.typerCursor.position;
									Vector3 position6 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
									_ = position5.x;
									_ = vector7.x;
									float num18 = position5.z - vector7.z;
									teleprinter.typerCursor.position = position6;
									goto IL_0feb;
								}
							}
						}
						else if ((object)teleprinter.typerCursor != null)
						{
							Vector3 localPosition10 = teleprinter.typerCursor.localPosition;
							float z3 = localPosition10.z;
							_ = localPosition10.x;
							_ = localPosition10.z;
							_ = localPosition10.x;
							if (teleprinter.preserveLocalZ)
							{
								if ((object)teleprinter.typerCursor == null)
								{
									goto IL_1100;
								}
								z3 = teleprinter.typerCursor.localPosition.z;
							}
							if ((object)teleprinter.typerCursor != null)
							{
								Vector3 localPosition11 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
								teleprinter.typerCursor.localPosition = localPosition11;
								goto IL_0feb;
							}
						}
					}
				}
			}
			goto IL_1100;
			IL_0c54:
			_003CprevPaperLocal_003E5__6 = vector;
			float deltaTime = Time.deltaTime;
			float num19 = deltaTime + _003Ct_003E5__5;
			_003C_003E2__current = null;
			_003Ct_003E5__5 = num19;
			_003C_003E1__state = 1;
			return true;
			IL_073c:
			return false;
			IL_1258:
			_003CdoRotate_003E5__7 = (byte)(int)num7 != 0;
			_003CstartRotLocal_003E5__8 = Quaternion.identityQuaternion;
			_003CendRotLocal_003E5__9 = Quaternion.identityQuaternion;
			_003CstartRotWorld_003E5__10 = Quaternion.identityQuaternion;
			_003CendRotWorld_003E5__11 = Quaternion.identityQuaternion;
			if (_003CdoRotate_003E5__7)
			{
				float num20 = ((lineDeltaLocal < 0f) ? (-1f) : 1f);
				Vector3 vector8 = (Vector3)(_003C_003E4__this + 120);
				Vector3 normalized = ((Vector3*)vector8)->normalized;
				ref Vector3 axis = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
				float num21 = (float)lineCount * teleprinter.degreesPerLine;
				_ = normalized.z;
				_ = normalized.x;
				float angle = num21 * num20;
				Quaternion quaternion3 = Quaternion.Internal_AngleAxis(angle, ref axis);
				if (!teleprinter.rotateInLocalSpace)
				{
					if ((object)teleprinter.rotateTransform == null)
					{
						goto IL_1100;
					}
					_003CstartRotWorld_003E5__10 = (Quaternion)teleprinter.rotateTransform.rotation.x;
					_003CendRotWorld_003E5__11 = (Quaternion)vector;
				}
				else
				{
					if ((object)teleprinter.rotateTransform == null)
					{
						goto IL_1100;
					}
					_003CstartRotLocal_003E5__8 = (Quaternion)teleprinter.rotateTransform.localRotation.x;
					_003CendRotLocal_003E5__9 = (Quaternion)vector;
				}
			}
			goto IL_133b;
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

	private sealed class _003CRunQueue_003Ed__91 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		private PrintJob _003Cjob_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRunQueue_003Ed__91(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_007a: Expected I4, but got I8
			//IL_06f2: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0066: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Expected Ref, but got Unknown
			//IL_0567: Expected O, but got Ref
			//IL_0335: Expected I, but got O
			//IL_034d: Expected O, but got I4
			//IL_0270: Expected I, but got O
			//IL_06a4: Expected O, but got Ref
			//IL_07f3: Expected I, but got O
			//IL_066b: Expected O, but got Ref
			//IL_07e0: Expected I, but got O
			//IL_02fc: Expected I, but got O
			Teleprinter teleprinter = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (flag)
				{
					_003C_003E1__state = -1;
					goto IL_0374;
				}
				if ((nint)obj != 1)
				{
					goto IL_06de;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_06e4;
				}
				Action onPrintingWillStart = teleprinter.OnPrintingWillStart;
				teleprinter._isRunning = true;
				if (teleprinter.OnPrintingWillStart != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v67.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
				}
				if (teleprinter.onTypingStarted != null)
				{
					teleprinter.onTypingStarted.Invoke();
				}
			}
			if ((object)_003C_003E4__this != null)
			{
				goto IL_0117;
			}
			goto IL_06e4;
			IL_06e4:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_075f:
			string text2;
			nint num;
			if (teleprinter.padEmptyLines)
			{
				string text = _003C_003E4__this.PadEmptyLines(text2);
				text2 = text;
				num = unchecked((nint)null);
			}
			if (teleprinter.skipAnimation)
			{
				object obj2 = teleprinter.printingOrder - 1;
				bool prepend = obj2 == null;
				_003C_003E4__this.AppendInstant(text2, prepend);
				goto IL_0374;
			}
			if (teleprinter.printingOrder == PrintingOrder.BottomUp)
			{
				nint num2 = (nint)typeof(_003CTypeChunkBottomUp_003Ed__95);
			}
			else
			{
				nint num2 = (nint)typeof(_003CTypeChunkTopDown_003Ed__94);
			}
			object obj3 = null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			_ = 0;
			_ = _003C_003E4__this;
			_003C_003E2__current = obj3;
			_003C_003E1__state = 1;
			return true;
			IL_06de:
			return false;
			IL_06a9:
			if (teleprinter.onAllJobsCompleted != null)
			{
				teleprinter.onAllJobsCompleted.Invoke();
			}
			goto IL_06de;
			IL_0192:
			if (teleprinter.onJobStarted != null)
			{
				teleprinter.onJobStarted.Invoke();
			}
			PrintJob printJob = _003Cjob_003E5__2;
			if (_003Cjob_003E5__2 == null)
			{
				goto IL_06e4;
			}
			IEnumerable<string> lines = printJob.lines;
			if (teleprinter.printingOrder == PrintingOrder.BottomUp)
			{
				if (printJob.lines != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v504 @ rdi_v10 (System.Collections.Generic.IEnumerable`1<System.String>)+18]");
					if ((nint)0 != 0)
					{
						goto IL_024c;
					}
				}
			}
			else if (printJob.lines != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v504 @ rdi_v10 (System.Collections.Generic.IEnumerable`1<System.String>)+18]");
				if ((nint)0 != 0)
				{
					if (teleprinter._currentRevealedCharIndex == 0)
					{
						goto IL_024c;
					}
					string text3 = string.Join("\n", printJob.lines);
					string text4 = "\n" + text3;
					text2 = text4;
					num = unchecked((nint)null);
					goto IL_075f;
				}
			}
			text2 = "";
			num = 0;
			goto IL_075f;
			IL_0117:
			while (teleprinter._pendingJobs != null)
			{
				if (teleprinter._pendingJobs.TryDequeue(out *(PrintJob*)(this + 40)))
				{
					if (_003Cjob_003E5__2 == null)
					{
						continue;
					}
					goto IL_0192;
				}
				goto IL_04bf;
			}
			goto IL_06e4;
			IL_024c:
			string text5 = string.Join("\n", printJob.lines);
			text2 = text5;
			num = unchecked((nint)null);
			goto IL_075f;
			IL_04bf:
			teleprinter._runner = null;
			teleprinter._isRunning = false;
			_003C_003E4__this.SetTypingAnimator(state: false);
			Vector3 initialPaperLocalPos = default(Vector3);
			if (teleprinter.resetPaperPositionOnComplete && !teleprinter.accumulatePaperFeed)
			{
				if ((object)teleprinter.paperTransform == null)
				{
					goto IL_06e4;
				}
				teleprinter.paperTransform.localPosition = (Vector3)(&initialPaperLocalPos);
				initialPaperLocalPos = teleprinter._initialPaperLocalPos;
			}
			if (!teleprinter.resetRotationOnComplete || !teleprinter.rotateTransform || teleprinter.accumulatePaperFeed || !teleprinter._initialRotateStored)
			{
				goto IL_06a9;
			}
			if (!teleprinter.rotateInLocalSpace)
			{
				if ((object)teleprinter.rotateTransform != null)
				{
					teleprinter.rotateTransform.rotation = (Quaternion)(&initialPaperLocalPos);
					goto IL_06a9;
				}
			}
			else if ((object)teleprinter.rotateTransform != null)
			{
				teleprinter.rotateTransform.localRotation = (Quaternion)(&initialPaperLocalPos);
				goto IL_06a9;
			}
			goto IL_06e4;
			IL_0374:
			PrintJob printJob2 = _003Cjob_003E5__2;
			if (_003Cjob_003E5__2 != null)
			{
				printJob2.complete = true;
				if ((object)_003C_003E4__this != null)
				{
					if (teleprinter.onJobCompleted != null)
					{
						teleprinter.onJobCompleted.Invoke();
					}
					if (teleprinter._waitInterJob != null)
					{
						Queue<PrintJob> pendingJobs = teleprinter._pendingJobs;
						if (teleprinter._pendingJobs == null)
						{
							goto IL_06e4;
						}
						if (pendingJobs._size > 0)
						{
							_003C_003E2__current = teleprinter._waitInterJob;
							_003C_003E1__state = 2;
							return true;
						}
					}
					goto IL_0117;
				}
			}
			goto IL_06e4;
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

	private sealed class _003CTypeChunkBottomUp_003Ed__95 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		public string chunkRich;

		private string _003CnewFull_003E5__2;

		private TMP_TextInfo _003Cti_003E5__3;

		private int _003CnewCount_003E5__4;

		private List<LineRange> _003Clines_003E5__5;

		private int _003CfromLine_003E5__6;

		private int _003CtoLine_003E5__7;

		private LineRange _003Clr_003E5__8;

		private TMP_CharacterInfo _003Cci_003E5__9;

		private char _003Cc_003E5__10;

		private bool _003CisLineBreakChar_003E5__11;

		private Vector3 _003CtargetPos_003E5__12;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CTypeChunkBottomUp_003Ed__95(int _003C_003E1__state)
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
			//IL_22da: Expected O, but got I4
			//IL_0287: Expected I4, but got I8
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_0196: Expected I4, but got I8
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_23b5: Expected I, but got O
			//IL_0106: Expected I4, but got I8
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0322: Expected I, but got O
			//IL_032c: Expected I, but got O
			//IL_033c: Expected O, but got I
			//IL_0266: Expected O, but got I4
			//IL_015e: Expected O, but got I4
			//IL_0169: Expected I, but got O
			//IL_00d4: Expected I4, but got I8
			//IL_00d9: Expected I, but got O
			//IL_0390: Expected I, but got O
			//IL_03de: Expected I, but got O
			//IL_1962: Expected F4, but got O
			//IL_2186: Expected I, but got O
			//IL_1a49: Expected O, but got F4
			//IL_00aa: Expected I4, but got I8
			//IL_0409: Unknown result type (might be due to invalid IL or missing references)
			//IL_040e: Expected O, but got Unknown
			//IL_1994: Expected F4, but got O
			//IL_1d53: Expected F4, but got O
			//IL_0427: Expected I, but got O
			//IL_1a73: Expected O, but got Ref
			//IL_1636: Expected I4, but got O
			//IL_163e: Expected F4, but got O
			//IL_164c: Expected O, but got Ref
			//IL_19c6: Expected F4, but got O
			//IL_2237: Expected I, but got O
			//IL_2245: Expected O, but got Ref
			//IL_27db: Expected I4, but got O
			//IL_19f8: Expected F4, but got O
			//IL_04bd: Expected O, but got F4
			//IL_1ff1: Expected F4, but got O
			//IL_167c: Expected F4, but got O
			//IL_1e4b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e50: Expected O, but got Unknown
			//IL_1e91: Expected I, but got O
			//IL_1e9a: Expected O, but got I4
			//IL_1b2a: Expected O, but got Ref
			//IL_072c: Expected I, but got O
			//IL_0745: Expected F4, but got O
			//IL_1ee3: Expected O, but got I4
			//IL_1eeb: Expected F4, but got O
			//IL_1e16: Expected O, but got Ref
			//IL_1e37: Expected O, but got I
			//IL_1acb: Expected I, but got O
			//IL_203c: Expected O, but got Ref
			//IL_2044: Expected F4, but got O
			//IL_16b3: Expected F4, but got O
			//IL_1b65: Expected I, but got O
			//IL_1b75: Expected O, but got I
			//IL_052d: Expected O, but got F4
			//IL_208b: Expected O, but got Ref
			//IL_2093: Expected F4, but got O
			//IL_16cb: Expected O, but got I4
			//IL_16d9: Expected O, but got Ref
			//IL_16e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_16ee: Expected O, but got Unknown
			//IL_16fc: Expected O, but got Ref
			//IL_2702: Unknown result type (might be due to invalid IL or missing references)
			//IL_2707: Expected O, but got Unknown
			//IL_2710: Unknown result type (might be due to invalid IL or missing references)
			//IL_2715: Expected O, but got Unknown
			//IL_278e: Expected O, but got I4
			//IL_1bea: Expected I, but got O
			//IL_1bfa: Expected O, but got I
			//IL_2404: Expected O, but got F4
			//IL_2438: Expected O, but got Ref
			//IL_2442: Expected I, but got O
			//IL_211b: Expected O, but got I4
			//IL_1751: Expected O, but got I
			//IL_1766: Expected O, but got I
			//IL_1776: Expected F4, but got I
			//IL_179b: Expected O, but got I
			//IL_17da: Expected O, but got I
			//IL_1c73: Expected I, but got O
			//IL_1c83: Expected O, but got I
			//IL_1ca9: Expected F4, but got O
			//IL_1837: Expected O, but got I
			//IL_1d06: Expected O, but got I
			//IL_1d20: Expected I, but got O
			//IL_24e7: Expected O, but got I
			//IL_080d: Expected O, but got I4
			//IL_27bf: Expected F4, but got O
			//IL_08e0: Expected O, but got I4
			//IL_24cc: Expected F4, but got O
			//IL_2172: Unknown result type (might be due to invalid IL or missing references)
			//IL_2177: Expected F4, but got Unknown
			//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a61: Expected O, but got Unknown
			//IL_0a6e: Expected I, but got O
			//IL_0a8d: Expected F4, but got O
			//IL_2988: Expected F4, but got O
			//IL_25a3: Expected O, but got I
			//IL_0b5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b60: Expected I4, but got Unknown
			//IL_0bb0: Expected F4, but got O
			//IL_0832: Expected F4, but got O
			//IL_188f: Expected I, but got O
			//IL_0fbb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fc0: Expected O, but got Unknown
			//IL_0fed: Expected I, but got O
			//IL_0ff5: Expected F4, but got O
			//IL_0ac6: Expected F4, but got O
			//IL_0627: Expected O, but got I4
			//IL_0673: Expected O, but got F4
			//IL_0d40: Expected O, but got I
			//IL_0d66: Expected O, but got I
			//IL_0d6e: Expected F4, but got O
			//IL_0bfb: Expected F4, but got O
			//IL_0970: Expected O, but got I4
			//IL_085b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0860: Expected O, but got Unknown
			//IL_0869: Unknown result type (might be due to invalid IL or missing references)
			//IL_086e: Expected O, but got Unknown
			//IL_18c8: Expected I, but got O
			//IL_0aee: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af3: Expected O, but got Unknown
			//IL_0932: Expected O, but got Ref
			//IL_094c: Expected O, but got I
			//IL_068f: Expected O, but got Ref
			//IL_06b1: Expected O, but got Ref
			//IL_1f11: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f16: Expected Ref, but got Unknown
			//IL_1f25: Expected O, but got F4
			//IL_0d94: Expected O, but got I
			//IL_0dba: Expected O, but got I
			//IL_0dc2: Expected F4, but got O
			//IL_0c45: Expected F4, but got O
			//IL_253c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2541: Expected O, but got Unknown
			//IL_254e: Expected O, but got I4
			//IL_097d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0982: Expected O, but got Unknown
			//IL_0715: Expected O, but got F4
			//IL_1591: Expected I, but got O
			//IL_1599: Expected F4, but got O
			//IL_0df6: Expected O, but got I
			//IL_0dfe: Expected F4, but got O
			//IL_0ce2: Expected O, but got I4
			//IL_09ba: Expected F4, but got O
			//IL_0e1c: Expected O, but got I
			//IL_0e3c: Expected O, but got I4
			//IL_0e41: Expected I, but got O
			//IL_1090: Expected I, but got O
			//IL_1098: Expected F4, but got O
			//IL_25c8: Expected F4, but got O
			//IL_09d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_09dd: Expected O, but got Unknown
			//IL_09f8: Expected F4, but got O
			//IL_0e79: Expected I, but got O
			//IL_0f89: Expected O, but got Ref
			//IL_0fa8: Expected O, but got I
			//IL_29ba: Expected O, but got I
			//IL_29d9: Expected F4, but got O
			//IL_10e4: Expected I, but got O
			//IL_1100: Expected F4, but got O
			//IL_0eac: Expected F4, but got O
			//IL_113c: Expected F4, but got O
			//IL_25e6: Expected O, but got I
			//IL_1165: Expected O, but got I4
			//IL_116f: Expected I, but got O
			//IL_1198: Expected F4, but got O
			//IL_0f02: Expected F4, but got O
			//IL_0f6f: Expected O, but got I
			//IL_11bc: Expected O, but got I
			//IL_0f1d: Expected O, but got Ref
			//IL_0f35: Expected F4, but got O
			//IL_0f45: Expected O, but got I
			//IL_0f5d: Expected O, but got I
			//IL_264e: Expected I, but got O
			//IL_29f1: Expected O, but got I4
			//IL_29fb: Expected O, but got I4
			//IL_2a05: Expected I, but got O
			//IL_1476: Expected F4, but got I4
			//IL_124f: Expected F4, but got O
			//IL_1491: Unknown result type (might be due to invalid IL or missing references)
			//IL_1496: Expected F4, but got Unknown
			//IL_2a6d: Expected I4, but got O
			//IL_14a9: Expected O, but got Ref
			//IL_14c8: Expected O, but got Ref
			//IL_14ec: Expected O, but got Ref
			//IL_12b7: Expected O, but got I
			//IL_26bf: Expected F4, but got O
			//IL_1306: Expected O, but got I
			//IL_1360: Expected F4, but got O
			//IL_1377: Unknown result type (might be due to invalid IL or missing references)
			//IL_137c: Expected O, but got Unknown
			//IL_13b8: Expected F4, but got O
			//IL_13e3: Expected O, but got I
			//IL_13ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_13f1: Expected O, but got Unknown
			//IL_1439: Expected O, but got I
			object obj2 = default(object);
			object obj = (object)(&obj2);
			TMP_Text tMP_Text = (TMP_Text)_003C_003E1__state;
			Teleprinter teleprinter = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			TMP_Text tMP_Text2;
			bool flag2;
			IntPtr intPtr = default(IntPtr);
			nint num3;
			string text;
			Vector3 vector;
			UnityEngine.Object obj3;
			string text3;
			UnityEngine.Object typerCursor = default(UnityEngine.Object);
			string text2;
			int currentRevealedCharIndex;
			int num6;
			nint num2;
			if (!flag)
			{
				tMP_Text = (TMP_Text)(tMP_Text - 1);
				if (!flag)
				{
					tMP_Text = (TMP_Text)(tMP_Text - 1);
					if (!flag)
					{
						tMP_Text = (TMP_Text)(tMP_Text - 1);
						if (!flag)
						{
							if ((nint)tMP_Text != 1)
							{
								goto IL_2303;
							}
							int num = _003CtoLine_003E5__7 - 1;
							_003CtoLine_003E5__7 = num;
							_003C_003E1__state = -1;
							tMP_Text2 = null;
							flag2 = true;
							num2 = intPtr;
							goto IL_2311;
						}
						_003C_003E1__state = -1;
						num3 = unchecked((nint)null);
						flag2 = true;
						text = (string)(object)typerCursor;
						num2 = intPtr;
						goto IL_1a06;
					}
					_003C_003E1__state = -1;
					bool flag3 = (object)_003C_003E4__this == null;
					num2 = intPtr;
					if (!flag3)
					{
						vector = _003CtargetPos_003E5__12;
						teleprinter._lastCursorWorldPos = _003CtargetPos_003E5__12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+1EC]");
						_ = 0;
						_003CtargetPos_003E5__12 = (Vector3)0;
						_ = 0;
						num3 = unchecked((nint)null);
						flag2 = true;
						num2 = intPtr;
						obj3 = tMP_Text;
						goto IL_18e3;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					bool flag4 = (object)_003C_003E4__this == null;
					num2 = intPtr;
					if (!flag4)
					{
						bool flag5 = teleprinter.onLineTransition == null;
						tMP_Text2 = null;
						flag2 = true;
						num2 = intPtr;
						tMP_Text = (TMP_Text)(object)teleprinter.onLineTransition;
						if (!flag5)
						{
							int num4 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 432));
							int num5 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 448));
							_ = _003CtoLine_003E5__7;
							_ = _003CfromLine_003E5__6;
							teleprinter.onLineTransition.Invoke(num5, num4);
							tMP_Text2 = null;
							flag2 = true;
							num2 = num5;
							num6 = 0;
							text2 = (string)num4;
							tMP_Text = (TMP_Text)(object)teleprinter.onLineTransition;
						}
						goto IL_2330;
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				bool flag6 = (object)_003C_003E4__this == null;
				num2 = intPtr;
				if (!flag6)
				{
					currentRevealedCharIndex = teleprinter._currentRevealedCharIndex;
					if (!string.IsNullOrEmpty(teleprinter._currentFullRich))
					{
						bool flag7 = string.IsNullOrEmpty(chunkRich);
						bool flag8 = !flag7;
						text3 = "\n";
						if (flag8)
						{
							goto IL_2359;
						}
					}
					text3 = "";
					goto IL_2359;
				}
			}
			goto IL_22c4;
			IL_0f62:
			string text4 = null;
			nint num7;
			typerCursor = (UnityEngine.Object)num7;
			num2 = num7;
			goto IL_25b1;
			IL_25b1:
			List<LineRange> list;
			bool flag9 = list == null;
			TMP_Text tMP_Text3;
			float num8 = (float)tMP_Text3;
			float num9 = default(float);
			TMP_Text tMP_Text4 = default(TMP_Text);
			if (!flag9)
			{
				list.Add((LineRange)(&num9));
				IntPtr intPtr2 = num2;
				tMP_Text2 = (TMP_Text)(object)text4;
				tMP_Text4 = tMP_Text;
				text2 = (string)0;
				tMP_Text3 = tMP_Text;
				goto IL_0fb5;
			}
			throw new NullReferenceException();
			IL_22c4:
			throw new NullReferenceException();
			IL_2303:
			return false;
			IL_2359:
			text2 = teleprinter._currentFullRich;
			string text5 = (_003CnewFull_003E5__2 = chunkRich + text3 + teleprinter._currentFullRich);
			tMP_Text = teleprinter._tmp;
			bool flag10 = (object)teleprinter._tmp == null;
			num2 = (nint)text5;
			num6 = 0;
			if (!flag10)
			{
				nint num10 = (nint)tMP_Text;
				num2 = (nint)_003CnewFull_003E5__2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1584 @ r8_v39 (Il2CppClass<TMPro.TMP_Text>)+560]");
				text2 = (string)0;
				teleprinter._tmp.text = _003CnewFull_003E5__2;
				tMP_Text = teleprinter._tmp;
				bool flag11 = (object)teleprinter._tmp == null;
				num6 = 0;
				if (!flag11)
				{
					nint num11 = (nint)tMP_Text;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1755 @ r9_v33 (Il2CppClass<TMPro.TMP_Text>)+7E0]");
					num6 = 0;
					teleprinter._tmp.ForceMeshUpdate();
					tMP_Text = teleprinter._tmp;
					bool flag12 = (object)teleprinter._tmp == null;
					num2 = unchecked((nint)null);
					text2 = null;
					if (!flag12)
					{
						TMP_TextInfo textInfo = teleprinter._tmp.textInfo;
						tMP_Text = (TMP_Text)(this + 56);
						_003Cti_003E5__3 = textInfo;
						num2 = (nint)_003Cti_003E5__3;
						bool flag13 = _003Cti_003E5__3 == null;
						text2 = null;
						if (!flag13)
						{
							tMP_Text = teleprinter._tmp;
							bool flag14 = (object)teleprinter._tmp == null;
							text2 = null;
							if (!flag14)
							{
								TMP_Text tmp = teleprinter._tmp;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+18]");
								tmp.maxVisibleCharacters = 0;
								bool flag15 = teleprinter._baselineSet;
								text2 = null;
								tMP_Text3 = (TMP_Text)num8;
								if (flag15)
								{
									goto IL_0722;
								}
								TMP_TextInfo tMP_TextInfo = _003Cti_003E5__3;
								bool flag16 = _003Cti_003E5__3 == null;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+18]");
								num2 = 0;
								text2 = null;
								if (!flag16)
								{
									bool flag17 = tMP_TextInfo.characterCount <= 0;
									text2 = null;
									tMP_Text3 = (TMP_Text)num8;
									if (flag17)
									{
										goto IL_0722;
									}
									int num12 = tMP_TextInfo.characterCount - 1;
									bool flag18 = currentRevealedCharIndex < num12;
									int num13 = currentRevealedCharIndex;
									if (!flag18)
									{
										num13 = num12;
									}
									Vector3 charWorldPositionFromInfoOrApprox = _003C_003E4__this.GetCharWorldPositionFromInfoOrApprox(_003Cti_003E5__3, num13);
									float x = charWorldPositionFromInfoOrApprox.x;
									teleprinter._lastCursorWorldPos = (Vector3)charWorldPositionFromInfoOrApprox.x;
									_ = charWorldPositionFromInfoOrApprox.z;
									float num14 = default(float);
									teleprinter._baselineWorldY = num14;
									teleprinter._baselineSet = true;
									_003C_003E4__this.SetCursorPosition((Vector3)(&tMP_Text4));
									num2 = (nint)_003Cti_003E5__3;
									bool flag19 = _003Cti_003E5__3 == null;
									num6 = num13;
									text2 = null;
									num8 = num14;
									tMP_Text = (TMP_Text)(object)_003C_003E4__this;
									if (!flag19)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+38]");
										num2 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+38]");
										bool flag20 = (nint)0 == 0;
										num6 = num13;
										text2 = null;
										num8 = num14;
										tMP_Text = (TMP_Text)(object)_003C_003E4__this;
										if (!flag20)
										{
											int num15 = num13;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+18]");
											bool flag21 = (nint)num15 >= (nint)0;
											num6 = num13;
											text2 = null;
											num8 = num14;
											tMP_Text = (TMP_Text)(object)_003C_003E4__this;
											if (!flag21)
											{
												tMP_Text = (TMP_Text)(num13 * 376);
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rcx_v132 (TMPro.TMP_Text)+5C+v279 @ rdx_v131 (Il2CppMethodInfo)]");
												teleprinter._prevLineNum = 0;
												bool flag22 = !teleprinter.debugCursor;
												num9 = charWorldPositionFromInfoOrApprox.x;
												num6 = num13;
												text2 = null;
												tMP_Text3 = (TMP_Text)num14;
												if (!flag22)
												{
													object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 432));
													_ = teleprinter._baselineWorldY;
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
													object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 448));
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
													object arg = default(object);
													object obj6 = default(object);
													string text6 = $"[Teleprinter BU] Baseline set Y={arg} at char {obj6}.";
													Debug.Log(text6);
													num9 = charWorldPositionFromInfoOrApprox.x;
													num6 = 0;
													text2 = (string)obj6;
													tMP_Text3 = (TMP_Text)teleprinter._baselineWorldY;
													tMP_Text = (TMP_Text)(object)text6;
												}
												goto IL_0722;
											}
											goto IL_2480;
										}
									}
								}
							}
						}
					}
				}
			}
			goto IL_22c4;
			IL_18e3:
			bool flag23 = _003CisLineBreakChar_003E5__11;
			tMP_Text = (TMP_Text)obj3;
			if (!flag23)
			{
				_003C_003E4__this.SetTypingAnimator(state: true);
				num2 = 1;
				text2 = null;
				tMP_Text = (TMP_Text)(object)_003C_003E4__this;
			}
			int num16 = _003CfromLine_003E5__6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+5C]");
			bool flag24 = (nint)num16 == 0;
			text = (string)(object)typerCursor;
			num8 = (float)vector;
			if (!flag24)
			{
				bool flag25 = _003CisLineBreakChar_003E5__11;
				text = (string)(object)typerCursor;
				num8 = (float)vector;
				if (!flag25)
				{
					bool flag26 = teleprinter._waitPerLetter == null;
					text = (string)(object)typerCursor;
					num8 = (float)vector;
					if (!flag26)
					{
						bool flag27 = !teleprinter.skipAnimation;
						text = (string)(object)typerCursor;
						num8 = (float)vector;
						if (flag27)
						{
							_003C_003E2__current = teleprinter._waitPerLetter;
							_003C_003E1__state = 3;
							return true;
						}
					}
				}
			}
			goto IL_1a06;
			IL_0d0b:
			teleprinter._applyMaskThisFrame = true;
			_003C_003E4__this.ApplyAlphaMaskToText();
			_ = _003Cti_003E5__3;
			list = new List<LineRange>(8);
			int num17;
			bool flag28 = num17 <= 0;
			tMP_Text2 = null;
			bool[] array;
			typerCursor = (UnityEngine.Object)(object)array;
			tMP_Text4 = null;
			text2 = (string)0;
			if (flag28)
			{
				goto IL_0fb5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C8]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C8]");
			bool flag29 = (nint)0 == 0;
			num2 = 8;
			text2 = (string)0;
			num8 = (float)tMP_Text3;
			tMP_Text = (TMP_Text)(object)list;
			if (!flag29)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2966 @ rax_v189+38]");
				tMP_Text = (TMP_Text)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2966 @ rax_v189+38]");
				bool flag30 = (nint)0 == 0;
				num2 = 8;
				text2 = (string)0;
				num8 = (float)tMP_Text3;
				if (!flag30)
				{
					bool flag31 = (nint)((MonoBehaviour)tMP_Text).m_CancellationTokenSource <= 0;
					num2 = 8;
					text2 = (string)0;
					num8 = (float)tMP_Text3;
					if (flag31)
					{
						goto IL_2480;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rcx_v132 (TMPro.TMP_Text)+5C]");
					tMP_Text = (TMP_Text)0;
					bool flag32 = num17 <= 1;
					text4 = null;
					typerCursor = (UnityEngine.Object)1;
					num2 = unchecked((nint)null);
					text2 = null;
					if (flag32)
					{
						goto IL_25b1;
					}
					nint num18 = 1;
					num7 = 1;
					tMP_Text4 = null;
					currentRevealedCharIndex = 376;
					num2 = unchecked((nint)null);
					num6 = num17;
					text2 = null;
					while (true)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2966 @ rax_v189+38]");
						object obj8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2966 @ rax_v189+38]");
						bool flag33 = (nint)0 == 0;
						num8 = (float)tMP_Text3;
						if (flag33)
						{
							break;
						}
						nint num19 = num7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ rax_v190+18]");
						bool flag34 = num19 >= 0;
						num8 = (float)tMP_Text3;
						if (!flag34)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ rax_v190+5C+v265 @ r15_v2 (System.Int32)]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ rax_v190+5C+v265 @ r15_v2 (System.Int32)]");
							if (0 != (nint)tMP_Text)
							{
								bool flag35 = list == null;
								num8 = (float)tMP_Text3;
								if (flag35)
								{
									throw new NullReferenceException();
								}
								list.Add((LineRange)(&num9));
								IntPtr intPtr2 = num2;
								tMP_Text4 = tMP_Text;
								num9 = (float)tMP_Text;
								num6 = num17;
								text2 = (string)num7;
								tMP_Text3 = tMP_Text;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1B0]");
								tMP_Text = (TMP_Text)0;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C8]");
							obj7 = 0;
							num7++;
							num18++;
							currentRevealedCharIndex += 376;
							bool flag36 = num18 < num6;
							num2 = num7;
							if (flag36)
							{
								continue;
							}
							goto IL_0f62;
						}
						goto IL_2480;
					}
				}
			}
			goto IL_22c4;
			IL_26cd:
			float num21;
			if (teleprinter.invertPaperDirection)
			{
				float num20 = num21;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				num21 = num20 ^ 0;
			}
			bool flag37 = !teleprinter.debugCursor;
			int num22 = 1;
			if (!flag37)
			{
				object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 432));
				_ = _003CfromLine_003E5__6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 448));
				_ = _003CtoLine_003E5__7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 456));
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				object arg3 = default(object);
				object arg4 = default(object);
				string message = $"[Teleprinter BU] Cross-job feed: from line {arg2} to {arg3} (Δ={arg4})";
				Debug.Log(message);
				num22 = 1;
			}
			_003CMovePaper_003Ed__97 obj12 = new _003CMovePaper_003Ed__97(0);
			obj12._003C_003E1__state = (int)tMP_Text2;
			obj12._003C_003E4__this = _003C_003E4__this;
			obj12.lineDeltaLocal = num21;
			int num23;
			obj12.lineCount = num23;
			obj12.compensateCursorWorldPosition = true;
			_003C_003E2__current = obj12;
			_003C_003E1__state = num22;
			return true;
			IL_0fb5:
			tMP_Text = (TMP_Text)(this + 72);
			_003Clines_003E5__5 = list;
			List<LineRange> list2 = _003Clines_003E5__5;
			bool flag38 = _003Clines_003E5__5 == null;
			num2 = (nint)list;
			num8 = (float)tMP_Text3;
			if (flag38)
			{
				goto IL_22c4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ rax_v148 (System.Collections.Generic.List`1<Teleprinter+LineRange>)+18]");
			nint num32;
			if ((nint)0 != 0)
			{
				if (!string.IsNullOrEmpty(teleprinter._currentFullRich))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C0]");
					if ((nint)0 > (nint)0)
					{
						TMP_TextInfo tMP_TextInfo2 = _003Cti_003E5__3;
						bool flag39 = _003Cti_003E5__3 == null;
						num2 = unchecked((nint)null);
						num8 = (float)tMP_Text3;
						tMP_Text = (TMP_Text)(object)teleprinter._currentFullRich;
						if (!flag39)
						{
							if (num17 >= tMP_TextInfo2.characterCount)
							{
								goto IL_1582;
							}
							num2 = (nint)tMP_TextInfo2.characterInfo;
							bool flag40 = tMP_TextInfo2.characterInfo == null;
							num8 = (float)tMP_Text3;
							tMP_Text = (TMP_Text)(object)teleprinter._currentFullRich;
							if (!flag40)
							{
								int num24 = num17;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+18]");
								bool flag41 = (nint)num24 >= (nint)0;
								num8 = (float)tMP_Text3;
								tMP_Text = (TMP_Text)(object)teleprinter._currentFullRich;
								if (flag41)
								{
									goto IL_2480;
								}
								tMP_Text = (TMP_Text)(num17 * 376);
								num2 = (nint)_003Clines_003E5__5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rcx_v132 (TMPro.TMP_Text)+5C+v279 @ rdx_v131 (Il2CppMethodInfo)]");
								_003CfromLine_003E5__6 = 0;
								bool flag42 = num2 == 0;
								num8 = (float)tMP_Text3;
								if (!flag42)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+18]");
									object obj13 = -1;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									int num25 = default(int);
									_003CtoLine_003E5__7 = num25;
									int num26 = num25 - _003CfromLine_003E5__6;
									nint num27 = (nint)typeof(Math);
									num23 = -num26;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3054 @ rcx_v115 (Il2CppClass<System.Math>)+E4]");
									if ((nint)0 < (nint)0)
									{
										num23 = num26;
									}
									if (num23 == 0)
									{
										num23 = 1;
									}
									text2 = (string)_003CtoLine_003E5__7;
									tMP_Text = (TMP_Text)_003CfromLine_003E5__6;
									num2 = (nint)_003Cti_003E5__3;
									if (_003CfromLine_003E5__6 == _003CtoLine_003E5__7)
									{
										goto IL_146d;
									}
									bool flag43 = _003Cti_003E5__3 == null;
									num6 = 0;
									num8 = (float)tMP_Text3;
									if (!flag43)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+2C]");
										if ((nint)0 == 0)
										{
											goto IL_146d;
										}
										if (_003CfromLine_003E5__6 >= 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+2C]");
											TMP_Text tMP_Text5 = (TMP_Text)(-1);
											if (System.Runtime.CompilerServices.Unsafe.As<TMP_Text, UIntPtr>(ref tMP_Text) > System.Runtime.CompilerServices.Unsafe.As<TMP_Text, UIntPtr>(ref tMP_Text5))
											{
												tMP_Text = tMP_Text5;
											}
										}
										else
										{
											tMP_Text = tMP_Text2;
										}
										if ((nint)text2 >= 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+2C]");
											string text7 = (string)(-1);
											if (System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text2) > System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text7))
											{
												text2 = text7;
											}
										}
										else
										{
											text2 = (string)(object)tMP_Text2;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+50]");
										num6 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+50]");
										bool flag44 = (nint)0 == 0;
										num8 = (float)tMP_Text3;
										if (!flag44)
										{
											TMP_Text tMP_Text6 = tMP_Text;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2928 @ r9_v47 (System.Int32)+18]");
											bool flag45 = (nint)tMP_Text6 >= 0;
											num8 = (float)tMP_Text3;
											if (!flag45)
											{
												object obj14 = tMP_Text * 2;
												object obj15 = (object)tMP_Text + obj14;
												object obj16 = obj15 << 5;
												string text8 = text2;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2928 @ r9_v47 (System.Int32)+18]");
												bool flag46 = (nint)text8 >= 0;
												num8 = (float)tMP_Text3;
												if (!flag46)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1593 @ r10_v21+58+v2928 @ r9_v47 (System.Int32)]");
													nint num28 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1593 @ r10_v21+50+v2928 @ r9_v47 (System.Int32)]");
													object obj17 = num28 + 0;
													object obj18 = text2 * 2;
													object obj19 = text2 + obj18;
													object obj20 = obj19 << 5;
													float num29 = (float)obj17 * 0.5f;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3124 @ rcx_v134+58+v2928 @ r9_v47 (System.Int32)]");
													nint num30 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3124 @ rcx_v134+50+v2928 @ r9_v47 (System.Int32)]");
													object obj21 = num30 + 0;
													float num31 = (float)obj21 * 0.5f;
													num21 = num31 - num29;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+50]");
													num32 = 0;
													goto IL_26cd;
												}
											}
											goto IL_2480;
										}
									}
								}
							}
						}
						goto IL_22c4;
					}
				}
				goto IL_1582;
			}
			teleprinter._currentFullRich = _003CnewFull_003E5__2;
			teleprinter._currentRevealedCharIndex = _003CnewCount_003E5__4;
			teleprinter._applyMaskThisFrame = false;
			goto IL_2303;
			IL_0892:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C0]");
			nint num33 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C0]");
			TMP_Text tMP_Text7;
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C0]");
				_ = 0;
				object obj22 = 0;
				currentRevealedCharIndex = num17;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C0]");
				num2 = 0;
				while (true)
				{
					List<bool> revealMask = teleprinter._revealMask;
					bool flag47 = teleprinter._revealMask == null;
					num8 = (float)tMP_Text3;
					tMP_Text = tMP_Text7;
					if (flag47)
					{
						break;
					}
					object obj23 = obj22;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v368 @ rax_v201 (System.Collections.Generic.List`1<System.Boolean>)+18]");
					if ((nint)obj23 < 0)
					{
						text2 = (string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 432));
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1B0]");
						tMP_Text7 = (TMP_Text)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C8]");
						num2 = 0;
						num6 = 0;
					}
					else
					{
						tMP_Text7 = (TMP_Text)1;
					}
					if (currentRevealedCharIndex >= 0)
					{
						object obj24 = obj22 + num17;
						if ((nint)obj24 < _003CnewCount_003E5__4)
						{
							bool flag48 = array == null;
							num8 = (float)tMP_Text3;
							tMP_Text = tMP_Text7;
							if (flag48)
							{
								break;
							}
							object obj25 = obj22 + num17;
							bool flag49 = (nint)obj25 >= array.Length;
							num8 = (float)tMP_Text3;
							tMP_Text = tMP_Text7;
							if (flag49)
							{
								goto IL_2480;
							}
							bool flag50 = (object)tMP_Text7 == null;
							bool flag51 = !flag50;
							array[currentRevealedCharIndex] = flag51;
						}
					}
					currentRevealedCharIndex++;
					obj22++;
					object obj26 = currentRevealedCharIndex - num17;
					if ((nint)obj26 < num2)
					{
						continue;
					}
					goto IL_0a3e;
				}
				goto IL_22c4;
			}
			goto IL_24da;
			IL_2311:
			int num34;
			LineRange lineRange2;
			if (_003CtoLine_003E5__7 >= 0)
			{
				tMP_Text = (TMP_Text)(object)_003Clines_003E5__5;
				if (_003Clines_003E5__5 != null)
				{
					num2 = _003CtoLine_003E5__7;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					LineRange lineRange = default(LineRange);
					_003Clr_003E5__8 = lineRange;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+5C]");
					_003CfromLine_003E5__6 = 0;
					num34 = (int)tMP_Text2;
					num9 = (float)lineRange;
					num6 = 0;
					text2 = (string)(&lineRange);
					lineRange2 = lineRange;
					goto IL_286e;
				}
			}
			else
			{
				num2 = (nint)_003CnewFull_003E5__2;
				if ((object)_003C_003E4__this != null)
				{
					teleprinter._currentFullRich = _003CnewFull_003E5__2;
					teleprinter._currentRevealedCharIndex = _003CnewCount_003E5__4;
					tMP_Text = (TMP_Text)(object)_003Clines_003E5__5;
					if (_003Clines_003E5__5 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						tMP_Text = teleprinter._tmp;
						int prevLineNum = default(int);
						teleprinter._prevLineNum = prevLineNum;
						teleprinter._applyMaskThisFrame = false;
						bool flag52 = (object)teleprinter._tmp == null;
						num2 = unchecked((nint)null);
						num6 = 0;
						text2 = (string)(&prevLineNum);
						if (!flag52)
						{
							teleprinter._tmp.ForceMeshUpdate();
							goto IL_2303;
						}
					}
				}
			}
			goto IL_22c4;
			IL_1582:
			flag2 = true;
			num2 = unchecked((nint)null);
			num8 = (float)tMP_Text3;
			tMP_Text = (TMP_Text)(object)teleprinter._currentFullRich;
			goto IL_2330;
			IL_0a3e:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1C0]");
			num33 = 0;
			goto IL_24da;
			IL_146d:
			num21 = 0f;
			num32 = 0;
			goto IL_26cd;
			IL_24da:
			TMP_Text tMP_Text8 = (TMP_Text)(num33 + num17);
			if ((nint)tMP_Text8 >= _003CnewCount_003E5__4)
			{
				goto IL_0b1f;
			}
			object obj27 = tMP_Text8 + 32;
			nint num35 = (nint)(obj27 + (object)array);
			bool flag53 = array == null;
			num2 = num35;
			num8 = (float)tMP_Text3;
			tMP_Text = tMP_Text8;
			if (flag53)
			{
				goto IL_22c4;
			}
			while (true)
			{
				bool flag54 = (nint)tMP_Text8 >= array.Length;
				num2 = num35;
				num8 = (float)tMP_Text3;
				tMP_Text = tMP_Text8;
				if (flag54)
				{
					break;
				}
				num35 = 1;
				tMP_Text8 = (TMP_Text)(tMP_Text8 + 1);
				num35++;
				if ((nint)tMP_Text8 < _003CnewCount_003E5__4)
				{
					continue;
				}
				goto IL_0b1f;
			}
			goto IL_2480;
			IL_1a06:
			if ((object)_003C_003E4__this != null)
			{
				bool flag55 = !teleprinter.debugChars;
				Vector3 vector2 = (Vector3)tMP_Text4;
				lineRange2 = (LineRange)num8;
				if (!flag55)
				{
					object[] array2 = new object[4];
					object obj28 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 448));
					_ = _003Cc_003E5__10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if (array2 == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					TMP_Text tMP_Text9 = default(TMP_Text);
					if ((object)tMP_Text9 != null)
					{
						nint num36 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1916 @ rdx_v72 (Il2CppClass<System.Object[]>)+40]");
						num2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj29 = default(object);
						bool flag56 = obj29 == null;
						tMP_Text = tMP_Text9;
						if (flag56)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj30 = default(object);
							throw obj30;
						}
					}
					array2[0] = tMP_Text9;
					object obj31 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 456));
					_ = _003CfromLine_003E5__6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj32 = default(object);
					if (obj32 != null)
					{
						nint num37 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2154 @ rdx_v70 (Il2CppClass<System.Object[]>)+40]");
						object obj33 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj34 = default(object);
						bool flag57 = obj34 == null;
						object obj35 = obj32;
						if (flag57)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj36 = default(object);
							throw obj36;
						}
					}
					array2[1] = obj32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj37 = default(object);
					if (obj37 != null)
					{
						nint num38 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2407 @ rdx_v68 (Il2CppClass<System.Object[]>)+40]");
						object obj38 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj39 = default(object);
						bool flag58 = obj39 == null;
						object obj40 = obj37;
						if (flag58)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							List<LineRange> list3 = default(List<LineRange>);
							throw list3;
						}
					}
					array2[2] = obj37;
					List<LineRange> list4 = (List<LineRange>)(object)(Vector3)tMP_Text4;
					if (list4 != null)
					{
						nint num39 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2545 @ rdx_v66 (Il2CppClass<System.Object[]>)+40]");
						object obj41 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj42 = default(object);
						bool flag59 = obj42 == null;
						num8 = (float)teleprinter._lastCursorWorldPos;
						List<LineRange> list5 = list4;
						if (flag59)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj43 = default(object);
							throw obj43;
						}
					}
					array2[3] = list4;
					string text9 = string.Format("[Teleprinter BU] Char '{0}' idx={1} line={2} pos={3}", array2);
					Debug.Log(text9);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+A4]");
					object obj44 = 0;
					text = text9;
					vector2 = teleprinter._lastCursorWorldPos;
					num2 = unchecked((nint)null);
					text2 = null;
					lineRange2 = (LineRange)teleprinter._lastCursorWorldPos;
				}
				bool flag60 = teleprinter._revealMask == null;
				num8 = (float)lineRange2;
				tMP_Text = (TMP_Text)(object)teleprinter._revealMask;
				if (!flag60)
				{
					bool value = (byte)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 432)) != 0;
					_ = 1;
					teleprinter._revealMask.set_Item(_003CfromLine_003E5__6, value);
					_003C_003E4__this.ApplyAlphaMaskToText();
					if (teleprinter.onCharacterPrinted != null)
					{
						teleprinter.onCharacterPrinted.Invoke();
					}
					bool flag61 = teleprinter.onCharacterPrintedDetailed == null;
					num6 = 0;
					if (!flag61)
					{
						object obj45 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 432));
						_ = _003Cc_003E5__10;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180941A60");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+A4]");
						object obj46 = 0;
						num6 = (int)(&obj46);
					}
					tMP_Text = (TMP_Text)(this + 104);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
					int num40 = _003CfromLine_003E5__6 + 1;
					_003CfromLine_003E5__6 = num40;
					num34 = (int)num3;
					typerCursor = (UnityEngine.Object)(object)text;
					tMP_Text4 = (TMP_Text)vector2;
					num2 = unchecked((nint)null);
					text2 = (string)376;
					goto IL_286e;
				}
			}
			goto IL_22c4;
			IL_286e:
			int num41 = _003CfromLine_003E5__6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+60]");
			if ((nint)num41 <= (nint)0)
			{
				TMP_TextInfo tMP_TextInfo3 = _003Cti_003E5__3;
				bool flag62 = _003Cti_003E5__3 == null;
				num8 = (float)lineRange2;
				if (!flag62)
				{
					tMP_Text = (TMP_Text)(object)tMP_TextInfo3.characterInfo;
					bool flag63 = tMP_TextInfo3.characterInfo == null;
					num8 = (float)lineRange2;
					if (!flag63)
					{
						text2 = (string)_003CfromLine_003E5__6;
						object obj47 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800039A0");
						_003CTypeChunkBottomUp_003Ed__95 obj48 = (_003CTypeChunkBottomUp_003Ed__95)(this + 104);
						object obj49 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
						num2 = 2;
						num2 = intPtr;
						object obj50 = default(object);
						obj49 = obj50;
						obj48 = this;
						object obj51;
						do
						{
							obj48 = (_003CTypeChunkBottomUp_003Ed__95)(obj48 + 128);
							obj49 += 128;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+10]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17-60]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17-50]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17-40]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17-30]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17-20]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17-10]");
							_ = 0;
							num2--;
							obj51 = !flag63;
						}
						while (obj51 != null);
						obj48 = (_003CTypeChunkBottomUp_003Ed__95)obj49;
						_003CTypeChunkBottomUp_003Ed__95 obj52 = obj48;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+10]");
						obj52._003C_003E1__state = 0;
						_003CTypeChunkBottomUp_003Ed__95 obj53 = obj48;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+20]");
						obj53._003C_003E4__this = (Teleprinter)0;
						_003CTypeChunkBottomUp_003Ed__95 obj54 = obj48;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+30]");
						obj54._003CnewFull_003E5__2 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+50]");
						float x = 0f;
						_003CTypeChunkBottomUp_003Ed__95 obj55 = obj48;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+40]");
						obj55._003CnewCount_003E5__4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+60]");
						vector = (Vector3)0;
						_003CTypeChunkBottomUp_003Ed__95 obj56 = obj48;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+50]");
						obj56._003CfromLine_003E5__6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+60]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2140 @ rax_v17+70]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+6C]");
						tMP_Text = (TMP_Text)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+6C]");
						_003Cc_003E5__10 = '\0';
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+6C]");
						bool flag64;
						if ((nint)0 == 10)
						{
							flag64 = flag2;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+6C]");
							object obj57 = -13;
							bool flag65 = obj57 == null;
							flag64 = flag65;
						}
						_003CisLineBreakChar_003E5__11 = flag64;
						bool flag66 = (object)_003C_003E4__this == null;
						num8 = (float)vector;
						if (!flag66)
						{
							typerCursor = teleprinter.typerCursor;
							bool flag67 = teleprinter.typerCursor;
							bool flag68 = !flag67;
							num3 = num34;
							num2 = unchecked((nint)null);
							obj3 = teleprinter.typerCursor;
							if (!flag68)
							{
								bool flag69 = !_003CisLineBreakChar_003E5__11;
								num3 = num34;
								num2 = unchecked((nint)null);
								obj3 = teleprinter.typerCursor;
								if (flag69)
								{
									Vector3 charWorldPositionFromInfoOrApprox2 = _003C_003E4__this.GetCharWorldPositionFromInfoOrApprox(_003Cti_003E5__3, _003CfromLine_003E5__6);
									ref Vector3 targetWorld = ref *(Vector3*)(this + 484);
									_003CtargetPos_003E5__12 = (Vector3)charWorldPositionFromInfoOrApprox2.x;
									_ = charWorldPositionFromInfoOrApprox2.z;
									_003C_003E4__this.ApplyCursorLock(ref targetWorld);
									_003CMoveCursor_003Ed__98 obj58 = new _003CMoveCursor_003Ed__98(0);
									obj58._003C_003E1__state = num34;
									obj58._003C_003E4__this = _003C_003E4__this;
									obj58.fromWorld = teleprinter._lastCursorWorldPos;
									obj58.toWorld = _003CtargetPos_003E5__12;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r14_v1 (Teleprinter)+12C]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkBottomUp>d__95)+1EC]");
									_ = 0;
									_003C_003E2__current = obj58;
									_003C_003E1__state = 2;
									return true;
								}
							}
							goto IL_18e3;
						}
					}
				}
			}
			else
			{
				if (_003CtoLine_003E5__7 < 1)
				{
					int num42 = _003CtoLine_003E5__7 - 1;
					_003CtoLine_003E5__7 = num42;
					tMP_Text2 = (TMP_Text)num34;
					num8 = (float)lineRange2;
					goto IL_2311;
				}
				tMP_Text = (TMP_Text)(object)_003Clines_003E5__5;
				bool flag70 = _003Clines_003E5__5 == null;
				num8 = (float)lineRange2;
				if (!flag70)
				{
					num2 = _003CtoLine_003E5__7;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					tMP_Text = (TMP_Text)(object)_003Clines_003E5__5;
					bool flag71 = _003Clines_003E5__5 == null;
					num6 = 0;
					int num43 = default(int);
					text2 = (string)(&num43);
					num8 = (float)lineRange2;
					if (!flag71)
					{
						num2 = _003CtoLine_003E5__7 - 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						bool flag72 = (object)_003C_003E4__this == null;
						num6 = 0;
						int num44 = default(int);
						text2 = (string)(&num44);
						num8 = (float)lineRange2;
						if (!flag72)
						{
							bool flag73 = teleprinter.onLineTransition == null;
							int arg5 = (int)(&num44);
							if (!flag73)
							{
								arg5 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 432));
								int arg6 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 448));
								teleprinter.onLineTransition.Invoke(arg6, arg5);
							}
							UnityEvent<int, int> unityEvent = (UnityEvent<int, int>)(num44 - num43);
							unityEvent.Invoke(0, arg5);
							int num45 = default(int);
							bool flag74 = num45 != 0;
							int lineCount = num45;
							if (!flag74)
							{
								lineCount = (flag2 ? 1 : 0);
							}
							float num46 = _003C_003E4__this.GetLineVerticalDeltaCached(_003Cti_003E5__3, num43, num44);
							if (teleprinter.invertPaperDirection)
							{
								float num47 = num46;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
								num46 = num47 ^ 0;
							}
							IEnumerator enumerator = _003C_003E4__this.MovePaper(num46, lineCount, compensateCursorWorldPosition: true);
							_003C_003E2__current = enumerator;
							_003C_003E1__state = 4;
							return true;
						}
					}
				}
			}
			goto IL_22c4;
			IL_0b1f:
			_003C_003E4__this.EnsureMaskCapacity(_003CnewCount_003E5__4);
			if (_003CnewCount_003E5__4 <= 0)
			{
				goto IL_0d0b;
			}
			int num48 = array + 32;
			bool flag75 = array == null;
			int num49 = 0;
			nint num50 = _003CnewCount_003E5__4;
			int num51 = num6;
			string text10 = null;
			TMP_Text tMP_Text10 = (TMP_Text)(object)_003C_003E4__this;
			num2 = _003CnewCount_003E5__4;
			text2 = null;
			num8 = (float)tMP_Text3;
			tMP_Text = (TMP_Text)(object)_003C_003E4__this;
			if (flag75)
			{
				goto IL_22c4;
			}
			while (true)
			{
				bool flag76 = num49 >= array.Length;
				num2 = num50;
				num6 = num51;
				text2 = text10;
				num8 = (float)tMP_Text3;
				tMP_Text = tMP_Text10;
				if (flag76)
				{
					break;
				}
				bool flag77 = teleprinter._revealMask == null;
				num2 = num50;
				num6 = num51;
				text2 = text10;
				num8 = (float)tMP_Text3;
				tMP_Text = (TMP_Text)(object)teleprinter._revealMask;
				if (!flag77)
				{
					bool flag78 = ((int*)num48)->m_value == 0;
					bool flag79 = (byte)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 432)) != 0;
					_ = !flag78;
					teleprinter._revealMask.set_Item(num49, flag79);
					num49++;
					num48++;
					bool flag80 = num49 < _003CnewCount_003E5__4;
					num50 = num49;
					num51 = 0;
					text10 = (string)flag79;
					tMP_Text10 = (TMP_Text)(object)teleprinter._revealMask;
					currentRevealedCharIndex = num48;
					num6 = 0;
					if (flag80)
					{
						continue;
					}
					goto IL_0d0b;
				}
				goto IL_22c4;
			}
			goto IL_2480;
			IL_2330:
			List<LineRange> list6 = _003Clines_003E5__5;
			if (_003Clines_003E5__5 == null)
			{
				goto IL_22c4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v388 @ rax_v58 (System.Collections.Generic.List`1<Teleprinter+LineRange>)+18]");
			int num52 = (int)(-1);
			_003CtoLine_003E5__7 = num52;
			goto IL_2311;
			IL_0722:
			num2 = (nint)_003Cti_003E5__3;
			bool flag81 = _003Cti_003E5__3 == null;
			num8 = (float)tMP_Text3;
			if (!flag81)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+18]");
				num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+18]");
				_003CnewCount_003E5__4 = 0;
				int num53 = currentRevealedCharIndex;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+18]");
				bool flag82 = (nint)num53 < (nint)0;
				int num54 = currentRevealedCharIndex;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v131 (Il2CppMethodInfo)+18]");
				bool flag83 = (nint)num54 > (nint)0;
				int num55 = 0;
				if (!flag83)
				{
					num55 = currentRevealedCharIndex;
				}
				int num56 = (int)(num2 - num55);
				num17 = 0;
				if (!flag82)
				{
					num17 = num56;
				}
				if (num17 == 0)
				{
					teleprinter._currentFullRich = _003CnewFull_003E5__2;
					teleprinter._currentRevealedCharIndex = _003CnewCount_003E5__4;
					goto IL_2303;
				}
				array = new bool[num2];
				bool flag84 = num17 <= 0;
				tMP_Text7 = null;
				if (flag84)
				{
					goto IL_0892;
				}
				object obj59 = 0;
				tMP_Text = null;
				while (true)
				{
					bool flag85 = array == null;
					num8 = (float)tMP_Text3;
					if (flag85)
					{
						break;
					}
					bool flag86 = (nint)tMP_Text >= array.Length;
					num8 = (float)tMP_Text3;
					if (!flag86)
					{
						array[obj59] = false;
						tMP_Text7 = (TMP_Text)(tMP_Text + 1);
						obj59++;
						bool flag87 = (nint)obj59 < num17;
						tMP_Text = tMP_Text7;
						if (flag87)
						{
							continue;
						}
						goto IL_0892;
					}
					goto IL_2480;
				}
			}
			goto IL_22c4;
			IL_2480:
			throw new IndexOutOfRangeException();
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

	private sealed class _003CTypeChunkTopDown_003Ed__94 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		public string chunkRich;

		private string _003CnewFull_003E5__2;

		private TMP_TextInfo _003Cti_003E5__3;

		private int _003CstartIndex_003E5__4;

		private int _003CendIndex_003E5__5;

		private int _003CprevLineNumLocal_003E5__6;

		private int _003Ci_003E5__7;

		private char _003Cc_003E5__8;

		private int _003CcharLineNum_003E5__9;

		private bool _003CisLineBreakChar_003E5__10;

		private Vector3 _003CtargetPos_003E5__11;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CTypeChunkTopDown_003Ed__94(int _003C_003E1__state)
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
			//IL_113f: Expected O, but got I4
			//IL_0122: Expected I4, but got I8
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_0106: Expected I4, but got I8
			//IL_010e: Expected O, but got F4
			//IL_019a: Expected I, but got O
			//IL_078c: Expected F4, but got O
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_0088: Expected I4, but got I8
			//IL_01b0: Expected I, but got O
			//IL_01ba: Expected I, but got O
			//IL_07c1: Expected I, but got O
			//IL_00d8: Expected O, but got I4
			//IL_0215: Expected I, but got O
			//IL_026c: Expected I, but got O
			//IL_0074: Expected I4, but got I8
			//IL_0882: Expected F4, but got O
			//IL_07f2: Expected I, but got O
			//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed5: Expected Ref, but got Unknown
			//IL_0ee4: Expected O, but got F4
			//IL_02d4: Expected I, but got O
			//IL_0a8c: Expected O, but got F4
			//IL_09d7: Expected F4, but got O
			//IL_08de: Expected F4, but got O
			//IL_0ab6: Expected O, but got Ref
			//IL_0a09: Expected F4, but got O
			//IL_08f9: Expected O, but got I4
			//IL_0915: Expected F4, but got O
			//IL_12a3: Expected I4, but got O
			//IL_0a3b: Expected F4, but got O
			//IL_0ea2: Expected F4, but got O
			//IL_0933: Expected O, but got I4
			//IL_0951: Expected F4, but got O
			//IL_0b6d: Expected O, but got Ref
			//IL_0632: Expected I4, but got O
			//IL_0b0e: Expected I, but got O
			//IL_0968: Unknown result type (might be due to invalid IL or missing references)
			//IL_096d: Expected O, but got Unknown
			//IL_097d: Expected O, but got I
			//IL_099d: Expected F4, but got O
			//IL_09ad: Expected O, but got I
			//IL_0680: Expected I4, but got O
			//IL_0ba8: Expected I, but got O
			//IL_0bb8: Expected O, but got I
			//IL_0c87: Expected O, but got F4
			//IL_0c8b: Expected I4, but got O
			//IL_0c2d: Expected I, but got O
			//IL_0c3d: Expected O, but got I
			//IL_0709: Expected I4, but got O
			//IL_0d13: Expected O, but got I4
			//IL_11cf: Expected O, but got F4
			//IL_1203: Expected O, but got Ref
			//IL_120d: Expected I, but got O
			//IL_0752: Unknown result type (might be due to invalid IL or missing references)
			//IL_0757: Expected O, but got Unknown
			//IL_0cb5: Expected I, but got O
			//IL_0cc5: Expected O, but got I
			//IL_0ceb: Expected F4, but got O
			//IL_1287: Expected O, but got F4
			//IL_0d5e: Expected I, but got O
			//IL_0d6b: Expected F4, but got O
			//IL_1046: Unknown result type (might be due to invalid IL or missing references)
			//IL_104b: Expected F4, but got Unknown
			//IL_04fe: Expected O, but got I4
			//IL_0574: Expected O, but got Ref
			//IL_0596: Expected O, but got Ref
			//IL_05f3: Expected I, but got O
			object obj2 = default(object);
			object obj = (object)(&obj2);
			TMP_Text tMP_Text = (TMP_Text)_003C_003E1__state;
			Teleprinter teleprinter = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			Vector3 vector;
			UnityEngine.Object obj3;
			object obj4;
			float num10 = default(float);
			float num = default(float);
			bool flag3;
			if (!flag)
			{
				tMP_Text = (TMP_Text)(tMP_Text - 1);
				if (flag)
				{
					_003CprevLineNumLocal_003E5__6 = _003CcharLineNum_003E5__9;
					_003C_003E1__state = -1;
					vector = (Vector3)num;
					goto IL_0773;
				}
				tMP_Text = (TMP_Text)(tMP_Text - 1);
				if (!flag)
				{
					if ((nint)tMP_Text == 1)
					{
						_003C_003E1__state = -1;
						goto IL_0a51;
					}
					goto IL_1168;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					vector = _003CtargetPos_003E5__11;
					teleprinter._lastCursorWorldPos = _003CtargetPos_003E5__11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkTopDown>d__94)+64]");
					_ = 0;
					_003CtargetPos_003E5__11 = (Vector3)0;
					_ = 0;
					obj3 = tMP_Text;
					goto IL_080d;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					string text = (_003CnewFull_003E5__2 = teleprinter._currentFullRich + chunkRich);
					tMP_Text = teleprinter._tmp;
					bool flag2 = (object)teleprinter._tmp == null;
					flag3 = false;
					nint num2 = (nint)text;
					if (!flag2)
					{
						nint num3 = (nint)tMP_Text;
						num2 = (nint)_003CnewFull_003E5__2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v821 @ r8_v33 (Il2CppClass<TMPro.TMP_Text>)+560]");
						flag3 = false;
						teleprinter._tmp.text = _003CnewFull_003E5__2;
						tMP_Text = teleprinter._tmp;
						if ((object)teleprinter._tmp != null)
						{
							nint num4 = (nint)tMP_Text;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v912 @ r9_v25 (Il2CppClass<TMPro.TMP_Text>)+7E0]");
							int num5 = 0;
							teleprinter._tmp.ForceMeshUpdate();
							tMP_Text = teleprinter._tmp;
							bool flag4 = (object)teleprinter._tmp == null;
							flag3 = false;
							num2 = unchecked((nint)null);
							if (!flag4)
							{
								TMP_TextInfo tMP_TextInfo = (_003Cti_003E5__3 = teleprinter._tmp.textInfo);
								TMP_TextInfo tMP_TextInfo2 = _003Cti_003E5__3;
								tMP_Text = teleprinter._tmp;
								bool flag5 = _003Cti_003E5__3 == null;
								flag3 = false;
								num2 = (nint)tMP_TextInfo;
								if (!flag5)
								{
									int num6 = teleprinter._currentRevealedCharIndex;
									if (teleprinter._currentRevealedCharIndex >= 0)
									{
										if (num6 > tMP_TextInfo2.characterCount)
										{
											num6 = tMP_TextInfo2.characterCount;
										}
									}
									else
									{
										num6 = 0;
									}
									bool flag6 = (object)tMP_Text == null;
									flag3 = false;
									num2 = num6;
									if (!flag6)
									{
										tMP_Text.maxVisibleCharacters = num6;
										bool flag7 = teleprinter._baselineSet;
										obj4 = null;
										num2 = num6;
										if (flag7)
										{
											goto IL_0600;
										}
										TMP_TextInfo tMP_TextInfo3 = _003Cti_003E5__3;
										bool flag8 = _003Cti_003E5__3 == null;
										flag3 = false;
										num2 = num6;
										if (!flag8)
										{
											bool flag9 = tMP_TextInfo3.characterCount <= 0;
											obj4 = null;
											num2 = num6;
											if (flag9)
											{
												goto IL_0600;
											}
											int num7 = teleprinter._currentRevealedCharIndex;
											int num8 = tMP_TextInfo3.characterCount - 1;
											if (teleprinter._currentRevealedCharIndex >= num8)
											{
												num7 = num8;
											}
											Vector3 charWorldPositionFromInfoOrApprox = _003C_003E4__this.GetCharWorldPositionFromInfoOrApprox(_003Cti_003E5__3, num7);
											teleprinter._lastCursorWorldPos = (Vector3)charWorldPositionFromInfoOrApprox.x;
											_ = charWorldPositionFromInfoOrApprox.z;
											float num9 = default(float);
											teleprinter._baselineWorldY = num9;
											teleprinter._baselineSet = true;
											_003C_003E4__this.SetCursorPosition((Vector3)(&num10));
											num2 = (nint)_003Cti_003E5__3;
											bool flag10 = _003Cti_003E5__3 == null;
											num5 = num7;
											flag3 = false;
											num = num9;
											tMP_Text = (TMP_Text)(object)_003C_003E4__this;
											if (!flag10)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rdx_v26 (Il2CppMethodInfo)+38]");
												num2 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rdx_v26 (Il2CppMethodInfo)+38]");
												bool flag11 = (nint)0 == 0;
												num5 = num7;
												flag3 = false;
												num = num9;
												tMP_Text = (TMP_Text)(object)_003C_003E4__this;
												if (!flag11)
												{
													int num11 = num7;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rdx_v26 (Il2CppMethodInfo)+18]");
													bool flag12 = (nint)num11 >= (nint)0;
													num5 = num7;
													flag3 = false;
													num = num9;
													tMP_Text = (TMP_Text)(object)_003C_003E4__this;
													if (!flag12)
													{
														tMP_Text = (TMP_Text)(num7 * 376);
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ rcx_v82 (TMPro.TMP_Text)+5C+v213 @ rdx_v26 (Il2CppMethodInfo)]");
														teleprinter._prevLineNum = 0;
														bool flag13 = !teleprinter.debugCursor;
														num10 = charWorldPositionFromInfoOrApprox.x;
														num5 = num7;
														obj4 = null;
														num = num9;
														if (!flag13)
														{
															num = teleprinter._baselineWorldY;
															object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 320));
															_ = teleprinter._baselineWorldY;
															Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
															object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 336));
															Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
															object arg = default(object);
															object obj7 = default(object);
															string text2 = $"[Teleprinter] Baseline set Y={arg} at char {obj7}.";
															Debug.Log(text2);
															num10 = charWorldPositionFromInfoOrApprox.x;
															num5 = 0;
															obj4 = obj7;
															num2 = unchecked((nint)null);
															tMP_Text = (TMP_Text)(object)text2;
														}
														goto IL_0600;
													}
													goto IL_124f;
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
			goto IL_1129;
			IL_0773:
			bool flag14 = (object)_003C_003E4__this == null;
			num = (float)vector;
			if (!flag14)
			{
				bool flag15 = teleprinter.typerCursor;
				bool flag16 = !flag15;
				nint num2 = unchecked((nint)null);
				obj3 = teleprinter.typerCursor;
				if (!flag16)
				{
					bool flag17 = !_003CisLineBreakChar_003E5__10;
					num2 = unchecked((nint)null);
					obj3 = teleprinter.typerCursor;
					if (flag17)
					{
						Vector3 charWorldPositionFromInfoOrApprox2 = _003C_003E4__this.GetCharWorldPositionFromInfoOrApprox(_003Cti_003E5__3, _003Ci_003E5__7);
						ref Vector3 targetWorld = ref *(Vector3*)(this + 92);
						_003CtargetPos_003E5__11 = (Vector3)charWorldPositionFromInfoOrApprox2.x;
						_ = charWorldPositionFromInfoOrApprox2.z;
						_003C_003E4__this.ApplyCursorLock(ref targetWorld);
						_003CMoveCursor_003Ed__98 obj8 = new _003CMoveCursor_003Ed__98(0);
						obj8._003C_003E1__state = 0;
						obj8._003C_003E4__this = _003C_003E4__this;
						obj8.fromWorld = teleprinter._lastCursorWorldPos;
						obj8.toWorld = _003CtargetPos_003E5__11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rsi_v1 (Teleprinter)+12C]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter+<TypeChunkTopDown>d__94)+64]");
						_ = 0;
						_003C_003E2__current = obj8;
						_003C_003E1__state = 2;
						goto IL_138a;
					}
				}
				goto IL_080d;
			}
			goto IL_1129;
			IL_080d:
			bool flag18 = _003CisLineBreakChar_003E5__10;
			tMP_Text = (TMP_Text)obj3;
			if (!flag18)
			{
				_003C_003E4__this.SetTypingAnimator(state: true);
				flag3 = false;
				nint num2 = 1;
				tMP_Text = (TMP_Text)(object)_003C_003E4__this;
			}
			bool flag19 = _003Ci_003E5__7 == _003CstartIndex_003E5__4;
			num = (float)vector;
			if (flag19)
			{
				goto IL_0a51;
			}
			bool flag20 = _003Ci_003E5__7 <= _003CstartIndex_003E5__4;
			TMP_Text tMP_Text2 = tMP_Text;
			if (flag20)
			{
				goto IL_09bb;
			}
			tMP_Text = (TMP_Text)(object)_003Cti_003E5__3;
			bool flag21 = _003Cti_003E5__3 == null;
			num = (float)vector;
			if (!flag21)
			{
				tMP_Text = (TMP_Text)((Graphic)tMP_Text).m_SkipLayoutUpdate;
				bool flag22 = !((Graphic)tMP_Text).m_SkipLayoutUpdate;
				num = (float)vector;
				if (!flag22)
				{
					object obj9 = _003Ci_003E5__7 - 1;
					CancellationTokenSource cancellationTokenSource = ((MonoBehaviour)tMP_Text).m_CancellationTokenSource;
					bool flag23 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) >= System.Runtime.CompilerServices.Unsafe.As<CancellationTokenSource, UIntPtr>(ref cancellationTokenSource);
					num = (float)vector;
					if (!flag23)
					{
						object obj10 = obj9 * 376;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v618 @ rax_v89+5C+v246 @ rcx_v82 (TMPro.TMP_Text)]");
						tMP_Text2 = (TMP_Text)0;
						int num12 = _003CcharLineNum_003E5__9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v618 @ rax_v89+5C+v246 @ rcx_v82 (TMPro.TMP_Text)]");
						bool flag24 = (nint)num12 > (nint)0;
						num = (float)vector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v618 @ rax_v89+5C+v246 @ rcx_v82 (TMPro.TMP_Text)]");
						tMP_Text = (TMP_Text)0;
						if (!flag24)
						{
							goto IL_09bb;
						}
						goto IL_0a51;
					}
					goto IL_124f;
				}
			}
			goto IL_1129;
			IL_1168:
			return false;
			IL_0600:
			_003CstartIndex_003E5__4 = teleprinter._currentRevealedCharIndex;
			TMP_TextInfo tMP_TextInfo4 = _003Cti_003E5__3;
			bool flag25 = _003Cti_003E5__3 == null;
			flag3 = (byte)(int)obj4 != 0;
			if (flag25)
			{
				goto IL_1129;
			}
			_003CendIndex_003E5__5 = tMP_TextInfo4.characterCount;
			_003CprevLineNumLocal_003E5__6 = teleprinter._prevLineNum;
			teleprinter._applyMaskThisFrame = false;
			_003Ci_003E5__7 = _003CstartIndex_003E5__4;
			flag3 = (byte)(int)obj4 != 0;
			goto IL_1336;
			IL_09bb:
			bool flag26 = _003CisLineBreakChar_003E5__10;
			num = (float)vector;
			tMP_Text = tMP_Text2;
			if (!flag26)
			{
				bool flag27 = teleprinter._waitPerLetter == null;
				num = (float)vector;
				tMP_Text = tMP_Text2;
				if (!flag27)
				{
					bool flag28 = !teleprinter.skipAnimation;
					num = (float)vector;
					tMP_Text = tMP_Text2;
					if (flag28)
					{
						_003C_003E2__current = teleprinter._waitPerLetter;
						_003C_003E1__state = 3;
						goto IL_138a;
					}
				}
			}
			goto IL_0a51;
			IL_1336:
			if (_003Ci_003E5__7 < _003CendIndex_003E5__5)
			{
				TMP_TextInfo tMP_TextInfo5 = _003Cti_003E5__3;
				if (_003Cti_003E5__3 != null)
				{
					tMP_Text = (TMP_Text)(object)tMP_TextInfo5.characterInfo;
					if (tMP_TextInfo5.characterInfo != null)
					{
						flag3 = (byte)_003Ci_003E5__7 != 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800039A0");
						object obj11 = default(object);
						tMP_Text = (TMP_Text)(obj11 >> 32);
						_003Cc_003E5__8 = (char)(int)tMP_Text;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-64]");
						_003CcharLineNum_003E5__9 = 0;
						bool flag29;
						if ((nint)tMP_Text == 10)
						{
							flag29 = true;
						}
						else
						{
							object obj12 = tMP_Text - 13;
							bool flag30 = obj12 == null;
							flag29 = flag30;
						}
						_003CisLineBreakChar_003E5__10 = flag29;
						bool flag31 = _003CcharLineNum_003E5__9 > _003CprevLineNumLocal_003E5__6;
						nint num2 = (nint)(&obj11);
						vector = (Vector3)num;
						if (flag31)
						{
							if (teleprinter.onLineTransition != null)
							{
								int arg2 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 320));
								int arg3 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 336));
								_ = _003CcharLineNum_003E5__9;
								_ = _003CprevLineNumLocal_003E5__6;
								teleprinter.onLineTransition.Invoke(arg3, arg2);
							}
							int lineCount = _003CcharLineNum_003E5__9 - _003CprevLineNumLocal_003E5__6;
							float num13 = _003C_003E4__this.GetLineVerticalDeltaCached(_003Cti_003E5__3, _003CprevLineNumLocal_003E5__6, _003CcharLineNum_003E5__9);
							if (teleprinter.invertPaperDirection)
							{
								float num14 = num13;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
								num13 = num14 ^ 0;
							}
							IEnumerator enumerator = _003C_003E4__this.MovePaper(num13, lineCount, compensateCursorWorldPosition: false);
							_003C_003E2__current = enumerator;
							_003C_003E1__state = 1;
							goto IL_138a;
						}
						goto IL_0773;
					}
				}
			}
			else
			{
				teleprinter._currentFullRich = _003CnewFull_003E5__2;
				tMP_Text = teleprinter._tmp;
				teleprinter._currentRevealedCharIndex = _003CendIndex_003E5__5;
				teleprinter._prevLineNum = _003CprevLineNumLocal_003E5__6;
				bool flag32 = (object)teleprinter._tmp == null;
				nint num2 = _003CendIndex_003E5__5;
				if (!flag32)
				{
					teleprinter._tmp.maxVisibleCharacters = _003CendIndex_003E5__5;
					tMP_Text = teleprinter._tmp;
					bool flag33 = (object)teleprinter._tmp == null;
					flag3 = false;
					num2 = _003CendIndex_003E5__5;
					if (!flag33)
					{
						teleprinter._tmp.ForceMeshUpdate();
						goto IL_1168;
					}
				}
			}
			goto IL_1129;
			IL_124f:
			throw new IndexOutOfRangeException();
			IL_0a51:
			if ((object)_003C_003E4__this != null)
			{
				bool flag34 = !teleprinter.debugChars;
				Vector3 vector2 = (Vector3)num10;
				if (!flag34)
				{
					object[] array = new object[4];
					object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 320));
					_ = _003Cc_003E5__8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if (array == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					TMP_Text tMP_Text3 = default(TMP_Text);
					nint num2;
					if ((object)tMP_Text3 != null)
					{
						nint num15 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1204 @ rdx_v54 (Il2CppClass<System.Object[]>)+40]");
						num2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj14 = default(object);
						bool flag35 = obj14 == null;
						tMP_Text = tMP_Text3;
						if (flag35)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj15 = default(object);
							throw obj15;
						}
					}
					array[0] = tMP_Text3;
					object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 344));
					_ = _003Ci_003E5__7;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj17 = default(object);
					if (obj17 != null)
					{
						nint num16 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1496 @ rdx_v52 (Il2CppClass<System.Object[]>)+40]");
						object obj18 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj19 = default(object);
						bool flag36 = obj19 == null;
						object obj20 = obj17;
						if (flag36)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj21 = default(object);
							throw obj21;
						}
					}
					array[1] = obj17;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj22 = default(object);
					if (obj22 != null)
					{
						nint num17 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1660 @ rdx_v50 (Il2CppClass<System.Object[]>)+40]");
						object obj23 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj24 = default(object);
						bool flag37 = obj24 == null;
						object obj25 = obj22;
						if (flag37)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							int num18 = default(int);
							throw num18;
						}
					}
					array[2] = obj22;
					int num19 = (int)(object)(Vector3)num10;
					if (num19 != 0)
					{
						nint num20 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1726 @ rdx_v48 (Il2CppClass<System.Object[]>)+40]");
						object obj26 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj27 = default(object);
						bool flag38 = obj27 == null;
						num = (float)teleprinter._lastCursorWorldPos;
						int num21 = num19;
						if (flag38)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj28 = default(object);
							throw obj28;
						}
					}
					array[3] = num19;
					string message = string.Format("[Teleprinter] Char '{0}' idx={1} line={2} pos={3}", array);
					Debug.Log(message);
					int num22 = _003CcharLineNum_003E5__9;
					vector2 = teleprinter._lastCursorWorldPos;
					flag3 = false;
					num2 = unchecked((nint)null);
					num = (float)teleprinter._lastCursorWorldPos;
				}
				tMP_Text = teleprinter._tmp;
				if ((object)teleprinter._tmp != null)
				{
					int num23 = _003Ci_003E5__7 + 1;
					teleprinter._tmp.maxVisibleCharacters = num23;
					if (teleprinter.onCharacterPrinted != null)
					{
						teleprinter.onCharacterPrinted.Invoke();
						num23 = 0;
					}
					tMP_Text = (TMP_Text)(object)teleprinter.onCharacterPrintedDetailed;
					bool flag39 = teleprinter.onCharacterPrintedDetailed == null;
					flag3 = false;
					if (!flag39)
					{
						num23 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 336));
						_ = _003Cc_003E5__8;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180941A60");
						int num24 = _003Ci_003E5__7;
						int num25 = _003CcharLineNum_003E5__9;
						int num5 = (int)(&num25);
						flag3 = (byte)(&num24) != 0;
					}
					int num26 = _003Ci_003E5__7 + 1;
					_003Ci_003E5__7 = num26;
					num10 = (float)vector2;
					nint num2 = num23;
					goto IL_1336;
				}
			}
			goto IL_1129;
			IL_138a:
			return true;
			IL_1129:
			throw new NullReferenceException();
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

	public Teleprinters TeleprinterType;

	private static Dictionary<Teleprinters, Teleprinter> Lookup;

	public float pausePerLetter;

	public float cursorMaxSpeed;

	public float paperFeedSpeed;

	public float interJobDelay;

	public bool padEmptyLines;

	public bool skipAnimation;

	public bool invertPaperDirection;

	public bool resetPaperPositionOnComplete;

	public bool accumulatePaperFeed;

	public PrintingOrder printingOrder;

	public Transform paperTransform;

	public RectTransform typerCursor;

	public Animator typerAnimator;

	public string typingBoolName;

	public CursorLockMode cursorLockMode;

	public bool useLocalCursorPosition;

	public bool preserveLocalZ;

	public Transform rotateTransform;

	public string rotateTransformTag;

	public Vector3 rotationAxis;

	public float degreesPerLine;

	public bool rotateInLocalSpace;

	public bool resetRotationOnComplete;

	public bool debugCursor;

	public bool debugChars;

	public UnityEvent onTypingStarted;

	public UnityEvent onAllJobsCompleted;

	public UnityEvent onJobStarted;

	public UnityEvent onJobCompleted;

	public UnityEvent onCharacterPrinted;

	public UnityEvent onLineFeed;

	public UnityEvent onJobsEnqueued;

	public UnityEvent OnSignal_Reset;

	public UnityEvent OnSignal_None;

	public UnityEvent OnSignal_High;

	public UnityEvent OnSignal_Low;

	public UnityEvent OnSignal_Success;

	public TeleprinterCharacterEvent onCharacterPrintedDetailed;

	public TeleprinterLineTransitionEvent onLineTransition;

	private TMP_Text _tmp;

	private Coroutine _runner;

	private bool _isRunning;

	private bool _baselineSet;

	private float _baselineWorldY;

	private Vector3 _initialPaperLocalPos;

	private Vector3 _lastCursorWorldPos;

	private int _prevLineNum;

	private bool _animTypingState;

	private readonly Queue<PrintJob> _pendingJobs;

	private long _nextJobId;

	public Action OnPrintingWillStart;

	public Action OnCleared;

	private static readonly Regex emptyLineRegex;

	private string _currentFullRich;

	private int _currentRevealedCharIndex;

	private Quaternion _initialRotateLocal;

	private Quaternion _initialRotateWorld;

	private bool _initialRotateStored;

	private float _cachedPausePerLetter;

	private WaitForSeconds _waitPerLetter;

	private float _cachedInterJobDelay;

	private WaitForSeconds _waitInterJob;

	private readonly List<bool> _revealMask;

	private bool _applyMaskThisFrame;

	private int _003CCurrentLineCount_003Ek__BackingField;

	public bool HasJobs
	{
		get
		{
			//IL_009e: Expected I4, but got O
			Queue<PrintJob> pendingJobs = _pendingJobs;
			if (_pendingJobs != null)
			{
				int num = pendingJobs._size ^ pendingJobs._size;
				int num2 = pendingJobs._size & num;
				bool flag = num2 < 0;
				bool flag2 = pendingJobs._size < 0;
				bool flag3 = pendingJobs._size == 0;
				bool flag4 = flag2 == flag;
				bool flag5 = !flag3;
				return flag5 & flag4;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public bool IsPrinting => _isRunning;

	public int CurrentLineCount
	{
		get
		{
			return _003CCurrentLineCount_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentLineCount_003Ek__BackingField = value;
		}
	}

	public unsafe static Teleprinter GetTeleprinter(Teleprinters type)
	{
		if (Lookup != null)
		{
			object obj = default(object);
			bool flag = Lookup.TryGetValue((Teleprinters)(int)(&obj), out var value);
			bool flag2 = !flag;
			Teleprinter result = null;
			if (!flag2)
			{
				result = value;
			}
			return result;
		}
		return (Teleprinter)(object)new NullReferenceException();
	}

	private unsafe void Awake()
	{
		//IL_00f7: Expected O, but got F4
		//IL_03d9: Expected O, but got F4
		//IL_03fc: Expected O, but got F4
		Teleprinters teleprinters = default(Teleprinters);
		if (Lookup.TryGetValue((Teleprinters)(int)(&teleprinters), out var value) && value != null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		Lookup.set_Item((Teleprinters)(int)(&teleprinters), this);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		TMP_Text tmp = default(TMP_Text);
		_tmp = tmp;
		if (!paperTransform)
		{
			Transform transform = _tmp.transform;
			paperTransform = transform;
		}
		Vector3 localPosition = paperTransform.localPosition;
		_initialPaperLocalPos = (Vector3)localPosition.x;
		_ = localPosition.z;
		string text = _tmp.text;
		bool flag = text == null;
		string currentFullRich = "";
		if (!flag)
		{
			currentFullRich = text;
		}
		_currentFullRich = currentFullRich;
		_tmp.ForceMeshUpdate();
		TMP_TextInfo textInfo = _tmp.textInfo;
		_currentRevealedCharIndex = textInfo.characterCount;
		_tmp.maxVisibleCharacters = textInfo.characterCount;
		TMP_TextInfo textInfo2 = _tmp.textInfo;
		EnsureMaskCapacity(textInfo2.characterCount);
		TMP_Text tmp2 = _tmp;
		int num = 0;
		int num2 = 0;
		while (true)
		{
			TMP_TextInfo textInfo3 = tmp2.textInfo;
			if (num < textInfo3.characterCount)
			{
				_revealMask.set_Item(num2, (byte)(&teleprinters) != 0);
				num2++;
				tmp2 = _tmp;
				num = num2;
				continue;
			}
			break;
		}
		if (rotateTransform == null && !string.IsNullOrEmpty(rotateTransformTag))
		{
			GameObject gameObject = GameObject.FindWithTag(rotateTransformTag);
			if (!(gameObject != null))
			{
				string message = "[Teleprinter] No GameObject found with tag '" + rotateTransformTag + "'. rotateTransform remains null.";
				Debug.LogWarning(message);
			}
			else
			{
				Transform transform2 = gameObject.transform;
				rotateTransform = transform2;
				if (debugCursor || debugChars)
				{
					string text2 = gameObject.name;
					string message2 = "[Teleprinter] Found rotateTransform by tag '" + rotateTransformTag + "' -> " + text2;
					Debug.Log(message2);
				}
			}
		}
		if ((bool)rotateTransform)
		{
			_initialRotateLocal = (Quaternion)rotateTransform.localRotation.x;
			_initialRotateWorld = (Quaternion)rotateTransform.rotation.x;
			_initialRotateStored = true;
		}
		RefreshWaitCaches();
	}

	private void OnValidate()
	{
		RefreshWaitCaches();
	}

	private void LateUpdate()
	{
		if (_applyMaskThisFrame && printingOrder == PrintingOrder.BottomUp)
		{
			ApplyAlphaMaskToText();
		}
	}

	private void RefreshWaitCaches()
	{
		//IL_001c: Invalid comparison between F4 and I4
		//IL_0076: Invalid comparison between F4 and I4
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		bool flag = obj != null;
		Teleprinter teleprinter = this;
		if (!flag)
		{
			_cachedPausePerLetter = pausePerLetter;
			WaitForSeconds waitPerLetter;
			if (pausePerLetter > 0f)
			{
				WaitForSeconds waitForSeconds = new WaitForSeconds(pausePerLetter);
				waitPerLetter = waitForSeconds;
			}
			else
			{
				waitPerLetter = null;
			}
			teleprinter = (Teleprinter)(this + 400);
			_waitPerLetter = waitPerLetter;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj2 = default(object);
		if (obj2 == null)
		{
			_cachedInterJobDelay = interJobDelay;
			bool flag2 = !(interJobDelay > 0f);
			WaitForSeconds waitInterJob = null;
			if (!flag2)
			{
				WaitForSeconds waitForSeconds2 = new WaitForSeconds(interJobDelay);
				waitInterJob = waitForSeconds2;
			}
			_waitInterJob = waitInterJob;
		}
	}

	public void SignalAlarm(TeleprinterAlarmState alarmState)
	{
		//IL_002b: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		bool flag = alarmState == TeleprinterAlarmState.None;
		UnityEvent unityEvent;
		if (!flag)
		{
			object obj = alarmState - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						return;
					}
					unityEvent = OnSignal_Success;
				}
				else
				{
					unityEvent = OnSignal_Low;
				}
			}
			else
			{
				unityEvent = OnSignal_High;
			}
		}
		else
		{
			unityEvent = OnSignal_None;
		}
		unityEvent?.Invoke();
	}

	public void ClearAlarm()
	{
		if (OnSignal_Reset != null)
		{
			OnSignal_Reset.Invoke();
		}
	}

	public PrintJob SubmitLines(string sourceId, IEnumerable<string> lines, object userData = null, bool waitForTrigger = false)
	{
		long nextJobId = _nextJobId + 1;
		_nextJobId = nextJobId;
		PrintJob printJob = null;
		List<string> lines2 = new List<string>();
		printJob.lines = lines2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		printJob.jobId = _nextJobId;
		printJob.sourceId = sourceId;
		if (lines != null)
		{
			if (printJob.lines == null)
			{
				goto IL_0170;
			}
			printJob.lines.AddRange(lines);
		}
		DateTime utcNow = DateTime.UtcNow;
		printJob.submittedUtc = utcNow;
		printJob.userData = userData;
		if (_pendingJobs != null)
		{
			_pendingJobs.Enqueue(printJob);
			int num = Enumerable.Count(lines);
			int num2 = num + _003CCurrentLineCount_003Ek__BackingField;
			_003CCurrentLineCount_003Ek__BackingField = num2;
			if (onJobsEnqueued != null)
			{
				onJobsEnqueued.Invoke();
			}
			object obj = default(object);
			if (obj == null)
			{
				TryStart();
			}
			return printJob;
		}
		goto IL_0170;
		IL_0170:
		return (PrintJob)(object)new NullReferenceException();
	}

	public void TryStart(bool ignoreInitialDelay = false)
	{
		if (_runner == null)
		{
			Queue<PrintJob> pendingJobs = _pendingJobs;
			if (pendingJobs._size > 0)
			{
				_003CRunQueue_003Ed__91 obj = new _003CRunQueue_003Ed__91(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				Coroutine runner = StartCoroutine(obj);
				_runner = runner;
			}
		}
	}

	public unsafe void ForceCompleteAll()
	{
		//IL_0083: Expected O, but got Ref
		//IL_0190: Expected O, but got Ref
		//IL_0150: Expected O, but got Ref
		if (_runner != null)
		{
			StopCoroutine(_runner);
			_runner = null;
		}
		DrainAllJobsInstant();
		SetTypingAnimator(state: false);
		Vector3 initialPaperLocalPos = default(Vector3);
		if (resetPaperPositionOnComplete && !accumulatePaperFeed)
		{
			paperTransform.localPosition = (Vector3)(&initialPaperLocalPos);
			initialPaperLocalPos = _initialPaperLocalPos;
		}
		if (resetRotationOnComplete && (bool)rotateTransform && !accumulatePaperFeed && _initialRotateStored)
		{
			if (!rotateInLocalSpace)
			{
				rotateTransform.rotation = (Quaternion)(&initialPaperLocalPos);
			}
			else
			{
				rotateTransform.localRotation = (Quaternion)(&initialPaperLocalPos);
			}
		}
		if (onAllJobsCompleted != null)
		{
			onAllJobsCompleted.Invoke();
		}
	}

	public unsafe void ClearAll()
	{
		//IL_01db: Expected O, but got Ref
		//IL_0083: Expected O, but got Ref
		//IL_0297: Expected O, but got I
		//IL_02a7: Expected O, but got I
		//IL_02c6: Expected O, but got I
		//IL_02d6: Expected O, but got I
		//IL_0262: Expected O, but got Ref
		//IL_0244: Expected O, but got Ref
		//IL_0395: Expected O, but got F4
		//IL_03be: Expected O, but got Ref
		//IL_018e: Expected O, but got Ref
		//IL_0170: Expected O, but got Ref
		if (_runner != null)
		{
			StopCoroutine(_runner);
			_runner = null;
		}
		DrainAllJobsInstant();
		SetTypingAnimator(state: false);
		Vector3 vector = default(Vector3);
		if (resetPaperPositionOnComplete && !accumulatePaperFeed)
		{
			paperTransform.localPosition = (Vector3)(&vector);
			vector = _initialPaperLocalPos;
		}
		bool flag = !resetRotationOnComplete;
		Quaternion quaternion = (Quaternion)vector;
		if (!flag)
		{
			bool flag2 = rotateTransform;
			bool flag3 = !flag2;
			quaternion = (Quaternion)vector;
			if (!flag3)
			{
				bool flag4 = accumulatePaperFeed;
				quaternion = (Quaternion)vector;
				if (!flag4)
				{
					bool flag5 = !_initialRotateStored;
					quaternion = (Quaternion)vector;
					if (!flag5)
					{
						if (!rotateInLocalSpace)
						{
							rotateTransform.rotation = (Quaternion)(&vector);
							quaternion = _initialRotateWorld;
						}
						else
						{
							rotateTransform.localRotation = (Quaternion)(&vector);
							quaternion = _initialRotateLocal;
						}
					}
				}
			}
		}
		if (onAllJobsCompleted != null)
		{
			onAllJobsCompleted.Invoke();
		}
		paperTransform.localPosition = (Vector3)(&quaternion);
		bool flag6 = rotateTransform != null;
		bool flag7 = !flag6;
		vector = _initialPaperLocalPos;
		if (!flag7)
		{
			if (!rotateInLocalSpace)
			{
				rotateTransform.rotation = (Quaternion)(&vector);
				vector = (Vector3)_initialRotateWorld;
			}
			else
			{
				rotateTransform.localRotation = (Quaternion)(&vector);
				vector = (Vector3)_initialRotateLocal;
			}
		}
		_003CCurrentLineCount_003Ek__BackingField = 0;
		_currentRevealedCharIndex = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v508 @ rax_v16+B8]");
		object currentFullRich = 0;
		_currentFullRich = (string)currentFullRich;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v511 @ rax_v18+B8]");
		object text = 0;
		_tmp.text = (string)text;
		_tmp.ForceMeshUpdate();
		TMP_TextInfo textInfo = _tmp.textInfo;
		int charIndex = _currentRevealedCharIndex;
		int num = textInfo.characterCount - 1;
		if (_currentRevealedCharIndex >= num)
		{
			charIndex = num;
		}
		Vector3 charWorldPositionFromInfoOrApprox = GetCharWorldPositionFromInfoOrApprox(textInfo, charIndex);
		_lastCursorWorldPos = (Vector3)charWorldPositionFromInfoOrApprox.x;
		_ = charWorldPositionFromInfoOrApprox.z;
		float baselineWorldY = default(float);
		_baselineWorldY = baselineWorldY;
		_baselineSet = true;
		SetCursorPosition((Vector3)(&vector));
		Action onCleared = OnCleared;
		_prevLineNum = 0;
		if (OnCleared != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v544.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	private IEnumerator RunQueue()
	{
		_003CRunQueue_003Ed__91 obj = new _003CRunQueue_003Ed__91(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void AppendInstant(string chunkRich, bool prepend)
	{
		//IL_0185: Expected O, but got I4
		//IL_0214: Expected O, but got Ref
		string currentFullRich;
		if (prepend)
		{
			bool flag = string.IsNullOrEmpty(_currentFullRich);
			string text = chunkRich;
			if (!flag)
			{
				if (string.IsNullOrEmpty(chunkRich))
				{
					text = _currentFullRich;
				}
				else
				{
					string text2 = chunkRich + "\n" + _currentFullRich;
					text = text2;
				}
			}
			currentFullRich = text;
		}
		else
		{
			currentFullRich = _currentFullRich + chunkRich;
		}
		_currentFullRich = currentFullRich;
		_tmp.text = _currentFullRich;
		_tmp.maxVisibleCharacters = 2147483647;
		_tmp.ForceMeshUpdate();
		TMP_TextInfo textInfo = _tmp.textInfo;
		_currentRevealedCharIndex = textInfo.characterCount;
		EnsureMaskCapacity(textInfo.characterCount);
		bool flag2 = _currentRevealedCharIndex <= 0;
		int num = 0;
		if (!flag2)
		{
			object obj = default(object);
			bool flag3;
			do
			{
				_revealMask.set_Item(num, (byte)(&obj) != 0);
				num++;
				flag3 = num < _currentRevealedCharIndex;
				obj = 1;
			}
			while (flag3);
		}
		_applyMaskThisFrame = false;
		if (!_baselineSet && _currentRevealedCharIndex > 0)
		{
			Vector3 charWorldPositionFromInfoOrApprox = GetCharWorldPositionFromInfoOrApprox(textInfo, 0);
			float baselineWorldY = default(float);
			_baselineWorldY = baselineWorldY;
			_baselineSet = true;
			object obj2 = default(object);
			SetCursorPosition((Vector3)(&obj2));
			TMP_CharacterInfo[] characterInfo = textInfo.characterInfo;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ rax_v19 (TMPro.TMP_CharacterInfo[])+5C]");
			_prevLineNum = 0;
		}
	}

	private unsafe void DrainAllJobsInstant()
	{
		//IL_02b7: Expected O, but got I4
		//IL_05b0: Expected O, but got I4
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		string text = default(string);
		if (printingOrder == PrintingOrder.TopDown)
		{
			string currentFullRich = _currentFullRich;
			StringBuilder stringBuilder = new StringBuilder(_currentFullRich);
			Queue<PrintJob> pendingJobs = _pendingJobs;
			PrintJob result;
			while (pendingJobs.TryDequeue(out result))
			{
				if (result != null)
				{
					bool flag = _currentRevealedCharIndex <= 0;
					PrintJob printJob = result;
					if (!flag)
					{
						StringBuilder stringBuilder2 = stringBuilder.Append('\n');
						printJob = result;
					}
					currentFullRich = null;
					while (true)
					{
						List<string> lines = printJob.lines;
						if ((nint)currentFullRich >= lines._size)
						{
							break;
						}
						if ((nint)currentFullRich > 0)
						{
							StringBuilder stringBuilder3 = stringBuilder.Append('\n');
							printJob = result;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						bool flag2 = text == null;
						string text2 = "";
						if (!flag2)
						{
							text2 = text;
						}
						if (padEmptyLines && text2._stringLength == 0)
						{
							text2 = " ";
						}
						StringBuilder stringBuilder4 = stringBuilder.Append(text2);
						currentFullRich++;
						printJob = result;
					}
				}
				pendingJobs = _pendingJobs;
			}
			string currentFullRich2 = stringBuilder.ToString();
			_currentFullRich = currentFullRich2;
			_tmp.text = _currentFullRich;
			_tmp.maxVisibleCharacters = 2147483647;
			_tmp.ForceMeshUpdate();
			TMP_TextInfo textInfo = _tmp.textInfo;
			_currentRevealedCharIndex = textInfo.characterCount;
			EnsureMaskCapacity(textInfo.characterCount);
			bool flag3 = _currentRevealedCharIndex <= 0;
			int num = 0;
			if (!flag3)
			{
				bool flag4;
				do
				{
					_revealMask.set_Item(num, (byte)(&text) != 0);
					num++;
					flag4 = num < _currentRevealedCharIndex;
					text = (string)1;
				}
				while (flag4);
				_applyMaskThisFrame = false;
				return;
			}
		}
		else
		{
			if (printingOrder != PrintingOrder.BottomUp)
			{
				return;
			}
			StringBuilder stringBuilder5 = new StringBuilder();
			Queue<PrintJob> pendingJobs2 = _pendingJobs;
			PrintJob result2;
			while (pendingJobs2.TryDequeue(out result2))
			{
				if (result2 == null)
				{
					goto IL_0417;
				}
				IEnumerable<string> lines2 = result2.lines;
				string text4;
				if (result2.lines != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v730 @ rbx_v12 (System.Collections.Generic.IEnumerable`1<System.String>)+18]");
					if ((nint)0 != 0)
					{
						string text3 = string.Join("\n", result2.lines);
						text4 = text3;
						goto IL_066d;
					}
				}
				text4 = "";
				goto IL_066d;
				IL_066d:
				if (padEmptyLines)
				{
					string text5 = PadEmptyLines(text4);
					text4 = text5;
				}
				if (!string.IsNullOrEmpty(text4))
				{
					int length = stringBuilder5.Length;
					if (length > 0)
					{
						StringBuilder stringBuilder6 = stringBuilder5.Insert(0, '\n');
					}
					StringBuilder stringBuilder7 = stringBuilder5.Insert(0, text4);
				}
				goto IL_0417;
				IL_0417:
				pendingJobs2 = _pendingJobs;
				if (_pendingJobs == null)
				{
					throw new NullReferenceException();
				}
			}
			string text6 = stringBuilder5.ToString();
			bool flag5 = string.IsNullOrEmpty(_currentFullRich);
			string currentFullRich3 = text6;
			if (!flag5)
			{
				if (string.IsNullOrEmpty(text6))
				{
					currentFullRich3 = _currentFullRich;
				}
				else
				{
					string text7 = text6 + "\n" + _currentFullRich;
					currentFullRich3 = text7;
				}
			}
			_currentFullRich = currentFullRich3;
			_tmp.text = _currentFullRich;
			_tmp.maxVisibleCharacters = 2147483647;
			_tmp.ForceMeshUpdate();
			TMP_TextInfo textInfo2 = _tmp.textInfo;
			_currentRevealedCharIndex = textInfo2.characterCount;
			EnsureMaskCapacity(textInfo2.characterCount);
			bool flag6 = _currentRevealedCharIndex <= 0;
			int num2 = 0;
			if (!flag6)
			{
				bool flag7;
				do
				{
					_revealMask.set_Item(num2, (byte)(&text) != 0);
					num2++;
					flag7 = num2 < _currentRevealedCharIndex;
					text = (string)1;
				}
				while (flag7);
			}
		}
		_applyMaskThisFrame = false;
	}

	private IEnumerator TypeChunkTopDown(string chunkRich)
	{
		_003CTypeChunkTopDown_003Ed__94 obj = new _003CTypeChunkTopDown_003Ed__94(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.chunkRich = chunkRich;
		return obj;
	}

	private IEnumerator TypeChunkBottomUp(string chunkRich)
	{
		_003CTypeChunkBottomUp_003Ed__95 obj = new _003CTypeChunkBottomUp_003Ed__95(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.chunkRich = chunkRich;
		return obj;
	}

	private void ApplyAlphaMaskToText()
	{
		//IL_0056: Expected O, but got I4
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_00db: Expected O, but got I4
		//IL_00e4: Expected O, but got I4
		//IL_00ed: Expected O, but got I4
		//IL_00f6: Expected O, but got I4
		//IL_00b2: Expected O, but got I4
		//IL_00bb: Expected O, but got I4
		//IL_00c4: Expected O, but got I4
		//IL_00cd: Expected O, but got I4
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0133: Expected O, but got I
		TMP_TextInfo textInfo = _tmp.textInfo;
		if (textInfo.characterCount <= 0)
		{
			return;
		}
		List<bool> revealMask = _revealMask;
		object obj = 0;
		object obj3 = default(object);
		object obj4 = default(object);
		object obj10 = default(object);
		do
		{
			object obj2 = obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rcx_v6 (System.Collections.Generic.List`1<System.Boolean>)+18]");
			if ((nint)obj2 >= 0)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800039A0");
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (obj4 != null)
				{
					object obj5 = 255;
					object obj6 = 255;
					object obj7 = 255;
					object obj8 = 255;
				}
				else
				{
					object obj5 = 0;
					object obj6 = 0;
					object obj7 = 0;
					object obj8 = 0;
				}
				TMP_MeshInfo[] meshInfo = textInfo.meshInfo;
				object obj9 = obj10 * 4;
				object obj11 = obj10 + obj9;
				object obj12 = obj11 + obj11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rcx_v13 (TMPro.TMP_MeshInfo[])+58+v199 @ rax_v17*8]");
				TMP_MeshInfo tMP_MeshInfo = (TMP_MeshInfo)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rcx_v13 (TMPro.TMP_MeshInfo[])+58+v199 @ rax_v17*8]");
				if ((nint)0 != 0 && tMP_MeshInfo.normals == null)
				{
				}
			}
			obj++;
		}
		while ((nint)obj < textInfo.characterCount);
		_tmp.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
	}

	private IEnumerator MovePaper(float lineDeltaLocal, int lineCount, bool compensateCursorWorldPosition)
	{
		_003CMovePaper_003Ed__97 obj = new _003CMovePaper_003Ed__97(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.lineDeltaLocal = lineDeltaLocal;
		obj.lineCount = lineCount;
		obj.compensateCursorWorldPosition = compensateCursorWorldPosition;
		return obj;
	}

	private IEnumerator MoveCursor(Vector3 fromWorld, Vector3 toWorld)
	{
		//IL_0017: Expected O, but got F4
		//IL_0029: Expected O, but got F4
		_003CMoveCursor_003Ed__98 obj = new _003CMoveCursor_003Ed__98(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.fromWorld = (Vector3)fromWorld.x;
		obj.toWorld = (Vector3)toWorld.x;
		_ = toWorld.z;
		_ = fromWorld.z;
		return obj;
	}

	private unsafe void ApplyInstantRotation(float lineDeltaLocal, int lineCount)
	{
		//IL_00fb: Expected I, but got O
		//IL_0145: Expected O, but got I
		//IL_018f: Invalid comparison between F4 and O
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_006c: Invalid comparison between F4 and I4
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_00dd: Expected O, but got Ref
		//IL_00ba: Expected O, but got Ref
		if (!rotateTransform)
		{
			return;
		}
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rax_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		object obj = rotationAxis - Vector3.zeroVector;
		object obj3 = default(object);
		object obj4 = default(object);
		object obj2 = obj3 - obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Teleprinter)+80]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ rcx_v6 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		object obj5 = num3 - 0;
		object obj6 = obj2 * obj2;
		object obj7 = obj * obj;
		object obj8 = obj5 * obj5;
		object obj9 = obj6 + obj7;
		object obj10 = obj9 + obj8;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10))
		{
			return;
		}
		object obj11 = degreesPerLine & -2147483649L;
		if ((nint)obj11 <= 2139095040)
		{
			float num4 = ((lineDeltaLocal < 0f) ? (-1f) : 1f);
			Vector3 vector = (Vector3)(this + 120);
			Vector3 normalized = ((Vector3*)vector)->normalized;
			float num5 = (float)lineCount * degreesPerLine;
			float angle = num5 * num4;
			Vector3 axis = default(Vector3);
			Quaternion quaternion = Quaternion.Internal_AngleAxis(angle, ref axis);
			object obj12 = default(object);
			if (!rotateInLocalSpace)
			{
				Quaternion rotation = rotateTransform.rotation;
				rotateTransform.rotation = (Quaternion)(&obj12);
			}
			else
			{
				Quaternion localRotation = rotateTransform.localRotation;
				rotateTransform.localRotation = (Quaternion)(&obj12);
			}
		}
	}

	private unsafe void ApplyCursorLock(ref Vector3 targetWorld)
	{
		//IL_0015: Expected O, but got I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_00af: Expected Ref, but got F4
		bool flag = cursorLockMode == CursorLockMode.None;
		if (flag)
		{
			return;
		}
		object obj = cursorLockMode - 1;
		if (!flag)
		{
			object obj2 = obj - 1;
			if (!flag)
			{
				if ((nint)obj2 == 1)
				{
					_ = _baselineWorldY;
					if ((bool)typerCursor)
					{
						ref Vector3 reference = ref *(Vector3*)typerCursor.position.x;
					}
				}
			}
			else
			{
				_ = _baselineWorldY;
				if ((bool)typerCursor)
				{
					_ = typerCursor.position.z;
				}
			}
		}
		else
		{
			_ = _baselineWorldY;
		}
	}

	private unsafe void SetCursorPosition(Vector3 worldPos)
	{
		//IL_0102: Expected O, but got Ref
		//IL_00a8: Expected O, but got Ref
		//IL_00ee: Expected O, but got Ref
		if (!typerCursor)
		{
			return;
		}
		float num = default(float);
		if (useLocalCursorPosition)
		{
			Transform parent = typerCursor.parent;
			if ((bool)parent)
			{
				Transform parent2 = typerCursor.parent;
				Vector3 vector = parent2.InverseTransformPoint((Vector3)(&num));
				if (preserveLocalZ)
				{
					Vector3 localPosition = typerCursor.localPosition;
				}
				typerCursor.localPosition = (Vector3)(&num);
				return;
			}
		}
		typerCursor.position = (Vector3)(&num);
	}

	private unsafe Vector3 GetCharWorldPositionFromInfoOrApprox(TMP_TextInfo ti, int charIndex)
	{
		//IL_013a: Expected native int or pointer, but got O
		//IL_014c: Expected native int or pointer, but got O
		//IL_024b: Expected O, but got Ref
		Transform transform;
		if (charIndex >= 0)
		{
			if (ti != null)
			{
				if (charIndex >= ti.characterCount)
				{
					goto IL_015b;
				}
				if (ti.characterInfo != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800039A0");
					object obj = default(object);
					if (obj == null)
					{
						if ((object)_tmp != null)
						{
							transform = _tmp.transform;
							if ((object)transform != null)
							{
								goto IL_023e;
							}
						}
					}
					else if ((object)_tmp != null)
					{
						transform = _tmp.transform;
						if ((object)transform != null)
						{
							goto IL_023e;
						}
					}
				}
			}
			goto IL_0213;
		}
		goto IL_015b;
		IL_023e:
		object obj2 = default(object);
		Vector3 vector = transform.TransformPoint((Vector3)(&obj2));
		goto IL_012d;
		IL_012d:
		Vector3 vector2 = default(Vector3);
		((Vector3*)(nint)vector2)->x = vector.x;
		((Vector3*)(nint)vector2)->z = vector.z;
		return vector2;
		IL_015b:
		if ((bool)typerCursor)
		{
			if ((object)typerCursor != null)
			{
				vector = typerCursor.position;
				goto IL_012d;
			}
		}
		else if ((object)_tmp != null)
		{
			Transform transform2 = _tmp.transform;
			if ((object)transform2 != null)
			{
				vector = transform2.position;
				goto IL_012d;
			}
		}
		goto IL_0213;
		IL_0213:
		return (Vector3)new NullReferenceException();
	}

	private float GetLineVerticalDeltaCached(TMP_TextInfo ti, int fromLine, int toLine)
	{
		//IL_01b8: Expected F4, but got I4
		//IL_0103: Expected O, but got I4
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0140: Expected O, but got I
		//IL_014e: Expected O, but got I4
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Expected O, but got Unknown
		int num = default(int);
		int num2 = default(int);
		if (num == num2 || ti.lineCount == 0)
		{
			return 0f;
		}
		if (num >= 0)
		{
			int num3 = ti.lineCount - 1;
			if (num > num3)
			{
				num = num3;
			}
		}
		else
		{
			num = 0;
		}
		if (num2 >= 0)
		{
			int num4 = ti.lineCount - 1;
			if (num2 > num4)
			{
				num2 = num4;
			}
		}
		else
		{
			num2 = 0;
		}
		TMP_LineInfo[] lineInfo = ti.lineInfo;
		object obj = num * 2;
		object obj2 = num + obj;
		object obj3 = obj2 << 5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ r8_v5+58+v45 @ r10_v2 (TMPro.TMP_LineInfo[])]");
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ r8_v5+50+v45 @ r10_v2 (TMPro.TMP_LineInfo[])]");
		object obj4 = num5 + 0;
		object obj5 = num2 * 2;
		object obj6 = num2 + obj5;
		object obj7 = obj6 << 5;
		float num6 = (float)obj4 * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rcx_v2+50+v45 @ r10_v2 (TMPro.TMP_LineInfo[])]");
		object obj9 = default(object);
		object obj8 = obj9 + 0;
		float num7 = (float)obj8 * 0.5f;
		return num7 - num6;
	}

	private unsafe List<LineRange> BuildChunkLineRanges(TMP_TextInfo ti, int startIndex, int endIndex)
	{
		//IL_005f: Expected O, but got I4
		//IL_007d: Expected O, but got I
		//IL_010d: Expected O, but got Ref
		//IL_00aa: Expected O, but got I4
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_00e2: Expected O, but got Ref
		//IL_00fa: Expected O, but got I
		List<LineRange> list = new List<LineRange>(8);
		TMP_CharacterInfo tMP_CharacterInfo2 = default(TMP_CharacterInfo);
		if (startIndex < endIndex)
		{
			if (ti != null)
			{
				TMP_CharacterInfo[] characterInfo = ti.characterInfo;
				if (ti.characterInfo != null)
				{
					object obj = startIndex * 376;
					int num = startIndex + 1;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ rcx_v6+5C+v79 @ rdx_v3 (TMPro.TMP_CharacterInfo[])]");
					TMP_CharacterInfo tMP_CharacterInfo = (TMP_CharacterInfo)0;
					if (num >= endIndex)
					{
						goto IL_0100;
					}
					object obj2 = num * 376;
					while (true)
					{
						TMP_CharacterInfo[] characterInfo2 = ti.characterInfo;
						if (ti.characterInfo == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rax_v17 (TMPro.TMP_CharacterInfo[])+5C+v113 @ rbp_v7]");
						if (0 != (nint)tMP_CharacterInfo)
						{
							list.Add((LineRange)(&tMP_CharacterInfo2));
							tMP_CharacterInfo2 = tMP_CharacterInfo;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rax_v17 (TMPro.TMP_CharacterInfo[])+5C+v113 @ rbp_v7]");
							tMP_CharacterInfo = (TMP_CharacterInfo)0;
						}
						num++;
						obj2 += 376;
						if (num < endIndex)
						{
							continue;
						}
						goto IL_0100;
					}
				}
			}
			return (List<LineRange>)(object)new NullReferenceException();
		}
		return list;
		IL_0100:
		list.Add((LineRange)(&tMP_CharacterInfo2));
		return list;
	}

	private string PadEmptyLines(string rich)
	{
		if (string.IsNullOrEmpty(rich))
		{
			return rich;
		}
		if (emptyLineRegex != null)
		{
			return emptyLineRegex.Replace(rich, "$1 $2");
		}
		return (string)(object)new NullReferenceException();
	}

	private string JoinJobLinesAppend(List<string> lines)
	{
		if (lines != null && lines._size != 0)
		{
			if (_currentRevealedCharIndex != 0)
			{
				string text = string.Join("\n", lines);
				return "\n" + text;
			}
			return string.Join("\n", lines);
		}
		return "";
	}

	private string JoinJobLinesPrepend(List<string> lines)
	{
		if (lines != null && lines._size != 0)
		{
			return string.Join("\n", lines);
		}
		return "";
	}

	private unsafe void EnsureMaskCapacity(int count)
	{
		//IL_008a: Expected O, but got I4
		int capacity = _revealMask.Capacity;
		if (capacity < count)
		{
			_revealMask.Capacity = count;
		}
		List<bool> revealMask = _revealMask;
		List<bool> revealMask2 = _revealMask;
		object obj = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rcx_v6 (System.Collections.Generic.List`1<System.Boolean>)+18]");
			if ((nint)0 >= (nint)count)
			{
				break;
			}
			revealMask2.Add((byte)(&obj) != 0);
			revealMask2 = _revealMask;
			obj = 1;
			revealMask = _revealMask;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v7 (System.Collections.Generic.List`1<System.Boolean>)+18]");
		if ((nint)0 > (nint)count)
		{
			List<bool> revealMask3 = _revealMask;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v5 (System.Collections.Generic.List`1<System.Boolean>)+18]");
			int count2 = (int)(-count);
			_revealMask.RemoveRange(count, count2);
		}
	}

	private void SetTypingAnimator(bool state)
	{
		if ((bool)typerAnimator && !string.IsNullOrEmpty(typingBoolName) && _animTypingState != state)
		{
			typerAnimator.SetBool(typingBoolName, state);
			_animTypingState = state;
		}
	}

	public Teleprinter()
	{
		//IL_0028: Expected I8, but got I4
		pausePerLetter = 0.07f;
		cursorMaxSpeed = 10f;
		paperFeedSpeed = 10f;
		accumulatePaperFeed = true;
		typingBoolName = "typing";
		cursorLockMode = CursorLockMode.LockYOnly;
		preserveLocalZ = true;
		rotateTransformTag = "";
		degreesPerLine = 360f;
		Vector3 vector = default(Vector3);
		rotationAxis = vector;
		_ = 1f;
		rotateInLocalSpace = true;
		Queue<PrintJob> queue = new Queue<PrintJob>();
		queue._002Ector();
		_pendingJobs = queue;
		_nextJobId = 1L;
		_currentFullRich = "";
		_initialRotateLocal = Quaternion.identityQuaternion;
		_cachedPausePerLetter = -1f;
		_cachedInterJobDelay = -1f;
		_initialRotateWorld = Quaternion.identityQuaternion;
		List<bool> revealMask = new List<bool>(256);
		_revealMask = revealMask;
		base._002Ector();
	}

	static Teleprinter()
	{
		Dictionary<Teleprinters, Teleprinter> lookup = new Dictionary<Teleprinters, Teleprinter>();
		Lookup = lookup;
		Regex regex = new Regex("(^|\\n)(\\n)", RegexOptions.Compiled);
		emptyLineRegex = regex;
	}
}
