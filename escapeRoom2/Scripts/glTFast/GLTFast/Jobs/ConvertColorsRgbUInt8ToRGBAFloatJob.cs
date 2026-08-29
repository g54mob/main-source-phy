using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertColorsRgbUInt8ToRGBAFloatJob : IJobParallelFor
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
			result[index] = new float4(new float3((int)(*ptr), (int)ptr[1], (int)ptr[2]) / 255f, 1f);
		}
	}
}
