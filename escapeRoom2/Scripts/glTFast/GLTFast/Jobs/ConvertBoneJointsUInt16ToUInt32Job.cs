using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertBoneJointsUInt16ToUInt32Job : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe byte* input;

		[ReadOnly]
		public int inputByteStride;

		[WriteOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe uint4* result;

		[ReadOnly]
		public int outputByteStride;

		public unsafe void Execute(int index)
		{
			uint4* ptr = (uint4*)((byte*)result + index * outputByteStride);
			ushort* ptr2 = (ushort*)(input + index * inputByteStride);
			*ptr = new uint4(*ptr2, ptr2[1], ptr2[2], ptr2[3]);
		}
	}
}
