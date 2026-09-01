using System.Collections.Generic;
using UnityEngine;

public class FloatPacker
{
	public static int[] PackFloatsToInts(float[] floats, int precisionBits)
	{
		if (precisionBits != 1 && precisionBits != 2 && precisionBits != 4 && precisionBits != 8 && precisionBits != 16 && precisionBits != 32)
		{
			Debug.LogError("Precision must be one of 1, 2, 4, 8, 16, or 32 bits.");
			return null;
		}
		int num = (1 << precisionBits) - 1;
		List<int> list = new List<int>();
		int num2 = 0;
		int num3 = 0;
		foreach (float num4 in floats)
		{
			if (num4 < 0f || num4 > 1f)
			{
				Debug.LogError("All floats must be in the range [0, 1].");
				return null;
			}
			uint num5 = (uint)(num4 * (float)num);
			num2 |= (int)(num5 << num3);
			num3 += precisionBits;
			if (num3 >= 32)
			{
				list.Add(num2);
				num2 = 0;
				num3 = 0;
			}
		}
		if (num3 > 0)
		{
			list.Add(num2);
		}
		return list.ToArray();
	}

	public static float[] UnpackIntsToFloats(int[] packedInts, int precisionBits)
	{
		if (precisionBits != 1 && precisionBits != 2 && precisionBits != 4 && precisionBits != 8 && precisionBits != 16 && precisionBits != 32)
		{
			Debug.LogError("Precision must be one of 1, 2, 4, 8, 16, or 32 bits.");
			return null;
		}
		int num = (1 << precisionBits) - 1;
		int num2 = 32 / precisionBits;
		List<float> list = new List<float>();
		foreach (int num3 in packedInts)
		{
			int num4 = 0;
			for (int j = 0; j < num2; j++)
			{
				uint num5 = (uint)((num3 >> num4) & num);
				list.Add((float)num5 / (float)num);
				num4 += precisionBits;
				if (num4 >= 32)
				{
					break;
				}
			}
		}
		return list.ToArray();
	}
}
