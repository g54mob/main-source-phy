using System;
using UnityEngine;
using UnityEngine.UI;

public class SandboxTapeSlider : MonoBehaviour
{
	public Slider m_Slider;

	public SandboxInputField m_SandboxInputField;

	private Action<float> m_Callback;

	private float m_MinValue;

	private float m_MaxValue;

	private float m_TickValue;

	private float m_ValueWhenStartDragging;

	private bool m_Dragging;

	private bool m_SkipCallback;

	private int m_NumTicks;

	private void Start()
	{
		m_Slider.onValueChanged.AddListener(OnSliderChanged);
	}

	private void OnEnable()
	{
		m_SkipCallback = false;
	}

	private void OnDisable()
	{
		m_SkipCallback = false;
	}

	private void Update()
	{
		if (!GameInput.GetMouseButtonIsDown(0) && m_Dragging)
		{
			m_Dragging = false;
			if (!Mathf.Approximately(m_ValueWhenStartDragging, m_Slider.value) && (GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.DECOR))
			{
				SandboxUndo.SnapShot();
			}
		}
	}

	public void SetCallback(Action<float> callback)
	{
		m_Callback = callback;
	}

	public void SetRange(float min, float max, float tick)
	{
		m_MinValue = min;
		m_MaxValue = max;
		m_TickValue = tick;
		m_NumTicks = Mathf.RoundToInt((max - min) / tick);
		m_Slider.wholeNumbers = true;
		m_Slider.minValue = 0f;
		m_Slider.maxValue = m_NumTicks;
	}

	public void SetValue(float value)
	{
		m_SkipCallback = true;
		if (Mathf.Approximately(m_MaxValue - m_MinValue, 0f))
		{
			m_Slider.value = m_MinValue;
			return;
		}
		float num = Mathf.Clamp01((value - m_MinValue) / (m_MaxValue - m_MinValue));
		m_Slider.value = num * (float)m_NumTicks;
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
			m_ValueWhenStartDragging = m_Slider.value;
			m_Dragging = true;
		}
		float obj = m_MinValue + Mathf.Clamp01(m_Slider.value / (float)m_NumTicks) * (m_MaxValue - m_MinValue);
		m_Callback?.Invoke(obj);
	}
}
