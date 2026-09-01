using UnityEngine;

namespace MTAssets.UltimateLODSystem
{
	[AddComponentMenu(null)]
	public class UltimateLevelOfDetailGlobal : MonoBehaviour
	{
		private static bool enableGlobalUlodSystem;

		private static float lodDistanceMultiplier;

		public static Camera currentCameraThatIsOnTopOfScreenInThisScene;

		public static bool isGlobalULodSystemEnabled()
		{
			return false;
		}

		public static void EnableGlobalULodSystem(bool enable)
		{
		}

		public static void SetGlobalLodDistanceMultiplier(float multiplier)
		{
		}

		public static float GetGlobalLodDistanceMultiplier()
		{
			return 0f;
		}

		public static Mesh GetSimplifiedVersionOfThisMesh(Mesh meshToSimplify, bool isSkinnedMesh, bool skinnedAnimsCompatibilityMode, bool simplificationDestroyerMode, bool preventArtifacts, float percentOfVerticesOfSimplifyiedVersion)
		{
			return null;
		}
	}
}
