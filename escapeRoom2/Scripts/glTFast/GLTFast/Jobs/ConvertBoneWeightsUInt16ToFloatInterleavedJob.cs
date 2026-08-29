using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertBoneWeightsUInt16ToFloatInterleavedJob : IJobParallelForBatch
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
			ushort* ptr2 = (ushort*)(input + startIndex * inputByteStride);
			for (int i = 0; i < count; i++)
			{
				*ptr = new float4((float)(int)(*ptr2) / 65535f, (float)(int)ptr2[1] / 65535f, (float)(int)ptr2[2] / 65535f, (float)(int)ptr2[3] / 65535f);
				ptr = (float4*)((byte*)ptr + outputByteStride);
				ptr2 = (ushort*)((byte*)ptr2 + inputByteStride);
			}
		}
	}
}
