using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using Localisation;
using SleepyNodes;
using UnityEngine;

public class FireMission : MonoBehaviour
{
	public class TimerValue
	{
		public float InitialSeconds;

		public float CurrentSeconds;

		public double StartedAt;
	}

	private sealed class _003CInternal_MoveEntity_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FireMission _003C_003E4__this;

		public Vector3 worldPos;

		public MapEntity entity;

		public float timespan;

		public double endsAt;

		public double startedAt;

		private Vector3 _003CdesiredLocation_003E5__2;

		private Vector3 _003CstartingLocation_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CInternal_MoveEntity_003Ed__36(int _003C_003E1__state)
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
			//IL_0f36: Expected I4, but got I8
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Expected O, but got Unknown
			//IL_008c: Expected F8, but got I
			//IL_0096: Expected O, but got F8
			//IL_00ba: Expected I, but got O
			//IL_0f4e: Expected O, but got I4
			//IL_0f77: Expected O, but got I4
			//IL_00f4: Expected I, but got O
			//IL_010c: Expected O, but got I
			//IL_012b: Expected O, but got I
			//IL_0148: Expected I, but got O
			//IL_0160: Expected O, but got I
			//IL_0fe7: Expected O, but got I4
			//IL_018a: Expected O, but got F4
			//IL_0d03: Expected I, but got O
			//IL_1368: Expected I, but got O
			//IL_1385: Expected O, but got I
			//IL_13a2: Expected O, but got I
			//IL_0d2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d34: Expected O, but got Unknown
			//IL_0d3e: Expected F8, but got O
			//IL_0d97: Expected I, but got O
			//IL_10a7: Expected F4, but got I4
			//IL_105e: Invalid comparison between I4 and F4
			//IL_1070: Expected F4, but got I4
			//IL_024d: Expected I, but got O
			//IL_027a: Invalid comparison between F4 and I4
			//IL_0dde: Expected I, but got O
			//IL_0b6d: Expected I, but got O
			//IL_0b75: Expected O, but got I
			//IL_10c8: Expected F8, but got O
			//IL_10ff: Expected O, but got I
			//IL_111c: Expected O, but got I
			//IL_11a4: Expected I, but got O
			//IL_0c3f: Expected F8, but got I4
			//IL_0c50: Expected F8, but got I4
			//IL_029c: Invalid comparison between I4 and F4
			//IL_0e34: Expected I, but got O
			//IL_0b8d: Expected I, but got O
			//IL_0ba7: Expected F8, but got I4
			//IL_0bb4: Expected O, but got I
			//IL_0818: Expected I, but got O
			//IL_0820: Expected O, but got I
			//IL_11c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_11cd: Expected O, but got Unknown
			//IL_121c: Expected I, but got O
			//IL_08e2: Expected F8, but got I4
			//IL_02ce: Expected I, but got O
			//IL_0e7b: Expected O, but got F4
			//IL_0ebd: Expected I, but got O
			//IL_0bdd: Expected O, but got I
			//IL_0838: Expected I, but got O
			//IL_0852: Expected F8, but got I4
			//IL_085f: Expected O, but got I
			//IL_1263: Expected I, but got O
			//IL_0bf8: Expected O, but got I4
			//IL_0c06: Expected I, but got O
			//IL_090d: Expected F8, but got I4
			//IL_0edb: Expected O, but got I
			//IL_0888: Expected O, but got I
			//IL_1410: Expected I4, but got O
			//IL_12b9: Expected I, but got O
			//IL_0952: Expected F8, but got I4
			//IL_0957: Expected I, but got O
			//IL_08a3: Expected O, but got I4
			//IL_08b1: Expected I, but got O
			//IL_12f3: Expected O, but got F4
			//IL_097b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0980: Expected O, but got Unknown
			//IL_098a: Expected F8, but got O
			//IL_09cf: Expected F8, but got I4
			//IL_09dc: Expected I, but got O
			//IL_0a0f: Expected F8, but got I4
			//IL_0a1c: Expected I, but got O
			//IL_035a: Expected I, but got O
			//IL_038c: Expected F8, but got I4
			//IL_0a61: Expected F8, but got I4
			//IL_0a6b: Expected I, but got O
			//IL_0ab2: Expected O, but got F4
			//IL_0ae0: Expected F8, but got I4
			//IL_0aed: Expected I, but got O
			//IL_0445: Expected I, but got O
			//IL_0455: Expected O, but got I
			//IL_0477: Expected F8, but got I4
			//IL_0b0b: Expected O, but got I
			//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_04db: Expected O, but got Unknown
			//IL_0514: Expected I, but got O
			//IL_0524: Expected O, but got I
			//IL_0546: Expected F8, but got I4
			//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05aa: Expected O, but got Unknown
			//IL_05bd: Expected I4, but got O
			//IL_064f: Expected F8, but got O
			//IL_0658: Unknown result type (might be due to invalid IL or missing references)
			//IL_065d: Expected O, but got Unknown
			//IL_05e6: Expected I, but got O
			//IL_05f6: Expected O, but got I
			//IL_0618: Expected F8, but got I4
			//IL_0711: Expected F8, but got O
			//IL_071a: Unknown result type (might be due to invalid IL or missing references)
			//IL_071f: Expected O, but got Unknown
			//IL_06a8: Expected I, but got O
			//IL_06b8: Expected O, but got I
			//IL_06da: Expected F8, but got I4
			//IL_076a: Expected I, but got O
			//IL_077a: Expected O, but got I
			//IL_079c: Expected F8, but got I4
			//IL_07f8: Expected I, but got O
			FireMission fireMission = _003C_003E4__this;
			object obj2 = default(object);
			double num2 = default(double);
			EntityLocation entityLocation;
			string text6;
			nint num21;
			MapEntity mapEntity7;
			object obj37;
			double num22;
			double num23;
			double num4;
			_003CInternal_MoveEntity_003Ed__36 obj;
			object obj36 = default(object);
			double num;
			string text = default(string);
			nint num3 = default(nint);
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				bool flag = (object)_003C_003E4__this == null;
				obj = this;
				if (!flag)
				{
					Vector3 vector = (Vector3)(obj2 - 96);
					_ = worldPos;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+30]");
					_ = 0;
					Vector2 vector2 = _003C_003E4__this.ToLocalSpace(vector);
					obj = (_003CInternal_MoveEntity_003Ed__36)(object)entity;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+24]");
					num = 0.0;
					_003CdesiredLocation_003E5__2 = (Vector3)num2;
					_ = 0;
					bool flag2 = entity == null;
					text = null;
					num3 = (nint)vector;
					num4 = num2;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rcx_v14 (FireMission+<Internal_MoveEntity>d__36)+70]");
						bool flag3 = (nint)0 == 0;
						text = null;
						num3 = (nint)vector;
						num4 = num2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rcx_v14 (FireMission+<Internal_MoveEntity>d__36)+70]");
						obj = (_003CInternal_MoveEntity_003Ed__36)0;
						if (!flag3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rcx_v14 (FireMission+<Internal_MoveEntity>d__36)+70]");
							Transform transform = ((Component)0).transform;
							bool flag4 = (object)transform == null;
							text = null;
							num3 = unchecked((nint)null);
							num4 = num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rcx_v14 (FireMission+<Internal_MoveEntity>d__36)+70]");
							obj = (_003CInternal_MoveEntity_003Ed__36)0;
							if (!flag4)
							{
								Vector3 localPosition = transform.localPosition;
								_003CstartingLocation_003E5__3 = (Vector3)localPosition.x;
								_ = localPosition.x;
								_ = localPosition.z;
								_ = _003CdesiredLocation_003E5__2;
								nint num5 = (nint)typeof(Math);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-60]");
								nint num6 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-50]");
								object obj3 = num6 - 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-5C]");
								nint num7 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-4C]");
								object obj4 = num7 - 0;
								float num8 = localPosition.z;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+60]");
								float num9 = num8 - 0f;
								object obj5 = obj4 * obj4;
								object obj6 = obj3 * obj3;
								float num10 = num9 * num9;
								object obj7 = obj5 + obj6;
								float num11 = (float)obj7 + num10;
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v944 @ rcx_v42 (Il2CppClass<System.Math>)+E4]");
								if ((nint)0 <= (nint)0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
								}
								else
								{
									double num12 = Math.Sqrt(num11);
								}
								nint num13 = (nint)typeof(Mathf);
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rax_v51 (Il2CppClass<UnityEngine.Mathf>)+B8]");
								nint num14 = 0;
								num4 = Mathf.Epsilon;
								if (Mathf.Epsilon < 0f)
								{
									if (0f < timespan)
									{
										bool flag5 = (byte)(~(fireMission.DebugLogs ? 1u : 0u)) != 0;
										text = null;
										num3 = (nint)transform;
										if (!flag5)
										{
											object[] array = new object[6];
											MapEntity mapEntity = entity;
											if (entity != null && array != null)
											{
												if (mapEntity.ID != null)
												{
													nint num15 = (nint)array;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1405 @ rdx_v76 (Il2CppClass<System.Object[]>)+40]");
													num3 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
													object obj8 = default(object);
													bool flag6 = obj8 == null;
													num = 0.0;
													text = null;
													obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity.ID;
													if (flag6)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
														string text2 = default(string);
														throw text2;
													}
												}
												array[0] = mapEntity.ID;
												MapEntity mapEntity2 = entity;
												if (entity != null && mapEntity2.Name != null)
												{
													string text3 = mapEntity2.Name.Get();
													if (text3 != null)
													{
														nint num16 = (nint)array;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1567 @ rdx_v74 (Il2CppClass<System.Object[]>)+40]");
														object obj9 = 0;
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
														object obj10 = default(object);
														bool flag7 = obj10 == null;
														num = 0.0;
														text = null;
														string text4 = text3;
														if (flag7)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
															object obj11 = default(object);
															throw obj11;
														}
													}
													array[1] = text3;
													MapEntity mapEntity3 = entity;
													if (entity != null)
													{
														object obj12 = obj2 + 32;
														_ = mapEntity3.IDIndex;
														Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
														object obj13 = default(object);
														if (obj13 != null)
														{
															nint num17 = (nint)array;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1595 @ rdx_v72 (Il2CppClass<System.Object[]>)+40]");
															object obj14 = 0;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
															object obj15 = default(object);
															bool flag8 = obj15 == null;
															num = 0.0;
															text = null;
															object obj16 = obj13;
															if (flag8)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																object obj17 = default(object);
																throw obj17;
															}
														}
														array[2] = obj13;
														MapEntity mapEntity4 = entity;
														if (entity != null)
														{
															object obj18 = obj2 + 48;
															_ = mapEntity4.Role;
															object obj19 = (EntityRoles)obj18;
															if (obj19 != null)
															{
																nint num18 = (nint)array;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1622 @ rdx_v70 (Il2CppClass<System.Object[]>)+40]");
																object obj20 = 0;
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																object obj21 = default(object);
																bool flag9 = obj21 == null;
																num = 0.0;
																text = null;
																object obj22 = obj19;
																if (flag9)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																	object obj23 = default(object);
																	throw obj23;
																}
															}
															array[3] = obj19;
															num4 = (double)_003CstartingLocation_003E5__3;
															object obj24 = obj2 - 80;
															_ = _003CstartingLocation_003E5__3;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+6C]");
															_ = 0;
															object obj25 = (Vector3)obj24;
															if (obj25 != null)
															{
																nint num19 = (nint)array;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1651 @ rdx_v68 (Il2CppClass<System.Object[]>)+40]");
																object obj26 = 0;
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																object obj27 = default(object);
																bool flag10 = obj27 == null;
																num = 0.0;
																text = null;
																object obj28 = obj25;
																if (flag10)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																	object obj29 = default(object);
																	throw obj29;
																}
															}
															array[4] = obj25;
															num4 = (double)_003CdesiredLocation_003E5__2;
															object obj30 = obj2 - 96;
															_ = _003CdesiredLocation_003E5__2;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+60]");
															_ = 0;
															object obj31 = (Vector3)obj30;
															if (obj31 != null)
															{
																nint num20 = (nint)array;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1680 @ rdx_v66 (Il2CppClass<System.Object[]>)+40]");
																object obj32 = 0;
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																object obj33 = default(object);
																bool flag11 = obj33 == null;
																num = 0.0;
																text = null;
																object obj34 = obj31;
																if (flag11)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																	object obj35 = default(object);
																	throw obj35;
																}
															}
															array[5] = obj31;
															string message = string.Format("[ENTITY] Moving: {0} | {1}#{2} ({3}) | {4} -> {5}", array);
															Debug.Log(message);
															text = null;
															num3 = unchecked((nint)null);
															goto IL_13c3;
														}
													}
												}
											}
											NullReferenceException ex = new NullReferenceException();
											return (byte)(int)ex != 0;
										}
										goto IL_13c3;
									}
									bool flag12 = (byte)(~(fireMission.DebugLogs ? 1u : 0u)) != 0;
									text = null;
									num3 = (nint)transform;
									obj = (_003CInternal_MoveEntity_003Ed__36)num14;
									if (!flag12)
									{
										num3 = (nint)entity;
										bool flag13 = entity == null;
										num = 0.0;
										text = null;
										obj = (_003CInternal_MoveEntity_003Ed__36)num14;
										if (flag13)
										{
											goto IL_1322;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rdx_v16 (Il2CppMethodInfo)+10]");
										string text5 = "[ENTITY] Moving: " + (string)0 + " | Moved Instantly, no timespan";
										Debug.Log(text5);
										obj36 = 0;
										text = " | Moved Instantly, no timespan";
										num3 = unchecked((nint)null);
										obj = (_003CInternal_MoveEntity_003Ed__36)(object)text5;
									}
									MapEntity mapEntity5 = entity;
									bool flag14 = entity == null;
									num = 0.0;
									if (!flag14)
									{
										bool flag15 = (object)mapEntity5.Location == null;
										num = 0.0;
										obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity5.Location;
										if (!flag15)
										{
											Transform transform2 = mapEntity5.Location.transform;
											bool flag16 = (object)transform2 == null;
											num = 0.0;
											num3 = unchecked((nint)null);
											obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity5.Location;
											if (!flag16)
											{
												Vector3 vector3 = (Vector3)(obj2 - 80);
												num4 = (double)_003CdesiredLocation_003E5__2;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+60]");
												_ = 0;
												_ = _003CdesiredLocation_003E5__2;
												transform2.localPosition = vector3;
												MapEntity mapEntity6 = entity;
												bool flag17 = entity == null;
												num = 0.0;
												text = null;
												num3 = (nint)vector3;
												obj = (_003CInternal_MoveEntity_003Ed__36)(object)transform2;
												if (!flag17)
												{
													bool flag18 = (object)mapEntity6.Location == null;
													num = 0.0;
													text = null;
													num3 = (nint)vector3;
													obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity6.Location;
													if (!flag18)
													{
														Transform transform3 = mapEntity6.Location.transform;
														bool flag19 = (object)transform3 == null;
														num = 0.0;
														text = null;
														num3 = unchecked((nint)null);
														obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity6.Location;
														if (!flag19)
														{
															Vector3 localPosition2 = transform3.localPosition;
															num4 = localPosition2.x;
															mapEntity6.Position = (Vector3)localPosition2.x;
															_ = localPosition2.z;
															obj = (_003CInternal_MoveEntity_003Ed__36)(object)entity;
															bool flag20 = entity == null;
															num = 0.0;
															text = null;
															num3 = (nint)transform3;
															if (!flag20)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rcx_v14 (FireMission+<Internal_MoveEntity>d__36)+70]");
																entityLocation = (EntityLocation)0;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rcx_v14 (FireMission+<Internal_MoveEntity>d__36)+70]");
																if ((nint)0 != 0)
																{
																	goto IL_0b30;
																}
																goto IL_0b3e;
															}
														}
													}
												}
											}
										}
									}
								}
								else
								{
									bool flag21 = !fireMission.DebugLogs;
									text6 = null;
									num21 = (nint)transform;
									obj = (_003CInternal_MoveEntity_003Ed__36)num14;
									if (!flag21)
									{
										num3 = (nint)entity;
										bool flag22 = entity == null;
										num = 0.0;
										text = null;
										obj = (_003CInternal_MoveEntity_003Ed__36)num14;
										if (flag22)
										{
											goto IL_1322;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rdx_v16 (Il2CppMethodInfo)+10]");
										string text7 = "[ENTITY] Moving: " + (string)0 + " | Moved Instantly, no movement distance";
										Debug.Log(text7);
										obj36 = 0;
										text6 = " | Moved Instantly, no movement distance";
										num21 = unchecked((nint)null);
										obj = (_003CInternal_MoveEntity_003Ed__36)(object)text7;
									}
									mapEntity7 = entity;
									bool flag23 = entity == null;
									obj37 = obj36;
									num22 = 0.0;
									num23 = num4;
									num = 0.0;
									text = text6;
									num3 = num21;
									if (!flag23)
									{
										goto IL_0c6e;
									}
								}
							}
						}
					}
				}
				goto IL_1322;
			}
			if (_003C_003E1__state != 1)
			{
				goto IL_0b3e;
			}
			_003C_003E1__state = -1;
			goto IL_13c3;
			IL_13c3:
			num4 = Time.timeAsDouble;
			mapEntity7 = entity;
			bool flag24 = entity == null;
			num = num4;
			obj = null;
			if (!flag24)
			{
				object obj38 = mapEntity7.State & MapEntityStates.Destroyed;
				bool flag25 = obj38 == null;
				bool flag26 = (nint)obj38 < 0;
				object obj39 = !flag25;
				if (obj39 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,qword ptr [rbx+48h]\"");
					bool flag27 = !flag26;
					obj37 = obj36;
					num22 = num4;
					text6 = text;
					num21 = num3;
					num23 = num4;
					if (flag27)
					{
						goto IL_0c6e;
					}
					object obj40 = mapEntity7.State & MapEntityStates.Moving;
					if (obj40 != null)
					{
						double num24 = endsAt;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rbx+50h]\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [rbx+50h]\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm6,xmm1\"");
						float num25;
						if (0 <= 0)
						{
							bool flag28 = !(0f > 1f);
							num25 = 0f;
							num24 = 1.0;
							if (!flag28)
							{
								num25 = 1f;
								num24 = 1.0;
							}
						}
						else
						{
							num25 = 0f;
						}
						bool flag29 = (object)mapEntity7.Location == null;
						num = num4;
						num4 = num24;
						obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity7.Location;
						if (!flag29)
						{
							Transform transform4 = mapEntity7.Location.transform;
							num4 = (double)_003CstartingLocation_003E5__3;
							double num26 = (double)_003CdesiredLocation_003E5__2 - (double)_003CstartingLocation_003E5__3;
							_ = _003CstartingLocation_003E5__3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+5C]");
							nint num27 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-4C]");
							object obj41 = num27 - 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+60]");
							nint num28 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+6C]");
							object obj42 = num28 - 0;
							double num29 = num26 * (double)num25;
							float num30 = (float)obj41 * num25;
							float num31 = (float)obj42 * num25;
							double num32 = num29 + (double)_003CstartingLocation_003E5__3;
							float num33 = num30;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-4C]");
							float num34 = num33 + 0f;
							float num35 = num31;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+6C]");
							float num36 = num35 + 0f;
							bool flag30 = (object)transform4 == null;
							num = num36;
							num3 = unchecked((nint)null);
							obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity7.Location;
							if (!flag30)
							{
								Vector3 vector4 = (transform4.localPosition = (Vector3)(obj2 - 80));
								MapEntity mapEntity8 = entity;
								bool flag31 = entity == null;
								num32 = num2;
								num = num36;
								text = null;
								num3 = (nint)vector4;
								obj = (_003CInternal_MoveEntity_003Ed__36)(object)transform4;
								if (!flag31)
								{
									bool flag32 = (object)mapEntity8.Location == null;
									num32 = num2;
									num = num36;
									text = null;
									num3 = (nint)vector4;
									obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity8.Location;
									if (!flag32)
									{
										Transform transform5 = mapEntity8.Location.transform;
										bool flag33 = (object)transform5 == null;
										num32 = num2;
										num = num36;
										text = null;
										num3 = unchecked((nint)null);
										obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity8.Location;
										if (!flag33)
										{
											Vector3 localPosition3 = transform5.localPosition;
											mapEntity8.Position = (Vector3)localPosition3.x;
											_ = localPosition3.z;
											_003C_003E2__current = null;
											_003C_003E1__state = 1;
											return true;
										}
									}
								}
							}
						}
						goto IL_1322;
					}
				}
				goto IL_0b3e;
			}
			goto IL_1322;
			IL_1322:
			throw new NullReferenceException();
			IL_0b30:
			entityLocation.OnEntityMoved();
			goto IL_0b3e;
			IL_0b3e:
			return false;
			IL_0c6e:
			bool flag34 = (object)mapEntity7.Location == null;
			obj36 = obj37;
			num = num22;
			text = text6;
			num3 = num21;
			num4 = num23;
			obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity7.Location;
			if (!flag34)
			{
				Transform transform6 = mapEntity7.Location.transform;
				bool flag35 = (object)transform6 == null;
				obj36 = obj37;
				num = num22;
				text = text6;
				num3 = unchecked((nint)null);
				num4 = num23;
				obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity7.Location;
				if (!flag35)
				{
					Vector3 vector6 = (Vector3)(obj2 - 80);
					num4 = (double)_003CdesiredLocation_003E5__2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v1 (FireMission+<Internal_MoveEntity>d__36)+60]");
					_ = 0;
					_ = _003CdesiredLocation_003E5__2;
					transform6.localPosition = vector6;
					MapEntity mapEntity9 = entity;
					bool flag36 = entity == null;
					obj36 = obj37;
					num = num22;
					text = null;
					num3 = (nint)vector6;
					obj = (_003CInternal_MoveEntity_003Ed__36)(object)transform6;
					if (!flag36)
					{
						bool flag37 = (object)mapEntity9.Location == null;
						obj36 = obj37;
						num = num22;
						text = null;
						num3 = (nint)vector6;
						obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity9.Location;
						if (!flag37)
						{
							Transform transform7 = mapEntity9.Location.transform;
							bool flag38 = (object)transform7 == null;
							obj36 = obj37;
							num = num22;
							text = null;
							num3 = unchecked((nint)null);
							obj = (_003CInternal_MoveEntity_003Ed__36)(object)mapEntity9.Location;
							if (!flag38)
							{
								Vector3 localPosition4 = transform7.localPosition;
								num4 = localPosition4.x;
								mapEntity9.Position = (Vector3)localPosition4.x;
								_ = localPosition4.z;
								obj = (_003CInternal_MoveEntity_003Ed__36)(object)entity;
								bool flag39 = entity == null;
								obj36 = obj37;
								num = num22;
								text = null;
								num3 = (nint)transform7;
								if (!flag39)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rcx_v14 (FireMission+<Internal_MoveEntity>d__36)+70]");
									entityLocation = (EntityLocation)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rcx_v14 (FireMission+<Internal_MoveEntity>d__36)+70]");
									if ((nint)0 != 0)
									{
										goto IL_0b30;
									}
									goto IL_0b3e;
								}
							}
						}
					}
				}
			}
			goto IL_1322;
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

	private static FireMission _003CInstance_003Ek__BackingField;

	public bool useFixedSeed;

	public int fixedSeed = 12345;

	public RectTransform coordinateRoot;

	public EntityLocation POI_Prefab;

	public float cellWidth = 1f;

	public float cellHeight = 1f;

	public bool yIncreasesUp = true;

	public float distanceToKmScale = 1f;

	public bool clearSpawnedMarkers = true;

	public bool DebugLogs;

	public bool selectOnlyActivePoints = true;

	public bool useAlternateTextWhenNoActive;

	public string altTextNoActiveTarget = "No active targets remaining.";

	public string altTextNoActiveEnemy = "No active enemies detected.";

	public string altTextNoActiveAlly = "No active allies available.";

	public string altTextNoActiveOptionalTarget = "No optional targets active.";

	public int seed;

	public Dictionary<string, MapEntity> Entities = new Dictionary<string, MapEntity>(StringComparer.s_ordinalIgnoreCase);

	public Dictionary<string, TimerValue> RunningTimers;

	public List<ImpactGraph> RunningImpactGraphs;

	public static FireMission Instance
	{
		get
		{
			return _003CInstance_003Ek__BackingField;
		}
		private set
		{
			_003CInstance_003Ek__BackingField = value;
		}
	}

	private void OnValidate()
	{
		AutoAssignCoordinateRootIfNeeded();
	}

	private void Awake()
	{
		AutoAssignCoordinateRootIfNeeded();
		_003CInstance_003Ek__BackingField = this;
		if (POI_Prefab == null)
		{
			EntityLocation pOI_Prefab = Resources.Load<EntityLocation>("POI_Template");
			POI_Prefab = pOI_Prefab;
			if (POI_Prefab != null)
			{
				Debug.Log("Found 'POI_Template' Dynamically");
			}
		}
	}

	private void OnEnable()
	{
		//IL_0027: Expected I4, but got I8
		AutoAssignCoordinateRootIfNeeded();
		ClearSpawnedMarkersIfNeeded();
		int num = ((!useFixedSeed) ? UnityEngine.Random.Range(-2147483648, 2147483647) : fixedSeed);
		seed = num;
		Dictionary<string, MapEntity> entities = new Dictionary<string, MapEntity>();
		Entities = entities;
	}

	private void OnDestroy()
	{
		if (_003CInstance_003Ek__BackingField == this)
		{
			_003CInstance_003Ek__BackingField = null;
		}
	}

	public void GenerateMission()
	{
		//IL_0027: Expected I4, but got I8
		AutoAssignCoordinateRootIfNeeded();
		ClearSpawnedMarkersIfNeeded();
		int num = ((!useFixedSeed) ? UnityEngine.Random.Range(-2147483648, 2147483647) : fixedSeed);
		seed = num;
		Dictionary<string, MapEntity> entities = new Dictionary<string, MapEntity>();
		Entities = entities;
	}

	public Vector3[] GetGridBounds()
	{
		GameObject gameObject = GameObject.FindWithTag("MissionParent");
		if (gameObject != null)
		{
			Vector3[] array = new Vector3[4];
			if ((object)gameObject != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				RectTransform rectTransform = default(RectTransform);
				if ((object)rectTransform != null)
				{
					rectTransform.GetWorldCorners(array);
					return array;
				}
			}
			return (Vector3[])(object)new NullReferenceException();
		}
		return System.EmptyArray<Vector3>.Value;
	}

	private unsafe void Update()
	{
		//IL_003b: Expected F8, but got I4
		//IL_0098: Expected F8, but got I4
		//IL_0238: Expected O, but got Ref
		//IL_00e8: Expected O, but got Ref
		//IL_0117: Expected O, but got Ref
		//IL_02f6: Expected O, but got I
		//IL_038d: Expected O, but got I4
		//IL_019b: Expected O, but got Ref
		//IL_01dc: Expected O, but got Ref
		//IL_01ff: Expected F4, but got I
		//IL_0219: Expected O, but got I
		bool flag = RunningTimers == null;
		Dictionary<string, StateNode.NodeExecutionState> runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(object)RunningTimers;
		if (!flag)
		{
			int count = RunningTimers.Count;
			bool flag2 = count <= 0;
			double num = 0.0;
			runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(object)RunningTimers;
			if (!flag2)
			{
				double timeAsDouble = Time.timeAsDouble;
				runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(object)RunningTimers;
				if (RunningTimers == null)
				{
					goto IL_039c;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
				double num2 = 0.0;
				Dictionary<string, TimerValue>.Enumerator enumerator2 = default(Dictionary<string, TimerValue>.Enumerator);
				Dictionary<string, TimerValue>.Enumerator enumerator = enumerator2;
				double num3 = default(double);
				num = num3;
				Dictionary<string, TimerValue>.Enumerator enumerator3 = default(Dictionary<string, TimerValue>.Enumerator);
				object obj = default(object);
				object obj2 = default(object);
				Dictionary<string, StateNode.NodeExecutionState> dictionary = default(Dictionary<string, StateNode.NodeExecutionState>);
				string timerID = default(string);
				object obj3 = default(object);
				while (enumerator3.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
					bool flag3 = obj == null;
					runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(&num2);
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
						bool flag4 = obj2 == null;
						runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(&num2);
						if (!flag4)
						{
							bool flag5 = dictionary == null;
							runningTimers = dictionary;
							if (!flag5)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rax+18h]\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ stack_18_v9+10]");
								_ = 0;
								EventData_GenericTimerTimeUpdate eventData_GenericTimerTimeUpdate = new EventData_GenericTimerTimeUpdate();
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
								bool flag6 = eventData_GenericTimerTimeUpdate == null;
								runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(&num2);
								if (!flag6)
								{
									eventData_GenericTimerTimeUpdate.TimerID = timerID;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
									bool flag7 = obj3 == null;
									runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(&num2);
									if (!flag7)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ stack_-B8_v9+14]");
										eventData_GenericTimerTimeUpdate.CurrentSeconds = 0f;
										ProcessEvent(eventData_GenericTimerTimeUpdate);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ stack_18_v9+10]");
										enumerator = (Dictionary<string, TimerValue>.Enumerator)0;
										num = timeAsDouble;
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				enumerator3.Dispose();
				runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(&enumerator3);
			}
			List<ImpactGraph> runningImpactGraphs = RunningImpactGraphs;
			bool flag8 = (nint)RunningImpactGraphs < 0;
			if (RunningImpactGraphs != null)
			{
				int num4 = runningImpactGraphs._size - 1;
				if (flag8)
				{
					return;
				}
				object obj4 = default(object);
				while (true)
				{
					runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(object)RunningImpactGraphs;
					if (RunningImpactGraphs == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj4 == null)
					{
						break;
					}
					object obj5 = obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v679 @ rdx_v12+208] (should have been resolved before IL gen)");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ stack_8_v9+48]");
					if ((nint)0 == 0)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ stack_8_v9+48]");
					int count2 = ((Dictionary<string, StateNode.NodeExecutionState>)0).Count;
					bool flag9 = count2 < 0;
					if (count2 <= 0)
					{
						flag9 = (nint)RunningImpactGraphs < 0;
						bool flag10 = RunningImpactGraphs == null;
						runningTimers = (Dictionary<string, StateNode.NodeExecutionState>)(object)RunningImpactGraphs;
						if (flag10)
						{
							break;
						}
						RunningImpactGraphs.RemoveAt(num4);
					}
					num4--;
					object obj6 = !flag9;
					if (obj6 == null)
					{
						return;
					}
				}
			}
		}
		goto IL_039c;
		IL_039c:
		throw new NullReferenceException();
	}

	public unsafe Vector2 ToLocalSpace(Vector3 worldPos)
	{
		//IL_005b: Expected O, but got Ref
		if ((bool)coordinateRoot)
		{
			if ((object)coordinateRoot == null)
			{
				return (Vector2)new NullReferenceException();
			}
			object obj = default(object);
			Vector3 vector = coordinateRoot.InverseTransformPoint((Vector3)(&obj));
		}
		Vector2 result = default(Vector2);
		return result;
	}

	public unsafe MapEntity CreateMapEntity(string id, TextIdentifier name, int entityIDIndex, Vector3 worldPos, EntityRoles role, int health, int armour, int stars, MapEntityStates startingState, string icon)
	{
		//IL_00c6: Expected O, but got I4
		//IL_0202: Expected O, but got I
		//IL_0212: Expected O, but got I
		//IL_0103: Expected O, but got I4
		//IL_0234: Expected I4, but got O
		//IL_023f: Expected O, but got Ref
		//IL_0140: Expected O, but got I4
		//IL_0170: Expected O, but got I4
		//IL_02f3: Expected I, but got O
		//IL_036d: Expected I, but got O
		//IL_037d: Expected O, but got I
		//IL_044c: Expected I4, but got O
		//IL_01db: Expected O, but got I4
		//IL_01e4: Expected O, but got I4
		//IL_03f2: Expected I, but got O
		//IL_0402: Expected O, but got I
		//IL_04e1: Expected O, but got I4
		//IL_047a: Expected I, but got O
		//IL_048a: Expected O, but got I
		//IL_050f: Expected I, but got O
		//IL_051f: Expected O, but got I
		MapEntity mapEntity;
		string text5;
		if (!string.IsNullOrWhiteSpace(id))
		{
			mapEntity = new MapEntity();
			if (id != null)
			{
				string text = id.ToLowerInvariant();
				if (text != null)
				{
					string iD = text.Replace(" ", "_");
					if (mapEntity != null)
					{
						mapEntity.ID = iD;
						string text2 = id.ToLowerInvariant();
						bool flag = text2 == null;
						object obj = 0;
						object obj2;
						if (!flag)
						{
							string text3 = text2.Replace(" ", "_");
							bool flag2 = text3 == null;
							obj = 0;
							if (!flag2)
							{
								string[] array = text3.Split('#');
								bool flag3 = array == null;
								obj = 0;
								if (!flag3)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
									string text4 = default(string);
									bool flag4 = text4 == null;
									obj = 0;
									if (!flag4)
									{
										if ("0123456789" == null)
										{
											goto IL_05d7;
										}
										char[] trimChars = "0123456789".ToCharArray();
										text5 = text4.TrimEnd(trimChars);
										bool flag5 = text5 != null;
										obj = 0;
										obj2 = 0;
										if (flag5)
										{
											goto IL_05e5;
										}
									}
								}
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v350 @ rax_v72+B8]");
						object obj4 = 0;
						text5 = (string)obj4;
						obj2 = obj;
						goto IL_05e5;
					}
				}
			}
			goto IL_05d7;
		}
		return null;
		IL_05e5:
		mapEntity.RawID = text5;
		mapEntity.Name = name;
		string icon2 = default(string);
		mapEntity.Icon = icon2;
		mapEntity.IDIndex = entityIDIndex;
		mapEntity.Role = (EntityRoles)icon;
		int num = default(int);
		Vector2 vector = ToLocalSpace((Vector3)(&num));
		MapEntityStates state = default(MapEntityStates);
		mapEntity.State = state;
		IntPtr intPtr = default(IntPtr);
		mapEntity.MaxHealth = (int)(nint)intPtr;
		mapEntity.Health = (int)(nint)intPtr;
		int armour2 = default(int);
		mapEntity.Armour = armour2;
		Vector3 position = default(Vector3);
		mapEntity.Position = position;
		int stars2 = default(int);
		mapEntity.Stars = stars2;
		_ = 0;
		if (DebugLogs)
		{
			object[] array2 = new object[5];
			if (mapEntity.ID != null)
			{
				nint num2 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj5 = default(object);
				if (obj5 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text6 = default(string);
					throw text6;
				}
			}
			array2[0] = mapEntity.ID;
			string text7 = mapEntity.Name.Get();
			if (text7 != null)
			{
				nint num3 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v666 @ rdx_v47 (Il2CppClass<System.Object[]>)+40]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj7 = default(object);
				bool flag6 = obj7 == null;
				string text8 = text7;
				if (flag6)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj8 = default(object);
					throw obj8;
				}
			}
			array2[1] = text7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj9 = default(object);
			if (obj9 != null)
			{
				nint num4 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v731 @ rdx_v45 (Il2CppClass<System.Object[]>)+40]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj11 = default(object);
				bool flag7 = obj11 == null;
				object obj12 = obj9;
				if (flag7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj13 = default(object);
					throw obj13;
				}
			}
			array2[2] = obj9;
			object obj15 = default(object);
			object obj14 = (EntityRoles)obj15;
			if (obj14 != null)
			{
				nint num5 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v794 @ rdx_v43 (Il2CppClass<System.Object[]>)+40]");
				object obj16 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj17 = default(object);
				bool flag8 = obj17 == null;
				object obj18 = obj14;
				if (flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj19 = default(object);
					throw obj19;
				}
			}
			array2[3] = obj14;
			Vector3 position2 = mapEntity.Position;
			object obj20 = (Vector3)num;
			if (obj20 != null)
			{
				nint num6 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v840 @ rdx_v41 (Il2CppClass<System.Object[]>)+40]");
				object obj21 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj22 = default(object);
				bool flag9 = obj22 == null;
				object obj23 = obj20;
				if (flag9)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj24 = default(object);
					throw obj24;
				}
			}
			array2[4] = obj20;
			string message = string.Format("[ENTITY] Created: {0} | {1}#{2} ({3}) @ {4}", array2);
			Debug.Log(message);
		}
		return mapEntity;
		IL_05d7:
		return (MapEntity)(object)new NullReferenceException();
	}

	public unsafe void MoveMapEntity(MapEntity entity, Vector3 worldPos, bool continousMovement, float timespan)
	{
		//IL_004c: Expected O, but got Ref
		double timeAsDouble = Time.timeAsDouble;
		object obj = default(object);
		if (0 < (nint)obj)
		{
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
		object obj2 = default(object);
		float timespan2 = default(float);
		double startedAt = default(double);
		double endsAt = default(double);
		MoveMapEntity(entity, (Vector3)(&obj2), continousMovement, timespan2, startedAt, endsAt);
	}

	public unsafe void MoveMapEntity(MapEntity entity, Vector3 worldPos, bool continousMovement, float timespan, double startedAt, double endsAt)
	{
		//IL_001d: Expected O, but got Ref
		//IL_03ab: Expected O, but got F4
		//IL_00f0: Expected I, but got O
		//IL_016a: Expected I, but got O
		//IL_017a: Expected O, but got I
		//IL_0249: Expected I4, but got O
		//IL_01ef: Expected I, but got O
		//IL_01ff: Expected O, but got I
		//IL_0277: Expected I, but got O
		//IL_0287: Expected O, but got I
		//IL_030c: Expected I, but got O
		//IL_031c: Expected O, but got I
		if (entity == null)
		{
			return;
		}
		if (!continousMovement)
		{
			Vector3 vector2 = default(Vector3);
			Vector2 vector = ToLocalSpace((Vector3)(&vector2));
			Vector2 vector3 = default(Vector2);
			entity.Position = vector3;
			_ = 0;
			GameObject go = entity.Location.gameObject;
			PositionInRootSpace(go, vector3);
			if ((object)entity.Location != null)
			{
				entity.Location.OnEntityMoved();
			}
			if (!DebugLogs)
			{
				return;
			}
			object[] array = new object[5];
			if (entity.ID != null)
			{
				nint num = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text = default(string);
					throw text;
				}
			}
			array[0] = entity.ID;
			string text2 = entity.Name.Get();
			if (text2 != null)
			{
				nint num2 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v560 @ rdx_v44 (Il2CppClass<System.Object[]>)+40]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				bool flag = obj3 == null;
				string text3 = text2;
				if (flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj4 = default(object);
					throw obj4;
				}
			}
			array[1] = text2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj5 = default(object);
			if (obj5 != null)
			{
				nint num3 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v625 @ rdx_v42 (Il2CppClass<System.Object[]>)+40]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj7 = default(object);
				bool flag2 = obj7 == null;
				object obj8 = obj5;
				if (flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj9 = default(object);
					throw obj9;
				}
			}
			array[2] = obj5;
			object obj11 = default(object);
			object obj10 = (EntityRoles)obj11;
			if (obj10 != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v688 @ rdx_v40 (Il2CppClass<System.Object[]>)+40]");
				object obj12 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj13 = default(object);
				bool flag3 = obj13 == null;
				object obj14 = obj10;
				if (flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj15 = default(object);
					throw obj15;
				}
			}
			array[3] = obj10;
			Vector2 position = entity.Position;
			object obj16 = vector2;
			if (obj16 != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v734 @ rdx_v38 (Il2CppClass<System.Object[]>)+40]");
				object obj17 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj18 = default(object);
				bool flag4 = obj18 == null;
				object obj19 = obj16;
				if (flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj20 = default(object);
					throw obj20;
				}
			}
			array[4] = obj16;
			string message = string.Format("[ENTITY] Moved: {0} | {1}#{2} ({3}) -> {4}", array);
			Debug.Log(message);
		}
		else
		{
			_003CInternal_MoveEntity_003Ed__36 obj21 = new _003CInternal_MoveEntity_003Ed__36(0);
			obj21._003C_003E1__state = 0;
			obj21._003C_003E4__this = this;
			obj21.entity = entity;
			obj21.worldPos = (Vector3)worldPos.x;
			float timespan2 = default(float);
			obj21.timespan = timespan2;
			double endsAt2 = default(double);
			obj21.endsAt = endsAt2;
			double startedAt2 = default(double);
			obj21.startedAt = startedAt2;
			_ = worldPos.z;
			Coroutine coroutine = StartCoroutine(obj21);
		}
	}

	public IEnumerator Internal_MoveEntity(MapEntity entity, Vector3 worldPos, float timespan, double startedAt, double endsAt)
	{
		//IL_0024: Expected O, but got F4
		_003CInternal_MoveEntity_003Ed__36 obj = new _003CInternal_MoveEntity_003Ed__36(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.entity = entity;
		obj.worldPos = (Vector3)worldPos.x;
		_ = worldPos.z;
		obj.timespan = timespan;
		double startedAt2 = default(double);
		obj.startedAt = startedAt2;
		double endsAt2 = default(double);
		obj.endsAt = endsAt2;
		return obj;
	}

	public void RegisterMapEntity(MapEntity entity)
	{
		//IL_0466: Expected I, but got O
		//IL_049c: Expected I, but got O
		//IL_010c: Expected I, but got O
		//IL_012a: Expected O, but got I
		//IL_014a: Expected O, but got I
		//IL_0152: Expected I, but got O
		//IL_084f: Expected I, but got O
		//IL_0880: Expected O, but got I
		//IL_077c: Expected O, but got I
		//IL_01c0: Expected I, but got O
		//IL_01d0: Expected O, but got I
		//IL_01e9: Expected O, but got I
		//IL_0201: Expected I, but got O
		//IL_07b1: Expected O, but got I
		//IL_08bc: Expected O, but got I
		//IL_025d: Expected I, but got O
		//IL_026d: Expected O, but got I
		//IL_0286: Expected O, but got I
		//IL_029e: Expected I, but got O
		//IL_07da: Expected O, but got I
		//IL_02fd: Expected I, but got O
		//IL_030d: Expected O, but got I
		//IL_0326: Expected O, but got I
		//IL_033e: Expected I, but got O
		//IL_0803: Expected O, but got I
		//IL_05a2: Expected I, but got O
		//IL_065c: Expected I, but got O
		//IL_039e: Expected I, but got O
		//IL_03ae: Expected O, but got I
		//IL_03c7: Expected O, but got I
		//IL_03df: Expected I, but got O
		//IL_082c: Expected O, but got I
		//IL_05e6: Expected I, but got O
		if (entity == null)
		{
			return;
		}
		Dictionary<string, MapEntity> entities = Entities;
		bool flag = Entities == null;
		MapEntity mapEntity = entity;
		EntityRoles role = default(EntityRoles);
		int iDIndex = default(int);
		FireMission fireMission;
		if (!flag)
		{
			if (Entities.ContainsKey(entity.ID))
			{
				return;
			}
			bool flag2 = Entities == null;
			mapEntity = (MapEntity)(object)entity.ID;
			nint num = 0;
			entities = Entities;
			if (!flag2)
			{
				Entities.set_Item(entity.ID, entity);
				if (DebugLogs)
				{
					object[] array = new object[5];
					if (entity.ID != null)
					{
						nint num2 = (nint)array;
						string iD = entity.ID;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v633 @ rdx_v65 (Il2CppClass<System.Object[]>)+40]");
						((Dictionary<string, MapEntity>)(object)iD).set_Item((string)0, entity);
						object obj = default(object);
						bool flag3 = obj == null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v633 @ rdx_v65 (Il2CppClass<System.Object[]>)+40]");
						mapEntity = (MapEntity)0;
						num = (nint)entity;
						entities = (Dictionary<string, MapEntity>)(object)entity.ID;
						if (flag3)
						{
							entities.set_Item((string)(object)mapEntity, (MapEntity)num);
							Dictionary<string, MapEntity> dictionary = default(Dictionary<string, MapEntity>);
							throw dictionary;
						}
					}
					array[0] = entity.ID;
					string text = entity.Name.Get();
					if (text != null)
					{
						nint num3 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v978 @ rdx_v63 (Il2CppClass<System.Object[]>)+40]");
						string key = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v978 @ rdx_v63 (Il2CppClass<System.Object[]>)+40]");
						((Dictionary<string, MapEntity>)(object)text).set_Item((string)0, entity);
						object obj2 = default(object);
						bool flag4 = obj2 == null;
						num = (nint)entity;
						Dictionary<string, MapEntity> dictionary2 = (Dictionary<string, MapEntity>)(object)text;
						if (flag4)
						{
							dictionary2.set_Item(key, (MapEntity)num);
							Dictionary<string, MapEntity> dictionary3 = default(Dictionary<string, MapEntity>);
							throw dictionary3;
						}
					}
					array[1] = text;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Dictionary<string, MapEntity> dictionary4 = default(Dictionary<string, MapEntity>);
					if (dictionary4 != null)
					{
						nint num4 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1109 @ rdx_v61 (Il2CppClass<System.Object[]>)+40]");
						string key2 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1109 @ rdx_v61 (Il2CppClass<System.Object[]>)+40]");
						dictionary4.set_Item((string)0, entity);
						object obj3 = default(object);
						bool flag5 = obj3 == null;
						num = (nint)entity;
						Dictionary<string, MapEntity> dictionary5 = dictionary4;
						if (flag5)
						{
							dictionary5.set_Item(key2, (MapEntity)num);
							Dictionary<string, MapEntity> dictionary6 = default(Dictionary<string, MapEntity>);
							throw dictionary6;
						}
					}
					array[2] = dictionary4;
					Dictionary<string, MapEntity> dictionary7 = (Dictionary<string, MapEntity>)(object)role;
					if (dictionary7 != null)
					{
						nint num5 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1148 @ rdx_v59 (Il2CppClass<System.Object[]>)+40]");
						string key3 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1148 @ rdx_v59 (Il2CppClass<System.Object[]>)+40]");
						dictionary7.set_Item((string)0, entity);
						object obj4 = default(object);
						bool flag6 = obj4 == null;
						num = (nint)entity;
						Dictionary<string, MapEntity> dictionary8 = dictionary7;
						if (flag6)
						{
							dictionary8.set_Item(key3, (MapEntity)num);
							Dictionary<string, MapEntity> dictionary9 = default(Dictionary<string, MapEntity>);
							throw dictionary9;
						}
					}
					array[3] = dictionary7;
					object obj5 = default(object);
					Dictionary<string, MapEntity> dictionary10 = (Dictionary<string, MapEntity>)(object)(Vector3)obj5;
					if (dictionary10 != null)
					{
						nint num6 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1190 @ rdx_v57 (Il2CppClass<System.Object[]>)+40]");
						string key4 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1190 @ rdx_v57 (Il2CppClass<System.Object[]>)+40]");
						dictionary10.set_Item((string)0, entity);
						object obj6 = default(object);
						bool flag7 = obj6 == null;
						num = (nint)entity;
						Dictionary<string, MapEntity> dictionary11 = dictionary10;
						if (flag7)
						{
							dictionary11.set_Item(key4, (MapEntity)num);
							object obj7 = default(object);
							throw obj7;
						}
					}
					array[4] = dictionary10;
					string message = string.Format("[ENTITY] Registered: {0} | {1}#{2} ({3}) @ {4}", array);
					Debug.Log(message);
					role = entity.Role;
					iDIndex = entity.IDIndex;
				}
				bool flag8 = POI_Prefab != null;
				num = unchecked((nint)null);
				fireMission = (FireMission)(object)POI_Prefab;
				if (flag8)
				{
					bool flag9 = entity.Location == null;
					num = unchecked((nint)null);
					fireMission = (FireMission)(object)entity.Location;
					if (flag9)
					{
						Transform parent;
						if (coordinateRoot != null)
						{
							parent = coordinateRoot;
						}
						else
						{
							Transform transform = base.transform;
							parent = transform;
						}
						EntityLocation entityLocation = (entity.Location = UnityEngine.Object.Instantiate(POI_Prefab, parent));
						bool flag10 = (object)entity.Location == null;
						mapEntity = (MapEntity)(object)entityLocation;
						num = 0;
						entities = (Dictionary<string, MapEntity>)(object)entity.Location;
						if (!flag10)
						{
							entity.Location.Init(entity);
							bool flag11 = (object)entity.Location == null;
							mapEntity = entity;
							num = unchecked((nint)null);
							entities = (Dictionary<string, MapEntity>)(object)entity.Location;
							if (!flag11)
							{
								GameObject go = entity.Location.gameObject;
								Vector2 vector = default(Vector2);
								PositionInRootSpace(go, vector);
								num = (nint)vector;
								fireMission = this;
								goto IL_05f0;
							}
						}
						goto IL_073b;
					}
				}
				goto IL_05f0;
			}
		}
		goto IL_073b;
		IL_073b:
		throw new NullReferenceException();
		IL_05f0:
		entities = (Dictionary<string, MapEntity>)(object)fireMission;
		nint num7 = (nint)typeof(FireMission);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ rax_v33 (Il2CppClass<FireMission>)+B8]");
		nint num8 = 0;
		FireMission fireMission2 = _003CInstance_003Ek__BackingField;
		bool flag12 = (object)_003CInstance_003Ek__BackingField == null;
		mapEntity = (MapEntity)num8;
		if (!flag12)
		{
			entity.State = entity.State;
			bool flag13 = !fireMission2.DebugLogs;
			mapEntity = (MapEntity)num8;
			if (!flag13)
			{
				object arg = (MapEntityStates)iDIndex;
				object obj8 = (MapEntityStates)role;
				string message2 = $"[ENTITY] State Update: {arg} -> {obj8} For {entity.ID}";
				Debug.Log(message2);
				mapEntity = null;
				nint num = (nint)obj8;
			}
			bool flag14 = (object)entity.Location == null;
			entities = (Dictionary<string, MapEntity>)(object)entity.Location;
			if (!flag14)
			{
				entity.Location.OnEntityStateChanged(entity.State, entity.State);
				EventData_EntityStateChanged eventData_EntityStateChanged = new EventData_EntityStateChanged();
				bool flag15 = eventData_EntityStateChanged == null;
				mapEntity = null;
				nint num = (nint)entity.State;
				entities = (Dictionary<string, MapEntity>)(object)eventData_EntityStateChanged;
				if (!flag15)
				{
					eventData_EntityStateChanged.Entity = entity;
					eventData_EntityStateChanged.oldState = entity.State;
					eventData_EntityStateChanged.newState = entity.State;
					_003CInstance_003Ek__BackingField.ProcessEvent(eventData_EntityStateChanged);
					return;
				}
			}
		}
		goto IL_073b;
	}

	private void SpawnRuntimeObjectForEntity(MapEntity entity)
	{
		if (entity != null && POI_Prefab != null && entity.Location == null)
		{
			Transform parent;
			if (coordinateRoot != null)
			{
				parent = coordinateRoot;
			}
			else
			{
				Transform transform = base.transform;
				parent = transform;
			}
			EntityLocation location = UnityEngine.Object.Instantiate(POI_Prefab, parent);
			entity.Location = location;
			entity.Location.Init(entity);
			GameObject go = entity.Location.gameObject;
			Vector2 rootLocalPos = default(Vector2);
			PositionInRootSpace(go, rootLocalPos);
		}
	}

	public unsafe string GetNoActiveAlternateTextForRoles(HashSet<EntityRoles> roles)
	{
		if (roles != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [roles @ rdx (System.Collections.Generic.HashSet`1<EntityRoles>)+20]");
			object obj = default(object);
			if ((nint)0 != 0 && !roles.Contains((EntityRoles)(int)(&obj)))
			{
				if (roles.Contains((EntityRoles)(int)(&obj)))
				{
					return altTextNoActiveEnemy;
				}
				if (roles.Contains((EntityRoles)(int)(&obj)))
				{
					return altTextNoActiveAlly;
				}
				if (roles.Contains((EntityRoles)(int)(&obj)))
				{
					return altTextNoActiveOptionalTarget;
				}
			}
		}
		return altTextNoActiveTarget;
	}

	public unsafe bool TryGetMapEntity(string id, out MapEntity entity)
	{
		//IL_0195: Expected O, but got I
		//IL_00a5: Expected O, but got I
		//IL_00b5: Expected O, but got I
		//IL_00bd: Expected I, but got O
		//IL_03b4: Expected O, but got I
		//IL_01d6: Expected O, but got I
		//IL_0222: Expected O, but got I4
		//IL_0258: Expected O, but got Ref
		//IL_02a5: Expected O, but got I
		Dictionary<string, MapEntity> entities = Entities;
		if (Entities != null)
		{
			if (Entities.TryGetValue(id, out entity))
			{
				goto IL_034a;
			}
			if (id != null)
			{
				string[] array = id.Split('#');
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
				IntPtr intPtr = default(IntPtr);
				bool flag = intPtr != (IntPtr)0;
				nint num = intPtr;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rax_v42+B8]");
					entities = (Dictionary<string, MapEntity>)0;
					num = (nint)entities;
					if (entities == null)
					{
						goto IL_0350;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rdi_v8 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, MapEntity>>)+10]");
				int num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rdi_v8 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, MapEntity>>)+10]");
				bool flag2 = (nint)0 <= (nint)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rdi_v8 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, MapEntity>>)+10]");
				int num3 = 0;
				if (!flag2)
				{
					bool flag5;
					do
					{
						int index = num3 - 1;
						char c = ((string)num).get_Chars(index);
						bool flag3 = char.IsDigit(c);
						bool flag4 = !flag3;
						num2 = num3;
						if (flag4)
						{
							break;
						}
						num2 = num3 - 1;
						flag5 = num2 > 0;
						num3 = num2;
					}
					while (flag5);
				}
				string value = ((string)num).Substring(0, num2);
				int num4 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rdi_v8 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, MapEntity>>)+10]");
				bool flag6 = (nint)num4 >= (nint)0;
				int result = 1;
				if (!flag6)
				{
					string s = ((string)num).Substring(num2);
					bool flag7 = int.TryParse(s, out result);
				}
				if (Entities != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
					object obj2 = 0;
					Dictionary<string, MapEntity>.Enumerator enumerator = default(Dictionary<string, MapEntity>.Enumerator);
					Dictionary<string, MapEntity> dictionary = default(Dictionary<string, MapEntity>);
					while (true)
					{
						if (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
							bool flag8 = intPtr == (IntPtr)0;
							string text = (string)(&obj2);
							if (!flag8)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ stack_10_v6 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, MapEntity>>)+18]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ stack_10_v6 (Il2CppClass<System.Collections.Generic.Dictionary`2<System.String, MapEntity>>)+18]");
									if (((string)0).Equals(value, StringComparison.OrdinalIgnoreCase))
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
										if (dictionary == null)
										{
											throw new NullReferenceException();
										}
										if (dictionary._freeCount == 1)
										{
											break;
										}
									}
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						enumerator.Dispose();
						return false;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
					ref MapEntity reference = ref *(MapEntity*)intPtr;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
					goto IL_034a;
				}
			}
		}
		goto IL_0350;
		IL_0350:
		throw new NullReferenceException();
		IL_034a:
		return true;
	}

	public void ProcessNotification(string notifID)
	{
		//IL_004a: Expected O, but got I4
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		missionManager._003CCurrentMission_003Ek__BackingField.OnNotification(notifID);
		List<ImpactGraph> runningImpactGraphs = RunningImpactGraphs;
		bool flag = (nint)RunningImpactGraphs < 0;
		object obj = runningImpactGraphs._size - 1;
		if (!flag)
		{
			ImpactGraph impactGraph = default(ImpactGraph);
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				impactGraph.OnNotification(notifID);
				obj--;
			}
			while ((nint)impactGraph >= 0);
		}
	}

	public void ProcessEvent(EventNode.EventData evt)
	{
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		if (!(missionManager._003CCurrentMission_003Ek__BackingField != null))
		{
			return;
		}
		MissionManager missionManager2 = MissionManager._003CInstance_003Ek__BackingField;
		missionManager2._003CCurrentMission_003Ek__BackingField.CheckEvents(evt);
		List<ImpactGraph> runningImpactGraphs = RunningImpactGraphs;
		int num = runningImpactGraphs._size;
		ImpactGraph impactGraph = default(ImpactGraph);
		while (true)
		{
			num--;
			if (num >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				impactGraph.CheckEvents(evt);
				continue;
			}
			break;
		}
	}

	public void SetEntityState(MapEntity entity, MapEntityStates newState)
	{
		//IL_003a: Expected I4, but got O
		//IL_0047: Expected I4, but got O
		entity.State = newState;
		if (DebugLogs)
		{
			object obj = default(object);
			object arg = (MapEntityStates)obj;
			object obj2 = default(object);
			object arg2 = (MapEntityStates)obj2;
			string message = $"[ENTITY] State Update: {arg} -> {arg2} For {entity.ID}";
			Debug.Log(message);
		}
		entity.Location.OnEntityStateChanged(entity.State, newState);
		EventData_EntityStateChanged eventData_EntityStateChanged = new EventData_EntityStateChanged();
		eventData_EntityStateChanged.Entity = entity;
		eventData_EntityStateChanged.oldState = entity.State;
		eventData_EntityStateChanged.newState = newState;
		ProcessEvent(eventData_EntityStateChanged);
	}

	private void AutoAssignCoordinateRootIfNeeded()
	{
		if (!coordinateRoot)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if ((bool)obj)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				RectTransform rectTransform = default(RectTransform);
				coordinateRoot = rectTransform;
			}
			if (!coordinateRoot)
			{
				string text = base.name;
				string message = "[" + text + "] No coordinateRoot assigned and no parent Canvas found.";
				Debug.LogWarning(message);
			}
		}
	}

	private void ClearSpawnedMarkersIfNeeded()
	{
		if (!clearSpawnedMarkers)
		{
			return;
		}
		Transform transform;
		if (coordinateRoot != null)
		{
			transform = coordinateRoot;
		}
		else
		{
			Transform transform2 = base.transform;
			transform = transform2;
		}
		List<GameObject> list = new List<GameObject>();
		int num = 0;
		int num2 = 0;
		UnityEngine.Object obj = default(UnityEngine.Object);
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		while (true)
		{
			int childCount = transform.childCount;
			if (num2 >= childCount)
			{
				break;
			}
			Transform child = transform.GetChild(num);
			GameObject item = child.gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				if (!(obj2 == null))
				{
					goto IL_00f4;
				}
			}
			list.Add(item);
			goto IL_00f4;
			IL_00f4:
			num++;
			num2 = num;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			UnityEngine.Object.Destroy(obj);
		}
		enumerator.Dispose();
	}

	internal unsafe Vector2 SampleAreaPosition(RectTransform zone, System.Random rng)
	{
		//IL_0169: Expected O, but got Ref
		//IL_0073: Invalid comparison between I4 and F4
		//IL_0099: Invalid comparison between I4 and F4
		//IL_010b: Expected O, but got Ref
		if ((object)zone != null)
		{
			Rect rect = zone.rect;
			if (rng != null)
			{
				double num = rng.NextDouble();
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
				if (0 > 0 || 0f > 1f)
				{
				}
				double num2 = rng.NextDouble();
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
				if (0 > 0 || 0f > 1f)
				{
				}
				float num3 = default(float);
				Vector3 vector = zone.TransformPoint((Vector3)(&num3));
				if ((bool)coordinateRoot)
				{
					if ((object)coordinateRoot == null)
					{
						goto IL_0114;
					}
					Vector3 vector2 = coordinateRoot.InverseTransformPoint((Vector3)(&num3));
				}
				Vector2 result = default(Vector2);
				return result;
			}
		}
		goto IL_0114;
		IL_0114:
		return (Vector2)new NullReferenceException();
	}

	internal unsafe Vector3 RandomPointWorldInside(RectTransform zone, System.Random rng)
	{
		//IL_0122: Expected O, but got Ref
		//IL_0133: Expected native int or pointer, but got O
		//IL_0145: Expected native int or pointer, but got O
		//IL_0073: Invalid comparison between I4 and F4
		//IL_0099: Invalid comparison between I4 and F4
		if ((object)zone != null)
		{
			Rect rect = zone.rect;
			if (rng != null)
			{
				double num = rng.NextDouble();
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
				if (0 > 0 || 0f > 1f)
				{
				}
				double num2 = rng.NextDouble();
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
				if (0 > 0 || 0f > 1f)
				{
				}
				object obj = default(object);
				Vector3 vector = zone.TransformPoint((Vector3)(&obj));
				Vector3 vector2 = default(Vector3);
				((Vector3*)(nint)vector2)->x = vector.x;
				((Vector3*)(nint)vector2)->z = vector.z;
				return vector2;
			}
		}
		return (Vector3)new NullReferenceException();
	}

	public unsafe void PositionInRootSpace(GameObject go, Vector2 rootLocalPos)
	{
		//IL_0126: Expected O, but got Ref
		//IL_00a1: Expected O, but got Ref
		object obj2 = default(object);
		if ((bool)coordinateRoot)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (!obj)
			{
				Transform transform = go.transform;
				transform.SetParent(coordinateRoot, worldPositionStays: false);
				Transform transform2 = go.transform;
				transform2.localPosition = (Vector3)(&obj2);
				return;
			}
			Transform parent = ((Transform)obj).parent;
			if (parent != coordinateRoot)
			{
				((Transform)obj).SetParent((Transform)coordinateRoot, false);
			}
			((RectTransform)obj).anchoredPosition = rootLocalPos;
		}
		else
		{
			Transform transform3 = go.transform;
			transform3.position = (Vector3)(&obj2);
		}
	}

	public FireMission()
	{
		Dictionary<string, TimerValue> runningTimers = new Dictionary<string, TimerValue>(StringComparer.s_ordinalIgnoreCase);
		RunningTimers = runningTimers;
		RunningImpactGraphs = new List<ImpactGraph>();
		base._002Ector();
	}
}
