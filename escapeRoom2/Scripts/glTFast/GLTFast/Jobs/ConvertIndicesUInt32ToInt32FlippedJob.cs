using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertIndicesUInt32ToInt32FlippedJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe uint* input;

		[WriteOnly]
		public NativeArray<int3> result;

		public unsafe void Execute(int index)
		{
			result[index] = new int3((int)input[index * 3], (int)input[index * 3 + 2], (int)input[index * 3 + 1]);
		}
	}
}
