using InternalModding.Loading;
using Localisation;
using UnityEngine;

public class LogicHeader : LogicWidget
{
	public DynamicText headerText;

	public UIButton trashButton;

	public UIButton prevButton;

	public UIButton nextButton;

	public GameObject subTitle;

	public GameObject subTitleAI;

	public GameObject infoIcon;

	public UIButtonExtended simStartToggle;

	[SerializeField]
	protected Texture simStartEnabled;

	[SerializeField]
	protected Texture simStartDisabled;

	protected void Awake()
	{
		trashButton.Click += OnRemove;
		prevButton.Click += OnPrev;
		nextButton.Click += OnNext;
		simStartToggle.Click += ToggleActivateSimTriggerState;
	}

	protected override void Init()
	{
		bool active = entityBehaviour.TriggerTypeCount() > 1;
		prevButton.gameObject.SetActive(active);
		nextButton.gameObject.SetActive(active);
	}

	private void OnRemove()
	{
		logicSelector.OnRemove();
	}

	private void OnPrev()
	{
		logic.triggerType = entityBehaviour.PreviousTriggerType(logic.triggerType);
		if (logic.triggerType == TriggerType.Modded && logic.moddedTriggerType == null)
		{
			logic.moddedTriggerType = ModIds.GetTriggerByEffectiveId(entityBehaviour.entity.behaviour.prefab.moddedEvents[0]);
		}
		UpdateVisual();
		if (hasHandler)
		{
			editLogicHandler.OnEditLogic(logic);
		}
	}

	private void OnNext()
	{
		logic.triggerType = entityBehaviour.NextTriggerType(logic.triggerType);
		if (logic.triggerType == TriggerType.Modded && logic.moddedTriggerType == null)
		{
			logic.moddedTriggerType = ModIds.GetTriggerByEffectiveId(entityBehaviour.entity.behaviour.prefab.moddedEvents[0]);
		}
		UpdateVisual();
		if (hasHandler)
		{
			editLogicHandler.OnEditLogic(logic);
		}
	}

	private string GetHeaderString(TriggerType triggerType)
	{
		string empty = string.Empty;
		switch (triggerType)
		{
		case TriggerType.Start:
			return entityBehaviour.GetStartString();
		case TriggerType.End:
			return entityBehaviour.GetEndString();
		default:
			return ReferenceMaster.TranslateTriggerType(triggerType);
		}
	}

	protected override void UpdateVisual()
	{
		string text = string.Format("{0} {1}", LocalisationManager.GetTranslation(2839), ReferenceMaster.CamelCaseToSpaces(GetHeaderString(logic.triggerType)).ToUpper());
		if (logic.triggerType == TriggerType.Behaviour)
		{
			subTitle.SetActive(false);
			subTitleAI.SetActive(true);
			headerText.SetText(ReferenceMaster.TranslateTriggerType(logic.triggerType).ToUpper());
		}
		else
		{
			subTitle.SetActive(true);
			subTitleAI.SetActive(false);
			headerText.SetText(text);
		}
		switch (logic.triggerType)
		{
		case TriggerType.Behaviour:
			simStartToggle.gameObject.SetActive(false);
			subTitle.SetActive(false);
			subTitleAI.SetActive(true);
			headerText.SetText(ReferenceMaster.TranslateTriggerType(logic.triggerType).ToUpper());
			break;
		case TriggerType.Activate:
			simStartToggle.gameObject.SetActive(true);
			simStartToggle.icon.material.mainTexture = ((!logic.simStartTrigger) ? simStartDisabled : simStartEnabled);
			subTitle.SetActive(true);
			subTitleAI.SetActive(false);
			headerText.SetText(text);
			break;
		default:
			simStartToggle.gameObject.SetActive(false);
			subTitle.SetActive(true);
			subTitleAI.SetActive(false);
			headerText.SetText(text);
			break;
		}
	}

	protected void ToggleActivateSimTriggerState()
	{
		logic.simStartTrigger = !logic.simStartTrigger;
		UpdateVisual();
		if (hasHandler)
		{
			editLogicHandler.OnEditLogic(logic);
		}
	}

	protected override void ToggleHover(bool toggle)
	{
		TriggerType triggerType = logic.triggerType;
		if (triggerType == TriggerType.Behaviour)
		{
			infoIcon.SetActive(toggle);
			toggle = false;
		}
		else
		{
			infoIcon.SetActive(false);
		}
		nextButton.gameObject.SetActive(toggle);
		prevButton.gameObject.SetActive(toggle);
		trashButton.gameObject.SetActive(toggle);
	}
}
