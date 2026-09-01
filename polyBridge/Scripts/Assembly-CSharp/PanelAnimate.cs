using System;
using UnityEngine;

public class PanelAnimate : MonoBehaviour
{
	public RectTransform m_RectTransform;

	public float m_TransitionTimeSeconds;

	public Vector2 m_OnPos;

	public Vector2 m_OffPos;

	private PanelAnimateState m_State;

	private Vector2 m_Start;

	private Vector2 m_Target;

	private float m_StartTime;

	private Action m_Callback;

	private void Update()
	{
		if (m_State == PanelAnimateState.ANIMATING_ON || m_State == PanelAnimateState.ANIMATING_OFF)
		{
			float num = Mathf.Clamp01((Time.realtimeSinceStartup - m_StartTime) / m_TransitionTimeSeconds);
			m_RectTransform.anchoredPosition = new Vector2(Mathf.SmoothStep(m_Start.x, m_Target.x, num), Mathf.SmoothStep(m_Start.y, m_Target.y, num));
			if (Mathf.Approximately(num, 1f))
			{
				m_State = ((m_State != PanelAnimateState.ANIMATING_ON) ? PanelAnimateState.OFF : PanelAnimateState.ON);
				m_Callback?.Invoke();
			}
		}
	}

	public void Play(bool on, Action callback)
	{
		m_Start = new Vector2(m_RectTransform.anchoredPosition.x, m_RectTransform.anchoredPosition.y);
		m_Target = (on ? m_OnPos : m_OffPos);
		m_State = (on ? PanelAnimateState.ANIMATING_ON : PanelAnimateState.ANIMATING_OFF);
		m_StartTime = Time.realtimeSinceStartup;
		m_Callback = callback;
	}

	public void ForceState(PanelAnimateState state)
	{
		switch (state)
		{
		case PanelAnimateState.ON:
			m_RectTransform.anchoredPosition = m_OnPos;
			m_State = PanelAnimateState.ON;
			break;
		case PanelAnimateState.OFF:
			m_RectTransform.anchoredPosition = m_OffPos;
			m_State = PanelAnimateState.OFF;
			break;
		default:
			Debug.LogWarning("PanelAnimate::ForceState() does not support the state " + state);
			break;
		}
	}

	public PanelAnimateState GetState()
	{
		return m_State;
	}
}
