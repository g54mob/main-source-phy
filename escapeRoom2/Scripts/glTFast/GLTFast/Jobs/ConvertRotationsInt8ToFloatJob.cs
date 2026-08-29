using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertRotationsInt8ToFloatJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe sbyte* input;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe float* result;

		public unsafe void Execute(int index)
		{
			result[index * 4] = Mathf.Max((float)input[index * 4] / 127f, -1f);
			result[index * 4 + 1] = 0f - Mathf.Max((float)input[index * 4 + 1] / 127f, -1f);
			result[index * 4 + 2] = 0f - Mathf.Max((float)input[index * 4 + 2] / 127f, -1f);
			result[index * 4 + 3] = Mathf.Max((float)input[index * 4 + 3] / 127f, -1f);
		}
	}
}
