using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace MagicaCloth2
{
	public class ExNativeArray<T> : IDisposable where T : unmanaged
	{
		private NativeArray<T> nativeArray;

		private List<DataChunk> emptyChunks = new List<DataChunk>();

		private int useCount;

		public bool IsValid => nativeArray.IsCreated;

		public int Length
		{
			get
			{
				if (!nativeArray.IsCreated)
				{
					return 0;
				}
				return nativeArray.Length;
			}
		}

		public int Count => useCount;

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

		public void Dispose()
		{
			if (nativeArray.IsCreated)
			{
				nativeArray.Dispose();
			}
			emptyChunks.Clear();
			useCount = 0;
		}

		public ExNativeArray()
		{
		}

		public ExNativeArray(int emptyLength, bool create = false)
			: this()
		{
			if (emptyLength > 0)
			{
				nativeArray = new NativeArray<T>(emptyLength, Allocator.Persistent);
				DataChunk item = new DataChunk(0, emptyLength);
				emptyChunks.Add(item);
				if (create)
				{
					AddRange(emptyLength);
				}
			}
			else if (create)
			{
				nativeArray = new NativeArray<T>(0, Allocator.Persistent);
			}
		}

		public ExNativeArray(int emptyLength, T fillData)
			: this(emptyLength, false)
		{
			if (emptyLength > 0)
			{
				Fill(fillData);
			}
		}

		public ExNativeArray(NativeArray<T> dataArray)
			: this()
		{
			this.AddRange<T>(dataArray);
		}

		public ExNativeArray(T[] dataArray)
			: this()
		{
			AddRange(dataArray);
		}

		public DataChunk AddRange(int dataLength)
		{
			if (dataLength == 0)
			{
				if (!nativeArray.IsCreated)
				{
					nativeArray = new NativeArray<T>(0, Allocator.Persistent);
				}
				return DataChunk.Empty;
			}
			DataChunk emptyChunk = GetEmptyChunk(dataLength);
			if (!emptyChunk.IsValid)
			{
				int length = Length;
				int num = Length + math.max(dataLength, length);
				if (length == 0)
				{
					if (nativeArray.IsCreated)
					{
						nativeArray.Dispose();
					}
					nativeArray = new NativeArray<T>(num, Allocator.Persistent);
					emptyChunk.dataLength = dataLength;
				}
				else
				{
					NativeArray<T> dst = new NativeArray<T>(num, Allocator.Persistent);
					NativeArray<T>.Copy(nativeArray, dst, length);
					nativeArray.Dispose();
					nativeArray = dst;
					emptyChunk.startIndex = length;
					emptyChunk.dataLength = dataLength;
					int num2 = length + dataLength;
					if (num2 < num)
					{
						DataChunk chunk = new DataChunk(num2, num - num2);
						AddEmptyChunk(chunk);
					}
				}
			}
			useCount = math.max(useCount, emptyChunk.startIndex + emptyChunk.dataLength);
			return emptyChunk;
		}

		public DataChunk AddRange(int dataLength, T fillData = default(T))
		{
			DataChunk dataChunk = AddRange(dataLength);
			Fill(dataChunk, fillData);
			return dataChunk;
		}

		public DataChunk AddRange(T[] array)
		{
			if (array == null || array.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = array.Length;
			DataChunk result = AddRange(num);
			NativeArray<T>.Copy(array, 0, nativeArray, result.startIndex, num);
			return result;
		}

		public DataChunk AddRange(NativeArray<T> narray, int length = 0)
		{
			if (!narray.IsCreated || narray.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = ((length > 0) ? length : narray.Length);
			DataChunk result = AddRange(num);
			NativeArray<T>.Copy(narray, 0, nativeArray, result.startIndex, num);
			return result;
		}

		public DataChunk AddRange(ExNativeArray<T> exarray)
		{
			return AddRange(exarray.GetNativeArray(), exarray.Count);
		}

		public DataChunk AddRange(ExSimpleNativeArray<T> exarray)
		{
			return AddRange(exarray.GetNativeArray(), exarray.Count);
		}

		public unsafe DataChunk AddRange<U>(U[] array) where U : struct
		{
			if (array == null || array.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = array.Length;
			DataChunk result = AddRange(num2);
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			UnsafeUtility.MemCpy(unsafePtr + result.startIndex * num, source, num2 * num);
			UnsafeUtility.ReleaseGCObject(gcHandle);
			return result;
		}

		public DataChunk AddRange<U>(NativeArray<U> udata) where U : struct
		{
			if (!udata.IsCreated || udata.Length == 0)
			{
				return DataChunk.Empty;
			}
			int length = udata.Length;
			DataChunk result = AddRange(length);
			NativeArray<T>.Copy(udata.Reinterpret<T>(), 0, nativeArray, result.startIndex, length);
			return result;
		}

		public unsafe DataChunk AddRangeTypeChange<U>(U[] array) where U : struct
		{
			if (array == null || array.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = UnsafeUtility.SizeOf<U>();
			int num2 = UnsafeUtility.SizeOf<T>();
			int num3 = array.Length * num / num2;
			DataChunk result = AddRange(num3);
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			UnsafeUtility.MemCpy(unsafePtr + result.startIndex * num2, source, num3 * num2);
			UnsafeUtility.ReleaseGCObject(gcHandle);
			return result;
		}

		public unsafe DataChunk AddRangeStride<U>(U[] array) where U : struct
		{
			if (array == null || array.Length == 0)
			{
				return DataChunk.Empty;
			}
			int num = UnsafeUtility.SizeOf<U>();
			int num2 = UnsafeUtility.SizeOf<T>();
			int num3 = array.Length;
			DataChunk result = AddRange(num3);
			ulong gcHandle;
			void* source = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			int elementSize = math.min(num, num2);
			UnsafeUtility.MemCpyStride(unsafePtr + result.startIndex * num2, num2, source, num, elementSize, num3);
			UnsafeUtility.ReleaseGCObject(gcHandle);
			return result;
		}

		public DataChunk Add(T data)
		{
			DataChunk result = AddRange(1);
			nativeArray[result.startIndex] = data;
			return result;
		}

		public DataChunk Expand(DataChunk c, int newDataLength)
		{
			if (!c.IsValid)
			{
				return c;
			}
			if (newDataLength <= c.dataLength)
			{
				return c;
			}
			DataChunk result = AddRange(newDataLength);
			NativeArray<T>.Copy(nativeArray, c.startIndex, nativeArray, result.startIndex, c.dataLength);
			Remove(c);
			return result;
		}

		public DataChunk ExpandAndFill(DataChunk c, int newDataLength, T fillData = default(T), T clearData = default(T))
		{
			if (!c.IsValid)
			{
				return c;
			}
			if (newDataLength <= c.dataLength)
			{
				return c;
			}
			DataChunk result = AddRange(newDataLength, fillData);
			NativeArray<T>.Copy(nativeArray, c.startIndex, nativeArray, result.startIndex, c.dataLength);
			RemoveAndFill(c, clearData);
			return result;
		}

		public T[] ToArray()
		{
			return nativeArray.ToArray();
		}

		public void CopyTo(T[] array)
		{
			NativeArray<T>.Copy(nativeArray, array);
		}

		public void CopyTo(T[] array, int startIndex)
		{
			NativeArray<T>.Copy(nativeArray, startIndex, array, 0, array.Length);
		}

		public void CopyTo<U>(U[] array) where U : struct
		{
			NativeArray<U>.Copy(nativeArray.Reinterpret<U>(), array);
		}

		public void CopyFrom(NativeArray<T> array)
		{
			NativeArray<T>.Copy(array, nativeArray);
		}

		public void CopyFrom(T[] array, int startIndex)
		{
			NativeArray<T>.Copy(array, 0, nativeArray, startIndex, array.Length);
		}

		public void CopyFrom<U>(NativeArray<U> array) where U : struct
		{
			NativeArray<T>.Copy(array.Reinterpret<T>(), nativeArray);
		}

		public void CopyFrom<U>(NativeArray<U> array, int dstIndex, int length) where U : struct
		{
			NativeArray<T>.Copy(array.Reinterpret<T>(), 0, nativeArray, dstIndex, length);
		}

		public unsafe void CopyTypeChange<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = UnsafeUtility.SizeOf<U>();
			int num3 = Length * num / num2;
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			UnsafeUtility.MemCpy(UnsafeUtility.PinGCArrayAndGetDataAddress(array, out var gcHandle), unsafePtr, num3 * num2);
			UnsafeUtility.ReleaseGCObject(gcHandle);
		}

		public unsafe void CopyTypeChangeStride<U>(U[] array) where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int destinationStride = UnsafeUtility.SizeOf<U>();
			int length = Length;
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr();
			ulong gcHandle;
			void* destination = UnsafeUtility.PinGCArrayAndGetDataAddress(array, out gcHandle);
			int elementSize = num;
			UnsafeUtility.MemCpyStride(destination, destinationStride, unsafePtr, num, elementSize, length);
			UnsafeUtility.ReleaseGCObject(gcHandle);
		}

		public void AddEmpty(int dataLength)
		{
			DataChunk chunk = AddRange(dataLength);
			Remove(chunk);
		}

		public void Remove(DataChunk chunk)
		{
			if (!chunk.IsValid)
			{
				return;
			}
			AddEmptyChunk(chunk);
			if (chunk.startIndex + chunk.dataLength != useCount)
			{
				return;
			}
			useCount = 0;
			foreach (DataChunk emptyChunk in emptyChunks)
			{
				useCount = math.max(useCount, emptyChunk.startIndex);
			}
		}

		public void Remove(int index)
		{
			Remove(new DataChunk(index));
		}

		public void RemoveAndFill(DataChunk chunk, T clearData = default(T))
		{
			Remove(chunk);
			Fill(chunk, clearData);
		}

		public void Fill(T fillData = default(T))
		{
			if (IsValid)
			{
				FillInternal(0, nativeArray.Length, fillData);
			}
		}

		public void Fill(DataChunk chunk, T fillData = default(T))
		{
			if (IsValid && chunk.IsValid)
			{
				FillInternal(chunk.startIndex, chunk.dataLength, fillData);
			}
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

		public void Clear()
		{
			emptyChunks.Clear();
			useCount = 0;
			if (IsValid && Length > 0)
			{
				DataChunk item = new DataChunk(0, Length);
				emptyChunks.Add(item);
			}
		}

		public unsafe ref T GetRef(int index)
		{
			T* unsafePtr = (T*)nativeArray.GetUnsafePtr();
			return ref unsafePtr[index];
		}

		public NativeArray<T> GetNativeArray()
		{
			return nativeArray;
		}

		public NativeArray<U> GetNativeArray<U>() where U : struct
		{
			return nativeArray.Reinterpret<U>();
		}

		private DataChunk GetEmptyChunk(int dataLength)
		{
			if (dataLength <= 0)
			{
				return default(DataChunk);
			}
			for (int i = 0; i < emptyChunks.Count; i++)
			{
				DataChunk dataChunk = emptyChunks[i];
				if (dataLength == dataChunk.dataLength)
				{
					emptyChunks.RemoveAtSwapBack(i);
					return dataChunk;
				}
				if (dataLength < dataChunk.dataLength)
				{
					DataChunk result = new DataChunk
					{
						startIndex = dataChunk.startIndex,
						dataLength = dataLength
					};
					dataChunk.startIndex += dataLength;
					dataChunk.dataLength -= dataLength;
					emptyChunks[i] = dataChunk;
					return result;
				}
			}
			return default(DataChunk);
		}

		private void AddEmptyChunk(DataChunk chunk)
		{
			if (!chunk.IsValid)
			{
				return;
			}
			for (int i = 0; i < emptyChunks.Count; i++)
			{
				DataChunk dataChunk = emptyChunks[i];
				if (dataChunk.startIndex + dataChunk.dataLength == chunk.startIndex)
				{
					dataChunk.dataLength += chunk.dataLength;
					chunk = dataChunk;
					emptyChunks.RemoveAtSwapBack(i);
					break;
				}
			}
			for (int j = 0; j < emptyChunks.Count; j++)
			{
				DataChunk dataChunk2 = emptyChunks[j];
				if (dataChunk2.startIndex == chunk.startIndex + chunk.dataLength)
				{
					chunk.dataLength += dataChunk2.dataLength;
					emptyChunks.RemoveAtSwapBack(j);
					break;
				}
			}
			emptyChunks.Add(chunk);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"ExNativeArray Length:{Length} Count:{Count} IsValid:{IsValid}");
			stringBuilder.AppendLine("---- Datas[100] ----");
			if (IsValid)
			{
				for (int i = 0; i < Length && i < 100; i++)
				{
					stringBuilder.AppendLine(nativeArray[i].ToString());
				}
			}
			stringBuilder.AppendLine("---- Empty Chunks ----");
			foreach (DataChunk emptyChunk in emptyChunks)
			{
				stringBuilder.AppendLine(emptyChunk.ToString());
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		public string ToSummary()
		{
			return $"ExNativeArray Length:{Length} Count:{Count} IsValid:{IsValid}";
		}
	}
}
