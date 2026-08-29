using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertVector3FloatToFloatInterleavedJob : IJobParallelForBatch
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
			float3* ptr2 = (float3*)(input + startIndex * inputByteStride);
			for (int i = 0; i < count; i++)
			{
				float3 float5 = *ptr2;
				float5.x *= -1f;
				*ptr = float5;
				ptr = (float3*)((byte*)ptr + outputByteStride);
				ptr2 = (float3*)((byte*)ptr2 + inputByteStride);
			}
		}
	}
}
