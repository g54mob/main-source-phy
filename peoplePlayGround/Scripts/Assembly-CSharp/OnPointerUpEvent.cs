using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnPointerUpEvent : MonoBehaviour, IPointerUpHandler, IEventSystemHandler
{
	public UnityEvent onPointerUp = new UnityEvent();

	public void OnPointerUp(PointerEventData eventData)
	{
		onPointerUp?.Invoke();
	}
}
