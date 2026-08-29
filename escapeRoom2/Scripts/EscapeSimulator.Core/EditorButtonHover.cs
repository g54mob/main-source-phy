using UnityEngine;
using UnityEngine.EventSystems;

public class EditorButtonHover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public delegate void OnEnterHover();

	public delegate void OnExitHover();

	public delegate void OnHovering();

	private bool hovered;

	public OnEnterHover onEnterHover;

	public OnExitHover onExitHover;

	public OnHovering onHovering;

	[Header("PropButtonUI")]
	public PropButtonUI buttonUI;

	private void Update()
	{
		if (buttonUI != null)
		{
			Vector3 target = (hovered ? (Vector3.one * 1.1f) : Vector3.one);
			Vector3 localScale = Vector3.MoveTowards(buttonUI.Icon.transform.localScale, target, Time.deltaTime);
			buttonUI.Icon.transform.localScale = localScale;
		}
		if (hovered && onHovering != null)
		{
			onHovering();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		hovered = true;
		if (onEnterHover != null)
		{
			onEnterHover();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		hovered = false;
		if (onExitHover != null)
		{
			onExitHover();
		}
	}
}
