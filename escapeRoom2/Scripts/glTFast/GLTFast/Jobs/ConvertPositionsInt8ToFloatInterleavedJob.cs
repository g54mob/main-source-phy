using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertPositionsInt8ToFloatInterleavedJob : IJobParallelForBatch
	{
		[ReadOnly]
		public int inputByteStride;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe sbyte* input;

		[ReadOnly]
		public int outputByteStride;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe float3* result;

		public unsafe void Execute(int startIndex, int count)
		{
			float3* ptr = (float3*)((byte*)result + startIndex * outputByteStride);
			sbyte* ptr2 = input + startIndex * inputByteStride;
			for (int i = 0; i < count; i++)
			{
				*ptr = new float3(-(*ptr2), ptr2[1], ptr2[2]);
				ptr = (float3*)((byte*)ptr + outputByteStride);
				ptr2 += inputByteStride;
			}
		}
	}
}
