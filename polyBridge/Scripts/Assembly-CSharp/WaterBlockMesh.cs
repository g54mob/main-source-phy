using System.Collections.Generic;
using UnityEngine;

public class WaterBlockMesh
{
	public static float DEFAULT_HEIGHT = 4f;

	private static List<int> m_MeshBottomVertIndicies = new List<int>();

	private static Dictionary<int, float> m_SharedVertOffsets = new Dictionary<int, float>();

	public static Mesh Create(Mesh baseMesh, Mesh leftSide, Mesh rightSide, float width, float height)
	{
		int num = Mathf.CeilToInt(width);
		if (num < 2)
		{
			num = 2;
		}
		if (num % 2 != 0)
		{
			num = (num / 2 + 1) * 2;
		}
		List<Mesh> list = new List<Mesh>();
		if (leftSide != null)
		{
			Mesh item = AllocateMesh(leftSide, 0);
			list.Add(item);
		}
		int num2 = 0;
		for (num2 = 0; num2 < num / 2; num2++)
		{
			Mesh item2 = AllocateMesh(baseMesh, num2);
			list.Add(item2);
		}
		if (rightSide != null && num2 > 0)
		{
			Mesh item3 = AllocateMesh(rightSide, num2 - 1);
			list.Add(item3);
		}
		CombineInstance[] array = new CombineInstance[list.Count];
		int i;
		for (i = 0; i < list.Count; i++)
		{
			array[i].mesh = list[i];
			array[i].transform = Matrix4x4.identity;
		}
		Mesh mesh = new Mesh();
		mesh.name = "combinedMesh" + i;
		mesh.CombineMeshes(array, mergeSubMeshes: true);
		SetUVs(mesh, num);
		if (leftSide != null || rightSide != null)
		{
			TranslateMeshVerts(mesh, height - DEFAULT_HEIGHT);
		}
		for (int j = 0; j < list.Count; j++)
		{
			Object.Destroy(list[j]);
		}
		return mesh;
	}

	public static void SetUVsOld(Mesh mesh, int width)
	{
	}

	public static void SetUVs(Mesh mesh, int width)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = float.MaxValue;
		float num4 = float.MinValue;
		float num5 = float.MaxValue;
		float num6 = float.MinValue;
		Vector3[] vertices = mesh.vertices;
		int vertexCount = mesh.vertexCount;
		Vector3[] array = vertices;
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 vector = array[i];
			if (vector.x < num3)
			{
				num3 = vector.x;
			}
			if (vector.x > num4)
			{
				num4 = vector.x;
			}
			if (vector.z < num5)
			{
				num5 = vector.z;
			}
			if (vector.z > num6)
			{
				num6 = vector.z;
			}
			if (vector.y > num)
			{
				num = vector.y;
			}
			if (Mathf.Abs(vector.z) > num2)
			{
				num2 = Mathf.Abs(vector.z);
			}
		}
		num -= 0.001f;
		m_SharedVertOffsets.Clear();
		float num7 = num4 - num3;
		float num8 = num6 - num5;
		Vector2[] array2 = new Vector2[vertexCount];
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j] = new Vector2((vertices[j].x - num3) / num7, (vertices[j].z - num5) / num8);
		}
		mesh.uv = array2;
	}

	private static void TranslateMeshVerts(Mesh mesh, float height)
	{
		m_MeshBottomVertIndicies.Clear();
		m_MeshBottomVertIndicies.AddRange(GetMeshBottomVertIndicies(mesh));
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < m_MeshBottomVertIndicies.Count; i++)
		{
			vertices[m_MeshBottomVertIndicies[i]].y = 0f - height;
		}
		mesh.vertices = vertices;
		mesh.RecalculateBounds();
	}

	private static List<int> GetMeshBottomVertIndicies(Mesh mesh)
	{
		List<int> list = new List<int>();
		Vector3[] vertices = mesh.vertices;
		float num = float.MaxValue;
		for (int i = 0; i < vertices.Length; i++)
		{
			if (vertices[i].y < num)
			{
				num = vertices[i].y;
			}
		}
		for (int j = 0; j < vertices.Length; j++)
		{
			if (vertices[j].y < num + 0.01f)
			{
				list.Add(j);
			}
		}
		return list;
	}

	private static Mesh AllocateMesh(Mesh template, int index)
	{
		Mesh mesh = new Mesh();
		Vector3[] vertices = template.vertices;
		for (int i = 0; i < template.vertexCount; i++)
		{
			vertices[i].x += index * 2;
		}
		mesh.vertices = vertices;
		mesh.triangles = template.triangles;
		mesh.uv = template.uv;
		mesh.normals = template.normals;
		mesh.colors = template.colors;
		mesh.name = "mesh";
		return mesh;
	}
}
