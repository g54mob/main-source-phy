using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasURL : MonoBehaviour
{
	[SerializeField]
	private string url;

	[SerializeField]
	private Graphic clickableGraphic;

	private EventTrigger eventTrigger;

	private EventTrigger.Entry pointerClickEntry;

	private void Awake()
	{
		clickableGraphic = GetComponent<Graphic>();
		if (clickableGraphic == null)
		{
			Debug.LogWarning("CanvasURL could not find any Graphic and has no clickable graphic assigned.");
		}
		else if (string.IsNullOrEmpty(url))
		{
			Debug.LogWarning("CanvasURL's url field is not set.");
		}
		else
		{
			Initialize();
		}
	}

	private void Initialize()
	{
		eventTrigger = clickableGraphic.gameObject.GetComponent<EventTrigger>();
		if (eventTrigger == null)
		{
			eventTrigger = clickableGraphic.gameObject.AddComponent<EventTrigger>();
		}
		pointerClickEntry = new EventTrigger.Entry();
		pointerClickEntry.eventID = EventTriggerType.PointerClick;
		pointerClickEntry.callback.AddListener(OnGraphicClick);
		eventTrigger.triggers.Add(pointerClickEntry);
		clickableGraphic.raycastTarget = true;
	}

	private void OnGraphicClick(BaseEventData eventData)
	{
		Application.OpenURL(url);
	}

	private void OnDestroy()
	{
		eventTrigger.triggers.Remove(pointerClickEntry);
		pointerClickEntry = null;
	}
}
