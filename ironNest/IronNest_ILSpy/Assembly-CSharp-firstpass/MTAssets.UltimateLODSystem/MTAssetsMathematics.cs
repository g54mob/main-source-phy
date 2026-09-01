using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace MTAssets.UltimateLODSystem;

public class MTAssetsMathematics : MonoBehaviour
{
	public unsafe static List<T> RandomizeThisList<T>(List<T> list)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0029: Expected O, but got I
		//IL_014d: Expected O, but got I
		//IL_0072: Expected O, but got I4
		//IL_018c: Expected O, but got I
		//IL_0194: Expected O, but got Ref
		//IL_01d3: Expected O, but got I
		//IL_01db: Expected O, but got Ref
		//IL_00d3: Expected O, but got I
		//IL_00ed: Expected O, but got Ref
		//IL_0262: Expected O, but got I
		//IL_0262: Expected O, but got Ref
		//IL_027e: Expected O, but got I
		//IL_0298: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v1 (Il2CppClass<T>)+FC]");
		T val = default(T);
		List<T> list2 = default(List<T>);
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v1 (Il2CppClass<T>)+FC]");
			object obj4 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj4 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v1 (Il2CppClass<T>)+FC]");
			object obj5 = (nint)0 + (nint)15;
			val = (T)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj5 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v1 (Il2CppClass<T>)+FC]");
			object obj6 = (nint)0 + (nint)15;
			list2 = (List<T>)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj6 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			if (list == null)
			{
				return (List<T>)(object)new NullReferenceException();
			}
		}
		int maxExclusive = list._size;
		_ = list._size;
		object obj7 = list._size - 1;
		if ((nint)obj7 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r8_v1 (Il2CppClass<T>)+FC]");
			_ = 0;
			int num2 = 0;
			do
			{
				int index = UnityEngine.Random.Range(num2, maxExclusive);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rcx_v11 (Il2CppClass<T>)+28]");
				object obj8 = (nint)0 >> 31;
				bool flag = obj8 != null;
				T value = (T)(&obj2);
				if (!flag)
				{
					value = val;
				}
				list.set_Item(num2, value);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+68]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+60]");
				((List<T>)(&obj2)).set_Item((int)num4, (T)0);
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rcx_v14 (Il2CppClass<T>)+28]");
				object obj9 = (nint)0 >> 31;
				bool flag2 = obj9 != null;
				T value2 = (T)(&obj2);
				if (!flag2)
				{
					value2 = (T)list2;
				}
				list.set_Item(index, value2);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+50]");
				maxExclusive = 0;
				num2++;
			}
			while (num2 < (nint)obj7);
		}
		return list;
	}

	public unsafe static Vector3 GetHalfPositionBetweenTwoPoints(Vector3 pointA, Vector3 pointB)
	{
		//IL_0045: Expected native int or pointer, but got O
		//IL_0052: Expected native int or pointer, but got O
		float num = pointB.z - pointA.z;
		float num2 = num * 0.5f;
		float z = num2 + pointA.z;
		Vector3 vector = default(Vector3);
		float x = default(float);
		((Vector3*)(nint)vector)->x = x;
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}
}
