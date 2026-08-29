using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace MagicaCloth2
{
	public static class MathExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float MC2GetValue(this in float4x4 m, int index)
		{
			index = math.clamp(index, 0, 15);
			return m[index / 4][index % 4];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void MC2SetValue(this ref float4x4 m, int index, float value)
		{
			index = math.clamp(index, 0, 15);
			m[index / 4][index % 4] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float MC2EvaluateCurveClamp01(this in float4x4 m, float time)
		{
			return math.saturate(DataUtility.EvaluateCurve(in m, time));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float MC2EvaluateCurve(this in float4x4 m, float time)
		{
			return DataUtility.EvaluateCurve(in m, time);
		}
	}
}
