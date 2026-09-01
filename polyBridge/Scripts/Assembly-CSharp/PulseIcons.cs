using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseIcons
{
	public static float PULSE_RAMP_SECONDS = 0.125f;

	public static float PULSE_MAX_SCALE = 1.2f;

	private static List<PulseIcon> m_Icons = new List<PulseIcon>();

	public static void UpdateManual()
	{
		foreach (PulseIcon icon in m_Icons)
		{
			icon.UpdateManual();
		}
		for (int num = m_Icons.Count - 1; num >= 0; num--)
		{
			if (m_Icons[num].m_PulseState == PulseState.NONE)
			{
				m_Icons.RemoveAt(num);
			}
		}
	}

	public static void Pulse(Image image, float startScale, float endScale, Color startColor, Color endColor)
	{
		PulseIcon item = new PulseIcon(image, startScale, endScale, startColor, endColor);
		m_Icons.Add(item);
	}
}
