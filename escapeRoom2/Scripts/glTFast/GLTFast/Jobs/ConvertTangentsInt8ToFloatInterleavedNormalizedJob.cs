using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertTangentsInt8ToFloatInterleavedNormalizedJob : IJobParallelForBatch
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
		public unsafe float4* result;

		public unsafe void Execute(int startIndex, int count)
		{
			float4* ptr = (float4*)((byte*)result + startIndex * outputByteStride);
			sbyte* ptr2 = input + startIndex * inputByteStride;
			for (int i = 0; i < count; i++)
			{
				float4 x = math.max(new float4(*ptr2, ptr2[1], ptr2[2], ptr2[3]) / 127f, -1f);
				x.z *= -1f;
				*ptr = math.normalize(x);
				ptr = (float4*)((byte*)ptr + outputByteStride);
				ptr2 += inputByteStride;
			}
		}
	}
}
