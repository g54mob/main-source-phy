using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertPositionsUInt16ToFloatInterleavedNormalizedJob : IJobParallelForBatch
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
			ushort* ptr2 = (ushort*)(input + startIndex * inputByteStride);
			for (int i = 0; i < count; i++)
			{
				float3 float5 = new float3(0f - (float)(int)(*ptr2) / 65535f, (float)(int)ptr2[1] / 65535f, (float)(int)ptr2[2] / 65535f);
				*ptr = float5;
				ptr = (float3*)((byte*)ptr + outputByteStride);
				ptr2 = (ushort*)((byte*)ptr2 + inputByteStride);
			}
		}
	}
}
