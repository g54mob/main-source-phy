using System.Runtime.CompilerServices;

namespace Poly.Solver
{
	public static class JointUtil
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ComputeInverseVirtualMass(in Motion motion0, in Motion motion1, in Vec2 pivotPoint0InWorld, in Vec2 pivotPoint1InWorld, in Vec2 jointDirection)
		{
			float x = jointDirection.x;
			float y = jointDirection.y;
			float num = pivotPoint0InWorld.x - motion0.com.x;
			float num2 = pivotPoint0InWorld.y - motion0.com.y;
			float num3 = (num * y - num2 * x) * motion0.invInertia;
			float num4 = (0f - num3) * num2;
			float num5 = num3 * num;
			num = pivotPoint1InWorld.x - motion1.com.x;
			num2 = pivotPoint1InWorld.y - motion1.com.y;
			num3 = (num * y - num2 * x) * motion1.invInertia;
			num4 -= num3 * num2;
			num5 += num3 * num;
			return motion0.invMass + motion1.invMass + (x * num4 + y * num5);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ComputeInverseVirtualRotationalInertiaOnly(in Motion motion0, in Motion motion1)
		{
			return motion0.invInertia + motion1.invInertia;
		}
	}
}
