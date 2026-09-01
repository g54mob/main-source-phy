using UnityEngine;

namespace Koenigz.PerfectCulling
{
	[RequireComponent(typeof(Terrain))]
	public class TerrainToMeshUtility : MonoBehaviour
	{
		private readonly string EditorOnlyTag;

		[Header("Creates double-sided mesh to make it not see-through from the other side.")]
		public bool DoubleSided;

		[Range(1f, 2048f)]
		public int MeshResolutionX;

		[Range(1f, 2048f)]
		public int MeshResolutionZ;

		public MeshRenderer meshRendererReference;

		public void CreateOrUpdateMesh()
		{
		}

		public static Mesh CreateMesh(Terrain terrain, int meshResolutionX, int meshResolutionZ, bool doubleSided)
		{
			return null;
		}
	}
}
