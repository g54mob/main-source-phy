using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTCommon
{
	public static class GraphicsUtility
	{
		private static readonly List<Vector2> s_vector2List = new List<Vector2>();

		private static readonly List<Vector3> s_vector3List = new List<Vector3>();

		private static readonly List<Color> s_colorList = new List<Color>();

		private static readonly List<int> s_indexList = new List<int>();

		private static readonly Vector2 k_Billboard0 = new Vector2(-1f, -1f);

		private static readonly Vector2 k_Billboard1 = new Vector2(-1f, 1f);

		private static readonly Vector2 k_Billboard2 = new Vector2(1f, -1f);

		private static readonly Vector2 k_Billboard3 = new Vector2(1f, 1f);

		private static readonly RTECommandBuffer s_commandBuffer = new RTECommandBuffer(null);

		public static float GetScreenScale(Vector3 position, Camera camera)
		{
			float num = camera.pixelHeight;
			if (camera.orthographic)
			{
				return camera.orthographicSize * 2f / num * 90f;
			}
			Transform transform = camera.transform;
			float num2 = (camera.stereoEnabled ? (position - transform.position).magnitude : Vector3.Dot(position - transform.position, transform.forward));
			return 2f * num2 * Mathf.Tan(camera.fieldOfView * 0.5f * (MathF.PI / 180f)) / num * 90f;
		}

		public static Mesh CreateCube(Color color, Vector3 center, float scale, float cubeLength = 1f, float cubeWidth = 1f, float cubeHeight = 1f)
		{
			cubeHeight *= scale;
			cubeWidth *= scale;
			cubeLength *= scale;
			Vector3 vector = center + new Vector3((0f - cubeLength) * 0.5f, (0f - cubeWidth) * 0.5f, cubeHeight * 0.5f);
			Vector3 vector2 = center + new Vector3(cubeLength * 0.5f, (0f - cubeWidth) * 0.5f, cubeHeight * 0.5f);
			Vector3 vector3 = center + new Vector3(cubeLength * 0.5f, (0f - cubeWidth) * 0.5f, (0f - cubeHeight) * 0.5f);
			Vector3 vector4 = center + new Vector3((0f - cubeLength) * 0.5f, (0f - cubeWidth) * 0.5f, (0f - cubeHeight) * 0.5f);
			Vector3 vector5 = center + new Vector3((0f - cubeLength) * 0.5f, cubeWidth * 0.5f, cubeHeight * 0.5f);
			Vector3 vector6 = center + new Vector3(cubeLength * 0.5f, cubeWidth * 0.5f, cubeHeight * 0.5f);
			Vector3 vector7 = center + new Vector3(cubeLength * 0.5f, cubeWidth * 0.5f, (0f - cubeHeight) * 0.5f);
			Vector3 vector8 = center + new Vector3((0f - cubeLength) * 0.5f, cubeWidth * 0.5f, (0f - cubeHeight) * 0.5f);
			Vector3[] array = new Vector3[24]
			{
				vector, vector2, vector3, vector4, vector8, vector5, vector, vector4, vector5, vector6,
				vector2, vector, vector7, vector8, vector4, vector3, vector6, vector7, vector3, vector2,
				vector8, vector7, vector6, vector5
			};
			int[] triangles = new int[36]
			{
				3, 1, 0, 3, 2, 1, 7, 5, 4, 7,
				6, 5, 11, 9, 8, 11, 10, 9, 15, 13,
				12, 15, 14, 13, 19, 17, 16, 19, 18, 17,
				23, 21, 20, 23, 22, 21
			};
			Color[] array2 = new Color[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = color;
			}
			Mesh mesh = new Mesh();
			mesh.name = "cube";
			mesh.vertices = array;
			mesh.triangles = triangles;
			mesh.colors = array2;
			mesh.RecalculateNormals();
			return mesh;
		}

		public static Mesh CreateQuad(float quadWidth = 1f, float quadHeight = 1f)
		{
			Vector3 vector = new Vector3((0f - quadWidth) * 0.5f, (0f - quadHeight) * 0.5f, 0f);
			Vector3 vector2 = new Vector3(quadWidth * 0.5f, (0f - quadHeight) * 0.5f, 0f);
			Vector3 vector3 = new Vector3((0f - quadWidth) * 0.5f, quadHeight * 0.5f, 0f);
			Vector3 vector4 = new Vector3(quadWidth * 0.5f, quadHeight * 0.5f, 0f);
			Vector3[] vertices = new Vector3[4] { vector3, vector4, vector2, vector };
			int[] triangles = new int[6] { 3, 1, 0, 3, 2, 1 };
			Vector2[] uv = new Vector2[4]
			{
				new Vector2(1f, 0f),
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 1f)
			};
			Mesh mesh = new Mesh();
			mesh.name = "quad";
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.uv = uv;
			mesh.RecalculateNormals();
			return mesh;
		}

		public static Mesh CreateWireQuad(float width = 1f, float height = 1f)
		{
			Vector3 vector = new Vector3((0f - width) * 0.5f, (0f - height) * 0.5f, 0f);
			Vector3 vector2 = new Vector3(width * 0.5f, (0f - height) * 0.5f, 0f);
			Vector3 vector3 = new Vector3(width * 0.5f, height * 0.5f, 0f);
			Vector3 vector4 = new Vector3((0f - width) * 0.5f, height * 0.5f, 0f);
			Vector3[] vertices = new Vector3[4] { vector, vector2, vector3, vector4 };
			Mesh mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.SetIndices(new int[8] { 0, 1, 1, 2, 2, 3, 3, 0 }, MeshTopology.Lines, 0);
			return mesh;
		}

		public static Mesh CreateWireCubeMesh()
		{
			Mesh mesh = new Mesh();
			mesh.vertices = new Vector3[8]
			{
				new Vector3(-1f, -1f, -1f),
				new Vector3(-1f, -1f, 1f),
				new Vector3(-1f, 1f, -1f),
				new Vector3(-1f, 1f, 1f),
				new Vector3(1f, -1f, -1f),
				new Vector3(1f, -1f, 1f),
				new Vector3(1f, 1f, -1f),
				new Vector3(1f, 1f, 1f)
			};
			mesh.SetIndices(new int[24]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 0, 2,
				1, 3, 4, 6, 5, 7, 0, 4, 1, 5,
				2, 6, 3, 7
			}, MeshTopology.Lines, 0);
			return mesh;
		}

		public static Mesh CreateCircle(float radius = 1f, int pointsCount = 64)
		{
			return CreateArc(Vector3.zero, radius, pointsCount);
		}

		public static Mesh CreateArc(Vector3 offset, float radius = 1f, int pointsCount = 64, float fromAngle = 0f, float toAngle = MathF.PI * 2f)
		{
			Vector3[] array = new Vector3[pointsCount + 2];
			Vector2[] array2 = new Vector2[pointsCount + 2];
			List<int> list = new List<int>();
			for (int i = 1; i <= pointsCount; i++)
			{
				list.Add(0);
				list.Add(i);
				list.Add(i + 1);
			}
			float num = fromAngle;
			float num2 = toAngle - fromAngle;
			float z = 0f;
			float num3 = Mathf.Cos(num);
			float num4 = Mathf.Sin(num);
			array[0] = offset;
			array2[0] = Vector2.one * 0.5f;
			Vector3 vector = new Vector3(num3 * radius, num4 * radius, z) + offset;
			Vector2 vector2 = (Vector2.one + new Vector2(num3, num4)) * 0.5f;
			vector2.y = 1f - vector2.y;
			for (int j = 1; j <= pointsCount; j++)
			{
				array[j] = vector;
				array2[j] = vector2;
				num += num2 / (float)pointsCount;
				num3 = Mathf.Cos(num);
				num4 = Mathf.Sin(num);
				vector = new Vector3(num3 * radius, num4 * radius, z) + offset;
				vector2 = (Vector2.one + new Vector2(num3, num4)) * 0.5f;
				vector2.y = 1f - vector2.y;
			}
			array[pointsCount + 1] = vector;
			array2[pointsCount + 1] = vector2;
			Mesh mesh = new Mesh();
			mesh.vertices = array;
			mesh.SetIndices(list.ToArray(), MeshTopology.Triangles, 0);
			mesh.uv = array2;
			return mesh;
		}

		public static Mesh CreateWireCircle(float radius = 1f, int pointsCount = 64)
		{
			return CreateWireArc(Vector3.zero, radius, pointsCount);
		}

		public static Mesh CreateWireArc(Vector3 offset, float radius = 1f, int pointsCount = 64, float fromAngle = 0f, float toAngle = MathF.PI * 2f)
		{
			Vector3[] array = new Vector3[pointsCount + 1];
			List<int> list = new List<int>();
			for (int i = 0; i < pointsCount; i++)
			{
				list.Add(i);
				list.Add(i + 1);
			}
			float num = fromAngle;
			float num2 = toAngle - fromAngle;
			float z = 0f;
			float x = Mathf.Cos(num) * radius;
			float y = Mathf.Sin(num) * radius;
			Vector3 vector = new Vector3(x, y, z) + offset;
			for (int j = 0; j < pointsCount; j++)
			{
				array[j] = vector;
				num += num2 / (float)pointsCount;
				x = Mathf.Cos(num) * radius;
				y = Mathf.Sin(num) * radius;
				vector = (array[j + 1] = new Vector3(x, y, z) + offset);
			}
			Mesh mesh = new Mesh();
			mesh.vertices = array;
			mesh.SetIndices(list.ToArray(), MeshTopology.Lines, 0);
			return mesh;
		}

		public static Mesh CreateWireCylinder(float radius = 1f, float length = 1f, int pointsCount = 8, float fromAngle = 0f, float toAngle = MathF.PI * 2f)
		{
			Vector3[] array = new Vector3[pointsCount * 2];
			List<int> list = new List<int>();
			for (int i = 0; i < array.Length; i += 2)
			{
				list.Add(i);
				list.Add(i + 1);
			}
			float num = fromAngle;
			float num2 = toAngle - fromAngle;
			float z = 0f;
			for (int j = 0; j < array.Length; j += 2)
			{
				float x = radius * Mathf.Cos(num);
				float y = radius * Mathf.Sin(num);
				Vector3 vector = new Vector3(x, y, z);
				Vector3 vector2 = new Vector3(x, y, z) + Vector3.forward * length;
				array[j] = vector;
				array[j + 1] = vector2;
				num += num2 / (float)pointsCount;
			}
			Mesh mesh = new Mesh();
			mesh.vertices = array;
			mesh.SetIndices(list.ToArray(), MeshTopology.Lines, 0);
			return mesh;
		}

		public static void DrawMesh(IRTECommandBuffer commandBuffer, Mesh mesh, Matrix4x4 transform, Material material, MaterialPropertyBlock propertyBlock)
		{
			commandBuffer.DrawMesh(mesh, transform, material, 0, 0, propertyBlock);
		}

		public static Mesh CreateCone(Color color, float scale)
		{
			int num = 12;
			float num2 = 0.2f;
			num2 *= scale;
			Vector3[] array = new Vector3[num * 3 + 1];
			int[] array2 = new int[num * 6];
			Color[] array3 = new Color[array.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = color;
			}
			float num3 = num2 / 2.6f;
			float num4 = num2;
			float num5 = MathF.PI * 2f / (float)num;
			float y = 0f - num4;
			array[^1] = new Vector3(0f, 0f - num4, 0f);
			for (int j = 0; j < num; j++)
			{
				float f = (float)j * num5;
				float x = Mathf.Cos(f) * num3;
				float z = Mathf.Sin(f) * num3;
				array[j] = new Vector3(x, y, z);
				array[num + j] = new Vector3(0f, 0.01f, 0f);
				array[2 * num + j] = array[j];
			}
			for (int k = 0; k < num; k++)
			{
				array2[k * 6] = k;
				array2[k * 6 + 1] = num + k;
				array2[k * 6 + 2] = (k + 1) % num;
				array2[k * 6 + 3] = array.Length - 1;
				array2[k * 6 + 4] = 2 * num + k;
				array2[k * 6 + 5] = 2 * num + (k + 1) % num;
			}
			return new Mesh
			{
				name = "Cone",
				vertices = array,
				triangles = array2,
				colors = array3
			};
		}

		public static void CreatePointBillboardMesh(IList<Vector3> positions, IList<int> indexes, IList<Color> colors, Mesh target)
		{
			int count = indexes.Count;
			int num = count * 4;
			s_vector2List.Clear();
			s_vector3List.Clear();
			s_colorList.Clear();
			s_indexList.Clear();
			s_vector2List.Capacity = num;
			s_vector3List.Capacity = num;
			s_colorList.Capacity = num;
			s_indexList.Capacity = num;
			for (int i = 0; i < count; i++)
			{
				int index = indexes[i];
				s_vector3List.Add(positions[index]);
				s_vector3List.Add(positions[index]);
				s_vector3List.Add(positions[index]);
				s_vector3List.Add(positions[index]);
				s_vector2List.Add(k_Billboard0);
				s_vector2List.Add(k_Billboard1);
				s_vector2List.Add(k_Billboard2);
				s_vector2List.Add(k_Billboard3);
				if (colors != null)
				{
					s_colorList.Add(colors[index]);
					s_colorList.Add(colors[index]);
					s_colorList.Add(colors[index]);
					s_colorList.Add(colors[index]);
				}
				s_indexList.Add(i * 4);
				s_indexList.Add(i * 4 + 1);
				s_indexList.Add(i * 4 + 3);
				s_indexList.Add(i * 4 + 2);
			}
			target.Clear();
			target.indexFormat = ((num > 65535) ? IndexFormat.UInt32 : IndexFormat.UInt16);
			target.SetVertices(s_vector3List);
			target.SetUVs(0, s_vector2List);
			if (colors != null)
			{
				target.SetColors(s_colorList);
			}
			target.subMeshCount = 1;
			target.SetIndices(s_indexList, MeshTopology.Quads, 0);
		}

		public static void UpdatePointBillboardMeshVertices(IList<Vector3> positions, Mesh target)
		{
			s_vector3List.Clear();
			s_vector3List.Capacity = positions.Count * 4;
			for (int i = 0; i < positions.Count; i++)
			{
				s_vector3List.Add(positions[i]);
				s_vector3List.Add(positions[i]);
				s_vector3List.Add(positions[i]);
				s_vector3List.Add(positions[i]);
			}
			target.SetVertices(s_vector3List);
		}

		public static void DrawMesh(CommandBuffer commandBuffer, Mesh mesh, Matrix4x4 transform, Material material, MaterialPropertyBlock propertyBlock)
		{
			s_commandBuffer.WrappedCommandBuffer = commandBuffer;
			DrawMesh(s_commandBuffer, mesh, transform, material, propertyBlock);
		}
	}
}
