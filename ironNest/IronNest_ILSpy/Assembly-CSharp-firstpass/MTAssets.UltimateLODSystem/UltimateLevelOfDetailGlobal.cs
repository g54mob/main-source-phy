using System;
using MTAssets.UltimateLODSystem.MeshSimplifier;
using UnityEngine;

namespace MTAssets.UltimateLODSystem;

public class UltimateLevelOfDetailGlobal : MonoBehaviour
{
	private static bool enableGlobalUlodSystem = true;

	private static float lodDistanceMultiplier = 1f;

	public static Camera currentCameraThatIsOnTopOfScreenInThisScene = null;

	public static bool isGlobalULodSystemEnabled()
	{
		return enableGlobalUlodSystem;
	}

	public static void EnableGlobalULodSystem(bool enable)
	{
		enableGlobalUlodSystem = enable;
	}

	public static void SetGlobalLodDistanceMultiplier(float multiplier)
	{
		lodDistanceMultiplier = multiplier;
	}

	public static float GetGlobalLodDistanceMultiplier()
	{
		return lodDistanceMultiplier;
	}

	public unsafe static Mesh GetSimplifiedVersionOfThisMesh(Mesh meshToSimplify, bool isSkinnedMesh, bool skinnedAnimsCompatibilityMode, bool simplificationDestroyerMode, bool preventArtifacts, float percentOfVerticesOfSimplifyiedVersion)
	{
		//IL_0043: Expected O, but got Ref
		//IL_0051: Expected O, but got I4
		MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier meshSimplifier = new MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier();
		float num = (simplificationDestroyerMode ? 0.4f : 1f);
		object obj = default(object);
		Mesh mesh;
		if (obj != null || meshSimplifier != null)
		{
			object obj2 = default(object);
			MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier.ValidateOptions((SimplificationOptions)(&obj2));
			meshSimplifier.simplificationOptions = (SimplificationOptions)0;
			_ = 100;
			_ = 0;
			meshSimplifier.Initialize(meshToSimplify);
			object obj3 = default(object);
			float num2 = (float)obj3 / 100f;
			float quality = num2 * num;
			meshSimplifier.SimplifyMesh(quality);
			mesh = meshSimplifier.ToMesh();
			if (!isSkinnedMesh || !skinnedAnimsCompatibilityMode)
			{
				goto IL_0138;
			}
			if ((object)meshToSimplify != null)
			{
				Matrix4x4[] bindposes = meshToSimplify.bindposes;
				if ((object)mesh != null)
				{
					mesh.bindposes = bindposes;
					goto IL_0138;
				}
			}
		}
		return (Mesh)(object)new NullReferenceException();
		IL_0138:
		return mesh;
	}
}
