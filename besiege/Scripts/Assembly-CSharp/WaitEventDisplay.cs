using Selectors;
using UnityEngine;

public class WaitEventDisplay : LogicEventDisplay
{
	public ValueHolder valueHolder;

	public UIButtonExtended toggleOnScreenCountDown;

	public UIButton cycleButton;

	public GameObject iconSwitcher;

	public GameObject[] icons;

	private EventContainer.WaitEvent waitEvent;

	protected override void Awake()
	{
		base.Awake();
		valueHolder.ValueChanged += OnWaitTime;
		toggleOnScreenCountDown.Down += ToggledOnScreenCountDown;
		cycleButton.Down += CycleIcons;
	}

	public override void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
	{
		base.Init(parentWidget, inLogic, inEvent);
		UpdateVisual();
	}

	public override void UpdateVisual()
	{
		waitEvent = currentEvent.eventData as EventContainer.WaitEvent;
		if (!isEditing || waitEvent == null)
		{
			return;
		}
		if (waitEvent.displayCountDown)
		{
			toggleOnScreenCountDown.BG.SetActive(true);
			iconSwitcher.SetActive(true);
			for (int i = 0; i < icons.Length; i++)
			{
				icons[i].SetActive(i == waitEvent.icon);
			}
		}
		else
		{
			toggleOnScreenCountDown.BG.SetActive(false);
			iconSwitcher.SetActive(false);
		}
		valueHolder.SetText(waitEvent.waitTime);
		UpdateBackground();
	}

	public void OnWaitTime(float newWaitTime)
	{
		if (isEditing)
		{
			waitEvent.waitTime = newWaitTime;
			eventWidget.OnEditEvent();
		}
	}

	public void ToggledOnScreenCountDown()
	{
		waitEvent.displayCountDown = !waitEvent.displayCountDown;
		UpdateVisual();
		eventWidget.OnEditEvent();
	}

	public void CycleIcons()
	{
		int icon = waitEvent.icon;
		icon++;
		if (icon >= icons.Length)
		{
			icon = 0;
		}
		waitEvent.icon = icon;
		UpdateVisual();
		eventWidget.OnEditEvent();
	}

	protected override void UpdateBackground()
	{
		float y = backgroundTransform.localScale.y;
		float num = defaultHeight + ((!waitEvent.displayCountDown) ? 0f : 0.23f);
		backgroundTransform.localScale = new Vector3(backgroundTransform.localScale.x, num, backgroundTransform.localScale.z);
		UpdateBottomLine();
		if (y != num)
		{
			BlockMapper currentInstance = BlockMapper.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.IsDirty = true;
			}
		}
	}
}
