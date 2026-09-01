using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

internal sealed class ResizableArray<T>
{
	private T[] items;

	private int length;

	private static T[] emptyArr;

	public int Length
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return length;
		}
	}

	public T[] Data
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return items;
		}
	}

	// C# has no syntax for parameterized property 'Item'.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe T get_Item(int index)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0037: Expected O, but got I
		//IL_004c: Expected O, but got I
		//IL_0062: Expected O, but got I
		//IL_00aa: Expected I, but got O
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00cd: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r9+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v1+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r9_v1+10]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2+FC]");
		T[] array = default(T[]);
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			array = items;
		}
		if (index < array.Length)
		{
			nint num = (nint)array;
			object obj7 = array + 32;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v10 (Il2CppClass<T[]>)+104]");
			object obj8 = (nint)0 * (nint)index;
			object obj9 = obj7 + obj8;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			T result = default(T);
			return result;
		}
		return (T)new IndexOutOfRangeException();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void set_Item(int index, T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_002d: Expected O, but got I
		//IL_0043: Expected O, but got I
		//IL_017f: Expected O, but got Ref
		//IL_0195: Expected O, but got I
		//IL_008a: Expected O, but got Ref
		//IL_00a0: Expected I, but got O
		//IL_00b5: Expected O, but got I
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00f5: Expected O, but got Ref
		//IL_010b: Expected I, but got O
		//IL_0120: Expected O, but got I
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ r10_v1 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1>)+10]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v2+FC]");
		T[] array = default(T[]);
		T val;
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			array = items;
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rcx_v1 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1>)+10]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v8+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_01bd;
			}
		}
		val = value;
		goto IL_01bd;
		IL_01bd:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		bool flag = index >= array.Length;
		object obj6 = (object)(&obj2);
		if (!flag)
		{
			nint num3 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rax_v24 (Il2CppClass<T[]>)+104]");
			object obj7 = (nint)0 * (nint)index;
			object obj8 = obj7 + 32;
			object obj9 = obj8 + (object)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			bool flag2 = index >= array.Length;
			obj6 = (object)(&obj2);
			if (!flag2)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v26 (Il2CppClass<T[]>)+104]");
				object obj10 = (nint)0 * (nint)index;
				object obj11 = (object)array + obj10;
				nint num5 = 0;
				object obj12 = obj11 + 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				return;
			}
		}
		throw new IndexOutOfRangeException();
	}

	public ResizableArray(int capacity)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 4 Invalid \"Jump target not found in method: 0x1808F8540\"");
	}

	public ResizableArray(int capacity, int length)
	{
		//IL_0037: Expected I, but got O
		//IL_0058: Expected I, but got O
		//IL_00a9: Expected O, but got I
		//IL_00be: Expected O, but got I
		base._002Ector();
		if (capacity >= 0)
		{
			int num = default(int);
			bool flag = num < 0;
			nint num2 = unchecked((nint)null);
			if (!flag)
			{
				bool flag2 = num > capacity;
				num2 = unchecked((nint)null);
				if (!flag2)
				{
					nint num3 = 0;
					T[] array = default(T[]);
					if (capacity <= 0)
					{
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ rcx_v20 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1>)+28]");
						object obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rax_v26+B8]");
						object obj2 = 0;
						array = (T[])obj2;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
					}
					items = array;
					this.length = num;
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("length");
			ex._002Ector("length");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException("capacity");
		throw ex2;
	}

	public ResizableArray(T[] initialArray)
	{
		//IL_006b: Expected O, but got I
		//IL_0080: Expected O, but got I
		base._002Ector();
		if (initialArray != null)
		{
			nint num = 0;
			if (initialArray.Length == 0)
			{
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ rcx_v14 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1>)+28]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rax_v21+B8]");
				object obj2 = 0;
				items = (T[])obj2;
				length = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
				T[] array = default(T[]);
				items = array;
				length = initialArray.Length;
				int num3 = default(int);
				Array.Copy(initialArray, 0, items, 0, num3);
			}
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex = new ArgumentNullException("initialArray");
		throw ex;
	}

	private void IncreaseCapacity(int capacity)
	{
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
		int num2 = Math.Min(length, capacity);
		Array destinationArray = default(Array);
		int num3 = default(int);
		Array.Copy(items, 0, destinationArray, 0, num3);
		items = (T[])destinationArray;
	}

	public void Clear()
	{
		Array.Clear(items, 0, length);
		length = 0;
	}

	public void Resize(int length, bool trimExess = false, bool clearMemory = false)
	{
		//IL_00b8: Expected O, but got I
		//IL_00c8: Expected O, but got I
		//IL_0147: Expected O, but got I
		//IL_0157: Expected O, but got I
		//IL_0167: Expected O, but got I
		//IL_0177: Expected O, but got I
		//IL_0187: Expected O, but got I
		if (length >= 0)
		{
			T[] array = items;
			if (length <= array.Length)
			{
				bool flag = length >= this.length;
				bool flag2 = false;
				if (!flag)
				{
					bool flag3 = default(bool);
					flag2 = flag3;
				}
				bool flag4 = !flag2;
				int num = (trimExess ? 1 : 0);
				if (!flag4)
				{
					num = this.length - length;
					Array.Clear(items, length, num);
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ stack_28+20]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ rax_v21+C0]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ r8_v6+30]");
				int num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180069E60");
			}
			this.length = length;
			if (trimExess)
			{
				T[] array2 = items;
				if (array2.Length != length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ stack_28+20]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ rax_v12+C0]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rcx_v9+38]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v236 @ rax_v13+20]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ rcx_v10+C0]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
					Array destinationArray = default(Array);
					int num2 = default(int);
					Array.Copy(items, 0, destinationArray, 0, num2);
					items = (T[])destinationArray;
				}
			}
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("length");
		throw ex;
	}

	public void TrimExcess()
	{
		T[] array = items;
		if (array.Length != length)
		{
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
			Array destinationArray = default(Array);
			int num2 = default(int);
			Array.Copy(items, 0, destinationArray, 0, num2);
			items = (T[])destinationArray;
		}
	}

	public unsafe void Add(T item)
	{
		//IL_0008: Expected O, but got Ref
		//IL_002d: Expected O, but got I
		//IL_0043: Expected O, but got I
		//IL_00c0: Expected O, but got Ref
		//IL_00fa: Expected O, but got I
		//IL_00a3: Expected O, but got I4
		//IL_0137: Expected I, but got O
		//IL_014e: Expected O, but got I
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_0180: Expected I, but got O
		//IL_0197: Expected O, but got I
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v23 @ r9_v1 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1>)+10]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2+FC]");
		T[] array = default(T[]);
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			array = items;
		}
		if (length >= array.Length)
		{
			nint num2 = 0;
			T[] array2 = items;
			object obj5 = array2.Length + array2.Length;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180069E60");
		}
		T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
		T[] array3 = items;
		int num3 = length + 1;
		length = num3;
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rcx_v3 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1>)+10]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rax_v16+28]");
		if ((nint)0 < (nint)0)
		{
			val = item;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num5 = (nint)array3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rax_v18 (Il2CppClass<T[]>)+104]");
		object obj7 = (nint)0 * (nint)length;
		object obj8 = obj7 + 32;
		object obj9 = obj8 + (object)array3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num6 = (nint)array3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ rax_v20 (Il2CppClass<T[]>)+104]");
		object obj10 = (nint)0 * (nint)length;
		object obj11 = (object)array3 + obj10;
		nint num7 = 0;
		object obj12 = obj11 + 32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
	}

	public T[] ToArray()
	{
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
		Array array = default(Array);
		int num2 = default(int);
		Array.Copy(items, 0, array, 0, num2);
		return (T[])array;
	}

	static ResizableArray()
	{
		//IL_0035: Expected O, but got I
		//IL_004a: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v9 (Il2CppRgctx<MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1>)+28]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rax_v11+B8]");
		object obj2 = 0;
		object obj3 = default(object);
		obj2 = obj3;
	}
}
