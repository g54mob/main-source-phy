using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cpp2ILInjected;
using UnityEngine;

namespace MTAssets.UltimateLODSystem;

public class UltimateLevelOfDetailOptimizer : MonoBehaviour
{
	private sealed class _003CUlodOptimizationLoop_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UltimateLevelOfDetailOptimizer _003C_003E4__this;

		private int _003Ci_003E5__2;

		private List<UltimateLevelOfDetail>.Enumerator _003C_003E7__wrap2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CUlodOptimizationLoop_003Ed__9(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		unsafe void IDisposable.Dispose()
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			if (_003C_003E1__state == -3 || _003C_003E1__state == 4)
			{
				_ = 4294967295L;
				object obj = default(object);
				List<UltimateLevelOfDetail>.Enumerator enumerator = (List<UltimateLevelOfDetail>.Enumerator)(obj + 48);
				((List<UltimateLevelOfDetail>.Enumerator*)enumerator)->Dispose();
			}
		}

		private unsafe bool MoveNext()
		{
			//IL_16d0: Expected I, but got I8
			//IL_0018: Expected O, but got I
			//IL_15b3: Expected I, but got I8
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_00f1: Expected I, but got I8
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			//IL_00b2: Expected I, but got I8
			//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Expected O, but got Unknown
			//IL_1a5f: Expected I4, but got O
			//IL_1b15: Expected I, but got O
			//IL_1671: Expected I, but got O
			//IL_0166: Expected I, but got O
			//IL_009b: Expected I, but got I8
			//IL_1add: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ae2: Expected O, but got Unknown
			//IL_019d: Expected I, but got O
			//IL_1c56: Expected I, but got I8
			//IL_1c5f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c64: Expected O, but got Unknown
			//IL_1c8b: Expected O, but got I
			//IL_149f: Unknown result type (might be due to invalid IL or missing references)
			//IL_14a4: Expected O, but got Unknown
			//IL_01d4: Expected I, but got O
			//IL_148b: Expected I, but got I8
			//IL_1511: Expected I, but got O
			//IL_1519: Expected I, but got O
			//IL_1b60: Expected I, but got O
			//IL_1a1f: Expected O, but got I
			//IL_1548: Expected I, but got O
			//IL_154d: Expected I, but got O
			//IL_19ca: Expected I, but got O
			//IL_1033: Unknown result type (might be due to invalid IL or missing references)
			//IL_1038: Expected O, but got Unknown
			//IL_02c6: Expected I, but got O
			//IL_036d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0372: Expected O, but got Unknown
			//IL_0377: Expected I, but got O
			//IL_0332: Expected I4, but got I8
			//IL_033b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0340: Expected O, but got Unknown
			//IL_0345: Expected I, but got O
			//IL_1b77: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b7c: Expected O, but got Unknown
			//IL_11d7: Expected I, but got O
			//IL_1202: Expected I, but got O
			//IL_1226: Expected I4, but got I8
			//IL_122b: Expected I, but got O
			//IL_138e: Expected I, but got O
			//IL_13b9: Expected I, but got O
			//IL_13dd: Expected I4, but got I8
			//IL_0749: Expected I, but got O
			//IL_0778: Expected I, but got O
			//IL_0817: Expected O, but got Ref
			//IL_0846: Expected I, but got O
			//IL_084e: Expected O, but got Ref
			//IL_0875: Expected I, but got O
			//IL_087d: Expected O, but got Ref
			//IL_0b03: Expected I, but got O
			//IL_0b32: Expected I, but got O
			//IL_0bd1: Expected O, but got Ref
			//IL_0c00: Expected I, but got O
			//IL_0c08: Expected O, but got Ref
			//IL_0c2f: Expected I, but got O
			//IL_0c37: Expected O, but got Ref
			UltimateLevelOfDetailOptimizer ultimateLevelOfDetailOptimizer = default(UltimateLevelOfDetailOptimizer);
			UltimateLevelOfDetailOptimizer dELAY_BETWEEN_OPTIMIZATION_UPDATES = (UltimateLevelOfDetailOptimizer)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES;
			bool flag = ((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr == (IntPtr)0;
			UltimateLevelOfDetailOptimizer ultimateLevelOfDetailOptimizer3;
			UltimateLevelOfDetailOptimizer ultimateLevelOfDetailOptimizer2;
			if (!flag)
			{
				object obj = (nint)((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr - 1;
				RuntimeInstancesDetector runtimeInstancesDetector5 = default(RuntimeInstancesDetector);
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						object obj3 = obj2 - 1;
						if (!flag)
						{
							if ((nint)obj3 != 1)
							{
								return false;
							}
							((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = unchecked((nint)4294967293L);
							goto IL_1ad4;
						}
						((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = unchecked((nint)4294967295L);
						WaitForSecondsRealtime dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE = (WaitForSecondsRealtime)(ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE + 1);
						ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE = dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
						UltimateLevelOfDetail ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)ultimateLevelOfDetailOptimizer;
					}
					else
					{
						((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = unchecked((nint)4294967295L);
						bool flag2 = ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES == null;
						ultimateLevelOfDetailOptimizer2 = ultimateLevelOfDetailOptimizer;
						if (flag2)
						{
							throw new NullReferenceException();
						}
						if (!dELAY_BETWEEN_OPTIMIZATION_UPDATES.enableOptimizationTasks)
						{
							ultimateLevelOfDetailOptimizer2 = ultimateLevelOfDetailOptimizer;
							ultimateLevelOfDetailOptimizer3 = ultimateLevelOfDetailOptimizer;
							goto IL_1bb5;
						}
						int[] instructionsToMakeOnUlods = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
						bool flag3 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods == null;
						nint num = (nint)ultimateLevelOfDetailOptimizer;
						if (flag3)
						{
							throw new NullReferenceException();
						}
						RuntimeInstancesDetector runtimeInstancesDetector = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
						bool flag4 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
						num = (nint)ultimateLevelOfDetailOptimizer;
						if (flag4)
						{
							throw new NullReferenceException();
						}
						List<UltimateLevelOfDetail> instancesOfUlodInThisScene = runtimeInstancesDetector.instancesOfUlodInThisScene;
						bool flag5 = runtimeInstancesDetector.instancesOfUlodInThisScene == null;
						num = (nint)ultimateLevelOfDetailOptimizer;
						if (flag5)
						{
							throw new NullReferenceException();
						}
						bool flag6 = instructionsToMakeOnUlods.Length == instancesOfUlodInThisScene._size;
						UltimateLevelOfDetail ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)ultimateLevelOfDetailOptimizer;
						if (!flag6)
						{
							RuntimeInstancesDetector runtimeInstancesDetector2 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
							List<UltimateLevelOfDetail> instancesOfUlodInThisScene2 = runtimeInstancesDetector2.instancesOfUlodInThisScene;
							ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)(dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods = new int[instancesOfUlodInThisScene2._size]);
						}
						UltimateLevelOfDetail ultimateLevelOfDetail2 = null;
						UltimateLevelOfDetail ultimateLevelOfDetail3 = default(UltimateLevelOfDetail);
						while (true)
						{
							int[] instructionsToMakeOnUlods2 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
							bool flag7 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods == null;
							num = (nint)ultimateLevelOfDetail;
							if (!flag7)
							{
								if ((nint)ultimateLevelOfDetail2 >= instructionsToMakeOnUlods2.Length)
								{
									break;
								}
								RuntimeInstancesDetector runtimeInstancesDetector3 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
								if ((object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector != null)
								{
									bool flag8 = runtimeInstancesDetector3.instancesOfUlodInThisScene == null;
									nint num2 = unchecked((nint)null);
									ultimateLevelOfDetail = ultimateLevelOfDetail3;
									if (!flag8)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
										bool flag9 = ((UltimateLevelOfDetailOptimizer)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES).isThisUlodPresentOnUlodsToBeIgnored(ultimateLevelOfDetail3);
										int[] instructionsToMakeOnUlods3 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
										if (!flag9)
										{
											instructionsToMakeOnUlods3[(object)ultimateLevelOfDetail2] = -1;
											ultimateLevelOfDetail2 = (UltimateLevelOfDetail)(ultimateLevelOfDetail2 + 1);
											num2 = unchecked((nint)null);
											ultimateLevelOfDetail = ultimateLevelOfDetail3;
										}
										else
										{
											instructionsToMakeOnUlods3[(object)ultimateLevelOfDetail2] = 2;
											ultimateLevelOfDetail2 = (UltimateLevelOfDetail)(ultimateLevelOfDetail2 + 1);
											num2 = unchecked((nint)null);
											ultimateLevelOfDetail = ultimateLevelOfDetail3;
										}
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						UltimateLevelOfDetail ultimateLevelOfDetail4 = null;
						object obj4 = default(object);
						UltimateLevelOfDetail ultimateLevelOfDetail5 = default(UltimateLevelOfDetail);
						Component component = default(Component);
						float x = default(float);
						float x2 = default(float);
						object obj5 = default(object);
						Component component2 = default(Component);
						Component component3 = default(Component);
						float x3 = default(float);
						float x4 = default(float);
						object obj6 = default(object);
						Component component4 = default(Component);
						while (true)
						{
							RuntimeInstancesDetector runtimeInstancesDetector4 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
							if ((object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector != null)
							{
								List<UltimateLevelOfDetail> instancesOfUlodInThisScene3 = runtimeInstancesDetector4.instancesOfUlodInThisScene;
								if (runtimeInstancesDetector4.instancesOfUlodInThisScene != null)
								{
									if ((nint)ultimateLevelOfDetail4 >= instancesOfUlodInThisScene3._size)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									bool flag10 = (object)runtimeInstancesDetector5 == null;
									nint num2 = (nint)(&runtimeInstancesDetector5);
									ultimateLevelOfDetail = ultimateLevelOfDetail4;
									if (!flag10)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ stack_20_v8 (MTAssets.UltimateLODSystem.RuntimeInstancesDetector)+8C]");
										bool flag11 = (nint)0 == 0;
										num2 = (nint)(&runtimeInstancesDetector5);
										ultimateLevelOfDetail = ultimateLevelOfDetail4;
										if (!flag11)
										{
											RuntimeInstancesDetector runtimeInstancesDetector6 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
											bool flag12 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
											num2 = (nint)(&runtimeInstancesDetector5);
											ultimateLevelOfDetail = ultimateLevelOfDetail4;
											if (flag12)
											{
												throw new NullReferenceException();
											}
											bool flag13 = runtimeInstancesDetector6.instancesOfUlodInThisScene == null;
											num2 = (nint)(&runtimeInstancesDetector5);
											ultimateLevelOfDetail = ultimateLevelOfDetail4;
											if (flag13)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
											bool flag14 = obj4 == null;
											num2 = (nint)(&obj4);
											ultimateLevelOfDetail = ultimateLevelOfDetail4;
											if (flag14)
											{
												throw new NullReferenceException();
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ stack_-F8+8C]");
											bool flag15 = (nint)0 == 0;
											num2 = (nint)(&obj4);
											ultimateLevelOfDetail = ultimateLevelOfDetail4;
											if (!flag15)
											{
												RuntimeInstancesDetector runtimeInstancesDetector7 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
												bool flag16 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
												num2 = (nint)(&obj4);
												ultimateLevelOfDetail = ultimateLevelOfDetail4;
												if (flag16)
												{
													throw new NullReferenceException();
												}
												bool flag17 = runtimeInstancesDetector7.instancesOfUlodInThisScene == null;
												num2 = (nint)(&obj4);
												ultimateLevelOfDetail = ultimateLevelOfDetail4;
												if (flag17)
												{
													throw new NullReferenceException();
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
												bool flag18 = (object)ultimateLevelOfDetail5 == null;
												num2 = (nint)(&ultimateLevelOfDetail5);
												ultimateLevelOfDetail = ultimateLevelOfDetail4;
												if (flag18)
												{
													throw new NullReferenceException();
												}
												bool flag19 = ultimateLevelOfDetail5.isMeshesCurrentScannedAndLodsWorkingInThisComponent();
												bool flag20 = !flag19;
												num2 = (nint)(&ultimateLevelOfDetail5);
												ultimateLevelOfDetail = null;
												if (!flag20)
												{
													int[] instructionsToMakeOnUlods4 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
													bool flag21 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods == null;
													num2 = (nint)(&ultimateLevelOfDetail5);
													ultimateLevelOfDetail = null;
													if (flag21)
													{
														throw new NullReferenceException();
													}
													bool flag22 = (nint)ultimateLevelOfDetail4 >= instructionsToMakeOnUlods4.Length;
													num2 = (nint)(&ultimateLevelOfDetail5);
													ultimateLevelOfDetail = null;
													if (flag22)
													{
														throw new IndexOutOfRangeException();
													}
													bool flag23 = instructionsToMakeOnUlods4[(object)ultimateLevelOfDetail4] == 2;
													num2 = (nint)(&ultimateLevelOfDetail5);
													ultimateLevelOfDetail = null;
													if (!flag23)
													{
														GameObject gameObject = ((Component)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES).gameObject;
														bool flag24 = (object)gameObject == null;
														num2 = (nint)(&ultimateLevelOfDetail5);
														ultimateLevelOfDetail = null;
														if (flag24)
														{
															throw new NullReferenceException();
														}
														Transform transform = gameObject.transform;
														bool flag25 = (object)transform == null;
														num2 = (nint)(&ultimateLevelOfDetail5);
														ultimateLevelOfDetail = null;
														if (flag25)
														{
															throw new NullReferenceException();
														}
														Vector3 position = transform.position;
														RuntimeInstancesDetector runtimeInstancesDetector8 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
														bool flag26 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
														num2 = unchecked((nint)null);
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)transform;
														if (flag26)
														{
															throw new NullReferenceException();
														}
														bool flag27 = runtimeInstancesDetector8.instancesOfUlodInThisScene == null;
														num2 = unchecked((nint)null);
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)transform;
														if (flag27)
														{
															throw new NullReferenceException();
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
														bool flag28 = (object)component == null;
														num2 = (nint)(&component);
														ultimateLevelOfDetail = ultimateLevelOfDetail4;
														if (flag28)
														{
															throw new NullReferenceException();
														}
														Transform transform2 = component.transform;
														bool flag29 = (object)transform2 == null;
														num2 = (nint)(&component);
														ultimateLevelOfDetail = null;
														if (flag29)
														{
															throw new NullReferenceException();
														}
														Vector3 position2 = transform2.position;
														UltimateLevelOfDetail ultimateLevelOfDetail6 = ((List<UltimateLevelOfDetail>)(&x)).get_Item((int)(&x2));
														RuntimeInstancesDetector runtimeInstancesDetector9 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
														bool flag30 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
														num2 = unchecked((nint)null);
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(&x2);
														if (flag30)
														{
															throw new NullReferenceException();
														}
														bool flag31 = runtimeInstancesDetector9.instancesOfUlodInThisScene == null;
														num2 = unchecked((nint)null);
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(&x2);
														if (flag31)
														{
															throw new NullReferenceException();
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
														bool flag32 = obj5 == null;
														num2 = (nint)(&obj5);
														ultimateLevelOfDetail = ultimateLevelOfDetail4;
														if (flag32)
														{
															throw new NullReferenceException();
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ stack_-E0+B0]");
														float num3 = 0f + dELAY_BETWEEN_OPTIMIZATION_UPDATES.ADITIONAL_CULLING_DISTANCE_OFFSET;
														bool flag33 = !(position2.x > num3);
														num2 = (nint)(&obj5);
														if (!flag33)
														{
															RuntimeInstancesDetector runtimeInstancesDetector10 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
															bool flag34 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
															num2 = (nint)(&obj5);
															ultimateLevelOfDetail = ultimateLevelOfDetail4;
															if (flag34)
															{
																throw new NullReferenceException();
															}
															bool flag35 = runtimeInstancesDetector10.instancesOfUlodInThisScene == null;
															num2 = (nint)(&obj5);
															ultimateLevelOfDetail = ultimateLevelOfDetail4;
															if (flag35)
															{
																throw new NullReferenceException();
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
															bool flag36 = (object)component2 == null;
															num2 = (nint)(&component2);
															ultimateLevelOfDetail = ultimateLevelOfDetail4;
															if (flag36)
															{
																throw new NullReferenceException();
															}
															GameObject gameObject2 = component2.gameObject;
															bool flag37 = (object)gameObject2 == null;
															num2 = (nint)(&component2);
															ultimateLevelOfDetail = null;
															if (flag37)
															{
																throw new NullReferenceException();
															}
															bool activeSelf = gameObject2.activeSelf;
															bool flag38 = !activeSelf;
															num2 = (nint)(&component2);
															if (!flag38)
															{
																int[] instructionsToMakeOnUlods5 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
																bool flag39 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods == null;
																num2 = (nint)(&component2);
																ultimateLevelOfDetail = null;
																if (flag39)
																{
																	throw new NullReferenceException();
																}
																bool flag40 = (nint)ultimateLevelOfDetail4 >= instructionsToMakeOnUlods5.Length;
																num2 = (nint)(&component2);
																ultimateLevelOfDetail = null;
																if (flag40)
																{
																	throw new IndexOutOfRangeException();
																}
																instructionsToMakeOnUlods5[(object)ultimateLevelOfDetail4] = 0;
																num2 = (nint)(&component2);
															}
														}
														GameObject gameObject3 = ((Component)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES).gameObject;
														bool flag41 = (object)gameObject3 == null;
														ultimateLevelOfDetail = null;
														if (flag41)
														{
															throw new NullReferenceException();
														}
														Transform transform3 = gameObject3.transform;
														bool flag42 = (object)transform3 == null;
														ultimateLevelOfDetail = null;
														if (flag42)
														{
															throw new NullReferenceException();
														}
														Vector3 position3 = transform3.position;
														RuntimeInstancesDetector runtimeInstancesDetector11 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
														bool flag43 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
														num2 = unchecked((nint)null);
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)transform3;
														if (flag43)
														{
															throw new NullReferenceException();
														}
														bool flag44 = runtimeInstancesDetector11.instancesOfUlodInThisScene == null;
														num2 = unchecked((nint)null);
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)transform3;
														if (flag44)
														{
															throw new NullReferenceException();
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
														bool flag45 = (object)component3 == null;
														num2 = (nint)(&component3);
														ultimateLevelOfDetail = ultimateLevelOfDetail4;
														if (flag45)
														{
															throw new NullReferenceException();
														}
														Transform transform4 = component3.transform;
														bool flag46 = (object)transform4 == null;
														num2 = (nint)(&component3);
														ultimateLevelOfDetail = null;
														if (flag46)
														{
															throw new NullReferenceException();
														}
														Vector3 position4 = transform4.position;
														UltimateLevelOfDetail ultimateLevelOfDetail7 = ((List<UltimateLevelOfDetail>)(&x3)).get_Item((int)(&x4));
														RuntimeInstancesDetector runtimeInstancesDetector12 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
														bool flag47 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
														num2 = unchecked((nint)null);
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(&x4);
														if (flag47)
														{
															throw new NullReferenceException();
														}
														bool flag48 = runtimeInstancesDetector12.instancesOfUlodInThisScene == null;
														num2 = unchecked((nint)null);
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(&x4);
														if (flag48)
														{
															throw new NullReferenceException();
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
														bool flag49 = obj6 == null;
														num2 = (nint)(&obj6);
														ultimateLevelOfDetail = ultimateLevelOfDetail4;
														if (flag49)
														{
															throw new NullReferenceException();
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ stack_-C8+B0]");
														float num4 = 0f + dELAY_BETWEEN_OPTIMIZATION_UPDATES.ADITIONAL_CULLING_DISTANCE_OFFSET;
														bool flag50 = num4 < position4.x;
														x3 = position3.x;
														x4 = position4.x;
														x = position.x;
														x2 = position2.x;
														num2 = (nint)(&obj6);
														ultimateLevelOfDetail = ultimateLevelOfDetail4;
														if (!flag50)
														{
															RuntimeInstancesDetector runtimeInstancesDetector13 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
															bool flag51 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
															num2 = (nint)(&obj6);
															ultimateLevelOfDetail = ultimateLevelOfDetail4;
															if (flag51)
															{
																throw new NullReferenceException();
															}
															bool flag52 = runtimeInstancesDetector13.instancesOfUlodInThisScene == null;
															num2 = (nint)(&obj6);
															ultimateLevelOfDetail = ultimateLevelOfDetail4;
															if (flag52)
															{
																throw new NullReferenceException();
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
															bool flag53 = (object)component4 == null;
															num2 = (nint)(&component4);
															ultimateLevelOfDetail = ultimateLevelOfDetail4;
															if (flag53)
															{
																throw new NullReferenceException();
															}
															GameObject gameObject4 = component4.gameObject;
															bool flag54 = (object)gameObject4 == null;
															num2 = (nint)(&component4);
															ultimateLevelOfDetail = null;
															if (flag54)
															{
																throw new NullReferenceException();
															}
															bool activeSelf2 = gameObject4.activeSelf;
															x3 = position3.x;
															x4 = position4.x;
															x = position.x;
															x2 = position2.x;
															num2 = (nint)(&component4);
															ultimateLevelOfDetail = null;
															if (!activeSelf2)
															{
																int[] instructionsToMakeOnUlods6 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
																bool flag55 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods == null;
																num2 = (nint)(&component4);
																ultimateLevelOfDetail = null;
																if (flag55)
																{
																	throw new NullReferenceException();
																}
																bool flag56 = (nint)ultimateLevelOfDetail4 >= instructionsToMakeOnUlods6.Length;
																num2 = (nint)(&component4);
																ultimateLevelOfDetail = null;
																if (flag56)
																{
																	throw new IndexOutOfRangeException();
																}
																instructionsToMakeOnUlods6[(object)ultimateLevelOfDetail4] = 1;
																x3 = position3.x;
																x4 = position4.x;
																x = position.x;
																x2 = position2.x;
																num2 = (nint)(&component4);
																ultimateLevelOfDetail = null;
															}
														}
													}
												}
											}
										}
										ultimateLevelOfDetail4 = (UltimateLevelOfDetail)(ultimateLevelOfDetail4 + 1);
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							num = (nint)ultimateLevelOfDetail;
							throw new NullReferenceException();
						}
						ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE = null;
					}
					Component component5 = default(Component);
					Component component6 = default(Component);
					while (true)
					{
						nint num2 = (nint)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
						if (ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES != null)
						{
							RuntimeInstancesDetector runtimeInstancesDetector14 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
							if ((object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector != null)
							{
								ultimateLevelOfDetailOptimizer2 = (UltimateLevelOfDetailOptimizer)(object)runtimeInstancesDetector14.instancesOfUlodInThisScene;
								bool flag57 = runtimeInstancesDetector14.instancesOfUlodInThisScene == null;
								UltimateLevelOfDetail ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)runtimeInstancesDetector14.instancesOfUlodInThisScene;
								if (!flag57)
								{
									WaitForSecondsRealtime dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE2 = ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
									CancellationTokenSource cancellationTokenSource = ((MonoBehaviour)ultimateLevelOfDetailOptimizer2).m_CancellationTokenSource;
									bool flag58 = System.Runtime.CompilerServices.Unsafe.As<WaitForSecondsRealtime, UIntPtr>(ref dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE2) >= System.Runtime.CompilerServices.Unsafe.As<CancellationTokenSource, UIntPtr>(ref cancellationTokenSource);
									ultimateLevelOfDetailOptimizer3 = ultimateLevelOfDetailOptimizer;
									if (flag58)
									{
										break;
									}
									ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
									if (dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods != null)
									{
										WaitForSecondsRealtime dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE3 = ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
										CancellationTokenSource cancellationTokenSource2 = ((MonoBehaviour)ultimateLevelOfDetail).m_CancellationTokenSource;
										if (System.Runtime.CompilerServices.Unsafe.As<WaitForSecondsRealtime, UIntPtr>(ref dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE3) < System.Runtime.CompilerServices.Unsafe.As<CancellationTokenSource, UIntPtr>(ref cancellationTokenSource2))
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4776 @ rdx_v91 (MTAssets.UltimateLODSystem.UltimateLevelOfDetail)+20+v4773 @ r8_v73 (Il2CppMethodInfo)*4]");
											if ((nint)0 == -1)
											{
												WaitForSecondsRealtime dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE4 = (WaitForSecondsRealtime)(ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE + 1);
												ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE = dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE4;
												continue;
											}
											WaitForSecondsRealtime dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE5 = ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
											if (dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods != null)
											{
												WaitForSecondsRealtime dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE6 = ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
												CancellationTokenSource cancellationTokenSource3 = ((MonoBehaviour)ultimateLevelOfDetail).m_CancellationTokenSource;
												if (System.Runtime.CompilerServices.Unsafe.As<WaitForSecondsRealtime, UIntPtr>(ref dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE6) < System.Runtime.CompilerServices.Unsafe.As<CancellationTokenSource, UIntPtr>(ref cancellationTokenSource3))
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4776 @ rdx_v91 (MTAssets.UltimateLODSystem.UltimateLevelOfDetail)+20+v1861 @ rax_v38 (UnityEngine.WaitForSecondsRealtime)*4]");
													bool flag59 = (nint)0 != 0;
													UltimateLevelOfDetailOptimizer ultimateLevelOfDetailOptimizer4 = ultimateLevelOfDetailOptimizer;
													if (!flag59)
													{
														RuntimeInstancesDetector runtimeInstancesDetector15 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
														if ((object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null)
														{
															throw new NullReferenceException();
														}
														if (runtimeInstancesDetector15.instancesOfUlodInThisScene == null)
														{
															throw new NullReferenceException();
														}
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
														bool flag60 = (object)component5 == null;
														num2 = (nint)(&component5);
														if (flag60)
														{
															throw new NullReferenceException();
														}
														GameObject gameObject5 = component5.gameObject;
														bool flag61 = (object)gameObject5 == null;
														num2 = (nint)(&component5);
														ultimateLevelOfDetail = null;
														if (flag61)
														{
															throw new NullReferenceException();
														}
														gameObject5.SetActive(value: false);
														int[] instructionsToMakeOnUlods7 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
														ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
														bool flag62 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods == null;
														num2 = unchecked((nint)null);
														if (flag62)
														{
															throw new NullReferenceException();
														}
														bool flag63 = (nint)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE >= instructionsToMakeOnUlods7.Length;
														num2 = unchecked((nint)null);
														if (flag63)
														{
															throw new IndexOutOfRangeException();
														}
														instructionsToMakeOnUlods7[(object)ultimateLevelOfDetail] = -1;
														num2 = unchecked((nint)null);
														ultimateLevelOfDetailOptimizer4 = ultimateLevelOfDetailOptimizer;
													}
													ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
													WaitForSecondsRealtime dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE7 = ultimateLevelOfDetailOptimizer4.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
													if (dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods != null)
													{
														WaitForSecondsRealtime dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE8 = ultimateLevelOfDetailOptimizer4.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
														CancellationTokenSource cancellationTokenSource4 = ((MonoBehaviour)ultimateLevelOfDetail).m_CancellationTokenSource;
														if (System.Runtime.CompilerServices.Unsafe.As<WaitForSecondsRealtime, UIntPtr>(ref dELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE8) < System.Runtime.CompilerServices.Unsafe.As<CancellationTokenSource, UIntPtr>(ref cancellationTokenSource4))
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4776 @ rdx_v91 (MTAssets.UltimateLODSystem.UltimateLevelOfDetail)+20+v2171 @ rax_v40 (UnityEngine.WaitForSecondsRealtime)*4]");
															if ((nint)0 == 1)
															{
																RuntimeInstancesDetector runtimeInstancesDetector16 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
																if ((object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null)
																{
																	throw new NullReferenceException();
																}
																if (runtimeInstancesDetector16.instancesOfUlodInThisScene == null)
																{
																	throw new NullReferenceException();
																}
																ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)ultimateLevelOfDetailOptimizer4.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
																bool flag64 = (object)component6 == null;
																num2 = (nint)(&component6);
																if (flag64)
																{
																	throw new NullReferenceException();
																}
																GameObject gameObject6 = component6.gameObject;
																bool flag65 = (object)gameObject6 == null;
																num2 = (nint)(&component6);
																ultimateLevelOfDetail = null;
																if (flag65)
																{
																	throw new NullReferenceException();
																}
																gameObject6.SetActive(value: true);
																int[] instructionsToMakeOnUlods8 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods;
																ultimateLevelOfDetail = (UltimateLevelOfDetail)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
																bool flag66 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.instructionsToMakeOnUlods == null;
																num2 = unchecked((nint)null);
																if (flag66)
																{
																	throw new NullReferenceException();
																}
																bool flag67 = (nint)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE >= instructionsToMakeOnUlods8.Length;
																num2 = unchecked((nint)null);
																if (flag67)
																{
																	throw new IndexOutOfRangeException();
																}
																instructionsToMakeOnUlods8[(object)ultimateLevelOfDetail] = -1;
																ultimateLevelOfDetailOptimizer4 = ultimateLevelOfDetailOptimizer;
															}
															((MonoBehaviour)ultimateLevelOfDetailOptimizer4).m_CancellationTokenSource = (CancellationTokenSource)(object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
															((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = (IntPtr)3;
															return true;
														}
														throw new IndexOutOfRangeException();
													}
													throw new NullReferenceException();
												}
												throw new IndexOutOfRangeException();
											}
											throw new NullReferenceException();
										}
										throw new IndexOutOfRangeException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					goto IL_1bb5;
				}
				((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = unchecked((nint)4294967295L);
				GameObject gameObject7 = GameObject.Find("Ultimate LOD Data");
				bool flag68 = (object)gameObject7 == null;
				int num5 = 0;
				if (!flag68)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					bool flag69 = ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES == null;
					num5 = (int)(&runtimeInstancesDetector5);
					if (!flag69)
					{
						dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector = runtimeInstancesDetector5;
						bool flag70 = (object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector == null;
						ultimateLevelOfDetailOptimizer2 = (UltimateLevelOfDetailOptimizer)(object)runtimeInstancesDetector5;
						if (!flag70)
						{
							dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector.RegisterNewUlodOptimizerInThisScene((UltimateLevelOfDetailOptimizer)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES);
							nint num2 = unchecked((nint)null);
							ultimateLevelOfDetailOptimizer2 = (UltimateLevelOfDetailOptimizer)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES;
							goto IL_1683;
						}
						num5 = (int)ultimateLevelOfDetailOptimizer2;
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = unchecked((nint)4294967295L);
			WaitForSecondsRealtime cancellationTokenSource5 = new WaitForSecondsRealtime(0.1f);
			((MonoBehaviour)ultimateLevelOfDetailOptimizer).m_CancellationTokenSource = (CancellationTokenSource)(object)cancellationTokenSource5;
			((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = (IntPtr)1;
			return true;
			IL_1683:
			ultimateLevelOfDetailOptimizer3 = ultimateLevelOfDetailOptimizer;
			goto IL_1cbf;
			IL_1cbf:
			if (ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES != null)
			{
				((MonoBehaviour)ultimateLevelOfDetailOptimizer3).m_CancellationTokenSource = (CancellationTokenSource)(object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.DELAY_BETWEEN_OPTIMIZATION_UPDATES;
				((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = (IntPtr)2;
				return true;
			}
			throw new NullReferenceException();
			IL_1bb5:
			if (!dELAY_BETWEEN_OPTIMIZATION_UPDATES.enableOptimizationTasks)
			{
				RuntimeInstancesDetector runtimeInstancesDetector17 = dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector;
				if ((object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.runtimeInstancesDetector != null)
				{
					if (runtimeInstancesDetector17.instancesOfUlodInThisScene != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						float aDITIONAL_CULLING_DISTANCE_OFFSET = default(float);
						ultimateLevelOfDetailOptimizer.ADITIONAL_CULLING_DISTANCE_OFFSET = aDITIONAL_CULLING_DISTANCE_OFFSET;
						int[] instructionsToMakeOnUlods9 = default(int[]);
						ultimateLevelOfDetailOptimizer.instructionsToMakeOnUlods = instructionsToMakeOnUlods9;
						((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = unchecked((nint)4294967293L);
						nint num2 = 0;
						goto IL_1ad4;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			goto IL_1cbf;
			IL_1ad4:
			List<UltimateLevelOfDetail>.Enumerator enumerator = (List<UltimateLevelOfDetail>.Enumerator)(ultimateLevelOfDetailOptimizer + 48);
			if (((List<UltimateLevelOfDetail>.Enumerator*)enumerator)->MoveNext())
			{
				List<UltimateLevelOfDetail>.Enumerator enumerator2 = (List<UltimateLevelOfDetail>.Enumerator)(ultimateLevelOfDetailOptimizer + 48);
				UltimateLevelOfDetail current = ((List<UltimateLevelOfDetail>.Enumerator*)enumerator2)->Current;
				bool flag71 = ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES == null;
				nint num = 0;
				if (!flag71)
				{
					if (!((UltimateLevelOfDetailOptimizer)(object)ultimateLevelOfDetailOptimizer.DELAY_BETWEEN_OPTIMIZATION_UPDATES).isThisUlodPresentOnUlodsToBeIgnored(current))
					{
						bool flag72 = (object)current == null;
						nint num2 = unchecked((nint)null);
						num = (nint)current;
						if (flag72)
						{
							ultimateLevelOfDetailOptimizer2 = (UltimateLevelOfDetailOptimizer)num;
							throw new NullReferenceException();
						}
						GameObject gameObject8 = current.gameObject;
						bool flag73 = (object)gameObject8 == null;
						num2 = unchecked((nint)null);
						num = unchecked((nint)null);
						if (flag73)
						{
							throw new NullReferenceException();
						}
						gameObject8.SetActive(value: true);
					}
					((MonoBehaviour)ultimateLevelOfDetailOptimizer).m_CancellationTokenSource = (CancellationTokenSource)(object)dELAY_BETWEEN_OPTIMIZATION_UPDATES.DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;
					((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = (IntPtr)4;
					return true;
				}
				throw new NullReferenceException();
			}
			((UnityEngine.Object)ultimateLevelOfDetailOptimizer).m_CachedPtr = unchecked((nint)4294967295L);
			List<UltimateLevelOfDetail>.Enumerator enumerator3 = (List<UltimateLevelOfDetail>.Enumerator)(ultimateLevelOfDetailOptimizer + 48);
			((List<UltimateLevelOfDetail>.Enumerator*)enumerator3)->Dispose();
			ultimateLevelOfDetailOptimizer.ADITIONAL_CULLING_DISTANCE_OFFSET = 0f;
			ultimateLevelOfDetailOptimizer.instructionsToMakeOnUlods = null;
			ultimateLevelOfDetailOptimizer2 = (UltimateLevelOfDetailOptimizer)0;
			goto IL_1683;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private unsafe void _003C_003Em__Finally1()
		{
			//IL_0014: Expected I4, but got I8
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			_003C_003E1__state = -1;
			List<UltimateLevelOfDetail>.Enumerator enumerator = (List<UltimateLevelOfDetail>.Enumerator)(this + 48);
			((List<UltimateLevelOfDetail>.Enumerator*)enumerator)->Dispose();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private WaitForSecondsRealtime DELAY_BETWEEN_OPTIMIZATION_UPDATES;

	private WaitForSecondsRealtime DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;

	private float ADITIONAL_CULLING_DISTANCE_OFFSET;

	private RuntimeInstancesDetector runtimeInstancesDetector;

	private int[] instructionsToMakeOnUlods;

	public bool enableOptimizationTasks;

	public List<UltimateLevelOfDetail> ulodsToBeIgnored;

	public void Awake()
	{
		_003CUlodOptimizationLoop_003Ed__9 obj = new _003CUlodOptimizationLoop_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private bool isThisUlodPresentOnUlodsToBeIgnored(UltimateLevelOfDetail ulod)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<UltimateLevelOfDetail>.Enumerator enumerator = default(List<UltimateLevelOfDetail>.Enumerator);
		List<UltimateLevelOfDetail>.Enumerator enumerator2 = default(List<UltimateLevelOfDetail>.Enumerator);
		while (enumerator.MoveNext())
		{
			UltimateLevelOfDetail current = enumerator2.Current;
			if (current == ulod)
			{
				enumerator.Dispose();
				return true;
			}
		}
		enumerator.Dispose();
		return false;
	}

	private IEnumerator UlodOptimizationLoop()
	{
		_003CUlodOptimizationLoop_003Ed__9 obj = new _003CUlodOptimizationLoop_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public UltimateLevelOfDetailOptimizer()
	{
		WaitForSecondsRealtime dELAY_BETWEEN_OPTIMIZATION_UPDATES = new WaitForSecondsRealtime(0.2f);
		DELAY_BETWEEN_OPTIMIZATION_UPDATES = dELAY_BETWEEN_OPTIMIZATION_UPDATES;
		DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE = new WaitForSecondsRealtime(0.05f);
		ADITIONAL_CULLING_DISTANCE_OFFSET = 10f;
		instructionsToMakeOnUlods = new int[0];
		enableOptimizationTasks = true;
		ulodsToBeIgnored = new List<UltimateLevelOfDetail>();
		base._002Ector();
	}
}
