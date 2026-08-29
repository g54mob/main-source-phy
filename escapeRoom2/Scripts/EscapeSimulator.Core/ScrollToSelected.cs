using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollToSelected : MonoBehaviour
{
	public float scrollSpeed = 10f;

	private ScrollRect scrollRect;

	private RectTransform rectTransform;

	private RectTransform selectedRectTransform;

	private void Awake()
	{
		scrollRect = GetComponent<ScrollRect>();
		rectTransform = GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (!Controller.isActive())
		{
			return;
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return;
		}
		RectTransform content = scrollRect.content;
		if (!(currentSelectedGameObject.transform.parent != content.transform))
		{
			selectedRectTransform = currentSelectedGameObject.GetComponent<RectTransform>();
			Vector3 vector = rectTransform.localPosition - selectedRectTransform.localPosition;
			float num = content.rect.height - rectTransform.rect.height;
			float num2 = content.rect.height - vector.y;
			float num3 = scrollRect.normalizedPosition.y * num;
			float num4 = num3 - selectedRectTransform.rect.height / 2f + rectTransform.rect.height;
			float num5 = num3 + selectedRectTransform.rect.height / 2f;
			if (num2 > num4)
			{
				float num6 = num2 - num4;
				float y = (num3 + num6) / num;
				scrollRect.normalizedPosition = Vector2.Lerp(scrollRect.normalizedPosition, new Vector2(0f, y), scrollSpeed * Time.deltaTime);
			}
			else if (num2 < num5)
			{
				float num7 = num2 - num5;
				float y2 = (num3 + num7) / num;
				scrollRect.normalizedPosition = Vector2.Lerp(scrollRect.normalizedPosition, new Vector2(0f, y2), scrollSpeed * Time.deltaTime);
			}
		}
	}
}
