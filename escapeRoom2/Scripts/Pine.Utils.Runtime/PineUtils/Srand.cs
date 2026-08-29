using System;

namespace PineUtils
{
	public class Srand
	{
		private int inext;

		private int inextp;

		private int[] seedArray;

		private const int MBIG = int.MaxValue;

		private const int MSEED = 161803398;

		private const int MZ = 0;

		public static void init(Srand srand, int seed)
		{
			srand.seedArray = new int[56];
			int num = ((seed == int.MinValue) ? int.MaxValue : Math.Abs(seed));
			int num2 = 161803398 - num;
			srand.seedArray[55] = num2;
			int num3 = 1;
			for (int i = 1; i < 55; i++)
			{
				int num4 = 21 * i % 55;
				srand.seedArray[num4] = num3;
				num3 = num2 - num3;
				if (num3 < 0)
				{
					num3 += int.MaxValue;
				}
				num2 = srand.seedArray[num4];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					srand.seedArray[k] -= srand.seedArray[1 + (k + 30) % 55];
					if (srand.seedArray[k] < 0)
					{
						srand.seedArray[k] += int.MaxValue;
					}
				}
			}
			srand.inext = 0;
			srand.inextp = 21;
		}

		public static int next(Srand srand)
		{
			return internalSample(srand);
		}

		public static int next(Srand srand, int maxValue)
		{
			if (maxValue < 0)
			{
				return 0;
			}
			return (int)(sample(srand) * (double)maxValue);
		}

		public static int next(Srand srand, int minValue, int maxValue)
		{
			if (minValue == maxValue)
			{
				return minValue;
			}
			if (minValue < maxValue)
			{
				long num = (long)maxValue - (long)minValue;
				if (num <= int.MaxValue)
				{
					return (int)(sample(srand) * (double)num) + minValue;
				}
				return (int)((long)(getSampleForLargeRange(srand) * (double)num) + minValue);
			}
			return 0;
		}

		public static double nextDouble(Srand srand)
		{
			return sample(srand);
		}

		private static double sample(Srand srand)
		{
			return (double)internalSample(srand) * 4.656612875245797E-10;
		}

		private static int internalSample(Srand srand)
		{
			int num = srand.inext;
			int num2 = srand.inextp;
			if (++num >= 56)
			{
				num = 1;
			}
			if (++num2 >= 56)
			{
				num2 = 1;
			}
			int num3 = srand.seedArray[num] - srand.seedArray[num2];
			if (num3 == int.MaxValue)
			{
				num3--;
			}
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			srand.seedArray[num] = num3;
			srand.inext = num;
			srand.inextp = num2;
			return num3;
		}

		private static double getSampleForLargeRange(Srand srand)
		{
			int num = internalSample(srand);
			if (internalSample(srand) % 2 == 0)
			{
				num = -num;
			}
			return ((double)num + 2147483646.0) / 4294967293.0;
		}
	}
}
