using System.Collections.Generic;
using UnityEngine;

public class PickEventDisplay : LogicEventDisplay
{
	public EventPickWidget pickWidget;

	public bool pickSingle;

	public StatMaster.Mode.PickMode mode = StatMaster.Mode.PickMode.Entity;

	protected EventContainer.PickContainer pickEvent;

	protected List<EventPickWidget> pickWidgets;

	protected float pickSpacer = 0.4f;

	protected Vector3 pickWidgetPosition;

	private Transform pickWidgetParent;

	private BMWidgetPool.Pool pickerPool;

	private bool hasPool;

	protected override void Awake()
	{
		base.Awake();
		BMWidgetPool instance = BMWidgetPool.Instance;
		if (instance != null)
		{
			pickerPool = instance.GetPool(pickWidget.gameObject);
			hasPool = true;
		}
		else
		{
			Debug.LogError("Couldn't fetch widget pool!");
		}
		pickWidgets = new List<EventPickWidget>();
		pickWidget.gameObject.SetActive(false);
		Transform transform = pickWidget.transform;
		pickWidgetParent = transform.parent;
		pickWidgetPosition = transform.localPosition;
	}

	public virtual void Refresh()
	{
		for (int i = 0; i < pickWidgets.Count; i++)
		{
			pickWidgets[i].Refresh();
		}
	}

	public override void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
	{
		base.Init(parentWidget, inLogic, inEvent);
		UpdateVisual();
	}

	protected virtual void OnDestroy()
	{
	}

	public virtual void OnEditEvent()
	{
		eventWidget.OnEditEvent();
	}

	public override void ResetToPool()
	{
		base.ResetToPool();
		ClearPickers();
	}

	public virtual EventPickWidget AddPicker(int index, long id, bool updateVis)
	{
		GameObject gameObject = ((!hasPool) ? Object.Instantiate(pickWidget.gameObject) : pickerPool.Get());
		Transform transform = gameObject.transform;
		transform.parent = pickWidgetParent;
		transform.localPosition = pickWidgetPosition + Vector3.down * pickSpacer * index;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		EventPickWidget component = gameObject.GetComponent<EventPickWidget>();
		gameObject.SetActive(true);
		component.Init(this, currentLogic, currentEvent, index);
		component.SetPickMode(mode);
		pickWidgets.Add(component);
		return component;
	}

	private void ClearPickers()
	{
		for (int i = 0; i < pickWidgets.Count; i++)
		{
			EventPickWidget eventPickWidget = pickWidgets[i];
			if (hasPool)
			{
				pickerPool.Add(eventPickWidget.gameObject);
			}
			else
			{
				Object.Destroy(eventPickWidget.gameObject);
			}
		}
		pickWidgets.Clear();
	}

	public override void UpdateVisual()
	{
		if (isEditing)
		{
			ClearPickers();
			for (int i = 0; i < currentEvent.entityList.Count; i++)
			{
				AddPicker(i, currentEvent.entityList[i], true);
			}
			if (!pickSingle || currentEvent.entityList.Count == 0)
			{
				AddPicker(currentEvent.entityList.Count, LevelPrefab.INVALID_ID, true);
			}
			UpdateBackground();
		}
	}

	protected override void UpdateBackground()
	{
		backgroundTransform.localScale = new Vector3(backgroundTransform.localScale.x, defaultHeight - pickSpacer + (float)pickWidgets.Count * pickSpacer, backgroundTransform.localScale.z);
		UpdateBottomLine();
	}
}
