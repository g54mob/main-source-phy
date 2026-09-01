using System.Collections.Generic;
using InternalModding.Events;
using UnityEngine;

public class LogicEventWidget : LogicWidget
{
	public UIButton trashButton;

	public UIButton downButton;

	public UIButton upButton;

	public Transform displayContainer;

	public EntityEvent entityEvent;

	protected LogicEventDisplay currentDisplay;

	private Vector3 downButtonPos;

	private Vector3 upButtonPos;

	private List<EventContainer.EventType> events;

	public void RefreshPicker()
	{
		if (EventContainer.IsPickEvent(entityEvent.eventType))
		{
			(currentDisplay as PickEventDisplay).Refresh();
		}
	}

	public override void ResetToPool()
	{
		base.ResetToPool();
		if (entityEvent != null)
		{
			entityEvent.EventChanged -= OnEventChange;
		}
		entityEvent = null;
		currentDisplay.ResetToPool();
	}

	public void Awake()
	{
		trashButton.Click += OnRemove;
		downButton.Click += OnMoveDown;
		upButton.Click += OnMoveUp;
		downButtonPos = downButton.transform.localPosition;
		upButtonPos = upButton.transform.localPosition;
		GameObject gameObject = Object.Instantiate(SingleInstanceFindOnly<EventLoader>.Instance.ModdedEventDisplay);
		gameObject.name = "Modded";
		gameObject.transform.parent = displayContainer;
		ModdedEventDisplay component = gameObject.GetComponent<ModdedEventDisplay>();
		component.backgroundTransform = base.transform.FindChild("Background");
		component.line = base.transform.FindChild("Line");
	}

	protected override void Init()
	{
		EntityEvent entityEvent = logic.events[index];
		if (entityEvent != this.entityEvent)
		{
			if (this.entityEvent != null)
			{
				this.entityEvent.EventChanged -= OnEventChange;
			}
			entityEvent.EventChanged += OnEventChange;
			this.entityEvent = entityEvent;
		}
		events = EventContainer.GetEvents(logic.triggerType);
	}

	private void OnEventChange()
	{
		if (BlockMapper.IsOpen)
		{
			if (currentDisplay == null || !currentDisplay.name.Equals(entityEvent.eventType.ToString()))
			{
				UpdateVisual();
			}
			else
			{
				currentDisplay.UpdateVisual();
			}
		}
	}

	protected override void UpdateVisual()
	{
		string value = entityEvent.eventType.ToString();
		if (displayContainer == null)
		{
			Debug.LogWarning("Tried to update a visual without a displaycontainer, this should not happen");
			return;
		}
		for (int i = 0; i < displayContainer.childCount; i++)
		{
			Transform child = displayContainer.GetChild(i);
			GameObject gameObject = child.gameObject;
			if (gameObject.name.Equals(value))
			{
				LogicEventDisplay component = child.GetComponent<LogicEventDisplay>();
				currentDisplay = component;
				if (!gameObject.activeSelf)
				{
					gameObject.SetActive(true);
				}
				currentDisplay.Init(this, logic, entityEvent);
			}
			else if (gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
		}
	}

	public void OnEditEvent()
	{
		if (hasHandler)
		{
			editLogicHandler.OnEditEvent(logic, entityEvent);
		}
	}

	public void OnMoveUp()
	{
		logicSelector.OnMoveEvent(index, false);
	}

	public void OnMoveDown()
	{
		logicSelector.OnMoveEvent(index, true);
	}

	public void OnNext()
	{
		int num = events.IndexOf(entityEvent.eventType);
		EventContainer.EventType e = ((num != events.Count - 1) ? events[num + 1] : events[0]);
		entityEvent.ChangeType(e);
		UpdateVisual();
		OnEditEvent();
	}

	public void OnPrev()
	{
		int num = events.IndexOf(entityEvent.eventType);
		EventContainer.EventType e = ((num != 0) ? events[num - 1] : events[events.Count - 1]);
		entityEvent.ChangeType(e);
		UpdateVisual();
		OnEditEvent();
	}

	public void OnRemove()
	{
		logicSelector.OnRemoveEvent(index);
	}

	protected override void ToggleHover(bool toggle)
	{
		trashButton.gameObject.SetActive(toggle);
		bool flag = toggle && index + 1 < logic.events.Count;
		bool flag2 = toggle && index > 0;
		downButton.gameObject.SetActive(flag);
		upButton.gameObject.SetActive(flag2);
		if (flag)
		{
			if (flag2)
			{
				downButton.transform.localPosition = downButtonPos;
				upButton.transform.localPosition = upButtonPos;
			}
			else
			{
				downButton.transform.localPosition = (downButtonPos + upButtonPos) / 2f;
			}
		}
		else if (flag2)
		{
			upButton.transform.localPosition = (downButtonPos + upButtonPos) / 2f;
		}
		currentDisplay.ToggleHover(toggle);
	}
}
