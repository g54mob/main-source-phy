using System;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class ExSimpleNativeArray<T> : IDisposable where T : unmanaged
	{
		[Serializable]
		public class SerializationData
		{
			public int count;

			public int length;

			public byte[] arrayBytes;
		}

		private NativeArray<T> nativeArray;

		private int count;

		private int length;

		public bool IsValid => nativeArray.IsCreated;

		public int Count => count;

		public int Length => length;

		public T this[int index]
		{
			get
			{
				return nativeArray[index];
			}
			set
			{
				nativeArray[index] = value;
			}
		}

		public ExSimpleNativeArray()
		{
			count = 0;
			length = 0;
		}

		public ExSimpleNativeArray(int dataLength, bool areaOnly = false)
			: this()
		{
			nativeArray = new NativeArray<T>(dataLength, Allocator.Persistent);
			length = dataLength;
			if (!areaOnly)
			{
				count = length;
			}
		}

		public ExSimpleNativeArray(T[] dataArray)
			: this()
		{
			nativeArray = new NativeArray<T>(dataArray, Allocator.Persistent);
			length = dataArray.Length;
			count = length;
		}

		public ExSimpleNativeArray(NativeArray<T> array)
			: this()
		{
			nativeArray = new NativeArray<T>(array, Allocator.Persistent);
			length = array.Length;
			count = length;
		}

		public ExSimpleNativeArray(NativeList<T> array)
			: this()
		{
			nativeArray = new NativeArray<T>(array.AsArray(), Allocator.Persistent);
			length = array.Length;
			count = length;
		}

		public ExSimpleNativeArray(SerializationData sdata)
		{
			Deserialize(sdata);
		}

		public void Dispose()
		{
			if (nativeArray.IsCreated)
			{
				nativeArray.Dispose();
			}
			count = 0;
			length = 0;
		}

		public void SetCount(int newCount)
		{
			count = newCount;
		}

		public void SetLength(int newLength)
		{
			if (newLength > length)
			{
				Expand(newLength - length, force: true);
			}
		}

		public void AddRange(int dataLength)
		{
			Expand(dataLength);
			count += dataLength;
		}

		public void AddRange(T[] dataArray)
		{
			if (length == 0)
			{
				if (nativeArray.IsCreated)
				{
					nativeArray.Dispose();
				}
				nativeArray = new NativeArray<T>(dataArray, Allocator.Persistent);
				length = dataArray.Length;
				count = length;
			}
			else
			{
				int num = dataArray.Length;
				Expand(num);
				NativeArray<T>.Copy(dataArray, 0, nativeArray, count, num);
				count += num;
			}
		}

		public void AddRange(T[] dataArray, int cnt)
		{
			if (length == 0)
			{
				if (nativeArray.IsCreated)
				{
					nativeArray.Dispose();
				}
				nativeArray = new NativeArray<T>(cnt, Allocator.Persistent);
				NativeArray<T>.Copy(dataArray, 0, nativeArray, 0, cnt);
				length = cnt;
				count = length;
			}
			else
			{
				Expand(cnt);
				NativeArray<T>.Copy(dataArray, 0, nativeArray, count, cnt);
				count += cnt;
			}
		}

		public void AddRange(int dataLength, T fillData = default(T))
		{
			Expand(dataLength);
			Fill(count, dataLength, fillData);
			count += dataLength;
		}

		public void AddRange(NativeArray<T> narray)
		{
			if (length == 0)
			{
				if (nativeArray.IsCreated)
				{
					nativeArray.Dispose();
				}
				nativeArray = new NativeArray<T>(narray, Allocator.Persistent);
				length = narray.Length;
				count = length;
			}
			else
			{
				int num = narray.Length;
				Expand(num);
				NativeArray<T>.Copy(narray, 0, nativeArray, count, num);
				count += num;
			}
		}

		public void AddRange(NativeArray<T> narray, int start, int length)
		{
			if (length > 0)
			{
				Expand(length);
				NativeArray<T>.Copy(narray, start, nativeArray, count, length);
				count += length;
			}
		}

		public void AddRange(NativeList<T> nlist)
		{
			AddRange(nlist.AsArray());
		}

		public void AddRange(ExSimpleNativeArray<T> exarray)
		{
			AddRange(exarray.GetNativeArray());
		}

		public unsafe void AddRange<U>(U[] array) where U : struct
		{
			int num = array.Length;
			Expand(num);
			int num2 = UnsafeUtility.SizeOf<T>();
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			UnsafeUtility.MemCpy(unsafePtr + count * num2, source, num * num2);
			UnsafeUtility.ReleaseGCObject(gcHandle);
			count += num;
		}

		public unsafe void AddRangeTypeChange<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			int num2 = UnsafeUtility.SizeOf<T>();
			int num3 = array.Length * num / num2;
			Expand(num3);
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			UnsafeUtility.MemCpy(unsafePtr + count * num2, source, num3 * num2);
			UnsafeUtility.ReleaseGCObject(gcHandle);
			count += num3;
		}

		public unsafe void AddRangeTypeChange<U>(NativeArray<U> array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			int num2 = UnsafeUtility.SizeOf<T>();
			int num3 = array.Length * num / num2;
			Expand(num3);
			byte* unsafePtr = (byte*)array.GetUnsafePtr();
			byte* unsafePtr2 = (byte*)nativeArray.GetUnsafePtr();
			UnsafeUtility.MemCpy(unsafePtr2 + count * num2, unsafePtr, num3 * num2);
			count += num3;
		}

		public unsafe void AddRangeStride<U>(U[] array) where U : struct
		{
			int num = array.Length;
			Expand(num);
			int num2 = UnsafeUtility.SizeOf<U>();
			int num3 = UnsafeUtility.SizeOf<T>();
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			int elementSize = math.min(num2, num3);
			UnsafeUtility.MemCpyStride(unsafePtr + count * num3, num3, source, num2, elementSize, num);
			UnsafeUtility.ReleaseGCObject(gcHandle);
			count += num;
		}

		public void Add(T data)
		{
			if (Length == 0)
			{
				Expand(16);
			}
			else if (count == Length)
			{
				Expand(Length);
			}
			nativeArray[count] = data;
			count++;
		}

		public T[] ToArray()
		{
			return nativeArray.ToArray();
		}

		public void CopyTo(T[] array)
		{
			NativeArray<T>.Copy(nativeArray, array);
		}

		public void CopyTo<U>(U[] array) where U : struct
		{
			NativeArray<U>.Copy(nativeArray.Reinterpret<U>(), array);
		}

		public unsafe void CopyToWithTypeChange<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = UnsafeUtility.SizeOf<U>();
			int num3 = Length * num / num2;
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			UnsafeUtility.MemCpy(UnsafeUtility.PinGCArrayAndGetDataAddress(array, out var gcHandle), unsafePtr, num3 * num2);
			UnsafeUtility.ReleaseGCObject(gcHandle);
		}

		public unsafe void CopyToWithTypeChangeStride<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int destinationStride = UnsafeUtility.SizeOf<U>();
			int num2 = Length;
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			ulong gcHandle;
			void* destination = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			int elementSize = num;
			UnsafeUtility.MemCpyStride(destination, destinationStride, unsafePtr, num, elementSize, num2);
			UnsafeUtility.ReleaseGCObject(gcHandle);
		}

		public void CopyFrom(NativeArray<T> array)
		{
			NativeArray<T>.Copy(array, nativeArray);
		}

		public void CopyFrom<U>(NativeArray<U> array) where U : struct
		{
			NativeArray<T>.Copy(array.Reinterpret<T>(), nativeArray);
		}

		public unsafe void CopyFromWithTypeChangeStride<U>(NativeArray<U> array) where U : struct
		{
			int sourceStride = UnsafeUtility.SizeOf<U>();
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = array.Length;
			byte* unsafePtr = (byte*)array.GetUnsafePtr();
			byte* unsafePtr2 = (byte*)nativeArray.GetUnsafePtr();
			int elementSize = num;
			UnsafeUtility.MemCpyStride(unsafePtr2, num, unsafePtr, sourceStride, elementSize, num2);
		}

		public void Fill(int startIndex, int dataLength, T fillData = default(T))
		{
			FillInternal(startIndex, dataLength, fillData);
		}

		private unsafe void FillInternal(int start, int size, T fillData = default(T))
		{
			void* unsafePtr = nativeArray.GetUnsafePtr();
			int num = start;
			int num2 = 0;
			while (num2 < size)
			{
				UnsafeUtility.WriteArrayElement(unsafePtr, num, fillData);
				num2++;
				num++;
			}
		}

		public NativeArray<T> GetNativeArray()
		{
			return nativeArray;
		}

		public NativeArray<U> GetNativeArray<U>() where U : struct
		{
			return nativeArray.Reinterpret<U>();
		}

		private void Expand(int dataLength, bool force = false, bool copy = true)
		{
			int num = (force ? (length + dataLength) : (count + dataLength));
			if (length == 0)
			{
				if (nativeArray.IsCreated)
				{
					nativeArray.Dispose();
				}
				nativeArray = new NativeArray<T>(dataLength, Allocator.Persistent);
				length = dataLength;
			}
			else if (num > Length)
			{
				NativeArray<T> dst = new NativeArray<T>(num, Allocator.Persistent);
				if (copy)
				{
					NativeArray<T>.Copy(nativeArray, dst, count);
				}
				nativeArray.Dispose();
				nativeArray = dst;
				length = num;
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"ExSimpleNativeArray Length:{Length} Count:{Count} IsValid:{IsValid}");
			stringBuilder.AppendLine("---- Datas[~100] ----");
			if (IsValid)
			{
				for (int i = 0; i < Length && i < 100; i++)
				{
					stringBuilder.AppendLine(nativeArray[i].ToString());
				}
			}
			return stringBuilder.ToString();
		}

		public SerializationData Serialize()
		{
			SerializationData serializationData = new SerializationData();
			serializationData.count = count;
			serializationData.length = length;
			if (nativeArray.IsCreated && nativeArray.Length > 0)
			{
				serializationData.arrayBytes = NativeArrayExtensions.MC2ToRawBytes(ref nativeArray);
			}
			return serializationData;
		}

		public bool Deserialize(SerializationData data)
		{
			try
			{
				Dispose();
				count = data.count;
				length = data.length;
				if (data.length > 0 && data.arrayBytes != null)
				{
					nativeArray = NativeArrayExtensions.MC2FromRawBytes<T>(data.arrayBytes);
				}
				return true;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return false;
			}
		}
	}
}
