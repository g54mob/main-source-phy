using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CCVariantButton : Button, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public CCVariantUI ui;

	public override void OnPointerEnter(PointerEventData eventData)
	{
		hover();
		base.OnPointerEnter(eventData);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		unhover();
		base.OnPointerExit(eventData);
	}

	public void hover()
	{
		ui.HoverFrame.gameObject.SetActive(value: true);
	}

	public void unhover()
	{
		ui.HoverFrame.gameObject.SetActive(value: false);
	}
}
