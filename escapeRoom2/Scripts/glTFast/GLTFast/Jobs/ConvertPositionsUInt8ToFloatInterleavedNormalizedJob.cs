using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertPositionsUInt8ToFloatInterleavedNormalizedJob : IJobParallelForBatch
	{
		[ReadOnly]
		public int inputByteStride;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe byte* input;

		[ReadOnly]
		public int outputByteStride;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe float3* result;

		public unsafe void Execute(int startIndex, int count)
		{
			float3* ptr = (float3*)((byte*)result + startIndex * outputByteStride);
			byte* ptr2 = input + startIndex * inputByteStride;
			for (int i = 0; i < count; i++)
			{
				*ptr = new float3(0f - (float)(int)(*ptr2) / 255f, (float)(int)ptr2[1] / 255f, (float)(int)ptr2[2] / 255f);
				ptr = (float3*)((byte*)ptr + outputByteStride);
				ptr2 += inputByteStride;
			}
		}
	}
}
