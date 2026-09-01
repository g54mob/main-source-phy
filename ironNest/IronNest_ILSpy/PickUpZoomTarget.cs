using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PickUpZoomTarget : MonoBehaviour
{
	public enum FocusScaleMode
	{
		MultiplyOriginal,
		SetAbsolute
	}

	public enum ReleaseBehavior
	{
		ReturnToOriginal,
		KeepCurrentWorldPose,
		UseReleaseTagWithOffsets
	}

	public enum DropTriggerMode
	{
		UseDropActionReferences,
		UseAnyButtonActionInAsset
	}

	private sealed class _003CMoveToFocus_003Ed__60 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PickUpZoomTarget _003C_003E4__this;

		private Vector3 _003CstartPos_003E5__2;

		private Quaternion _003CstartRot_003E5__3;

		private Vector3 _003CstartScale_003E5__4;

		private Vector3 _003CtargetScale_003E5__5;

		private float _003Celapsed_003E5__6;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CMoveToFocus_003Ed__60(int _003C_003E1__state)
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
			//IL_003b: Expected I4, but got I8
			//IL_0677: Expected I4, but got I8
			//IL_13bb: Expected I4, but got O
			//IL_06a8: Invalid comparison between I and F4
			//IL_0071: Expected O, but got I
			//IL_0cda: Expected O, but got I
			//IL_06d6: Expected O, but got I
			//IL_00cb: Expected O, but got F4
			//IL_10d3: Expected O, but got I
			//IL_070a: Invalid comparison between I4 and F4
			//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d39: Expected O, but got Unknown
			//IL_0d68: Expected O, but got I
			//IL_0755: Expected F4, but got I4
			//IL_0122: Expected O, but got F4
			//IL_1136: Expected O, but got I
			//IL_0d92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d97: Expected O, but got Unknown
			//IL_076f: Expected O, but got I
			//IL_077c: Invalid comparison between I4 and F4
			//IL_016f: Expected O, but got F4
			//IL_07cf: Expected F4, but got I4
			//IL_1170: Unknown result type (might be due to invalid IL or missing references)
			//IL_1175: Expected O, but got Unknown
			//IL_0e02: Expected O, but got I
			//IL_0e0f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e14: Expected Ref, but got Unknown
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ac: Expected O, but got Unknown
			//IL_01db: Expected O, but got I
			//IL_11b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_11be: Expected Ref, but got Unknown
			//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e2: Expected O, but got Unknown
			//IL_0811: Expected O, but got I
			//IL_0215: Expected O, but got I
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_0227: Expected Ref, but got Unknown
			//IL_1220: Unknown result type (might be due to invalid IL or missing references)
			//IL_1225: Expected O, but got Unknown
			//IL_1045: Unknown result type (might be due to invalid IL or missing references)
			//IL_104a: Expected O, but got Unknown
			//IL_0855: Expected O, but got I
			//IL_0862: Unknown result type (might be due to invalid IL or missing references)
			//IL_0867: Expected Ref, but got Unknown
			//IL_04fe: Expected O, but got I
			//IL_109e: Unknown result type (might be due to invalid IL or missing references)
			//IL_10a3: Expected O, but got Unknown
			//IL_04d9: Expected O, but got I
			//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af5: Expected O, but got Unknown
			//IL_0b29: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b2e: Expected Ref, but got Unknown
			//IL_0b37: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b3c: Expected Ref, but got Unknown
			//IL_055a: Unknown result type (might be due to invalid IL or missing references)
			//IL_055f: Expected O, but got Unknown
			//IL_04a7: Expected O, but got I
			//IL_04b7: Expected O, but got I
			//IL_1386: Unknown result type (might be due to invalid IL or missing references)
			//IL_138b: Expected O, but got Unknown
			//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b90: Expected O, but got Unknown
			//IL_0bdf: Expected O, but got I
			//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c9: Expected O, but got Unknown
			//IL_1315: Expected O, but got I
			//IL_1325: Expected F4, but got I
			//IL_0c2b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c30: Expected O, but got Unknown
			//IL_061d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0622: Expected O, but got Unknown
			object obj2 = default(object);
			object obj = obj2 - 95;
			Component component = _003C_003E4__this;
			Vector3 vector;
			Vector3 vector2 = default(Vector3);
			Vector3 vector3;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
					if (!((UnityEngine.Object)0 != null))
					{
						_ = 0;
						goto IL_1430;
					}
					Transform transform = _003C_003E4__this.transform;
					if ((object)transform != null)
					{
						Vector3 position = transform.position;
						_003CstartPos_003E5__2 = (Vector3)position.x;
						_ = position.z;
						Transform transform2 = _003C_003E4__this.transform;
						if ((object)transform2 != null)
						{
							_003CstartRot_003E5__3 = (Quaternion)transform2.rotation.x;
							Transform transform3 = _003C_003E4__this.transform;
							if ((object)transform3 != null)
							{
								Vector3 localScale = transform3.localScale;
								_003CstartScale_003E5__4 = (Vector3)localScale.x;
								_ = localScale.z;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
								if ((nint)0 != 0)
								{
									Vector3 position2 = (Vector3)(obj - 121);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+5C]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+64]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
									vector = ((Transform)0).TransformPoint(position2);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
										Quaternion rotation = ((Transform)0).rotation;
										ref Vector3 euler = ref *(Vector3*)(obj - 121);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+70]");
										float num = 0f * ((float)Math.PI / 180f);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+68]");
										_ = 0;
										Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
										float num2 = (float)vector2 * quaternion.x;
										float num3 = (float)vector2 * quaternion.x;
										float num4 = rotation.x * (float)vector2;
										object obj3 = (object)vector2 * (object)vector2;
										float num5 = num4 + num2;
										object obj4 = (object)vector2 * (object)vector2;
										object obj5 = (object)vector2 * (object)vector2;
										float num6 = num5 + (float)obj4;
										object obj6 = (object)vector2 * (object)vector2;
										object obj7 = (object)vector2 * (object)vector2;
										float num7 = num6 - (float)obj6;
										object obj8 = (object)vector2 * (object)vector2;
										object obj9 = obj3 + obj8;
										float num8 = rotation.x * (float)vector2;
										float num9 = (float)obj9 + num3;
										float num10 = rotation.x * quaternion.x;
										float num11 = rotation.x * (float)vector2;
										float num12 = num9 - num8;
										object obj10 = (object)vector2 * (object)vector2;
										object obj11 = (object)vector2 * (object)vector2;
										object obj12 = obj5 + obj10;
										float num13 = (float)vector2 * quaternion.x;
										float num14 = (float)obj11 - num10;
										object obj13 = (object)vector2 * (object)vector2;
										float num15 = (float)obj12 + num11;
										float num16 = num14 - (float)obj13;
										float num17 = num15 - num13;
										_ = _003CstartScale_003E5__4;
										float num18 = num16 - (float)obj7;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+74]");
										if ((nint)0 != 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+78]");
											object obj14;
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+78]");
												if ((nint)0 == 1)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+7C]");
													vector3 = (Vector3)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+84]");
													obj14 = 0;
													goto IL_13e7;
												}
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+130]");
											nint num19 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+84]");
											object obj15 = num19 * 0;
											vector3 = vector2;
											obj14 = obj15;
										}
										else
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PickUpZoomTarget+<MoveToFocus>d__60)+4C]");
											object obj14 = 0;
											vector3 = vector2;
										}
										goto IL_13e7;
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_1430;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_0696;
				}
			}
			goto IL_13ad;
			IL_13ad:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_148d:
			Transform transform4;
			if ((object)transform4 != null)
			{
				Vector3 localScale2 = (Vector3)(obj - 105);
				transform4.localScale = localScale2;
				goto IL_13a2;
			}
			goto IL_13ad;
			IL_1430:
			return false;
			IL_10c1:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
			if (!((UnityEngine.Object)0 != null))
			{
				goto IL_13a2;
			}
			Transform transform5 = _003C_003E4__this.transform;
			if ((object)transform5 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
				transform5.SetParent((Transform)0, worldPositionStays: true);
				Transform transform6 = _003C_003E4__this.transform;
				if ((object)transform6 != null)
				{
					Vector3 localPosition = (Vector3)(obj - 105);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+64]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+5C]");
					_ = 0;
					transform6.localPosition = localPosition;
					Transform transform7 = _003C_003E4__this.transform;
					ref Vector3 euler2 = ref *(Vector3*)(obj - 105);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+70]");
					float num20 = 0f * ((float)Math.PI / 180f);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+68]");
					_ = 0;
					Quaternion quaternion2 = Quaternion.Internal_FromEulerRad(ref euler2);
					if ((object)transform7 != null)
					{
						Quaternion localRotation = (Quaternion)(obj - 89);
						_ = quaternion2.x;
						transform7.localRotation = localRotation;
						transform4 = _003C_003E4__this.transform;
						Transform transform8 = _003C_003E4__this.transform;
						if ((object)transform8 != null)
						{
							Vector3 localScale3 = transform8.localScale;
							_ = localScale3.x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+74]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+78]");
								Vector3 vector4;
								float num21;
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+78]");
									if ((nint)0 == 1)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+7C]");
										vector4 = (Vector3)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+84]");
										num21 = 0f;
										goto IL_148d;
									}
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+130]");
								float num22 = 0f;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+84]");
								float num23 = num22 * 0f;
								vector4 = vector2;
								num21 = num23;
							}
							else
							{
								Vector3 vector4 = vector2;
								float num21 = localScale3.z;
							}
							goto IL_148d;
						}
					}
				}
			}
			goto IL_13ad;
			IL_0696:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+C0]");
			if (0f > _003Celapsed_003E5__6)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
				if (!((UnityEngine.Object)0 == null))
				{
					float num24 = _003Celapsed_003E5__6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+C0]");
					float num25 = num24 / 0f;
					if (!(0f > num25))
					{
						if (num25 > 1f)
						{
							num25 = 1f;
						}
					}
					else
					{
						num25 = 0f;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+C8]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+C8]");
						float num26 = ((AnimationCurve)0).Evaluate(num25);
						float num27;
						if (!(0f > num26))
						{
							bool flag = !(num26 > 1f);
							num27 = num26;
							if (!flag)
							{
								num27 = 1f;
							}
						}
						else
						{
							num27 = 0f;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
						if ((nint)0 != 0)
						{
							Vector3 position3 = (Vector3)(obj - 121);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+5C]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+64]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
							Vector3 vector5 = ((Transform)0).TransformPoint(position3);
							_ = vector5.x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
								Quaternion rotation2 = ((Transform)0).rotation;
								ref Vector3 euler3 = ref *(Vector3*)(obj - 121);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+70]");
								float num28 = 0f * ((float)Math.PI / 180f);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+68]");
								_ = 0;
								Quaternion quaternion3 = Quaternion.Internal_FromEulerRad(ref euler3);
								float num29 = (float)vector2 * quaternion3.x;
								float num30 = rotation2.x * (float)vector2;
								float num31 = num30 + num29;
								object obj16 = (object)vector2 * (object)vector2;
								object obj17 = (object)vector2 * (object)vector2;
								object obj18 = (object)vector2 * (object)vector2;
								float num32 = num31 + (float)obj18;
								object obj19 = (object)vector2 * (object)vector2;
								float num33 = num32 - (float)obj19;
								object obj20 = (object)vector2 * (object)vector2;
								object obj21 = obj16 + obj20;
								float num34 = rotation2.x * (float)vector2;
								float num35 = (float)vector2 * quaternion3.x;
								object obj22 = (object)vector2 * (object)vector2;
								float num36 = (float)obj21 + num35;
								float num37 = rotation2.x * quaternion3.x;
								float num38 = rotation2.x * (float)vector2;
								float num39 = num36 - num34;
								object obj23 = (object)vector2 * (object)vector2;
								object obj24 = (object)vector2 * (object)vector2;
								object obj25 = obj17 + obj23;
								object obj26 = (object)vector2 * (object)vector2;
								float num40 = (float)obj24 - num37;
								float num41 = (float)vector2 * quaternion3.x;
								float num42 = (float)obj25 + num38;
								float num43 = num40 - (float)obj26;
								float num44 = num42 - num41;
								float num45 = num43 - (float)obj22;
								Transform transform9 = _003C_003E4__this.transform;
								_ = _003CstartPos_003E5__2;
								float num46 = vector5.z;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PickUpZoomTarget+<MoveToFocus>d__60)+30]");
								float num47 = num46 - 0f;
								float num48 = num47 * num27;
								float num49 = num48;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PickUpZoomTarget+<MoveToFocus>d__60)+30]");
								float num50 = num49 + 0f;
								if ((object)transform9 != null)
								{
									Vector3 position4 = (Vector3)(obj - 105);
									transform9.position = position4;
									Transform transform10 = _003C_003E4__this.transform;
									ref Quaternion b = ref *(Quaternion*)(obj - 89);
									ref Quaternion a = ref *(Quaternion*)(obj - 105);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-59]");
									_ = 0;
									_ = _003CstartRot_003E5__3;
									Quaternion quaternion4 = Quaternion.Internal_SlerpUnclamped(ref a, ref b, num27);
									if ((object)transform10 != null)
									{
										Quaternion rotation3 = (Quaternion)(obj - 89);
										_ = quaternion4.x;
										transform10.rotation = rotation3;
										Transform transform11 = _003C_003E4__this.transform;
										_ = _003CstartScale_003E5__4;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PickUpZoomTarget+<MoveToFocus>d__60)+58]");
										nint num51 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PickUpZoomTarget+<MoveToFocus>d__60)+4C]");
										object obj27 = num51 - 0;
										float num52 = (float)obj27 * num27;
										float num53 = num52;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PickUpZoomTarget+<MoveToFocus>d__60)+4C]");
										float num54 = num53 + 0f;
										if ((object)transform11 != null)
										{
											Vector3 localScale4 = (Vector3)(obj - 105);
											transform11.localScale = localScale4;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+D0]");
											float num55 = (((nint)0 == 0) ? Time.deltaTime : Time.unscaledDeltaTime);
											float num56 = num55 + _003Celapsed_003E5__6;
											_003C_003E2__current = null;
											_003Celapsed_003E5__6 = num56;
											_003C_003E1__state = 1;
											return true;
										}
									}
								}
							}
						}
					}
					goto IL_13ad;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
			Transform transform14;
			Vector3 localScale5;
			if ((UnityEngine.Object)0 != null)
			{
				Transform transform12 = _003C_003E4__this.transform;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
				if ((nint)0 != 0)
				{
					Vector3 position5 = (Vector3)(obj - 105);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+64]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+5C]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
					Vector3 vector6 = ((Transform)0).TransformPoint(position5);
					if ((object)transform12 != null)
					{
						Vector3 position6 = (Vector3)(obj - 105);
						_ = vector6.x;
						_ = vector6.z;
						transform12.position = position6;
						Transform transform13 = _003C_003E4__this.transform;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+148]");
							Quaternion rotation4 = ((Transform)0).rotation;
							ref Vector3 euler4 = ref *(Vector3*)(obj - 105);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+70]");
							float num57 = 0f * ((float)Math.PI / 180f);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+68]");
							_ = 0;
							Quaternion quaternion5 = Quaternion.Internal_FromEulerRad(ref euler4);
							float num58 = (float)vector2 * quaternion5.x;
							float num59 = rotation4.x * (float)vector2;
							object obj28 = (object)vector2 * (object)vector2;
							float num60 = num59 + num58;
							object obj29 = (object)vector2 * (object)vector2;
							object obj30 = (object)vector2 * (object)vector2;
							float num61 = num60 + (float)obj30;
							object obj31 = (object)vector2 * (object)vector2;
							float num62 = num61 - (float)obj31;
							object obj32 = (object)vector2 * (object)vector2;
							object obj33 = obj28 + obj32;
							float num63 = rotation4.x * (float)vector2;
							float num64 = (float)vector2 * quaternion5.x;
							object obj34 = (object)vector2 * (object)vector2;
							float num65 = (float)obj33 + num64;
							float num66 = rotation4.x * quaternion5.x;
							float num67 = rotation4.x * (float)vector2;
							float num68 = num65 - num63;
							object obj35 = (object)vector2 * (object)vector2;
							object obj36 = (object)vector2 * (object)vector2;
							object obj37 = obj29 + obj35;
							object obj38 = (object)vector2 * (object)vector2;
							float num69 = (float)vector2 * quaternion5.x;
							float num70 = (float)obj36 - num66;
							float num71 = (float)obj37 + num67;
							float num72 = num70 - (float)obj38;
							float num73 = num71 - num69;
							float num74 = num72 - (float)obj34;
							if ((object)transform13 != null)
							{
								Quaternion rotation5 = (Quaternion)(obj - 89);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-59]");
								_ = 0;
								transform13.rotation = rotation5;
								transform14 = _003C_003E4__this.transform;
								if ((object)transform14 != null)
								{
									localScale5 = (Vector3)(obj - 105);
									_ = _003CtargetScale_003E5__5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PickUpZoomTarget+<MoveToFocus>d__60)+58]");
									_ = 0;
									goto IL_141e;
								}
							}
						}
					}
				}
				goto IL_13ad;
			}
			goto IL_10c1;
			IL_13a2:
			_ = 0;
			goto IL_1430;
			IL_141e:
			transform14.localScale = localScale5;
			goto IL_10c1;
			IL_13e7:
			_003CtargetScale_003E5__5 = vector3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rbx_v1 (UnityEngine.Component)+C0]");
			if ((nint)0 < (nint)0)
			{
				_003Celapsed_003E5__6 = 0f;
				goto IL_0696;
			}
			Transform transform15 = _003C_003E4__this.transform;
			if ((object)transform15 != null)
			{
				_ = vector.x;
				Vector3 position7 = (Vector3)(obj - 121);
				_ = vector.z;
				transform15.position = position7;
				Transform transform16 = _003C_003E4__this.transform;
				if ((object)transform16 != null)
				{
					Quaternion rotation6 = (Quaternion)(obj - 89);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-59]");
					_ = 0;
					transform16.rotation = rotation6;
					transform14 = _003C_003E4__this.transform;
					if ((object)transform14 != null)
					{
						localScale5 = (Vector3)(obj - 121);
						_ = _003CtargetScale_003E5__5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PickUpZoomTarget+<MoveToFocus>d__60)+58]");
						_ = 0;
						goto IL_141e;
					}
				}
			}
			goto IL_13ad;
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

	private sealed class _003CMoveToRelease_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PickUpZoomTarget _003C_003E4__this;

		private Transform _003CtargetParent_003E5__2;

		private Vector3 _003CtargetPos_003E5__3;

		private Quaternion _003CtargetRot_003E5__4;

		private Vector3 _003CtargetScale_003E5__5;

		private bool _003CsetLocalAfterParent_003E5__6;

		private bool _003CsetScaleAfterParent_003E5__7;

		private Vector3 _003CstartPos_003E5__8;

		private Quaternion _003CstartRot_003E5__9;

		private Vector3 _003CstartScale_003E5__10;

		private float _003Celapsed_003E5__11;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CMoveToRelease_003Ed__61(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0039: Expected I4, but got I8
			//IL_069b: Expected I4, but got I8
			//IL_06a9: Expected O, but got I4
			//IL_0073: Expected O, but got F4
			//IL_0bdc: Invalid comparison between I and F4
			//IL_00ab: Expected O, but got F4
			//IL_00d9: Expected O, but got F4
			//IL_06d4: Invalid comparison between O and F4
			//IL_047c: Expected O, but got I
			//IL_071f: Expected F4, but got I4
			//IL_0492: Expected O, but got I
			//IL_0129: Expected O, but got I
			//IL_094d: Expected O, but got Ref
			//IL_091d: Expected O, but got Ref
			//IL_0930: Expected O, but got F4
			//IL_0739: Expected O, but got I
			//IL_0745: Invalid comparison between O and F4
			//IL_04f8: Expected F4, but got I
			//IL_0508: Expected F4, but got I
			//IL_096d: Expected O, but got Ref
			//IL_0798: Expected F4, but got I4
			//IL_0b69: Expected O, but got F4
			//IL_0b80: Expected O, but got I
			//IL_0b92: Expected O, but got I
			//IL_04c5: Expected O, but got Ref
			//IL_04c5: Expected O, but got I
			//IL_03f5: Expected O, but got F4
			//IL_098d: Expected O, but got Ref
			//IL_0998: Expected O, but got I4
			//IL_09a9: Expected O, but got I4
			//IL_09b4: Expected O, but got I4
			//IL_042d: Expected O, but got F4
			//IL_045b: Expected O, but got F4
			//IL_0168: Expected O, but got I
			//IL_0805: Expected O, but got Ref
			//IL_07d6: Expected O, but got Ref
			//IL_07e9: Expected O, but got F4
			//IL_0620: Expected O, but got Ref
			//IL_0190: Expected O, but got I
			//IL_0640: Expected O, but got Ref
			//IL_0572: Expected O, but got F4
			//IL_0841: Expected O, but got Ref
			//IL_0660: Expected O, but got Ref
			//IL_05aa: Expected O, but got F4
			//IL_02d8: Expected O, but got Ref
			//IL_02eb: Expected O, but got F4
			//IL_022a: Expected O, but got I
			//IL_0860: Expected O, but got Ref
			//IL_05d8: Expected O, but got F4
			//IL_05f6: Expected O, but got I4
			//IL_0240: Expected O, but got I
			//IL_02a6: Expected F4, but got I
			//IL_02b6: Expected F4, but got I
			//IL_0c33: Expected O, but got Ref
			//IL_0a4d: Expected O, but got Ref
			//IL_0375: Expected F4, but got O
			//IL_0273: Expected O, but got Ref
			//IL_0273: Expected O, but got I
			//IL_0bae: Expected O, but got F4
			//IL_0a7f: Expected O, but got Ref
			Component component = _003C_003E4__this;
			Vector3 euler = default(Vector3);
			float num;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003CtargetParent_003E5__2 = null;
				Transform transform = component.transform;
				Vector3 position = transform.position;
				_003CtargetPos_003E5__3 = (Vector3)position.x;
				_ = position.z;
				Transform transform2 = component.transform;
				_003CtargetRot_003E5__4 = (Quaternion)transform2.rotation.x;
				Transform transform3 = component.transform;
				Vector3 localScale = transform3.localScale;
				_003CtargetScale_003E5__5 = (Vector3)localScale.x;
				_ = localScale.z;
				_003CsetLocalAfterParent_003E5__6 = false;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+88]");
				bool flag = (nint)0 == 0;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+88]");
					object obj = -1;
					if (!flag)
					{
						if ((nint)obj == 1)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+90]");
							bool flag2 = string.IsNullOrWhiteSpace((string)0);
							UnityEngine.Object obj2 = null;
							if (!flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+90]");
								GameObject gameObject = GameObject.FindWithTag((string)0);
								bool flag3 = gameObject != null;
								bool flag4 = !flag3;
								obj2 = null;
								if (!flag4)
								{
									if ((object)gameObject == null)
									{
										throw new NullReferenceException();
									}
									Transform transform4 = gameObject.transform;
									obj2 = transform4;
								}
							}
							if (!(obj2 != null))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+120]");
								_003CtargetParent_003E5__2 = (Transform)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+120]");
								if ((bool)(UnityEngine.Object)0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+120]");
									Vector3 vector = ((Transform)0).TransformPoint((Vector3)(&euler));
									num = vector.x;
									float z = vector.z;
								}
								else
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+100]");
									num = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+108]");
									float z = 0f;
								}
								goto IL_0b5f;
							}
							_003CtargetParent_003E5__2 = (Transform)obj2;
							Vector3 vector2 = ((Transform)obj2).TransformPoint((Vector3)(&euler));
							_003CtargetPos_003E5__3 = (Vector3)vector2.x;
							_ = vector2.z;
							Quaternion rotation = ((Transform)obj2).rotation;
							Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
							Vector3 vector3 = default(Vector3);
							_003CtargetRot_003E5__4 = (Quaternion)vector3;
							_003CsetLocalAfterParent_003E5__6 = true;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+B0]");
							float num4;
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+130]");
								float num2 = 0f;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+BC]");
								float num3 = num2 * 0f;
								num4 = (float)vector3;
								float num5 = num3;
							}
							else
							{
								Transform transform5 = component.transform;
								Vector3 localScale2 = transform5.localScale;
								num4 = localScale2.x;
								float num5 = localScale2.z;
							}
							_003CtargetScale_003E5__5 = (Vector3)num4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+B0]");
							_003CsetScaleAfterParent_003E5__7 = false;
						}
					}
					else
					{
						_003CtargetParent_003E5__2 = null;
						Transform transform6 = component.transform;
						Vector3 position2 = transform6.position;
						_003CtargetPos_003E5__3 = (Vector3)position2.x;
						_ = position2.z;
						Transform transform7 = component.transform;
						_003CtargetRot_003E5__4 = (Quaternion)transform7.rotation.x;
						Transform transform8 = component.transform;
						Vector3 localScale3 = transform8.localScale;
						_003CtargetScale_003E5__5 = (Vector3)localScale3.x;
						_ = localScale3.z;
					}
					goto IL_0c38;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+120]");
				_003CtargetParent_003E5__2 = (Transform)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+120]");
				if ((bool)(UnityEngine.Object)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+120]");
					Vector3 vector4 = ((Transform)0).TransformPoint((Vector3)(&euler));
					num = vector4.x;
					float z = vector4.z;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+100]");
					num = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+108]");
					float z = 0f;
				}
				goto IL_0b5f;
			}
			if (_003C_003E1__state != 1)
			{
				goto IL_0b02;
			}
			_003C_003E1__state = -1;
			object obj3 = 0;
			Transform transform9 = null;
			goto IL_0bca;
			IL_0b5f:
			_003CtargetPos_003E5__3 = (Vector3)num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+10C]");
			_003CtargetRot_003E5__4 = (Quaternion)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+128]");
			_003CtargetScale_003E5__5 = (Vector3)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+130]");
			_ = 0;
			goto IL_0c38;
			IL_0b02:
			return false;
			IL_09bf:
			if (_003CtargetParent_003E5__2 != null)
			{
				Transform transform10 = component.transform;
				transform10.SetParent(_003CtargetParent_003E5__2, worldPositionStays: true);
				if (_003CsetLocalAfterParent_003E5__6)
				{
					Transform transform11 = component.transform;
					transform11.localPosition = (Vector3)(&euler);
					Transform transform12 = component.transform;
					Quaternion quaternion2 = Quaternion.Internal_FromEulerRad(ref euler);
					float num6 = default(float);
					transform12.localRotation = (Quaternion)(&num6);
				}
				Transform transform13;
				if (!_003CsetScaleAfterParent_003E5__7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+88]");
					if ((nint)0 != 0)
					{
						goto IL_0b02;
					}
					transform13 = component.transform;
				}
				else
				{
					transform13 = component.transform;
				}
				transform13.localScale = (Vector3)(&euler);
			}
			goto IL_0b02;
			IL_0c38:
			Transform transform14 = component.transform;
			transform14.SetParent(null, worldPositionStays: true);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+C0]");
			if ((nint)0 < (nint)0)
			{
				Transform transform15 = component.transform;
				Vector3 position3 = transform15.position;
				_003CstartPos_003E5__8 = (Vector3)position3.x;
				_ = position3.z;
				Transform transform16 = component.transform;
				_003CstartRot_003E5__9 = (Quaternion)transform16.rotation.x;
				Transform transform17 = component.transform;
				Vector3 localScale4 = transform17.localScale;
				_003CstartScale_003E5__10 = (Vector3)localScale4.x;
				_ = localScale4.z;
				_003Celapsed_003E5__11 = 0f;
				obj3 = 0;
				transform9 = null;
				goto IL_0bca;
			}
			Transform transform18 = component.transform;
			transform18.position = (Vector3)(&euler);
			Transform transform19 = component.transform;
			Quaternion quaternion3 = default(Quaternion);
			transform19.rotation = (Quaternion)(&quaternion3);
			Transform transform20 = component.transform;
			transform20.localScale = (Vector3)(&euler);
			transform9 = null;
			goto IL_09bf;
			IL_0bca:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+C0]");
			if (0f > _003Celapsed_003E5__11)
			{
				float num7 = _003Celapsed_003E5__11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+C0]");
				float num8 = num7 / 0f;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num8))
				{
					if (num8 > 1f)
					{
						num8 = 1f;
					}
				}
				else
				{
					num8 = 0f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+C8]");
				float num9 = ((AnimationCurve)0).Evaluate(num8);
				float t;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num9))
				{
					bool flag5 = !(num9 > 1f);
					t = num9;
					if (!flag5)
					{
						t = 1f;
					}
				}
				else
				{
					t = 0f;
				}
				if (_003CtargetParent_003E5__2 != null)
				{
					Vector3 vector5 = _003CtargetParent_003E5__2.TransformPoint((Vector3)(&euler));
					_003CtargetPos_003E5__3 = (Vector3)vector5.x;
					_ = vector5.z;
				}
				Transform transform21 = component.transform;
				transform21.position = (Vector3)(&euler);
				Transform transform22 = component.transform;
				Quaternion a = default(Quaternion);
				Quaternion b = default(Quaternion);
				Quaternion quaternion4 = Quaternion.Internal_SlerpUnclamped(ref a, ref b, t);
				float num10 = default(float);
				transform22.rotation = (Quaternion)(&num10);
				Transform transform23 = component.transform;
				transform23.localScale = (Vector3)(&euler);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdi_v1 (UnityEngine.Component)+D0]");
				float num11 = (((nint)0 == 0) ? Time.deltaTime : Time.unscaledDeltaTime);
				float num12 = num11 + _003Celapsed_003E5__11;
				_003Celapsed_003E5__11 = num12;
				_003C_003E2__current = transform9;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003CtargetParent_003E5__2 != null)
			{
				Vector3 vector6 = _003CtargetParent_003E5__2.TransformPoint((Vector3)(&euler));
				_003CtargetPos_003E5__3 = (Vector3)vector6.x;
				_ = vector6.z;
			}
			Transform transform24 = component.transform;
			transform24.position = (Vector3)(&euler);
			Transform transform25 = component.transform;
			Quaternion quaternion5 = default(Quaternion);
			transform25.rotation = (Quaternion)(&quaternion5);
			Transform transform26 = component.transform;
			transform26.localScale = (Vector3)(&euler);
			_003CstartPos_003E5__8 = (Vector3)0;
			_ = 0;
			_003CstartRot_003E5__9 = (Quaternion)0;
			_003CstartScale_003E5__10 = (Vector3)0;
			_ = 0;
			goto IL_09bf;
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

	private string focusRootTag;

	private bool createAnchorUnderFocusRoot;

	private string focusAnchorName;

	private Vector3 focusAnchorLocalPosition;

	private Vector3 focusAnchorLocalRotation;

	private Vector3 focusAnchorLocalScale;

	public Vector3 positionOffset;

	public Vector3 rotationOffset;

	private bool applyScaleOnFocus;

	private FocusScaleMode focusScaleMode;

	public Vector3 focusScaleMultiplier;

	private ReleaseBehavior releaseMode;

	private string releaseTargetTag;

	public Vector3 releasePositionOffset;

	public Vector3 releaseRotationOffset;

	private bool applyScaleOnRelease;

	public Vector3 releaseScaleMultiplier;

	public float moveDuration;

	public AnimationCurve easing;

	public bool useUnscaledTime;

	private DropTriggerMode dropTriggerMode;

	private InputActionReference[] dropActionReferences;

	private InputActionAsset dropAnyActionAsset;

	private bool includeExpectedControlTypeButtonAsButton;

	private bool onlySubscribeEnabledActions;

	public bool resolveOnAwake;

	public bool resolveIfMissingOnPickUp;

	public UnityEvent onPickedUp;

	public UnityEvent onReleased;

	private Vector3 originalLocalPosition;

	private Quaternion originalRotation;

	private Transform originalParent;

	private Vector3 originalLocalScale;

	private bool isHeld;

	private Coroutine moveCoroutine;

	private Transform resolvedFocusRoot;

	private Transform resolvedFocus;

	private readonly List<InputAction> dropSubscribedActions;

	public bool IsHeld => isHeld;

	private bool IsMoving
	{
		get
		{
			bool flag = (nint)moveCoroutine < 0;
			bool flag2 = moveCoroutine == null;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	private float DeltaTime
	{
		get
		{
			if (useUnscaledTime)
			{
				return Time.unscaledDeltaTime;
			}
			return Time.deltaTime;
		}
	}

	public void SetDropTriggerMode(DropTriggerMode mode)
	{
		dropTriggerMode = mode;
		if (base.isActiveAndEnabled)
		{
			UnsubscribeDropActions();
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 26 Invalid \"Jump target not found in method: 0x180417010\"");
		}
	}

	public void SetDropActionReferences(InputActionReference[] actions)
	{
		bool flag = actions != null;
		InputActionReference[] array = actions;
		if (!flag)
		{
			InputActionReference[] array2 = new InputActionReference[0];
			array = array2;
		}
		dropActionReferences = array;
		if (base.isActiveAndEnabled)
		{
			UnsubscribeDropActions();
			SubscribeDropActions();
		}
	}

	public void SetDropAnyActionAsset(InputActionAsset asset)
	{
		dropAnyActionAsset = asset;
		if (base.isActiveAndEnabled)
		{
			UnsubscribeDropActions();
			SubscribeDropActions();
		}
	}

	private void Awake()
	{
		CaptureOriginalState();
		if (resolveOnAwake)
		{
			bool flag = TryResolveFocus(out var _, out var _);
		}
	}

	private void CaptureOriginalState()
	{
		//IL_002b: Expected O, but got F4
		//IL_0060: Expected O, but got F4
		//IL_00b6: Expected O, but got F4
		Transform transform = base.transform;
		Vector3 localPosition = transform.localPosition;
		originalLocalPosition = (Vector3)localPosition.x;
		_ = localPosition.z;
		Transform transform2 = base.transform;
		originalRotation = (Quaternion)transform2.rotation.x;
		Transform transform3 = base.transform;
		Transform parent = transform3.parent;
		originalParent = parent;
		Transform transform4 = base.transform;
		Vector3 localScale = transform4.localScale;
		originalLocalScale = (Vector3)localScale.x;
		_ = localScale.z;
	}

	private void OnEnable()
	{
		SubscribeDropActions();
	}

	private void OnDisable()
	{
		if (moveCoroutine != null)
		{
			StopCoroutine(moveCoroutine);
			moveCoroutine = null;
		}
		UnsubscribeDropActions();
	}

	public void PickUp()
	{
		if ((nint)moveCoroutine > 0 || isHeld)
		{
			return;
		}
		CaptureOriginalState();
		Transform transform;
		if (resolvedFocus != null)
		{
			bool flag = resolvedFocusRoot == null;
			bool flag2 = !flag;
			transform = null;
			if (flag2)
			{
				goto IL_00c3;
			}
		}
		bool flag3 = !resolveIfMissingOnPickUp;
		transform = null;
		if (!flag3)
		{
			bool flag4 = TryResolveFocus(out var _, out transform);
		}
		goto IL_00c3;
		IL_00c3:
		if (resolvedFocus != null)
		{
			isHeld = true;
			if (moveCoroutine != null)
			{
				StopCoroutine(moveCoroutine);
			}
			_003CMoveToFocus_003Ed__60 obj = new _003CMoveToFocus_003Ed__60(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
			moveCoroutine = coroutine;
			if (onPickedUp != null)
			{
				onPickedUp.Invoke();
			}
		}
		else
		{
			string text = base.name;
			string message = text + ": PickUp aborted — could not resolve focus by tag '" + focusRootTag + "'. Ensure the tagged object (e.g., MainCamera) is loaded and active.";
			Debug.LogWarning(message, this);
		}
	}

	public void Release()
	{
		if ((nint)moveCoroutine <= 0 && isHeld)
		{
			bool flag = moveCoroutine == null;
			isHeld = false;
			if (!flag)
			{
				StopCoroutine(moveCoroutine);
			}
			_003CMoveToRelease_003Ed__61 obj = new _003CMoveToRelease_003Ed__61(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
			moveCoroutine = coroutine;
			if (onReleased != null)
			{
				onReleased.Invoke();
			}
		}
	}

	public void TogglePickUp()
	{
		if ((nint)moveCoroutine <= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 25 Invalid \"Jump target not found in method: 0x180416AD0\"");
			Release();
		}
	}

	public unsafe void ResetToOriginalImmediate()
	{
		//IL_007b: Expected O, but got Ref
		//IL_0098: Expected O, but got Ref
		//IL_00b4: Expected O, but got Ref
		if (moveCoroutine != null)
		{
			StopCoroutine(moveCoroutine);
		}
		isHeld = false;
		Transform transform = base.transform;
		transform.SetParent(originalParent, worldPositionStays: true);
		Transform transform2 = base.transform;
		Vector3 vector = default(Vector3);
		transform2.localPosition = (Vector3)(&vector);
		Transform transform3 = base.transform;
		transform3.rotation = (Quaternion)(&vector);
		Transform transform4 = base.transform;
		Quaternion quaternion = default(Quaternion);
		transform4.localScale = (Vector3)(&quaternion);
	}

	public unsafe bool TryResolveFocus(out Transform focusRoot, out Transform focus)
	{
		//IL_029e: Expected I4, but got O
		//IL_01fd: Expected O, but got Ref
		//IL_021d: Expected O, but got Ref
		//IL_022b: Expected O, but got Ref
		ref Transform reference = ref *(Transform*)null;
		ref Transform reference2 = ref *(Transform*)null;
		bool flag = string.IsNullOrWhiteSpace(focusRootTag);
		UnityEngine.Object obj = null;
		if (!flag)
		{
			GameObject gameObject = GameObject.FindWithTag(focusRootTag);
			bool flag2 = gameObject != null;
			bool flag3 = !flag2;
			obj = null;
			if (!flag3)
			{
				if ((object)gameObject == null)
				{
					throw new NullReferenceException();
				}
				Transform transform = gameObject.transform;
				obj = transform;
			}
		}
		UnityEngine.Object obj2;
		if (obj != null)
		{
			reference = ref *(Transform*)obj;
			resolvedFocusRoot = (Transform)obj;
			if (!createAnchorUnderFocusRoot)
			{
				obj2 = obj;
				goto IL_029e;
			}
			if ((object)obj != null)
			{
				Transform transform2 = ((Transform)obj).Find(focusAnchorName);
				if (transform2 == null)
				{
					GameObject gameObject2 = new GameObject(focusAnchorName);
					if ((object)gameObject2 != null)
					{
						Transform transform3 = gameObject2.transform;
						if ((object)transform3 != null)
						{
							transform3.SetParent((Transform)obj, worldPositionStays: false);
							obj2 = transform3;
							goto IL_01ef;
						}
					}
				}
				else
				{
					bool flag4 = (object)transform2 == null;
					obj2 = transform2;
					if (!flag4)
					{
						goto IL_01ef;
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		resolvedFocusRoot = null;
		resolvedFocus = null;
		return false;
		IL_01ef:
		Vector3 euler = default(Vector3);
		((Transform)obj2).localPosition = (Vector3)(&euler);
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		float num = default(float);
		((Transform)obj2).localRotation = (Quaternion)(&num);
		((Transform)obj2).localScale = (Vector3)(&euler);
		goto IL_029e;
		IL_029e:
		reference2 = ref *(Transform*)obj2;
		resolvedFocus = focus;
		return true;
	}

	private unsafe Vector3 ComputeHeldLocalScale(Vector3 currentLocalScale)
	{
		//IL_0113: Expected native int or pointer, but got O
		//IL_0125: Expected native int or pointer, but got O
		//IL_00c3: Expected native int or pointer, but got O
		//IL_00ef: Expected native int or pointer, but got O
		//IL_00fc: Expected native int or pointer, but got O
		//IL_006f: Expected F4, but got O
		//IL_006a: Expected native int or pointer, but got O
		//IL_0084: Expected F4, but got I
		//IL_007f: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		if (applyScaleOnFocus)
		{
			if (focusScaleMode != FocusScaleMode.MultiplyOriginal && focusScaleMode == FocusScaleMode.SetAbsolute)
			{
				((Vector3*)(nint)vector)->x = (float)focusScaleMultiplier;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (PickUpZoomTarget)+84]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
			float x = (float)originalLocalScale * (float)focusScaleMultiplier;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (PickUpZoomTarget)+12C]");
			float num = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (PickUpZoomTarget)+80]");
			float y = num * 0f;
			((Vector3*)(nint)vector)->x = x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (PickUpZoomTarget)+130]");
			float num2 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (PickUpZoomTarget)+84]");
			float z = num2 * 0f;
			((Vector3*)(nint)vector)->y = y;
			((Vector3*)(nint)vector)->z = z;
			return vector;
		}
		((Vector3*)(nint)vector)->x = currentLocalScale.x;
		((Vector3*)(nint)vector)->z = currentLocalScale.z;
		return vector;
	}

	private IEnumerator MoveToFocus()
	{
		_003CMoveToFocus_003Ed__60 obj = new _003CMoveToFocus_003Ed__60(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator MoveToRelease()
	{
		_003CMoveToRelease_003Ed__61 obj = new _003CMoveToRelease_003Ed__61(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void SubscribeDropActions()
	{
		//IL_0181: Expected O, but got Ref
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0064: Expected O, but got I4
		//IL_01cc: Expected O, but got Ref
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		UnsubscribeDropActions();
		if (dropTriggerMode == DropTriggerMode.UseDropActionReferences)
		{
			if (dropActionReferences == null)
			{
				return;
			}
			InputActionReference[] array = dropActionReferences;
			if (array.Length == 0)
			{
				return;
			}
			object obj = array + 32;
			object obj2 = 0;
			while ((nint)obj2 < array.Length)
			{
				if ((UnityEngine.Object)obj != null)
				{
					InputAction action = ((InputActionReference)obj).action;
					if (action != null && !dropSubscribedActions.Contains(action))
					{
						Action<InputAction.CallbackContext> value = OnDropActionStarted;
						action.started += value;
						dropSubscribedActions.Add(action);
					}
				}
				obj2++;
				obj += 8;
			}
		}
		else
		{
			if (dropTriggerMode != DropTriggerMode.UseAnyButtonActionInAsset || !(dropAnyActionAsset != null))
			{
				return;
			}
			object obj3 = default(object);
			ReadOnlyArray<InputActionMap> actionMaps = ((InputActionAsset)(&obj3)).actionMaps;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA390");
			ReadOnlyArray<InputActionMap>.Enumerator enumerator = default(ReadOnlyArray<InputActionMap>.Enumerator);
			object obj4 = default(object);
			object obj5 = default(object);
			ReadOnlyArray<InputAction>.Enumerator enumerator2 = default(ReadOnlyArray<InputAction>.Enumerator);
			InputAction inputAction = default(InputAction);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18084D310");
				if (obj4 == null)
				{
					continue;
				}
				ReadOnlyArray<InputAction> actions = ((InputActionMap)(&obj5)).actions;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA390");
				while (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18084D310");
					if (inputAction == null)
					{
						continue;
					}
					if (dropSubscribedActions != null)
					{
						if (!dropSubscribedActions.Contains(inputAction) && IsButtonLike(inputAction) && (!onlySubscribeEnabledActions || inputAction.enabled))
						{
							Action<InputAction.CallbackContext> value2 = OnDropActionStarted;
							inputAction.started += value2;
							bool flag = dropSubscribedActions == null;
							List<InputAction> list = dropSubscribedActions;
							if (flag)
							{
								throw new NullReferenceException();
							}
							dropSubscribedActions.Add(inputAction);
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator2.Dispose();
			}
			enumerator.Dispose();
		}
	}

	private unsafe void UnsubscribeDropActions()
	{
		//IL_0192: Expected O, but got Ref
		if (dropSubscribedActions != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<InputAction>.Enumerator enumerator = default(List<InputAction>.Enumerator);
			InputAction inputAction = default(InputAction);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (inputAction != null)
				{
					Action<InputAction.CallbackContext> value = OnDropActionStarted;
					inputAction.started -= value;
				}
			}
			enumerator.Dispose();
			List<InputAction> list = dropSubscribedActions;
			bool flag = dropSubscribedActions == null;
			List<InputAction>.Enumerator enumerator2 = (List<InputAction>.Enumerator)(&enumerator);
			if (!flag)
			{
				int version = list._version + 1;
				list._version = version;
				((List<InputAction>.Enumerator*)null)->Dispose();
				object obj = default(object);
				if (obj == null)
				{
					list._size = 0;
					return;
				}
				list._size = 0;
				if (list._size > 0)
				{
					Array.Clear(list._items, 0, list._size);
				}
				return;
			}
		}
		throw new NullReferenceException();
	}

	private bool IsButtonLike(InputAction act)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A08F]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (act != null && (act.m_Type == InputActionType.Button || (includeExpectedControlTypeButtonAsButton && !string.IsNullOrWhiteSpace(act.m_ExpectedControlType) && string.Equals(act.m_ExpectedControlType, "Button", StringComparison.OrdinalIgnoreCase))))
		{
			return true;
		}
		return false;
	}

	private void OnDropActionStarted(InputAction.CallbackContext ctx)
	{
		if (isHeld)
		{
			Release();
		}
	}

	public PickUpZoomTarget()
	{
		//IL_007e: Expected I, but got O
		//IL_0200: Expected I, but got O
		//IL_00b9: Expected I, but got O
		//IL_023b: Expected I, but got O
		//IL_0104: Expected I, but got O
		//IL_014a: Expected I, but got O
		//IL_0281: Expected I, but got O
		//IL_0185: Expected I, but got O
		focusRootTag = "MainCamera";
		createAnchorUnderFocusRoot = true;
		focusAnchorName = "PickupFocusAnchor";
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v7 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		focusAnchorLocalPosition = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rcx_v5 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v10 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		focusAnchorLocalRotation = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rcx_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num5 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v101 @ rax_v13 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num6 = 0;
		focusAnchorLocalScale = Vector3.oneVector;
		Vector3 vector = default(Vector3);
		positionOffset = vector;
		_ = 0.7f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rcx_v9 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		_ = 0;
		nint num7 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ rax_v16 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num8 = 0;
		rotationOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rcx_v11 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		applyScaleOnFocus = true;
		nint num9 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rax_v19 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num10 = 0;
		focusScaleMultiplier = Vector3.oneVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v153 @ rcx_v13 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		_ = 0;
		releaseTargetTag = "";
		nint num11 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v175 @ rax_v24 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num12 = 0;
		releasePositionOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ rcx_v16 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num13 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v27 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num14 = 0;
		releaseRotationOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rdx_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		applyScaleOnRelease = true;
		nint num15 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ rax_v30 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num16 = 0;
		releaseScaleMultiplier = Vector3.oneVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ rcx_v19 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		_ = 0;
		moveDuration = 0.35f;
		AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		easing = animationCurve;
		InputActionReference[] array = new InputActionReference[0];
		dropActionReferences = array;
		includeExpectedControlTypeButtonAsButton = true;
		resolveOnAwake = true;
		dropSubscribedActions = new List<InputAction>();
		base._002Ector();
	}
}
