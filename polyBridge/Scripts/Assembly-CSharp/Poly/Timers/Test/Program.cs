using System;
using System.Diagnostics;

namespace Poly.Timers.Test
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			int num = 1000000;
			int num2 = 0;
			long[] array = new long[num];
			long[] array2 = new long[num];
			_ = HighResolutionDateTime.UtcNow;
			long timestamp = Stopwatch.GetTimestamp();
			long now = PT.Now;
			timestamp = Stopwatch.GetTimestamp();
			now = PT.Now;
			for (int i = 0; i < num; i++)
			{
				long timestamp2 = Stopwatch.GetTimestamp();
				long now2 = PT.Now;
				array[num2] = timestamp2;
				array2[num2] = now2;
				num2++;
			}
			array2[num2 - 1] = PT.Now;
			for (int j = num2 - 100; j < num2; j++)
			{
				Console.WriteLine("SW Ticks: {0}", array[j] - timestamp);
				Console.WriteLine("PT Ticks: {0}", array2[j] - now);
				Console.WriteLine("");
			}
			Console.ReadLine();
		}
	}
}
