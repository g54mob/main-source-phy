using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableRect : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
{
	public RectTransform dragTarget;

	public bool limitDragToCanvas = true;

	private Vector3 dragTargetStartPosition;

	private Vector3 dragTargetGoalPosition;

	private Vector3 velocity;

	private Vector2 dragStartCanvasPoint;

	private Canvas canvas;

	private void Awake()
	{
		if (dragTarget == null)
		{
			Debug.LogError("DraggableRect must have 'dragTarget' defined!", this);
			dragTarget = base.transform as RectTransform;
		}
		canvas = GetComponentInParent<Canvas>();
		dragTargetGoalPosition = dragTarget.localPosition;
	}

	private void Update()
	{
		dragTarget.localPosition = Vector3.Lerp(dragTarget.localPosition, dragTargetGoalPosition, 0.5f);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out dragStartCanvasPoint);
		dragTargetStartPosition = dragTarget.localPosition;
	}

	public void OnDrag(PointerEventData eventData)
	{
		bool flag = new Rect(0f, 0f, Screen.width, Screen.height).Contains(eventData.position);
		if ((!limitDragToCanvas || flag) && RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out var localPoint))
		{
			Vector3 vector = localPoint - dragStartCanvasPoint;
			dragTargetGoalPosition = dragTargetStartPosition + vector;
		}
	}
}
