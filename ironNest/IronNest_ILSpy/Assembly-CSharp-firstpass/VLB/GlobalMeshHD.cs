using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public static class GlobalMeshHD
{
	private static Mesh ms_Mesh;

	public static Mesh Get()
	{
		//IL_00ea: Expected I, but got O
		if (ms_Mesh == null)
		{
			if (ms_Mesh != null)
			{
				UnityEngine.Object.DestroyImmediate(ms_Mesh);
				ms_Mesh = null;
			}
			Config instance = Config.GetInstance(true);
			if ((object)instance != null)
			{
				bool inverted = default(bool);
				Mesh mesh = MeshGenerator.GenerateConeZ_Radii_DoubleCaps(1f, 1f, 1f, instance.sharedMeshSides, inverted);
				ms_Mesh = mesh;
				nint num = (nint)typeof(Consts.Internal);
				if ((object)ms_Mesh != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb edx,edx\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ rax_v23 (Il2CppClass<VLB.Consts+Internal>)+B8]");
					HideFlags hideFlags = (HideFlags)((nint)0 + (nint)61);
					ms_Mesh.hideFlags = hideFlags;
					goto IL_013b;
				}
			}
			return (Mesh)(object)new NullReferenceException();
		}
		goto IL_013b;
		IL_013b:
		return ms_Mesh;
	}

	public static void Destroy()
	{
		if (ms_Mesh != null)
		{
			UnityEngine.Object.DestroyImmediate(ms_Mesh);
			ms_Mesh = null;
		}
	}
}
