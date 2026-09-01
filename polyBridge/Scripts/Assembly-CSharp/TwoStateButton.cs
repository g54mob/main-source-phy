using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class TwoStateButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public Button m_Button;

	public Image m_Image;

	public Image m_DuckImage;

	public Sprite m_SpriteOff;

	public Sprite m_SpriteOn;

	public ButtonState m_StartState;

	public UnityEvent m_PointerDownEvent;

	[NonSerialized]
	public ToolTipText m_ToolTipText;

	private ButtonState m_State;

	private bool m_Initialized;

	private bool m_Ducked;

	private void Awake()
	{
		m_ToolTipText = GetComponent<ToolTipText>();
		m_State = m_StartState;
		m_Initialized = true;
	}

	private void OnEnable()
	{
		RefreshImage();
	}

	public void Toggle()
	{
		m_State = ((m_State != ButtonState.ON) ? ButtonState.ON : ButtonState.OFF);
		RefreshImage();
	}

	public bool IsOn()
	{
		return m_State == ButtonState.ON;
	}

	public bool IsOff()
	{
		return m_State == ButtonState.OFF;
	}

	public void SetState(bool on)
	{
		TurnOn(on);
	}

	public void TurnOn(bool on)
	{
		if (!m_Initialized)
		{
			m_StartState = (on ? ButtonState.ON : ButtonState.OFF);
			return;
		}
		m_State = (on ? ButtonState.ON : ButtonState.OFF);
		RefreshImage();
	}

	public void Duck()
	{
		if (!m_Ducked && (bool)m_DuckImage)
		{
			m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 0.5f);
			m_DuckImage.gameObject.SetActive(value: true);
			m_Ducked = true;
		}
	}

	public void UnDuck(int budget)
	{
		if (m_Ducked)
		{
			if (budget > 0)
			{
				m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 1f);
			}
			m_DuckImage.gameObject.SetActive(value: false);
			m_Ducked = false;
		}
	}

	public bool IsDucked()
	{
		return m_Ducked;
	}

	public void SetAlpha(float alpha)
	{
		if (!Mathf.Approximately(m_Image.color.a, alpha))
		{
			m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, alpha);
		}
	}

	private void RefreshImage()
	{
		m_Image.sprite = ((m_State == ButtonState.ON) ? m_SpriteOn : m_SpriteOff);
	}

	public void OnPointerDown(PointerEventData ev)
	{
		m_PointerDownEvent.Invoke();
	}
}
