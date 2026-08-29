using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace MagicaCloth2
{
	public class ExProcessingList<T> : IDisposable, IValid where T : struct
	{
		public NativeReference<int> Counter;

		public NativeArray<T> Buffer;

		public void Dispose()
		{
			if (Counter.IsCreated)
			{
				Counter.Dispose();
			}
			if (Buffer.IsCreated)
			{
				Buffer.Dispose();
			}
		}

		public bool IsValid()
		{
			return Counter.IsCreated;
		}

		public ExProcessingList()
		{
			Counter = new NativeReference<int>(Allocator.Persistent);
		}

		public void UpdateBuffer(int capacity)
		{
			if (!Buffer.IsCreated || Buffer.Length < capacity)
			{
				if (Buffer.IsCreated)
				{
					Buffer.Dispose();
				}
				Buffer = new NativeArray<T>(capacity, Allocator.Persistent);
			}
		}

		public unsafe int* GetJobSchedulePtr()
		{
			return Counter.GetUnsafePtrWithoutChecks();
		}

		public override string ToString()
		{
			int num = (Counter.IsCreated ? Counter.Value : 0);
			int num2 = (Buffer.IsCreated ? Buffer.Length : 0);
			return $"ExProcessingList BufferLength:{num2} Counter:{num}";
		}
	}
}
