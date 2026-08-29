using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

namespace INab.CommonVFX
{
	internal static class VFXMeshSamplingHelper
	{
		public static MeshData ComputeDataCache(Mesh input)
		{
			Vector3[] vertices = input.vertices;
			Vector3[] normals = input.normals;
			Vector4[] tangents = input.tangents;
			Color[] colors = input.colors;
			List<Vector4[]> uvs = new List<Vector4[]>();
			normals = ((normals.Length == input.vertexCount) ? normals : null);
			tangents = ((tangents.Length == input.vertexCount) ? tangents : null);
			colors = ((colors.Length == input.vertexCount) ? colors : null);
			for (int i = 0; i < 8; i++)
			{
				List<Vector4> list = new List<Vector4>();
				input.GetUVs(i, list);
				if (list.Count != input.vertexCount)
				{
					break;
				}
				uvs.Add(list.ToArray());
			}
			MeshData meshData = new MeshData();
			meshData.vertices = new MeshData.Vertex[input.vertexCount];
			int i2 = 0;
			while (i2 < input.vertexCount)
			{
				meshData.vertices[i2] = new MeshData.Vertex
				{
					position = vertices[i2],
					color = ((colors != null) ? colors[i2] : Color.white),
					normal = ((normals != null) ? normals[i2] : Vector3.up),
					tangent = ((tangents != null) ? tangents[i2] : Vector4.one),
					uvs = (from c in Enumerable.Range(0, uvs.Count)
						select uvs[c][i2]).ToArray()
				};
				int num = i2 + 1;
				i2 = num;
			}
			meshData.triangles = new MeshData.Triangle[input.triangles.Length / 3];
			int[] triangles = input.triangles;
			for (uint num2 = 0u; num2 < meshData.triangles.Length; num2++)
			{
				meshData.triangles[num2] = new MeshData.Triangle
				{
					a = (uint)triangles[num2 * 3],
					b = (uint)triangles[num2 * 3 + 1],
					c = (uint)triangles[num2 * 3 + 2]
				};
			}
			if (meshData.triangles.Length >= 1)
			{
				meshData.accumulatedTriangleArea = new double[meshData.triangles.Length];
				meshData.accumulatedTriangleArea[0] = ComputeTriangleArea(meshData, 0u);
				for (uint num3 = 1u; num3 < meshData.triangles.Length; num3++)
				{
					meshData.accumulatedTriangleArea[num3] = meshData.accumulatedTriangleArea[num3 - 1] + ComputeTriangleArea(meshData, num3);
				}
			}
			else
			{
				meshData.accumulatedTriangleArea = new double[0];
			}
			return meshData;
		}

		public static MeshData.Vertex GetInterpolatedVertex(MeshData meshData, TriangleSampling sampling)
		{
			MeshData.Triangle triangle = meshData.triangles[sampling.index];
			float x = sampling.coord.x;
			float y = sampling.coord.y;
			float num = 1f - x - y;
			MeshData.Vertex vertex = meshData.vertices[triangle.a];
			MeshData.Vertex vertex2 = meshData.vertices[triangle.b];
			MeshData.Vertex vertex3 = meshData.vertices[triangle.c];
			MeshData.Vertex result = x * vertex + y * vertex2 + num * vertex3;
			result.normal = result.normal.normalized;
			Vector3 normalized = new Vector3(result.tangent.x, result.tangent.y, result.tangent.z).normalized;
			result.tangent = new Vector4(normalized.x, normalized.y, normalized.z, (result.tangent.w > 0f) ? 1f : (-1f));
			return result;
		}

		public static TriangleSampling GetNextSampling(MeshData meshData, System.Random rand)
		{
			double area = rand.NextDouble() * meshData.accumulatedTriangleArea.Last();
			uint index = FindIndexOfArea(meshData, area);
			Vector2 vector = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());
			float x = vector.x;
			float num = Mathf.Sqrt(vector.y);
			float x2 = 1f - num;
			float y = (1f - x) * num;
			return new TriangleSampling
			{
				coord = new Vector2(x2, y),
				index = index
			};
		}

		private static double ComputeTriangleArea(MeshData meshData, uint triangleIndex)
		{
			MeshData.Triangle triangle = meshData.triangles[triangleIndex];
			Vector3 position = meshData.vertices[triangle.a].position;
			Vector3 position2 = meshData.vertices[triangle.b].position;
			Vector3 position3 = meshData.vertices[triangle.c].position;
			return 0.5f * Vector3.Cross(position2 - position, position3 - position).magnitude;
		}

		private static uint FindIndexOfArea(MeshData meshData, double area)
		{
			uint num = 0u;
			uint num2 = (uint)(meshData.accumulatedTriangleArea.Length - 1);
			uint num3 = num2 >> 1;
			while (num2 >= num)
			{
				if (num3 > meshData.accumulatedTriangleArea.Length)
				{
					throw new InvalidOperationException("Cannot Find FindIndexOfArea");
				}
				if (meshData.accumulatedTriangleArea[num3] >= area && (num3 == 0 || meshData.accumulatedTriangleArea[num3 - 1] < area))
				{
					return num3;
				}
				if (area < meshData.accumulatedTriangleArea[num3])
				{
					num2 = num3 - 1;
				}
				else
				{
					num = num3 + 1;
				}
				num3 = num + num2 >> 1;
			}
			throw new InvalidOperationException("Cannot FindIndexOfArea");
		}
	}
}
