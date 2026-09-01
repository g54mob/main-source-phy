using UnityEngine;

namespace Poly.Collide.Test.Visual
{
	public class SeparatingNormalOnSegmentsTest : MonoBehaviour
	{
		public Transform[] segmentA;

		public Transform[] segmentB;

		private void TestSeparatingNormal(out Vector3 point, out Vector3 normal, out float distance)
		{
			Vec2 vec = (Vec2)segmentA[0].position;
			Vec2 vec2 = (Vec2)segmentA[1].position;
			Vec2 vec3 = (Vec2)segmentB[0].position;
			Vec2 vec4 = (Vec2)segmentB[1].position;
			Vec2 dir = vec2 - vec;
			Vec2 dir2 = vec4 - vec3;
			float magnitude = dir.magnitude;
			float magnitude2 = dir2.magnitude;
			dir /= magnitude;
			dir2 /= magnitude2;
			Vec2 closest;
			Vec2 closest2;
			Vec2 vec5 = ProcessCollision.CalcClosestPoint_Approx(vec, dir, magnitude, vec3, dir2, magnitude2, out closest, out closest2);
			Vec2 vec6 = closest2 - closest;
			float separatingDistance = vec6.magnitude;
			if (1E-06f < separatingDistance)
			{
				normal = vec6 / separatingDistance;
			}
			else
			{
				ProcessCollision.CalcSeparatingNormal_Approx(dir, magnitude, vec5.x, dir2, magnitude2, vec5.y, out var separatingNormal, out separatingDistance);
				normal = separatingNormal;
			}
			distance = separatingDistance;
			point = closest;
		}

		private void OnDrawGizmos()
		{
			if (segmentA.Length >= 2 && segmentB.Length >= 2)
			{
				TestSeparatingNormal(out var point, out var normal, out var distance);
				Gizmos.color = ((distance < 0f) ? Color.red : Color.green);
				Gizmos.DrawLine(segmentA[0].position, segmentA[1].position);
				Gizmos.DrawLine(segmentB[0].position, segmentB[1].position);
				Gizmos.color = Color.blue;
				Gizmos.DrawWireSphere(point, 0.05f);
				Gizmos.DrawLine(point, point + normal * 0.1f);
			}
		}
	}
}
