using System;

namespace TFBGames
{
	public class MathHelpers
	{
		public static int Wrap(int value, int min, int max)
		{
			if (max < min)
			{
				throw new ArgumentException("Max must be greater than min");
			}
			if (max == min)
			{
				return min;
			}
			if (value >= min && value <= max)
			{
				return value;
			}
			int num = max - min + 1;
			int num2 = (value - min) % num;
			if (num2 < 0)
			{
				num2 += num;
			}
			return num2 + min;
		}
	}
}
