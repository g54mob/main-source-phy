using System.Collections.Generic;

namespace MoreMountains.Tools
{
	public static class MMGeometry
	{
		public struct MMEdge
		{
			public int Vertice1;

			public int Vertice2;

			public int TriangleIndex;

			public MMEdge(int aV1, int aV2, int aIndex)
			{
				Vertice1 = 0;
				Vertice2 = 0;
				TriangleIndex = 0;
			}
		}

		public static List<MMEdge> GetEdges(int[] indices)
		{
			return null;
		}

		public static List<MMEdge> FindBoundary(this List<MMEdge> edges)
		{
			return null;
		}

		public static List<MMEdge> SortEdges(this List<MMEdge> edges)
		{
			return null;
		}
	}
}
