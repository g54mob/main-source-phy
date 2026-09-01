using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

public struct SymmetricMatrix
{
	public double m0;

	public double m1;

	public double m2;

	public double m3;

	public double m4;

	public double m5;

	public double m6;

	public double m7;

	public double m8;

	public double m9;

	// C# has no syntax for parameterized property 'Item'.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double get_Item(int index)
	{
		//IL_002a: Expected O, but got I8
		//IL_0044: Expected O, but got I8
		if (index <= 9)
		{
			object obj = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ r8_v2+3BAA88+index @ rdx (System.Int32)*4]");
			object obj2 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v17 @ rdx_v4 (should have been resolved before IL gen)");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("index");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SymmetricMatrix(double c)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm1\"");
		m0 = c;
		m2 = c;
		m4 = c;
		m6 = c;
		m8 = c;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SymmetricMatrix(double m0, double m1, double m2, double m3, double m4, double m5, double m6, double m7, double m8, double m9)
	{
		//IL_001e: Expected F8, but got I
		this.m3 = m8;
		this.m0 = m0;
		IntPtr intPtr = default(IntPtr);
		this.m5 = (nint)intPtr;
		this.m4 = m9;
		double num = default(double);
		this.m7 = num;
		double num2 = default(double);
		this.m6 = num2;
		double num3 = default(double);
		this.m9 = num3;
		this.m1 = m1;
		this.m2 = m2;
		double num4 = default(double);
		this.m8 = num4;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SymmetricMatrix(double a, double b, double c, double d)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm2\"");
		m0 = a;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm3\"");
		m2 = a;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
		m3 = a;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm5\"");
		m4 = b;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm5,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm3\"");
		m1 = a;
		m5 = b;
		m6 = b;
		double num = default(double);
		m9 = num;
		m7 = c;
		m8 = c;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static SymmetricMatrix operator +(SymmetricMatrix a, SymmetricMatrix b)
	{
		//IL_0021: Expected native int or pointer, but got O
		//IL_003d: Expected native int or pointer, but got O
		//IL_0059: Expected native int or pointer, but got O
		//IL_0075: Expected native int or pointer, but got O
		//IL_0091: Expected native int or pointer, but got O
		//IL_00ad: Expected native int or pointer, but got O
		//IL_00c9: Expected native int or pointer, but got O
		//IL_00e5: Expected native int or pointer, but got O
		//IL_0101: Expected native int or pointer, but got O
		//IL_0113: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [r8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [r8+8]\"");
		SymmetricMatrix symmetricMatrix = default(SymmetricMatrix);
		((SymmetricMatrix*)(nint)symmetricMatrix)->m0 = a.m0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [r8+10h]\"");
		((SymmetricMatrix*)(nint)symmetricMatrix)->m1 = a.m1;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [r8+18h]\"");
		((SymmetricMatrix*)(nint)symmetricMatrix)->m2 = a.m2;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [r8+20h]\"");
		((SymmetricMatrix*)(nint)symmetricMatrix)->m3 = a.m3;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [r8+28h]\"");
		((SymmetricMatrix*)(nint)symmetricMatrix)->m4 = a.m4;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [r8+30h]\"");
		((SymmetricMatrix*)(nint)symmetricMatrix)->m5 = a.m5;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [r8+38h]\"");
		((SymmetricMatrix*)(nint)symmetricMatrix)->m6 = a.m6;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [r8+40h]\"");
		((SymmetricMatrix*)(nint)symmetricMatrix)->m7 = a.m7;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [r8+48h]\"");
		((SymmetricMatrix*)(nint)symmetricMatrix)->m8 = a.m8;
		((SymmetricMatrix*)(nint)symmetricMatrix)->m9 = a.m9;
		return symmetricMatrix;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal double Determinant1()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+38h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rcx+28h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,qword ptr [rcx+28h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,qword ptr [rcx+38h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm3\"");
		return m5;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal double Determinant2()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+18h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+40h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+28h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,qword ptr [rcx+18h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rcx+8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,qword ptr [rcx+38h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rcx+38h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+28h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+40h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm2\"");
		return m6;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal double Determinant3()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+18h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+40h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,qword ptr [rcx+8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,qword ptr [rcx+38h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rcx+38h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+8]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+40h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm2\"");
		return m6;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal double Determinant4()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+18h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+40h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rcx+28h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,qword ptr [rcx+28h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+10h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,qword ptr [rcx+40h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm3\"");
		return m6;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double Determinant(int a11, int a12, int a13, int a21, int a22, int a23, int a31, int a32, int a33)
	{
		double num = this.get_Item(a11);
		IntPtr intPtr = default(IntPtr);
		double num2 = this.get_Item((int)(nint)intPtr);
		int index = default(int);
		double num3 = this.get_Item(index);
		double num4 = this.get_Item(a13);
		double result = this.get_Item(a33);
		int index2 = default(int);
		double num5 = this.get_Item(index2);
		double num6 = this.get_Item(a12);
		int index3 = default(int);
		double num7 = this.get_Item(index3);
		int index4 = default(int);
		double num8 = this.get_Item(index4);
		double num9 = this.get_Item(a13);
		double num10 = this.get_Item((int)(nint)intPtr);
		double num11 = this.get_Item(index4);
		double num12 = this.get_Item(a11);
		double num13 = this.get_Item(index3);
		double num14 = this.get_Item(index2);
		double num15 = this.get_Item(a12);
		double num16 = this.get_Item(a33);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rsp+28h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rsp+38h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm15\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm12,xmm13\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rsp+40h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm10\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm12,xmm11\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rsp+50h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm14\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm12\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm9\"");
		double num17 = this.get_Item(index);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm7,xmm8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm0\"");
		return result;
	}
}
