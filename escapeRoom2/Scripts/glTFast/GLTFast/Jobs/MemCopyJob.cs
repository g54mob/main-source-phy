using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct MemCopyJob : IJob
	{
		[ReadOnly]
		public long bufferSize;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* input;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* result;

		public unsafe void Execute()
		{
			UnsafeUtility.MemCpy(result, input, bufferSize);
		}
	}
}
