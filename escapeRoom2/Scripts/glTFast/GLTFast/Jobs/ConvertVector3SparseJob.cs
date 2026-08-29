using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct ConvertVector3SparseJob : IJobParallelFor
	{
		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* indexBuffer;

		public FunctionPointer<CachedFunction.GetIndexDelegate> indexConverter;

		[ReadOnly]
		public int inputByteStride;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* input;

		public FunctionPointer<CachedFunction.GetFloat3Delegate> valueConverter;

		[ReadOnly]
		public int outputByteStride;

		[ReadOnly]
		[NativeDisableUnsafePtrRestriction]
		public unsafe float3* result;

		public unsafe void Execute(int index)
		{
			int num = indexConverter.Invoke(indexBuffer, index);
			float3* destination = (float3*)((byte*)result + num * outputByteStride);
			valueConverter.Invoke(destination, (byte*)input + index * inputByteStride);
		}
	}
}
