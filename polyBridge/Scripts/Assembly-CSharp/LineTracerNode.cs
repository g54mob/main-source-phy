using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineTracerNode : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Color normalColor;

	public Color highlightedColor;

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		base.gameObject.GetComponent<Image>().color = highlightedColor;
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		base.gameObject.GetComponent<Image>().color = normalColor;
	}
}
