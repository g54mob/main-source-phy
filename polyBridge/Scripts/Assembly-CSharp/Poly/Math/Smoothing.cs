using System;
using System.Runtime.CompilerServices;

namespace Poly.Math
{
	public static class Smoothing
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Smooth(float source, float target, float smoothing, float dt)
		{
			return LerpUnclamped(source, target, 1f - (float)System.Math.Pow(1f - smoothing, dt));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SmoothingToInterpolationParam(float smoothing, float dt)
		{
			return 1f - (float)System.Math.Pow(1f - smoothing, dt);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float LerpUnclamped(float a, float b, float t)
		{
			return (1f - t) * a + t * b;
		}
	}
}
