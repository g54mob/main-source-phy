using UnityEngine;

namespace MeshBrush
{
	public static class CombineUtility
	{
		public struct MeshInstance
		{
			public Mesh mesh;

			public int subMeshIndex;

			public Matrix4x4 transform;
		}

		private static int vertexCount;

		private static int triangleCount;

		private static int stripCount;

		private static int curStripCount;

		private static Vector3[] vertices;

		private static Vector3[] normals;

		private static Vector4[] tangents;

		private static Vector2[] uv;

		private static Vector2[] uv1;

		private static Color[] colors;

		private static int[] triangles;

		private static int[] strip;

		private static int offset;

		private static int triangleOffset;

		private static int stripOffset;

		private static int vertexOffset;

		private static Matrix4x4 invTranspose;

		public const string combinedMeshName = "Combined Mesh";

		private static Vector4 p4;

		private static Vector3 p;

		public static Mesh Combine(MeshInstance[] combines, bool generateStrips)
		{
			vertexCount = 0;
			triangleCount = 0;
			stripCount = 0;
			MeshInstance[] array = combines;
			for (int i = 0; i < array.Length; i++)
			{
				MeshInstance meshInstance = array[i];
				if (!(meshInstance.mesh != null))
				{
					continue;
				}
				vertexCount += meshInstance.mesh.vertexCount;
				if (!generateStrips)
				{
					continue;
				}
				curStripCount = meshInstance.mesh.GetTriangles(meshInstance.subMeshIndex).Length;
				if (curStripCount != 0)
				{
					if (stripCount != 0)
					{
						if ((stripCount & 1) == 1)
						{
							stripCount += 3;
						}
						else
						{
							stripCount += 2;
						}
					}
					stripCount += curStripCount;
				}
				else
				{
					generateStrips = false;
				}
			}
			if (!generateStrips)
			{
				array = combines;
				for (int i = 0; i < array.Length; i++)
				{
					MeshInstance meshInstance2 = array[i];
					if (meshInstance2.mesh != null)
					{
						triangleCount += meshInstance2.mesh.GetTriangles(meshInstance2.subMeshIndex).Length;
					}
				}
			}
			vertices = new Vector3[vertexCount];
			normals = new Vector3[vertexCount];
			tangents = new Vector4[vertexCount];
			uv = new Vector2[vertexCount];
			uv1 = new Vector2[vertexCount];
			colors = new Color[vertexCount];
			triangles = new int[triangleCount];
			strip = new int[stripCount];
			offset = 0;
			array = combines;
			for (int i = 0; i < array.Length; i++)
			{
				MeshInstance meshInstance3 = array[i];
				if (meshInstance3.mesh != null)
				{
					Copy(meshInstance3.mesh.vertexCount, meshInstance3.mesh.vertices, vertices, ref offset, meshInstance3.transform);
				}
			}
			offset = 0;
			array = combines;
			for (int i = 0; i < array.Length; i++)
			{
				MeshInstance meshInstance4 = array[i];
				if (meshInstance4.mesh != null)
				{
					invTranspose = meshInstance4.transform;
					invTranspose = invTranspose.inverse.transpose;
					CopyNormal(meshInstance4.mesh.vertexCount, meshInstance4.mesh.normals, normals, ref offset, invTranspose);
				}
			}
			offset = 0;
			array = combines;
			for (int i = 0; i < array.Length; i++)
			{
				MeshInstance meshInstance5 = array[i];
				if (meshInstance5.mesh != null)
				{
					invTranspose = meshInstance5.transform;
					invTranspose = invTranspose.inverse.transpose;
					CopyTangents(meshInstance5.mesh.vertexCount, meshInstance5.mesh.tangents, tangents, ref offset, invTranspose);
				}
			}
			offset = 0;
			array = combines;
			for (int i = 0; i < array.Length; i++)
			{
				MeshInstance meshInstance6 = array[i];
				if (meshInstance6.mesh != null)
				{
					Copy(meshInstance6.mesh.vertexCount, meshInstance6.mesh.uv, uv, ref offset);
				}
			}
			offset = 0;
			array = combines;
			for (int i = 0; i < array.Length; i++)
			{
				MeshInstance meshInstance7 = array[i];
				if (meshInstance7.mesh != null)
				{
					Copy(meshInstance7.mesh.vertexCount, meshInstance7.mesh.uv2, uv1, ref offset);
				}
			}
			offset = 0;
			array = combines;
			for (int i = 0; i < array.Length; i++)
			{
				MeshInstance meshInstance8 = array[i];
				if (meshInstance8.mesh != null)
				{
					CopyColors(meshInstance8.mesh.vertexCount, meshInstance8.mesh.colors, colors, ref offset);
				}
			}
			triangleOffset = 0;
			stripOffset = 0;
			vertexOffset = 0;
			array = combines;
			for (int i = 0; i < array.Length; i++)
			{
				MeshInstance meshInstance9 = array[i];
				if (!(meshInstance9.mesh != null))
				{
					continue;
				}
				if (generateStrips)
				{
					int[] array2 = meshInstance9.mesh.GetTriangles(meshInstance9.subMeshIndex);
					if (stripOffset != 0)
					{
						if ((stripOffset & 1) == 1)
						{
							strip[stripOffset] = strip[stripOffset - 1];
							strip[stripOffset + 1] = array2[0] + vertexOffset;
							strip[stripOffset + 2] = array2[0] + vertexOffset;
							stripOffset += 3;
						}
						else
						{
							strip[stripOffset] = strip[stripOffset - 1];
							strip[stripOffset + 1] = array2[0] + vertexOffset;
							stripOffset += 2;
						}
					}
					for (int j = 0; j < array2.Length; j++)
					{
						strip[j + stripOffset] = array2[j] + vertexOffset;
					}
					stripOffset += array2.Length;
				}
				else
				{
					int[] array3 = meshInstance9.mesh.GetTriangles(meshInstance9.subMeshIndex);
					for (int k = 0; k < array3.Length; k++)
					{
						triangles[k + triangleOffset] = array3[k] + vertexOffset;
					}
					triangleOffset += array3.Length;
				}
				vertexOffset += meshInstance9.mesh.vertexCount;
			}
			Mesh mesh = new Mesh();
			mesh.name = "Combined Mesh";
			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.colors = colors;
			mesh.uv = uv;
			mesh.uv2 = uv1;
			mesh.tangents = tangents;
			if (generateStrips)
			{
				mesh.SetTriangles(strip, 0);
			}
			else
			{
				mesh.triangles = triangles;
			}
			return mesh;
		}

		private static void Copy(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = transform.MultiplyPoint(src[i]);
			}
			offset += vertexcount;
		}

		private static void CopyNormal(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = transform.MultiplyVector(src[i]).normalized;
			}
			offset += vertexcount;
		}

		private static void Copy(int vertexcount, Vector2[] src, Vector2[] dst, ref int offset)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = src[i];
			}
			offset += vertexcount;
		}

		private static void CopyColors(int vertexcount, Color[] src, Color[] dst, ref int offset)
		{
			for (int i = 0; i < src.Length; i++)
			{
				dst[i + offset] = src[i];
			}
			offset += vertexcount;
		}

		private static void CopyTangents(int vertexcount, Vector4[] src, Vector4[] dst, ref int offset, Matrix4x4 transform)
		{
			for (int i = 0; i < src.Length; i++)
			{
				p4 = src[i];
				p = new Vector3(p4.x, p4.y, p4.z);
				p = transform.MultiplyVector(p).normalized;
				dst[i + offset] = new Vector4(p.x, p.y, p.z, p4.w);
			}
			offset += vertexcount;
		}
	}
}
