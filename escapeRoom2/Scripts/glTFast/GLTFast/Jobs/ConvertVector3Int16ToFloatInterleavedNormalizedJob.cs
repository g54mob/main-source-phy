using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertVector3Int16ToFloatInterleavedNormalizedJob : IJobParallelForBatch
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
			short* ptr2 = (short*)(input + startIndex * inputByteStride);
			for (int i = 0; i < count; i++)
			{
				float3 float5 = math.max(new float3(*ptr2, ptr2[1], ptr2[2]) / 32767f, -1f);
				float5.x *= -1f;
				*ptr = float5;
				ptr = (float3*)((byte*)ptr + outputByteStride);
				ptr2 = (short*)((byte*)ptr2 + inputByteStride);
			}
		}
	}
}
