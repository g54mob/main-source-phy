using System;
using BlockMapperInternal;
using Localisation;
using UnityEngine;

public class PickWidget : ParameterWidget
{
	public bool supportsBlocks;

	public Action<PickWidget> onPickDone;

	public int mask = -1;

	public GameObject pickIcon;

	public DynamicText targetText;

	protected MeshRenderer targetRen;

	private string defaultText;

	private Color defaultColour;

	protected Color generalColour;

	private UIButton pickerButton;

	private TriggerTarget pickTarget;

	private bool entityType;

	private StatMaster.Mode.PickMode pickMode = StatMaster.Mode.PickMode.Entity;

	private MText entityName;

	private GenericEntity entityBehaviour;

	private bool trackEntity;

	private bool entityHighlight;

	private Camera hudCam;

	private Collider area;

	private bool isPicking;

	private bool mouseOver;

	public bool Hovered
	{
		get
		{
			return mouseOver;
		}
	}

	public void SetDefaultText(string def, Color col)
	{
		defaultText = def;
		defaultColour = col;
	}

	protected void Awake()
	{
		defaultText = LocalisationManager.GetTranslation(1637);
		pickerButton = GetComponent<UIButton>();
		pickerButton.Click += OnPickClicked;
		hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		area = base.gameObject.GetComponent<Collider>();
		targetRen = targetText.GetComponent<MeshRenderer>();
		generalColour = targetRen.material.color;
	}

	protected void LateUpdate()
	{
		if (isPicking && (InputManager.CloseKey() || InputManager.RotateCameraKey()))
		{
			OnCancelPick();
			return;
		}
		if (!UIMask.InsideMask(mask, base.transform.position))
		{
			ToggleHover(false);
			return;
		}
		Vector2 vector = InputManager.CursorPosition();
		Vector3 vector2 = hudCam.ScreenToWorldPoint(new Vector3(vector.x, vector.y, 10f));
		Bounds bounds = area.bounds;
		bool flag = vector2.x > bounds.min.x && vector2.x < bounds.max.x && vector2.y > bounds.min.y && vector2.y < bounds.max.y;
		ToggleHover(flag);
		if (flag && trackEntity && InputManager.FocusCameraKey())
		{
			SingleInstanceFindOnly<MouseOrbit>.Instance.SetTarget(entityBehaviour);
		}
	}

	protected void ToggleHover(bool toggle)
	{
		if (mouseOver == toggle)
		{
			return;
		}
		if (toggle)
		{
			if (trackEntity)
			{
				if (entityBehaviour.prefab.ignoreOutline && entityBehaviour.hasBoundingBox)
				{
					entityBehaviour.boundingBox.Toggle(true);
				}
				else
				{
					entityBehaviour.UpdateMaterial(ReferenceMaster.Instance.BMReferenceMaterial);
				}
				entityHighlight = true;
			}
		}
		else
		{
			RemoveEntityHighlight();
		}
		mouseOver = toggle;
	}

	protected void OnDisable()
	{
		ToggleHover(false);
	}

	protected void OnDestroy()
	{
		ResetTrackEntity();
	}

	protected void RemoveEntityHighlight()
	{
		if (entityHighlight)
		{
			if (entityBehaviour.prefab.ignoreOutline && entityBehaviour.hasBoundingBox)
			{
				entityBehaviour.boundingBox.Toggle(false);
			}
			else
			{
				entityBehaviour.RestoreMaterial();
			}
			entityHighlight = false;
		}
	}

	public void Init(TriggerTarget pick)
	{
		pickTarget = pick;
		UpdateVisual();
	}

	public void ToggleEntityType(bool toggle)
	{
		entityType = toggle;
	}

	public void TogglePickMode(StatMaster.Mode.PickMode mode)
	{
		pickMode = mode;
		UpdateVisual();
	}

	private void OnPickClicked()
	{
		if (StatMaster.Mode.pickMode == StatMaster.Mode.PickMode.None)
		{
			OnStartPick();
		}
		else
		{
			OnCancelPick();
		}
	}

	public void OnStartPick()
	{
		StatMaster.Mode.pickMode = pickMode;
		pickIcon.SetActive(false);
		isPicking = true;
		BlockMapper currentInstance = BlockMapper.CurrentInstance;
		currentInstance.PickTarget = this;
		currentInstance.PickSupportsBlocks = supportsBlocks;
	}

	protected void OnCancelPick()
	{
		if (isPicking)
		{
			StatMaster.Mode.pickMode = StatMaster.Mode.PickMode.None;
			if (pickIcon != null)
			{
				pickIcon.SetActive(true);
			}
			isPicking = false;
		}
	}

	public override void Pick(GameObject obj)
	{
		pickTarget.type = TriggerTargetObjectType.All;
		if (obj != null)
		{
			LevelEntity component = obj.GetComponent<LevelEntity>();
			if (component != null)
			{
				pickTarget.type = TriggerTargetObjectType.Entity;
				pickTarget.IsEntityType = false;
				entityType = false;
				pickTarget.PrefabID = component.behaviour.prefab.ID;
				pickTarget.EntityID = component.identifier;
			}
			else if (supportsBlocks)
			{
				BlockBehaviour component2 = obj.GetComponent<BlockBehaviour>();
				if (component2 != null)
				{
					pickTarget.type = TriggerTargetObjectType.Block;
					pickTarget.TargetBlockType = component2.Prefab.Type;
				}
			}
		}
		OnCancelPick();
		UpdateVisual();
		if (onPickDone != null)
		{
			onPickDone(this);
		}
	}

	public void ResetPick()
	{
		OnCancelPick();
		pickTarget.type = TriggerTargetObjectType.All;
		UpdateVisual();
	}

	private void ResetTrackEntity()
	{
		if (trackEntity)
		{
			RemoveEntityHighlight();
			entityBehaviour.OnRemoved -= OnEntityChanged;
			entityBehaviour.OnAdded -= OnEntityChanged;
			entityBehaviour.OnChanged -= OnEntityChanged;
			if (entityName != null)
			{
				entityName.TextChanged -= OnEntityNameChanged;
			}
			trackEntity = false;
		}
	}

	private void OnEntityChanged()
	{
		UpdateVisual();
	}

	private void OnEntityNameChanged(string newName)
	{
		UpdateVisual();
	}

	public void UpdateVisual()
	{
		ResetTrackEntity();
		string text = null;
		Color color = generalColour;
		Color color2 = new Color(generalColour.r, generalColour.g, generalColour.b, 0.62f);
		char c = '«';
		char c2 = '»';
		if (pickTarget.type == TriggerTargetObjectType.All)
		{
			color = defaultColour;
			text = c + defaultText + c2;
		}
		else if (pickTarget.type == TriggerTargetObjectType.Entity)
		{
			LevelEntity entity;
			if (entityType)
			{
				LevelPrefab prefab;
				if (LevelEditor.Instance.GetPrefab(pickTarget.PrefabID, out prefab))
				{
					text = LocalisationManager.GetTranslation(prefab.LocalisationID);
				}
				else
				{
					color = color2;
					text = c + "UNKNOWN" + c2;
				}
			}
			else if (LevelEditor.Instance.Get(pickTarget.EntityID, out entity))
			{
				entityBehaviour = entity.behaviour;
				entityBehaviour.OnRemoved += OnEntityChanged;
				entityBehaviour.OnAdded += OnEntityChanged;
				entityBehaviour.OnChanged += OnEntityChanged;
				entityName = entityBehaviour.logicName;
				if (entityName != null)
				{
					entityName.TextChanged += OnEntityNameChanged;
					text = ReferenceMaster.CamelCaseToSpaces(entityName.Value).ToUpper();
				}
				else
				{
					text = entity.LogicName();
				}
				trackEntity = true;
			}
			else
			{
				color = color2;
				text = c + "MISSING" + c2;
			}
		}
		else if (pickTarget.type == TriggerTargetObjectType.Block)
		{
			BlockBehaviour block;
			if (PrefabMaster.GetBlock(pickTarget.TargetBlockType, out block))
			{
				text = ReferenceMaster.TranslateBlockName(block.Prefab.Type);
			}
			else
			{
				color = color2;
				text = c + "ERROR" + c2;
				Debug.LogError(string.Concat("Couldn't find block type ", pickTarget.TargetBlockType, "!"));
			}
		}
		if (targetRen != null && targetRen.material != null)
		{
			targetRen.material.color = color;
		}
		ReferenceMaster.SetDynamicText(targetText, text);
	}
}
