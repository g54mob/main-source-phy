using System.Collections.Generic;
using UnityEngine;

namespace Koenigz.PerfectCulling
{
	[RequireComponent(typeof(Terrain))]
	public class PerfectCullingTerrain : PerfectCullingMonoGroup
	{
		[Range(16f, 2048f)]
		public int MeshResolutionX;

		[Range(16f, 2048f)]
		public int MeshResolutionZ;

		[Header("Creates double-sided mesh to make it not see-through from the other side.")]
		public bool DoubleSided;

		[HideInInspector]
		public Renderer terrainMeshRenderer;

		[HideInInspector]
		public MeshFilter terrainMeshFilter;

		public override List<Renderer> Renderers => null;

		public override List<Behaviour> UnityBehaviours => null;

		public override void PreBake(PerfectCullingBakingBehaviour bakingBehaviour)
		{
		}

		public override void PreSceneSave(PerfectCullingBakingBehaviour bakingBehaviour)
		{
		}

		public override void PostBake(PerfectCullingBakingBehaviour bakingBehaviour)
		{
		}

		private void UpdateRenderer()
		{
		}

		public Renderer CreatePreview()
		{
			return null;
		}

		public Renderer GetPreview()
		{
			return null;
		}

		public void DestroyPreview()
		{
		}
	}
}
