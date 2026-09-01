using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

public struct Vector3d : IEquatable<Vector3d>
{
	public static readonly Vector3d zero;

	public const double Epsilon = 5E-324;

	public double x;

	public double y;

	public double z;

	public double Magnitude
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			//IL_00a5: Expected I, but got O
			//IL_0079: Expected F8, but got I4
			nint num = (nint)typeof(Math);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm10\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm7,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm8,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm6\"");
				return 0.0;
			}
			return Math.Sqrt(y);
		}
	}

	public double MagnitudeSqr
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm2\"");
			return x;
		}
	}

	public unsafe Vector3d Normalized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			//IL_0021: Expected native int or pointer, but got O
			//IL_0033: Expected native int or pointer, but got O
			Vector3d vector3d = default(Vector3d);
			((Vector3d*)(nint)vector3d)->x = 0.0;
			((Vector3d*)(nint)vector3d)->z = 0.0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAE70");
			return vector3d;
		}
	}

	// C# has no syntax for parameterized property 'Item'.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double get_Item(int index)
	{
		//IL_002b: Expected O, but got I4
		bool flag = index == 0;
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				if ((nint)obj == 1)
				{
					return z;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("index", "Invalid Vector3d index!");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			return y;
		}
		return x;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void set_Item(int index, double value)
	{
		//IL_002b: Expected O, but got I4
		bool flag = index == 0;
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("index", "Invalid Vector3d index!");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				z = value;
			}
			else
			{
				y = value;
			}
		}
		else
		{
			x = value;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3d(double value)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm1\"");
		z = value;
		x = value;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3d(double x, double y, double z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3d(Vector3 vector)
	{
		x = vector.x;
		z = vector.z;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Vector3d operator +(Vector3d a, Vector3d b)
	{
		//IL_0021: Expected native int or pointer, but got O
		//IL_003d: Expected native int or pointer, but got O
		//IL_004f: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [r8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [r8+8]\"");
		Vector3d vector3d = default(Vector3d);
		((Vector3d*)(nint)vector3d)->x = a.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [r8+10h]\"");
		((Vector3d*)(nint)vector3d)->y = a.y;
		((Vector3d*)(nint)vector3d)->z = a.z;
		return vector3d;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Vector3d operator -(Vector3d a, Vector3d b)
	{
		//IL_0021: Expected native int or pointer, but got O
		//IL_003d: Expected native int or pointer, but got O
		//IL_004f: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [r8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [r8+8]\"");
		Vector3d vector3d = default(Vector3d);
		((Vector3d*)(nint)vector3d)->x = a.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [r8+10h]\"");
		((Vector3d*)(nint)vector3d)->y = a.y;
		((Vector3d*)(nint)vector3d)->z = a.z;
		return vector3d;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Vector3d operator *(Vector3d a, double d)
	{
		//IL_002b: Expected native int or pointer, but got O
		//IL_0038: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulpd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rdx+10h]\"");
		Vector3d vector3d = default(Vector3d);
		((Vector3d*)(nint)vector3d)->x = a.x;
		((Vector3d*)(nint)vector3d)->z = d;
		return vector3d;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Vector3d operator *(double d, Vector3d a)
	{
		//IL_002b: Expected native int or pointer, but got O
		//IL_0038: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulpd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [r8+10h]\"");
		Vector3d vector3d = default(Vector3d);
		((Vector3d*)(nint)vector3d)->x = a.x;
		((Vector3d*)(nint)vector3d)->z = d;
		return vector3d;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Vector3d operator /(Vector3d a, double d)
	{
		//IL_0021: Expected native int or pointer, but got O
		//IL_003d: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divpd xmm0,xmm2\"");
		Vector3d vector3d = default(Vector3d);
		((Vector3d*)(nint)vector3d)->x = a.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm2\"");
		((Vector3d*)(nint)vector3d)->z = a.z;
		return vector3d;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Vector3d operator -(Vector3d a)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected F8, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected F8, but got Unknown
		//IL_003c: Expected native int or pointer, but got O
		//IL_0049: Expected native int or pointer, but got O
		double num = a.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206EE0]");
		double num2 = num ^ 0;
		double num3 = a.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206EE0]");
		double num4 = num3 ^ 0;
		Vector3d vector3d = default(Vector3d);
		((Vector3d*)(nint)vector3d)->x = num2;
		((Vector3d*)(nint)vector3d)->z = num4;
		return vector3d;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector3d lhs, Vector3d rhs)
	{
		//IL_00a9: Expected I, but got O
		nint num = (nint)typeof(Vector3d);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
		bool flag = (nint)0 < (nint)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
		bool flag2 = (nint)0 == 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,qword ptr [rbx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm4,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm0,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm4,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm4\"");
		bool flag3 = !flag;
		bool flag4 = !flag2;
		return flag4 & flag3;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector3d lhs, Vector3d rhs)
	{
		//IL_0091: Expected I, but got O
		nint num = (nint)typeof(Vector3d);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
		bool flag = (nint)0 < (nint)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,qword ptr [rbx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm4,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm0,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm4,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm4,qword ptr [182206DA0h]\"");
		return !flag;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static implicit operator Vector3d(Vector3 v)
	{
		//IL_000d: Expected native int or pointer, but got O
		//IL_001f: Expected native int or pointer, but got O
		//IL_0031: Expected native int or pointer, but got O
		Vector3d vector3d = default(Vector3d);
		((Vector3d*)(nint)vector3d)->x = v.x;
		((Vector3d*)(nint)vector3d)->y = v.y;
		((Vector3d*)(nint)vector3d)->z = v.z;
		return vector3d;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static explicit operator Vector3(Vector3d v)
	{
		//IL_000d: Expected native int or pointer, but got O
		//IL_001f: Expected native int or pointer, but got O
		//IL_0031: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = (float)v.x;
		((Vector3*)(nint)vector)->y = (float)v.y;
		((Vector3*)(nint)vector)->z = (float)v.z;
		return vector;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Set(double x, double y, double z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Scale(ref Vector3d scale)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rdx]\"");
		x = x;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rdx+8]\"");
		y = y;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rdx+10h]\"");
		z = z;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Normalize()
	{
		//IL_00a2: Expected I, but got O
		nint num = (nint)typeof(Vector3d);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAF40");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm2,qword ptr [182206DA0h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rcx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			x = 0.0;
			y = 0.0;
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divpd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm2\"");
		x = x;
		z = z;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Clamp(double min, double max)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_005f: Expected O, but got I4
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_00bb: Expected O, but got I4
		//IL_0117: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,qword ptr [rcx]\"");
		object obj = default(object);
		object obj2 = default(object);
		bool flag = obj == obj2;
		object obj4 = default(object);
		object obj3 = ~obj4;
		object obj5 = flag & obj3;
		if (obj5 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm2\"");
			bool flag2 = obj == obj2;
			object obj6 = !flag2;
			object obj7 = obj6 | obj4;
			if (obj7 == null)
			{
				x = max;
			}
		}
		else
		{
			x = min;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,qword ptr [rcx+8]\"");
		bool flag3 = obj == obj2;
		object obj8 = ~obj4;
		object obj9 = flag3 & obj8;
		if (obj9 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm2\"");
			bool flag4 = obj == obj2;
			object obj10 = !flag4;
			object obj11 = obj10 | obj4;
			if (obj11 == null)
			{
				y = max;
			}
		}
		else
		{
			y = min;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,qword ptr [rcx+10h]\"");
		bool flag5 = obj == obj2;
		object obj12 = ~obj4;
		object obj13 = flag5 & obj12;
		if (obj13 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm2\"");
			bool flag6 = obj == obj2;
			object obj14 = !flag6;
			object obj15 = obj14 | obj4;
			if (obj15 == null)
			{
				z = max;
			}
		}
		else
		{
			z = min;
		}
	}

	public override int GetHashCode()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_00ac: Expected O, but got F8
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_0162: Expected O, but got F8
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected F8, but got Unknown
		//IL_010c: Expected O, but got F8
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		//IL_014f: Expected I4, but got O
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected F8, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected F8, but got Unknown
		double num = x;
		double num2 = x - 1.0;
		object obj = num2 & 0x7FFFFFFFFFFFFFFFL;
		if ((long)obj >= 9218868437227405312L)
		{
			num &= 0x7FF0000000000000L;
		}
		double num3 = y;
		object obj2 = num >> 32;
		object obj3 = obj2 ^ num;
		double num4 = y - 1.0;
		object obj4 = num4 & 0x7FFFFFFFFFFFFFFFL;
		if ((long)obj4 >= 9218868437227405312L)
		{
			num3 &= 0x7FF0000000000000L;
		}
		object obj5 = num3 >> 32;
		object obj6 = obj5 ^ num3;
		double num5 = z;
		double num6 = z - 1.0;
		object obj7 = num6 & 0x7FFFFFFFFFFFFFFFL;
		if ((long)obj7 >= 9218868437227405312L)
		{
			num5 &= 0x7FF0000000000000L;
		}
		object obj8 = num5 >> 32;
		object obj9 = obj8 ^ num5;
		object obj10 = obj6 * 4;
		object obj11 = obj9 >> 2;
		object obj12 = obj11 ^ obj10;
		return obj12 ^ obj3;
	}

	public override bool Equals(object obj)
	{
		//IL_0013: Expected I, but got O
		//IL_0057: Expected I, but got O
		if (obj != null)
		{
			nint num = (nint)typeof(Vector3d);
			bool flag = (object)obj.GetType() != typeof(Vector3d);
			object obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			if (obj2 != null)
			{
				nint num2 = (nint)obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ rcx_v3 (Il2CppClass<System.Object>)+40]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+40]");
				if (num3 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					bool result = default(bool);
					return result;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm1,xmm2\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803D3492h\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ rcx_v3 (Il2CppClass<System.Object>)+40]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+40]");
				if (num4 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm2,xmm2\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803D3492h\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ rcx_v3 (Il2CppClass<System.Object>)+40]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+40]");
					if (num5 == 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [rsp+30h]\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803D3492h\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ rcx_v3 (Il2CppClass<System.Object>)+40]");
						nint num6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+40]");
						if (num6 == 0)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	public bool Equals(Vector3d other)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [rdx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803D33E3h\"");
		object obj2 = default(object);
		object obj = ~obj2;
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm1,qword ptr [rdx+8]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803D33E3h\"");
			object obj3 = ~obj2;
			if (obj3 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm1,qword ptr [rdx+10h]\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803D33E3h\"");
				object obj4 = ~obj2;
				if (obj4 == null)
				{
					return true;
				}
			}
		}
		return false;
	}

	public override string ToString()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E4F]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		object arg2 = default(object);
		object arg3 = default(object);
		return $"({arg:F1}, {arg2:F1}, {arg3:F1})";
	}

	public unsafe string ToString(string format)
	{
		//IL_007a: Expected Ref, but got F8
		//IL_0098: Expected Ref, but got F8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E50]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		string arg = ((double)this).ToString(format);
		double num = (double)(ref this) + 8.0;
		string arg2 = ((double*)num)->ToString(format);
		double num2 = (double)(ref this) + 16.0;
		string arg3 = ((double*)num2)->ToString(format);
		return $"({arg}, {arg2}, {arg3})";
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double Dot(ref Vector3d lhs, ref Vector3d rhs)
	{
		//IL_003f: Expected F8, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rdx+8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rdx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rdx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [lhs @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+8]");
		return 0.0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static void Cross(ref Vector3d lhs, ref Vector3d rhs, out Vector3d result)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rdx+8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rdx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,qword ptr [rdx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rdx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [lhs @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+8]");
		ref Vector3d reference = ref *(Vector3d*)null;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [rhs @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+8]");
		_ = 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double Angle(ref Vector3d from, ref Vector3d to)
	{
		//IL_0119: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAE70");
		nint num = (nint)typeof(Vector3d);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rsp+40h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,qword ptr [rsp+38h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rsp+48h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v101 @ rcx_v7 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
		bool flag = (nint)0 < (nint)0;
		double num2 = -1.0;
		double d;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v101 @ rcx_v7 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
			bool flag2 = (nint)0 >= (nint)0;
			num2 = 1.0;
			double num3 = default(double);
			d = num3;
			if (flag2)
			{
				goto IL_00e5;
			}
		}
		d = num2;
		goto IL_00e5;
		IL_00e5:
		double result = Math.Acos(d);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [182206E98h]\"");
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static void Lerp(ref Vector3d a, ref Vector3d b, double t, out Vector3d result)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,qword ptr [rcx+8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,qword ptr [rcx+8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm3\"");
		ref Vector3d reference = ref *(Vector3d*)b;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [b @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+10]");
		_ = 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static void Scale(ref Vector3d a, ref Vector3d b, out Vector3d result)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rdx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rdx+8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rdx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm1\"");
		ref Vector3d reference = ref *(Vector3d*)a;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [a @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+10]");
		_ = 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static void Normalize(ref Vector3d value, out Vector3d result)
	{
		//IL_00a2: Expected I, but got O
		//IL_004c: Expected I, but got O
		nint num = (nint)typeof(Vector3d);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAF40");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,qword ptr [182206DA0h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rcx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			nint num2 = (nint)typeof(Vector3d);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rax_v5 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+B8]");
			nint num3 = 0;
			ref Vector3d reference = ref *(Vector3d*)zero;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rax_v6 (Il2CppStaticFields<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+10]");
			_ = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm2,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm1\"");
			ref Vector3d reference = ref *(Vector3d*)value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+10]");
			_ = 0;
		}
	}

	static Vector3d()
	{
		//IL_0013: Expected I, but got O
		//IL_002d: Expected O, but got I4
		nint num = (nint)typeof(Vector3d);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+B8]");
		nint num2 = 0;
		zero = (Vector3d)0;
		_ = 0;
	}
}
