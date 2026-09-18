using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixScrollRect : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
	public ScrollRect MainScroll;

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (MainScroll != null)
		{
			MainScroll.OnBeginDrag(eventData);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (MainScroll != null)
		{
			MainScroll.OnDrag(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (MainScroll != null)
		{
			MainScroll.OnEndDrag(eventData);
		}
	}

	public void OnScroll(PointerEventData data)
	{
		if (MainScroll != null)
		{
			MainScroll.OnScroll(data);
		}
	}
}
