using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Mesh/Wave Mesh")]
	public class WaveMesh : GridMesh
	{
		public float verticalBoundIncrease = 50f;

		public override void GenerateGrid()
		{
			base.GenerateGrid();
			increaseBoundsSize();
		}

		private void increaseBoundsSize()
		{
			Bounds bounds = generatedMesh.bounds;
			Vector3 center = bounds.center;
			center.y += verticalBoundIncrease;
			bounds.Encapsulate(center);
			generatedMesh.bounds = bounds;
		}
	}
}
