using System.Collections.Generic;
using Selectors;
using UnityEngine;

public class TransformEventDisplay : PickEventDisplay
{
	public ValueHolder[] PosHolders;

	public ValueHolder[] RotHolders;

	public ValueHolder LerpHolder;

	public ValueHolder ForceHolder;

	private EventContainer.TransformEvent transformEvent;

	[SerializeField]
	private UIButton positionMode;

	[SerializeField]
	private UIButton rotationMode;

	[SerializeField]
	private UIButton prevTransformTypes;

	[SerializeField]
	private UIButton nextTransformTypes;

	[SerializeField]
	private GameObject[] transformTypes;

	[SerializeField]
	private GameObject[] positionTypes;

	[SerializeField]
	private GameObject[] rotationTypes;

	[SerializeField]
	private Material lineMaterial;

	private float distance;

	private List<EventContainer.TransformEvent> transformEvents;

	private EventContainer.TransformEvent tempEvent;

	private LevelEntity pickerEntity;

	private Vector3 startPosition;

	protected override void Awake()
	{
		base.Awake();
		if (PosHolders.Length == 3)
		{
			PosHolders[0].ValueChanged += OnPosX;
			PosHolders[1].ValueChanged += OnPosY;
			PosHolders[2].ValueChanged += OnPosZ;
			RotHolders[0].ValueChanged += OnRotX;
			RotHolders[1].ValueChanged += OnRotY;
			RotHolders[2].ValueChanged += OnRotZ;
			LerpHolder.ValueChanged += OnLerpChange;
			ForceHolder.ValueChanged += OnForceChange;
			positionMode.Click += ChangePositionMode;
			rotationMode.Click += ChangeRotationMode;
			prevTransformTypes.Click += OnPrevClicked;
			nextTransformTypes.Click += OnNextClicked;
		}
	}

	public override void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
	{
		base.Init(parentWidget, inLogic, inEvent);
		LerpHolder.hideTextOnLock = true;
		UpdateVisual();
	}

	public override void UpdateVisual()
	{
		transformEvent = currentEvent.eventData as EventContainer.TransformEvent;
		if (isEditing && transformEvent != null)
		{
			Vector3 position = transformEvent.position;
			Vector3 eulerAngles = transformEvent.eulerAngles;
			for (int i = 0; i < 3; i++)
			{
				PosHolders[i].SetText(position[i]);
				RotHolders[i].SetText(eulerAngles[i]);
			}
			LerpHolder.SetText(transformEvent.lerpTime);
			ForceHolder.SetText(transformEvent.lerpTime);
			int transformType = (int)transformEvent.transformType;
			for (int j = 0; j < transformTypes.Length; j++)
			{
				transformTypes[j].SetActive(j == transformType);
			}
			int rotationType = (int)transformEvent.rotationType;
			for (int k = 0; k < rotationTypes.Length; k++)
			{
				rotationTypes[k].SetActive(k == rotationType);
			}
			int positionType = (int)transformEvent.positionType;
			for (int l = 0; l < positionTypes.Length; l++)
			{
				positionTypes[l].SetActive(l == positionType);
			}
			base.UpdateVisual();
			UpdateBackground();
		}
	}

	public void Update()
	{
		if (transformEvent.entityEvent.entityList == null)
		{
			return;
		}
		transformEvents = new List<EventContainer.TransformEvent>();
		if (currentLogic.events.Count <= 1)
		{
			return;
		}
		for (int i = 0; i < currentLogic.events.Count; i++)
		{
			tempEvent = currentLogic.events[i].eventData as EventContainer.TransformEvent;
			if (!object.ReferenceEquals(tempEvent, null))
			{
				if (tempEvent == transformEvent)
				{
					break;
				}
				transformEvents.Add(tempEvent);
			}
		}
	}

	private void OnPosX(float posX)
	{
		OnPos(posX, 0);
	}

	private void OnPosY(float posY)
	{
		OnPos(posY, 1);
	}

	private void OnPosZ(float posZ)
	{
		OnPos(posZ, 2);
	}

	private void OnRotX(float rotX)
	{
		OnRot(rotX, 0);
	}

	private void OnRotY(float rotY)
	{
		OnRot(rotY, 1);
	}

	private void OnRotZ(float rotZ)
	{
		OnRot(rotZ, 2);
	}

	private void OnPos(float val, int axis)
	{
		if (isEditing)
		{
			Vector3 position = transformEvent.position;
			transformEvent.position = new Vector3((axis != 0) ? position.x : val, (axis != 1) ? position.y : val, (axis != 2) ? position.z : val);
			OnEditEvent();
		}
	}

	private void OnRot(float val, int axis)
	{
		if (isEditing)
		{
			transformEvent.eulerAngles[axis] = val;
			transformEvent.rotation = Quaternion.Euler(transformEvent.eulerAngles);
			OnEditEvent();
		}
	}

	private void OnLerpChange(float val)
	{
		if (isEditing)
		{
			transformEvent.lerpTime = val;
			OnEditEvent();
		}
	}

	private void OnForceChange(float val)
	{
		if (isEditing)
		{
			transformEvent.lerpTime = val;
			OnEditEvent();
		}
	}

	protected override void UpdateBackground()
	{
		backgroundTransform.localScale = new Vector3(backgroundTransform.localScale.x, defaultHeight + 1f + (float)pickWidgets.Count * pickSpacer, backgroundTransform.localScale.z);
		UpdateBottomLine();
	}

	private void OnNextClicked()
	{
		transformEvent.transformType = ((transformEvent.transformType < EventContainer.TransformEvent.TransformType.Force) ? (transformEvent.transformType + 1) : EventContainer.TransformEvent.TransformType.Instant);
		OnEditEvent();
	}

	private void OnPrevClicked()
	{
		transformEvent.transformType = ((transformEvent.transformType <= EventContainer.TransformEvent.TransformType.Instant) ? EventContainer.TransformEvent.TransformType.Force : (transformEvent.transformType - 1));
		OnEditEvent();
	}

	private void ChangePositionMode()
	{
		transformEvent.positionType = ((transformEvent.positionType < EventContainer.TransformEvent.TransformPositionType.LocalDirection) ? (transformEvent.positionType + 1) : EventContainer.TransformEvent.TransformPositionType.WorldPosition);
		OnEditEvent();
	}

	private void ChangeRotationMode()
	{
		transformEvent.rotationType = ((transformEvent.rotationType < EventContainer.TransformEvent.TransformRotationType.SetRotation) ? (transformEvent.rotationType + 1) : EventContainer.TransformEvent.TransformRotationType.AroundWorldAxis);
		OnEditEvent();
	}
}
