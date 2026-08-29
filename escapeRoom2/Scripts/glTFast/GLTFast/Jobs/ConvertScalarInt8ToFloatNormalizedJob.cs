using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertScalarInt8ToFloatNormalizedJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe sbyte* input;

		[WriteOnly]
		public NativeArray<float> result;

		public unsafe void Execute(int index)
		{
			result[index] = math.max((float)input[index] / 127f, -1f);
		}
	}
}
