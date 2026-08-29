using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertUVsUInt16ToFloatInterleavedJob : IJobParallelForBatch
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
		public unsafe float2* result;

		public unsafe void Execute(int startIndex, int count)
		{
			float2* ptr = (float2*)((byte*)result + startIndex * outputByteStride);
			ushort* ptr2 = (ushort*)(input + startIndex * inputByteStride);
			for (int i = 0; i < count; i++)
			{
				*ptr = new float2((int)(*ptr2), 1 - ptr2[1]);
				ptr = (float2*)((byte*)ptr + outputByteStride);
				ptr2 = (ushort*)((byte*)ptr2 + inputByteStride);
			}
		}
	}
}
