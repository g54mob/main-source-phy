using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PineUITrigger : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public List<EventTrigger.Entry> triggers = new List<EventTrigger.Entry>();

	public void OnPointerClick(PointerEventData data)
	{
		dispatch(EventTriggerType.PointerClick, data);
	}

	public void OnPointerEnter(PointerEventData data)
	{
		dispatch(EventTriggerType.PointerEnter, data);
	}

	public void OnPointerExit(PointerEventData data)
	{
		dispatch(EventTriggerType.PointerExit, data);
	}

	public void OnPointerDown(PointerEventData data)
	{
		dispatch(EventTriggerType.PointerDown, data);
	}

	public void OnPointerUp(PointerEventData data)
	{
		dispatch(EventTriggerType.PointerUp, data);
	}

	public void OnSelect(BaseEventData data)
	{
		dispatch(EventTriggerType.Select, data);
	}

	public void OnBeginDrag(PointerEventData data)
	{
		dispatch(EventTriggerType.BeginDrag, data);
	}

	public void OnDrag(PointerEventData data)
	{
		dispatch(EventTriggerType.Drag, data);
	}

	public void OnEndDrag(PointerEventData data)
	{
		dispatch(EventTriggerType.EndDrag, data);
	}

	public void OnScroll(PointerEventData data)
	{
		dispatch(EventTriggerType.Scroll, data);
	}

	private void dispatch(EventTriggerType type, BaseEventData data)
	{
		foreach (EventTrigger.Entry trigger in triggers)
		{
			if (trigger.eventID == type)
			{
				trigger.callback.Invoke(data);
			}
		}
	}
}
