using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertRotationsFloatToFloatJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe float4* input;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe float4* result;

		public unsafe void Execute(int index)
		{
			float4 float5 = input[index];
			float5.y *= -1f;
			float5.z *= -1f;
			result[index] = float5;
		}
	}
}
