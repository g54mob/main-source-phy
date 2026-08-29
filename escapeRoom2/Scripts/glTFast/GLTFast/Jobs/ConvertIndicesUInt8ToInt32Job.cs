using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertIndicesUInt8ToInt32Job : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe byte* input;

		[WriteOnly]
		public NativeArray<int> result;

		public unsafe void Execute(int index)
		{
			result[index] = input[index];
		}
	}
}
