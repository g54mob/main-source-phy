using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ArticleSystem;

public class ArticleController : MonoBehaviour
{
	private sealed class _003CPopulationCoroutine_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ArticleController _003C_003E4__this;

		private List<(GameObject, GameObject)> _003Cstaged_003E5__2;

		private List<ArticleNewspaperPacker.ColumnState> _003CpackerColumns_003E5__3;

		private List<int> _003CcolumnIndexMap_003E5__4;

		private int _003Cf_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CPopulationCoroutine_003Ed__36(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_2730: Expected O, but got I4
			//IL_0761: Expected I4, but got I8
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			//IL_0706: Expected I4, but got I8
			//IL_07b5: Expected O, but got I
			//IL_0748: Expected F4, but got I4
			//IL_006b: Expected I4, but got I8
			//IL_07f1: Expected O, but got I
			//IL_081e: Expected O, but got I
			//IL_0845: Expected O, but got I
			//IL_2879: Expected O, but got I
			//IL_08d7: Expected O, but got I
			//IL_282b: Expected O, but got I
			//IL_0676: Expected O, but got I
			//IL_0869: Expected O, but got I
			//IL_06ba: Expected O, but got I
			//IL_06d9: Expected O, but got I
			//IL_2cc3: Expected O, but got Ref
			//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0602: Expected O, but got Unknown
			//IL_190b: Expected I, but got O
			//IL_1911: Expected O, but got I
			//IL_0c55: Expected O, but got I4
			//IL_0b8e: Expected O, but got Ref
			//IL_18d9: Expected I, but got O
			//IL_18df: Expected O, but got I
			//IL_14e9: Expected F4, but got O
			//IL_1941: Expected O, but got I
			//IL_1946: Expected I, but got O
			//IL_1956: Expected O, but got I
			//IL_29ba: Expected O, but got I
			//IL_1573: Expected O, but got F4
			//IL_0c8c: Expected O, but got I
			//IL_253c: Expected O, but got I
			//IL_0bf1: Expected O, but got I
			//IL_0eb2: Expected F4, but got I
			//IL_2580: Expected O, but got I
			//IL_0cc0: Expected O, but got I
			//IL_0c24: Expected O, but got I
			//IL_0c24: Expected O, but got I
			//IL_0281: Expected F4, but got I4
			//IL_0eec: Expected O, but got I
			//IL_0d37: Expected O, but got I
			//IL_1559: Expected O, but got F4
			//IL_0263: Expected O, but got I
			//IL_259f: Expected O, but got I
			//IL_2d23: Expected O, but got Ref
			//IL_19e7: Expected O, but got Ref
			//IL_128e: Expected I4, but got O
			//IL_12a9: Expected F4, but got I4
			//IL_1072: Expected O, but got I
			//IL_0d5c: Expected O, but got I
			//IL_1a97: Expected O, but got Ref
			//IL_1a24: Expected O, but got F4
			//IL_1262: Unknown result type (might be due to invalid IL or missing references)
			//IL_1267: Expected O, but got Unknown
			//IL_2a29: Expected O, but got I
			//IL_2a29: Expected O, but got I
			//IL_2a29: Expected O, but got I
			//IL_1dff: Expected O, but got I
			//IL_1a5f: Expected O, but got Ref
			//IL_1006: Expected F4, but got I4
			//IL_1e4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e54: Expected O, but got Unknown
			//IL_1e64: Expected O, but got I
			//IL_1b76: Expected O, but got I
			//IL_1784: Expected O, but got I
			//IL_2ab1: Invalid comparison between F4 and I4
			//IL_0e11: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e16: Expected O, but got Unknown
			//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a7: Expected O, but got Unknown
			//IL_1eb4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1eb9: Expected O, but got Unknown
			//IL_1ec9: Expected O, but got I
			//IL_1231: Expected O, but got Ref
			//IL_0ff7: Expected F4, but got O
			//IL_2dac: Expected O, but got I
			//IL_2dc4: Expected O, but got Ref
			//IL_1be5: Expected O, but got Ref
			//IL_124e: Expected O, but got Ref
			//IL_1254: Expected O, but got I
			//IL_2e6a: Expected O, but got I
			//IL_11a4: Expected O, but got Ref
			//IL_1c54: Expected O, but got I
			//IL_2b1d: Expected O, but got Ref
			//IL_2d4a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d4f: Expected O, but got Unknown
			//IL_1c3f: Expected O, but got I
			//IL_11d1: Invalid comparison between F4 and I4
			//IL_2e27: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e2c: Expected O, but got Unknown
			//IL_2e55: Expected O, but got Ref
			//IL_1cbc: Expected O, but got Ref
			//IL_1fbb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fc0: Expected O, but got Unknown
			//IL_2e9f: Expected O, but got I
			//IL_2dd2: Unknown result type (might be due to invalid IL or missing references)
			//IL_2dd7: Expected O, but got Unknown
			//IL_1d21: Expected O, but got I
			//IL_20f1: Expected O, but got I
			//IL_203e: Expected I, but got O
			//IL_204c: Expected O, but got I4
			//IL_22b4: Expected O, but got I
			//IL_1d83: Expected F4, but got I4
			//IL_1ddb: Expected O, but got I
			//IL_05cf: Expected O, but got I
			//IL_05df: Expected O, but got I
			//IL_05ef: Expected O, but got I
			//IL_21b4: Expected F4, but got I
			//IL_21c9: Expected F4, but got I
			//IL_21de: Expected F4, but got I
			//IL_21f3: Expected F4, but got I
			//IL_23ee: Expected O, but got I
			//IL_2f88: Expected O, but got Ref
			//IL_2444: Expected O, but got Ref
			//IL_24b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_24bd: Expected O, but got Unknown
			//IL_2469: Expected O, but got Ref
			//IL_2492: Expected F4, but got I4
			UnityEngine.Object obj = _003C_003E4__this;
			UnityEngine.Object obj2 = (UnityEngine.Object)_003C_003E1__state;
			bool flag = _003C_003E1__state == 0;
			List<GameObject> list2 = default(List<GameObject>);
			UnityEngine.Object obj6 = default(UnityEngine.Object);
			List<ArticleNewspaperPacker.Candidate> list4 = default(List<ArticleNewspaperPacker.Candidate>);
			int num3;
			float num4;
			List<ArticleNewspaperPacker.Candidate> list5;
			List<ArticleNewspaperPacker.Candidate> list3 = default(List<ArticleNewspaperPacker.Candidate>);
			if (!flag)
			{
				obj2 = (UnityEngine.Object)(obj2 - 1);
				if (!flag)
				{
					if ((nint)obj2 != 1)
					{
						goto IL_06e9;
					}
					_003C_003E1__state = -1;
					List<ArticleNewspaperPacker.ColumnState> list = _003CpackerColumns_003E5__3;
					bool flag2 = _003CpackerColumns_003E5__3 == null;
					obj2 = null;
					if (!flag2)
					{
						UnityEngine.Object obj3 = null;
						obj2 = null;
						ArticleColumn articleColumn = default(ArticleColumn);
						UnityEngine.Object obj5 = default(UnityEngine.Object);
						object arg = default(object);
						object arg2 = default(object);
						object arg3 = default(object);
						object arg4 = default(object);
						while ((nint)obj2 < list._size)
						{
							if ((object)_003C_003E4__this != null)
							{
								obj2 = (UnityEngine.Object)(object)_003CcolumnIndexMap_003E5__4;
								if (_003CcolumnIndexMap_003E5__4 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
										bool flag3 = (object)articleColumn == null;
										obj2 = articleColumn;
										if (!flag3)
										{
											articleColumn.FlushLayout();
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+99]");
											if ((nint)0 == 0)
											{
												goto IL_05f4;
											}
											obj2 = (UnityEngine.Object)(object)_003CpackerColumns_003E5__3;
											if (_003CpackerColumns_003E5__3 != null)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
												if (list2 != null)
												{
													obj2 = (UnityEngine.Object)(object)_003CpackerColumns_003E5__3;
													if (_003CpackerColumns_003E5__3 != null)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
														if (this != null)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ stack_-178_v36 (System.Collections.Generic.List`1<UnityEngine.GameObject>)+10]");
															if ((nint)0 > (nint)0)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ArticleSystem.ArticleController+<PopulationCoroutine>d__36)+14]");
																nint num = 0;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ stack_-178_v36 (System.Collections.Generic.List`1<UnityEngine.GameObject>)+10]");
																object obj4 = num / 0;
																float num2 = (float)obj4 * 100f;
															}
															else
															{
																float num2 = 0f;
															}
															string[] array = new string[5];
															bool flag4 = array == null;
															obj2 = (UnityEngine.Object)(object)typeof(string[]);
															if (!flag4)
															{
																bool flag5 = array.Length <= 0;
																obj2 = (UnityEngine.Object)(object)typeof(string[]);
																if (flag5)
																{
																	goto IL_27a0;
																}
																array[0] = "[ArticleController] Column '";
																obj2 = (UnityEngine.Object)(object)_003CcolumnIndexMap_003E5__4;
																if (_003CcolumnIndexMap_003E5__4 != null)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
																	Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
																	if ((nint)0 != 0)
																	{
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
																		bool flag6 = (object)obj5 == null;
																		obj2 = obj5;
																		if (!flag6)
																		{
																			string name = obj5.name;
																			bool flag7 = array.Length <= 1;
																			obj2 = obj5;
																			if (!flag7)
																			{
																				array[1] = name;
																				obj2 = (UnityEngine.Object)(array + 40);
																				if (array.Length > 2)
																				{
																					array[2] = "': ";
																					obj2 = (UnityEngine.Object)(object)_003CpackerColumns_003E5__3;
																					if (_003CpackerColumns_003E5__3 != null)
																					{
																						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
																						if (list3 != null)
																						{
																							object syncRoot = list3._syncRoot;
																							if (list3._syncRoot != null)
																							{
																								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
																								string text = $"{arg} articles  ";
																								bool flag8 = array.Length <= 3;
																								obj2 = (UnityEngine.Object)(object)"{0} articles  ";
																								if (!flag8)
																								{
																									array[3] = text;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
																									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
																									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
																									string text2 = $"{arg2:F1}/{arg3:F1}px  ({arg4:F1}% full)";
																									bool flag9 = array.Length <= 4;
																									obj2 = (UnityEngine.Object)(object)"{0:F1}/{1:F1}px  ({2:F1}% full)";
																									if (!flag9)
																									{
																										array[4] = text2;
																										string message = string.Concat(array);
																										obj2 = (UnityEngine.Object)(object)_003CcolumnIndexMap_003E5__4;
																										if (_003CcolumnIndexMap_003E5__4 != null)
																										{
																											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
																											if ((nint)0 != 0)
																											{
																												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
																												Debug.Log(message, obj6);
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ stack_-178_v36 (System.Collections.Generic.List`1<UnityEngine.GameObject>)+10]");
																												object obj7 = 0;
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ArticleSystem.ArticleController+<PopulationCoroutine>d__36)+14]");
																												object obj8 = 0;
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2128 @ rax_v474 (System.Object)+18]");
																												list4 = (List<ArticleNewspaperPacker.Candidate>)0;
																												goto IL_05f4;
																											}
																										}
																										goto IL_2794;
																									}
																								}
																								goto IL_27a0;
																							}
																						}
																					}
																					goto IL_2794;
																				}
																			}
																			goto IL_27a0;
																		}
																	}
																}
															}
															goto IL_2794;
														}
													}
												}
											}
										}
									}
								}
							}
							goto IL_274f;
							IL_2794:
							throw new NullReferenceException();
							IL_05f4:
							obj3 = (UnityEngine.Object)(obj3 + 1);
							list = _003CpackerColumns_003E5__3;
							bool flag10 = _003CpackerColumns_003E5__3 == null;
							obj2 = obj3;
							if (!flag10)
							{
								obj2 = obj3;
								continue;
							}
							goto IL_274f;
							IL_27a0:
							throw new IndexOutOfRangeException();
						}
						if ((object)_003C_003E4__this != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
							if ((UnityEngine.Object)0 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
								bool flag11 = (nint)0 == 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
								obj2 = (UnityEngine.Object)0;
								if (flag11)
								{
									goto IL_274f;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
								((ArticlePoolQueueManager)0).EndPass();
							}
							_ = 0;
							goto IL_06e9;
						}
					}
					goto IL_274f;
				}
				_003C_003E1__state = -1;
				num3 = ++_003Cf_003E5__5;
				if ((object)_003C_003E4__this != null)
				{
					num4 = 0f;
					list5 = null;
					goto IL_27d0;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
						object obj9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v701 @ rax_v276+18]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
							if (!((UnityEngine.Object)0 != null))
							{
								goto IL_08a2;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
							object obj10 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
							bool flag12 = (nint)0 == 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
							obj2 = (UnityEngine.Object)0;
							if (!flag12)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+A0]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ rbx_v87+38]");
								bool flag13 = (nint)0 == 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ rbx_v87+38]");
								obj2 = (UnityEngine.Object)0;
								if (!flag13)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ rbx_v87+38]");
									((Dictionary<ArticlePoolDefinition, List<GameObject>>)0).Clear();
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ rbx_v87+21]");
									if ((nint)0 != 0)
									{
										Debug.Log("[ArticlePoolQueueManager] BeginPass — pass decks cleared.");
									}
									goto IL_08a2;
								}
							}
							goto IL_261b;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+98]");
					if ((nint)0 != 0)
					{
						Debug.LogWarning("[ArticleController] No ArticleColumns assigned.", _003C_003E4__this);
					}
					goto IL_06e9;
				}
			}
			goto IL_261b;
			IL_0b3d:
			List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
			enumerator.Dispose();
			Vector2 vector2 = default(Vector2);
			Vector2 vector = vector2;
			List<GameObject>.Enumerator enumerator3 = default(List<GameObject>.Enumerator);
			List<ArticleColumn>.Enumerator enumerator2 = (List<ArticleColumn>.Enumerator)enumerator3;
			list3 = list5;
			goto IL_0b93;
			IL_252a:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
				bool flag14 = (nint)0 == 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
				obj2 = (UnityEngine.Object)0;
				if (flag14)
				{
					goto IL_261b;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
				((ArticlePoolQueueManager)0).EndPass();
			}
			goto IL_06e9;
			IL_06e9:
			return false;
			IL_290f:
			int capacity;
			List<GameObject> list6 = new List<GameObject>(capacity);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+50]");
			bool flag15 = (nint)0 == 0;
			vector = vector2;
			List<ArticleColumn>.Enumerator enumerator4 = default(List<ArticleColumn>.Enumerator);
			enumerator2 = enumerator4;
			obj2 = (UnityEngine.Object)(object)list6;
			int num5;
			HashSet<GameObject> hashSet;
			if (!flag15)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<GameObject>.Enumerator enumerator5 = default(List<GameObject>.Enumerator);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (!((UnityEngine.Object)enumerator5 != null))
					{
						continue;
					}
					bool flag16 = list6 == null;
					obj2 = (UnityEngine.Object)enumerator5;
					if (!flag16)
					{
						if (list6._size < num5)
						{
							bool flag17 = hashSet == null;
							obj2 = (UnityEngine.Object)enumerator5;
							if (!flag17)
							{
								if (!hashSet.Contains((GameObject)enumerator5))
								{
									list6.Add((GameObject)enumerator5);
									hashSet.Add((GameObject)enumerator5);
								}
								continue;
							}
							throw new NullReferenceException();
						}
						goto IL_0b3d;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				vector = vector2;
				enumerator2 = (List<ArticleColumn>.Enumerator)enumerator3;
				list3 = list5;
				obj2 = (UnityEngine.Object)(&enumerator);
			}
			if (list6 != null)
			{
				goto IL_0b93;
			}
			goto IL_261b;
			IL_27d0:
			int num6 = num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+4C]");
			if ((nint)num6 < (nint)0)
			{
				_003C_003E2__current = list5;
				_003C_003E1__state = 1;
				return true;
			}
			List<(GameObject, GameObject)> list7 = _003Cstaged_003E5__2;
			ISet<GameObject> set = default(ISet<GameObject>);
			nint num8;
			List<ArticleNewspaperPacker.Candidate> list12;
			if (_003Cstaged_003E5__2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ rbx_v13 (System.Collections.Generic.List`1<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>)+18]");
				List<ArticleNewspaperPacker.Candidate> list8 = new List<ArticleNewspaperPacker.Candidate>(0);
				obj2 = (UnityEngine.Object)(object)_003Cstaged_003E5__2;
				if (_003Cstaged_003E5__2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<(GameObject, GameObject)>.Enumerator enumerator7 = default(List<(GameObject, GameObject)>.Enumerator);
					List<(GameObject, GameObject)>.Enumerator enumerator6 = enumerator7;
					List<ArticleNewspaperPacker.Candidate> list9 = list8;
					List<(GameObject, GameObject)>.Enumerator enumerator8 = default(List<(GameObject, GameObject)>.Enumerator);
					UnityEngine.Object obj11 = default(UnityEngine.Object);
					UnityEngine.Object obj14 = default(UnityEngine.Object);
					List<(GameObject, GameObject)>.Enumerator enumerator9 = default(List<(GameObject, GameObject)>.Enumerator);
					object arg5 = default(object);
					while (enumerator8.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (!(obj11 != null))
						{
							continue;
						}
						bool flag18 = (object)obj11 == null;
						UnityEngine.Object obj12 = obj11;
						float num7;
						float measuredHeight;
						string text3;
						UnityEngine.Object obj15;
						if (!flag18)
						{
							Transform transform = ((GameObject)obj11).transform;
							bool flag19 = (object)transform == null;
							UnityEngine.Object obj13 = (UnityEngine.Object)(object)list5;
							if (!flag19)
							{
								bool flag20 = (object)transform.GetType() != typeof(RectTransform);
								obj13 = (UnityEngine.Object)(object)list5;
								if (!flag20)
								{
									obj13 = transform;
								}
							}
							if (obj13 != null)
							{
								RebuildLayoutBottomUp((RectTransform)obj13);
							}
							if (obj13 != null)
							{
								num7 = LayoutUtility.GetPreferredHeight((RectTransform)obj13);
								bool flag21 = num4 < num7;
								measuredHeight = num7;
								if (flag21)
								{
									goto IL_156b;
								}
								if ((object)_003C_003E4__this == null)
								{
									throw new NullReferenceException();
								}
								measuredHeight = num7;
							}
							else
							{
								num7 = (float)enumerator6;
								measuredHeight = num4;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+98]");
							if ((nint)0 == 0)
							{
								goto IL_156b;
							}
							if ((object)obj14 != null)
							{
								string name2 = obj14.name;
								string message2 = "[ArticleController] Staged instance of '" + name2 + "' reports zero preferred height. Verify it has a VerticalLayoutGroup on its root with children that have preferred heights. If using nested layout groups, try increasing 'Staging Settle Frames'.";
								Debug.LogWarning(message2, _003C_003E4__this);
								enumerator6 = (List<(GameObject, GameObject)>.Enumerator)num7;
								text3 = null;
								obj15 = obj14;
								goto IL_2bcc;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
						IL_2bcc:
						if ((object)obj15 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							if ((UnityEngine.Object)enumerator9 == null)
							{
								if ((object)_003C_003E4__this == null)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+98]");
								if ((nint)0 != 0)
								{
									string name3 = obj15.name;
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									string text4 = $"using default priority ({arg5}) and reusable = false.";
									string message3 = "[ArticleController] Prefab '" + name3 + "' has no ArticleMetadata component — " + text4;
									Debug.LogWarning(message3, _003C_003E4__this);
									text3 = text4;
								}
							}
							ArticleNewspaperPacker.Candidate candidate = new ArticleNewspaperPacker.Candidate();
							if (candidate != null)
							{
								candidate.Prefab = (GameObject)obj15;
								candidate.MeasuredHeight = measuredHeight;
								int priority;
								if ((UnityEngine.Object)enumerator9 != null)
								{
									if ((object)enumerator9 == null)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2224 @ stack_-208_v29 (System.Collections.Generic.List`1<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>+Enumerator<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>)+20]");
									priority = 0;
								}
								else
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+38]");
									priority = 0;
								}
								candidate.Priority = priority;
								List<ArticleNewspaperPacker.Candidate> list10;
								if (!((UnityEngine.Object)enumerator9 != null))
								{
									list10 = list5;
								}
								else
								{
									if ((object)enumerator9 == null)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2224 @ stack_-208_v29 (System.Collections.Generic.List`1<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>+Enumerator<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>)+24]");
									list10 = (List<ArticleNewspaperPacker.Candidate>)0;
								}
								bool flag22 = list10 == null;
								bool reusable = !flag22;
								candidate.Reusable = reusable;
								int maxColumnsPerPass;
								if ((UnityEngine.Object)enumerator9 != null)
								{
									if ((object)enumerator9 == null)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2224 @ stack_-208_v29 (System.Collections.Generic.List`1<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>+Enumerator<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>)+28]");
									maxColumnsPerPass = 0;
								}
								else
								{
									maxColumnsPerPass = 1;
								}
								candidate.MaxColumnsPerPass = maxColumnsPerPass;
								if (list8 != null)
								{
									list8.Add(candidate);
									list9 = list8;
									set = (ISet<GameObject>)(object)text3;
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
						IL_156b:
						enumerator6 = (List<(GameObject, GameObject)>.Enumerator)num7;
						text3 = (string)(object)set;
						obj15 = obj14;
						goto IL_2bcc;
					}
					enumerator8.Dispose();
					obj2 = (UnityEngine.Object)(object)_003Cstaged_003E5__2;
					if (_003Cstaged_003E5__2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						list3 = list8;
						List<(GameObject, GameObject)>.Enumerator enumerator10 = default(List<(GameObject, GameObject)>.Enumerator);
						while (enumerator10.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if ((UnityEngine.Object)(object)list3 != null)
							{
								UnityEngine.Object.Destroy((UnityEngine.Object)(object)list3);
							}
						}
						enumerator10.Dispose();
						List<(GameObject, GameObject)> list11 = _003Cstaged_003E5__2;
						bool flag23 = _003Cstaged_003E5__2 == null;
						obj2 = (UnityEngine.Object)(&enumerator10);
						if (!flag23)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v958 @ rbx_v17 (System.Collections.Generic.List`1<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>)+1C]");
							_ = (nint)0 + (nint)1;
							((List<(GameObject, GameObject)>.Enumerator*)null)->Dispose();
							object obj16 = default(object);
							if (obj16 == null)
							{
								num8 = (nint)set;
								obj2 = (UnityEngine.Object)0;
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v958 @ rbx_v17 (System.Collections.Generic.List`1<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>)+18]");
								bool flag24 = (nint)0 <= (nint)0;
								num8 = (nint)set;
								obj2 = (UnityEngine.Object)0;
								if (!flag24)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v958 @ rbx_v17 (System.Collections.Generic.List`1<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>)+10]");
									nint num9 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v958 @ rbx_v17 (System.Collections.Generic.List`1<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>)+18]");
									Array.Clear((Array)num9, 0, 0);
									num8 = unchecked((nint)null);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v958 @ rbx_v17 (System.Collections.Generic.List`1<System.ValueTuple`2<UnityEngine.GameObject, UnityEngine.GameObject>>)+10]");
									obj2 = (UnityEngine.Object)0;
								}
							}
							if (list9 != null)
							{
								list12 = new List<ArticleNewspaperPacker.Candidate>(list9._size);
								List<ArticleNewspaperPacker.Candidate> list13 = new List<ArticleNewspaperPacker.Candidate>();
								List<ArticleNewspaperPacker.Candidate> list14 = new List<ArticleNewspaperPacker.Candidate>();
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator12 = default(List<ArticleNewspaperPacker.Candidate>.Enumerator);
								List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator11 = enumerator12;
								List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator13 = default(List<ArticleNewspaperPacker.Candidate>.Enumerator);
								ArticleNewspaperPacker.Candidate candidate2 = default(ArticleNewspaperPacker.Candidate);
								while (enumerator13.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									bool flag25 = candidate2 == null;
									UnityEngine.Object obj12 = (UnityEngine.Object)(&enumerator13);
									if (!flag25)
									{
										if (candidate2.Reusable)
										{
											enumerator11 = (List<ArticleNewspaperPacker.Candidate>.Enumerator)candidate2.MeasuredHeight;
											if (candidate2.MeasuredHeight > num4)
											{
												bool flag26 = list13 == null;
												obj12 = (UnityEngine.Object)(&enumerator13);
												if (!flag26)
												{
													list13.Add(candidate2);
													continue;
												}
												throw new NullReferenceException();
											}
										}
										bool flag27 = list14 == null;
										obj12 = (UnityEngine.Object)(&enumerator13);
										if (!flag27)
										{
											list14.Add(candidate2);
											continue;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								enumerator13.Dispose();
								bool flag28 = list12 == null;
								obj2 = (UnityEngine.Object)(&enumerator13);
								if (!flag28)
								{
									list12.AddRange(list14);
									list12.AddRange(list13);
									bool flag29 = list13 == null;
									obj2 = (UnityEngine.Object)(object)list12;
									if (!flag29)
									{
										bool flag30 = list13._size <= 0;
										Vector2 vector3 = vector2;
										obj2 = (UnityEngine.Object)(object)list12;
										if (flag30)
										{
											goto IL_1def;
										}
										bool flag31 = (object)_003C_003E4__this == null;
										obj2 = (UnityEngine.Object)(object)list12;
										if (!flag31)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
											object obj17 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
											bool flag32 = (nint)0 == 0;
											obj2 = (UnityEngine.Object)(object)list12;
											if (!flag32)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
												list4 = list5;
												List<ArticleNewspaperPacker.Candidate> list15 = list5;
												List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator14 = default(List<ArticleNewspaperPacker.Candidate>.Enumerator);
												List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator15 = default(List<ArticleNewspaperPacker.Candidate>.Enumerator);
												while (enumerator14.MoveNext())
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
													bool flag33 = (object)enumerator15 == null;
													UnityEngine.Object obj12 = (UnityEngine.Object)(&enumerator14);
													if (!flag33)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4181 @ stack_-208_v28 (System.Collections.Generic.List`1<ArticleSystem.ArticleNewspaperPacker+Candidate>+Enumerator<ArticleSystem.ArticleNewspaperPacker+Candidate>)+24]");
														bool flag34 = (nint)0 < (nint)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4181 @ stack_-208_v28 (System.Collections.Generic.List`1<ArticleSystem.ArticleNewspaperPacker+Candidate>+Enumerator<ArticleSystem.ArticleNewspaperPacker+Candidate>)+24]");
														ArticleNewspaperPacker.Candidate candidate3;
														if ((nint)0 <= (nint)0)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v816 @ rdi_v26+18]");
															candidate3 = (ArticleNewspaperPacker.Candidate)0;
														}
														else
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4181 @ stack_-208_v28 (System.Collections.Generic.List`1<ArticleSystem.ArticleNewspaperPacker+Candidate>+Enumerator<ArticleSystem.ArticleNewspaperPacker+Candidate>)+24]");
															candidate3 = (ArticleNewspaperPacker.Candidate)0;
														}
														List<ArticleNewspaperPacker.Candidate> list16 = (List<ArticleNewspaperPacker.Candidate>)(candidate3 - 1);
														List<ArticleNewspaperPacker.Candidate> list17 = list5;
														if (!flag34)
														{
															list17 = list16;
														}
														if (System.Runtime.CompilerServices.Unsafe.As<List<ArticleNewspaperPacker.Candidate>, UIntPtr>(ref list17) > System.Runtime.CompilerServices.Unsafe.As<List<ArticleNewspaperPacker.Candidate>, UIntPtr>(ref list15))
														{
															list4 = list17;
															list15 = list17;
														}
														continue;
													}
													throw new NullReferenceException();
												}
												enumerator14.Dispose();
												vector3 = vector2;
												List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator16 = default(List<ArticleNewspaperPacker.Candidate>.Enumerator);
												enumerator11 = enumerator16;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v816 @ rdi_v26+18]");
												ArticleNewspaperPacker.Candidate candidate4 = (ArticleNewspaperPacker.Candidate)0;
												list3 = list5;
												List<ArticleNewspaperPacker.Candidate> list18 = list5;
												obj2 = (UnityEngine.Object)(&enumerator14);
												List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator17 = default(List<ArticleNewspaperPacker.Candidate>.Enumerator);
												List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator18 = default(List<ArticleNewspaperPacker.Candidate>.Enumerator);
												Vector2 vector4 = default(Vector2);
												List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator19 = default(List<ArticleNewspaperPacker.Candidate>.Enumerator);
												while (System.Runtime.CompilerServices.Unsafe.As<List<ArticleNewspaperPacker.Candidate>, UIntPtr>(ref list18) < System.Runtime.CompilerServices.Unsafe.As<List<ArticleNewspaperPacker.Candidate>, UIntPtr>(ref list15))
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
													while (enumerator17.MoveNext())
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
														bool flag35 = (object)enumerator18 == null;
														ArticleNewspaperPacker.Candidate candidate5 = (ArticleNewspaperPacker.Candidate)(&enumerator17);
														if (!flag35)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4644 @ stack_-208_v27 (System.Collections.Generic.List`1<ArticleSystem.ArticleNewspaperPacker+Candidate>+Enumerator<ArticleSystem.ArticleNewspaperPacker+Candidate>)+24]");
															bool flag36 = (nint)0 < (nint)0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4644 @ stack_-208_v27 (System.Collections.Generic.List`1<ArticleSystem.ArticleNewspaperPacker+Candidate>+Enumerator<ArticleSystem.ArticleNewspaperPacker+Candidate>)+24]");
															bool flag37 = (nint)0 <= (nint)0;
															ArticleNewspaperPacker.Candidate candidate6 = candidate4;
															if (!flag37)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4644 @ stack_-208_v27 (System.Collections.Generic.List`1<ArticleSystem.ArticleNewspaperPacker+Candidate>+Enumerator<ArticleSystem.ArticleNewspaperPacker+Candidate>)+24]");
																candidate6 = (ArticleNewspaperPacker.Candidate)0;
															}
															List<ArticleNewspaperPacker.Candidate> list19 = (List<ArticleNewspaperPacker.Candidate>)(candidate6 - 1);
															List<ArticleNewspaperPacker.Candidate> list20 = list5;
															if (!flag36)
															{
																list20 = list19;
															}
															if (System.Runtime.CompilerServices.Unsafe.As<List<ArticleNewspaperPacker.Candidate>, UIntPtr>(ref list18) < System.Runtime.CompilerServices.Unsafe.As<List<ArticleNewspaperPacker.Candidate>, UIntPtr>(ref list20))
															{
																ArticleNewspaperPacker.Candidate candidate7 = new ArticleNewspaperPacker.Candidate();
																if (candidate7 == null)
																{
																	throw new NullReferenceException();
																}
																candidate7.Prefab = (GameObject)(object)enumerator18._list;
																candidate7.MeasuredHeight = enumerator18._index;
																candidate7.Priority = enumerator18._version;
																candidate7.Reusable = true;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4644 @ stack_-208_v27 (System.Collections.Generic.List`1<ArticleSystem.ArticleNewspaperPacker+Candidate>+Enumerator<ArticleSystem.ArticleNewspaperPacker+Candidate>)+24]");
																candidate7.MaxColumnsPerPass = 0;
																list12.Add(candidate7);
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v816 @ rdi_v26+18]");
																candidate4 = (ArticleNewspaperPacker.Candidate)0;
															}
															continue;
														}
														throw new NullReferenceException();
													}
													enumerator17.Dispose();
													list18 = (List<ArticleNewspaperPacker.Candidate>)(list18 + 1);
													vector3 = vector4;
													enumerator11 = enumerator19;
													list3 = list5;
													list15 = list4;
													obj2 = (UnityEngine.Object)(&enumerator17);
												}
												goto IL_1def;
											}
										}
									}
								}
							}
						}
					}
					goto IL_274f;
				}
			}
			goto IL_261b;
			IL_0b93:
			int num10 = num5 - list6._size;
			if (num10 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
					bool flag38 = (nint)0 == 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
					obj2 = (UnityEngine.Object)0;
					if (flag38)
					{
						goto IL_261b;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
					nint num11 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+A0]");
					List<GameObject> collection = ((ArticlePoolQueueManager)num11).RequestSpecialPicks(num10, (System.Random)0, hashSet);
					list6.AddRange(collection);
					set = hashSet;
				}
			}
			object obj18 = num5 - list6._size;
			if ((nint)obj18 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+68]");
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
					bool flag39 = (UnityEngine.Object)0 != null;
					if (!flag39)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+98]");
						if ((nint)0 != (flag39 ? 1 : 0))
						{
							Debug.LogWarning("[ArticleController] Fallback pool is assigned but no ArticlePoolQueueManager was found — pool will not be sampled.", _003C_003E4__this);
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
						bool flag40 = (nint)0 == 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+A0]");
						obj2 = (UnityEngine.Object)0;
						if (flag40)
						{
							goto IL_261b;
						}
						List<GameObject> list21 = new List<GameObject>();
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+68]");
						bool flag41 = (UnityEngine.Object)0 != null;
						List<GameObject> collection2 = list21;
						if (flag41)
						{
							List<ArticleNewspaperPacker.Candidate> list22 = list5;
							bool advanceSequentialOnSuccess = default(bool);
							while (true)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+58]");
								nint num12 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+68]");
								nint num13 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+A0]");
								GameObject gameObject = ((ArticlePoolQueueManager)num12).TryPickFromPool((ArticlePoolDefinition)num13, (System.Random)0, (ISet<GameObject>)hashSet, advanceSequentialOnSuccess);
								bool flag42 = gameObject != null;
								set = hashSet;
								if (!flag42)
								{
									break;
								}
								bool flag43 = list21 == null;
								obj2 = gameObject;
								if (!flag43)
								{
									list21.Add(gameObject);
									bool flag44 = hashSet == null;
									obj2 = (UnityEngine.Object)(object)list21;
									if (!flag44)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
										List<ArticleNewspaperPacker.Candidate> list23 = (List<ArticleNewspaperPacker.Candidate>)(list22 + 1);
										bool flag45 = System.Runtime.CompilerServices.Unsafe.As<List<ArticleNewspaperPacker.Candidate>, UIntPtr>(ref list23) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj18);
										set = (ISet<GameObject>)(object)gameObject;
										list22 = list23;
										if (!flag45)
										{
											break;
										}
										continue;
									}
								}
								goto IL_261b;
							}
							collection2 = list21;
						}
						list6.AddRange(collection2);
					}
				}
			}
			object message4;
			if (list6._size != 0)
			{
				RectTransform orCreateStagingParent = _003C_003E4__this.GetOrCreateStagingParent();
				if (orCreateStagingParent != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+48]");
					float num14 = 0f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+48]");
					Vector2 vector5 = default(Vector2);
					if ((nint)0 >= (nint)0)
					{
						Canvas.ForceUpdateCanvases();
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
						obj2 = (UnityEngine.Object)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
						if ((nint)0 == 0)
						{
							goto IL_261b;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						List<ArticleColumn>.Enumerator enumerator20 = default(List<ArticleColumn>.Enumerator);
						List<ArticleColumn>.Enumerator enumerator21 = default(List<ArticleColumn>.Enumerator);
						while (true)
						{
							if (enumerator20.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								if (!((UnityEngine.Object)enumerator21 != null))
								{
									continue;
								}
								bool flag46 = (object)enumerator21 == null;
								obj2 = (UnityEngine.Object)enumerator21;
								if (!flag46)
								{
									Transform transform2 = ((Component)enumerator21).transform;
									if ((object)transform2 != null)
									{
										bool flag47 = (object)transform2.GetType() != typeof(RectTransform);
										RectTransform rectTransform = (RectTransform)(object)list5;
										if (!flag47)
										{
											rectTransform = (RectTransform)transform2;
										}
										if ((object)rectTransform != null)
										{
											Rect rect = rectTransform.rect;
											num14 = (float)vector5;
											goto IL_2aa8;
										}
									}
									num14 = 0f;
									goto IL_2aa8;
								}
								throw new NullReferenceException();
							}
							enumerator20.Dispose();
							break;
							IL_2aa8:
							if (num14 > 0f)
							{
								enumerator20.Dispose();
								break;
							}
						}
					}
					List<(GameObject, GameObject)> list24 = new List<(GameObject, GameObject)>(list6._size);
					_003Cstaged_003E5__2 = list24;
					RectTransform parent = orCreateStagingParent;
					list2 = list6;
					obj2 = (UnityEngine.Object)(object)list5;
					List<ArticleNewspaperPacker.Candidate> list25;
					UnityEngine.Object obj20;
					HashSet<GameObject> hashSet2 = default(HashSet<GameObject>);
					Quaternion quaternion = default(Quaternion);
					for (list25 = list5; (nint)obj2 < list6._size; obj20 = (UnityEngine.Object)(list25 + 1), parent = orCreateStagingParent, obj2 = obj20, list25 = (List<ArticleNewspaperPacker.Candidate>)(object)obj20)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						bool flag48 = obj6 != null;
						set = (ISet<GameObject>)0;
						if (!flag48)
						{
							continue;
						}
						GameObject gameObject2 = UnityEngine.Object.Instantiate((GameObject)obj6, parent);
						bool flag49 = (object)obj6 == null;
						obj2 = obj6;
						if (!flag49)
						{
							string name4 = obj6.name;
							string name5 = "[Staging] " + name4;
							bool flag50 = (object)gameObject2 == null;
							obj2 = (UnityEngine.Object)(object)"[Staging] ";
							if (!flag50)
							{
								gameObject2.name = name5;
								Transform transform3 = gameObject2.transform;
								bool flag51 = (object)transform3 == null;
								UnityEngine.Object obj19 = (UnityEngine.Object)(object)list5;
								if (!flag51)
								{
									bool flag52 = (object)transform3.GetType() != typeof(RectTransform);
									obj19 = (UnityEngine.Object)(object)list5;
									if (!flag52)
									{
										obj19 = transform3;
									}
								}
								if (obj19 != null)
								{
									obj2 = obj19;
									if ((object)obj19 == null)
									{
										goto IL_261b;
									}
									((Transform)obj19).localScale = (Vector3)(&hashSet2);
									((Transform)obj19).localRotation = (Quaternion)(&quaternion);
									((RectTransform)obj19).anchorMin = vector5;
									((RectTransform)obj19).anchorMax = vector5;
									((RectTransform)obj19).pivot = vector5;
									((RectTransform)obj19).anchoredPosition = vector5;
									if (num14 > 0f)
									{
										Vector2 sizeDelta = ((RectTransform)obj19).sizeDelta;
										((RectTransform)obj19).sizeDelta = vector5;
									}
								}
								(GameObject, GameObject) tuple = (gameObject2, (GameObject)obj6);
								bool flag53 = _003Cstaged_003E5__2 == null;
								obj2 = (UnityEngine.Object)(&tuple);
								if (!flag53)
								{
									_003Cstaged_003E5__2.Add(((GameObject, GameObject))(&list2));
									set = (ISet<GameObject>)0;
									continue;
								}
								throw new NullReferenceException();
							}
						}
						goto IL_261b;
					}
					_003Cf_003E5__5 = (int)list5;
					num3 = _003Cf_003E5__5;
					List<ArticleNewspaperPacker.Candidate> list22 = list25;
					num4 = 0f;
					goto IL_27d0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+98]");
				if ((nint)0 == 0)
				{
					goto IL_252a;
				}
				message4 = "[ArticleController] Could not obtain or create a staging parent — aborting population.";
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+98]");
				if ((nint)0 == 0)
				{
					goto IL_252a;
				}
				message4 = "[ArticleController] No candidates gathered from pools — nothing to place.";
			}
			Debug.LogWarning(message4, _003C_003E4__this);
			goto IL_252a;
			IL_274f:
			throw new NullReferenceException();
			IL_261b:
			throw new NullReferenceException();
			IL_08a2:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+30]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
				obj2 = (UnityEngine.Object)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
				if ((nint)0 == 0)
				{
					goto IL_261b;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<ArticleColumn>.Enumerator enumerator22 = default(List<ArticleColumn>.Enumerator);
				ArticleColumn articleColumn2 = default(ArticleColumn);
				while (enumerator22.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					articleColumn2?.Clear();
				}
				enumerator22.Dispose();
				list5 = null;
			}
			else
			{
				list5 = null;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
			obj2 = (UnityEngine.Object)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
			if ((nint)0 == 0)
			{
				goto IL_261b;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ArticleColumn>.Enumerator enumerator23 = default(List<ArticleColumn>.Enumerator);
			ArticleColumn articleColumn3 = default(ArticleColumn);
			while (enumerator23.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				articleColumn3?.BeginPopulation();
			}
			enumerator23.Dispose();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+34]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+34]");
				capacity = 0;
				HashSet<GameObject> hashSet3 = new HashSet<GameObject>();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+34]");
				bool flag54 = (nint)0 == 2147483647;
				hashSet = hashSet3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+34]");
				num5 = 0;
				if (!flag54)
				{
					hashSet = hashSet3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+34]");
					num5 = 0;
					goto IL_290f;
				}
			}
			else
			{
				HashSet<GameObject> hashSet4 = new HashSet<GameObject>();
				hashSet = hashSet4;
				num5 = 2147483647;
			}
			capacity = 16;
			goto IL_290f;
			IL_1def:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
			object obj21 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v960 @ rbx_v21+18]");
				List<ArticleNewspaperPacker.ColumnState> list26 = new List<ArticleNewspaperPacker.ColumnState>(0);
				_003CpackerColumns_003E5__3 = list26;
				obj2 = (UnityEngine.Object)(this + 48);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
				object obj22 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v961 @ rbx_v23+18]");
					List<int> list27 = new List<int>(0);
					_003CcolumnIndexMap_003E5__4 = list27;
					obj2 = (UnityEngine.Object)(this + 56);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
					object obj23 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v962 @ rbx_v25+18]");
						List<int> list28 = new List<int>(0);
						List<ArticleNewspaperPacker.Candidate> list29 = list5;
						obj2 = (UnityEngine.Object)(object)list5;
						UnityEngine.Object obj26 = default(UnityEngine.Object);
						ArticleNewspaperPacker.Candidate candidate8 = default(ArticleNewspaperPacker.Candidate);
						int index = default(int);
						List<int>.Enumerator enumerator24 = default(List<int>.Enumerator);
						ArticleColumn articleColumn4 = default(ArticleColumn);
						List<ArticleNewspaperPacker.Candidate>.Enumerator enumerator25 = default(List<ArticleNewspaperPacker.Candidate>.Enumerator);
						while (true)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
							object obj24 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
							if ((nint)0 == 0)
							{
								break;
							}
							UnityEngine.Object obj25 = obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1391 @ rax_v61+18]");
							if ((nint)obj25 < 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								if (obj26 != null)
								{
									bool flag55 = list28 == null;
									obj2 = obj26;
									if (flag55)
									{
										break;
									}
									list28.Add((int)(&candidate8));
								}
								UnityEngine.Object obj27 = (UnityEngine.Object)(list29 + 1);
								num8 = 0;
								list29 = (List<ArticleNewspaperPacker.Candidate>)(object)obj27;
								obj2 = obj27;
								continue;
							}
							if (list28 == null)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6573 @ rax_v58 (System.Collections.Generic.List`1<System.Int32>)+18]");
							int num15 = (int)(-1);
							bool flag56 = num15 <= 0;
							nint num16 = num8;
							if (!flag56)
							{
								while (true)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+A0]");
									obj2 = (UnityEngine.Object)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+A0]");
									if ((nint)0 == 0)
									{
										break;
									}
									nint num17 = (nint)obj2;
									object obj28 = num15 + 1;
									Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v6925 @ r8_v29 (Il2CppClass<UnityEngine.Object>)+1A8] (should have been resolved before IL gen)");
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									list28.set_Item(num15, (int)(&list4));
									list28.set_Item(index, (int)(&list4));
									num15--;
									bool flag57 = num15 > 0;
									num16 = 0;
									if (flag57)
									{
										continue;
									}
									goto IL_20c7;
								}
								break;
							}
							goto IL_20c7;
							IL_20c7:
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							while (enumerator24.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
								List<int> list30 = (List<int>)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									ArticleNewspaperPacker.ColumnState columnState = new ArticleNewspaperPacker.ColumnState();
									List<ArticleNewspaperPacker.Candidate> assigned = new List<ArticleNewspaperPacker.Candidate>();
									columnState.Assigned = assigned;
									HashSet<GameObject> placedInColumn = new HashSet<GameObject>();
									columnState.PlacedInColumn = placedInColumn;
									bool flag58 = (object)articleColumn4 == null;
									list30 = (List<int>)(object)columnState;
									if (!flag58)
									{
										bool flag59 = columnState == null;
										list30 = (List<int>)(object)columnState;
										if (!flag59)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v735 @ stack_-168_v14 (ArticleSystem.ArticleColumn)+2C]");
											columnState.CapacityHeight = 0f;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v735 @ stack_-168_v14 (ArticleSystem.ArticleColumn)+30]");
											columnState.UsedHeight = 0f;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v735 @ stack_-168_v14 (ArticleSystem.ArticleColumn)+34]");
											columnState.ArticleSpacing = 0f;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v735 @ stack_-168_v14 (ArticleSystem.ArticleColumn)+28]");
											columnState.FillTolerance = 0f;
											bool flag60 = _003CpackerColumns_003E5__3 == null;
											list30 = (List<int>)(object)columnState;
											if (!flag60)
											{
												_003CpackerColumns_003E5__3.Add(columnState);
												list30 = _003CcolumnIndexMap_003E5__4;
												if (_003CcolumnIndexMap_003E5__4 != null)
												{
													_003CcolumnIndexMap_003E5__4.Add((int)(&list4));
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
							enumerator24.Dispose();
							ArticleNewspaperPacker.PackOptions packOptions = new ArticleNewspaperPacker.PackOptions();
							bool flag61 = packOptions == null;
							obj2 = (UnityEngine.Object)(object)packOptions;
							if (flag61)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+3C]");
							packOptions.ShuffleColumnOrder = false;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+3D]");
							packOptions.PinHighestPriorityToTop = false;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+A0]");
							packOptions.Rng = (System.Random)0;
							ArticleNewspaperPacker.Pack(list12, _003CpackerColumns_003E5__3, packOptions);
							List<ArticleNewspaperPacker.Candidate> list31 = list5;
							obj2 = (UnityEngine.Object)(object)list12;
							while (true)
							{
								List<ArticleNewspaperPacker.ColumnState> list32 = _003CpackerColumns_003E5__3;
								if (_003CpackerColumns_003E5__3 == null)
								{
									break;
								}
								if ((nint)list31 < list32._size)
								{
									if ((object)_003C_003E4__this == null)
									{
										break;
									}
									obj2 = (UnityEngine.Object)(object)_003CcolumnIndexMap_003E5__4;
									if (_003CcolumnIndexMap_003E5__4 == null)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r14_v1 (UnityEngine.Object)+20]");
									if ((nint)0 == 0)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									obj2 = (UnityEngine.Object)(object)_003CpackerColumns_003E5__3;
									if (_003CpackerColumns_003E5__3 == null)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									bool flag62 = (object)obj6 == null;
									obj2 = obj6;
									if (flag62)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ stack_-208 (UnityEngine.Object)+20]");
									obj2 = (UnityEngine.Object)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ stack_-208 (UnityEngine.Object)+20]");
									if ((nint)0 == 0)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
									while (enumerator25.MoveNext())
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
										bool flag63 = list3 == null;
										List<int> list30 = (List<int>)(&enumerator25);
										if (!flag63)
										{
											bool flag64 = (object)articleColumn4 == null;
											list30 = (List<int>)(&enumerator25);
											if (!flag64)
											{
												articleColumn4.PlaceArticle((GameObject)(object)list3._items, list3._size);
												continue;
											}
											throw new NullReferenceException();
										}
										throw new NullReferenceException();
									}
									enumerator25.Dispose();
									bool flag65 = (object)articleColumn4 == null;
									obj2 = (UnityEngine.Object)(&enumerator25);
									if (flag65)
									{
										break;
									}
									articleColumn4.FlushLayout();
									list31 = (List<ArticleNewspaperPacker.Candidate>)(list31 + 1);
									obj2 = articleColumn4;
									continue;
								}
								_003C_003E2__current = list5;
								_003C_003E1__state = 2;
								return true;
							}
							break;
						}
					}
				}
			}
			goto IL_274f;
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

	private List<ArticleColumn> columns;

	private Transform columnsRoot;

	private bool clearColumnsBeforePopulate;

	private int maxCandidatesGathered;

	private int defaultPriority;

	private bool shuffleColumnOrder;

	private bool pinHighestPriorityToTop;

	private RectTransform stagingParent;

	private float stagingWidth;

	private int stagingSettleFrames;

	private List<GameObject> guaranteedInstanceArticles;

	private ArticlePoolQueueManager poolQueueManager;

	private bool autoLocatePoolQueueManager;

	private ArticlePoolDefinition fallbackPool;

	private bool useFixedSeed;

	private int fixedSeed;

	private bool autoPopulateOnEnable;

	private bool autoPopulateAfterReseed;

	private InputActionReference populateAction;

	private InputActionReference reseedFromValueAction;

	private InputActionReference reseedRandomAction;

	private bool logWarnings;

	private bool logPackerResults;

	private bool debugRegenerate;

	private System.Random _rng;

	private int _currentSeed;

	private bool _hasExplicitSeed;

	private Coroutine _populationCoroutine;

	private Canvas _runtimeStagingCanvas;

	private void OnEnable()
	{
		//IL_00b2: Expected I4, but got I8
		if (autoLocatePoolQueueManager && poolQueueManager == null)
		{
			ArticlePoolQueueManager instance = ArticlePoolQueueManager.Instance;
			poolQueueManager = instance;
		}
		WireInputs(enable: true);
		int seedInternal = (useFixedSeed ? fixedSeed : (_hasExplicitSeed ? _currentSeed : UnityEngine.Random.Range(-2147483648, 2147483647)));
		SetSeedInternal(seedInternal);
		if (autoPopulateOnEnable)
		{
			PopulateNow();
		}
	}

	private void OnDisable()
	{
		WireInputs(enable: false);
		if (_populationCoroutine != null)
		{
			StopCoroutine(_populationCoroutine);
			_populationCoroutine = null;
		}
	}

	private void OnDestroy()
	{
		if (_runtimeStagingCanvas != null)
		{
			GameObject obj = _runtimeStagingCanvas.gameObject;
			UnityEngine.Object.Destroy(obj);
		}
	}

	private void OnValidate()
	{
		if (Application.isPlaying && debugRegenerate)
		{
			debugRegenerate = false;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 63 Invalid \"Jump target not found in method: 0x1804AF720\"");
		}
	}

	public void PopulateNow()
	{
		if (_populationCoroutine != null)
		{
			StopCoroutine(_populationCoroutine);
		}
		_003CPopulationCoroutine_003Ed__36 obj = new _003CPopulationCoroutine_003Ed__36(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine populationCoroutine = StartCoroutine(obj);
		_populationCoroutine = populationCoroutine;
	}

	public void ClearAllColumns()
	{
		if (columns != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<ArticleColumn>.Enumerator enumerator = default(List<ArticleColumn>.Enumerator);
			ArticleColumn articleColumn = default(ArticleColumn);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				articleColumn?.Clear();
			}
			enumerator.Dispose();
		}
	}

	private void CollectColumnsFromChildren()
	{
		//IL_007b: Expected O, but got I
		Transform transform;
		if (columnsRoot != null)
		{
			transform = columnsRoot;
		}
		else
		{
			Transform transform2 = base.transform;
			transform = transform2;
		}
		List<ArticleColumn> list = columns;
		int version = list._version + 1;
		list._version = version;
		((List<ArticleColumn>)0).Clear();
		object obj = default(object);
		if (obj == null)
		{
			list._size = 0;
		}
		else
		{
			list._size = 0;
			if (list._size > 0)
			{
				Array.Clear(list._items, 0, list._size);
			}
		}
		int num = 0;
		int num2 = 0;
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		while (true)
		{
			int childCount = transform.childCount;
			if (num < childCount)
			{
				Transform child = transform.GetChild(num2);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				if (obj2 != null)
				{
					columns.Add((ArticleColumn)obj2);
				}
				num2++;
				num = num2;
				continue;
			}
			break;
		}
	}

	private IEnumerator PopulationCoroutine()
	{
		_003CPopulationCoroutine_003Ed__36 obj = new _003CPopulationCoroutine_003Ed__36(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private static void RebuildLayoutBottomUp(RectTransform root)
	{
		int num = 0;
		int num2 = 0;
		while (true)
		{
			int childCount = root.childCount;
			if (num2 >= childCount)
			{
				break;
			}
			Transform child = root.GetChild(num);
			bool flag = (object)child == null;
			UnityEngine.Object obj = null;
			if (!flag)
			{
				bool flag2 = (object)child.GetType() != typeof(RectTransform);
				obj = null;
				if (!flag2)
				{
					obj = child;
				}
			}
			if (obj != null)
			{
				RebuildLayoutBottomUp((RectTransform)obj);
			}
			num++;
			num2 = num;
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(root);
	}

	private RectTransform GetOrCreateStagingParent()
	{
		//IL_00e0: Expected I4, but got I8
		if (stagingParent == null)
		{
			GameObject gameObject = new GameObject("ArticleController_StagingCanvas");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			if ((object)gameObject != null)
			{
				Canvas runtimeStagingCanvas = gameObject.AddComponent<Canvas>();
				_runtimeStagingCanvas = runtimeStagingCanvas;
				if ((object)_runtimeStagingCanvas != null)
				{
					_runtimeStagingCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
					if ((object)_runtimeStagingCanvas != null)
					{
						_runtimeStagingCanvas.sortingOrder = -9999;
						CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
						GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
						GameObject gameObject2 = new GameObject("StagingPanel");
						if ((object)gameObject2 != null)
						{
							Transform transform = gameObject2.transform;
							Transform parent = gameObject.transform;
							if ((object)transform != null)
							{
								transform.SetParent(parent, worldPositionStays: false);
								RectTransform rectTransform = gameObject2.AddComponent<RectTransform>();
								if ((object)rectTransform != null)
								{
									Vector2 vector = default(Vector2);
									rectTransform.anchorMin = vector;
									rectTransform.anchorMax = vector;
									rectTransform.pivot = vector;
									rectTransform.anchoredPosition = vector;
									rectTransform.sizeDelta = vector;
									stagingParent = rectTransform;
									return stagingParent;
								}
							}
						}
					}
				}
			}
			return (RectTransform)(object)new NullReferenceException();
		}
		return stagingParent;
	}

	private void WireInputs(bool enable)
	{
		Action<InputAction.CallbackContext> handler = OnPopulatePerformed;
		WireAction(populateAction, enable, handler);
		Action<InputAction.CallbackContext> handler2 = OnReseedFromValuePerformed;
		WireAction(reseedFromValueAction, enable, handler2);
		Action<InputAction.CallbackContext> handler3 = OnReseedRandomPerformed;
		WireAction(reseedRandomAction, enable, handler3);
	}

	private static void WireAction(InputActionReference reference, bool enable, Action<InputAction.CallbackContext> handler)
	{
		if ((object)reference == null)
		{
			return;
		}
		InputAction action = reference.action;
		if (action == null)
		{
			return;
		}
		InputAction action2 = reference.action;
		if (!enable)
		{
			action2.performed -= handler;
			if (action2.enabled)
			{
				action2.Disable();
			}
		}
		else
		{
			action2.performed += handler;
			if (!action2.enabled)
			{
				action2.Enable();
			}
		}
	}

	private void OnPopulatePerformed(InputAction.CallbackContext _)
	{
		PopulateNow();
	}

	private unsafe void OnReseedFromValuePerformed(InputAction.CallbackContext context)
	{
		object message;
		if (!useFixedSeed)
		{
			object value = ((InputAction.CallbackContext*)context)->ReadValueAsObject();
			if (TryComputeSeedFromObject(value, out var seed))
			{
				_hasExplicitSeed = true;
				SetSeedInternal(seed);
				if (autoPopulateAfterReseed)
				{
					PopulateNow();
				}
				return;
			}
			if (!logWarnings)
			{
				return;
			}
			message = "[ArticleController] ReseedFromValue received an unsupported value type — reseed ignored.";
		}
		else
		{
			if (!logWarnings)
			{
				return;
			}
			message = "[ArticleController] ReseedFromValue ignored — 'Use Fixed Seed' is enabled.";
		}
		Debug.LogWarning(message, this);
	}

	private void OnReseedRandomPerformed(InputAction.CallbackContext _)
	{
		//IL_0023: Expected I4, but got I8
		if (!useFixedSeed)
		{
			_hasExplicitSeed = true;
			int seedInternal = UnityEngine.Random.Range(-2147483648, 2147483647);
			SetSeedInternal(seedInternal);
			if (autoPopulateAfterReseed)
			{
				PopulateNow();
			}
		}
		else if (logWarnings)
		{
			Debug.LogWarning("[ArticleController] Random reseed ignored — 'Use Fixed Seed' is enabled.", this);
		}
	}

	private void ConfigureRngOnEnable()
	{
		//IL_0057: Expected I4, but got I8
		if (!useFixedSeed)
		{
			if (!_hasExplicitSeed)
			{
				int seedInternal = UnityEngine.Random.Range(-2147483648, 2147483647);
				SetSeedInternal(seedInternal);
			}
			else
			{
				SetSeedInternal(_currentSeed);
			}
		}
		else
		{
			SetSeedInternal(fixedSeed);
		}
	}

	private void SetSeedInternal(int seed)
	{
		_currentSeed = seed;
		System.Random rng = new System.Random(seed);
		_rng = rng;
	}

	private unsafe static bool TryComputeSeedFromObject(object value, out int seed)
	{
		//IL_0023: Expected O, but got I
		//IL_03c4: Expected I, but got O
		//IL_006c: Expected O, but got I
		//IL_0388: Expected I, but got O
		//IL_00b5: Expected O, but got I
		//IL_0322: Expected I, but got O
		//IL_00fe: Expected O, but got I
		//IL_02b9: Expected I, but got O
		//IL_0300: Expected F8, but got O
		//IL_030c: Expected Ref, but got F8
		//IL_01e8: Expected I, but got O
		//IL_01f8: Expected O, but got I
		//IL_022a: Expected O, but got I8
		//IL_0186: Expected O, but got I8
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected I4, but got Unknown
		//IL_0288: Expected O, but got I4
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected I4, but got Unknown
		//IL_01b7: Expected O, but got I4
		object obj = default(object);
		ref int reference;
		if (obj == null)
		{
			reference = ref *(int*)null;
			return false;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		bool flag = obj != null;
		object obj3 = null;
		if (!flag)
		{
			obj3 = obj;
		}
		if (obj3 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502A8]");
			obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502A8]");
			bool flag2 = obj != null;
			object obj4 = null;
			if (!flag2)
			{
				obj4 = obj;
			}
			if (obj4 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
				obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
				bool flag3 = obj != null;
				object obj5 = null;
				if (!flag3)
				{
					obj5 = obj;
				}
				if (obj5 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502C0]");
					obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502C0]");
					bool flag4 = obj != null;
					object obj6 = null;
					if (!flag4)
					{
						obj6 = obj;
					}
					if (obj6 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
						bool flag5 = obj != null;
						object obj7 = null;
						if (!flag5)
						{
							obj7 = obj;
						}
						if (obj7 != null)
						{
							int num = 0;
							object obj8 = 2166136261L;
							while (true)
							{
								int num2 = num;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rsi_v8 (System.Object)+10]");
								if ((nint)num2 >= (nint)0)
								{
									break;
								}
								char c = ((string)obj7).get_Chars(num);
								int num3 = c ^ obj8;
								obj8 = num3 * 16777619;
								num++;
							}
							reference = ref *(int*)obj8;
							return true;
						}
						nint num4 = (nint)obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v438 @ rdx_v12 (Il2CppClass<System.Object>)+170]");
						obj2 = 0;
						string text = obj.ToString();
						bool flag6 = text == null;
						int i = 0;
						object obj9 = 2166136261L;
						if (!flag6)
						{
							for (; i < text._stringLength; i++)
							{
								char c2 = text.get_Chars(i);
								int num5 = c2 ^ obj9;
								obj9 = num5 * 16777619;
							}
							reference = ref *(int*)obj9;
							return true;
						}
						goto IL_050b;
					}
					nint num6 = (nint)obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ rcx_v14 (Il2CppClass<System.Object>)+40]");
					nint num7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rdx_v4+40]");
					if (num7 == 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
						object obj10 = default(object);
						double num8 = Math.Round((double)obj10);
						reference = ref *(int*)num8;
						return true;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				}
				else
				{
					nint num9 = (nint)obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ rcx_v12 (Il2CppClass<System.Object>)+40]");
					nint num10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rdx_v4+40]");
					if (num10 == 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
						object obj11 = default(object);
						reference = ref *(int*)obj11;
						return true;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			}
			else
			{
				nint num11 = (nint)obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ rcx_v10 (Il2CppClass<System.Object>)+40]");
				nint num12 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rdx_v4+40]");
				if (num12 == 0)
				{
					goto IL_03f3;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			bool result = default(bool);
			return result;
		}
		nint num13 = (nint)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ rcx_v8 (Il2CppClass<System.Object>)+40]");
		nint num14 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rdx_v4+40]");
		if (num14 == 0)
		{
			goto IL_03f3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_050b;
		IL_050b:
		throw new NullReferenceException();
		IL_03f3:
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
		object obj12 = default(object);
		reference = ref *(int*)obj12;
		return true;
	}

	private static int Fnv1a32(string text)
	{
		//IL_0025: Expected I4, but got I8
		//IL_00ba: Expected I4, but got O
		bool flag = text == null;
		int num = 0;
		int num2 = -2128831035;
		int num3 = 0;
		if (!flag)
		{
			while (num < text._stringLength)
			{
				char c = text.get_Chars(num3);
				int num4 = c ^ num2;
				num2 = num4 * 16777619;
				num3++;
				num = num3;
			}
			return num2;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public ArticleController()
	{
		List<ArticleColumn> list = new List<ArticleColumn>();
		columns = list;
		clearColumnsBeforePopulate = true;
		maxCandidatesGathered = 64;
		defaultPriority = 50;
		shuffleColumnOrder = true;
		stagingSettleFrames = 3;
		guaranteedInstanceArticles = new List<GameObject>();
		autoLocatePoolQueueManager = true;
		fixedSeed = 12345;
		autoPopulateOnEnable = true;
		logWarnings = true;
		base._002Ector();
	}
}
