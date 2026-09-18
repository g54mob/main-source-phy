using System;
using UnityEngine;

public static class FloatSystem
{
	public static float GetRoundedFloat(float value, int maxNumbersAfterPoint = 1)
	{
		if (value == 0f)
		{
			return value;
		}
		int num = 1;
		bool flag = false;
		float num2 = Math.Abs(value);
		while (!flag)
		{
			if (num2 > 1f)
			{
				flag = true;
				continue;
			}
			num2 *= 10f;
			num++;
		}
		return (float)Math.Round(value, Mathf.Min(num, maxNumbersAfterPoint));
	}
}
