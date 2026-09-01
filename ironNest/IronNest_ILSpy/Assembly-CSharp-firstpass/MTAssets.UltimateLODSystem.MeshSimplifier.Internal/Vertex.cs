using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal;

internal struct Vertex : IEquatable<Vertex>
{
	public int index;

	public Vector3d p;

	public int tstart;

	public int tcount;

	public SymmetricMatrix q;

	public bool borderEdge;

	public bool uvSeamEdge;

	public bool uvFoldoverEdge;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vertex(int index, Vector3d p)
	{
		//IL_0019: Expected O, but got F8
		//IL_0039: Expected O, but got I4
		this.index = index;
		this.p = (Vector3d)p.x;
		tstart = 0;
		_ = p.z;
		q = (SymmetricMatrix)0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		borderEdge = true;
		uvFoldoverEdge = false;
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
			nint num = (nint)typeof(Vertex);
			bool flag = (object)obj.GetType() != typeof(Vertex);
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
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+40]");
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

	public bool Equals(Vertex other)
	{
		//IL_0014: Expected O, but got I4
		object obj = index - other.index;
		return obj == null;
	}
}
