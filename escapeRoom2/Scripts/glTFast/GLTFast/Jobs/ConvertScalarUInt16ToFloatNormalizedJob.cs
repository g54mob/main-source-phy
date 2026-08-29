using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertScalarUInt16ToFloatNormalizedJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe ushort* input;

		[WriteOnly]
		public NativeArray<float> result;

		public unsafe void Execute(int index)
		{
			result[index] = (float)(int)input[index] / 65535f;
		}
	}
}
