using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class EspressoCupDrinker : MonoBehaviour
{
	private sealed class _003CAnimateIn_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public EspressoCupDrinker _003C_003E4__this;

		public Vector3 fromLocalPos;

		public Quaternion fromLocalRot;

		public Vector3 fromWorldScale;

		private float _003Celapsed_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAnimateIn_003Ed__32(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			//IL_0079: Expected I4, but got I8
			//IL_0084: Invalid comparison between I4 and F4
			//IL_0140: Expected I4, but got I8
			//IL_043f: Expected I4, but got O
			//IL_00a7: Expected F4, but got I4
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Expected O, but got Unknown
			//IL_0183: Invalid comparison between I4 and F4
			//IL_040f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0414: Expected O, but got Unknown
			//IL_01ce: Expected F4, but got I4
			//IL_0214: Invalid comparison between I4 and F4
			//IL_0267: Expected F4, but got I4
			//IL_048a: Unknown result type (might be due to invalid IL or missing references)
			//IL_048f: Expected Ref, but got Unknown
			//IL_0498: Unknown result type (might be due to invalid IL or missing references)
			//IL_049d: Expected Ref, but got Unknown
			//IL_04c0: Invalid comparison between I4 and F4
			//IL_02ad: Expected F4, but got I4
			//IL_04f1: Expected O, but got I
			//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c0: Expected O, but got Unknown
			//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ea: Expected Ref, but got Unknown
			//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f8: Expected Ref, but got Unknown
			//IL_0351: Unknown result type (might be due to invalid IL or missing references)
			//IL_0356: Expected O, but got Unknown
			//IL_038c: Invalid comparison between I4 and F4
			//IL_03d7: Expected F4, but got I4
			//IL_0542: Unknown result type (might be due to invalid IL or missing references)
			//IL_0547: Expected O, but got Unknown
			//IL_0564: Expected O, but got I
			object obj2 = default(object);
			object obj = obj2 - 95;
			EspressoCupDrinker espressoCupDrinker = _003C_003E4__this;
			_ = 0;
			_ = 0;
			_ = 0;
			Vector3 targetWorldScale;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (!(0f < duration))
				{
					if ((object)_003C_003E4__this == null)
					{
						goto IL_0431;
					}
					_003C_003E4__this.SnapToAnchorLocal();
					targetWorldScale = (Vector3)(obj - 25);
					_ = espressoCupDrinker.drinkScale;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbx_v1 (EspressoCupDrinker)+58]");
					_ = 0;
					goto IL_05c8;
				}
				_003Celapsed_003E5__2 = _003C_003E1__state;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0101;
				}
				_003C_003E1__state = -1;
			}
			if (duration > _003Celapsed_003E5__2)
			{
				float deltaTime = Time.deltaTime;
				float num = (_003Celapsed_003E5__2 = deltaTime + _003Celapsed_003E5__2) / duration;
				if (!(0f > num))
				{
					if (num > 1f)
					{
						num = 1f;
					}
				}
				else
				{
					num = 0f;
				}
				if ((object)_003C_003E4__this != null && espressoCupDrinker.easingCurve != null)
				{
					float num2 = espressoCupDrinker.easingCurve.Evaluate(num);
					float num3;
					if (!(0f > num2))
					{
						bool flag = !(num2 > 1f);
						num3 = num2;
						if (!flag)
						{
							num3 = 1f;
						}
					}
					else
					{
						num3 = 0f;
					}
					_003C_003E4__this.AnchorToLocal(out *(Vector3*)(obj - 41), out *(Quaternion*)(obj + 7));
					_ = fromLocalPos;
					float num4 = ((0f > num3) ? 0f : ((num3 > 1f) ? 1f : num3));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-21]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateIn>d__32)+38]");
					object obj3 = num5 - 0;
					float num6 = (float)obj3 * num4;
					float num7 = num6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateIn>d__32)+38]");
					float num8 = num7 + 0f;
					if ((object)espressoCupDrinker._resolvedTarget != null)
					{
						Vector3 localPosition = (Vector3)(obj - 25);
						espressoCupDrinker._resolvedTarget.localPosition = localPosition;
						ref Quaternion b = ref *(Quaternion*)(obj - 9);
						ref Quaternion a = ref *(Quaternion*)(obj - 25);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+7]");
						_ = 0;
						_ = fromLocalRot;
						Quaternion quaternion = Quaternion.Internal_Slerp(ref a, ref b, num3);
						if ((object)espressoCupDrinker._resolvedTarget != null)
						{
							Quaternion localRotation = (Quaternion)(obj - 9);
							_ = quaternion.x;
							espressoCupDrinker._resolvedTarget.localRotation = localRotation;
							_ = espressoCupDrinker.drinkScale;
							_ = fromWorldScale;
							if (!(0f > num3))
							{
								if (num3 > 1f)
								{
									num3 = 1f;
								}
							}
							else
							{
								num3 = 0f;
							}
							Vector3 targetWorldScale2 = (Vector3)(obj - 9);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbx_v1 (EspressoCupDrinker)+58]");
							nint num9 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateIn>d__32)+54]");
							object obj4 = num9 - 0;
							float num10 = (float)obj4 * num3;
							float num11 = num10;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateIn>d__32)+54]");
							float num12 = num11 + 0f;
							_003C_003E4__this.ApplyWorldScale(targetWorldScale2);
							_003C_003E2__current = null;
							_003C_003E1__state = 1;
							return true;
						}
					}
				}
			}
			else if ((object)_003C_003E4__this != null)
			{
				_003C_003E4__this.SnapToAnchorLocal();
				targetWorldScale = (Vector3)(obj - 9);
				_ = espressoCupDrinker.drinkScale;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbx_v1 (EspressoCupDrinker)+58]");
				_ = 0;
				goto IL_05c8;
			}
			goto IL_0431;
			IL_0101:
			return false;
			IL_05c8:
			_003C_003E4__this.ApplyWorldScale(targetWorldScale);
			goto IL_0101;
			IL_0431:
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

	private sealed class _003CAnimateOut_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public EspressoCupDrinker _003C_003E4__this;

		public Vector3 toLocalPos;

		public Quaternion toLocalRot;

		public Vector3 toWorldScale;

		public Vector3 fromLocalPos;

		public Vector3 fromWorldScale;

		private float _003Celapsed_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAnimateOut_003Ed__34(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			//IL_0074: Expected I4, but got I8
			//IL_007f: Invalid comparison between I4 and F4
			//IL_01da: Expected I4, but got I8
			//IL_04a3: Expected I4, but got O
			//IL_00a2: Expected F4, but got I4
			//IL_021d: Invalid comparison between I4 and F4
			//IL_0268: Expected F4, but got I4
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Expected O, but got Unknown
			//IL_0147: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Expected O, but got Unknown
			//IL_016e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Expected O, but got Unknown
			//IL_02ae: Invalid comparison between I4 and F4
			//IL_0301: Expected F4, but got I4
			//IL_04fc: Invalid comparison between I4 and F4
			//IL_0347: Expected F4, but got I4
			//IL_052d: Expected O, but got I
			//IL_0355: Unknown result type (might be due to invalid IL or missing references)
			//IL_035a: Expected O, but got Unknown
			//IL_038d: Invalid comparison between I4 and F4
			//IL_03dd: Expected F4, but got I4
			//IL_057e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0583: Expected O, but got Unknown
			//IL_05a0: Expected O, but got I
			//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ed: Expected Ref, but got Unknown
			//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fb: Expected Ref, but got Unknown
			//IL_0617: Unknown result type (might be due to invalid IL or missing references)
			//IL_061c: Expected Ref, but got Unknown
			//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f0: Expected Ref, but got Unknown
			//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fe: Expected Ref, but got Unknown
			//IL_044f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0454: Expected O, but got Unknown
			object obj2 = default(object);
			object obj = obj2 - 95;
			EspressoCupDrinker espressoCupDrinker = _003C_003E4__this;
			_ = 0;
			_ = 0;
			_ = 0;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (!(0f < duration))
				{
					if ((object)_003C_003E4__this != null && (object)espressoCupDrinker._resolvedTarget != null)
					{
						Vector3 localPosition = (Vector3)(obj - 121);
						_ = toLocalPos;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateOut>d__34)+38]");
						_ = 0;
						espressoCupDrinker._resolvedTarget.localPosition = localPosition;
						if ((object)espressoCupDrinker._resolvedTarget != null)
						{
							Quaternion localRotation = (Quaternion)(obj - 105);
							_ = toLocalRot;
							espressoCupDrinker._resolvedTarget.localRotation = localRotation;
							Vector3 targetWorldScale = (Vector3)(obj - 121);
							_ = toWorldScale;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateOut>d__34)+54]");
							_ = 0;
							_003C_003E4__this.ApplyWorldScale(targetWorldScale);
							goto IL_019b;
						}
					}
					goto IL_0495;
				}
				_003Celapsed_003E5__2 = _003C_003E1__state;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_019b;
				}
				_003C_003E1__state = -1;
			}
			if (!(duration > _003Celapsed_003E5__2))
			{
				goto IL_019b;
			}
			float deltaTime = Time.deltaTime;
			float num = (_003Celapsed_003E5__2 = deltaTime + _003Celapsed_003E5__2) / duration;
			if (!(0f > num))
			{
				if (num > 1f)
				{
					num = 1f;
				}
			}
			else
			{
				num = 0f;
			}
			if ((object)_003C_003E4__this != null && espressoCupDrinker.easingCurve != null)
			{
				float num2 = espressoCupDrinker.easingCurve.Evaluate(num);
				float num3;
				if (!(0f > num2))
				{
					bool flag = !(num2 > 1f);
					num3 = num2;
					if (!flag)
					{
						num3 = 1f;
					}
				}
				else
				{
					num3 = 0f;
				}
				_ = toLocalPos;
				_ = fromLocalPos;
				float num4 = ((0f > num3) ? 0f : ((num3 > 1f) ? 1f : num3));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateOut>d__34)+38]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateOut>d__34)+60]");
				object obj3 = num5 - 0;
				float num6 = (float)obj3 * num4;
				float num7 = num6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateOut>d__34)+60]");
				float num8 = num7 + 0f;
				if ((object)espressoCupDrinker._resolvedTarget != null)
				{
					Vector3 localPosition2 = (Vector3)(obj - 105);
					espressoCupDrinker._resolvedTarget.localPosition = localPosition2;
					_ = toWorldScale;
					_ = fromWorldScale;
					float num9;
					if (!(0f > num3))
					{
						bool flag2 = num3 > 1f;
						num9 = 1f;
						if (!flag2)
						{
							num9 = num3;
						}
					}
					else
					{
						num9 = 0f;
					}
					Vector3 targetWorldScale2 = (Vector3)(obj - 105);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateOut>d__34)+54]");
					nint num10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateOut>d__34)+6C]");
					object obj4 = num10 - 0;
					float num11 = (float)obj4 * num9;
					float num12 = num11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<AnimateOut>d__34)+6C]");
					float num13 = num12 + 0f;
					_003C_003E4__this.ApplyWorldScale(targetWorldScale2);
					_003C_003E4__this.AnchorToLocal(out *(Vector3*)(obj - 89), out *(Quaternion*)(obj - 73));
					ref Vector3 euler = ref *(Vector3*)(obj - 105);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rdi_v1 (EspressoCupDrinker)+80]");
					float num14 = 0f * ((float)Math.PI / 180f);
					_ = espressoCupDrinker.drinkTiltOffset;
					Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
					ref Quaternion b = ref *(Quaternion*)(obj - 121);
					ref Quaternion a = ref *(Quaternion*)(obj - 105);
					_ = toLocalRot;
					Quaternion quaternion2 = Quaternion.Internal_Slerp(ref a, ref b, num3);
					if ((object)espressoCupDrinker._resolvedTarget != null)
					{
						Quaternion localRotation2 = (Quaternion)(obj - 105);
						_ = quaternion2.x;
						espressoCupDrinker._resolvedTarget.localRotation = localRotation2;
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
			}
			goto IL_0495;
			IL_019b:
			return false;
			IL_0495:
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

	private sealed class _003CDrinkRoutine_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EspressoCupDrinker _003C_003E4__this;

		private Vector3 _003CoriginLocalPos_003E5__2;

		private Quaternion _003CoriginLocalRot_003E5__3;

		private Vector3 _003CoriginLocalScale_003E5__4;

		private Vector3 _003CoriginWorldScale_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDrinkRoutine_003Ed__31(int _003C_003E1__state)
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
			//IL_0408: Expected I4, but got I8
			//IL_001d: Expected O, but got I4
			//IL_03b6: Expected I4, but got I8
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_01e0: Expected I4, but got I8
			//IL_049e: Expected O, but got I4
			//IL_04b5: Expected O, but got I4
			//IL_08f8: Expected O, but got I
			//IL_0076: Expected I4, but got I8
			//IL_04db: Expected O, but got F4
			//IL_0089: Expected O, but got Ref
			//IL_050b: Expected O, but got F4
			//IL_02d8: Expected O, but got Ref
			//IL_02e6: Expected O, but got Ref
			//IL_02f4: Expected O, but got Ref
			//IL_00c2: Expected O, but got Ref
			//IL_0531: Expected O, but got F4
			//IL_00ee: Expected O, but got Ref
			//IL_0561: Expected O, but got F4
			//IL_0825: Expected O, but got Ref
			//IL_0833: Expected O, but got Ref
			//IL_0841: Expected O, but got Ref
			//IL_0889: Expected F4, but got O
			//IL_090b: Expected I4, but got O
			//IL_06ad: Expected O, but got Ref
			//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_06de: Expected Ref, but got Unknown
			//IL_06f9: Expected O, but got Ref
			//IL_0743: Expected O, but got Ref
			//IL_079f: Expected O, but got Ref
			object obj2 = default(object);
			object obj = (object)(&obj2);
			EspressoCupDrinker espressoCupDrinker = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			Vector3 vector = default(Vector3);
			if (!flag)
			{
				object obj3 = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj4 = obj3 - 1;
					if (!flag)
					{
						if ((nint)obj4 == 1)
						{
							_003C_003E1__state = -1;
							Vector3 localPosition = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
							_ = _003CoriginLocalPos_003E5__2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<DrinkRoutine>d__31)+30]");
							_ = 0;
							espressoCupDrinker._resolvedTarget.localPosition = localPosition;
							Quaternion localRotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
							_ = _003CoriginLocalRot_003E5__3;
							espressoCupDrinker._resolvedTarget.localRotation = localRotation;
							Vector3 localScale = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
							_ = _003CoriginLocalScale_003E5__4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<DrinkRoutine>d__31)+4C]");
							_ = 0;
							espressoCupDrinker._resolvedScaleTarget.localScale = localScale;
							if (espressoCupDrinker.debugLog)
							{
								string name = espressoCupDrinker.name;
								string message = "[" + name + "] Drink completed.";
								Debug.Log(message, espressoCupDrinker);
							}
							espressoCupDrinker._isAnimating = false;
							espressoCupDrinker._drinkRoutine = null;
							if (espressoCupDrinker.OnDrinkCompleted != null)
							{
								espressoCupDrinker.OnDrinkCompleted.Invoke();
							}
						}
						return false;
					}
					_003C_003E1__state = -1;
					espressoCupDrinker._cup.MarkEmpty();
					if (espressoCupDrinker.OnDrinkEmptied != null)
					{
						espressoCupDrinker.OnDrinkEmptied.Invoke();
					}
					if (espressoCupDrinker.debugLog)
					{
						string name2 = espressoCupDrinker.name;
						string message2 = "[" + name2 + "] Cup emptied. Beginning animate-out.";
						Debug.Log(message2, espressoCupDrinker);
					}
					Vector3 localPosition2 = espressoCupDrinker._resolvedTarget.localPosition;
					Quaternion localRotation2 = espressoCupDrinker._resolvedTarget.localRotation;
					Vector3 lossyScale = espressoCupDrinker._resolvedScaleTarget.lossyScale;
					Vector3 fromWorldScale = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
					Quaternion fromLocalRot = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
					Vector3 fromLocalPos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
					_ = _003CoriginWorldScale_003E5__5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<DrinkRoutine>d__31)+58]");
					_ = 0;
					_ = _003CoriginLocalRot_003E5__3;
					_ = lossyScale.z;
					_ = lossyScale.x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<DrinkRoutine>d__31)+30]");
					_ = 0;
					_ = _003CoriginLocalPos_003E5__2;
					_ = localRotation2.x;
					_ = localPosition2.x;
					_ = localPosition2.z;
					Quaternion toLocalRot = default(Quaternion);
					Vector3 toWorldScale = default(Vector3);
					float duration = default(float);
					IEnumerator enumerator = espressoCupDrinker.AnimateOut(fromLocalPos, fromLocalRot, fromWorldScale, vector, toLocalRot, toWorldScale, duration);
					_003C_003E2__current = enumerator;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E1__state = -1;
				IEnumerator enumerator2 = espressoCupDrinker.HoldWithTilt(espressoCupDrinker.drinkHoldDuration);
				_003C_003E2__current = enumerator2;
				_003C_003E1__state = 2;
				return true;
			}
			_003C_003E1__state = -1;
			espressoCupDrinker._isAnimating = true;
			Transform resolvedTarget = ((!(espressoCupDrinker.animationTarget != null)) ? espressoCupDrinker.transform : espressoCupDrinker.animationTarget);
			espressoCupDrinker._resolvedTarget = resolvedTarget;
			bool flag2 = espressoCupDrinker.scaleTarget == null;
			object obj5 = 72;
			if (flag2)
			{
				obj5 = 232;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v827 @ rdx_v10+v36 @ rdi_v1 (EspressoCupDrinker)]");
			espressoCupDrinker._resolvedScaleTarget = (Transform)0;
			Vector3 localPosition3 = espressoCupDrinker._resolvedTarget.localPosition;
			_003CoriginLocalPos_003E5__2 = (Vector3)localPosition3.x;
			_ = localPosition3.z;
			_003CoriginLocalRot_003E5__3 = (Quaternion)espressoCupDrinker._resolvedTarget.localRotation.x;
			Vector3 localScale2 = espressoCupDrinker._resolvedScaleTarget.localScale;
			_003CoriginLocalScale_003E5__4 = (Vector3)localScale2.x;
			_ = localScale2.z;
			Vector3 lossyScale2 = espressoCupDrinker._resolvedScaleTarget.lossyScale;
			_003CoriginWorldScale_003E5__5 = (Vector3)lossyScale2.x;
			_ = lossyScale2.z;
			if (espressoCupDrinker.OnDrinkStarted != null)
			{
				espressoCupDrinker.OnDrinkStarted.Invoke();
			}
			if (espressoCupDrinker.debugLog)
			{
				string[] array = new string[7];
				if (array != null)
				{
					array[0] = "[";
					string name3 = espressoCupDrinker.name;
					array[1] = name3;
					array[2] = "] Drink started. Target='";
					if ((object)espressoCupDrinker._resolvedTarget != null)
					{
						string name4 = espressoCupDrinker._resolvedTarget.name;
						array[3] = name4;
						array[4] = "' ";
						object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
						_ = _003CoriginLocalPos_003E5__2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<DrinkRoutine>d__31)+30]");
						_ = 0;
						object arg = (Vector3)obj6;
						Vector3 vector2 = Quaternion.Internal_ToEulerRad(ref *(Quaternion*)(this + 52));
						Vector3 euler = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
						float num = vector2.z * 57.29578f;
						_ = vector2.x;
						Vector3 vector3 = Quaternion.Internal_MakePositive(euler);
						object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
						_ = vector3.x;
						_ = vector3.z;
						object arg2 = (Vector3)obj7;
						string text = $"LocalPos={arg} LocalRot={arg2} ";
						array[5] = text;
						object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
						_ = _003CoriginWorldScale_003E5__5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<DrinkRoutine>d__31)+58]");
						_ = 0;
						object arg3 = (Vector3)obj8;
						string text2 = $"WorldScale={arg3}";
						array[6] = text2;
						string message3 = string.Concat(array);
						Debug.Log(message3, espressoCupDrinker);
						goto IL_0817;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_0817;
			IL_0817:
			Vector3 fromWorldScale2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
			Quaternion fromLocalRot2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
			Vector3 fromLocalPos2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			_ = _003CoriginWorldScale_003E5__5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<DrinkRoutine>d__31)+58]");
			_ = 0;
			_ = _003CoriginLocalRot_003E5__3;
			_ = _003CoriginLocalPos_003E5__2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (EspressoCupDrinker+<DrinkRoutine>d__31)+30]");
			_ = 0;
			IEnumerator enumerator3 = espressoCupDrinker.AnimateIn(fromLocalPos2, fromLocalRot2, fromWorldScale2, (float)vector);
			_003C_003E2__current = enumerator3;
			_003C_003E1__state = 1;
			return true;
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

	private sealed class _003CHoldWithTilt_003Ed__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public EspressoCupDrinker _003C_003E4__this;

		private float _003Celapsed_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CHoldWithTilt_003Ed__33(int _003C_003E1__state)
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
			//IL_0046: Invalid comparison between I4 and F4
			//IL_00e1: Expected I4, but got I8
			//IL_0213: Expected I4, but got O
			//IL_0069: Expected F4, but got I4
			//IL_0124: Invalid comparison between I4 and F4
			//IL_016f: Expected F4, but got I4
			//IL_01b5: Invalid comparison between I4 and F4
			//IL_0200: Expected F4, but got I4
			EspressoCupDrinker espressoCupDrinker = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (!(0f < duration))
				{
					goto IL_006e;
				}
				_003Celapsed_003E5__2 = _003C_003E1__state;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_00a2;
				}
				_003C_003E1__state = -1;
			}
			if (!(duration > _003Celapsed_003E5__2))
			{
				goto IL_006e;
			}
			float deltaTime = Time.deltaTime;
			float num = (_003Celapsed_003E5__2 = deltaTime + _003Celapsed_003E5__2) / duration;
			if (!(0f > num))
			{
				if (num > 1f)
				{
					num = 1f;
				}
			}
			else
			{
				num = 0f;
			}
			if ((object)_003C_003E4__this != null && espressoCupDrinker.tiltCurve != null)
			{
				float num2 = espressoCupDrinker.tiltCurve.Evaluate(num);
				if (!(0f > num2))
				{
					if (num2 > 1f)
					{
						num2 = 1f;
					}
				}
				else
				{
					num2 = 0f;
				}
				_003C_003E4__this.ApplyAnchorWithTilt(num2);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_0205;
			IL_006e:
			if ((object)_003C_003E4__this != null)
			{
				_003C_003E4__this.ApplyAnchorWithTilt(1f);
				goto IL_00a2;
			}
			goto IL_0205;
			IL_00a2:
			return false;
			IL_0205:
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

	private Transform animationTarget;

	private string cameraTag;

	private Vector3 drinkPositionOffset;

	private Vector3 drinkRotationOffset;

	private Transform scaleTarget;

	private Vector3 drinkScale;

	private float animateInDuration;

	private float drinkHoldDuration;

	private float animateOutDuration;

	private AnimationCurve easingCurve;

	private AnimationCurve tiltCurve;

	private Vector3 drinkTiltOffset;

	public UnityEvent OnDrinkTriggered;

	public UnityEvent OnDrinkStarted;

	public UnityEvent OnDrinkEmptied;

	public UnityEvent OnDrinkCompleted;

	public UnityEvent OnDrinkFailed;

	private bool debugLog;

	private EspressoCup _cup;

	private DraggableItem _draggable;

	private Transform _cameraTransform;

	private Transform _drinkAnchor;

	private Coroutine _drinkRoutine;

	private bool _isAnimating;

	private Transform _resolvedTarget;

	private Transform _resolvedScaleTarget;

	public bool IsAnimating => _isAnimating;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		EspressoCup cup = default(EspressoCup);
		_cup = cup;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		DraggableItem draggable = default(DraggableItem);
		_draggable = draggable;
		if (TryResolveCamera())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 56 Invalid \"Jump target not found in method: 0x18053B560\"");
		}
	}

	private void OnDestroy()
	{
		if (_drinkAnchor != null)
		{
			GameObject obj = _drinkAnchor.gameObject;
			UnityEngine.Object.Destroy(obj);
		}
	}

	public void DrinkCoffee()
	{
		string text;
		string text2;
		if (!_isAnimating)
		{
			EspressoCup cup = _cup;
			if (cup.isFull)
			{
				if (_draggable != null)
				{
					DraggableItem draggable = _draggable;
					if (draggable.IsBeingDragged)
					{
						if (!debugLog)
						{
							goto IL_02eb;
						}
						text = base.name;
						text2 = "] DrinkCoffee ignored — cup is being dragged.";
						goto IL_033d;
					}
				}
				if (_cameraTransform == null && TryResolveCamera())
				{
					BuildDrinkAnchor();
				}
				if (_cameraTransform != null && !(_drinkAnchor == null))
				{
					if (OnDrinkTriggered != null)
					{
						OnDrinkTriggered.Invoke();
					}
					_003CDrinkRoutine_003Ed__31 obj = new _003CDrinkRoutine_003Ed__31(0);
					obj._003C_003E4__this = this;
					Coroutine drinkRoutine = StartCoroutine(obj);
					_drinkRoutine = drinkRoutine;
					return;
				}
				string text3 = base.name;
				string message = "[" + text3 + "] DrinkCoffee failed — could not resolve camera with tag '" + cameraTag + "'.";
				Debug.LogWarning(message, this);
			}
			else if (debugLog)
			{
				text = base.name;
				text2 = "] DrinkCoffee ignored — cup is empty.";
				goto IL_033d;
			}
		}
		else if (debugLog)
		{
			text = base.name;
			text2 = "] DrinkCoffee ignored — already animating.";
			goto IL_033d;
		}
		goto IL_02eb;
		IL_033d:
		string message2 = "[" + text + text2;
		Debug.Log(message2, this);
		goto IL_02eb;
		IL_02eb:
		if (OnDrinkFailed != null)
		{
			OnDrinkFailed.Invoke();
		}
	}

	private IEnumerator DrinkRoutine()
	{
		_003CDrinkRoutine_003Ed__31 obj = new _003CDrinkRoutine_003Ed__31(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private IEnumerator AnimateIn(Vector3 fromLocalPos, Quaternion fromLocalRot, Vector3 fromWorldScale, float duration)
	{
		//IL_0021: Expected O, but got F4
		//IL_003d: Expected O, but got F4
		//IL_004f: Expected O, but got F4
		_003CAnimateIn_003Ed__32 obj = new _003CAnimateIn_003Ed__32(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.fromLocalPos = (Vector3)fromLocalPos.x;
			_ = fromLocalPos.z;
			obj.fromLocalRot = (Quaternion)fromLocalRot.x;
			obj.fromWorldScale = (Vector3)fromWorldScale.x;
			_ = fromWorldScale.z;
			float duration2 = default(float);
			obj.duration = duration2;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private IEnumerator HoldWithTilt(float duration)
	{
		_003CHoldWithTilt_003Ed__33 obj = new _003CHoldWithTilt_003Ed__33(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.duration = duration;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private IEnumerator AnimateOut(Vector3 fromLocalPos, Quaternion fromLocalRot, Vector3 fromWorldScale, Vector3 toLocalPos, Quaternion toLocalRot, Vector3 toWorldScale, float duration)
	{
		//IL_0021: Expected O, but got F4
		//IL_003d: Expected O, but got F4
		_003CAnimateOut_003Ed__34 obj = new _003CAnimateOut_003Ed__34(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.fromLocalPos = (Vector3)fromLocalPos.x;
			_ = fromLocalPos.z;
			obj.fromWorldScale = (Vector3)fromWorldScale.x;
			_ = fromWorldScale.z;
			object toLocalPos2 = default(object);
			obj.toLocalPos = (Vector3)toLocalPos2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ stack_28+8]");
			_ = 0;
			object toLocalRot2 = default(object);
			obj.toLocalRot = (Quaternion)toLocalRot2;
			object toWorldScale2 = default(object);
			obj.toWorldScale = (Vector3)toWorldScale2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ stack_38+8]");
			_ = 0;
			float duration2 = default(float);
			obj.duration = duration2;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private unsafe void ApplyAnchorWithTilt(float tiltT)
	{
		//IL_0022: Expected O, but got Ref
		//IL_004f: Expected O, but got Ref
		AnchorToLocal(out var _, out var _);
		Vector3 euler = default(Vector3);
		_resolvedTarget.localPosition = (Vector3)(&euler);
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		float num = default(float);
		_resolvedTarget.localRotation = (Quaternion)(&num);
	}

	private unsafe void AnchorToLocal(out Vector3 localPos, out Quaternion localRot)
	{
		//IL_00a7: Expected O, but got Ref
		//IL_00b8: Expected Ref, but got F4
		//IL_0056: Expected Ref, but got F4
		//IL_0081: Expected Ref, but got F4
		Transform parent = _resolvedTarget.parent;
		ref Vector3 reference;
		ref Quaternion reference2;
		if (!(parent != null))
		{
			Vector3 position = _drinkAnchor.position;
			reference = ref *(Vector3*)position.x;
			_ = position.z;
			reference2 = ref *(Quaternion*)_drinkAnchor.rotation.x;
			return;
		}
		Vector3 position2 = _drinkAnchor.position;
		object obj = default(object);
		Vector3 vector = parent.InverseTransformPoint((Vector3)(&obj));
		reference = ref *(Vector3*)vector.x;
		_ = vector.z;
		Quaternion rotation = parent.rotation;
		Quaternion rotation2 = default(Quaternion);
		Quaternion quaternion = Quaternion.Internal_Inverse(ref rotation2);
		Quaternion rotation3 = _drinkAnchor.rotation;
		object obj2 = default(object);
		reference2 = ref *(Quaternion*)obj2;
	}

	private unsafe void SnapToAnchorLocal()
	{
		//IL_0022: Expected O, but got Ref
		//IL_0036: Expected O, but got Ref
		AnchorToLocal(out var _, out var localRot);
		object obj = default(object);
		_resolvedTarget.localPosition = (Vector3)(&obj);
		_resolvedTarget.localRotation = (Quaternion)(&localRot);
	}

	private unsafe void ApplyWorldScale(Vector3 targetWorldScale)
	{
		//IL_0078: Invalid comparison between F4 and I4
		//IL_0105: Invalid comparison between F4 and I4
		//IL_0139: Expected O, but got I4
		//IL_0149: Expected O, but got I
		//IL_012b: Expected O, but got Ref
		if (!(_resolvedScaleTarget != null))
		{
			return;
		}
		Transform parent = _resolvedScaleTarget.parent;
		Transform transform = default(Transform);
		float num = default(float);
		if (parent != null)
		{
			Vector3 lossyScale = parent.lossyScale;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018053B410h\"");
			if (lossyScale.x == 0f)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018053B42Ah\"");
			object obj = default(object);
			if (obj == null)
			{
			}
			bool flag = lossyScale.z == 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018053B43Fh\"");
			if (flag)
			{
				object obj2 = 240;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ rax_v14+this @ rcx (EspressoCupDrinker)]");
				transform = (Transform)0;
				float num2 = default(float);
				num = num2;
			}
		}
		else
		{
			transform = _resolvedScaleTarget;
			num = targetWorldScale.x;
		}
		transform.localScale = (Vector3)(&num);
	}

	private unsafe void BuildDrinkAnchor()
	{
		//IL_00b1: Expected O, but got Ref
		//IL_00d6: Expected O, but got Ref
		//IL_00ea: Expected O, but got Ref
		//IL_0149: Expected O, but got F4
		if (_drinkAnchor == null)
		{
			string text = base.name;
			string text2 = "[DrinkAnchor] " + text;
			GameObject gameObject = new GameObject(text2);
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			Transform drinkAnchor = gameObject.transform;
			_drinkAnchor = drinkAnchor;
			_drinkAnchor.SetParent(_cameraTransform, worldPositionStays: false);
			Vector3 euler = default(Vector3);
			_drinkAnchor.localPosition = (Vector3)(&euler);
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			Vector3 vector = default(Vector3);
			_drinkAnchor.localRotation = (Quaternion)(&vector);
			_drinkAnchor.localScale = (Vector3)(&euler);
			if (debugLog)
			{
				string text3 = base.name;
				string text4 = _cameraTransform.name;
				object arg = euler;
				float num = default(float);
				object arg2 = (Vector3)num;
				string text5 = $"LocalPos={arg} LocalRot={arg2}";
				string message = "[" + text3 + "] Drink anchor built under '" + text4 + "'. " + text5;
				Debug.Log(message, this);
			}
		}
	}

	private bool TryResolveCamera()
	{
		//IL_039b: Expected I4, but got O
		if (!(_cameraTransform == null))
		{
			goto IL_020c;
		}
		if (!string.IsNullOrWhiteSpace(cameraTag))
		{
			GameObject gameObject = GameObject.FindWithTag(cameraTag);
			if (gameObject != null)
			{
				Transform cameraTransform = gameObject.transform;
				_cameraTransform = cameraTransform;
				if (!debugLog)
				{
					goto IL_020c;
				}
				string[] array = new string[5];
				if (array.Length > 0)
				{
					array[0] = "[";
					string text = base.name;
					if (array.Length > 1)
					{
						array[1] = text;
						if (array.Length > 2)
						{
							array[2] = "] Camera resolved: '";
							string text2 = gameObject.name;
							if (array.Length > 3)
							{
								array[3] = text2;
								if (array.Length > 4)
								{
									array[4] = "'.";
									string message = string.Concat(array);
									Debug.Log(message, this);
									goto IL_020c;
								}
							}
						}
					}
				}
			}
			else
			{
				if (!debugLog)
				{
					goto IL_0382;
				}
				string[] array2 = new string[5];
				if (array2.Length > 0)
				{
					array2[0] = "[";
					string text3 = base.name;
					if (array2.Length > 1)
					{
						array2[1] = text3;
						if (array2.Length > 2)
						{
							array2[2] = "] No GameObject found with tag '";
							if (array2.Length > 3)
							{
								array2[3] = cameraTag;
								if (array2.Length > 4)
								{
									array2[4] = "'.";
									string message2 = string.Concat(array2);
									Debug.LogWarning(message2, this);
									goto IL_0382;
								}
							}
						}
					}
				}
			}
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (byte)(int)ex != 0;
		}
		goto IL_0382;
		IL_020c:
		return true;
		IL_0382:
		return false;
	}

	public EspressoCupDrinker()
	{
		//IL_00f0: Expected I, but got O
		cameraTag = "MainCamera";
		Vector3 vector = default(Vector3);
		drinkPositionOffset = vector;
		drinkRotationOffset = vector;
		_ = 0;
		_ = 0.45f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		drinkScale = Vector3.oneVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rcx_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		_ = 0;
		animateInDuration = 0.35f;
		drinkHoldDuration = 1.2f;
		animateOutDuration = 0.4f;
		AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		easingCurve = animationCurve;
		AnimationCurve animationCurve2 = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		tiltCurve = animationCurve2;
		drinkTiltOffset = vector;
		_ = 0;
		OnDrinkTriggered = new UnityEvent();
		OnDrinkStarted = new UnityEvent();
		OnDrinkEmptied = new UnityEvent();
		OnDrinkCompleted = new UnityEvent();
		OnDrinkFailed = new UnityEvent();
		base._002Ector();
	}
}
