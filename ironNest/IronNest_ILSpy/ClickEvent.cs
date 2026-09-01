using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	private UnityEvent m_OnClick;

	public void OnPointerClick(PointerEventData eventData)
	{
		m_OnClick.Invoke();
	}
}
