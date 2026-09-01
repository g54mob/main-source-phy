using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal;

internal struct Triangle : IEquatable<Triangle>
{
	public int index;

	public int v0;

	public int v1;

	public int v2;

	public int subMeshIndex;

	public int va0;

	public int va1;

	public int va2;

	public double err0;

	public double err1;

	public double err2;

	public double err3;

	public bool deleted;

	public bool dirty;

	public Vector3d n;

	// C# has no syntax for parameterized property 'Item'.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int get_Item(int index)
	{
		return index switch
		{
			0 => v0, 
			1 => v1, 
			_ => v2, 
		};
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void set_Item(int index, int value)
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
					ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("index");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				v2 = value;
			}
			else
			{
				v1 = value;
			}
		}
		else
		{
			v0 = value;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Triangle(int index, int v0, int v1, int v2, int subMeshIndex)
	{
		//IL_00a2: Expected O, but got I4
		this.subMeshIndex = v1;
		this.index = index;
		err3 = 0.0;
		err2 = 0.0;
		err1 = 0.0;
		err0 = 0.0;
		deleted = false;
		this.v0 = v0;
		this.v1 = v1;
		int num = default(int);
		this.v2 = num;
		va0 = v0;
		va1 = v1;
		va2 = num;
		n = (Vector3d)0;
		_ = 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void GetAttributeIndices(int[] attributeIndices)
	{
		attributeIndices[0] = va0;
		attributeIndices[1] = va1;
		attributeIndices[2] = va2;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void SetAttributeIndex(int index, int value)
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
					ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("index");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				va2 = value;
			}
			else
			{
				va1 = value;
			}
		}
		else
		{
			va0 = value;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void GetErrors(double[] err)
	{
		err[0] = err0;
		err[1] = err1;
		err[2] = err2;
	}

	public override int GetHashCode()
	{
		return index;
	}

	public override bool Equals(object obj)
	{
		//IL_0013: Expected I, but got O
		//IL_0057: Expected I, but got O
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		if (obj != null)
		{
			nint num = (nint)typeof(Triangle);
			bool flag = (object)obj.GetType() != typeof(Triangle);
			object obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			if (obj2 != null)
			{
				nint num2 = (nint)obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rcx_v2 (Il2CppClass<System.Object>)+40]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+40]");
				if (num3 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
					object obj4 = default(object);
					object obj3 = index - obj4;
					return obj3 == null;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				bool result = default(bool);
				return result;
			}
		}
		return false;
	}

	public bool Equals(Triangle other)
	{
		//IL_0014: Expected O, but got I4
		object obj = index - other.index;
		return obj == null;
	}
}
