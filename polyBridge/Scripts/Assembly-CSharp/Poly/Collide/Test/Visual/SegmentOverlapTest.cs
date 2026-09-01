using Poly.Math;
using UnityEngine;

namespace Poly.Collide.Test.Visual
{
	public class SegmentOverlapTest : MonoBehaviour
	{
		public Transform[] segmentA;

		public Transform[] segmentB;

		private bool TestOverlap()
		{
			bool result = false;
			if (segmentA.Length >= 2 && segmentB.Length >= 2)
			{
				Vector2 vector = segmentA[0].position;
				Vector2 vector2 = segmentB[0].position;
				Vector2 vector3 = (Vector2)segmentA[1].position - vector;
				Vector2 vector4 = (Vector2)segmentB[1].position - vector2;
				Segment segment = new Segment(vector3.magnitude, 0f);
				Segment segment2 = new Segment(vector4.magnitude, 0f);
				vector = 0.5f * (vector + (Vector2)segmentA[1].position);
				vector2 = 0.5f * (vector2 + (Vector2)segmentB[1].position);
				Transform2 tA = new Transform2(vector, vector3.normalized);
				Transform2 tB = new Transform2(vector2, vector4.normalized);
				result = SegmentIntersection.Overlap(segment, ref tA, segment2, ref tB);
			}
			return result;
		}

		private void OnDrawGizmos()
		{
			if (segmentA.Length >= 2 && segmentB.Length >= 2)
			{
				Gizmos.color = (TestOverlap() ? Color.red : Color.green);
				Gizmos.DrawLine(segmentA[0].position, segmentA[1].position);
				Gizmos.DrawLine(segmentB[0].position, segmentB[1].position);
			}
		}
	}
}
