using System.Collections.Generic;
using UnityEngine;

public class SurfaceInfo
{
	public class Quad
	{
		public int X;

		public int Y;

		public Vector3 Position;

		public Vector3 Normal;

		public Vector2 SurfaceUV;

		public ColliderArea Collider;

		public bool IsRect;

		public Vector3 TopLeft;

		public Vector3 TopRight;

		public Vector3 BottomLeft;

		public Vector3 BottomRight;

		public Vector3 TopDelta;

		public float TopWidth;

		public Vector3 BottomDelta;

		public float BottomWidth;

		public Vector3 LeftDelta;

		public float LeftHeight;

		public Vector3 RightDelta;

		public float RightHeight;
	}

	public class Edge
	{
		public Vector3 Position;

		public float Length;

		public float Angle;

		public bool IsPlanar;

		public bool Invert;
	}

	public class Vertex
	{
		public Vector3 Position;

		public Vector3 Normal;

		public Vector2 SurfaceUV;

		public Vector2 TextureUV;

		public int X;

		public int Y;
	}

	public class ColliderArea
	{
		public List<Quad> quads;

		public Vector3 TopLeft;

		public Vector3 TopRight;

		public Vector3 BottomRight;

		public Vector3 BottomLeft;

		public ColliderArea()
		{
			quads = new List<Quad>();
		}
	}

	private static float divisions = 1.5f;

	private static int minDivisions = 5;

	private static int maxDivisions = 11;

	public float MaxEdgeWidth;

	public float MaxEdgeHeight;

	public int Width;

	public int Height;

	public Matrix4x4 localMatrix;

	public bool IsQuad;

	public bool EdgesStraight;

	public bool EdgesPlanar;

	public bool CombineQuadsH;

	public bool CombineQuadsV;

	public Vector3[] Nodes;

	public Vector3 DragNormal;

	public float SurfaceArea;

	public Vertex[] Vertices;

	public Edge[] Edges;

	public Quad[] Quads;

	public List<ColliderArea> Colliders;

	public SurfaceInfo(BuildSurface surface, BuildNodeBlock[] nodes, BuildEdgeBlock[] edges)
	{
		if (surface == null)
		{
			Debug.LogError("Trying to create surface info from null surface");
			return;
		}
		if (nodes == null)
		{
			Debug.LogError("Trying to create surface info with missing nodes");
			return;
		}
		if (edges == null)
		{
			Debug.LogError("Trying to create surface info with missing edges");
			return;
		}
		int num = nodes.Length;
		IsQuad = num == 4;
		localMatrix = Matrix4x4.TRS(surface.Position, surface.Rotation, surface.Scale).inverse;
		Nodes = new Vector3[num];
		Edges = new Edge[num];
		EdgesPlanar = true;
		EdgesStraight = true;
		Plane p = default(Plane);
		Vector3 centre = Vector3.zero;
		Vector3 centre2 = Vector3.zero;
		if (num < 3)
		{
			EdgesPlanar = false;
		}
		else
		{
			if (CreatePlane(ref p, nodes))
			{
				if (IsQuad && Mathf.Abs(p.GetDistanceToPoint(nodes[3].Position)) > 0.001f)
				{
					EdgesPlanar = false;
				}
			}
			else
			{
				EdgesPlanar = false;
			}
			centre = nodes[0].Position + nodes[1].Position + nodes[2].Position;
			centre2 = edges[0].Position + edges[1].Position + edges[2].Position;
			if (IsQuad)
			{
				centre += nodes[3].Position;
				centre2 += edges[3].Position;
			}
			centre /= (float)num * 1f;
			centre2 /= (float)num * 1f;
		}
		for (int i = 0; i < num; i++)
		{
			Nodes[i] = localMatrix.MultiplyPoint3x4(nodes[i].Position);
			Edges[i] = new Edge
			{
				Position = localMatrix.MultiplyPoint3x4(edges[i].Position),
				Length = edges[i].Length,
				Angle = edges[i].Angle,
				IsPlanar = edges[i].isStraight,
				Invert = (edges[i].startNode == nodes[(i + 1) % num] && edges[i].endNode == nodes[i])
			};
			if (!edges[i].isStraight)
			{
				EdgesStraight = false;
				if (!EdgesPlanar)
				{
					continue;
				}
				if (Mathf.Abs(p.GetDistanceToPoint(edges[i].Position)) > 0.001f)
				{
					EdgesPlanar = false;
					continue;
				}
				int num2 = (i + 1) % num;
				if (EdgeConcave(nodes[i].Position, nodes[num2].Position, edges[i].Position, centre))
				{
					EdgesPlanar = false;
				}
			}
			else if (EdgesPlanar)
			{
				int num3 = (i + 1) % num;
				if (EdgeConcave(edges[i].Position, edges[num3].Position, nodes[num3].Position, centre2))
				{
					EdgesPlanar = false;
				}
			}
		}
		MaxEdgeWidth = Mathf.Max(Edges[0].Length, Edges[2].Length);
		Width = Mathf.Clamp(Mathf.CeilToInt(MaxEdgeWidth * divisions), minDivisions, maxDivisions);
		CombineQuadsH = (CombineQuadsV = false);
		if (IsQuad)
		{
			bool flag = Edges[0].IsPlanar && Edges[2].IsPlanar;
			bool flag2 = Edges[1].IsPlanar && Edges[3].IsPlanar;
			float num4 = 10f;
			Vector3 normalized = (Nodes[1] - Nodes[0]).normalized;
			Vector3 normalized2 = (Nodes[3] - Nodes[0]).normalized;
			float num5 = 57.29578f * Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized, normalized2), -1f, 1f));
			if (Mathf.Abs(num5 - 90f) < num4)
			{
				Vector3 normalized3 = (Nodes[1] - Nodes[2]).normalized;
				Vector3 normalized4 = (Nodes[3] - Nodes[2]).normalized;
				float num6 = 57.29578f * Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized3, normalized4), -1f, 1f));
				CombineQuadsH = (CombineQuadsV = Mathf.Abs(num6 - 90f) < num4);
			}
			MaxEdgeHeight = Mathf.Max(Edges[1].Length, Edges[3].Length);
			Height = Mathf.Clamp(Mathf.CeilToInt(MaxEdgeHeight * divisions), minDivisions, maxDivisions);
			if (CombineQuadsH && CombineQuadsV)
			{
				float num7 = 50f;
				if (EdgesStraight)
				{
					Width = (Height = 1);
				}
				else if (flag && Mathf.Abs(Edges[3].Angle - Edges[1].Angle) < num7 && SameDirection(edges, 3, 1))
				{
					Width = 1;
				}
				else if (flag2 && Mathf.Abs(Edges[2].Angle - Edges[0].Angle) < num7 && SameDirection(edges, 2, 0))
				{
					Height = 1;
				}
			}
		}
		else
		{
			MaxEdgeHeight = Edges[1].Length;
			Height = Mathf.Clamp(Mathf.CeilToInt(MaxEdgeHeight * divisions), minDivisions, maxDivisions);
			float num8 = 1.04f;
			if (EdgesStraight && Mathf.Min(Edges[0].Length, Edges[2].Length) < num8)
			{
				Width = (Height = 1);
			}
		}
		Vertices = new Vertex[(Width + 1) * (Height + 1)];
		Quads = new Quad[Width * Height];
		Colliders = new List<ColliderArea>();
		DragNormal = Vector3.zero;
		SurfaceArea = 0f;
	}

	private bool CreatePlane(ref Plane p, BuildNodeBlock[] nodes)
	{
		Vector3 vector = Vector3.one * float.PositiveInfinity;
		Vector3[] array = new Vector3[3] { vector, vector, vector };
		int num = 0;
		for (int i = 0; i < nodes.Length; i++)
		{
			Vector3 position = nodes[i].Position;
			bool flag = false;
			for (int num2 = i - 1; num2 >= 0; num2--)
			{
				if (position == array[num2])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				array[num] = position;
				num++;
				if (num > 2)
				{
					break;
				}
			}
		}
		p = new Plane(array[0], array[1], array[2]);
		return num > 2;
	}

	private bool EdgeConcave(Vector3 A, Vector3 B, Vector3 mid, Vector3 centre)
	{
		Vector3 vector = (A + B) * 0.5f;
		Vector3 lhs = vector - mid;
		return Vector3.Dot(lhs, mid - centre) > 0.1f;
	}

	private bool SameDirection(BuildEdgeBlock[] edges, int edge1, int edge2)
	{
		Vector3 vector = Vector3.Lerp(edges[edge1].startNode.Position, edges[edge1].endNode.Position, 0.5f);
		Vector3 vector2 = Vector3.Lerp(edges[edge2].startNode.Position, edges[edge2].endNode.Position, 0.5f);
		return Vector3.Dot(edges[edge1].Position - vector, edges[edge2].Position - vector2) > 0f;
	}

	public void UpdateTransformData(BuildNodeBlock[] nodes, BuildEdgeBlock[] edges)
	{
		for (int i = 0; i < nodes.Length; i++)
		{
			Nodes[i] = localMatrix.MultiplyPoint3x4(nodes[i].Position);
			Edges[i].Position = localMatrix.MultiplyPoint3x4(edges[i].Position);
		}
	}
}
