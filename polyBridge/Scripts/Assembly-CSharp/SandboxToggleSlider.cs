using System;
using UnityEngine;
using UnityEngine.UI;

public class SandboxToggleSlider : MonoBehaviour
{
	public Button m_Button;

	public Animator m_Animator;

	public SandboxToggleSliderHandle m_ToggleSliderHandle;

	public Image m_HandleImage;

	public Image m_BackgroundImage;

	public ToggleSliderState m_InitialState;

	private ToggleSliderState m_State;

	private Action m_Callback;

	private void Awake()
	{
		m_Button.onClick.AddListener(OnButton);
		m_ToggleSliderHandle.SetCallback(AnimComplete);
		SetStateImmediate(m_InitialState);
	}

	public void Update()
	{
		m_BackgroundImage.color = new Color(m_BackgroundImage.color.r, m_BackgroundImage.color.g, m_BackgroundImage.color.b, m_HandleImage.color.a);
	}

	public ToggleSliderState GetState()
	{
		return m_State;
	}

	public void SetCallback(Action callback)
	{
		m_Callback = callback;
	}

	public void AnimComplete()
	{
		if (m_State == ToggleSliderState.TRANSITION_ON)
		{
			m_State = ToggleSliderState.ON;
		}
		else if (m_State == ToggleSliderState.TRANSITION_OFF)
		{
			m_State = ToggleSliderState.OFF;
		}
		m_Callback?.Invoke();
	}

	private void OnButton()
	{
		if (m_State != ToggleSliderState.TRANSITION_OFF && m_State != ToggleSliderState.TRANSITION_ON)
		{
			if (m_State == ToggleSliderState.OFF)
			{
				SetStateAnimated(ToggleSliderState.TRANSITION_ON);
			}
			else
			{
				SetStateAnimated(ToggleSliderState.TRANSITION_OFF);
			}
		}
	}

	public void SetStateAnimated(ToggleSliderState state)
	{
		if (state != m_State)
		{
			m_State = state;
			m_Animator.SetTrigger(GetTriggerName(state));
		}
	}

	public void SetStateImmediate(ToggleSliderState state)
	{
		m_State = state;
		m_Animator.CrossFade(GetStateName(state), 0f, 0, 1f);
	}

	private string GetStateName(ToggleSliderState state)
	{
		if (state == ToggleSliderState.OFF)
		{
			return "Off";
		}
		return "On";
	}

	private string GetTriggerName(ToggleSliderState state)
	{
		if (state == ToggleSliderState.OFF || state == ToggleSliderState.TRANSITION_OFF)
		{
			return "off";
		}
		return "on";
	}
}
