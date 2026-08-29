using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerPointerEnterExit : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public List<EventTrigger.Entry> triggers;

	private void execute(EventTriggerType id, BaseEventData eventData)
	{
		for (int i = 0; i < triggers.Count; i++)
		{
			EventTrigger.Entry entry = triggers[i];
			if (entry.eventID == id && entry.callback != null)
			{
				entry.callback.Invoke(eventData);
			}
		}
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		execute(EventTriggerType.PointerEnter, eventData);
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		execute(EventTriggerType.PointerExit, eventData);
	}
}
