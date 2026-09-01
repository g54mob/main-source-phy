using Poly.Collide;
using Poly.Math;
using UnityEngine;

namespace Poly.Physics.Test.Visual
{
	public class PolygonSegmentOverlapTest : MonoBehaviour
	{
		public enum Method
		{
			SeparatingAxes = 0
		}

		public Method method;

		public Transform polygon;

		public Transform[] segmentB;

		private void OnValidate()
		{
			method = Method.SeparatingAxes;
		}

		private void OnDrawGizmos()
		{
			if (polygon.childCount >= 3 && segmentB.Length >= 2)
			{
				Gizmos.color = (TestOverlap() ? Color.red : Color.green);
				for (int i = 0; i < polygon.childCount; i++)
				{
					Gizmos.DrawLine(polygon.GetChild(i).position, polygon.GetChild((i + 1) % polygon.childCount).position);
				}
				Gizmos.DrawLine(segmentB[0].position, segmentB[1].position);
			}
		}

		private bool TestOverlap()
		{
			bool result = false;
			if (polygon.childCount >= 3 && segmentB.Length >= 2)
			{
				Transform2 wTa = polygon;
				PolygonShape polygonShape = new PolygonShape();
				polygonShape.verts = new Vec2[polygon.childCount];
				for (int i = 0; i < polygon.childCount; i++)
				{
					Vector3 direction = polygon.GetChild(i).position - polygon.position;
					Vec2 vec = (Vec2)polygon.InverseTransformDirection(direction);
					polygonShape.verts[i] = vec;
				}
				polygonShape.CacheLengths();
				Vec2 vec2 = (Vec2)segmentB[0].position;
				Vec2 vec3 = (Vec2)segmentB[1].position;
				Vec2 vec4 = vec3 - vec2;
				Segment segment = new Segment(vec4.magnitude, 0f);
				Transform2 wTb = new Transform2(0.5f * (vec2 + vec3), vec4.normalized);
				PolygonShape polygonB = PolygonIntersection.CreatePolygon_LOCAL_ONLY(segment);
				if (method == Method.SeparatingAxes)
				{
					result = PolygonIntersection.Overlap(polygonShape, ref wTa, polygonB, ref wTb);
				}
			}
			return result;
		}
	}
}
