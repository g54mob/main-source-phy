using Localisation;
using UnityEngine;

public class TargetPicker : LogicWidget
{
	public UIButton trashButton;

	public UIButton typeButton;

	public UIButton prevButton;

	public UIButton nextButton;

	public DynamicText targetTypeText;

	public Texture typeTexture;

	public Texture instanceTexture;

	public PickWidget pickerWidget;

	public TeamButton teamButton;

	private UIButton teamBtn;

	private MeshRenderer typeButtonRenderer;

	private TriggerTarget triggerTarget;

	private bool updateCallback;

	protected void Awake()
	{
		teamBtn = teamButton.GetComponent<UIButton>();
		teamBtn.Click += OnTeamChange;
		typeButton.Click += OnTypeToggle;
		typeButtonRenderer = typeButton.GetComponent<MeshRenderer>();
		trashButton.Click += OnRemove;
		prevButton.Click += OnPrev;
		nextButton.Click += OnNext;
	}

	private void OnTeamChange()
	{
		teamButton.NextTeam();
		triggerTarget.Team = teamButton.Team;
		UpdateVisual();
		OnEditTarget();
	}

	protected override void Init()
	{
		if (updateCallback)
		{
			if (triggerTarget != null)
			{
				triggerTarget.TargetChanged -= OnTargetChange;
			}
			updateCallback = false;
		}
		triggerTarget = logic.targets[index];
		if (triggerTarget != null)
		{
			triggerTarget.TargetChanged += OnTargetChange;
			updateCallback = true;
		}
		pickerWidget.ToggleEntityType(triggerTarget.IsEntityType);
		pickerWidget.SetDefaultText(LocalisationManager.GetTranslation(3287), new Color(1f, 1f, 1f, 0.62f));
		pickerWidget.Init(triggerTarget);
		pickerWidget.onPickDone = OnPickDone;
		teamButton.SetTeam(triggerTarget.Team);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (updateCallback)
		{
			if (triggerTarget != null)
			{
				triggerTarget.TargetChanged -= OnTargetChange;
			}
			updateCallback = false;
		}
	}

	protected void OnDestroy()
	{
		typeButton.Click -= OnTypeToggle;
		trashButton.Click -= OnRemove;
		prevButton.Click -= OnPrev;
		nextButton.Click -= OnNext;
	}

	private void OnTargetChange()
	{
		pickerWidget.ToggleEntityType(triggerTarget.IsEntityType);
		UpdateVisual();
	}

	private void OnTypeToggle()
	{
		triggerTarget.IsEntityType = !triggerTarget.IsEntityType;
		pickerWidget.ToggleEntityType(triggerTarget.IsEntityType);
		pickerWidget.UpdateVisual();
		UpdateTypeVisual();
		OnEditTarget();
	}

	private void OnPickDone(PickWidget widget)
	{
		bool flag = triggerTarget.targetType == TriggerTargetType.Picker;
		typeButton.gameObject.SetActive(flag && triggerTarget.type == TriggerTargetObjectType.Entity);
		UpdateTypeVisual();
		OnEditTarget();
	}

	private void UpdateTypeVisual()
	{
		typeButtonRenderer.material.mainTexture = ((!triggerTarget.IsEntityType) ? instanceTexture : typeTexture);
	}

	private void OnPrev()
	{
		int targetType = (int)triggerTarget.targetType;
		triggerTarget.targetType = (TriggerTargetType)((targetType <= 0) ? (triggerTarget.targetTypeCount - 1) : (targetType - 1));
		UpdateVisual();
		OnEditTarget();
	}

	private void OnNext()
	{
		int targetType = (int)triggerTarget.targetType;
		triggerTarget.targetType = ((targetType + 1 < triggerTarget.targetTypeCount) ? ((TriggerTargetType)(targetType + 1)) : TriggerTargetType.Anything);
		UpdateVisual();
		OnEditTarget();
	}

	public void Refresh()
	{
		UpdateVisual();
	}

	protected override void UpdateVisual()
	{
		bool flag = triggerTarget.targetType == TriggerTargetType.Picker;
		targetTypeText.gameObject.SetActive(!flag);
		pickerWidget.gameObject.SetActive(flag);
		typeButton.gameObject.SetActive(flag && triggerTarget.type == TriggerTargetObjectType.Entity);
		if (flag)
		{
			pickerWidget.UpdateVisual();
			if (triggerTarget.type == TriggerTargetObjectType.Entity)
			{
				UpdateTypeVisual();
			}
		}
		else
		{
			targetTypeText.SetText(ReferenceMaster.CamelCaseToSpaces(GetString(triggerTarget.targetType)).ToUpper());
		}
		bool hasTeam = triggerTarget.hasTeam;
		teamButton.gameObject.SetActive(hasTeam);
		if (hasTeam)
		{
			teamButton.SetTeam(triggerTarget.Team);
		}
	}

	protected string GetString(TriggerTargetType type)
	{
		string result = string.Empty;
		switch (type)
		{
		case TriggerTargetType.Anything:
			result = LocalisationManager.GetTranslation(3256);
			break;
		case TriggerTargetType.AnyBlock:
			result = LocalisationManager.GetTranslation(3257);
			break;
		case TriggerTargetType.AnyProjectile:
			result = LocalisationManager.GetTranslation(3279);
			break;
		case TriggerTargetType.AnyLevelObject:
			result = LocalisationManager.GetTranslation(3258);
			break;
		case TriggerTargetType.Picker:
			result = LocalisationManager.GetTranslation(3259);
			break;
		}
		return result;
	}

	private void OnEditTarget()
	{
		if (hasHandler)
		{
			editLogicHandler.OnEditTarget(logic, triggerTarget);
		}
	}

	private void OnRemove()
	{
		if (logic.targets.Count == 1)
		{
			if (triggerTarget.targetType == TriggerTargetType.Picker)
			{
				triggerTarget.type = TriggerTargetObjectType.All;
			}
			else
			{
				triggerTarget.targetType = TriggerTargetType.Anything;
			}
			UpdateVisual();
			OnEditTarget();
		}
		else
		{
			logicSelector.OnRemoveTarget(index);
		}
	}

	protected override void ToggleHover(bool toggle)
	{
		nextButton.gameObject.SetActive(toggle);
		prevButton.gameObject.SetActive(toggle);
		trashButton.gameObject.SetActive(toggle);
	}
}
