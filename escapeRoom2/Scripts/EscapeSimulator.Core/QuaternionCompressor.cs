using UnityEngine;

public static class QuaternionCompressor
{
	private const float MAXIMUM_SECONDARY_VALUE = 0.70710677f;

	private const float PRECISION = 0.001382418f;

	private const int MAX_COMPONENT_INDEX = 1023;

	private const int BITS_PER_COMPONENT = 10;

	public static int compress(Quaternion quaternion)
	{
		int num = 0;
		float num2 = Mathf.Sign(quaternion[0]);
		float num3 = Mathf.Abs(quaternion[0]);
		for (int i = 1; i < 4; i++)
		{
			float num4 = Mathf.Abs(quaternion[i]);
			if (!(num4 <= num3))
			{
				num = i;
				num2 = Mathf.Sign(quaternion[i]);
				num3 = num4;
			}
		}
		int num5 = 0;
		int num6 = 22;
		for (int j = 0; j < 4; j++)
		{
			if (j != num)
			{
				int num7 = (int)((quaternion[j] * num2 + 0.70710677f) / 1.4142135f * 1023f);
				num5 |= num7 << num6;
				num6 -= 10;
			}
		}
		return num5 | num;
	}

	public static Quaternion decompress(int packedQuaternion)
	{
		int num = packedQuaternion & 3;
		Quaternion identity = Quaternion.identity;
		int num2 = 22;
		float num3 = 1f;
		for (int i = 0; i < 4; i++)
		{
			if (i != num)
			{
				int num4 = (packedQuaternion >> num2) & 0x3FF;
				float num5 = (identity[i] = -0.70710677f + (float)num4 * 0.001382418f);
				num3 -= num5 * num5;
				num2 -= 10;
			}
		}
		identity[num] = ((num3 > 0f) ? Mathf.Sqrt(num3) : 0f);
		return identity;
	}
}
