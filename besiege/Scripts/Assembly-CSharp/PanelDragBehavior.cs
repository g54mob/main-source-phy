using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class PanelDragBehavior : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	private RectTransform rect;

	public void Awake()
	{
		rect = GetComponent<RectTransform>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		rect.position += new Vector3(eventData.delta.x, eventData.delta.y, 0f);
	}
}
