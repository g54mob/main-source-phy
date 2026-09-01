using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	public static class ShapesMeshGen
	{
		private enum ReflexState
		{
			Unknown = 0,
			Reflex = 1,
			Convex = 2
		}

		private class EarClipPoint
		{
			public int vertIndex;

			public Vector2 pt;

			private ReflexState reflex;

			public EarClipPoint prev;

			public EarClipPoint next;

			public ReflexState ReflexState => default(ReflexState);

			public EarClipPoint(int vertIndex, Vector2 pt)
			{
			}

			public void MarkReflexUnknown()
			{
			}
		}

		private static readonly ExpandoList<Color> meshColors;

		private static readonly ExpandoList<Vector3> meshVertices;

		private static readonly ExpandoList<Vector4> meshUv0;

		private static readonly ExpandoList<Vector3> meshUv1Prevs;

		private static readonly ExpandoList<Vector3> meshUv2Nexts;

		private static readonly ExpandoList<int> meshTriangles;

		private static readonly ExpandoList<int> meshJoinsTriangles;

		private static bool generatingClockwisePolygon;

		private static bool SamePosition(Vector3 a, Vector3 b)
		{
			return false;
		}

		public static void GenPolylineMesh(Mesh mesh, IList<PolylinePoint> path, bool closed, PolylineJoins joins, bool flattenZ, bool useColors)
		{
		}

		public static void GenPolygonMesh(Mesh mesh, List<Vector2> path, PolygonTriangulation triangulation)
		{
		}

		public static void CreateDisc(Mesh mesh, int segmentsPerFullTurn, float radius)
		{
		}

		public static void CreateCircleSector(Mesh mesh, int segmentsPerFullTurn, float radius, float angRadiansStart, float angRadiansEnd)
		{
		}

		public static void CreateAnnulus(Mesh mesh, int segmentsPerFullTurn, float radius, float radiusInner)
		{
		}

		public static void CreateAnnulusSector(Mesh mesh, int segmentsPerFullTurn, float radius, float radiusInner, float angRadiansStart, float angRadiansEnd)
		{
		}

		private static void GenerateDiscMesh(Mesh mesh, int segmentsPerFullTurn, bool hasSector, bool hasInnerRadius, float radius, float radiusInner, float angRadiansStart, float angRadiansEnd)
		{
		}
	}
}
