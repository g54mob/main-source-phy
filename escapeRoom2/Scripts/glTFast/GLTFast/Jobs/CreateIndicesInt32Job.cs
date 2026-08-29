using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct CreateIndicesInt32Job : IJobParallelFor
	{
		[WriteOnly]
		public NativeArray<int> result;

		public void Execute(int index)
		{
			result[index] = index;
		}
	}
}
