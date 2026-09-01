using Selectors;
using UnityEngine;

public class BehavioureEventDisplay : LogicEventDisplay
{
	public ValueHolder valueHolderDistance;

	public ValueHolder valueHolderSpeed;

	public UIButton attackToggle;

	public GameObject attackToggleBG;

	private EventContainer.EntityBehaviourEvent valEvent;

	protected override void Awake()
	{
		base.Awake();
		valueHolderDistance.ValueChanged += OnDistanceChange;
		valueHolderSpeed.ValueChanged += OnSpeedChange;
		if ((bool)attackToggle)
		{
			attackToggle.Down += ToggleAttack;
		}
	}

	public override void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
	{
		base.Init(parentWidget, inLogic, inEvent);
		UpdateVisual();
	}

	public override void UpdateVisual()
	{
		valEvent = currentEvent.eventData as EventContainer.EntityBehaviourEvent;
		if (isEditing && valEvent != null)
		{
			valueHolderDistance.SetText(valEvent.activationDistance);
			valueHolderSpeed.SetText(valEvent.speed);
			attackToggleBG.SetActive(valEvent.attack);
			UpdateBackground();
		}
	}

	public void OnDistanceChange(float newDistance)
	{
		if (isEditing)
		{
			valEvent.activationDistance = newDistance;
			eventWidget.OnEditEvent();
			eventWidget.Selector.OnSortBehaviour(eventWidget.Index);
		}
	}

	public void OnSpeedChange(float newSpeed)
	{
		if (isEditing)
		{
			valEvent.speed = newSpeed;
			eventWidget.OnEditEvent();
		}
	}

	public void ToggleAttack()
	{
		if (valEvent != null)
		{
			valEvent.attack = !valEvent.attack;
			attackToggleBG.SetActive(valEvent.attack);
			eventWidget.OnEditEvent();
		}
	}
}
