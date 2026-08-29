using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct RecalculateIndicesForTriangleStripJob : IJobParallelFor
	{
		[ReadOnly]
		public NativeArray<int> input;

		[WriteOnly]
		[NativeDisableParallelForRestriction]
		public NativeArray<int> result;

		public void Execute(int index)
		{
			int num = index * 3;
			result[num + 1] = input[index];
			result[num] = input[index + (1 + index % 2)];
			result[num + 2] = input[index + (2 - index % 2)];
		}
	}
}
