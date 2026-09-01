using System;
using Localisation;
using UnityEngine;

public class SetRespawnEventDisplay : MachineEventDisplay
{
	public PickWidget targetPicker;

	public UIButton targetTrash;

	private TriggerTarget triggerTarget;

	private EventContainer.SetRespawnEvent respawnData;

	protected override void Awake()
	{
		base.Awake();
		targetTrash.Click += ResetTargetPicker;
		PickWidget obj = targetPicker;
		obj.onPickDone = (Action<PickWidget>)Delegate.Combine(obj.onPickDone, new Action<PickWidget>(OnTargetChange));
	}

	private void ResetTargetPicker()
	{
		targetPicker.ResetPick();
		respawnData.zoneTarget = LevelPrefab.INVALID_ID;
		OnEditEvent();
	}

	public override void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
	{
		respawnData = inEvent.eventData as EventContainer.SetRespawnEvent;
		CreateTargetPicker(inEvent);
		base.Init(parentWidget, inLogic, inEvent);
	}

	protected void LateUpdate()
	{
		GameObject gameObject = targetTrash.gameObject;
		if (targetPicker.Hovered && respawnData.zoneTarget != LevelPrefab.UNASSIGNED_ID)
		{
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
		}
		else if (gameObject.activeSelf)
		{
			gameObject.SetActive(false);
		}
	}

	public override void Refresh()
	{
		base.Refresh();
		targetPicker.UpdateVisual();
	}

	public override void UpdateVisual()
	{
		if (isEditing)
		{
			targetPicker.UpdateVisual();
			base.UpdateVisual();
		}
	}

	public void CreateTargetPicker(EntityEvent entityEvent)
	{
		if (entityEvent != null)
		{
			respawnData = entityEvent.eventData as EventContainer.SetRespawnEvent;
			if (respawnData != null)
			{
				if (triggerTarget == null)
				{
					triggerTarget = new TriggerTarget(TriggerTargetType.Picker);
				}
				if (respawnData.zoneTarget != LevelPrefab.UNASSIGNED_ID)
				{
					triggerTarget.type = TriggerTargetObjectType.Entity;
					triggerTarget.EntityID = respawnData.zoneTarget;
				}
				else
				{
					triggerTarget.type = TriggerTargetObjectType.All;
				}
				targetPicker.ToggleEntityType(triggerTarget.IsEntityType);
				targetPicker.SetDefaultText(LocalisationManager.GetTranslation(3287), new Color(1f, 1f, 1f, 0.62f));
				targetPicker.Init(triggerTarget);
			}
			else
			{
				Debug.LogError(string.Concat(Machine.GetObjectPath(base.gameObject), ": EventData is ", respawnData, " instead of PickContainer!"));
			}
		}
		else
		{
			Debug.LogError("Entity event is null while initializing TargetPicker!");
		}
	}

	public void OnTargetChange(PickWidget widget)
	{
		if (isEditing)
		{
			(currentEvent.eventData as EventContainer.SetRespawnEvent).zoneTarget = triggerTarget.EntityID;
			OnEditEvent();
		}
	}

	protected override void UpdateBackground()
	{
		backgroundTransform.localScale = new Vector3(backgroundTransform.localScale.x, defaultHeight + 0.35f + (float)pickWidgets.Count * pickSpacer, backgroundTransform.localScale.z);
		UpdateBottomLine();
	}
}
