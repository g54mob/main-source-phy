using UnityEngine;
using UnityEngine.UI;

public class PulseIcon
{
	public PulseState m_PulseState;

	public float m_PulseTimeElapsed;

	private Image m_Image;

	private Color m_StartColor;

	private Color m_EndColor;

	private float m_StartScale;

	private float m_EndScale;

	public PulseIcon(Image image, float startScale, float endScale, Color startColor, Color endColor)
	{
		m_Image = image;
		m_StartScale = startScale;
		m_EndScale = endScale;
		m_StartColor = startColor;
		m_EndColor = endColor;
		m_PulseState = PulseState.RAMP_UP;
		m_PulseTimeElapsed = 0f;
	}

	public void UpdateManual()
	{
		switch (m_PulseState)
		{
		case PulseState.RAMP_UP:
			m_PulseTimeElapsed += Time.unscaledDeltaTime;
			Animate(m_PulseTimeElapsed, PulseIcons.PULSE_RAMP_SECONDS, m_StartScale, m_EndScale, m_StartColor, m_EndColor);
			if (m_PulseTimeElapsed > PulseIcons.PULSE_RAMP_SECONDS)
			{
				m_PulseTimeElapsed = 0f;
				m_PulseState = PulseState.RAMP_DOWN;
			}
			break;
		case PulseState.RAMP_DOWN:
			m_PulseTimeElapsed += Time.unscaledDeltaTime;
			Animate(m_PulseTimeElapsed, PulseIcons.PULSE_RAMP_SECONDS, m_EndScale, m_StartScale, m_EndColor, m_StartColor);
			if (m_PulseTimeElapsed > PulseIcons.PULSE_RAMP_SECONDS)
			{
				m_PulseState = PulseState.NONE;
			}
			break;
		case PulseState.NONE:
			if (m_Image != null)
			{
				m_Image.transform.localScale = new Vector3(m_StartScale, m_StartScale, 1f);
				m_Image.color = m_StartColor;
			}
			break;
		}
	}

	private void Animate(float elapsedSeconds, float maxSeconds, float startScale, float endScale, Color startColor, Color endColor)
	{
		float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(elapsedSeconds / maxSeconds));
		float num = Mathf.Lerp(startScale, endScale, t);
		if (m_Image != null)
		{
			m_Image.transform.localScale = new Vector3(num, num, 1f);
			m_Image.color = Color.Lerp(startColor, endColor, t);
		}
	}
}
