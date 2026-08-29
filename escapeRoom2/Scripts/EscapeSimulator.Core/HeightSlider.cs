using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeightSlider : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
	public RectTransform moving;

	public GameObject iconCrouch;

	public GameObject iconStand;

	public GameObject iconStretch;

	public GameObject iconBaseCrouch;

	public GameObject iconBaseStand;

	public GameObject iconBaseStretch;

	private Vector2 beginDragPosition;

	private Vector2 originalMoving;

	[NonSerialized]
	public bool dragging;

	[NonSerialized]
	public int value = 2;

	[NonSerialized]
	public float visualValue = 2f;

	private const float offset = 50f;

	private void Start()
	{
		originalMoving = moving.anchoredPosition;
		dragging = false;
		iconCrouch.gameObject.SetActive(value == 0);
		iconStand.gameObject.SetActive(value == 1);
		iconStretch.gameObject.SetActive(value == 2);
	}

	public void OnPointerDown(PointerEventData data)
	{
		dragging = true;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(moving.parent.GetComponent<RectTransform>(), data.position, moving.GetComponentInParent<Canvas>().worldCamera, out beginDragPosition);
	}

	public void OnPointerUp(PointerEventData data)
	{
		dragging = false;
		float num = calculateChange(data).y / 50f;
		if (num > 0.6f)
		{
			if (value < 2)
			{
				value++;
			}
		}
		else if (num < -0.6f && value > 0)
		{
			value--;
		}
	}

	public void OnBeginDrag(PointerEventData data)
	{
	}

	private Vector2 calculateChange(PointerEventData data)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(moving.parent.GetComponent<RectTransform>(), data.position, moving.GetComponentInParent<Canvas>().worldCamera, out var localPoint);
		Vector2 result = localPoint - beginDragPosition;
		result.x = 0f;
		result.y = Mathf.Clamp(result.y, -50f, 50f);
		return result;
	}

	public void OnDrag(PointerEventData data)
	{
		Vector2 vector = calculateChange(data);
		moving.anchoredPosition = originalMoving + vector;
		float num = Mathf.Abs(vector.y / 50f);
		moving.GetComponent<CanvasGroup>().alpha = num;
		int num2 = ((vector.y > 0f) ? (value + 1) : (value - 1));
		if (Mathf.Abs(vector.y) < 0.3f)
		{
			num2 = value;
		}
		num2 = Mathf.Clamp(num2, 0, 2);
		iconCrouch.gameObject.SetActive(num2 == 0);
		iconStand.gameObject.SetActive(num2 == 1);
		iconStretch.gameObject.SetActive(num2 == 2);
		Color color = iconBaseCrouch.GetComponent<Image>().color;
		color.a = 1f - num;
		iconBaseCrouch.GetComponent<Image>().color = color;
		iconBaseStand.GetComponent<Image>().color = color;
		iconBaseStretch.GetComponent<Image>().color = color;
		visualValue = Mathf.Clamp((float)value + vector.y / 50f * 0.8f, 0f, 2f);
	}

	public void OnEndDrag(PointerEventData data)
	{
	}

	public void moveTo(int newValue)
	{
		value = newValue;
	}

	public void update(ControlMode controlMode)
	{
		if (!dragging)
		{
			moving.anchoredPosition = Vector2.MoveTowards(moving.anchoredPosition, originalMoving, Time.deltaTime * 50f * 4f);
			int num = ((controlMode != ControlMode.MouseAndKeyboard && controlMode != ControlMode.VirtualGamepad) ? 1 : 4);
			visualValue = Mathf.MoveTowards(visualValue, value, Time.deltaTime * 2f * (float)num);
			moving.GetComponent<CanvasGroup>().alpha = Mathf.Abs(moving.anchoredPosition.y - originalMoving.y) / 50f;
			Color color = iconBaseCrouch.GetComponent<Image>().color;
			color.a = 1f - moving.GetComponent<CanvasGroup>().alpha;
			iconBaseCrouch.GetComponent<Image>().color = color;
			iconBaseStand.GetComponent<Image>().color = color;
			iconBaseStretch.GetComponent<Image>().color = color;
		}
		iconBaseCrouch.gameObject.SetActive(value == 0);
		iconBaseStand.gameObject.SetActive(value == 1);
		iconBaseStretch.gameObject.SetActive(value == 2);
	}
}
