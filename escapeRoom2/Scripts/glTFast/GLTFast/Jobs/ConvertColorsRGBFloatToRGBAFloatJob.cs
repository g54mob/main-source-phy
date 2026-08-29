using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertColorsRGBFloatToRGBAFloatJob : IJobParallelFor
	{
		[ReadOnly]
		public int inputByteStride;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe byte* input;

		[WriteOnly]
		public NativeArray<float4> result;

		public unsafe void Execute(int index)
		{
			float3* ptr = (float3*)(input + index * inputByteStride);
			result[index] = new float4(*ptr, 1f);
		}
	}
}
