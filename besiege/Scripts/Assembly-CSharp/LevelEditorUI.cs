using System;
using System.Collections.Generic;
using System.IO;
using InternalModding.LevelEntities;
using Localisation;
using Selectors;
using UnityEngine;

public class LevelEditorUI : SingleInstanceFindOnly<LevelEditorUI>
{
	[Serializable]
	public class StateTools
	{
		public UIButtonExtended Save;

		public UIButtonExtended Load;

		public UIButtonExtended Undo;

		public UIButtonExtended Redo;

		public UIButtonExtended Bin;

		public UIButtonExtended Settings;

		private bool greyed;

		public void Setup()
		{
			ResetButtonBGs();
			Save.ResetDelegates();
			Load.ResetDelegates();
			Undo.ResetDelegates();
			Redo.ResetDelegates();
			Bin.ResetDelegates();
			Settings.ResetDelegates();
			Save.Down += OnSaveDown;
			Save.Released += OnSaveUp;
			Load.Down += OnLoadDown;
			Load.Released += OnLoadUp;
			Undo.Down += OnUndoDown;
			Undo.Released += OnUndoUp;
			Redo.Down += OnRedoDown;
			Redo.Released += OnRedoUp;
			Bin.Down += OnBinDown;
			Bin.Released += OnBinUp;
			Settings.Down += OnSettingsDown;
			Settings.Released += OnSettingsUp;
		}

		public void ResetButtonBGs()
		{
			OnSaveUp();
			OnLoadUp();
			OnUndoUp();
			OnRedoUp();
			OnBinUp();
			OnSettingsUp();
		}

		public void GreyOut(bool b)
		{
			greyed = b;
			if (greyed)
			{
				Save.DisableScripts();
				Load.DisableScripts();
				Undo.DisableScripts();
				Redo.DisableScripts();
				Bin.DisableScripts();
				Save.SetIconAlpha(0.1f);
				Load.SetIconAlpha(0.1f);
				Undo.SetIconAlpha(0.1f);
				Redo.SetIconAlpha(0.1f);
				Bin.SetIconAlpha(0.1f);
			}
			else
			{
				Save.EnableScripts();
				Load.EnableScripts();
				Undo.EnableScripts();
				Redo.EnableScripts();
				Bin.EnableScripts();
				Save.SetIconAlpha(0.5f);
				Load.SetIconAlpha(0.5f);
				Undo.SetIconAlpha(0.5f);
				Redo.SetIconAlpha(0.5f);
				Bin.SetIconAlpha(0.5f);
			}
		}

		public void OnSaveDown()
		{
			if (!greyed)
			{
				Save.BG.SetActive(true);
			}
		}

		public void OnSaveUp()
		{
			Save.BG.SetActive(false);
		}

		public void OnLoadDown()
		{
			if (!greyed)
			{
				Load.BG.SetActive(true);
			}
		}

		public void OnLoadUp()
		{
			Load.BG.SetActive(false);
		}

		public void OnUndoDown()
		{
			if (!greyed)
			{
				Undo.BG.SetActive(true);
				LevelEditor.Instance.Undo();
			}
		}

		public void OnUndoUp()
		{
			Undo.BG.SetActive(false);
		}

		public void OnRedoDown()
		{
			if (!greyed)
			{
				Redo.BG.SetActive(true);
				LevelEditor.Instance.Redo();
			}
		}

		public void OnRedoUp()
		{
			Redo.BG.SetActive(false);
		}

		public void OnBinDown()
		{
			if (!greyed)
			{
				Bin.BG.SetActive(true);
				LevelEditor.Instance.OnClearLevelClicked();
			}
		}

		public void OnBinUp()
		{
			Bin.BG.SetActive(false);
		}

		public void OnSettingsDown()
		{
			Settings.BG.SetActive(true);
			SingleInstanceFindOnly<LevelEditorUI>.Instance.settingsWindow.SetActive(true);
		}

		public void OnSettingsUp()
		{
			Settings.BG.SetActive(false);
		}
	}

	[Serializable]
	public class Options
	{
		public UIButtonMultiState ClientSimControl;

		public UIButtonExtended Global;

		public UIButtonExtended Grid;

		public UIButtonExtended ObjectPivot;

		public UIButtonExtended Paint;

		public UIButtonExtended Duplicate;

		public UIButton EditGrid;

		public UIButtonExtended Linked;

		public GameObject globalSimClientTooltip;

		public GameObject globalSimHostTooltip;

		private bool greyed;

		private float BGscaleY = 12.85f;

		private float CatPosY = -4.75f;

		private float BtnsPosY = -4.75f;

		private float DivFieldHeight = 0.7f;

		private Color linkDefault;

		public void Setup()
		{
			linkDefault = Linked.icon.material.GetColor("_TintColor");
			Global.BG.SetActive(StatMaster.Mode.LevelEditor.global);
			Grid.BG.SetActive(StatMaster.Mode.LevelEditor.grid);
			ObjectPivot.BG.SetActive(StatMaster.Mode.LevelEditor.objectPivot);
			Linked.gameObject.SetActive(!StatMaster.Mode.LevelEditor.global || StatMaster.Mode.LevelEditor.selectedTool == StatMaster.Tool.Rotate);
			Linked.icon.material.SetColor("_TintColor", (!StatMaster.Mode.LevelEditor.linked) ? new Color(1f, 1f, 1f, 0.45f) : linkDefault);
			globalSimClientTooltip.SetActive(StatMaster.isClient);
			globalSimHostTooltip.SetActive(!StatMaster.isClient);
			UpdateLocalSimButton();
			Paint.BG.SetActive(StatMaster.Mode.LevelEditor.paintPlacement);
			Duplicate.BG.SetActive(false);
			ClientSimControl.ResetDelegates();
			Global.ResetDelegates();
			Grid.ResetDelegates();
			ObjectPivot.ResetDelegates();
			Paint.ResetDelegates();
			Duplicate.ResetDelegates();
			EditGrid.ResetDelegates();
			Linked.ResetDelegates();
			ClientSimControl.Down += ToggleLocalSimButton;
			Global.Down += ToggleGlobalTransformation;
			Grid.Down += ToggleGrid;
			ObjectPivot.Down += ToggleObjectPivot;
			Paint.Down += TogglePaintPlacement;
			Duplicate.Down += OnDuplicateDown;
			Duplicate.Released += OnDuplicateUp;
			EditGrid.Down += ToggleDivisionsField;
			Linked.Down += ToggleLinkedTransformation;
			if (StatMaster.advancedBuilding)
			{
				DivisionFields.editingGrid = true;
				UpdateDivisionsField();
			}
		}

		public void GreyOut(bool b)
		{
			greyed = b;
			if (greyed)
			{
				Grid.DisableScripts();
				Global.DisableScripts();
				Duplicate.DisableScripts();
				ObjectPivot.DisableScripts();
				Paint.DisableScripts();
				Grid.SetIconAlpha(0.1f);
				Global.SetIconAlpha(0.1f);
				Duplicate.SetIconAlpha(0.1f);
				ObjectPivot.SetIconAlpha(0.1f);
				Paint.SetIconAlpha(0.1f);
			}
			else
			{
				Grid.EnableScripts();
				Global.EnableScripts();
				Duplicate.EnableScripts();
				ObjectPivot.EnableScripts();
				Paint.EnableScripts();
				Grid.SetIconAlpha(0.5f);
				Global.SetIconAlpha(0.5f);
				Duplicate.SetIconAlpha(0.5f);
				ObjectPivot.SetIconAlpha(0.5f);
				Paint.SetIconAlpha(0.5f);
			}
		}

		public void ToggleLocalSimButton()
		{
			if (StatMaster.isHosting)
			{
				byte[] messageData = new byte[1] { (!StatMaster.Mode.LevelEditor.clientSimControl) ? ((byte)1) : ((byte)0) };
				NetworkAuxAddPiece.Instance.SendServerRequest(RPCMessageType.ToggleSimControl, messageData);
			}
			else if (!StatMaster.waitingForServerResponse)
			{
				ToggleLocalSim();
			}
		}

		public void UpdateLocalSimButton()
		{
			int toState = (StatMaster.IsLevelEditorOnly ? 5 : (StatMaster.isClient ? ((!StatMaster.Mode.LevelEditor.clientSimControl) ? 2 : ((!StatMaster.Mode.LevelEditor.clientGlobalSim) ? 1 : 0)) : (StatMaster.Mode.LevelEditor.clientSimControl ? 4 : 3)));
			ClientSimControl.SetToState(toState);
		}

		public void UpdateClientSimControl(bool clientControl)
		{
			StatMaster.Mode.LevelEditor.clientSimControl = clientControl;
			UpdateLocalSimButton();
		}

		public void ResetLocalSim()
		{
			if (StatMaster.isClient)
			{
				StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.ToggleLocalSim, false);
				NetworkAuxAddPiece.Instance.HideLoadingText();
			}
			StatMaster.Mode.LevelEditor.clientSimControl = (StatMaster.Mode.LevelEditor.clientGlobalSim = true);
			for (int i = 0; i < Playerlist.Players.Count; i++)
			{
				PlayerData playerData = Playerlist.Players[i];
				playerData.inLocalSim = false;
			}
			UpdateLocalSimButton();
		}

		public void ToggleLocalSim()
		{
			bool clientGlobalSim = StatMaster.Mode.LevelEditor.clientGlobalSim;
			byte[] messageData = new byte[1] { (byte)(clientGlobalSim ? 1u : 0u) };
			NetworkAuxAddPiece instance = NetworkAuxAddPiece.Instance;
			instance.SendServerRequest(RPCMessageType.ToggleLocalSim, messageData);
			instance.SetLoadingText(LocalisationManager.GetTranslation((!clientGlobalSim) ? 3373 : 3372));
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.ToggleLocalSim, true);
		}

		public void ToggleGlobalTransformation()
		{
			if (!greyed)
			{
				StatMaster.Mode.LevelEditor.global = !StatMaster.Mode.LevelEditor.global;
				Global.BG.SetActive(StatMaster.Mode.LevelEditor.global);
				Linked.gameObject.SetActive(!StatMaster.Mode.LevelEditor.global || StatMaster.Mode.LevelEditor.selectedTool == StatMaster.Tool.Rotate);
				LevelEditor.Instance.ToggleGlobal();
			}
		}

		public void ToggleLinkedTransformation()
		{
			StatMaster.Mode.LevelEditor.linked = !StatMaster.Mode.LevelEditor.linked;
			Linked.icon.material.SetColor("_TintColor", (!StatMaster.Mode.LevelEditor.linked) ? new Color(1f, 1f, 1f, 0.45f) : linkDefault);
			LevelEditor.Instance.UpdateTool();
		}

		public void ToggleGrid()
		{
			if (!greyed)
			{
				StatMaster.Mode.LevelEditor.grid = !StatMaster.Mode.LevelEditor.grid;
				Grid.BG.SetActive(StatMaster.Mode.LevelEditor.grid);
			}
		}

		public void ToggleObjectPivot()
		{
			if (!greyed)
			{
				StatMaster.Mode.LevelEditor.objectPivot = !StatMaster.Mode.LevelEditor.objectPivot;
				ObjectPivot.BG.SetActive(StatMaster.Mode.LevelEditor.objectPivot);
				LevelEditor.Instance.TogglePivot();
			}
		}

		public void TogglePaintPlacement()
		{
			if (!greyed)
			{
				StatMaster.Mode.LevelEditor.paintPlacement = !StatMaster.Mode.LevelEditor.paintPlacement;
				Paint.BG.SetActive(StatMaster.Mode.LevelEditor.paintPlacement);
				LevelEditor.Instance.ResetGhostTransform();
			}
		}

		public void OnDuplicateDown()
		{
			if (!greyed)
			{
				Duplicate.BG.SetActive(true);
			}
		}

		public void OnDuplicateUp()
		{
			Duplicate.BG.SetActive(false);
			if (!greyed)
			{
				LevelEditor.Instance.DuplicateSelection();
			}
		}

		public void ToggleDivisionsField()
		{
			UpdateDivisionsField(!DivisionFields.editingGrid);
		}

		public void UpdateDivisionsField(bool b)
		{
			DivisionFields.editingGrid = b;
			UpdateDivisionsField();
			Vector3 position = SingleInstanceFindOnly<LevelEditorUI>.Instance.ClampInMoveArea(SingleInstanceFindOnly<LevelEditorUI>.Instance.container.transform.position + Vector3.up * ((!b) ? (-0.15f) : 0.15f));
			SingleInstanceFindOnly<LevelEditorUI>.Instance.container.transform.position = position;
			SingleInstanceFindOnly<LevelEditorUI>.Instance.collapsed.transform.position = position;
		}

		public void UpdateDivisionsField()
		{
			SingleInstanceFindOnly<LevelEditorUI>.Instance.divisionsFields.parent.SetActive(DivisionFields.editingGrid);
			Transform parent = SingleInstanceFindOnly<LevelEditorUI>.Instance.BG.parent;
			if (!SingleInstanceFindOnly<LevelEditorUI>.Instance._setup)
			{
				SingleInstanceFindOnly<LevelEditorUI>.Instance._Setup();
			}
			Transform pageTurner = SingleInstanceFindOnly<LevelEditorUI>.Instance.pageTurner;
			parent.localScale = new Vector3(parent.localScale.x, (!DivisionFields.editingGrid) ? (BGscaleY - DivFieldHeight) : BGscaleY, parent.localScale.z);
			pageTurner.localPosition = new Vector3(pageTurner.localPosition.x, (!DivisionFields.editingGrid) ? (SingleInstanceFindOnly<LevelEditorUI>.Instance.pageTurnerY + DivFieldHeight) : SingleInstanceFindOnly<LevelEditorUI>.Instance.pageTurnerY, pageTurner.localPosition.z);
			SingleInstanceFindOnly<LevelEditorUI>.Instance.categories.Buildings.transform.parent.localPosition = new Vector3(SingleInstanceFindOnly<LevelEditorUI>.Instance.categories.Buildings.transform.parent.localPosition.x, (!DivisionFields.editingGrid) ? (CatPosY + DivFieldHeight) : CatPosY, SingleInstanceFindOnly<LevelEditorUI>.Instance.categories.Buildings.transform.parent.localPosition.z);
			SingleInstanceFindOnly<LevelEditorUI>.Instance.prefabButtons[0].transform.parent.localPosition = new Vector3(SingleInstanceFindOnly<LevelEditorUI>.Instance.prefabButtons[0].transform.parent.localPosition.x, (!DivisionFields.editingGrid) ? (BtnsPosY + DivFieldHeight) : BtnsPosY, SingleInstanceFindOnly<LevelEditorUI>.Instance.prefabButtons[0].transform.parent.localPosition.z);
		}
	}

	[Serializable]
	public class TransformTools
	{
		public UIButtonExtended Translate;

		public UIButtonExtended Rotate;

		public UIButton ResetRot;

		public UIButtonExtended Scale;

		public UIButtonExtended Mirror;

		public UIButtonExtended Erase;

		public UIButtonExtended Modify;

		private bool greyed;

		public void Setup()
		{
			UpdateSelectedTool();
			Translate.ResetDelegates();
			Rotate.ResetDelegates();
			ResetRot.ResetDelegates();
			Scale.ResetDelegates();
			Mirror.ResetDelegates();
			Erase.ResetDelegates();
			Modify.ResetDelegates();
			Translate.Down += ToggleTranslateMode;
			Rotate.Down += ToggleRotateMode;
			ResetRot.Down += ResetRotation;
			Scale.Down += ToggleScaleMode;
			Mirror.Down += ToggleMirrorMode;
			Erase.Down += ToggleEraseMode;
			Modify.Down += ToggleModifyMode;
		}

		public void GreyOut(bool b)
		{
			greyed = b;
			if (greyed)
			{
				Translate.DisableScripts();
				Rotate.DisableScripts();
				Scale.DisableScripts();
				Mirror.DisableScripts();
				Erase.DisableScripts();
				Modify.DisableScripts();
				Translate.SetIconAlpha(0.1f);
				Rotate.SetIconAlpha(0.1f);
				Scale.SetIconAlpha(0.1f);
				Mirror.SetIconAlpha(0.1f);
				Erase.SetIconAlpha(0.1f);
				Modify.SetIconAlpha(0.1f);
			}
			else
			{
				Translate.EnableScripts();
				Rotate.EnableScripts();
				Scale.EnableScripts();
				Mirror.EnableScripts();
				Erase.EnableScripts();
				Modify.EnableScripts();
				Translate.SetIconAlpha(0.5f);
				Rotate.SetIconAlpha(0.5f);
				Scale.SetIconAlpha(0.5f);
				Mirror.SetIconAlpha(0.5f);
				Erase.SetIconAlpha(0.5f);
				Modify.SetIconAlpha(0.5f);
			}
		}

		public void ToggleTranslateMode()
		{
			ToggleTool(StatMaster.Tool.Translate);
		}

		public void ToggleRotateMode()
		{
			ToggleTool(StatMaster.Tool.Rotate);
		}

		public void ToggleScaleMode()
		{
			ToggleTool(StatMaster.Tool.Scale);
		}

		public void ToggleMirrorMode()
		{
			ToggleTool(StatMaster.Tool.Mirror);
		}

		public void ToggleEraseMode()
		{
			ToggleTool(StatMaster.Tool.Erase);
		}

		public void ToggleModifyMode()
		{
			ToggleTool(StatMaster.Tool.Modify);
		}

		public void ResetRotation()
		{
			LevelEditor.Instance.ResetRotation();
		}

		public void ToggleTool(StatMaster.Tool option)
		{
			if (!StatMaster.levelSimulating)
			{
				StatMaster.Tool tool = ((StatMaster.Mode.LevelEditor.selectedTool != option) ? option : StatMaster.Tool.None);
				LevelEditor.Instance.SetActiveTool(tool);
				SingleInstanceFindOnly<LevelEditorUI>.Instance.options.Linked.gameObject.SetActive(!StatMaster.Mode.LevelEditor.global || tool == StatMaster.Tool.Rotate);
			}
		}

		public void UpdateSelectedTool()
		{
			Translate.BG.SetActive(false);
			Rotate.BG.SetActive(false);
			Scale.BG.SetActive(false);
			Mirror.BG.SetActive(false);
			Erase.BG.SetActive(false);
			Modify.BG.SetActive(false);
			switch (StatMaster.Mode.LevelEditor.selectedTool)
			{
			case StatMaster.Tool.Translate:
				Translate.BG.SetActive(true);
				break;
			case StatMaster.Tool.Rotate:
				Rotate.BG.SetActive(true);
				break;
			case StatMaster.Tool.Scale:
				Scale.BG.SetActive(true);
				break;
			case StatMaster.Tool.Mirror:
				Mirror.BG.SetActive(true);
				break;
			case StatMaster.Tool.Erase:
				Erase.BG.SetActive(true);
				break;
			case StatMaster.Tool.Modify:
				Modify.BG.SetActive(true);
				break;
			}
		}
	}

	[Serializable]
	public class DivisionFields
	{
		public ValueHolder Position;

		public ValueHolder Rotation;

		public ValueHolder Scale;

		public GameObject parent;

		public static bool editingGrid;

		private bool greyed;

		public void Setup()
		{
			Position.ResetDelegate();
			Rotation.ResetDelegate();
			Scale.ResetDelegate();
			Position.SetText(EntityTranslateTool.SNAP_VALUE);
			Rotation.SetText(EntityRotateTool.SNAP_VALUE);
			Scale.SetText(EntityScaleTool.SNAP_VALUE);
			Position.ValueChanged += OnPositionSnapChange;
			Rotation.ValueChanged += OnRotationSnapChange;
			Scale.ValueChanged += OnScaleSnapChange;
		}

		public void GreyOut(bool b)
		{
			greyed = b;
			if (greyed)
			{
				Position.Lock(true);
				Rotation.Lock(true);
				Scale.Lock(true);
			}
			else
			{
				Position.Lock(false);
				Rotation.Lock(false);
				Scale.Lock(false);
			}
		}

		public void OnPositionSnapChange(float value)
		{
			if (value > OptionsMaster.minComponentUnit)
			{
				EntityTranslateTool.SNAP_VALUE = value;
				return;
			}
			EntityTranslateTool.SNAP_VALUE = OptionsMaster.minComponentUnit;
			Position.SetText(OptionsMaster.minComponentUnit);
		}

		public void OnRotationSnapChange(float value)
		{
			if (value > OptionsMaster.minComponentUnit)
			{
				EntityRotateTool.SNAP_VALUE = value;
				return;
			}
			EntityRotateTool.SNAP_VALUE = OptionsMaster.minComponentUnit;
			Rotation.SetText(OptionsMaster.minComponentUnit);
		}

		public void OnScaleSnapChange(float value)
		{
			if (value > OptionsMaster.minComponentUnit)
			{
				EntityScaleTool.SNAP_VALUE = value;
				return;
			}
			EntityScaleTool.SNAP_VALUE = OptionsMaster.minComponentUnit;
			Scale.SetText(OptionsMaster.minComponentUnit);
		}
	}

	[Serializable]
	public class Categories
	{
		public StatMaster.Category selected;

		public UIButtonExtended Buildings;

		public UIButtonExtended Props;

		public UIButtonExtended Brick;

		public UIButtonExtended Animals;

		public UIButtonExtended Humans;

		public UIButtonExtended Weaponry;

		public UIButtonExtended Primitives;

		public UIButtonExtended EnvironmentFoliage;

		public UIButtonExtended Virtual;

		private bool greyed;

		public void Setup()
		{
			UpdateSelectedCategory();
			Buildings.ResetDelegates();
			Brick.ResetDelegates();
			Animals.ResetDelegates();
			Humans.ResetDelegates();
			Weaponry.ResetDelegates();
			Primitives.ResetDelegates();
			EnvironmentFoliage.ResetDelegates();
			Props.ResetDelegates();
			Virtual.ResetDelegates();
			Buildings.Down += OpenBuildingTab;
			Props.Down += OpenPropsTab;
			Brick.Down += OpenBrickTab;
			Animals.Down += OpenAnimalsTab;
			Humans.Down += OpenHumansTab;
			Weaponry.Down += OpenWeaponryTab;
			Primitives.Down += OpenPrimitivesTab;
			EnvironmentFoliage.Down += OpenEnvironmentFoliageTab;
			Virtual.Down += OpenVirtualTab;
		}

		public void GreyOut(bool b)
		{
			greyed = b;
			if (greyed)
			{
				float iconAlpha = 0.1f;
				Buildings.DisableScripts();
				Buildings.SetIconAlpha(iconAlpha);
				Props.DisableScripts();
				Props.SetIconAlpha(iconAlpha);
				Brick.DisableScripts();
				Brick.SetIconAlpha(iconAlpha);
				Animals.DisableScripts();
				Animals.SetIconAlpha(iconAlpha);
				Humans.DisableScripts();
				Humans.SetIconAlpha(iconAlpha);
				Weaponry.DisableScripts();
				Weaponry.SetIconAlpha(iconAlpha);
				Primitives.DisableScripts();
				Primitives.SetIconAlpha(iconAlpha);
				EnvironmentFoliage.DisableScripts();
				EnvironmentFoliage.SetIconAlpha(iconAlpha);
				Virtual.DisableScripts();
				Virtual.SetIconAlpha(iconAlpha);
			}
			else
			{
				float iconAlpha2 = 0.5f;
				Buildings.EnableScripts();
				Buildings.SetIconAlpha(iconAlpha2);
				Props.EnableScripts();
				Props.SetIconAlpha(iconAlpha2);
				Brick.EnableScripts();
				Brick.SetIconAlpha(iconAlpha2);
				Animals.EnableScripts();
				Animals.SetIconAlpha(iconAlpha2);
				Humans.EnableScripts();
				Humans.SetIconAlpha(iconAlpha2);
				Weaponry.EnableScripts();
				Weaponry.SetIconAlpha(iconAlpha2);
				Primitives.EnableScripts();
				Primitives.SetIconAlpha(iconAlpha2);
				EnvironmentFoliage.EnableScripts();
				EnvironmentFoliage.SetIconAlpha(iconAlpha2);
				Virtual.EnableScripts();
				Virtual.SetIconAlpha(iconAlpha2);
			}
		}

		public void OpenBuildingTab()
		{
			OpenCategory(StatMaster.Category.Buildings);
		}

		public void OpenPropsTab()
		{
			OpenCategory(StatMaster.Category.Props);
		}

		public void OpenBrickTab()
		{
			OpenCategory(StatMaster.Category.Brick);
		}

		public void OpenAnimalsTab()
		{
			OpenCategory(StatMaster.Category.Animals);
		}

		public void OpenHumansTab()
		{
			OpenCategory(StatMaster.Category.Humans);
		}

		public void OpenWeaponryTab()
		{
			OpenCategory(StatMaster.Category.Weaponry);
		}

		public void OpenPrimitivesTab()
		{
			OpenCategory(StatMaster.Category.Primitives);
		}

		public void OpenEnvironmentFoliageTab()
		{
			OpenCategory(StatMaster.Category.EnvironmentFoliage);
		}

		public void OpenVirtualTab()
		{
			OpenCategory(StatMaster.Category.Virtual);
		}

		public void OpenCategory(StatMaster.Category option)
		{
			if (!greyed && selected != option)
			{
				selected = option;
				SingleInstanceFindOnly<LevelEditorUI>.Instance.OpenPage(0, option);
				UpdateSelectedCategory();
			}
		}

		public void UpdateSelectedCategory()
		{
			Buildings.BG.SetActive(false);
			Props.BG.SetActive(false);
			Brick.BG.SetActive(false);
			Animals.BG.SetActive(false);
			Humans.BG.SetActive(false);
			Weaponry.BG.SetActive(false);
			Primitives.BG.SetActive(false);
			EnvironmentFoliage.BG.SetActive(false);
			Virtual.BG.SetActive(false);
			switch (selected)
			{
			case StatMaster.Category.Buildings:
				Buildings.BG.SetActive(true);
				break;
			case StatMaster.Category.Props:
				Props.BG.SetActive(true);
				break;
			case StatMaster.Category.Brick:
				Brick.BG.SetActive(true);
				break;
			case StatMaster.Category.Animals:
				Animals.BG.SetActive(true);
				break;
			case StatMaster.Category.Humans:
				Humans.BG.SetActive(true);
				break;
			case StatMaster.Category.Weaponry:
				Weaponry.BG.SetActive(true);
				break;
			case StatMaster.Category.Primitives:
				Primitives.BG.SetActive(true);
				break;
			case StatMaster.Category.EnvironmentFoliage:
				EnvironmentFoliage.BG.SetActive(true);
				break;
			case StatMaster.Category.Virtual:
				Virtual.BG.SetActive(true);
				break;
			}
		}
	}

	public class UIRect
	{
		public Transform upperLeft;

		public Transform lowerRight;
	}

	public enum UIState
	{
		Inactive = 0,
		Simulating = 1,
		BuildMode = 2
	}

	public Camera hudCam;

	public BlurCamTest blurArea;

	public Transform BG;

	public DynamicText titleText;

	public StateTools stateTools = new StateTools();

	public Options options = new Options();

	public TransformTools transformTools = new TransformTools();

	public DivisionFields divisionsFields = new DivisionFields();

	public Categories categories = new Categories();

	public List<LevelPrefabButton> prefabButtons = new List<LevelPrefabButton>();

	public UIRect moveArea = new UIRect();

	public GameObject container;

	public GameObject collapsed;

	public GameObject returnGO;

	public GameObject playGO;

	public LevelPlaylistEditor playlistEditor;

	public ThumbnailCreator levelThumbnailCreator;

	public UIButton playButton;

	public UIButton returnButton;

	public UIButton Collapse;

	public UIButton Expand;

	public UIButton[] dragBar;

	public DynamicText pageText;

	public UIButton nextPage;

	public UIButton prevPage;

	[HideInInspector]
	public Transform pageTurner;

	[HideInInspector]
	public float pageTurnerY;

	public GameObject settingsWindow;

	protected GameObject activeObj;

	protected Transform blurTarget;

	private bool active;

	private bool _setup;

	private StatMaster.Category currentCategory;

	private int currentPage;

	protected Vector3 titlePos = Vector3.zero;

	protected Vector3 dragMouseOffset = Vector3.zero;

	private float expandHeld;

	protected bool greyed;

	public override string Name
	{
		get
		{
			return "LevelEditorUI";
		}
	}

	public bool MachineSimulating
	{
		get
		{
			Machine machine = Machine.Active();
			if (machine == null)
			{
				return false;
			}
			return machine.isSimulating || machine.isRespawning;
		}
	}

	public bool IsOpen
	{
		get
		{
			return container.activeInHierarchy;
		}
	}

	public void NextPage()
	{
		OpenPage(currentPage + 1, currentCategory);
	}

	public void PrevPage()
	{
		OpenPage(currentPage - 1, currentCategory);
	}

	public void OpenPage(int index, StatMaster.Category option)
	{
		if (StatMaster.levelSimulating)
		{
			return;
		}
		int count = prefabButtons.Count;
		int num = (PrefabMaster.LevelPrefabs.ContainsKey((int)option) ? PrefabMaster.LevelPrefabs[(int)option].Count : 0);
		int num2 = 0;
		if (num > 0)
		{
			for (int i = 0; i < PrefabMaster.LevelPrefabs[(int)option].Count; i++)
			{
				if (PrefabMaster.LevelPrefabs[(int)option][i].hidden)
				{
					num2++;
				}
			}
			num -= num2;
		}
		num -= SingleInstanceFindOnly<EntityLoader>.Instance.CountHiddenEntities(option);
		int num3 = (int)Mathf.Ceil((float)num / (1f * (float)count));
		currentCategory = option;
		if (index == -1)
		{
			currentPage = num3 - 1;
		}
		else if (index == num3)
		{
			currentPage = 0;
		}
		else
		{
			currentPage = index;
		}
		int num4 = 0;
		if (num > 0)
		{
			for (int j = 0; j < currentPage * count + num4; j++)
			{
				if (PrefabMaster.LevelPrefabs[(int)option][j].hidden)
				{
					num4++;
				}
			}
		}
		int num5 = 0;
		int num6 = 0;
		for (int k = 0; k < count + num6; k++)
		{
			int index2 = currentPage * count + num4 + k;
			LevelPrefab prefab = PrefabMaster.GetPrefab(option, index2);
			if (prefab != null && prefab.hidden)
			{
				num6++;
				continue;
			}
			prefabButtons[num5].SetUp(prefab);
			num5++;
		}
		GameObject gameObject = pageText.transform.parent.gameObject;
		if (num3 <= 1)
		{
			if (gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
			return;
		}
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
		pageText.SetText(currentPage + 1 + "/" + num3);
		pageText.GetText();
	}

	public void UpdatePage()
	{
		for (int i = 0; i < prefabButtons.Count; i++)
		{
			prefabButtons[i].UpdateBG();
		}
	}

	public void UpdateIcons()
	{
		OpenPage(currentPage, currentCategory);
	}

	private void _Setup()
	{
		if (!_setup)
		{
			pageTurner = nextPage.transform.parent;
			pageTurnerY = pageTurner.localPosition.y;
			_setup = true;
			options.UpdateDivisionsField();
			OpenPage(0, StatMaster.Category.Buildings);
			OnConnect();
		}
	}

	public void Toggle(bool toggle)
	{
		active = toggle;
	}

	private void OnConnect()
	{
		for (int i = 0; i < dragBar.Length; i++)
		{
			dragBar[i].ResetDelegates();
			dragBar[i].Down += SetupDragUI;
			dragBar[i].Held += DragUI;
			dragBar[i].Released += FinishDragUI;
		}
		playGO.SetActive(StatMaster.isHosting || StatMaster.IsLevelEditorOnly);
		playButton.ResetDelegates();
		playButton.Down += OpenPlaylistManager;
		returnButton.ResetDelegates();
		returnButton.Down += ReturnToEditor;
		nextPage.ResetDelegates();
		prevPage.ResetDelegates();
		nextPage.Down += NextPage;
		prevPage.Down += PrevPage;
		stateTools.Setup();
		options.Setup();
		transformTools.Setup();
		divisionsFields.Setup();
		categories.Setup();
		Collapse.ResetDelegates();
		Collapse.Click += InduceCollapse;
		Expand.ResetDelegates();
		Expand.Click += InduceExpansion;
		Expand.Down += SetupDragUI;
		Expand.Held += DragUIWait;
		Expand.Released += FinishDragUI;
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Combine(ReferenceMaster.onResolutionChanged, new Action(ClampPosition));
	}

	private void OpenPlaylistManager()
	{
		string text = "current";
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		string text2 = Path.Combine(StaticSettings.LevelAutosavePath, text + "." + StatMaster.LEVEL_FILE_EXTENSION);
		if ((SingleInstanceFindOnly<AddPiece>.Instance as NetworkAddPiece).AutoSave(text, true, false))
		{
			string thumbnailPath = StaticSettings.GetThumbnailPath(new FileInfo(text2));
			levelThumbnailCreator.CaptureImage(thumbnailPath, true);
			if (!serverSettings.playList.Contains(text2))
			{
				serverSettings.playList.Add(text2);
			}
		}
		playlistEditor.ToggleEditor(false);
		playlistEditor.gameObject.SetActive(true);
	}

	private void ReturnToEditor()
	{
		playlistEditor.ToggleEditor(true);
		playlistEditor.OnApply();
	}

	public void InduceCollapse()
	{
		activeObj = collapsed;
		collapsed.SetActive(true);
		container.SetActive(false);
		blurTarget = collapsed.transform.GetChild(0);
		blurArea.target = blurTarget;
		LevelEditor.Instance.ResetWindow();
	}

	public void InduceExpansion()
	{
		activeObj = container;
		collapsed.SetActive(false);
		container.SetActive(true);
		blurTarget = BG;
		blurArea.target = blurTarget;
	}

	public void OnUpdateSettings(ServerSettings settings)
	{
		active = settings.levelEditor;
		_Setup();
		if (StatMaster.isServer && !BesiegeNetworkManager.Instance.isConnected)
		{
			active = false;
		}
		SetUIState(active ? UIState.BuildMode : UIState.Inactive);
	}

	protected override void Awake()
	{
		base.Awake();
		if (!moveArea.upperLeft)
		{
			moveArea.upperLeft = GameObject.FindWithTag("upperLeft").transform;
		}
		if (!moveArea.lowerRight)
		{
			moveArea.lowerRight = GameObject.FindWithTag("lowerRight").transform;
		}
		InduceExpansion();
		ReferenceMaster.OnConnect += OnConnect;
		titlePos = titleText.transform.localPosition;
		SingleInstanceFindOnly<LevelEditorUI>.Instance.titleText.pixelSnapTransformPos = true;
	}

	private void Start()
	{
		SetUIState(UIState.Inactive);
	}

	private void OnDestroy()
	{
		ReferenceMaster.OnConnect -= OnConnect;
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Remove(ReferenceMaster.onResolutionChanged, new Action(ClampPosition));
	}

	private void SetupDragUI()
	{
		expandHeld = 0f;
		Vector2 vector = Input.mousePosition;
		Vector3 vector2 = hudCam.ScreenToWorldPoint(vector);
		vector = new Vector3(vector2.x, vector2.y, container.transform.position.z);
		dragMouseOffset = new Vector3(vector.x, vector.y, 0f) - container.transform.position;
	}

	private void DragUIWait()
	{
		expandHeld += Time.unscaledDeltaTime;
		if (expandHeld > 0.25f)
		{
			DragUI();
		}
	}

	private void DragUI()
	{
		Vector2 vector = Input.mousePosition;
		Vector3 vector2 = hudCam.ScreenToWorldPoint(vector);
		vector = new Vector3(vector2.x, vector2.y, container.transform.position.z);
		Vector3 pos = new Vector3(vector.x, vector.y, 0f) - dragMouseOffset;
		pos = ClampInMoveArea(pos);
		container.transform.position = pos;
		collapsed.transform.position = pos;
	}

	private void FinishDragUI()
	{
		expandHeld = 0f;
		titleText.pixelSnapTransformPos = false;
		titleText.transform.localPosition = titlePos;
		SingleInstanceFindOnly<LevelEditorUI>.Instance.titleText.pixelSnapTransformPos = true;
	}

	public Vector3 ClampInMoveArea(Vector3 pos)
	{
		return new Vector3(Mathf.Clamp(pos.x, moveArea.upperLeft.position.x, moveArea.lowerRight.position.x - BG.lossyScale.x), Mathf.Clamp(pos.y, moveArea.lowerRight.position.y + BG.lossyScale.y * 0.0864f, moveArea.upperLeft.position.y), pos.z);
	}

	public void ClampPosition()
	{
		Vector3 position = ClampInMoveArea(container.transform.position);
		container.transform.position = position;
		collapsed.transform.position = position;
	}

	private void GreyOut(bool b)
	{
		if (b != greyed)
		{
			greyed = b;
			stateTools.GreyOut(b);
			options.GreyOut(b);
			transformTools.GreyOut(b);
			divisionsFields.GreyOut(b);
			categories.GreyOut(b);
		}
	}

	public void SetUIState(UIState mode)
	{
		returnGO.SetActive(false);
		switch (mode)
		{
		case UIState.Inactive:
			if (BesiegeNetworkManager.Instance.isConnected && (StatMaster.isHosting || StatMaster.IsLevelEditorOnly) && !StatMaster.Mode.levelEdit)
			{
				returnGO.SetActive(true);
			}
			activeObj.SetActive(false);
			stateTools.ResetButtonBGs();
			blurArea.target = null;
			break;
		case UIState.Simulating:
			activeObj.SetActive(true);
			blurArea.target = blurTarget;
			UpdatePage();
			GreyOut(true);
			break;
		case UIState.BuildMode:
			activeObj.SetActive(true);
			blurArea.target = blurTarget;
			UpdatePage();
			GreyOut(false);
			break;
		}
	}
}
