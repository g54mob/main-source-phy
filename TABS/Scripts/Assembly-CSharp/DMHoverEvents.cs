using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DMHoverEvents : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	public UnityEvent onEnter;

	public UnityEvent onExit;

	public void OnPointerEnter(PointerEventData eventData)
	{
		onEnter?.Invoke();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		onExit?.Invoke();
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (PlayerActions.Instance.InputType == InputType.Controller)
		{
			onEnter?.Invoke();
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		if (PlayerActions.Instance.InputType == InputType.Controller)
		{
			onExit?.Invoke();
		}
	}
}
