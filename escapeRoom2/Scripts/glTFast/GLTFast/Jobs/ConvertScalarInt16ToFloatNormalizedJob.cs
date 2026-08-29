using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertScalarInt16ToFloatNormalizedJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe short* input;

		[WriteOnly]
		public NativeArray<float> result;

		public unsafe void Execute(int index)
		{
			result[index] = math.max((float)input[index] / 32767f, -1f);
		}
	}
}
