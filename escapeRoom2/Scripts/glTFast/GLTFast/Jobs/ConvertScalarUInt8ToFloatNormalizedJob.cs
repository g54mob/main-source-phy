using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertScalarUInt8ToFloatNormalizedJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe byte* input;

		[WriteOnly]
		public NativeArray<float> result;

		public unsafe void Execute(int index)
		{
			result[index] = (float)(int)input[index] / 255f;
		}
	}
}
