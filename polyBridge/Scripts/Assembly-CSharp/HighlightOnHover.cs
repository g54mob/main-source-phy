using UnityEngine;
using UnityEngine.UI;

public class HighlightOnHover : MonoBehaviour
{
	public Button m_Button;

	public Image m_Background;

	public Color m_NormalColor;

	public Color m_HoverColor;

	public PointerEvents m_PointerEvents;

	private void Awake()
	{
		m_Button = GetComponent<Button>();
	}

	private void OnDisable()
	{
		m_Background.color = m_NormalColor;
	}

	private void Update()
	{
		m_Background.color = ((m_PointerEvents.m_IsHovering && m_Button.interactable) ? m_HoverColor : m_NormalColor);
	}

	public bool IsHighlighted()
	{
		return m_Background.color == m_HoverColor;
	}

	public void ForceHighlight()
	{
		m_Background.color = m_HoverColor;
	}
}
