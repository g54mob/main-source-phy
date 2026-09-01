using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_LiveStress : MonoBehaviour
{
	public RectTransform m_RootRectTransform;

	public TextMeshProUGUI m_StressLabel;

	public Image m_StressFill;

	private float m_PeakStress;

	private float m_HoldPeakTimer;

	private readonly float HOLD_PEAK_DURATION_SECONDS = 0.5f;

	private void OnEnable()
	{
		m_PeakStress = 0f;
		m_HoldPeakTimer = 0f;
		m_StressFill.color = Utils.GetStressColor(0f);
	}

	private void Update()
	{
		float num = StressSamples.ComputeAverage();
		if (num > m_PeakStress)
		{
			m_PeakStress = num;
			m_HoldPeakTimer = HOLD_PEAK_DURATION_SECONDS;
		}
		else
		{
			m_HoldPeakTimer -= Time.unscaledDeltaTime;
		}
		if (m_HoldPeakTimer <= 0f)
		{
			m_StressLabel.text = Utils.FormatPercentage(num);
			m_StressFill.fillAmount = num;
			m_StressFill.color = Utils.GetStressColor(num);
		}
		else
		{
			m_StressLabel.text = Utils.FormatPercentage(m_PeakStress);
			m_StressFill.fillAmount = m_PeakStress;
			m_StressFill.color = Utils.GetStressColor(m_PeakStress);
		}
	}

	public void UpdateForCurrentDevice()
	{
		m_RootRectTransform.anchoredPosition = new Vector2(0f, (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? GamepadLegend.HEIGHT : 0);
	}
}
