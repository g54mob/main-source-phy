using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace MagicaCloth2
{
	public static class NativeArrayExtensions
	{
		public static void MC2DisposeSafe<T>(this ref NativeArray<T> array) where T : unmanaged
		{
			if (array.IsCreated)
			{
				array.Dispose();
			}
		}

		public static void MC2Resize<T>(this ref NativeArray<T> array, int size, Allocator allocator = Allocator.Persistent, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : unmanaged
		{
			if (!array.IsCreated || array.Length < size)
			{
				MC2DisposeSafe(ref array);
				array = new NativeArray<T>(size, allocator, options);
			}
		}

		public static byte[] MC2ToRawBytes<T>(this ref NativeArray<T> array) where T : unmanaged
		{
			if (!array.IsCreated || array.Length == 0)
			{
				return null;
			}
			NativeSlice<byte> nativeSlice = new NativeSlice<T>(array).SliceConvert<byte>();
			byte[] array2 = new byte[nativeSlice.Length];
			nativeSlice.CopyTo(array2);
			return array2;
		}

		public static NativeArray<T> MC2FromRawBytes<T>(byte[] bytes, Allocator allocator = Allocator.Persistent) where T : unmanaged
		{
			if (bytes == null)
			{
				return default(NativeArray<T>);
			}
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = bytes.Length / num;
			NativeArray<T> nativeArray = new NativeArray<T>(num2, allocator);
			if (num2 > 0)
			{
				using NativeArray<byte> array = new NativeArray<byte>(bytes, Allocator.Temp);
				new NativeSlice<byte>(array).SliceConvert<T>().CopyTo(nativeArray);
			}
			return nativeArray;
		}
	}
}
