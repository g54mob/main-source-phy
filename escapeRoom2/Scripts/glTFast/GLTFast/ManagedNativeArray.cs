using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace GLTFast
{
	public sealed class ManagedNativeArray<TIn, TOut> : IDisposable where TIn : unmanaged where TOut : unmanaged
	{
		private NativeArray<TOut> m_NativeArray;

		private GCHandle m_BufferHandle;

		private readonly bool m_Pinned;

		public NativeArray<TOut> nativeArray => m_NativeArray;

		public unsafe ManagedNativeArray(TIn[] original)
		{
			if (original != null)
			{
				m_BufferHandle = GCHandle.Alloc(original, GCHandleType.Pinned);
				fixed (TIn* ptr = &original[0])
				{
					void* dataPointer = ptr;
					m_NativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<TOut>(dataPointer, original.Length, Allocator.None);
				}
				m_Pinned = true;
			}
			else
			{
				m_NativeArray = default(NativeArray<TOut>);
			}
		}

		public void Dispose()
		{
			if (m_Pinned)
			{
				m_BufferHandle.Free();
			}
		}
	}
}
