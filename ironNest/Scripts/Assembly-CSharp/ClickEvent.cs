using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private UnityEvent m_OnClick;

	public void OnPointerClick(PointerEventData eventData)
	{
	}
}
