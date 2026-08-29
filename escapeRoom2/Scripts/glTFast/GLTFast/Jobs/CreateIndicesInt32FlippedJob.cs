using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct CreateIndicesInt32FlippedJob : IJobParallelFor
	{
		[WriteOnly]
		public NativeArray<int> result;

		public void Execute(int index)
		{
			result[index] = index - 2 * (index % 3 - 1);
		}
	}
}
