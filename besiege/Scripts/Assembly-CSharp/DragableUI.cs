using UnityEngine;
using UnityEngine.EventSystems;

public class DragableUI : UIBehaviour, IBeginDragHandler, IDragHandler, IEventSystemHandler
{
	public RectTransform dragObject;

	public RectTransform dragArea;

	private Vector2 originalLocalPointerPosition;

	private Vector3 originalPanelLocalPosition;

	private RectTransform dragObjectInternal
	{
		get
		{
			if (dragObject == null)
			{
				return base.transform as RectTransform;
			}
			return dragObject;
		}
	}

	private RectTransform dragAreaInternal
	{
		get
		{
			if (dragArea == null)
			{
				RectTransform rectTransform = base.transform as RectTransform;
				while (rectTransform.parent != null && rectTransform.parent is RectTransform)
				{
					rectTransform = rectTransform.parent as RectTransform;
				}
				return rectTransform;
			}
			return dragArea;
		}
	}

	public void OnBeginDrag(PointerEventData data)
	{
		originalPanelLocalPosition = dragObjectInternal.localPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out originalLocalPointerPosition);
	}

	public void OnDrag(PointerEventData data)
	{
		Vector2 localPoint;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out localPoint))
		{
			Vector3 vector = localPoint - originalLocalPointerPosition;
			dragObjectInternal.localPosition = originalPanelLocalPosition + vector;
		}
		ClampToArea();
	}

	private void ClampToArea()
	{
		Vector3 localPosition = dragObjectInternal.localPosition;
		Vector3 vector = dragAreaInternal.rect.min - dragObjectInternal.rect.min;
		Vector3 vector2 = dragAreaInternal.rect.max - dragObjectInternal.rect.max;
		localPosition.x = Mathf.Clamp(dragObjectInternal.localPosition.x, vector.x, vector2.x);
		localPosition.y = Mathf.Clamp(dragObjectInternal.localPosition.y, vector.y, vector2.y);
		dragObjectInternal.localPosition = localPosition;
	}
}
