using UnityEngine;

namespace Shapes
{
	public class PolygonPath : PointPath<Vector2>
	{
		private PolygonTriangulation lastUsedTriangulationMode;

		public void AddPoint(float x, float y)
		{
		}

		public void BezierTo(Vector2 startTangent, Vector2 endTangent, Vector2 end, int pointCount)
		{
		}

		public void BezierTo(Vector2 startTangent, Vector2 endTangent, Vector2 end, float pointsPerTurn)
		{
		}

		public void ArcTo(Vector2 corner, Vector2 next, float radius, float pointsPerTurn)
		{
		}

		public void ArcTo(Vector2 corner, Vector2 next, float radius, int pointCount)
		{
		}

		public void ArcTo(Vector2 corner, Vector2 next, float radius)
		{
		}

		public void ArcTo(Vector2 corner, Vector2 next, float radius, float pointsPerTurn, Color color)
		{
		}

		private void AddArcPoints(Vector2 corner, Vector2 next, float radius, bool useDensity, int targetPointCount, float pointsPerTurn)
		{
		}

		public bool EnsureMeshIsReadyToRender(PolygonTriangulation triangulation, out Mesh outMesh)
		{
			outMesh = null;
			return false;
		}

		private void TryUpdateMesh(PolygonTriangulation triangulation)
		{
		}
	}
}
