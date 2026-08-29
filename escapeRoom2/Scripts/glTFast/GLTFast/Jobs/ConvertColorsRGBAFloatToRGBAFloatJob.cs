using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertColorsRGBAFloatToRGBAFloatJob : IJobParallelForBatch
	{
		[ReadOnly]
		public int inputByteStride;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe byte* input;

		[WriteOnly]
		public NativeArray<float4> result;

		public unsafe void Execute(int startIndex, int count)
		{
			float4* ptr = (float4*)(input + startIndex * inputByteStride);
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				result[i] = *ptr;
				ptr = (float4*)((byte*)ptr + inputByteStride);
			}
		}
	}
}
