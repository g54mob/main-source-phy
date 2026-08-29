using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsPrefab_Base : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	public Selectable selectable;

	public Image selection;

	[NonSerialized]
	public bool usingHover;

	[NonSerialized]
	public Action onHover;

	[NonSerialized]
	public Action onUnhover;

	private void Awake()
	{
		selection.enabled = false;
	}

	private void OnEnable()
	{
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			OnSelect(null);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (usingHover)
		{
			selection.enabled = true;
			onHover?.Invoke();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (usingHover)
		{
			selection.enabled = false;
			onUnhover?.Invoke();
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		Debug.Log("Select " + base.name + " " + Controller.isActive(), base.gameObject);
		if (Controller.isActive())
		{
			selection.enabled = true;
			if (usingHover)
			{
				onHover?.Invoke();
			}
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		Debug.Log("Deselect " + base.name, base.gameObject);
		if (Controller.isActive())
		{
			selection.enabled = false;
			if (usingHover)
			{
				onUnhover?.Invoke();
			}
		}
	}

	private void OnDisable()
	{
		selection.enabled = false;
		if (usingHover)
		{
			onUnhover?.Invoke();
		}
	}
}
