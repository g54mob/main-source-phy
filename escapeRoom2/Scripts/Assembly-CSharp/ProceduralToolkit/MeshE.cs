using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit
{
	public static class MeshE
	{
		public static void Move(this Mesh mesh, Vector3 vector)
		{
			Vector3[] vertices = mesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] += vector;
			}
			mesh.vertices = vertices;
		}

		public static void Rotate(this Mesh mesh, Quaternion rotation)
		{
			Vector3[] vertices = mesh.vertices;
			Vector3[] normals = mesh.normals;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = rotation * vertices[i];
				normals[i] = rotation * normals[i];
			}
			mesh.vertices = vertices;
			mesh.normals = normals;
		}

		public static void Scale(this Mesh mesh, float scale)
		{
			Vector3[] vertices = mesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] *= scale;
			}
			mesh.vertices = vertices;
		}

		public static void Scale(this Mesh mesh, Vector3 scale)
		{
			Vector3[] vertices = mesh.vertices;
			Vector3[] normals = mesh.normals;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vector = vertices[i];
				vertices[i] = new Vector3(vector.x * scale.x, vector.y * scale.y, vector.z * scale.z);
				Vector3 vector2 = normals[i];
				normals[i] = new Vector3(vector2.x * scale.x, vector2.y * scale.y, vector2.z * scale.z).normalized;
			}
			mesh.vertices = vertices;
			mesh.normals = normals;
		}

		public static void Paint(this Mesh mesh, Color color)
		{
			Color[] array = new Color[mesh.vertexCount];
			for (int i = 0; i < mesh.vertexCount; i++)
			{
				array[i] = color;
			}
			mesh.colors = array;
		}

		public static void FlipFaces(this Mesh mesh)
		{
			mesh.FlipTriangles();
			mesh.FlipNormals();
		}

		public static void FlipTriangles(this Mesh mesh)
		{
			for (int i = 0; i < mesh.subMeshCount; i++)
			{
				int[] triangles = mesh.GetTriangles(i);
				for (int j = 0; j < triangles.Length; j += 3)
				{
					PTUtils.Swap(ref triangles[j], ref triangles[j + 1]);
				}
				mesh.SetTriangles(triangles, i);
			}
		}

		public static void FlipNormals(this Mesh mesh)
		{
			Vector3[] normals = mesh.normals;
			for (int i = 0; i < normals.Length; i++)
			{
				normals[i] = -normals[i];
			}
			mesh.normals = normals;
		}

		public static Mesh Triangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
		{
			Vector3 normalized = Vector3.Cross(vertex1 - vertex0, vertex2 - vertex0).normalized;
			Mesh mesh = new Mesh();
			mesh.vertices = new Vector3[3] { vertex0, vertex1, vertex2 };
			mesh.normals = new Vector3[3] { normalized, normalized, normalized };
			mesh.uv = new Vector2[3]
			{
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 1f)
			};
			mesh.triangles = new int[3] { 0, 1, 2 };
			mesh.name = "Triangle";
			return mesh;
		}

		public static Mesh Quad(Vector3 origin, Vector3 width, Vector3 length)
		{
			Vector3 normalized = Vector3.Cross(length, width).normalized;
			Mesh mesh = new Mesh();
			mesh.vertices = new Vector3[4]
			{
				origin,
				origin + length,
				origin + length + width,
				origin + width
			};
			mesh.normals = new Vector3[4] { normalized, normalized, normalized, normalized };
			mesh.uv = new Vector2[4]
			{
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 1f),
				new Vector2(1f, 0f)
			};
			mesh.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
			mesh.name = "Quad";
			return mesh;
		}

		public static Mesh Quad(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
		{
			Vector3 normalized = Vector3.Cross(vertex1 - vertex0, vertex2 - vertex0).normalized;
			Mesh mesh = new Mesh();
			mesh.vertices = new Vector3[4] { vertex0, vertex1, vertex2, vertex3 };
			mesh.normals = new Vector3[4] { normalized, normalized, normalized, normalized };
			mesh.uv = new Vector2[4]
			{
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 1f),
				new Vector2(1f, 0f)
			};
			mesh.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
			mesh.name = "Quad";
			return mesh;
		}

		public static Mesh TriangleFan(List<Vector3> vertices)
		{
			return MeshDraft.TriangleFan(vertices).ToMesh();
		}

		public static Mesh TriangleStrip(List<Vector3> vertices)
		{
			return MeshDraft.TriangleStrip(vertices).ToMesh();
		}

		public static Mesh Tetrahedron(float radius)
		{
			return MeshDraft.Tetrahedron(radius).ToMesh();
		}

		public static Mesh Hexahedron(float width, float length, float height)
		{
			return MeshDraft.Hexahedron(width, length, height).ToMesh();
		}

		public static Mesh Hexahedron(Vector3 width, Vector3 length, Vector3 height)
		{
			return MeshDraft.Hexahedron(width, length, height).ToMesh();
		}

		public static Mesh Octahedron(float radius)
		{
			return MeshDraft.Octahedron(radius).ToMesh();
		}

		public static Mesh Dodecahedron(float radius)
		{
			return MeshDraft.Dodecahedron(radius).ToMesh();
		}

		public static Mesh Icosahedron(float radius)
		{
			return MeshDraft.Icosahedron(radius).ToMesh();
		}

		public static Mesh Plane(float xSize = 1f, float zSize = 1f, int xSegments = 1, int zSegments = 1)
		{
			return MeshDraft.Plane(xSize, zSize, xSegments, zSegments).ToMesh();
		}

		public static Mesh Pyramid(float radius, int segments, float heignt, bool inverted = false)
		{
			return MeshDraft.Pyramid(radius, segments, heignt, inverted).ToMesh();
		}

		public static Mesh Prism(float radius, int segments, float heignt)
		{
			return MeshDraft.Prism(radius, segments, heignt).ToMesh();
		}

		public static Mesh Cylinder(float radius, int segments, float heignt)
		{
			return MeshDraft.Cylinder(radius, segments, heignt).ToMesh();
		}

		public static Mesh FlatSphere(float radius, int longitudeSegments, int latitudeSegments)
		{
			return MeshDraft.FlatSphere(radius, longitudeSegments, longitudeSegments).ToMesh();
		}

		public static Mesh Sphere(float radius, int longitudeSegments, int latitudeSegments)
		{
			return MeshDraft.Sphere(radius, longitudeSegments, longitudeSegments).ToMesh();
		}
	}
}
