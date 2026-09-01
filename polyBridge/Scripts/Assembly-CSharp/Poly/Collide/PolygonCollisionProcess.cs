using Poly.Math;

namespace Poly.Collide
{
	public struct PolygonCollisionProcess
	{
		public Transform2 aTb;

		public Vec2[] vA;

		public Vec2[] vB;

		public float[] invLengthsA;

		public float[] invLengthsB;

		public float radiusA;

		public float radiusB;

		public int vB_Count;

		private static Vec2[] processVertsBuffer = new Vec2[32];

		public static void Init(ref PolygonShape polyA, ref Transform2 wTa, ref PolygonShape polyB, ref Transform2 wTb, out PolygonCollisionProcess process)
		{
			Transform2._InvMul(ref wTa, ref wTb, out process.aTb);
			process.vA = polyA.verts;
			process.vB_Count = polyB.verts.Length;
			process.vB = processVertsBuffer;
			for (int i = 0; i < process.vB_Count; i++)
			{
				Transform2._InlineMul(ref process.aTb, ref polyB.verts[i], out process.vB[i]);
			}
			process.invLengthsA = polyA.invLengths;
			process.invLengthsB = polyB.invLengths;
			process.radiusA = polyA.radius;
			process.radiusB = polyB.radius;
		}
	}
}
