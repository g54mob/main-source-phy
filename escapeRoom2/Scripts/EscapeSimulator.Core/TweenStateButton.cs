using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TweenState))]
public class TweenStateButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
{
	private TweenState tween;

	private Button button;

	private void Awake()
	{
		if (!(tween != null))
		{
			tween = GetComponent<TweenState>();
			button = GetComponent<Button>();
			tween.init();
		}
	}

	private void Update()
	{
		if (Controller.isActive() && EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			if (Controller.getUIConfirmDown())
			{
				PineUI.interact(button, EventTriggerType.PointerDown);
				PineUI.interact(button, EventTriggerType.PointerClick);
			}
			else if (Controller.getUIConfirmUp())
			{
				PineUI.interact(button, EventTriggerType.PointerUp);
			}
		}
	}

	private void OnDisable()
	{
		TweenState.TweenStateRecord tweenStateRecord = tween.findStateByName("Hover");
		if (tweenStateRecord != null)
		{
			tweenStateRecord.targetWeight = 0f;
			tweenStateRecord.speed = 10f;
			tweenStateRecord = tween.findStateByName("Down");
			tweenStateRecord.targetWeight = 0f;
			tweenStateRecord.speed = 10f;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (tween == null)
		{
			Awake();
		}
		TweenState.TweenStateRecord tweenStateRecord = tween.findStateByName("Hover");
		tweenStateRecord.targetWeight = 1f;
		tweenStateRecord.speed = 10f;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		TweenState.TweenStateRecord tweenStateRecord = tween.findStateByName("Hover");
		tweenStateRecord.targetWeight = 0f;
		tweenStateRecord.speed = 10f;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		TweenState.TweenStateRecord tweenStateRecord = tween.findStateByName("Down");
		tweenStateRecord.targetWeight = 1f;
		tweenStateRecord.speed = 10f;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		TweenState.TweenStateRecord tweenStateRecord = tween.findStateByName("Down");
		tweenStateRecord.targetWeight = 0f;
		tweenStateRecord.speed = 10f;
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (Controller.isActive())
		{
			TweenState.TweenStateRecord tweenStateRecord = tween.findStateByName("Hover");
			tweenStateRecord.targetWeight = 1f;
			tweenStateRecord.speed = 10f;
			return;
		}
		TweenState.TweenStateRecord tweenStateRecord2 = tween.findStateByName("Hover");
		tweenStateRecord2.targetWeight = 0f;
		tweenStateRecord2.speed = 10f;
		TweenState.TweenStateRecord tweenStateRecord3 = tween.findStateByName("Down");
		tweenStateRecord3.targetWeight = 0f;
		tweenStateRecord3.speed = 10f;
	}

	public void OnDeselect(BaseEventData eventData)
	{
		TweenState.TweenStateRecord tweenStateRecord = tween.findStateByName("Hover");
		if (tweenStateRecord != null)
		{
			tweenStateRecord.targetWeight = 0f;
			tweenStateRecord.speed = 10f;
		}
	}
}
