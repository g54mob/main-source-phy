using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertColorsRgbaUInt16ToRGBAFloatJob : IJobParallelForBatch
	{
		[ReadOnly]
		public int inputByteStride;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe ushort* input;

		[WriteOnly]
		public NativeArray<float4> result;

		public unsafe void Execute(int startIndex, int count)
		{
			ushort* ptr = (ushort*)((byte*)input + startIndex * inputByteStride);
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				result[i] = new float4((float)(int)(*ptr) / 65535f, (float)(int)ptr[1] / 65535f, (float)(int)ptr[2] / 65535f, (float)(int)ptr[3] / 65535f);
				ptr = (ushort*)((byte*)ptr + inputByteStride);
			}
		}
	}
}
