using System.Collections.Generic;
using Poly.Collide;
using UnityEngine;

public class TerrainIslandCollisionMesh : MonoBehaviour
{
	public static Mesh BuildCollisionMesh(Transform parent, List<PolygonShape> polygonShapes, BoxCollider boxCollider)
	{
		List<Vector3> list = new List<Vector3>();
		List<Vector2> list2 = new List<Vector2>();
		Mesh[] array = new Mesh[polygonShapes.Count];
		int num = 0;
		foreach (PolygonShape polygonShape in polygonShapes)
		{
			array[num] = new Mesh();
			list.Clear();
			list2.Clear();
			for (int i = 0; i < polygonShape.verts.Length; i++)
			{
				Vector2 vector = polygonShape.verts[i];
				Vector3 item = parent.InverseTransformPoint(new Vector3(vector.x, vector.y, 0f));
				if (item.x > boxCollider.center.x)
				{
					item.x += polygonShape.radius;
				}
				else
				{
					item.x -= polygonShape.radius;
				}
				if (item.y > boxCollider.center.y)
				{
					item.y += polygonShape.radius;
				}
				else
				{
					item.y -= polygonShape.radius;
				}
				list.Add(item);
				list2.Add(new Vector2(item.x, item.y));
			}
			array[num].vertices = list.ToArray();
			array[num].uv = list2.ToArray();
			int[] output = new int[0];
			Triangulate(array[num].vertices, ref output);
			array[num].triangles = output;
			array[num].RecalculateNormals();
			array[num].RecalculateBounds();
			num++;
		}
		CombineInstance[] array2 = new CombineInstance[array.Length];
		for (int j = 0; j < array.Length; j++)
		{
			array2[j].mesh = array[j];
			array2[j].transform = Matrix4x4.identity;
		}
		Mesh mesh = new Mesh();
		mesh.name = "combinedMesh";
		mesh.CombineMeshes(array2, mergeSubMeshes: true);
		for (int k = 0; k < array.Length; k++)
		{
			Object.Destroy(array[k]);
		}
		return mesh;
	}

	public static void Triangulate(Vector3[] points, ref int[] output)
	{
		List<int> list = new List<int>();
		int num = points.Length;
		if (num < 3)
		{
			output = new int[0];
			return;
		}
		int[] array = new int[num];
		if (Area(points, num) > 0f)
		{
			for (int i = 0; i < num; i++)
			{
				array[i] = i;
			}
		}
		else
		{
			for (int j = 0; j < num; j++)
			{
				array[j] = num - 1 - j;
			}
		}
		int num2 = num;
		int num3 = 2 * num2;
		int num4 = 0;
		int num5 = num2 - 1;
		while (num2 > 2)
		{
			if (num3-- <= 0)
			{
				if (output.Length != list.Count)
				{
					output = new int[list.Count];
				}
				list.CopyTo(output, 0);
				return;
			}
			int num6 = num5;
			if (num2 <= num6)
			{
				num6 = 0;
			}
			num5 = num6 + 1;
			if (num2 <= num5)
			{
				num5 = 0;
			}
			int num7 = num5 + 1;
			if (num2 <= num7)
			{
				num7 = 0;
			}
			if (Snip(points, num6, num5, num7, num2, array))
			{
				int item = array[num6];
				int item2 = array[num5];
				int item3 = array[num7];
				list.Add(item3);
				list.Add(item2);
				list.Add(item);
				num4++;
				int num8 = num5;
				for (int k = num5 + 1; k < num2; k++)
				{
					array[num8] = array[k];
					num8++;
				}
				num2--;
				num3 = 2 * num2;
			}
		}
		if (output.Length != list.Count)
		{
			output = new int[list.Count];
		}
		list.CopyTo(output, 0);
	}

	private static float Area(Vector3[] points, int maxCount)
	{
		float num = 0f;
		int num2 = maxCount - 1;
		int num3 = 0;
		while (num3 < maxCount)
		{
			Vector2 vector = points[num2];
			Vector2 vector2 = points[num3];
			num += vector.x * vector2.y - vector2.x * vector.y;
			num2 = num3++;
		}
		return num * 0.5f;
	}

	private static bool Snip(Vector3[] points, int u, int v, int w, int n, int[] V)
	{
		Vector2 a = points[V[u]];
		Vector2 b = points[V[v]];
		Vector2 c = points[V[w]];
		if (Mathf.Epsilon > (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x))
		{
			return false;
		}
		for (int i = 0; i < n; i++)
		{
			if (i != u && i != v && i != w)
			{
				Vector2 p = points[V[i]];
				if (InsideTriangle(a, b, c, p))
				{
					return false;
				}
			}
		}
		return true;
	}

	private static bool InsideTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
	{
		float num = C.x - B.x;
		float num2 = C.y - B.y;
		float num3 = A.x - C.x;
		float num4 = A.y - C.y;
		float num5 = B.x - A.x;
		float num6 = B.y - A.y;
		float num7 = P.x - A.x;
		float num8 = P.y - A.y;
		float num9 = P.x - B.x;
		float num10 = P.y - B.y;
		float num11 = P.x - C.x;
		float num12 = P.y - C.y;
		float num13 = num * num10 - num2 * num9;
		float num14 = num5 * num8 - num6 * num7;
		float num15 = num3 * num12 - num4 * num11;
		if (num13 >= 0f && num15 >= 0f)
		{
			return num14 >= 0f;
		}
		return false;
	}
}
