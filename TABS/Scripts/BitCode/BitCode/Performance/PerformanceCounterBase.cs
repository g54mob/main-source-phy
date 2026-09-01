using System;
using BitCode.Collections;

namespace BitCode.Performance
{
	internal abstract class PerformanceCounterBase<T, TAverage> : IPerformanceCounter<T, TAverage>, IPerformanceCounter where T : IEquatable<T>, IComparable<T>
	{
		protected readonly RingBuffer<T> Samples;

		public int Count => Samples.Count;

		public T Current => Samples.Tail;

		public T Max { get; protected set; }

		public T Min { get; protected set; }

		public TAverage Average { get; protected set; }

		protected PerformanceCounterBase(int historySize)
		{
			while (true)
			{
				int num = -2026034940;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1748882006)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						goto IL_0028;
					case 1u:
						return;
					}
					break;
					IL_0028:
					Samples = new RingBuffer<T>(historySize);
					num = ((int)num2 * -244766340) ^ 0x234B8EEF;
				}
			}
		}

		public abstract void Tick();

		protected abstract bool GetSample(out T retrievedSample);
	}
}
