using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertBoneWeightsUInt8ToFloatInterleavedJob : IJobParallelForBatch
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
		public unsafe float4* result;

		public unsafe void Execute(int startIndex, int count)
		{
			float4* ptr = (float4*)((byte*)result + startIndex * outputByteStride);
			byte* ptr2 = input + startIndex * inputByteStride;
			for (int i = 0; i < count; i++)
			{
				*ptr = new float4((float)(int)(*ptr2) / 255f, (float)(int)ptr2[1] / 255f, (float)(int)ptr2[2] / 255f, (float)(int)ptr2[3] / 255f);
				ptr = (float4*)((byte*)ptr + outputByteStride);
				ptr2 += inputByteStride;
			}
		}
	}
}
