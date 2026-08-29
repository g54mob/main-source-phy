using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertTangentsFloatToFloatInterleavedJob : IJobParallelForBatch
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
			float4* ptr2 = (float4*)(input + startIndex * inputByteStride);
			for (int i = 0; i < count; i++)
			{
				float4 float5 = *ptr2;
				float5.z *= -1f;
				*ptr = float5;
				ptr = (float4*)((byte*)ptr + outputByteStride);
				ptr2 = (float4*)((byte*)ptr2 + inputByteStride);
			}
		}
	}
}
