using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITooltip : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public enum Position
	{
		Auto = 0,
		Top = 1,
		Bot = 2,
		Left = 3,
		Right = 4
	}

	public Canvas canvasOverride;

	public float scaleOverride = 1f;

	public float timeToShow = 0.5f;

	public string text;

	public Position position;

	public float mainPositionOffset;

	public float crossPositionOffset;

	public float pivotOffset;

	public float spacing;

	public Vector2 sizeModifier = Vector3.one;

	public RectTransform tooltipTemplate;

	public bool calculateSize;

	public bool useSpacingInSizeCalculation;

	private RectTransform tooltipObject;

	private float enterTimestamp = -1f;

	public void OnPointerEnter(PointerEventData eventData)
	{
		enterTimestamp = Time.time;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		enterTimestamp = -1f;
		removeTooltip();
	}

	private void OnDisable()
	{
		enterTimestamp = -1f;
		removeTooltip();
	}

	public void Update()
	{
		if (enterTimestamp == -1f || !(enterTimestamp + timeToShow < Time.time) || tooltipObject != null)
		{
			return;
		}
		Canvas componentInParent = GetComponentInParent<Canvas>();
		if (canvasOverride != null)
		{
			componentInParent = canvasOverride;
		}
		RectTransform component = componentInParent.GetComponent<RectTransform>();
		tooltipObject = Object.Instantiate(tooltipTemplate, component, worldPositionStays: true);
		float x = Input.mousePosition.x / (float)Screen.width;
		float y = Input.mousePosition.y / (float)Screen.height;
		tooltipObject.transform.localScale = Vector3.one * scaleOverride;
		tooltipObject.transform.localRotation = Quaternion.identity;
		if (position == Position.Auto)
		{
			tooltipObject.pivot = new Vector2(x, y);
			RectTransformUtility.ScreenPointToLocalPointInRectangle(component, Input.mousePosition, componentInParent.worldCamera, out var localPoint);
			tooltipObject.position = component.TransformPoint(localPoint + tooltipObject.pivot * (0f - mainPositionOffset));
		}
		else
		{
			RectTransform component2 = GetComponent<RectTransform>();
			Vector3[] array = new Vector3[4];
			component2.GetWorldCorners(array);
			if (position == Position.Right)
			{
				tooltipObject.pivot = new Vector2(0f, 0.5f + pivotOffset);
				tooltipObject.position = Vector3.Lerp(array[2], array[3], 0.5f + mainPositionOffset) + (array[2] - array[1]).normalized * crossPositionOffset;
				tooltipObject.anchoredPosition += Vector2.right * spacing;
			}
			else if (position == Position.Left)
			{
				tooltipObject.pivot = new Vector2(1f, 0.5f + pivotOffset);
				tooltipObject.position = Vector3.Lerp(array[0], array[1], 0.5f + mainPositionOffset) + (array[1] - array[2]).normalized * crossPositionOffset;
				tooltipObject.anchoredPosition += Vector2.left * spacing;
			}
			else if (position == Position.Top)
			{
				tooltipObject.pivot = new Vector2(0.5f + pivotOffset, 0f);
				tooltipObject.position = Vector3.Lerp(array[1], array[2], 0.5f + mainPositionOffset) + (array[1] - array[0]).normalized * crossPositionOffset;
				tooltipObject.anchoredPosition += Vector2.up * spacing;
			}
			else if (position == Position.Bot)
			{
				tooltipObject.pivot = new Vector2(0.5f + pivotOffset, 1f);
				tooltipObject.position = Vector3.Lerp(array[0], array[3], 0.5f + mainPositionOffset) + (array[0] - array[1]).normalized * crossPositionOffset;
				tooltipObject.anchoredPosition += Vector2.down * spacing;
			}
		}
		Text component3 = tooltipObject.GetChild(0).GetComponent<Text>();
		string text = Localization.lookupInDictionary(this.text, "%%%");
		if (text == "%%%")
		{
			text = this.text;
		}
		if (calculateSize)
		{
			component3.text = text.Replace("\\n", "\r\n");
			tooltipObject.sizeDelta = new Vector2(100000f, 100000f);
			float num = component3.rectT().offsetMin.x;
			float num2 = component3.rectT().offsetMin.y;
			if (useSpacingInSizeCalculation)
			{
				num += spacing;
				num2 += spacing;
			}
			tooltipObject.sizeDelta = new Vector2(component3.preferredWidth + num * 2f, component3.preferredHeight + num2 * 2f);
		}
		else
		{
			tooltipObject.sizeDelta *= sizeModifier;
			component3.text = text;
		}
		tooltipObject.ForceUpdateRectTransforms();
	}

	private void removeTooltip()
	{
		if (tooltipObject != null)
		{
			Object.Destroy(tooltipObject.gameObject);
		}
	}
}
