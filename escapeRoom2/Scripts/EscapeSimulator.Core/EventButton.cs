using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
	[Serializable]
	public class ButtonEvent : UnityEvent<Selectable>
	{
	}

	public bool clickOnSelect;

	public bool disableAfterClick = true;

	public ButtonEvent onPointerEnter;

	public ButtonEvent onPointerExit;

	public ButtonEvent onDoubleClick;

	public Font hoverTextFont;

	public Font normalTextFont;

	[NonSerialized]
	public Color hoverTextColor = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1f);

	[NonSerialized]
	public Color normalTextColor = new Color(0.6f, 0.6f, 0.6f, 1f);

	private Selectable selectable;

	public EventButtonType buttonType;

	private void Start()
	{
		selectable = GetComponent<Selectable>();
		Text componentInChildren = GetComponentInChildren<Text>();
		if (componentInChildren != null && normalTextFont != null)
		{
			componentInChildren.font = normalTextFont;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		onPointerEnter.Invoke(selectable);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!(EventSystem.current == null) && EventSystem.current.currentSelectedGameObject != base.gameObject)
		{
			onPointerExit.Invoke(selectable);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (disableAfterClick)
		{
			onPointerExit.Invoke(selectable);
		}
		if (eventData.clickCount == 2 && onDoubleClick != null)
		{
			onDoubleClick.Invoke(selectable);
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (clickOnSelect && Controller.isActive() && selectable is Toggle toggle)
		{
			toggle.isOn = true;
		}
		onPointerEnter.Invoke(selectable);
	}

	public void OnDeselect(BaseEventData eventData)
	{
		onPointerExit.Invoke(selectable);
	}

	public void OnDisable()
	{
		onPointerExit.Invoke(selectable);
	}
}
