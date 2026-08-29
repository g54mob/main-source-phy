using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct CreateIndicesForTriangleFanJob : IJobParallelFor
	{
		[WriteOnly]
		public NativeArray<int> result;

		public void Execute(int index)
		{
			int num = index / 3;
			int num2 = index % 3;
			result[index] = num2 switch
			{
				0 => num + 2, 
				1 => num + 1, 
				_ => 0, 
			};
		}
	}
}
