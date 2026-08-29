using System.IO;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace GLTFast
{
	internal static class NativeArrayExtensions
	{
		internal unsafe static UnmanagedMemoryStream ToUnmanagedMemoryStream(this NativeArray<byte> data)
		{
			return new UnmanagedMemoryStream((byte*)data.GetUnsafePtr(), data.Length, data.Length, FileAccess.Write);
		}

		internal unsafe static UnmanagedMemoryStream ToUnmanagedMemoryStream(this NativeArray<byte>.ReadOnly data, uint start, uint count)
		{
			return new UnmanagedMemoryStream((byte*)data.GetUnsafeReadOnlyPtr() + start, count, count, FileAccess.Read);
		}

		internal unsafe static uint ReadUInt32(this NativeArray<byte>.ReadOnly data, int offset)
		{
			uint* ptr = (uint*)((byte*)data.GetUnsafeReadOnlyPtr() + offset);
			return *ptr;
		}

		internal unsafe static NativeSlice<byte> Slice(this NativeArray<byte>.ReadOnly data, int start, int length)
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((byte*)data.GetUnsafeReadOnlyPtr() + start, length, Allocator.None);
		}
	}
}
