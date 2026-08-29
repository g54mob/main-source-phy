using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridContainer : MonoBehaviour
{
	private List<RectTransform> activeItems = new List<RectTransform>(32);

	private List<RectTransform> activeItemsLast = new List<RectTransform>(32);

	private RectTransform rt;

	public RectTransform selectionMarker;

	public RectTransform hoverMarker;

	public RectTransform preciseMarker;

	public Vector2 margins;

	public float hoverFlash = 1f;

	public float redFlash = 1f;

	public int selected = -1;

	public int hover = -1;

	public Vector2 cellSize = new Vector2(-1f, -1f);

	public float cellScale = 1f;

	private int lastSelected = -1;

	private int lastHover = -1;

	public int columns = -1;

	public float preciseSelection = -1f;

	public bool useHover;

	public bool isTrash;

	public bool useMobileVersion;

	private void Start()
	{
		useHover = false;
		rt = GetComponent<RectTransform>();
		sync(animate: false);
	}

	private void Update()
	{
		sync(animate: true);
	}

	private Vector2 MoveTowardsSmoothLerp(Vector2 current, Vector2 target, float moveAmount)
	{
		return Vector2.Lerp(target, current, Mathf.Exp(0f - moveAmount));
	}

	private void sync(bool animate)
	{
		Vector2 a = Vector2.zero;
		activeItems.Clear();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			if (!child.gameObject.activeSelf || !(child.GetComponent<ItemUI>() == null))
			{
				continue;
			}
			RectTransform component = child.GetComponent<RectTransform>();
			if (component != selectionMarker && component != hoverMarker && component != preciseMarker)
			{
				activeItems.Add(component);
				if (cellSize.x > 0f && cellSize.y > 0f)
				{
					component.sizeDelta = cellSize * cellScale;
				}
			}
		}
		if (activeItems.Count > 0)
		{
			a = activeItems[0].sizeDelta;
		}
		int num = ((columns > 0) ? columns : activeItems.Count);
		for (int j = 0; j < activeItems.Count; j++)
		{
			int num2 = j / num;
			int num3 = j % num;
			Vector2 vector = new Vector2(a.x * (float)num3, (0f - a.y) * (float)num2) + new Vector2(margins.x, 0f - margins.y);
			bool flag = activeItemsLast.Contains(activeItems[j]);
			activeItems[j].anchoredPosition = ((animate && !flag) ? MoveTowardsSmoothLerp(activeItems[j].anchoredPosition, vector, 8f * Time.deltaTime) : vector);
		}
		float y = ((num == 0) ? 0f : Mathf.Ceil((float)activeItems.Count / (float)num));
		Vector2 vector2 = Vector2.Scale(a, new Vector2(num, y));
		if (activeItems.Count > 0)
		{
			vector2 += margins * 2f;
		}
		rt.sizeDelta = (animate ? MoveTowardsSmoothLerp(rt.sizeDelta, vector2, 8f * Time.deltaTime) : vector2);
		handleMarker(selectionMarker, selected, lastSelected, 0.2f, 1f, 1f);
		if (!Controller.isActive() || useHover || isTrash)
		{
			handleMarker(hoverMarker, hover, lastHover, 0.1f, hoverFlash, redFlash);
		}
		hoverMarker.gameObject.SetActive(!Controller.isActive() || useHover || isTrash);
		hoverFlash = Mathf.MoveTowards(hoverFlash, 1f, Time.deltaTime * 5f);
		redFlash = Mathf.MoveTowards(redFlash, 1f, Time.deltaTime * 3f);
		List<RectTransform> list = activeItemsLast;
		activeItemsLast = activeItems;
		activeItems = list;
		if (preciseSelection >= 0f)
		{
			preciseMarker.gameObject.SetActive(value: true);
			preciseMarker.anchoredPosition = new Vector2(preciseSelection * a.x, preciseMarker.anchoredPosition.y);
		}
		else
		{
			preciseMarker.gameObject.SetActive(value: false);
		}
		lastSelected = selected;
		lastHover = hover;
		void handleMarker(RectTransform marker, int index, int lastIndex, float maxAlpha, float sizeModifier, float red)
		{
			Color color = marker.GetComponent<Image>().color;
			float num4 = ((index == -1) ? 0f : maxAlpha);
			color.a = (animate ? Mathf.MoveTowards(color.a, num4, Time.deltaTime * 5f) : num4);
			color.g = 1f - Mathf.PingPong(red * 2f, 1f);
			color.b = 1f - Mathf.PingPong(red * 2f, 1f);
			marker.GetComponent<Image>().color = color;
			Vector2 vector3 = Vector2.zero;
			Vector2 vector4 = Vector2.zero;
			bool flag2 = lastIndex == -1 && lastIndex != index;
			if (activeItems.Count > 0)
			{
				RectTransform rectTransform = activeItems[Mathf.Clamp(index + (useMobileVersion ? 1 : 0), 0, activeItems.Count - 1)];
				vector3 = rectTransform.sizeDelta;
				vector4 = rectTransform.anchoredPosition;
			}
			float num5 = 1f + Mathf.PingPong(sizeModifier * 2f, 1f) * 0.1f;
			Vector2 vector5 = vector3 * num5;
			vector4.x -= (vector5.x - vector3.x) * 0.5f;
			vector4.y += (vector5.y - vector3.y) * 0.5f;
			vector3 = vector5;
			marker.anchoredPosition = ((animate && !flag2) ? MoveTowardsSmoothLerp(marker.anchoredPosition, vector4, 8f * Time.deltaTime) : vector4);
			marker.sizeDelta = ((animate && !flag2) ? MoveTowardsSmoothLerp(marker.sizeDelta, vector3, 8f * Time.deltaTime) : vector3);
		}
	}
}
