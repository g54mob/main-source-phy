using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class PointPath<T> : DisposableMesh
{
	protected List<T> path;

	public int Count
	{
		get
		{
			//IL_0010: Expected O, but got I
			//IL_0050: Expected I4, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rax_v1+18]");
				return 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public unsafe T LastPoint
	{
		get
		{
			//IL_0008: Expected O, but got Ref
			//IL_0018: Expected O, but got I
			//IL_0037: Expected O, but got I
			//IL_0047: Expected O, but got I
			//IL_005d: Expected O, but got I
			//IL_00f8: Expected O, but got I
			//IL_008c: Expected O, but got I
			//IL_00a2: Expected O, but got I
			//IL_00b2: Expected O, but got I
			object obj2 = default(object);
			object obj = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r8+20]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v1+C0]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ r9_v1+20]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rax_v2+FC]");
			object obj6 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rax_v2+FC]");
			if ((nint)obj6 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
				if ((nint)0 == 0)
				{
					return (T)new NullReferenceException();
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r8+20]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rdx_v1+18]");
			object obj9 = -1;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v8+C0]");
			object obj10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			T result = default(T);
			return result;
		}
	}

	// C# has no syntax for parameterized property 'Item'.
	public unsafe T get_Item(int i)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0037: Expected O, but got I
		//IL_0047: Expected O, but got I
		//IL_005d: Expected O, but got I
		//IL_008c: Expected O, but got I
		//IL_009c: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r9+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v1+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ r10_v1+20]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rax_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rax_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
			if ((nint)0 == 0)
			{
				return (T)new NullReferenceException();
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r9+20]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rax_v8+C0]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	public unsafe void set_Item(int i, T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_002d: Expected O, but got I
		//IL_0043: Expected O, but got I
		//IL_00e8: Expected O, but got Ref
		//IL_00f6: Expected O, but got Ref
		//IL_0106: Expected O, but got I
		//IL_0085: Expected O, but got I
		//IL_009b: Expected O, but got I
		//IL_00b5: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ r10_v1 (Il2CppRgctx<Shapes.PointPath`1>)+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2+FC]");
		object obj5 = default(object);
		T val;
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
			nint num2 = 0;
			obj5 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v1 (Il2CppRgctx<Shapes.PointPath`1>)+20]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v8+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_012e;
			}
		}
		val = value;
		goto IL_012e;
		IL_012e:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rcx_v3 (Il2CppRgctx<Shapes.PointPath`1>)+20]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v12+28]");
		object obj8 = (nint)0 >> 31;
		bool flag = obj8 != null;
		object obj9 = (object)(&obj2);
		if (!flag)
		{
			obj9 = obj5;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6FE0");
		_ = 1;
	}

	protected void OnSetFirstDataPoint()
	{
		_ = 257;
	}

	public void ClearAllPoints()
	{
		//IL_0010: Expected O, but got I
		//IL_002b: Expected O, but got I
		//IL_004e: Expected O, but got I
		//IL_005e: Expected O, but got I
		//IL_00e4: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
		object obj = 0;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rdx_v1 (Il2CppRgctx<Shapes.PointPath`1>)+28]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbx_v1+1C]");
		_ = (nint)0 + (nint)1;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ r8_v1+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v3+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj5 = default(object);
		if (obj5 == null)
		{
			_ = 0;
			return;
		}
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbx_v1+18]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbx_v1+10]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbx_v1+18]");
			Array.Clear((Array)num2, 0, 0);
		}
		_ = 0;
	}

	public unsafe void SetPoint(int index, T point)
	{
		//IL_0008: Expected O, but got Ref
		//IL_002d: Expected O, but got I
		//IL_0043: Expected O, but got I
		//IL_00e8: Expected O, but got Ref
		//IL_00f6: Expected O, but got Ref
		//IL_0106: Expected O, but got I
		//IL_0085: Expected O, but got I
		//IL_009b: Expected O, but got I
		//IL_00b5: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ r10_v1 (Il2CppRgctx<Shapes.PointPath`1>)+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ rax_v2+FC]");
		object obj5 = default(object);
		T val;
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
			nint num2 = 0;
			obj5 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v1 (Il2CppRgctx<Shapes.PointPath`1>)+20]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v8+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_012e;
			}
		}
		val = point;
		goto IL_012e;
		IL_012e:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rcx_v3 (Il2CppRgctx<Shapes.PointPath`1>)+20]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v12+28]");
		object obj8 = (nint)0 >> 31;
		bool flag = obj8 != null;
		object obj9 = (object)(&obj2);
		if (!flag)
		{
			obj9 = obj5;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6FE0");
		_ = 1;
	}

	public void RemovePointAt(int index)
	{
		//IL_0010: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
		object obj = 0;
		if (index >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rdi_v1+18]");
			if ((nint)index < (nint)0)
			{
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A5600");
				_ = 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rdi_v1+18]");
				if ((nint)0 == 1)
				{
					_ = 0;
				}
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		throw ex;
	}

	public unsafe void AddPoint(T p)
	{
		//IL_0008: Expected O, but got Ref
		//IL_002d: Expected O, but got I
		//IL_0043: Expected O, but got I
		//IL_00ed: Expected O, but got Ref
		//IL_0123: Expected O, but got Ref
		//IL_0139: Expected O, but got I
		//IL_0090: Expected O, but got I
		//IL_00a6: Expected O, but got I
		//IL_00c0: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ r9_v1 (Il2CppRgctx<Shapes.PointPath`1>)+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2+FC]");
		object obj5 = default(object);
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj5 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+19]");
			if ((nint)0 != 0)
			{
				goto IL_0115;
			}
		}
		_ = 257;
		goto IL_0115;
		IL_0115:
		T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 40));
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rcx_v2 (Il2CppRgctx<Shapes.PointPath`1>)+20]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v8+28]");
		if ((nint)0 < (nint)0)
		{
			val = p;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rcx_v4 (Il2CppRgctx<Shapes.PointPath`1>)+20]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rax_v12+28]");
		object obj8 = (nint)0 >> 31;
		bool flag = obj8 != null;
		object obj9 = (object)(&obj2);
		if (!flag)
		{
			obj9 = obj5;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
		_ = 257;
	}

	public void AddPoints(T[] pts)
	{
		//IL_0010: Expected O, but got I
		//IL_002b: Expected O, but got I
		//IL_003b: Expected O, but got I
		//IL_004b: Expected O, but got I
		//IL_0065: Expected O, but got I
		//IL_0087: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
		object obj = 0;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ r8_v2 (Il2CppRgctx<Shapes.PointPath`1>)+58]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rax_v5+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v23 @ r8_v3+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A3850");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rax_v7+18]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rax_v1+18]");
		object obj6 = num2 - 0;
		if ((nint)obj6 > 0)
		{
			_ = 257;
		}
	}

	public void AddPoints(IEnumerable<T> ptsToAdd)
	{
		//IL_0010: Expected O, but got I
		//IL_0035: Expected O, but got I
		//IL_0057: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
		object obj = 0;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A3850");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+28]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v6+18]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rax_v1+18]");
		object obj3 = num2 - 0;
		if ((nint)obj3 > 0)
		{
			_ = 257;
		}
	}

	protected bool CheckCanAddContinuePoint(string callerName = null)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PointPath`1<T>)+19]");
		if ((nint)0 != 0)
		{
			return false;
		}
		string message = callerName + " requires adding a point before calling it, to determine starting point";
		Debug.LogWarning(message);
		return true;
	}

	public PointPath()
	{
		nint num = 0;
		object obj = null;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6A40");
		base._002Ector();
	}
}
