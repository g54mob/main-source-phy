using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct CreateIndicesForTriangleStripJob : IJobParallelFor
	{
		[WriteOnly]
		public NativeArray<int> result;

		public void Execute(int index)
		{
			int num = index / 3;
			int value = (index % 3) switch
			{
				0 => num + (1 + num % 2), 
				1 => num, 
				2 => num + (2 - num % 2), 
				_ => result[index], 
			};
			result[index] = value;
		}
	}
}
