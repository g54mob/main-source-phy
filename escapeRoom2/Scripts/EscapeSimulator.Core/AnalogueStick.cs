using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnalogueStick : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
	public RectTransform moving;

	private Vector2 beginDragPosition;

	private Vector2 originalMoving;

	[NonSerialized]
	public bool dragging;

	[NonSerialized]
	public Vector2 value;

	private const float offset = 50f;

	private void Start()
	{
		originalMoving = moving.anchoredPosition;
		dragging = false;
	}

	public void OnPointerDown(PointerEventData data)
	{
		dragging = true;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(moving.parent.GetComponent<RectTransform>(), data.position, moving.GetComponentInParent<Canvas>().worldCamera, out beginDragPosition);
	}

	public void OnPointerUp(PointerEventData data)
	{
		dragging = false;
		value = Vector2.zero;
	}

	public void OnBeginDrag(PointerEventData data)
	{
	}

	private Vector2 calculateChange(PointerEventData data)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(moving.parent.GetComponent<RectTransform>(), data.position, moving.GetComponentInParent<Canvas>().worldCamera, out var localPoint);
		return Vector2.ClampMagnitude(localPoint - beginDragPosition, 50f);
	}

	public void OnDrag(PointerEventData data)
	{
		Vector2 vector = calculateChange(data);
		moving.anchoredPosition = originalMoving + vector;
		value = vector / 50f;
	}

	public void OnEndDrag(PointerEventData data)
	{
	}

	private void Update()
	{
		if (!dragging)
		{
			moving.anchoredPosition = Vector2.MoveTowards(moving.anchoredPosition, originalMoving, Time.deltaTime * 50f * 4f);
		}
	}
}
