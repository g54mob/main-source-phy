using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BridgeSimSpeedSlider : MonoBehaviour
{
	public Slider m_Slider;

	public TextMeshProUGUI m_HandelLabel;

	private bool m_SkipCallback;

	private bool m_Dragging;

	private bool m_UpdateHandleLabel;

	private void Start()
	{
		m_UpdateHandleLabel = true;
		m_Slider.onValueChanged.AddListener(OnSliderChanged);
	}

	private void Update()
	{
		m_Slider.minValue = 1f;
		m_Slider.maxValue = BridgeSimSpeed.m_SimulationSpeeds.Count;
		if (!GameInput.GetMouseButtonIsDown(0) && m_Dragging)
		{
			m_Dragging = false;
		}
		if (m_UpdateHandleLabel)
		{
			UpdateHandleLabel();
			m_UpdateHandleLabel = false;
		}
	}

	private void OnEnable()
	{
		m_Dragging = false;
		m_SkipCallback = false;
	}

	private void OnDisable()
	{
		m_Dragging = false;
	}

	public bool IsDragging()
	{
		return m_Dragging;
	}

	public void SetValue(int value)
	{
		m_SkipCallback = true;
		m_Slider.value = value;
		m_SkipCallback = false;
		m_UpdateHandleLabel = true;
	}

	private void OnSliderChanged(float value)
	{
		if (m_SkipCallback)
		{
			m_SkipCallback = false;
			return;
		}
		if (!m_Dragging)
		{
			m_Dragging = true;
		}
		int num = Mathf.FloorToInt(m_Slider.value) - 1;
		if (num >= 0 && num < BridgeSimSpeed.m_SimulationSpeeds.Count)
		{
			BridgeSimSpeed.m_SimulationSpeedIndex = num;
			GameUI.m_Instance.m_TopBar.ApplyChangesAfterSimulationSpeedChange();
		}
		m_UpdateHandleLabel = true;
	}

	public void UpdateHandleLabel()
	{
		float timeScaleForDisplay = BridgeSimSpeed.GetTimeScaleForDisplay();
		if (Mathf.Approximately(timeScaleForDisplay, Mathf.FloorToInt(timeScaleForDisplay)))
		{
			m_HandelLabel.text = $"{Mathf.FloorToInt(timeScaleForDisplay)}x";
		}
		else
		{
			m_HandelLabel.text = $"{timeScaleForDisplay:0.0}x";
		}
	}
}
