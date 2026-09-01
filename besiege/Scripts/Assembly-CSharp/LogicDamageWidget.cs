using Selectors;
using UnityEngine;

public class LogicDamageWidget : LogicWidget
{
	public ValueHolder valueHolder;

	public UIButtonExtended hpRangeToggle;

	protected void Awake()
	{
		valueHolder.ValueChanged += OnDamageAmount;
		hpRangeToggle.Down += ToggleHealthBarRange;
	}

	private void ToggleHealthBarRange()
	{
		bool useHPRangeToggle = !logic.useHPRangeToggle;
		logic.useHPRangeToggle = useHPRangeToggle;
		for (int i = 0; i < entityBehaviour.logicData.Count; i++)
		{
			EntityLogic entityLogic = entityBehaviour.logicData[i];
			if (entityLogic != logic && entityLogic.useHPRangeToggle)
			{
				entityLogic.useHPRangeToggle = false;
				if (hasHandler)
				{
					editLogicHandler.OnEditLogic(entityLogic);
				}
			}
		}
		if (hasHandler)
		{
			editLogicHandler.OnEditLogic(logic);
		}
	}

	private void OnDamageAmount(float damageAmount)
	{
		if (!isEditing)
		{
			Debug.Log("OnDamageAmount return");
			return;
		}
		logic.damageIncrement = damageAmount;
		if (hasHandler)
		{
			editLogicHandler.OnEditLogic(logic);
		}
	}

	protected override void UpdateVisual()
	{
		valueHolder.SetText(logic.damageIncrement);
		hpRangeToggle.ToggleBG(logic.useHPRangeToggle);
	}
}
