using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertUVsUInt8ToFloatInterleavedNormalizedJob : IJobParallelFor
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

		public unsafe void Execute(int index)
		{
			float2* ptr = (float2*)((byte*)result + index * outputByteStride);
			byte* ptr2 = input + inputByteStride * index;
			float2 float5 = new float2((int)(*ptr2), (int)ptr2[1]) / 255f;
			float5.y = 1f - float5.y;
			*ptr = float5;
		}
	}
}
