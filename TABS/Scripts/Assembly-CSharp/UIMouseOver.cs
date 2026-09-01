using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseOver : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	private bool m_isMouseOver;

	private bool m_isPressed;

	public bool IsMouseOver => m_isMouseOver;

	public bool mIsPressed => m_isPressed;

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_isMouseOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_isMouseOver = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		m_isPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		m_isPressed = false;
	}
}
