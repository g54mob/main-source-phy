using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertUVsInt8ToFloatInterleavedNormalizedJob : IJobParallelForBatch
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
		public unsafe float2* result;

		public unsafe void Execute(int startIndex, int count)
		{
			float2* ptr = (float2*)((byte*)result + startIndex * outputByteStride);
			sbyte* ptr2 = input + startIndex * inputByteStride;
			for (int i = 0; i < count; i++)
			{
				float2 float5 = math.max(new float2(*ptr2, ptr2[1]) / 127f, -1f);
				float5.y = 1f - float5.y;
				*ptr = float5;
				ptr = (float2*)((byte*)ptr + outputByteStride);
				ptr2 += inputByteStride;
			}
		}
	}
}
