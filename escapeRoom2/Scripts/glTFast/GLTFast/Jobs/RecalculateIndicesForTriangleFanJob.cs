using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct RecalculateIndicesForTriangleFanJob : IJobParallelFor
	{
		[ReadOnly]
		public NativeArray<int> input;

		[WriteOnly]
		[NativeDisableParallelForRestriction]
		public NativeArray<int> result;

		public void Execute(int index)
		{
			int num = index * 3;
			result[num + 1] = input[index + 1];
			result[num] = input[index + 2];
			result[num + 2] = 0;
		}
	}
}
