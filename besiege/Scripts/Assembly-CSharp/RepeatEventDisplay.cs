using Selectors;

public class RepeatEventDisplay : LogicEventDisplay
{
	public ValueHolderDefaulting maxRepeats;

	private EventContainer.RepeatEvent repeatEvent;

	protected override void Awake()
	{
		base.Awake();
		maxRepeats.ValueChanged += OnRepeatChange;
		maxRepeats.FocusChange += delegate(bool b)
		{
			if (!b)
			{
				UpdateVisual();
			}
		};
	}

	public override void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
	{
		base.Init(parentWidget, inLogic, inEvent);
		repeatEvent = currentEvent.eventData as EventContainer.RepeatEvent;
		UpdateVisual();
	}

	public override void UpdateVisual()
	{
		if (isEditing && repeatEvent != null)
		{
			if (repeatEvent.repeatCount >= 1f && repeatEvent.repeatCount != float.PositiveInfinity)
			{
				maxRepeats.SetText(repeatEvent.repeatCount);
			}
			else
			{
				maxRepeats.SetDefaultText();
			}
			UpdateBackground();
		}
	}

	public void OnRepeatChange(float newValue)
	{
		if (isEditing)
		{
			if (newValue < 1f)
			{
				newValue = float.PositiveInfinity;
			}
			repeatEvent.repeatCount = newValue;
			eventWidget.OnEditEvent();
		}
	}
}
