using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertIndicesUInt32ToInt32Job : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe uint* input;

		[WriteOnly]
		public NativeArray<int> result;

		public unsafe void Execute(int index)
		{
			result[index] = (int)input[index];
		}
	}
}
