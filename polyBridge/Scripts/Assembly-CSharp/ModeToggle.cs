using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModeToggle : MonoBehaviour
{
	[Header("On")]
	public Image m_OnIcon;

	public TextMeshProUGUI m_OnText;

	public Color m_OnColor;

	public Color m_HandleOnColor;

	[Header("Off")]
	public Image m_OffIcon;

	public TextMeshProUGUI m_OffText;

	public Color m_OffColor;

	public Color m_HandleOffColor;

	[Header("Controls")]
	public Button m_Button;

	public Animator m_Animator;

	public SandboxToggleSliderHandle m_ToggleSliderHandle;

	private ToggleSliderState m_State;

	private Action m_Callback;

	private float m_ElapsedSeconds;

	private float m_StartAnchoredX;

	private const float HANDLE_X_OFFSET = 37.45f;

	private readonly float TRANSITION_TIME_SECONDS = 0.1f;

	private void Awake()
	{
		m_Button.onClick.AddListener(OnButton);
	}

	public void UpdateManual()
	{
		m_ElapsedSeconds += Time.unscaledDeltaTime;
		float num = Mathf.Clamp01(m_ElapsedSeconds / TRANSITION_TIME_SECONDS);
		if (m_State == ToggleSliderState.TRANSITION_OFF)
		{
			float x = Mathf.Lerp(m_StartAnchoredX, -37.45f, Mathf.SmoothStep(0f, 1f, num));
			m_ToggleSliderHandle.m_RectTransform.anchoredPosition = new Vector2(x, 0f);
			if (Mathf.Approximately(num, 1f))
			{
				SetStateImmediate(ToggleSliderState.OFF);
				m_Callback?.Invoke();
			}
		}
		else if (m_State == ToggleSliderState.TRANSITION_ON)
		{
			float x2 = Mathf.Lerp(m_StartAnchoredX, 37.45f, Mathf.SmoothStep(0f, 1f, num));
			m_ToggleSliderHandle.m_RectTransform.anchoredPosition = new Vector2(x2, 0f);
			if (Mathf.Approximately(num, 1f))
			{
				SetStateImmediate(ToggleSliderState.ON);
				m_Callback?.Invoke();
			}
		}
	}

	public ToggleSliderState GetState()
	{
		return m_State;
	}

	public void SetCallback(Action callback)
	{
		m_Callback = callback;
	}

	public void OnButton()
	{
		if (!CameraInterpolate.IsActive() && m_State != ToggleSliderState.TRANSITION_OFF && m_State != ToggleSliderState.TRANSITION_ON)
		{
			if (m_State == ToggleSliderState.OFF)
			{
				SetStateAnimated(ToggleSliderState.TRANSITION_ON);
				InterfaceAudio.Play("ui_menubar_gen_on");
			}
			else
			{
				SetStateAnimated(ToggleSliderState.TRANSITION_OFF);
				InterfaceAudio.Play("ui_menubar_gen_off");
			}
		}
	}

	public void Toggle()
	{
		OnButton();
	}

	public void SetStateAnimated(ToggleSliderState state)
	{
		if (state != m_State)
		{
			m_State = state;
			m_ElapsedSeconds = 0f;
			m_StartAnchoredX = m_ToggleSliderHandle.m_RectTransform.anchoredPosition.x;
		}
	}

	public void SetStateImmediate(ToggleSliderState state)
	{
		m_State = state;
		if (m_State == ToggleSliderState.ON)
		{
			m_State = ToggleSliderState.ON;
			m_OnIcon.color = m_OffColor;
			m_OnText.color = m_OffColor;
			m_OffIcon.color = m_OffColor;
			m_OffText.color = m_OffColor;
			m_ToggleSliderHandle.m_HandleImage.color = m_HandleOnColor;
			m_ToggleSliderHandle.m_RectTransform.anchoredPosition = new Vector2(37.45f, 0f);
		}
		else if (m_State == ToggleSliderState.OFF)
		{
			m_State = ToggleSliderState.OFF;
			m_OnIcon.color = m_OnColor;
			m_OnText.color = m_OnColor;
			m_OffIcon.color = m_OnColor;
			m_OffText.color = m_OnColor;
			m_ToggleSliderHandle.m_HandleImage.color = m_HandleOffColor;
			m_ToggleSliderHandle.m_RectTransform.anchoredPosition = new Vector2(-37.45f, 0f);
		}
		else
		{
			Debug.Log("Unexpected state in ModeToggle.SetStateImmediate: " + m_State);
		}
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
