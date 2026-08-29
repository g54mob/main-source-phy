using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertIndicesUInt16ToInt32FlippedJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe ushort* input;

		[WriteOnly]
		public NativeArray<int3> result;

		public unsafe void Execute(int index)
		{
			result[index] = new int3(input[index * 3], input[index * 3 + 2], input[index * 3 + 1]);
		}
	}
}
