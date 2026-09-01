using Selectors;
using UnityEngine;

public class LogicVarWidget : LogicWidget
{
	public ValueHolder valueHolder;

	public TextHolder keyHolder;

	public GameObject global;

	public GameObject local;

	public UIButton compareType;

	public UIButton globalToggle;

	public GameObject[] compareModes;

	protected void Awake()
	{
		compareType.Click += OnCompareChanged;
		globalToggle.Click += OnGlobalToggle;
		keyHolder.TextChanged += OnKeyChanged;
		valueHolder.ValueChanged += OnThresholdChanged;
	}

	protected override void Init()
	{
		keyHolder.useBackgroundWidthAsLimit = false;
		keyHolder.CharLimit = 13;
	}

	private void OnGlobalToggle()
	{
		logic.varGlobal = !logic.varGlobal;
		if (hasHandler)
		{
			editLogicHandler.OnEditLogic(logic);
		}
	}

	private void OnKeyChanged(string newKey)
	{
		string result;
		if (EventContainer.SanatizeKey(newKey, out result))
		{
			logic.varKey = result;
			if (hasHandler)
			{
				editLogicHandler.OnEditLogic(logic);
			}
		}
		else
		{
			keyHolder.SetText(logic.varKey);
		}
	}

	private void OnCompareChanged()
	{
		if (logic.varCompare == EntityLogic.VarCompareType.Equals)
		{
			logic.varCompare = EntityLogic.VarCompareType.EqualsOrHigher;
		}
		else if (logic.varCompare == EntityLogic.VarCompareType.EqualsOrHigher)
		{
			logic.varCompare = EntityLogic.VarCompareType.Higher;
		}
		else if (logic.varCompare == EntityLogic.VarCompareType.Higher)
		{
			logic.varCompare = EntityLogic.VarCompareType.Less;
		}
		else if (logic.varCompare == EntityLogic.VarCompareType.Less)
		{
			logic.varCompare = EntityLogic.VarCompareType.EqualsOrLess;
		}
		else
		{
			logic.varCompare = EntityLogic.VarCompareType.Equals;
		}
		if (hasHandler)
		{
			editLogicHandler.OnEditLogic(logic);
		}
	}

	private void OnThresholdChanged(float newThreshold)
	{
		if (isEditing)
		{
			logic.varThreshold = newThreshold;
			if (hasHandler)
			{
				editLogicHandler.OnEditLogic(logic);
			}
		}
	}

	protected void OnDestroy()
	{
		valueHolder.ValueChanged -= OnThresholdChanged;
		globalToggle.Click -= OnGlobalToggle;
		keyHolder.TextChanged -= OnKeyChanged;
	}

	protected override void UpdateVisual()
	{
		string varKey = logic.varKey;
		if (string.IsNullOrEmpty(varKey))
		{
			keyHolder.SetText(varKey);
		}
		else
		{
			WorkshopManager.VerifyString(varKey, delegate(WorkshopManager.VerifyStringResult res, string str)
			{
				if (keyHolder != null)
				{
					keyHolder.SetText(str);
				}
			});
		}
		int varCompare = (int)logic.varCompare;
		for (int num = 0; num < compareModes.Length; num++)
		{
			compareModes[num].SetActive(num == varCompare);
		}
		global.SetActive(logic.varGlobal);
		local.SetActive(!logic.varGlobal);
		valueHolder.SetText(logic.varThreshold);
	}
}
