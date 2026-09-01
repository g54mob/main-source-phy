#define DEBUG
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Pb;
using Poly.Base;
using Poly.Draw;
using Poly.Extension;
using Poly.Math;
using UnityEngine;

namespace Poly.Collide
{
	public class PolygonShape : Shape
	{
		public const int MaxNumVertices = 32;

		public Vec2[] verts;

		public float[] invLengths;

		private static FastList<Vec2> draw_edge_normals = new FastList<Vec2>(16);

		private static FastList<Vec2> draw_avg_vert_normals = new FastList<Vec2>(16);

		public PolygonShape()
		{
			type = Type.Polygon;
		}

		public override Aabb GetAabb(ref Transform2 t2, float padding)
		{
			_GetRangeAlong2(t2.rotation.InvMul(in Vec2.right), t2.rotation.InvMul(in Vec2.up), out var r, out var r2);
			Aabb result = default(Aabb);
			result.min = new Vec2(r.min, r2.min);
			result.max = new Vec2(r.max, r2.max);
			result.min.add(in t2.position);
			result.max.add(in t2.position);
			result._Expand(radius + padding);
			return result;
		}

		public void Dispose_unused()
		{
			verts = null;
			invLengths = null;
		}

		public void SetFromPointCloud(Vec2[] pointsInLocal)
		{
			Pb.Debug.Assert(pointsInLocal.Length <= 32, "Shape has more than 128 vertices & does not fit in Collision Process array.");
			verts = pointsInLocal;
			CacheLengths();
		}

		public void CacheLengths()
		{
			int num = verts.Length;
			invLengths = new float[num];
			Vector2 vector = verts[num - 1];
			for (int i = 0; i < num; i++)
			{
				Vector2 vector2 = verts[i];
				float magnitude = (vector2 - vector).magnitude;
				invLengths[i] = ((magnitude > 1E-12f) ? (1f / magnitude) : 0f);
				vector = vector2;
			}
		}

		private Poly.Math.Range GetRangeAlong(Vec2 dir)
		{
			float num = float.PositiveInfinity;
			float num2 = float.NegativeInfinity;
			for (int i = 0; i < verts.Length; i++)
			{
				float num3 = Vec2.Dot(in dir, in verts[i]);
				num = ((num < num3) ? num : num3);
				num2 = ((num3 < num2) ? num2 : num3);
			}
			return new Poly.Math.Range(num, num2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void _GetRangeAlong2(in Vec2 dir0, in Vec2 dir1, out Poly.Math.Range r0, out Poly.Math.Range r1)
		{
			r0.min = float.PositiveInfinity;
			r0.max = float.NegativeInfinity;
			r1.min = float.PositiveInfinity;
			r1.max = float.NegativeInfinity;
			for (int i = 0; i < verts.Length; i++)
			{
				ref Vec2 reference = ref verts[i];
				float num = dir0.x * reference.x + dir0.y * reference.y;
				float num2 = dir1.x * reference.x + dir1.y * reference.y;
				r0.min = ((r0.min < num) ? r0.min : num);
				r0.max = ((num < r0.max) ? r0.max : num);
				r1.min = ((r1.min < num2) ? r1.min : num2);
				r1.max = ((num2 < r1.max) ? r1.max : num2);
			}
		}

		public void DrawGizmos(Transform2 t2)
		{
			DrawGizmos(t2, Color.white);
		}

		public void DrawGizmos(Transform2 t2, Color tint, bool drawSomeDiagonals = true)
		{
			float angle_slow = t2.angle_slow;
			draw_edge_normals.SetSize(verts.Length);
			draw_avg_vert_normals.SetSize(verts.Length);
			for (int i = 0; i < verts.Length; i++)
			{
				draw_edge_normals[i] = (verts[i] - verts[(i - 1 + verts.Length) % verts.Length]).rotated90 * invLengths[i];
			}
			for (int j = 0; j < verts.Length; j++)
			{
				Vec2 a = draw_edge_normals[j];
				Vec2 b = draw_edge_normals[(j + 1) % verts.Length];
				draw_avg_vert_normals[j] = (a + b).normalized * 0.01f;
				float num = Vec2.Dot(in a, in b);
				float a2 = UnityEngine.Mathf.Sqrt(0.5f + 0.5f * num);
				float num2 = 1f / UnityEngine.Mathf.Max(a2, 0.1f);
				draw_avg_vert_normals[j] *= num2;
			}
			GlDrawer.color = Color.white.Tint(tint);
			for (int k = 0; k < verts.Length; k++)
			{
				Vec2 vec = draw_edge_normals[k] * radius;
				GlDrawer.DrawLine(t2 * (verts[(k - 1 + verts.Length) % verts.Length] + vec), t2 * (verts[k] + vec));
				if (radius > 0.01f)
				{
					GlDrawer.DrawCircle(t2 * verts[k], radius, angle_slow);
				}
			}
			if (drawSomeDiagonals)
			{
				GlDrawer.color = Color.gray.Tint(tint);
				for (int l = 0; l < verts.Length; l++)
				{
					GlDrawer.DrawLine(t2 * (verts[l] - draw_avg_vert_normals[l]), t2 * (verts[(l + 2) % verts.Length] - draw_avg_vert_normals[(l + 2) % verts.Length]));
				}
			}
			draw_edge_normals.Clear();
			draw_avg_vert_normals.Clear();
		}

		public void FlipX(float refX = 0f)
		{
			for (int i = 0; i < verts.Length; i++)
			{
				verts[i].x = 2f * refX - verts[i].x;
			}
			verts = verts.Reverse().ToArray();
			float[] array = invLengths.Reverse().ToArray();
			Array.Copy(array, 0, invLengths, 1, array.Length - 1);
			invLengths[0] = array.Last();
			Vec2 vec = verts.Last();
			for (int j = 0; j < invLengths.Length; j++)
			{
				ref Vec2 reference = ref verts[j];
				Pb.Debug.Assert(((reference - vec) * invLengths[j]).sqrMagnitude.IsEqual(1f) || reference == vec, "Segment lengths in PolygonShape, after flip, invalid.");
				vec = reference;
			}
		}

		public static PolygonShape FromCircle(Vec2 position, float radius)
		{
			PolygonShape polygonShape = new PolygonShape();
			polygonShape.verts = new Vec2[1] { position };
			polygonShape.CacheLengths();
			polygonShape.radius = radius;
			return polygonShape;
		}

		public static PolygonShape FromSegment(Vec2 endpoint0, Vec2 endpoint1, float radius)
		{
			PolygonShape polygonShape = new PolygonShape();
			polygonShape.verts = new Vec2[2] { endpoint0, endpoint1 };
			polygonShape.CacheLengths();
			polygonShape.radius = radius;
			return polygonShape;
		}

		public static PolygonShape FromRect(Vec2 center, Vec2 size)
		{
			PolygonShape polygonShape = new PolygonShape();
			polygonShape.verts = new Vec2[4]
			{
				new Vec2(center.x - size.x / 2f, center.y + size.y / 2f),
				new Vec2(center.x + size.x / 2f, center.y + size.y / 2f),
				new Vec2(center.x + size.x / 2f, center.y - size.y / 2f),
				new Vec2(center.x - size.x / 2f, center.y - size.y / 2f)
			};
			polygonShape.CacheLengths();
			return polygonShape;
		}

		public static PolygonShape FromPoints(Vec2[] points)
		{
			PolygonShape polygonShape = new PolygonShape();
			polygonShape.verts = new Vec2[points.Length];
			for (int i = 0; i < points.Length; i++)
			{
				polygonShape.verts[i] = points[i];
			}
			polygonShape.CacheLengths();
			return polygonShape;
		}
	}
}
