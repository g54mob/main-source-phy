using System.Collections.Generic;
using UnityEngine;

public class StressSamples
{
	public static float m_MaxStressNormalized;

	private const float MAX_SAMPLES_IN_QUEUE = 5f;

	private static Queue<float> m_StressSamples = new Queue<float>();

	public static void Reset()
	{
		m_StressSamples.Clear();
		m_MaxStressNormalized = 0f;
	}

	public static void FixedUpdateManual()
	{
		float maxMomentaryStressNormalizedSmoothed = Main.m_Instance.m_World.maxMomentaryStressNormalizedSmoothed;
		if (!Mathf.Approximately(maxMomentaryStressNormalizedSmoothed, 0f))
		{
			if (maxMomentaryStressNormalizedSmoothed > m_MaxStressNormalized && GameStateSim.IsSimulatingWithoutPassOrFail())
			{
				m_MaxStressNormalized = maxMomentaryStressNormalizedSmoothed;
			}
			m_StressSamples.Enqueue(maxMomentaryStressNormalizedSmoothed);
			if ((float)m_StressSamples.Count > 5f)
			{
				m_StressSamples.Dequeue();
			}
		}
	}

	public static float ComputeAverage()
	{
		float num = 0f;
		foreach (float stressSample in m_StressSamples)
		{
			num += stressSample;
		}
		if (m_StressSamples.Count != 0)
		{
			return Mathf.Clamp01(num / (float)m_StressSamples.Count);
		}
		return 0f;
	}
}
