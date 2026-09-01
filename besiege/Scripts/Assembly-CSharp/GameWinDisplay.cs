using Selectors;
using UnityEngine;

public class GameWinDisplay : LogicEventDisplay
{
	private EventContainer.GameWinEvent winEvent;

	[SerializeField]
	private UIButton prevWinType;

	[SerializeField]
	private UIButton nextWinType;

	[SerializeField]
	private TextHolder keyHolder;

	[SerializeField]
	private GameObject[] winTypes;

	protected override void Awake()
	{
		base.Awake();
		prevWinType.Click += OnPrevClicked;
		nextWinType.Click += OnNextClicked;
		keyHolder.TextChanged += OnKeyChanged;
	}

	protected void OnDestroy()
	{
		keyHolder.TextChanged -= OnKeyChanged;
	}

	private void OnKeyChanged(string newKey)
	{
		if (isEditing)
		{
			winEvent.varName = newKey;
			eventWidget.OnEditEvent();
		}
	}

	private void OnPrevClicked()
	{
		if (winEvent.winType == EventContainer.GameWinEvent.WinType.Health)
		{
			winEvent.winType = EventContainer.GameWinEvent.WinType.Variable;
		}
		else if (winEvent.winType == EventContainer.GameWinEvent.WinType.Variable)
		{
			winEvent.winType = EventContainer.GameWinEvent.WinType.Progress;
		}
		else
		{
			winEvent.winType = EventContainer.GameWinEvent.WinType.Health;
		}
		eventWidget.OnEditEvent();
	}

	private void OnNextClicked()
	{
		if (winEvent.winType == EventContainer.GameWinEvent.WinType.Health)
		{
			winEvent.winType = EventContainer.GameWinEvent.WinType.Progress;
		}
		else if (winEvent.winType == EventContainer.GameWinEvent.WinType.Progress)
		{
			winEvent.winType = EventContainer.GameWinEvent.WinType.Variable;
		}
		else
		{
			winEvent.winType = EventContainer.GameWinEvent.WinType.Health;
		}
		eventWidget.OnEditEvent();
	}

	public override void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
	{
		base.Init(parentWidget, inLogic, inEvent);
		UpdateVisual();
	}

	public override void UpdateVisual()
	{
		winEvent = currentEvent.eventData as EventContainer.GameWinEvent;
		if (isEditing && winEvent != null)
		{
			int winType = (int)winEvent.winType;
			for (int i = 0; i < winTypes.Length; i++)
			{
				winTypes[i].SetActive(i == winType);
			}
			if (winEvent.winType == EventContainer.GameWinEvent.WinType.Variable)
			{
				keyHolder.SetText(winEvent.varName);
			}
			UpdateBackground();
		}
	}
}
