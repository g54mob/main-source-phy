using Localisation;
using UnityEngine;

public class EventPickWidget : MonoBehaviour
{
	[HideInInspector]
	public int ID;

	public PickWidget pickWidget;

	public UIButton trashButton;

	private TriggerTarget pickTarget;

	private EntityLogic logic;

	private PickEventDisplay display;

	private EntityEvent entityEvent;

	private bool isZone;

	protected void Awake()
	{
		pickWidget.onPickDone = OnPickDone;
		trashButton.Click += OnResetPicker;
	}

	public void Refresh()
	{
		pickWidget.UpdateVisual();
	}

	protected void Update()
	{
		GameObject gameObject = trashButton.gameObject;
		if (pickWidget.Hovered && ID < entityEvent.entityList.Count && entityEvent.entityList[ID] != LevelPrefab.INVALID_ID)
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

	public void SetPickMode(StatMaster.Mode.PickMode mode)
	{
		pickWidget.TogglePickMode(mode);
		isZone = mode == StatMaster.Mode.PickMode.Zone;
		UpdateVisual();
	}

	public void Init(PickEventDisplay pickDisplay, EntityLogic l, EntityEvent evt, int index)
	{
		display = pickDisplay;
		entityEvent = evt;
		logic = l;
		ID = index;
		isZone = false;
		if (entityEvent != null)
		{
			EventContainer.PickContainer pickContainer = entityEvent.eventData as EventContainer.PickContainer;
			if (pickContainer != null)
			{
				while (ID >= pickContainer.pickTargets.Count)
				{
					pickContainer.pickTargets.Add(new TriggerTarget(TriggerTargetType.Picker));
				}
				pickTarget = pickContainer.pickTargets[ID];
				pickWidget.ToggleEntityType(false);
				UpdateVisual();
			}
			else
			{
				Debug.LogError(string.Concat(Machine.GetObjectPath(base.gameObject), ": EventData is ", pickContainer, " instead of PickContainer!"));
			}
		}
		else
		{
			Debug.LogError("Entity event is null while initializing EventPickWidget!");
		}
	}

	public void OnPickDone(PickWidget widget)
	{
		bool flag = pickTarget.type == TriggerTargetObjectType.All;
		long num = ((!flag) ? pickTarget.EntityID : LevelPrefab.INVALID_ID);
		bool flag2 = false;
		if (flag && ID == entityEvent.entityList.Count - 1)
		{
			entityEvent.entityList.RemoveAt(ID);
			flag2 = true;
		}
		else
		{
			if (!flag)
			{
				while (entityEvent.entityList.Count <= ID)
				{
					entityEvent.entityList.Add(LevelPrefab.INVALID_ID);
					flag2 = true;
				}
			}
			if (ID < entityEvent.entityList.Count && entityEvent.entityList[ID] != num)
			{
				entityEvent.entityList[ID] = num;
				flag2 = true;
			}
		}
		if (flag2)
		{
			display.OnEditEvent();
		}
	}

	private void OnResetPicker()
	{
		isZone = false;
		pickWidget.ResetPick();
		entityEvent.entityList.RemoveAt(ID);
		display.OnEditEvent();
	}

	public void UpdateVisual()
	{
		long num = ((ID < entityEvent.entityList.Count) ? entityEvent.entityList[ID] : LevelPrefab.INVALID_ID);
		if (num == LevelPrefab.INVALID_ID)
		{
			pickTarget.type = TriggerTargetObjectType.All;
		}
		else if (pickTarget.EntityID != num)
		{
			LevelEntity entity;
			if (LevelEditor.Instance.Get(num, out entity))
			{
				pickTarget.type = TriggerTargetObjectType.Entity;
				pickTarget.PrefabID = entity.behaviour.prefab.ID;
				pickTarget.EntityID = num;
			}
			else
			{
				pickTarget.type = TriggerTargetObjectType.All;
			}
		}
		Color col = new Color(1f, 1f, 1f, 0.62f);
		string def;
		if (!isZone || !logic.UseTriggerResult(entityEvent, true))
		{
			def = ((!logic.UseSelf(entityEvent)) ? LocalisationManager.GetTranslation(3287) : ((!logic.IsVarEvent(entityEvent.eventType)) ? LocalisationManager.GetTranslation(3286) : LocalisationManager.GetTranslation(3285)));
		}
		else
		{
			col = Color.white;
			def = LocalisationManager.GetTranslation(3284);
		}
		pickWidget.SetDefaultText(def, col);
		pickWidget.Init(pickTarget);
	}
}
