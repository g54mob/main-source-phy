using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertMatricesJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe float4x4* input;

		[WriteOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe float4x4* result;

		public unsafe void Execute(int index)
		{
			float4 c = input[index].c0;
			c.y *= -1f;
			c.z *= -1f;
			result[index].c0 = c;
			c = input[index].c1;
			c.x *= -1f;
			result[index].c1 = c;
			c = input[index].c2;
			c.x *= -1f;
			result[index].c2 = c;
			c = input[index].c3;
			c.x *= -1f;
			result[index].c3 = c;
		}
	}
}
