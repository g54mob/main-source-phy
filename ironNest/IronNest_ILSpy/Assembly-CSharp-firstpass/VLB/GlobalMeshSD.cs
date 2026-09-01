using System;
using UnityEngine;

namespace VLB;

public static class GlobalMeshSD
{
	private static Mesh ms_Mesh;

	private static bool ms_DoubleSided;

	public static Mesh Get()
	{
		Config instance = Config.Instance;
		if ((object)instance != null)
		{
			bool sD_useSinglePassShader = instance.SD_useSinglePassShader;
			if (ms_Mesh != null && ms_DoubleSided == sD_useSinglePassShader)
			{
				goto IL_019b;
			}
			if (ms_Mesh != null)
			{
				UnityEngine.Object.DestroyImmediate(ms_Mesh);
				ms_Mesh = null;
			}
			Config instance2 = Config.Instance;
			if ((object)instance2 != null)
			{
				Config instance3 = Config.Instance;
				if ((object)instance3 != null)
				{
					int numSegments = default(int);
					bool cap = default(bool);
					bool doubleSided = default(bool);
					Mesh mesh = MeshGenerator.GenerateConeZ_Radii(1f, 1f, 1f, instance2.sharedMeshSides, numSegments, cap, doubleSided);
					ms_Mesh = mesh;
					HideFlags proceduralObjectsHideFlags = Consts.Internal.ProceduralObjectsHideFlags;
					if ((object)ms_Mesh != null)
					{
						ms_Mesh.hideFlags = proceduralObjectsHideFlags;
						ms_DoubleSided = sD_useSinglePassShader;
						goto IL_019b;
					}
				}
			}
		}
		return (Mesh)(object)new NullReferenceException();
		IL_019b:
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
