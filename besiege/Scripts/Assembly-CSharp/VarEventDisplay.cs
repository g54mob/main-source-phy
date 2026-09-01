using Selectors;
using UnityEngine;

public class VarEventDisplay : PickEventDisplay
{
	public ValueHolder valueHolder;

	public TextHolder keyHolder;

	public UIButtonExtended modifyButton;

	public GameObject[] modifyModes;

	private EventContainer.VariableEvent varEvent;

	protected override void Awake()
	{
		base.Awake();
		modifyButton.Click += OnModifyChanged;
		valueHolder.ValueChanged += OnValueChange;
		keyHolder.TextChanged += OnKeyChange;
	}

	private void OnModifyChanged()
	{
		if (varEvent.modifyType == EventContainer.VarModifyType.Set)
		{
			varEvent.modifyType = EventContainer.VarModifyType.Add;
		}
		else if (varEvent.modifyType == EventContainer.VarModifyType.Add)
		{
			varEvent.modifyType = EventContainer.VarModifyType.Subtract;
		}
		else
		{
			varEvent.modifyType = EventContainer.VarModifyType.Set;
		}
		eventWidget.OnEditEvent();
	}

	public override void UpdateVisual()
	{
		varEvent = currentEvent.eventData as EventContainer.VariableEvent;
		if (varEvent == null)
		{
			return;
		}
		base.UpdateVisual();
		string key = varEvent.key;
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
		int modifyType = (int)varEvent.modifyType;
		for (int num = 0; num < modifyModes.Length; num++)
		{
			modifyModes[num].SetActive(num == modifyType);
		}
		valueHolder.SetText(varEvent.val);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		keyHolder.TextChanged -= OnKeyChange;
		valueHolder.ValueChanged -= OnValueChange;
	}

	private void OnKeyChange(string newKey)
	{
		if (isEditing)
		{
			string result;
			if (EventContainer.SanatizeKey(newKey, out result))
			{
				varEvent.key = result;
				eventWidget.OnEditEvent();
			}
			else
			{
				keyHolder.SetText(varEvent.key);
			}
		}
	}

	private void OnValueChange(float newValue)
	{
		if (isEditing)
		{
			varEvent.val = newValue;
			eventWidget.OnEditEvent();
		}
	}

	protected override void UpdateBackground()
	{
		backgroundTransform.localScale = new Vector3(backgroundTransform.localScale.x, defaultHeight + 0.15f + (float)pickWidgets.Count * pickSpacer, backgroundTransform.localScale.z);
		UpdateBottomLine();
	}
}
