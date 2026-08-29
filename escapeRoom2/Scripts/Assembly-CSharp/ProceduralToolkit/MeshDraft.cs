using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit
{
	public class MeshDraft
	{
		public string name = "";

		public List<Vector3> vertices = new List<Vector3>();

		public List<int> triangles = new List<int>();

		public List<Vector3> normals = new List<Vector3>();

		public List<Vector2> uv = new List<Vector2>();

		public List<Color> colors = new List<Color>();

		public MeshDraft()
		{
		}

		public MeshDraft(Mesh mesh)
		{
			name = mesh.name;
			vertices.AddRange(mesh.vertices);
			triangles.AddRange(mesh.triangles);
			normals.AddRange(mesh.normals);
			uv.AddRange(mesh.uv);
			colors.AddRange(mesh.colors);
		}

		public void Add(MeshDraft draft)
		{
			foreach (int triangle in draft.triangles)
			{
				triangles.Add(triangle + vertices.Count);
			}
			vertices.AddRange(draft.vertices);
			normals.AddRange(draft.normals);
			uv.AddRange(draft.uv);
			colors.AddRange(draft.colors);
		}

		public void Move(Vector3 vector)
		{
			for (int i = 0; i < vertices.Count; i++)
			{
				vertices[i] += vector;
			}
		}

		public void Rotate(Quaternion rotation)
		{
			for (int i = 0; i < vertices.Count; i++)
			{
				vertices[i] = rotation * vertices[i];
				normals[i] = rotation * normals[i];
			}
		}

		public void Scale(float scale)
		{
			for (int i = 0; i < vertices.Count; i++)
			{
				vertices[i] *= scale;
			}
		}

		public void Scale(Vector3 scale)
		{
			for (int i = 0; i < vertices.Count; i++)
			{
				Vector3 vector = vertices[i];
				vertices[i] = new Vector3(vector.x * scale.x, vector.y * scale.y, vector.z * scale.z);
				Vector3 vector2 = normals[i];
				normals[i] = new Vector3(vector2.x * scale.x, vector2.y * scale.y, vector2.z * scale.z).normalized;
			}
		}

		public void Paint(Color color)
		{
			colors.Clear();
			for (int i = 0; i < vertices.Count; i++)
			{
				colors.Add(color);
			}
		}

		public void FlipFaces()
		{
			FlipTriangles();
			FlipNormals();
		}

		public void FlipTriangles()
		{
			for (int i = 0; i < triangles.Count; i += 3)
			{
				int value = triangles[i];
				triangles[i] = triangles[i + 1];
				triangles[i + 1] = value;
			}
		}

		public void FlipNormals()
		{
			for (int i = 0; i < normals.Count; i++)
			{
				normals[i] = -normals[i];
			}
		}

		public Mesh ToMesh()
		{
			return new Mesh
			{
				name = name,
				vertices = vertices.ToArray(),
				triangles = triangles.ToArray(),
				normals = normals.ToArray(),
				uv = uv.ToArray(),
				colors = colors.ToArray()
			};
		}

		public static MeshDraft Triangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
		{
			Vector3 normalized = Vector3.Cross(vertex1 - vertex0, vertex2 - vertex0).normalized;
			return new MeshDraft
			{
				vertices = new List<Vector3>(3) { vertex0, vertex1, vertex2 },
				normals = new List<Vector3>(3) { normalized, normalized, normalized },
				uv = new List<Vector2>(3)
				{
					new Vector2(0f, 0f),
					new Vector2(0f, 1f),
					new Vector2(1f, 1f)
				},
				triangles = new List<int>(3) { 0, 1, 2 },
				name = "Triangle"
			};
		}

		public static MeshDraft Quad(Vector3 origin, Vector3 width, Vector3 length)
		{
			Vector3 normalized = Vector3.Cross(length, width).normalized;
			return new MeshDraft
			{
				vertices = new List<Vector3>(4)
				{
					origin,
					origin + length,
					origin + length + width,
					origin + width
				},
				normals = new List<Vector3>(4) { normalized, normalized, normalized, normalized },
				uv = new List<Vector2>(4)
				{
					new Vector2(0f, 0f),
					new Vector2(0f, 1f),
					new Vector2(1f, 1f),
					new Vector2(1f, 0f)
				},
				triangles = new List<int>(6) { 0, 1, 2, 0, 2, 3 },
				name = "Quad"
			};
		}

		public static MeshDraft Quad(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
		{
			Vector3 normalized = Vector3.Cross(vertex1 - vertex0, vertex2 - vertex0).normalized;
			return new MeshDraft
			{
				vertices = new List<Vector3>(4) { vertex0, vertex1, vertex2, vertex3 },
				normals = new List<Vector3>(4) { normalized, normalized, normalized, normalized },
				uv = new List<Vector2>(4)
				{
					new Vector2(0f, 0f),
					new Vector2(0f, 1f),
					new Vector2(1f, 1f),
					new Vector2(1f, 0f)
				},
				triangles = new List<int>(6) { 0, 1, 2, 0, 2, 3 },
				name = "Quad"
			};
		}

		public static MeshDraft TriangleFan(List<Vector3> vertices)
		{
			MeshDraft meshDraft = new MeshDraft
			{
				vertices = vertices,
				triangles = new List<int>(vertices.Count - 2),
				normals = new List<Vector3>(vertices.Count),
				uv = new List<Vector2>(vertices.Count),
				name = "TriangleFan"
			};
			for (int i = 1; i < vertices.Count - 1; i++)
			{
				meshDraft.triangles.Add(0);
				meshDraft.triangles.Add(i);
				meshDraft.triangles.Add(i + 1);
			}
			Vector3 normalized = Vector3.Cross(vertices[1] - vertices[0], vertices[2] - vertices[0]).normalized;
			for (int j = 0; j < vertices.Count; j++)
			{
				meshDraft.normals.Add(normalized);
				meshDraft.uv.Add(new Vector2((float)j / (float)vertices.Count, (float)j / (float)vertices.Count));
			}
			return meshDraft;
		}

		public static MeshDraft TriangleStrip(List<Vector3> vertices)
		{
			MeshDraft meshDraft = new MeshDraft
			{
				vertices = vertices,
				triangles = new List<int>(vertices.Count - 2),
				normals = new List<Vector3>(vertices.Count),
				uv = new List<Vector2>(vertices.Count),
				name = "TriangleStrip"
			};
			int num = 0;
			int num2 = 1;
			int num3 = 2;
			while (num < vertices.Count - 2)
			{
				meshDraft.triangles.Add(num);
				meshDraft.triangles.Add(num2);
				meshDraft.triangles.Add(num3);
				num++;
				num2 += num % 2 * 2;
				num3 += (num + 1) % 2 * 2;
			}
			Vector3 normalized = Vector3.Cross(vertices[1] - vertices[0], vertices[2] - vertices[0]).normalized;
			for (int i = 0; i < vertices.Count; i++)
			{
				meshDraft.normals.Add(normalized);
				meshDraft.uv.Add(new Vector2((float)i / (float)vertices.Count, (float)i / (float)vertices.Count));
			}
			return meshDraft;
		}

		public static MeshDraft BaselessPyramid(float radius, int segments, float heignt, bool inverted = false)
		{
			return BaselessPyramid(Vector3.zero, Vector3.up * heignt * ((!inverted) ? 1 : (-1)), radius, segments, inverted);
		}

		public static MeshDraft BaselessPyramid(Vector3 baseCenter, Vector3 apex, float radius, int segments, bool inverted = false)
		{
			float num = MathF.PI * 2f / (float)segments * (float)((!inverted) ? 1 : (-1));
			float num2 = 0f;
			Vector3[] array = new Vector3[segments + 1];
			array[0] = apex;
			for (int i = 1; i <= segments; i++)
			{
				array[i] = PTUtils.PointOnCircle3(radius, num2) + baseCenter;
				num2 += num;
			}
			MeshDraft meshDraft = new MeshDraft
			{
				name = "BaselessPyramid"
			};
			for (int j = 1; j < segments; j++)
			{
				meshDraft.Add(Triangle(array[0], array[j], array[j + 1]));
			}
			meshDraft.Add(Triangle(array[0], array[^1], array[1]));
			return meshDraft;
		}

		public static MeshDraft BaselessPyramid(Vector3 apex, List<Vector3> ring)
		{
			MeshDraft meshDraft = new MeshDraft
			{
				name = "BaselessPyramid"
			};
			for (int i = 0; i < ring.Count - 1; i++)
			{
				meshDraft.Add(Triangle(apex, ring[i], ring[i + 1]));
			}
			meshDraft.Add(Triangle(apex, ring[ring.Count - 1], ring[0]));
			return meshDraft;
		}

		public static MeshDraft Band(List<Vector3> lowerRing, List<Vector3> upperRing)
		{
			MeshDraft meshDraft = new MeshDraft
			{
				name = "Band"
			};
			if (lowerRing.Count < 3 || upperRing.Count < 3)
			{
				Debug.LogError("Array sizes must be greater than 2");
				return meshDraft;
			}
			if (lowerRing.Count != upperRing.Count)
			{
				Debug.LogError("Array sizes must be equal");
				return meshDraft;
			}
			meshDraft.vertices.AddRange(lowerRing);
			meshDraft.vertices.AddRange(upperRing);
			List<Vector3> list = new List<Vector3>();
			List<Vector3> list2 = new List<Vector3>();
			List<Vector2> list3 = new List<Vector2>();
			List<Vector2> list4 = new List<Vector2>();
			int num;
			int num2;
			int num3;
			int num4;
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			Vector3 vector4;
			for (int i = 0; i < lowerRing.Count - 1; i++)
			{
				num = i;
				num2 = i + lowerRing.Count;
				num3 = i + 1;
				num4 = i + 1 + lowerRing.Count;
				vector = meshDraft.vertices[num];
				vector2 = meshDraft.vertices[num2];
				vector3 = meshDraft.vertices[num3];
				vector4 = meshDraft.vertices[num4];
				meshDraft.triangles.AddRange(new int[3] { num, num2, num3 });
				meshDraft.triangles.AddRange(new int[3] { num3, num2, num4 });
				list.Add(Vector3.Cross(vector2 - vector, vector3 - vector).normalized);
				list2.Add(Vector3.Cross(vector4 - vector2, vector - vector2).normalized);
				float x = (float)i / (float)(lowerRing.Count - 1);
				list3.Add(new Vector2(x, 0f));
				list4.Add(new Vector2(x, 1f));
			}
			num = lowerRing.Count - 1;
			num2 = lowerRing.Count * 2 - 1;
			num3 = 0;
			num4 = lowerRing.Count;
			vector = meshDraft.vertices[num];
			vector2 = meshDraft.vertices[num2];
			vector3 = meshDraft.vertices[num3];
			vector4 = meshDraft.vertices[num4];
			meshDraft.triangles.AddRange(new int[3] { num, num2, num3 });
			meshDraft.triangles.AddRange(new int[3] { num3, num2, num4 });
			list.Add(Vector3.Cross(vector2 - vector, vector3 - vector).normalized);
			list2.Add(Vector3.Cross(vector4 - vector2, vector - vector2).normalized);
			meshDraft.normals.AddRange(list);
			meshDraft.normals.AddRange(list2);
			list3.Add(new Vector2(1f, 0f));
			list4.Add(new Vector2(1f, 1f));
			meshDraft.uv.AddRange(list3);
			meshDraft.uv.AddRange(list4);
			return meshDraft;
		}

		public static MeshDraft FlatBand(List<Vector3> lowerRing, List<Vector3> upperRing)
		{
			MeshDraft meshDraft = new MeshDraft
			{
				name = "Flat band"
			};
			if (lowerRing.Count < 3 || upperRing.Count < 3)
			{
				Debug.LogError("Array sizes must be greater than 2");
				return meshDraft;
			}
			if (lowerRing.Count != upperRing.Count)
			{
				Debug.LogError("Array sizes must be equal");
				return meshDraft;
			}
			Vector3 vertex;
			Vector3 vertex2;
			Vector3 vector;
			Vector3 vertex3;
			for (int i = 0; i < lowerRing.Count - 1; i++)
			{
				vertex = lowerRing[i];
				vertex2 = upperRing[i];
				vector = lowerRing[i + 1];
				vertex3 = upperRing[i + 1];
				meshDraft.Add(Triangle(vertex, vertex2, vector));
				meshDraft.Add(Triangle(vector, vertex2, vertex3));
			}
			vertex = lowerRing[lowerRing.Count - 1];
			vertex2 = upperRing[upperRing.Count - 1];
			vector = lowerRing[0];
			vertex3 = upperRing[0];
			meshDraft.Add(Triangle(vertex, vertex2, vector));
			meshDraft.Add(Triangle(vector, vertex2, vertex3));
			return meshDraft;
		}

		public static MeshDraft Tetrahedron(float radius)
		{
			float latitude = -0.3398369f;
			float num = MathF.PI * 2f / 3f;
			float num2 = 0f;
			List<Vector3> list = new List<Vector3>(4)
			{
				new Vector3(0f, radius, 0f)
			};
			for (int i = 1; i < 4; i++)
			{
				list.Add(PTUtils.PointOnSphere(radius, num2, latitude));
				num2 += num;
			}
			MeshDraft meshDraft = Triangle(list[0], list[1], list[2]);
			meshDraft.Add(Triangle(list[1], list[3], list[2]));
			meshDraft.Add(Triangle(list[0], list[2], list[3]));
			meshDraft.Add(Triangle(list[0], list[3], list[1]));
			meshDraft.name = "Tetrahedron";
			return meshDraft;
		}

		public static MeshDraft Hexahedron(float width, float length, float height)
		{
			return Hexahedron(Vector3.right * width, Vector3.forward * length, Vector3.up * height);
		}

		public static MeshDraft Hexahedron(Vector3 width, Vector3 length, Vector3 height)
		{
			Vector3 origin = -width / 2f - length / 2f - height / 2f;
			Vector3 origin2 = width / 2f + length / 2f + height / 2f;
			MeshDraft meshDraft = Quad(origin, length, width);
			meshDraft.Add(Quad(origin, width, height));
			meshDraft.Add(Quad(origin, height, length));
			meshDraft.Add(Quad(origin2, -width, -length));
			meshDraft.Add(Quad(origin2, -height, -width));
			meshDraft.Add(Quad(origin2, -length, -height));
			meshDraft.name = "Hexahedron";
			return meshDraft;
		}

		public static MeshDraft Octahedron(float radius)
		{
			MeshDraft meshDraft = BiPyramid(radius, 4, radius);
			meshDraft.name = "Octahedron";
			return meshDraft;
		}

		public static MeshDraft Dodecahedron(float radius)
		{
			float num = 0.9184383f;
			float num2 = 0.18871056f;
			float num3 = MathF.PI * 2f / 5f;
			float num4 = 0f;
			List<Vector3> list = new List<Vector3>();
			List<Vector3> list2 = new List<Vector3>();
			for (int i = 0; i <= 5; i++)
			{
				list.Add(PTUtils.PointOnSphere(radius, num4, 0f - num));
				list2.Add(PTUtils.PointOnSphere(radius, num4, 0f - num2));
				num4 -= num3;
			}
			num4 = (0f - num3) / 2f;
			List<Vector3> list3 = new List<Vector3>();
			List<Vector3> list4 = new List<Vector3>();
			for (int j = 0; j <= 5; j++)
			{
				list3.Add(PTUtils.PointOnSphere(radius, num4, num));
				list4.Add(PTUtils.PointOnSphere(radius, num4, num2));
				num4 -= num3;
			}
			MeshDraft meshDraft = TriangleFan(list);
			meshDraft.Add(FlatBand(list, list2));
			meshDraft.Add(FlatBand(list2, list4));
			meshDraft.Add(FlatBand(list4, list3));
			list3.Reverse();
			meshDraft.Add(TriangleFan(list3));
			meshDraft.name = "Dodecahedron";
			return meshDraft;
		}

		public static MeshDraft Icosahedron(float radius)
		{
			float num = 0.46364757f;
			float num2 = MathF.PI * 2f / 5f;
			float num3 = 0f;
			List<Vector3> list = new List<Vector3>(5);
			for (int i = 0; i < 5; i++)
			{
				list.Add(PTUtils.PointOnSphere(radius, num3, num));
				num3 -= num2;
			}
			num3 = num2 / 2f;
			List<Vector3> list2 = new List<Vector3>(5);
			for (int j = 0; j < 5; j++)
			{
				list2.Add(PTUtils.PointOnSphere(radius, num3, 0f - num));
				num3 -= num2;
			}
			MeshDraft meshDraft = BaselessPyramid(new Vector3(0f, 0f - radius, 0f), list2);
			meshDraft.Add(FlatBand(list2, list));
			list.Reverse();
			meshDraft.Add(BaselessPyramid(new Vector3(0f, radius, 0f), list));
			meshDraft.name = "Icosahedron";
			return meshDraft;
		}

		public static MeshDraft Plane(float xSize = 1f, float zSize = 1f, int xSegments = 1, int zSegments = 1)
		{
			float num = xSize / (float)xSegments;
			float num2 = zSize / (float)zSegments;
			int capacity = (xSegments + 1) * (zSegments + 1);
			MeshDraft meshDraft = new MeshDraft
			{
				name = "Plane",
				vertices = new List<Vector3>(capacity),
				triangles = new List<int>(xSegments * zSegments * 6),
				normals = new List<Vector3>(capacity),
				uv = new List<Vector2>(capacity)
			};
			for (int i = 0; i <= zSegments; i++)
			{
				for (int j = 0; j <= xSegments; j++)
				{
					meshDraft.vertices.Add(new Vector3((float)j * num, 0f, (float)i * num2));
					meshDraft.normals.Add(Vector3.up);
					meshDraft.uv.Add(new Vector2((float)j / (float)xSegments, (float)i / (float)zSegments));
				}
			}
			int num3 = 0;
			for (int k = 0; k < zSegments; k++)
			{
				for (int l = 0; l < xSegments; l++)
				{
					meshDraft.triangles.Add(num3);
					meshDraft.triangles.Add(num3 + xSegments + 1);
					meshDraft.triangles.Add(num3 + 1);
					meshDraft.triangles.Add(num3 + 1);
					meshDraft.triangles.Add(num3 + xSegments + 1);
					meshDraft.triangles.Add(num3 + xSegments + 2);
					num3++;
				}
				num3++;
			}
			return meshDraft;
		}

		public static MeshDraft Pyramid(float radius, int segments, float heignt, bool inverted = false)
		{
			MeshDraft meshDraft = BaselessPyramid(radius, segments, heignt, inverted);
			List<Vector3> list = new List<Vector3>(segments);
			for (int num = meshDraft.vertices.Count - 2; num >= 0; num -= 3)
			{
				list.Add(meshDraft.vertices[num]);
			}
			meshDraft.Add(TriangleFan(list));
			meshDraft.name = "Pyramid";
			return meshDraft;
		}

		public static MeshDraft BiPyramid(float radius, int segments, float heignt)
		{
			MeshDraft meshDraft = BaselessPyramid(radius, segments, heignt);
			meshDraft.Add(BaselessPyramid(radius, segments, heignt, inverted: true));
			return meshDraft;
		}

		public static MeshDraft Prism(float radius, int segments, float heignt)
		{
			float num = MathF.PI * 2f / (float)segments;
			float num2 = 0f;
			List<Vector3> list = new List<Vector3>(segments);
			List<Vector3> list2 = new List<Vector3>(segments);
			for (int i = 0; i < segments; i++)
			{
				Vector3 vector = PTUtils.PointOnCircle3(radius, num2);
				list.Add(vector - Vector3.up * heignt / 2f);
				list2.Add(vector + Vector3.up * heignt / 2f);
				num2 -= num;
			}
			MeshDraft meshDraft = TriangleFan(list);
			meshDraft.Add(FlatBand(list, list2));
			list2.Reverse();
			meshDraft.Add(TriangleFan(list2));
			meshDraft.name = "Prism";
			return meshDraft;
		}

		public static MeshDraft Cylinder(float radius, int segments, float heignt)
		{
			float num = MathF.PI * 2f / (float)segments;
			float num2 = 0f;
			List<Vector3> list = new List<Vector3>(segments);
			List<Vector3> list2 = new List<Vector3>(segments);
			for (int i = 0; i < segments; i++)
			{
				Vector3 vector = PTUtils.PointOnCircle3(radius, num2);
				list.Add(vector - Vector3.up * heignt / 2f);
				list2.Add(vector + Vector3.up * heignt / 2f);
				num2 -= num;
			}
			MeshDraft meshDraft = TriangleFan(list);
			meshDraft.Add(Band(list, list2));
			list2.Reverse();
			meshDraft.Add(TriangleFan(list2));
			meshDraft.name = "Cylinder";
			return meshDraft;
		}

		public static MeshDraft FlatSphere(float radius, int longitudeSegments, int latitudeSegments)
		{
			float num = MathF.PI * 2f / (float)longitudeSegments;
			float num2 = MathF.PI / (float)latitudeSegments;
			float num3 = -MathF.PI / 2f;
			List<List<Vector3>> list = new List<List<Vector3>>(latitudeSegments);
			for (int i = 0; i <= latitudeSegments; i++)
			{
				float num4 = 0f;
				List<Vector3> list2 = new List<Vector3>(longitudeSegments);
				for (int j = 0; j < longitudeSegments; j++)
				{
					list2.Add(PTUtils.PointOnSphere(radius, num4, num3));
					num4 -= num;
				}
				list.Add(list2);
				num3 += num2;
			}
			MeshDraft meshDraft = new MeshDraft
			{
				name = "Flat sphere"
			};
			for (int k = 0; k < list.Count - 1; k++)
			{
				meshDraft.Add(FlatBand(list[k], list[k + 1]));
			}
			return meshDraft;
		}

		public static MeshDraft Sphere(float radius, int longitudeSegments, int latitudeSegments)
		{
			MeshDraft meshDraft = new MeshDraft
			{
				name = "Sphere"
			};
			float num = MathF.PI * 2f / (float)longitudeSegments;
			float num2 = MathF.PI / (float)latitudeSegments;
			float num3 = -MathF.PI / 2f;
			for (int i = 0; i <= latitudeSegments; i++)
			{
				float num4 = 0f;
				for (int j = 0; j < longitudeSegments; j++)
				{
					Vector3 item = PTUtils.PointOnSphere(radius, num4, num3);
					meshDraft.vertices.Add(item);
					meshDraft.normals.Add(item.normalized);
					meshDraft.uv.Add(new Vector2((float)j / (float)longitudeSegments, (float)i / (float)latitudeSegments));
					num4 -= num;
				}
				num3 += num2;
			}
			for (int k = 0; k < latitudeSegments; k++)
			{
				int num5;
				int num6;
				int num7;
				int num8;
				for (int l = 0; l < longitudeSegments - 1; l++)
				{
					num5 = k * longitudeSegments + l;
					num6 = (k + 1) * longitudeSegments + l;
					num7 = k * longitudeSegments + l + 1;
					num8 = (k + 1) * longitudeSegments + l + 1;
					meshDraft.triangles.AddRange(new int[3] { num5, num6, num7 });
					meshDraft.triangles.AddRange(new int[3] { num7, num6, num8 });
				}
				num5 = (k + 1) * longitudeSegments - 1;
				num6 = (k + 2) * longitudeSegments - 1;
				num7 = k * longitudeSegments;
				num8 = (k + 1) * longitudeSegments;
				meshDraft.triangles.AddRange(new int[3] { num5, num6, num7 });
				meshDraft.triangles.AddRange(new int[3] { num7, num6, num8 });
			}
			return meshDraft;
		}
	}
}
