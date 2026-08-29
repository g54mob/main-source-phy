using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertRotationsInt16ToFloatJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe short* input;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe float* result;

		public unsafe void Execute(int index)
		{
			result[index * 4] = Mathf.Max((float)input[index * 4] / 32767f, -1f);
			result[index * 4 + 1] = 0f - Mathf.Max((float)input[index * 4 + 1] / 32767f, -1f);
			result[index * 4 + 2] = 0f - Mathf.Max((float)input[index * 4 + 2] / 32767f, -1f);
			result[index * 4 + 3] = Mathf.Max((float)input[index * 4 + 3] / 32767f, -1f);
		}
	}
}
