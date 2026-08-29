using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FaceAnimPicker : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public RectTransform rectTransform;

	[NonSerialized]
	public bool isDragging;

	[NonSerialized]
	public bool forceUpdate;

	private Vector2 offset;

	[NonSerialized]
	public float maxOffset = 70f;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		forceUpdate = true;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out var localPoint);
		offset = rectTransform.anchoredPosition - localPoint;
		isDragging = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isDragging = false;
	}

	private void Update()
	{
		if (Controller.controllerModeChanged)
		{
			isDragging = false;
		}
		if (Controller.isActive())
		{
			changeWithController();
		}
		else
		{
			changeWithMouse();
		}
		void changeWithController()
		{
			Vector2 axis = Controller.getAxis(ControllerAxisActionType.UIMoveAndRotate);
			isDragging = axis != Vector2.zero;
			if (isDragging)
			{
				Vector2 anchoredPosition = rectTransform.anchoredPosition + offset + axis * (Time.deltaTime * 100f);
				anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0f - maxOffset, maxOffset);
				anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0f - maxOffset, maxOffset);
				rectTransform.anchoredPosition = anchoredPosition;
			}
		}
		void changeWithMouse()
		{
			if (isDragging)
			{
				RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, Input.mousePosition, null, out var localPoint);
				Vector2 anchoredPosition = localPoint + offset;
				anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0f - maxOffset, maxOffset);
				anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0f - maxOffset, maxOffset);
				rectTransform.anchoredPosition = anchoredPosition;
			}
		}
	}

	private void OnDisable()
	{
		isDragging = false;
	}
}
