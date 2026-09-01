using System;
using System.Collections.Generic;
using Selectors;
using UnityEngine;

public class AdvancedBlockEditor : MonoBehaviour
{
	[Serializable]
	public class AdvancedBuildingUI
	{
		public ValueHolderScroll value;

		public UIButtonExtended global;

		public UIButtonExtended pivot;

		public UIButtonExtended linked;

		public DynamicText text;

		public MeshRenderer icon;
	}

	public static AdvancedBlockEditor Instance;

	public Transform ToolTransform;

	public BlockSelectionTool selectionController;

	public MeshRenderer duplicateIcon;

	private BlockBehaviour lastSelectedBlock;

	public Transform[] Tools;

	private GameObject ToolGO;

	private NetworkAddPiece addPiece;

	public static bool allowScaleGizmo;

	public static Action ScaleToolSet;

	public AdvancedBuildingUI translate;

	public AdvancedBuildingUI rotate;

	public AdvancedBuildingUI mirror;

	private float lastSelectUpdate;

	private float updateSelectionInterval = 0.5f;

	private bool updatedSelection;

	protected bool greyedValue;

	protected bool greyedDuplicate;

	public bool isActive
	{
		get
		{
			return StatMaster.advancedBuilding;
		}
	}

	public List<BlockBehaviour> Blocks
	{
		get
		{
			return Machine.Active().BuildingBlocks;
		}
	}

	public StatMaster.Tool CurrentState
	{
		get
		{
			return StatMaster.Mode.selectedTool;
		}
	}

	public int SelectionCount
	{
		get
		{
			return selectionController.Count;
		}
	}

	public void Awake()
	{
		Instance = this;
		SingleInstanceFindOnly<AddPiece>.Instance.selectionController.Init(this);
		ToolGO = ToolTransform.gameObject;
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(ToggleSimulation));
		ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineModified, new Action<Machine>(OnMachineModified));
		ReferenceMaster.onMachinePostLoad = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachinePostLoad, new Action<Machine>(OnMachinePostLoad));
		StatMaster.Mode.ToolChanged += ToolChanged;
	}

	private void OnMachineModified(Machine machine)
	{
		UpdateTool();
	}

	private void OnMachinePostLoad(Machine machine)
	{
		selectionController.DeselectAll(false);
		UpdateGizmo();
	}

	public void OnDestroy()
	{
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(ToggleSimulation));
		ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineModified, new Action<Machine>(OnMachineModified));
		ReferenceMaster.onMachinePostLoad = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachinePostLoad, new Action<Machine>(OnMachinePostLoad));
		StatMaster.Mode.ToolChanged -= ToolChanged;
	}

	public void ToolChanged(StatMaster.Tool t)
	{
		SetGizmo(t);
		switch (t)
		{
		case StatMaster.Tool.Translate:
		case StatMaster.Tool.Rotate:
		case StatMaster.Tool.Scale:
		case StatMaster.Tool.Mirror:
		{
			if (StatMaster.Mode.previousTool != StatMaster.Tool.Modify || selectionController.Count <= 0)
			{
				break;
			}
			BlockBehaviour blockBehaviour = selectionController.MachineSelection[selectionController.Count - 1];
			if (blockBehaviour.BlockID != 73)
			{
				break;
			}
			for (int num = selectionController.Count - 1; num >= 0; num--)
			{
				blockBehaviour = selectionController.MachineSelection[num];
				if (!blockBehaviour.IsSelectedExtra)
				{
					break;
				}
			}
			selectionController.DeselectAll(false);
			selectionController.Select(blockBehaviour, false, true, true);
			break;
		}
		}
	}

	public void SetGizmo(StatMaster.Tool t)
	{
		ShowToolGizmo(t);
		UpdateGizmo();
	}

	public static bool IsTransformTool(StatMaster.Tool tool)
	{
		return tool == StatMaster.Tool.Translate || tool == StatMaster.Tool.Rotate || tool == StatMaster.Tool.Mirror;
	}

	public static void ChangedGlobalToggle(bool b)
	{
		Instance.UpdateGizmo();
	}

	public static void ChangedPivotToggle(bool b)
	{
		Instance.UpdateGizmo();
	}

	public static void ChangedLinkToggle(bool b)
	{
		Instance.UpdateGizmo();
	}

	public void ToggleSimulation(bool toggle)
	{
		if (!toggle)
		{
			selectionController.RecolorSelection();
		}
	}

	public void UpdateTool()
	{
		UpdateGizmo();
		if (selectionController.Count == 0)
		{
			UpdatePlayerSelection(null);
			return;
		}
		BlockBehaviour lastBlock = selectionController.LastBlock;
		UpdatePlayerSelection(lastBlock);
	}

	public void UpdateGizmo()
	{
		if (selectionController == null || ToolTransform == null)
		{
			return;
		}
		if (selectionController.Count > 0)
		{
			BlockBehaviour lastBlock = selectionController.LastBlock;
			if (!(lastBlock != null))
			{
				return;
			}
			if (!StatMaster.Mode.isTranslating)
			{
				if (StatMaster.Mode.selectedTool == StatMaster.Tool.Modify)
				{
					ToolTransform.position = lastBlock.GetCenter();
				}
				else
				{
					ToolTransform.position = (StatMaster.Mode.Transform.pivot ? lastBlock.GetTarget() : ((selectionController.Count != 1 && StatMaster.Mode.Transform.global) ? selectionController.GetSelectionCenter() : lastBlock.GetCenter()));
				}
			}
			if (!StatMaster.Mode.isRotating)
			{
				Transform transform = lastBlock.transform;
				ToolTransform.rotation = ((!StatMaster.advancedBuilding || !StatMaster.Mode.Transform.global) ? transform.rotation : Quaternion.identity);
			}
			return;
		}
		Machine machine = Machine.Active();
		if (AddPiece.isEditingLevel || machine == null || !machine.CanModify || machine.isSimulating)
		{
			ShowToolGizmo(StatMaster.Tool.None);
			return;
		}
		if (!StatMaster.Mode.isTranslating)
		{
			ToolTransform.position = SingleInstanceFindOnly<AddPiece>.Instance.middleOfObject.position;
		}
		if (!StatMaster.Mode.isRotating)
		{
			ToolTransform.rotation = machine.Rotation;
		}
	}

	public bool Select(StatMaster.Tool tool, Machine machine, Guid guid, bool isAdditional, int symmetryIndex, float transformMultiplier)
	{
		SetActiveTool(tool, false);
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			selectionController.Select(block, true, false, isAdditional, symmetryIndex, transformMultiplier);
		}
		return true;
	}

	public bool SetBlockAsLast(Machine machine, Guid guid)
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			selectionController.SetBlockAsLast(block);
		}
		UpdateGizmo();
		return true;
	}

	public bool Deselect(StatMaster.Tool tool, Machine machine, Guid guid)
	{
		SetActiveTool(tool, false);
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			selectionController.Deselect(block, false, false);
		}
		return true;
	}

	public void SetActiveTool(StatMaster.Tool tool)
	{
		SetActiveTool(tool, true);
	}

	private void ToggleHoverOutline(bool toggle)
	{
		BlockBehaviour hoveredBlock = SingleInstanceFindOnly<AddPiece>.Instance.HoveredBlock;
		if (!(hoveredBlock != null))
		{
			return;
		}
		BlockVisualController visualController = hoveredBlock.VisualController;
		if (!(visualController != null))
		{
			return;
		}
		if (toggle)
		{
			if (!visualController.Highlighted)
			{
				visualController.SetHighlighted(true);
			}
		}
		else if (visualController.Highlighted)
		{
			visualController.SetNoOutline();
		}
	}

	public void ToggleTool(StatMaster.Tool tool)
	{
		if (tool == StatMaster.Mode.selectedTool)
		{
			ToggleHoverOutline(false);
			TranslateButton.Instance.TranslateOff();
		}
		else
		{
			SetActiveTool(tool, true);
		}
	}

	public void SetActiveTool(StatMaster.Tool tool, bool addToUndo)
	{
		Machine machine = Machine.Active();
		if (!machine || machine.isSimulating || StatMaster.Mode.selectedTool == tool)
		{
			return;
		}
		if (tool != StatMaster.Tool.None)
		{
			ToggleHoverOutline(true);
			ReferenceMaster.ResetLevelEditor();
		}
		switch (tool)
		{
		case StatMaster.Tool.Translate:
			TranslateButton.Instance.OnClicked();
			break;
		case StatMaster.Tool.Rotate:
			MachineRotation.Instance.OnClicked();
			break;
		case StatMaster.Tool.Mirror:
			MirrorButton.Instance.OnClicked();
			break;
		case StatMaster.Tool.Erase:
			EraseButton.Instance.EraserOn();
			break;
		case StatMaster.Tool.Modify:
			KeyMapModeButton.Instance.KeyMapOn();
			break;
		case StatMaster.Tool.Paint:
			PaintButton.Instance.PaintToolOn();
			break;
		case StatMaster.Tool.Scale:
			if (allowScaleGizmo && ScaleToolSet != null)
			{
				ScaleToolSet();
			}
			break;
		}
		ShowToolGizmo(tool);
	}

	public void ShowToolGizmo(StatMaster.Tool tool)
	{
		if (tool != StatMaster.Tool.None)
		{
			if (!ToolGO.activeSelf)
			{
				ToolGO.SetActive(true);
			}
			for (int i = 0; i < Tools.Length; i++)
			{
				int num = (int)tool;
				if (!allowScaleGizmo)
				{
					num = ((num <= 1) ? num : (num - 1));
				}
				num = ((tool != StatMaster.Tool.Modify) ? num : (num - 1));
				bool active = i == num && (tool != StatMaster.Tool.Modify || SelectionCount > 0);
				Tools[i].gameObject.SetActive(active);
			}
			UpdateGizmo();
		}
		else if (ToolGO.activeSelf)
		{
			ToolGO.SetActive(false);
		}
	}

	public void CheckTool()
	{
	}

	public void LateUpdate()
	{
		if (updatedSelection)
		{
			lastSelectUpdate += Time.deltaTime;
			if (lastSelectUpdate > updateSelectionInterval)
			{
				SendPlayerSelection();
				updatedSelection = false;
			}
		}
		if (Machine.Active() != null && Machine.Active().isSimulating)
		{
			if (ToolGO.activeSelf)
			{
				ShowToolGizmo(StatMaster.Tool.None);
			}
			return;
		}
		if (!StatMaster.inMenu)
		{
			if (InputManager.AdvancedBuilding.ToggleKey())
			{
				OptionsMaster.BesiegeConfig.AdvancedBuilding = !OptionsMaster.BesiegeConfig.AdvancedBuilding;
				ToggleHoverOutline(OptionsMaster.BesiegeConfig.AdvancedBuilding);
				if (ReferenceMaster.onAdvancedBuildingToggled != null)
				{
					ReferenceMaster.onAdvancedBuildingToggled();
				}
			}
			if (StatMaster.advancedBuilding)
			{
				if (InputManager.AdvancedBuilding.MoveToolKey())
				{
					ToggleTool(StatMaster.Tool.Translate);
				}
				else if (InputManager.AdvancedBuilding.RotateToolKey())
				{
					ToggleTool(StatMaster.Tool.Rotate);
				}
				else if (InputManager.AdvancedBuilding.MirrorToolKey())
				{
					ToggleTool(StatMaster.Tool.Mirror);
				}
				else if (InputManager.AdvancedBuilding.ModifyToolKey())
				{
					ToggleTool(StatMaster.Tool.Modify);
				}
				else if (InputManager.AdvancedBuilding.PaintToolKey())
				{
					if (OptionsMaster.skinsEnabled)
					{
						ToggleTool(StatMaster.Tool.Paint);
					}
					else
					{
						ToggleTool(StatMaster.Tool.Erase);
					}
				}
			}
		}
		GreyOutValuefields();
		HideGizmo();
		UpdateDuplicateIcon();
	}

	public void GreyOutValuefields()
	{
		if (greyedValue)
		{
			if (!InputManager.AdvancedBuilding.LeftCtrlKey())
			{
				SetValueAlpha(translate, 1f);
				SetValueAlpha(rotate, 1f);
				greyedValue = false;
			}
		}
		else if (InputManager.AdvancedBuilding.LeftCtrlKey())
		{
			SetValueAlpha(translate, 0.4f);
			SetValueAlpha(rotate, 0.4f);
			greyedValue = true;
		}
	}

	public void SetValueAlpha(AdvancedBuildingUI ui, float alpha)
	{
		Color color = ui.icon.material.GetColor("_TintColor");
		ui.icon.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, alpha * 0.5f));
		color = ui.text.color;
		ui.text.color = new Color(color.r, color.g, color.b, alpha);
	}

	public void HideGizmo()
	{
		Machine machine = Machine.Active();
		bool flag = machine != null;
		if (ToolGO.activeSelf)
		{
			if (InputManager.AdvancedBuilding.LeftShiftKey() || StatMaster.Mode.selectSymmetryPivot || selectionController.IsDragging || !flag || machine.isSimulating || AeroDynamicDisplay.IsSelected)
			{
				ShowToolGizmo(StatMaster.Tool.None);
			}
		}
		else if (!InputManager.AdvancedBuilding.LeftShiftKey() && !StatMaster.Mode.selectSymmetryPivot && flag && !machine.isSimulating && !selectionController.IsDragging && !AeroDynamicDisplay.IsSelected)
		{
			ShowToolGizmo(StatMaster.Mode.selectedTool);
		}
	}

	public void UpdateDuplicateIcon()
	{
		int num = 0;
		if (!duplicateIcon)
		{
			return;
		}
		if (greyedDuplicate)
		{
			if (selectionController.Count > num)
			{
				Color color = duplicateIcon.material.GetColor("_TintColor");
				duplicateIcon.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, 1f));
				greyedDuplicate = false;
			}
		}
		else if (selectionController.Count <= num)
		{
			Color color2 = duplicateIcon.material.GetColor("_TintColor");
			duplicateIcon.material.SetColor("_TintColor", new Color(color2.r, color2.g, color2.b, 0.18f));
			greyedDuplicate = true;
		}
	}

	public void UpdatePlayerSelection(BlockBehaviour block)
	{
		if (!(block == lastSelectedBlock))
		{
			lastSelectedBlock = block;
			if (!updatedSelection)
			{
				SendPlayerSelection();
				updatedSelection = true;
				lastSelectUpdate = 0f;
			}
		}
	}

	private void SendPlayerSelection()
	{
	}

	public void CheckShowBlockMapper()
	{
		if (StatMaster.Mode.selectedTool == StatMaster.Tool.Modify)
		{
			if (SelectionCount > 0)
			{
				BlockMapper.Open(selectionController.LastBlock);
			}
			else
			{
				BlockMapper.Close();
			}
		}
	}
}
