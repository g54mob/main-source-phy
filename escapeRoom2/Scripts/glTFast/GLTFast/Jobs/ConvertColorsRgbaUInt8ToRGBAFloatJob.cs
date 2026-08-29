using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertColorsRgbaUInt8ToRGBAFloatJob : IJobParallelFor
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
			byte* ptr = input + index * inputByteStride;
			result[index] = new float4((float)(int)(*ptr) / 255f, (float)(int)ptr[1] / 255f, (float)(int)ptr[2] / 255f, (float)(int)ptr[3] / 255f);
		}
	}
}
