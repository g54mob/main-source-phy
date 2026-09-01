using System;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[Serializable]
	[AddComponentMenu("Surface Waves/Graphics/Wave Mesh Group")]
	public class WaveMeshGroup : GridMeshGroup
	{
		public float maximumHeight = 256f;

		public Dimensions simulationSize;

		public override void StartCreatingGroup()
		{
			if (!base.isCreatingGroup)
			{
				totalMeshWidth = simulationSize.localSize.x;
				totalMeshDepth = simulationSize.localSize.z;
				base.StartCreatingGroup();
				sessionStartPosition = simulationSize.firstCorner;
			}
		}

		protected override void Reset()
		{
			base.Reset();
			meshNamePrefix = "Grouped Wave Mesh";
		}

		protected override GridMesh CreateSegment()
		{
			GameObject gameObject = new GameObject("incomplete grouped mesh");
			return gameObject.AddComponent<WaveMesh>();
		}

		protected override void SetupSegment(GridMesh segment)
		{
			base.SetupSegment(segment);
			WaveMesh waveMesh = segment as WaveMesh;
			if (waveMesh == null)
			{
				throw new NullReferenceException("The segment can not be processed because it isn't a WaveMesh segment");
			}
			waveMesh.verticalBoundIncrease = maximumHeight;
		}
	}
}
