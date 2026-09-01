using System;
using System.Collections.Generic;
using Poly.Collide;
using Poly.Math;
using UnityEngine;

namespace Poly.Physics
{
	public static class InertiaComputer
	{
		public static InertiaInfo ComputeInfoFromShapes(Shape[] shapes)
		{
			InertiaInfo result = default(InertiaInfo);
			List<InertiaInfo> list = new List<InertiaInfo>();
			for (int i = 0; i < shapes.Length; i++)
			{
				PolygonShape polygonShape = shapes[i] as PolygonShape;
				if ((bool)polygonShape)
				{
					InertiaInfo item = ComputeInfoFromPolygon(polygonShape);
					result.area += item.area;
					result.com += item.com * item.area;
					list.Add(item);
				}
			}
			if (result.area > 5.877472E-39f)
			{
				result.com /= result.area;
			}
			foreach (InertiaInfo item2 in list)
			{
				result.inertiaFactorAroundCom += (item2.inertiaFactorAroundCom + (item2.com - result.com).sqrMagnitude) * item2.area;
			}
			if (result.area > 5.877472E-39f)
			{
				result.inertiaFactorAroundCom /= result.area;
			}
			return result;
		}

		public static InertiaInfo ComputeInfoFromPolygon(PolygonShape polygon)
		{
			InertiaInfo result = default(InertiaInfo);
			if (polygon.verts.Length == 1)
			{
				return ComputeInfoFromCircle(polygon);
			}
			if (polygon.verts.Length != 2)
			{
				return ComputeInfoFromValidPolygon(polygon);
			}
			return result;
		}

		public static InertiaInfo ComputeInfoFromCircle(PolygonShape circle)
		{
			float num = circle.radius * circle.radius;
			InertiaInfo result = default(InertiaInfo);
			result.com = circle.verts[0];
			result.area = MathF.PI * num;
			result.inertiaFactorAroundCom = 0.5f * num;
			return result;
		}

		public static float CalcTriangleAreaAndCom(TriangleShape tri, out Vec2 com)
		{
			Vec2 a = tri.v1 - tri.v0;
			Vec2 b = tri.v2 - tri.v0;
			com = 1f / 3f * (tri.v0 + tri.v1 + tri.v2);
			return 0.5f * Mathf.Abs(Vec2.Cross(in a, in b));
		}

		public static float CalcPolygon_Layered_AreaAndCom(PolygonShape poly, out Vec2 com)
		{
			int num = poly.verts.Length - 2;
			float num2 = 0f;
			com = Vec2.zero;
			Vec2 a = poly.verts[poly.verts.Length - 2];
			Vec2 b = poly.verts[poly.verts.Length - 1];
			for (int i = 0; i < num; i++)
			{
				Vec2 vec = poly.verts[i];
				Vec2 com2;
				float num3 = CalcTriangleAreaAndCom(new TriangleShape(a, b, vec), out com2);
				num2 += num3;
				com += com2 * num3;
				b = vec;
			}
			com /= num2;
			return num2;
		}

		public static float CalcTriangleInertiaAroundV0(TriangleShape tri, out float inertiaFactor)
		{
			Vec2 a = tri.v1 - tri.v0;
			Vec2 b = tri.v2 - tri.v0;
			float x = a.x;
			float x2 = b.x;
			float y = a.y;
			float y2 = b.y;
			float num = 1f / 12f * (x * x + x * x2 + x2 * x2 + y * y + y * y2 + y2 * y2);
			float num2 = Mathf.Abs(Vec2.Cross(in a, in b));
			float num3 = 0.5f * num2;
			inertiaFactor = num * num2 / num3;
			return num3;
		}

		public static InertiaInfo ComputeInfoFromValidPolygon(PolygonShape poly)
		{
			int num = poly.verts.Length;
			Vec2 com;
			float area = CalcPolygon_Layered_AreaAndCom(poly, out com);
			float num2 = 0f;
			float num3 = 0f;
			Vec2 b = poly.verts[poly.verts.Length - 1];
			for (int i = 0; i < num; i++)
			{
				Vec2 vec = poly.verts[i];
				float inertiaFactor;
				float num4 = CalcTriangleInertiaAroundV0(new TriangleShape(com, b, vec), out inertiaFactor);
				num3 += inertiaFactor * num4;
				num2 += num4;
				b = vec;
			}
			float inertiaFactorAroundCom = num3 / num2;
			InertiaInfo result = default(InertiaInfo);
			result.com = com;
			result.area = area;
			result.inertiaFactorAroundCom = inertiaFactorAroundCom;
			return result;
		}

		public static void SubtractInertiaFromAnchors(ref InertiaInfo info, Transform2 bodyTransform, float bodyMassWithoutAnchors, List<Rigidbody.AnchorInfo> anchors)
		{
			float combinedMass = bodyMassWithoutAnchors;
			anchors.ForEach(delegate(Rigidbody.AnchorInfo a)
			{
				combinedMass += a.mass;
			});
			_ = combinedMass;
			Vec2 weightedCom = info.com * combinedMass;
			anchors.ForEach(delegate(Rigidbody.AnchorInfo a)
			{
				weightedCom -= a.localPosition * a.mass;
			});
			Vec2 newCom = weightedCom / bodyMassWithoutAnchors;
			float newInertia = info.inertiaFactorAroundCom * combinedMass + (info.com - newCom).sqrMagnitude * combinedMass;
			anchors.ForEach(delegate(Rigidbody.AnchorInfo a)
			{
				newInertia -= (a.localPosition - newCom).sqrMagnitude * a.mass;
			});
			float inertiaFactorAroundCom = newInertia / bodyMassWithoutAnchors;
			info.com = newCom;
			info.inertiaFactorAroundCom = inertiaFactorAroundCom;
			float totalMass = bodyMassWithoutAnchors;
			Vec2 accumulatedComTimesMass = info.com * bodyMassWithoutAnchors;
			anchors.ForEach(delegate(Rigidbody.AnchorInfo a)
			{
				totalMass += a.mass;
				accumulatedComTimesMass += a.localPosition * a.mass;
			});
			Vec2 checkCom = accumulatedComTimesMass / totalMass;
			float inertia = info.inertiaFactorAroundCom * bodyMassWithoutAnchors + (checkCom - info.com).sqrMagnitude * bodyMassWithoutAnchors;
			anchors.ForEach(delegate(Rigidbody.AnchorInfo a)
			{
				inertia += (checkCom - a.localPosition).sqrMagnitude * a.mass;
			});
		}
	}
}
