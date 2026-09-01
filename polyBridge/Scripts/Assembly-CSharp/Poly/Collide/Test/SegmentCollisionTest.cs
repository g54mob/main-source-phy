using UnityEngine;

namespace Poly.Collide.Test
{
	public class SegmentCollisionTest : MonoBehaviour
	{
		public Transform start0;

		public Transform end0;

		public Transform start1;

		public Transform end1;

		private void OnDrawGizmos()
		{
			Vec2 vec = (Vec2)start0.position;
			Vec2 vec2 = (Vec2)end0.position;
			Vec2 vec3 = (Vec2)start1.position;
			Vec2 vec4 = (Vec2)end1.position;
			Gizmos.color = Color.white;
			Gizmos.DrawLine(vec, vec2);
			Gizmos.DrawLine(vec3, vec4);
			ProcessCollision.CalcClosestPoint_older_unused(vec, vec2, vec3, vec4, out var closest, out var closest2);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(closest, 0.05f);
			Gizmos.DrawWireSphere(closest2, 0.05f);
			Gizmos.DrawLine(closest, closest2);
			Vec2 b = vec2 - vec;
			Vec2 b2 = vec4 - vec3;
			float magnitude = b.magnitude;
			float magnitude2 = b2.magnitude;
			b /= magnitude;
			b2 /= magnitude2;
			ProcessCollision.CalcClosestPoint_Approx(vec, b, magnitude, vec3, b2, magnitude2, out closest, out closest2);
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(closest, 0.03f);
			Gizmos.DrawWireSphere(closest2, 0.03f);
			Gizmos.DrawLine(closest, closest2);
			float f = Vec2.Dot((closest2 - closest).normalized, in b);
			float f2 = Vec2.Dot((closest2 - closest).normalized, in b2);
			Gizmos.color = Color.blue;
			if (Mathf.Abs(f) < 1E-06f)
			{
				Gizmos.DrawWireCube(closest, Vec2.one * 0.2f);
			}
			if (Mathf.Abs(f2) < 1E-06f)
			{
				Gizmos.DrawWireCube(closest2, Vec2.one * 0.2f);
			}
		}
	}
}
