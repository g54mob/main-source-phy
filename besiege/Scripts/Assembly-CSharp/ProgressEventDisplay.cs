using Selectors;

public class ProgressEventDisplay : LogicEventDisplay
{
	public ValueHolder valueHolder;

	public TeamButton teamButton;

	private UIButton teamBtn;

	private EventContainer.LevelProgressEvent progressEvent;

	protected override void Awake()
	{
		base.Awake();
		valueHolder.ValueChanged += OnProgressAmount;
		teamBtn = teamButton.GetComponent<UIButton>();
		teamBtn.Click += OnTeamChange;
	}

	private void OnTeamChange()
	{
		teamButton.NextTeam();
		currentEvent.team = teamButton.Team;
		UpdateVisual();
		eventWidget.OnEditEvent();
	}

	protected void OnDestroy()
	{
		valueHolder.ValueChanged -= OnProgressAmount;
	}

	public override void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
	{
		base.Init(parentWidget, inLogic, inEvent);
		UpdateVisual();
	}

	public override void UpdateVisual()
	{
		progressEvent = currentEvent.eventData as EventContainer.LevelProgressEvent;
		if (isEditing && progressEvent != null)
		{
			valueHolder.SetText(progressEvent.progress);
			teamButton.SetTeam(currentEvent.team);
			UpdateBackground();
		}
	}

	private void OnProgressAmount(float progressAmount)
	{
		if (isEditing)
		{
			progressEvent.progress = progressAmount;
			eventWidget.OnEditEvent();
		}
	}
}
