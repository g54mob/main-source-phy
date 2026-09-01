using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class ExposedList<T> where T : class
{
	public const int k_DefaultCapacity = 10;

	private const int k_MaxAutoIncrease = 1000;

	private int _capacity;

	public T[] Values;

	public int Capacity
	{
		get
		{
			return _capacity;
		}
		set
		{
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800526C0");
		}
	}

	public ExposedList()
	{
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
		T[] values = default(T[]);
		Values = values;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180052630");
	}

	public ExposedList(int capacity)
	{
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
		T[] values = default(T[]);
		Values = values;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180052630");
	}

	public ExposedList(IList<T> list)
	{
		//IL_0074: Expected O, but got I4
		//IL_007d: Expected O, but got I4
		//IL_0086: Expected O, but got I4
		//IL_01ce: Expected I, but got O
		//IL_00b0: Expected O, but got I
		//IL_00b9: Expected O, but got I4
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_0176: Expected O, but got I
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		base._002Ector();
		nint num = 0;
		if (list != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
			T[] values = default(T[]);
			Values = values;
			T[] values2 = Values;
			object obj = 32;
			object obj2 = 0;
			object obj11 = default(object);
			object obj4;
			for (object obj3 = 0; (nint)obj3 < values2.Length; obj4 = obj11, Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v382 @ rax_v26] (should have been resolved before IL gen)"), values2 = Values, obj2++, obj += 8, obj3 = obj2)
			{
				T[] values3 = Values;
				nint num3 = 0;
				nint num4 = (nint)list;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r10_v4 (Il2CppClass<System.Collections.Generic.IList`1<T>>)+12E]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r10_v4 (Il2CppClass<System.Collections.Generic.IList`1<T>>)+B0]");
					obj4 = 0;
					object obj5 = 0;
					while (true)
					{
						object obj6 = obj5 + obj5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v314 @ r9_v6+v340 @ rax_v34*8]");
						nint num5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rcx_v16 (Il2CppRgctx<Kamgam.SettingsGenerator.ExposedList`1>)+28]");
						if (num5 == 0)
						{
							break;
						}
						obj5++;
						object obj7 = obj5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r10_v4 (Il2CppClass<System.Collections.Generic.IList`1<T>>)+12E]");
						if ((nint)obj7 < 0)
						{
							continue;
						}
						goto IL_00f0;
					}
					object obj8 = obj5 + obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v314 @ r9_v6+8+v371 @ rcx_v27*8]");
					object obj9 = (nint)0 << 4;
					object obj10 = obj9 + 312;
					obj11 = obj10 + num4;
					continue;
				}
				goto IL_00f0;
				IL_00f0:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
			T[] values4 = default(T[]);
			Values = values4;
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180052630");
		}
	}

	protected void resizeTo(int newCapacity)
	{
		//IL_0098: Expected O, but got I4
		//IL_00a2: Expected O, but got I4
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		T[] values = Values;
		_capacity = newCapacity;
		if (values.Length == newCapacity)
		{
			return;
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
		T[] values2 = default(T[]);
		Values = values2;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180052630");
		int num3 = Math.Min(newCapacity, values.Length);
		if (num3 > 0)
		{
			object obj = 0;
			object obj2 = 32;
			do
			{
				T[] values3 = Values;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdi_v7+v33 @ rax_v2 (T[])]");
				_ = 0;
				obj++;
				obj2 += 8;
			}
			while ((nint)obj < num3);
		}
	}

	protected void autoIncreaseCapacity()
	{
		//IL_001a: Expected O, but got I4
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0037: Expected I4, but got O
		//IL_0058: Expected O, but got I4
		T[] values = Values;
		object obj = values.Length >> 31;
		object obj2 = values.Length - obj;
		int val = obj2 >> 1;
		int num = Math.Min(1000, val);
		object obj3 = values.Length + num;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800526C0");
	}

	public void Clear()
	{
		//IL_0018: Expected O, but got I4
		//IL_002b: Expected O, but got I4
		//IL_0034: Expected O, but got I4
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		T[] values = Values;
		object obj = 32;
		T[] values2 = Values;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < values.Length)
		{
			_ = 0;
			obj3++;
			obj += 8;
			obj2 = obj3;
			values = Values;
		}
	}

	public void Add(T value)
	{
		//IL_0035: Expected O, but got I4
		//IL_003e: Expected O, but got I4
		//IL_0047: Expected O, but got I4
		//IL_00ba: Expected O, but got I4
		//IL_00d0: Expected O, but got I
		//IL_00ea: Expected O, but got I4
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_0107: Expected I4, but got O
		//IL_0129: Expected O, but got I
		//IL_0138: Expected O, but got I4
		//IL_0148: Expected O, but got I
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		if (value == null)
		{
			return;
		}
		T[] values = Values;
		object obj = 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < values.Length)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ r9_v5 (T[])+v125 @ rdx_v5]");
			if ((nint)0 != 0)
			{
				obj3++;
				obj += 8;
				obj2 = obj3;
				continue;
			}
			values[obj3] = value;
			return;
		}
		object obj4 = values.Length;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rcx_v5 (Il2CppRgctx<Kamgam.SettingsGenerator.ExposedList`1>)+50]");
		object obj5 = 0;
		T[] values2 = Values;
		object obj6 = values2.Length >> 31;
		object obj7 = values2.Length - obj6;
		int val = obj7 >> 1;
		int num2 = Math.Min(1000, val);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ r15_v6+20]");
		object obj8 = 0;
		object obj9 = values2.Length + num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v326 @ rcx_v9+C0]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800526C0");
		T[] values3 = Values;
		values3[obj4] = value;
	}

	public unsafe void Add(IList<T> values)
	{
		//IL_0022: Expected O, but got Ref
		//IL_002b: Expected O, but got I4
		//IL_0030: Expected I, but got O
		//IL_0070: Expected I, but got O
		//IL_0422: Expected O, but got I
		//IL_00eb: Expected O, but got I4
		//IL_04bf: Expected O, but got I
		//IL_0099: Expected O, but got I
		//IL_0146: Expected O, but got I
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0507: Expected I, but got O
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_021c: Expected O, but got I4
		//IL_022c: Expected O, but got I
		//IL_024c: Expected O, but got I
		//IL_0266: Expected O, but got I4
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Expected O, but got Unknown
		//IL_0283: Expected I4, but got O
		//IL_02a5: Expected O, but got I
		//IL_02b5: Expected O, but got I
		//IL_02c4: Expected O, but got I4
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_02f8: Expected I, but got O
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Expected I, but got Unknown
		//IL_0560: Expected O, but got I4
		//IL_0354: Expected O, but got I4
		if (values == null)
		{
			return;
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj3 = 0;
		nint num2 = unchecked((nint)null);
		object obj4 = default(object);
		object obj15 = default(object);
		object obj17 = default(object);
		while (obj2 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj14;
			object obj6;
			if (obj4 != null)
			{
				bool flag = obj2 == null;
				num2 = unchecked((nint)null);
				if (!flag)
				{
					nint num3 = 0;
					object obj5 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ r10_v5+12E]");
					obj6 = 0;
					object obj7 = obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ r10_v5+12E]");
					if ((nint)obj7 >= 0)
					{
						goto IL_00d0;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ r10_v5+B0]");
					object obj8 = 0;
					object obj9 = obj3;
					while (true)
					{
						object obj10 = obj9 + obj9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r9_v5+v350 @ rax_v54*8]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v294 @ rcx_v12 (Il2CppRgctx<Kamgam.SettingsGenerator.ExposedList`1>)+68]");
						if (num4 == 0)
						{
							break;
						}
						obj9++;
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
						{
							continue;
						}
						goto IL_00d0;
					}
					object obj11 = obj9 + obj9;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r9_v5+8+v405 @ rcx_v44*8]");
					object obj12 = (nint)0 << 4;
					object obj13 = obj12 + 312;
					obj14 = obj13 + obj5;
					goto IL_049f;
				}
				throw new NullReferenceException();
			}
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			return;
			IL_00d0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj14 = obj15;
			obj6 = 0;
			goto IL_049f;
			IL_049f:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v410 @ rdx_v11] (should have been resolved before IL gen)");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ rcx_v17 (Il2CppRgctx<Kamgam.SettingsGenerator.ExposedList`1>)+78]");
			object obj16 = 0;
			bool flag2 = obj17 == null;
			num2 = 0;
			if (flag2)
			{
				continue;
			}
			object obj18 = obj3;
			object obj20;
			while (true)
			{
				T[] values2 = Values;
				bool flag3 = Values == null;
				num2 = (nint)Values;
				if (!flag3)
				{
					if ((nint)obj18 < values2.Length)
					{
						if (values2[obj18] != null)
						{
							obj18++;
							continue;
						}
						if (Values != null)
						{
							if ((nint)obj18 < values2.Length)
							{
								values2[obj18] = (T)obj17;
								object obj19 = obj18 * 8;
								obj20 = (object)Values + obj19;
								break;
							}
							throw new IndexOutOfRangeException();
						}
						throw new NullReferenceException();
					}
					object obj21 = values2.Length;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ r8_v11+20]");
					object obj22 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v498 @ rax_v34+C0]");
					num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ rcx_v7 (Il2CppRgctx<Kamgam.SettingsGenerator.ExposedList`1>)+50]");
					object obj23 = 0;
					T[] values3 = Values;
					if (Values != null)
					{
						object obj24 = values3.Length >> 31;
						object obj25 = values3.Length - obj24;
						int val = obj25 >> 1;
						int num6 = Math.Min(1000, val);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v500 @ r12_v13+20]");
						object obj26 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v656 @ rcx_v30+C0]");
						object obj27 = 0;
						object obj28 = values3.Length + num6;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800526C0");
						T[] values4 = Values;
						bool flag4 = Values == null;
						num2 = (nint)Values;
						if (!flag4)
						{
							bool flag5 = values2.Length >= values4.Length;
							values2 = Values;
							if (!flag5)
							{
								values4[obj21] = (T)obj17;
								object obj29 = values2.Length * 8;
								obj20 = (object)Values + obj29;
								break;
							}
							throw new IndexOutOfRangeException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			num2 = (nint)(obj20 + 32);
			obj3 = 0;
		}
		throw new NullReferenceException();
	}

	public void Remove(T value)
	{
		//IL_0035: Expected O, but got I4
		//IL_003e: Expected O, but got I4
		//IL_0047: Expected O, but got I4
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		if (value == null)
		{
			return;
		}
		T[] values = Values;
		object obj = 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < values.Length)
		{
			T[] values2 = Values;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rdi_v5+v102 @ rax_v8 (T[])]");
			if (0 == (nint)value)
			{
				_ = 0;
			}
			values = Values;
			obj3++;
			obj += 8;
			obj2 = obj3;
		}
	}

	protected bool Contains(T value)
	{
		//IL_00d7: Expected I4, but got O
		//IL_0059: Expected O, but got I4
		//IL_0062: Expected O, but got I4
		//IL_006b: Expected O, but got I4
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		if (value != null)
		{
			T[] values = Values;
			if (Values == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			T[] values2 = Values;
			object obj = 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj2 < values.Length)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v4+v32 @ rdx_v3 (T[])]");
				if (0 != (nint)value)
				{
					obj3++;
					obj += 8;
					obj2 = obj3;
					continue;
				}
				return true;
			}
		}
		return false;
	}
}
