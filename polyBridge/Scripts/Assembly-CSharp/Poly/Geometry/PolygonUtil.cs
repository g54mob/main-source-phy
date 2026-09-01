using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Poly.Geometry
{
	public static class PolygonUtil
	{
		public static bool AreVertsFormingAValidPolygon(Vector2[] verts)
		{
			return PolygonFromVerts(verts).IsValid();
		}

		public static Polygon PolygonFromVerts(Vector2[] verts)
		{
			Polygon result = default(Polygon);
			result.verts = new Vec2[verts.Length];
			for (int i = 0; i < result.verts.Length; i++)
			{
				result.verts[i] = verts[i];
			}
			return result;
		}

		public static bool IsValid(this Polygon poly)
		{
			List<Vec2> list = poly.verts.ToList();
			RemoveDuplicatedVertices(list, 0.01f);
			Polygon poly2 = default(Polygon);
			poly2.verts = list.ToArray();
			bool flag = poly2.IsSelfIntersecting();
			bool flag2 = poly2.IsAnyVertexWithinDistanceOfAnySegment();
			if (list.Count >= 3 && !flag)
			{
				return !flag2;
			}
			return false;
		}

		public static int RemoveDuplicatedVertices(List<Vec2> verts, float tolerance)
		{
			int num = 0;
			float num2 = tolerance * tolerance;
			Vec2 a = verts[0];
			for (int num3 = verts.Count - 1; num3 > 0; num3--)
			{
				Vec2 b = verts[num3];
				if (Vec2.DistanceSqr(in a, in b) < num2)
				{
					verts.RemoveAt(num3);
					num++;
				}
				else
				{
					a = b;
				}
			}
			return num;
		}

		public static bool IsSelfIntersecting(this Polygon poly)
		{
			ref Vec2[] verts = ref poly.verts;
			int num = verts.Length;
			bool flag = false;
			Vec2 v = verts.Last();
			Segment segA_in = default(Segment);
			Segment segB_in = default(Segment);
			for (int i = 0; i < verts.Length; i++)
			{
				if (flag)
				{
					break;
				}
				Vec2 vec = verts[i];
				segA_in.v0 = v;
				segA_in.v1 = vec;
				Vec2 v2 = verts[(i + 1) % num];
				int num2 = (i + 2) % num;
				int num3 = (i - 1 + num) % num;
				int num4 = num2;
				while (num4 != num3 && !flag)
				{
					Vec2 vec2 = verts[num4];
					segB_in.v0 = v2;
					segB_in.v1 = vec2;
					flag = SegmentUtil._AreNonZeroSegmentsIntersecting_OrNearlyIntersecting(ref segA_in, ref segB_in);
					v2 = vec2;
					num4 = (num4 + 1) % num;
				}
				v = vec;
			}
			return flag;
		}

		public static bool IsAnyVertexWithinDistanceOfAnySegment(this Polygon poly)
		{
			ref Vec2[] verts = ref poly.verts;
			int num = verts.Length;
			bool flag = false;
			Vec2 v = verts.Last();
			Segment segA = default(Segment);
			for (int i = 0; i < verts.Length; i++)
			{
				if (flag)
				{
					break;
				}
				Vec2 vec = verts[i];
				segA.v0 = v;
				segA.v1 = vec;
				v = vec;
				int num2 = (i + 1) % num;
				int num3 = (i - 2 + num) % num;
				int num4 = num2;
				while (num4 != num3 && !flag)
				{
					Vec2 v2 = verts[num4];
					flag = SegmentUtil._IsVertexWithinDistanceOfANonZeroSegment(ref segA, ref v2, 0.01f);
					num4 = (num4 + 1) % num;
				}
			}
			return flag;
		}

		public static void SubdivideConvexShapeVertices_ForMultipleShapes(List<List<Vec2>> vertLists, int maxSize)
		{
			List<int> list = new List<int>();
			List<List<Vec2>> list2 = new List<List<Vec2>>();
			for (int i = 0; i < vertLists.Count; i++)
			{
				if (0 < SubdivideConvexShapeVertices(vertLists[i], maxSize, list2))
				{
					list.Add(i);
				}
			}
			for (int num = list.Count - 1; num >= 0; num--)
			{
				vertLists.RemoveAt(list[num]);
			}
			vertLists.AddRange(list2);
		}

		public static int SubdivideConvexShapeVertices(List<Vec2> vertsToSplit, int maxSize, List<List<Vec2>> newChunksOut)
		{
			int num = 0;
			if (vertsToSplit.Count > maxSize)
			{
				int num2 = (vertsToSplit.Count - maxSize - 1) / (maxSize - 2) + 1 + 1;
				int num3 = vertsToSplit.Count + 2 * (num2 - 1);
				int num4 = num3 / num2;
				int num5 = num3 % num2;
				int num6 = 0;
				int num7 = vertsToSplit.Count - 1;
				int num8 = 0;
				for (int i = 0; i < num2; i++)
				{
					bool num9 = i % 2 == 0;
					int num10 = num4;
					if (i < num5)
					{
						num10++;
					}
					List<Vec2> list = new List<Vec2>();
					newChunksOut.Add(list);
					num++;
					if (num9)
					{
						list.Add(vertsToSplit[num7]);
						list.Add(vertsToSplit[num6]);
						num8 += 2;
						for (int j = 0; j + 2 < num10; j++)
						{
							num6++;
							list.Add(vertsToSplit[num6]);
							num8++;
						}
						continue;
					}
					list.Add(vertsToSplit[num6]);
					list.Add(vertsToSplit[num7]);
					num8 += 2;
					for (int k = 0; k + 2 < num10; k++)
					{
						num7--;
						list.Add(vertsToSplit[num7]);
						num8++;
					}
					list.Reverse();
				}
			}
			return num;
		}
	}
}
