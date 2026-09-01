using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace ArticleSystem;

public static class ArticleNewspaperPacker
{
	public class Candidate
	{
		public GameObject Prefab;

		public float MeasuredHeight;

		public int Priority;

		public bool Reusable;

		public int MaxColumnsPerPass;
	}

	public class ColumnState
	{
		public float CapacityHeight;

		public float UsedHeight;

		public float ArticleSpacing;

		public float FillTolerance;

		public readonly List<Candidate> Assigned;

		public readonly HashSet<GameObject> PlacedInColumn;

		public float RemainingHeight => CapacityHeight - UsedHeight;

		public bool IsSatisfied
		{
			get
			{
				//IL_000b: Invalid comparison between F4 and I4
				if (!(CapacityHeight > 0f))
				{
					return false;
				}
				float num = UsedHeight / CapacityHeight;
				bool flag = num < FillTolerance;
				return !flag;
			}
		}

		public bool TryAssign(Candidate c)
		{
			//IL_0160: Expected I4, but got O
			//IL_00b0: Expected F4, but got I4
			if (c != null && PlacedInColumn != null)
			{
				if (PlacedInColumn.Contains(c.Prefab))
				{
					goto IL_014c;
				}
				List<Candidate> assigned = Assigned;
				if (Assigned != null)
				{
					float num = ((assigned._size <= 0) ? 0f : ArticleSpacing);
					float num2 = num + UsedHeight;
					float num3 = num2 + c.MeasuredHeight;
					if (num3 > CapacityHeight)
					{
						goto IL_014c;
					}
					float num4 = num + c.MeasuredHeight;
					float usedHeight = num4 + UsedHeight;
					UsedHeight = usedHeight;
					if (Assigned != null)
					{
						Assigned.Add(c);
						if (PlacedInColumn != null)
						{
							PlacedInColumn.Add(c.Prefab);
							return true;
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_014c:
			return false;
		}

		public ColumnState()
		{
			List<Candidate> assigned = new List<Candidate>();
			Assigned = assigned;
			HashSet<GameObject> placedInColumn = new HashSet<GameObject>();
			PlacedInColumn = placedInColumn;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public class PackOptions
	{
		public bool ShuffleColumnOrder;

		public bool PinHighestPriorityToTop;

		public System.Random Rng;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Comparison<Candidate> _003C_003E9__4_0;

		public static Comparison<Candidate> _003C_003E9__4_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal unsafe int _003CPack_003Eb__4_0(Candidate a, Candidate b)
		{
			//IL_0071: Expected I4, but got O
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected I4, but got Unknown
			if (b != null && a != null)
			{
				int num = b + 28;
				return ((int*)num)->CompareTo(a.Priority);
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}

		internal unsafe int _003CPack_003Eb__4_1(Candidate a, Candidate b)
		{
			//IL_0073: Expected I4, but got O
			//IL_005c: Expected Ref, but got F4
			if (a != null && b != null)
			{
				float num = (float)a + 24f;
				return ((float*)num)->CompareTo(b.MeasuredHeight);
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public static void Pack(List<Candidate> candidates, List<ColumnState> columns)
	{
		Pack(candidates, columns, null);
	}

	public unsafe static void Pack(List<Candidate> candidates, List<ColumnState> columns, PackOptions options)
	{
		//IL_0141: Invalid comparison between I4 and F4
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Expected O, but got Unknown
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected I4, but got Unknown
		//IL_02ab: Expected I4, but got O
		//IL_02af: Expected O, but got I4
		//IL_02ea: Expected I4, but got O
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_03ed: Expected O, but got I
		//IL_048a: Expected O, but got I
		//IL_046f: Expected O, but got I
		if (candidates == null || candidates._size == 0 || columns == null || columns._size == 0)
		{
			return;
		}
		System.Random random;
		if (options != null)
		{
			random = options.Rng;
			if (options.Rng != null)
			{
				goto IL_00cc;
			}
		}
		System.Random random2 = new System.Random();
		random = random2;
		goto IL_00cc;
		IL_00cc:
		List<Candidate> list = new List<Candidate>(candidates._size);
		List<Candidate> list2 = new List<Candidate>(candidates._size);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<Candidate>.Enumerator enumerator = default(List<Candidate>.Enumerator);
		Candidate candidate = default(Candidate);
		List<ColumnState>.Enumerator enumerator3 = default(List<ColumnState>.Enumerator);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (candidate == null)
				{
					break;
				}
				if (0f < candidate.MeasuredHeight)
				{
					if (candidate.Priority <= 0)
					{
						list2.Add(candidate);
					}
					else
					{
						list.Add(candidate);
					}
				}
				continue;
			}
			enumerator.Dispose();
			Comparison<Candidate> comparison = _003C_003Ec._003C_003E9__4_0;
			if (_003C_003Ec._003C_003E9__4_0 == null)
			{
				comparison = (_003C_003Ec._003C_003E9__4_0 = delegate(Candidate a, Candidate b)
				{
					//IL_0071: Expected I4, but got O
					//IL_0043: Unknown result type (might be due to invalid IL or missing references)
					//IL_0048: Expected I4, but got Unknown
					if (b == null || a == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (int)ex;
					}
					int num2 = b + 28;
					return ((int*)num2)->CompareTo(a.Priority);
				});
			}
			list.Sort(comparison);
			bool flag = list._size < 2;
			List<Candidate> list3 = list;
			System.Random random3 = random;
			if (!flag)
			{
				Comparison<Candidate> comparison2 = null;
				List<Candidate> list4 = list2;
				list3 = list;
				random3 = random;
				Comparison<Candidate> comparison3 = null;
				while ((nint)comparison3 < list._size)
				{
					comparison3 = (Comparison<Candidate>)(comparison2 + 1);
					while ((nint)comparison3 < list._size)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (candidate.Priority != list2._version)
						{
							break;
						}
						comparison3 = (Comparison<Candidate>)(comparison3 + 1);
					}
					int num = comparison3 - 1;
					if (num > (nint)comparison2)
					{
						do
						{
							int maxValue = num + 1;
							List<Candidate>.Enumerator enumerator2 = (List<Candidate>.Enumerator)random.Next((int)comparison2, maxValue);
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							list.set_Item(num, (Candidate)(object)list3);
							list.set_Item((int)enumerator2, (Candidate)(object)random3);
							num--;
						}
						while (num > (nint)comparison2);
					}
					comparison2 = comparison3;
				}
			}
			BestFit(list, columns, fillerPhase: false);
			Comparison<Candidate> comparison4 = _003C_003Ec._003C_003E9__4_1;
			if (_003C_003Ec._003C_003E9__4_1 == null)
			{
				comparison4 = (_003C_003Ec._003C_003E9__4_1 = delegate(Candidate a, Candidate b)
				{
					//IL_0073: Expected I4, but got O
					//IL_005c: Expected Ref, but got F4
					if (a == null || b == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (int)ex;
					}
					float num2 = (float)a + 24f;
					return ((float*)num2)->CompareTo(b.MeasuredHeight);
				});
			}
			list2.Sort(comparison4);
			BestFit(list2, columns, fillerPhase: true);
			if (options == null || !options.ShuffleColumnOrder)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			while (true)
			{
				if (enumerator3.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (candidate != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ stack_8 (ArticleSystem.ArticleNewspaperPacker+Candidate)+20]");
						List<Candidate> list5 = (List<Candidate>)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ stack_8 (ArticleSystem.ArticleNewspaperPacker+Candidate)+20]");
						if ((nint)0 == 0)
						{
							break;
						}
						if (list5._size >= 2)
						{
							if (!options.PinHighestPriorityToTop)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ stack_8 (ArticleSystem.ArticleNewspaperPacker+Candidate)+20]");
								Shuffle((List<Candidate>)0, random);
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ stack_8 (ArticleSystem.ArticleNewspaperPacker+Candidate)+20]");
								ShuffleWithTopPin((List<Candidate>)0, random);
							}
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator3.Dispose();
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private unsafe static void BestFit(List<Candidate> candidates, List<ColumnState> columns, bool fillerPhase)
	{
		//IL_004f: Expected O, but got Ref
		//IL_007c: Expected O, but got Ref
		//IL_04ff: Expected O, but got I4
		//IL_01d0: Expected O, but got Ref
		//IL_0135: Expected O, but got Ref
		//IL_036b: Expected O, but got I4
		//IL_0150: Expected O, but got F4
		//IL_015e: Invalid comparison between F4 and I4
		//IL_018c: Expected O, but got F4
		//IL_0199: Invalid comparison between O and F4
		//IL_03b4: Expected O, but got I4
		//IL_02be: Expected F4, but got I4
		//IL_04c4: Expected O, but got F4
		//IL_04d1: Invalid comparison between O and F4
		HashSet<GameObject> hashSet = new HashSet<GameObject>();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<Candidate>.Enumerator enumerator = default(List<Candidate>.Enumerator);
		Candidate candidate = default(Candidate);
		List<ColumnState>.Enumerator enumerator3 = default(List<ColumnState>.Enumerator);
		List<ColumnState>.Enumerator enumerator4 = default(List<ColumnState>.Enumerator);
		ColumnState columnState2 = default(ColumnState);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = candidate == null;
				HashSet<GameObject> hashSet2 = (HashSet<GameObject>)(&enumerator);
				if (flag)
				{
					break;
				}
				bool flag2 = candidate.Reusable;
				hashSet2 = (HashSet<GameObject>)(&enumerator);
				if (!flag2 && hashSet.Contains(candidate.Prefab))
				{
					continue;
				}
				if (columns != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					float num = 3.4028235E+38f;
					ColumnState columnState = null;
					List<ColumnState>.Enumerator enumerator2 = enumerator3;
					while (enumerator4.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag3 = !fillerPhase;
						List<ColumnState>.Enumerator enumerator5 = enumerator2;
						if (!flag3)
						{
							bool flag4 = columnState2 == null;
							hashSet2 = (HashSet<GameObject>)(&enumerator4);
							if (flag4)
							{
								throw new NullReferenceException();
							}
							enumerator5 = (List<ColumnState>.Enumerator)columnState2.CapacityHeight;
							if (columnState2.CapacityHeight > 0f)
							{
								enumerator5 = (List<ColumnState>.Enumerator)(columnState2.UsedHeight / columnState2.CapacityHeight);
								bool flag5 = System.Runtime.CompilerServices.Unsafe.As<List<ColumnState>.Enumerator, UIntPtr>(ref enumerator5) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)columnState2.FillTolerance);
								enumerator2 = enumerator5;
								if (flag5)
								{
									continue;
								}
							}
						}
						bool flag6 = columnState2 == null;
						hashSet2 = (HashSet<GameObject>)(&enumerator4);
						if (!flag6)
						{
							if (candidate != null)
							{
								if (columnState2.PlacedInColumn != null)
								{
									bool flag7 = columnState2.PlacedInColumn.Contains(candidate.Prefab);
									enumerator2 = enumerator5;
									if (flag7)
									{
										continue;
									}
									List<Candidate> assigned = columnState2.Assigned;
									if (columnState2.Assigned == null)
									{
										throw new NullReferenceException();
									}
									float num2 = ((assigned._size <= 0) ? 0f : columnState2.ArticleSpacing);
									float num3 = num2 + columnState2.UsedHeight;
									enumerator2 = (List<ColumnState>.Enumerator)(num3 + candidate.MeasuredHeight);
									if (System.Runtime.CompilerServices.Unsafe.As<List<ColumnState>.Enumerator, UIntPtr>(ref enumerator2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)columnState2.CapacityHeight))
									{
										float num4 = columnState2.CapacityHeight - (float)enumerator2;
										if (num > num4)
										{
											num = num4;
											columnState = columnState2;
										}
									}
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					enumerator4.Dispose();
					bool flag8 = columnState == null;
					List<Candidate>.Enumerator enumerator6 = (List<Candidate>.Enumerator)0;
					if (flag8)
					{
						continue;
					}
					bool flag9 = columnState.TryAssign(candidate);
					if (candidate != null)
					{
						bool flag10 = candidate.Reusable;
						enumerator6 = (List<Candidate>.Enumerator)0;
						if (!flag10)
						{
							if (hashSet == null)
							{
								throw new NullReferenceException();
							}
							hashSet.Add(candidate.Prefab);
							enumerator6 = (List<Candidate>.Enumerator)0;
						}
						continue;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private static void ShuffleEqualPriorityGroups(List<Candidate> list, System.Random rng)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected I4, but got Unknown
		//IL_010b: Expected I4, but got O
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		List<Candidate> list2 = default(List<Candidate>);
		if (list2._size < 2)
		{
			return;
		}
		System.Random random = null;
		System.Random random2 = null;
		nint num2 = default(nint);
		Candidate value = default(Candidate);
		Candidate value2 = default(Candidate);
		while ((nint)random < list2._size)
		{
			random = (System.Random)(random2 + 1);
			while ((nint)random < list2._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ stack_8+1C]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ stack_20+1C]");
				bool flag = num != 0;
				num2 = 0;
				if (flag)
				{
					break;
				}
				random = (System.Random)(random + 1);
				num2 = 0;
			}
			int num3 = random - 1;
			bool flag2 = num3 <= (nint)random2;
			nint num4 = num2;
			if (!flag2)
			{
				bool flag3;
				do
				{
					int maxValue = num3 + 1;
					int index = rng.Next((int)random2, maxValue);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					list2.set_Item(num3, value);
					list2.set_Item(index, value2);
					num3--;
					flag3 = num3 > (nint)random2;
					num4 = 0;
				}
				while (flag3);
			}
			num2 = num4;
			random2 = random;
		}
	}

	private unsafe static void Shuffle<T>(List<T> list, System.Random rng)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0029: Expected O, but got I
		//IL_0215: Expected O, but got I
		//IL_0055: Expected O, but got I8
		//IL_0259: Expected O, but got I
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Expected O, but got Unknown
		//IL_0421: Expected O, but got I
		//IL_029d: Expected O, but got I
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Expected O, but got Unknown
		//IL_02dc: Expected O, but got I
		//IL_02e4: Expected O, but got Ref
		//IL_0067: Expected O, but got I8
		//IL_0328: Expected O, but got I
		//IL_0367: Expected O, but got I
		//IL_036f: Expected O, but got Ref
		//IL_00af: Expected O, but got I
		//IL_03a0: Expected O, but got Ref
		//IL_03c0: Expected O, but got I
		//IL_00d0: Expected O, but got I4
		//IL_0150: Expected O, but got I
		//IL_016a: Expected O, but got Ref
		//IL_04a6: Expected O, but got I
		//IL_04a6: Expected O, but got I
		//IL_04c7: Expected O, but got I
		//IL_04c7: Expected O, but got Ref
		//IL_019e: Expected O, but got I
		//IL_019e: Expected O, but got Ref
		//IL_01ba: Expected O, but got I
		//IL_01d4: Expected O, but got Ref
		//IL_04f5: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
		T val = default(T);
		List<T> list2 = default(List<T>);
		object obj10;
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			object obj4 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj4 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			object obj5 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj5 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			object obj6 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj6 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			object obj7 = (nint)0 + (nint)15;
			val = (T)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj7 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			object obj8 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj8 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			object obj9 = (nint)0 + (nint)15;
			list2 = (List<T>)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj9 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			obj10 = (nint)0 + (nint)15;
			object obj11 = obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj11 > 0)
			{
				goto IL_03e4;
			}
		}
		obj10 = 1152921504606846960L;
		goto IL_03e4;
		IL_03e4:
		object obj12 = obj10 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
		object obj13 = (nint)0 + (nint)15;
		object obj14 = obj13;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
		if ((nint)obj14 <= 0)
		{
			obj13 = 1152921504606846960L;
		}
		object obj15 = obj13 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		int num2 = list._size - 1;
		if (num2 <= 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+78]");
		object obj16 = 0;
		nint num3 = 0;
		bool flag3;
		do
		{
			object obj17 = obj16;
			object obj18 = num2 + 1;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v370 @ r8_v6+1A8] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v434 @ rcx_v22 (Il2CppClass<T>)+28]");
			object obj19 = (nint)0 >> 31;
			bool flag = obj19 != null;
			T value = (T)(&obj2);
			if (!flag)
			{
				value = val;
			}
			list.set_Item(num2, value);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+18]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+10]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			((List<T>)num5).set_Item((int)num6, (T)0);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+18]");
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			((List<T>)(&obj2)).set_Item((int)num7, (T)0);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ r9_v1 (Il2CppClass<T>)+FC]");
			((List<T>)(&obj2)).set_Item((int)(&obj2), (T)0);
			nint num8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v467 @ rcx_v27 (Il2CppClass<T>)+28]");
			object obj20 = (nint)0 >> 31;
			bool flag2 = obj20 != null;
			T value2 = (T)(&obj2);
			if (!flag2)
			{
				value2 = (T)list2;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+70]");
			list.set_Item(0, value2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+78]");
			obj16 = 0;
			num2--;
			flag3 = num2 > 0;
			num3 = 0;
		}
		while (flag3);
	}

	private static void ShuffleWithTopPin(List<Candidate> list, System.Random rng)
	{
		//IL_0024: Expected O, but got I
		//IL_0090: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ stack_8_v8 (ArticleSystem.ArticleNewspaperPacker+Candidate)+1C]");
		object obj = 0;
		int num = 0;
		for (int i = 1; i < list._size; i++)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ stack_8_v8 (ArticleSystem.ArticleNewspaperPacker+Candidate)+1C]");
			if (0 > (nint)obj)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v264 @ stack_20_v7 (ArticleSystem.ArticleNewspaperPacker+Candidate)+1C]");
				obj = 0;
				num = i;
			}
		}
		Candidate value = default(Candidate);
		Candidate value2 = default(Candidate);
		if (num != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			list.set_Item(0, value);
			list.set_Item(num, value2);
		}
		int num2 = list._size - 1;
		if (num2 > 1)
		{
			do
			{
				int maxValue = num2 + 1;
				int index = rng.Next(1, maxValue);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				list.set_Item(num2, value);
				list.set_Item(index, value2);
				num2--;
			}
			while (num2 > 1);
		}
	}
}
