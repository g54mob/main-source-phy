using System;
using System.Runtime.CompilerServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier
{
	public static class MathHelper
	{
		public const float PI = (float)Math.PI;

		public const double PId = Math.PI;

		public const float Deg2Rad = (float)Math.PI / 180f;

		public const double Deg2Radd = Math.PI / 180.0;

		public const float Rad2Deg = 180f / (float)Math.PI;

		public const double Rad2Degd = 180.0 / Math.PI;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Min(double val1, double val2, double val3)
		{
			return 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Clamp(double value, double min, double max)
		{
			return 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double TriangleArea(ref Vector3d p0, ref Vector3d p1, ref Vector3d p2)
		{
			return 0.0;
		}
	}
}
