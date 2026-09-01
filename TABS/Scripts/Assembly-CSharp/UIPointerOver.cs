using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointerOver : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	private bool m_pointerOver;

	public bool PointerOver => m_pointerOver;

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_pointerOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_pointerOver = false;
	}
}
