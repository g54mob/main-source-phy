using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;

public abstract class TerrainModifierEntity : GenericEntity
{
	protected TerrainModifierController modifierController;

	[SerializeField]
	[Header("Terrain Modifier Settings")]
	private GameObject hideOnPlay;

	private MMenu blendModeMenu;

	private static string MODIFIER_PREFIX = "tme-";

	public TerrainModifierController Controller
	{
		get
		{
			return modifierController;
		}
	}

	public virtual int BrushIndex { get; set; }

	public ModifierBlendMode BlendMode { get; private set; }

	public ModifierBlendMode LastBlendMode { get; private set; }

	public abstract TerrainModifierType ModifierType { get; }

	public int BrushSize { get; protected set; }

	public Vector3 Position { get; protected set; }

	public bool IsDirty { get; protected set; }

	public Vector3 LastPosition { get; protected set; }

	public int LastBrushSize { get; protected set; }

	public override void Init()
	{
		if (!isInitialized)
		{
			base.Init();
			LastBrushSize = BrushSize;
			LastPosition = Position;
			LevelEnvironment env = LevelEditor.Instance.environmentManager.GetEnv(LevelSettings.LevelEnvironment.Water);
			if (env != null)
			{
				ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulate));
				modifierController = env.envParent.GetComponentInChildren<TerrainModifierController>();
				modifierController.RegisterModifier(this);
				blendModeMenu = AddMenu(MODIFIER_PREFIX + "blend-mode", 0, new List<string>
				{
					LocalisationManager.GetTranslation(4577),
					LocalisationManager.GetTranslation(4578),
					LocalisationManager.GetTranslation(4579)
				});
				blendModeMenu.ValueChanged += OnBlendModeChanged;
				UpdateEntityTransform();
			}
		}
	}

	public override void OnRemove()
	{
		base.OnRemove();
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnLevelSimulate));
		modifierController.UnregisterModifier(this);
	}

	protected virtual void OnTransformChanged()
	{
		LastPosition = Position;
		LastBrushSize = BrushSize;
		UpdateEntityTransform();
		modifierController.EntityUpdated(this);
	}

	protected virtual void UpdateEntityTransform()
	{
		Position = base.transform.position;
		Vector3 scale = entity.Scale;
		BrushSize = Mathf.CeilToInt(Mathf.Max(scale.x, scale.y, scale.z));
	}

	public void SetDirty(bool dirty)
	{
		IsDirty = dirty;
		if (!dirty)
		{
			LastPosition = Position;
			LastBrushSize = BrushSize;
		}
	}

	public override void UpdateOnTransformEvent()
	{
		OnTransformChanged();
	}

	public override void OnPositionChanged(Vector3 pos)
	{
		OnTransformChanged();
	}

	public override void OnRotationChanged(Quaternion rot)
	{
		OnTransformChanged();
	}

	public override void OnScaleChanged(Vector3 scale)
	{
		OnTransformChanged();
	}

	private void OnBlendModeChanged(int value)
	{
		LastBlendMode = BlendMode;
		if (value != (int)BlendMode)
		{
			BlendMode = (ModifierBlendMode)value;
			modifierController.EntityBlendModeUpdate(this);
		}
	}

	private void OnLevelSimulate(bool toggle)
	{
		hideOnPlay.SetActive(!toggle);
	}
}
