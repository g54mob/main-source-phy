using Selectors;
using UnityEngine;

public class RandomEventDisplay : PickEventDisplay
{
	public ValueHolder minHolder;

	public ValueHolder maxHolder;

	public TextHolder keyHolder;

	public UIButtonExtended modifyButton;

	public GameObject[] modifyModes;

	private EventContainer.RandomEvent randomEvent;

	protected override void Awake()
	{
		base.Awake();
		modifyButton.Click += OnModifyChanged;
		minHolder.ValueChanged += OnMinChange;
		maxHolder.ValueChanged += OnMaxChange;
		keyHolder.TextChanged += OnKeyChange;
	}

	private void OnModifyChanged()
	{
		if (isEditing)
		{
			if (randomEvent.modifyType == EventContainer.VarModifyType.Set)
			{
				randomEvent.modifyType = EventContainer.VarModifyType.Add;
			}
			else if (randomEvent.modifyType == EventContainer.VarModifyType.Add)
			{
				randomEvent.modifyType = EventContainer.VarModifyType.Subtract;
			}
			else
			{
				randomEvent.modifyType = EventContainer.VarModifyType.Set;
			}
			OnEditEvent();
		}
	}

	public override void UpdateVisual()
	{
		randomEvent = currentEvent.eventData as EventContainer.RandomEvent;
		if (randomEvent == null)
		{
			return;
		}
		base.UpdateVisual();
		string key = randomEvent.key;
		if (string.IsNullOrEmpty(key))
		{
			keyHolder.SetText(key);
		}
		else
		{
			WorkshopManager.VerifyString(key, delegate(WorkshopManager.VerifyStringResult res, string str)
			{
				if (keyHolder != null)
				{
					keyHolder.SetText(str);
				}
			});
		}
		int modifyType = (int)randomEvent.modifyType;
		for (int num = 0; num < modifyModes.Length; num++)
		{
			modifyModes[num].SetActive(num == modifyType);
		}
		minHolder.SetText(randomEvent.min);
		maxHolder.SetText(randomEvent.max);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		keyHolder.TextChanged -= OnKeyChange;
		minHolder.ValueChanged -= OnMinChange;
		maxHolder.ValueChanged -= OnMaxChange;
	}

	private void OnKeyChange(string newKey)
	{
		if (isEditing)
		{
			randomEvent.key = newKey;
			OnEditEvent();
		}
	}

	private void OnMaxChange(float newValue)
	{
		if (isEditing)
		{
			randomEvent.max = Mathf.RoundToInt(newValue);
			OnEditEvent();
		}
	}

	private void OnMinChange(float newValue)
	{
		if (isEditing)
		{
			randomEvent.min = Mathf.RoundToInt(newValue);
			OnEditEvent();
		}
	}

	protected override void UpdateBackground()
	{
		backgroundTransform.localScale = new Vector3(backgroundTransform.localScale.x, defaultHeight + 0.15f + (float)pickWidgets.Count * pickSpacer, backgroundTransform.localScale.z);
		UpdateBottomLine();
	}
}
