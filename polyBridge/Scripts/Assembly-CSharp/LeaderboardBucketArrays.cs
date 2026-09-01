using UnityEngine;

public class LeaderboardBucketArrays
{
	public static int BUCKETS_PER_ARRAY = 20;

	public int[] m_Start = new int[BUCKETS_PER_ARRAY];

	public int[] m_End = new int[BUCKETS_PER_ARRAY];

	public int[] m_Count = new int[BUCKETS_PER_ARRAY];

	public int ComputePercentile(int score)
	{
		int num = 0;
		int[] count = m_Count;
		foreach (int num2 in count)
		{
			num += num2;
		}
		int num3 = 0;
		for (int j = 0; j < BUCKETS_PER_ARRAY; j++)
		{
			if (score >= m_End[j])
			{
				num3 += m_Count[j];
				continue;
			}
			if (m_End[j] - m_Start[j] > 0)
			{
				float num4 = (float)(score - m_Start[j]) / (float)(m_End[j] - m_Start[j]);
				num3 += Mathf.FloorToInt((float)m_Count[j] * num4);
			}
			break;
		}
		int num5 = Mathf.Clamp(num - num3, 0, num);
		return Mathf.RoundToInt(100f * (float)num5 / (float)num);
	}
}
