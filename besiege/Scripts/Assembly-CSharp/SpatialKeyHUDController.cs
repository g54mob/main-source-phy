using System;
using System.Collections.Generic;
using UnityEngine;

public class SpatialKeyHUDController : MonoBehaviour
{
	public static SpatialKeyHUDController Instance;

	public FollowCursor positioner;

	public GameObject showWithGrid;

	public GameObject showWithNodeSelection;

	public GameObject showWithSurfaceSelection;

	private Camera hudCam;

	private void Awake()
	{
		Instance = this;
		FixDynamicText();
		StatMaster.Mode.ToolChanged += OnToolChanged;
		ReferenceMaster.onHotkeyHUDToggled = (Action)Delegate.Combine(ReferenceMaster.onHotkeyHUDToggled, new Action(UpdateVisibility));
		ReferenceMaster.onTooltipsToggled = (Action)Delegate.Combine(ReferenceMaster.onTooltipsToggled, new Action(UpdateVisibility));
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnMachineSim));
		UpdateVisibility();
	}

	private void OnDestroy()
	{
		StatMaster.Mode.ToolChanged -= OnToolChanged;
		ReferenceMaster.onHotkeyHUDToggled = (Action)Delegate.Remove(ReferenceMaster.onHotkeyHUDToggled, new Action(UpdateVisibility));
		ReferenceMaster.onTooltipsToggled = (Action)Delegate.Remove(ReferenceMaster.onTooltipsToggled, new Action(UpdateVisibility));
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnMachineSim));
	}

	private void OnMachineSim(bool b)
	{
		UpdateVisibility();
	}

	private void UpdateVisibility()
	{
		Machine machine = Machine.Active();
		base.gameObject.SetActive((machine == null || !machine.isSimulating) && OptionsMaster.BesiegeConfig.Tooltips && OptionsMaster.BesiegeConfig.HotkeyHUD);
	}

	public void FixDynamicText()
	{
		hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		positioner.cam = hudCam;
		if (hudCam == null)
		{
			return;
		}
		DynamicText[] componentsInChildren = base.transform.GetComponentsInChildren<DynamicText>(true);
		DynamicText[] array = componentsInChildren;
		foreach (DynamicText dynamicText in array)
		{
			if (!(dynamicText == null) && !(dynamicText.cam == hudCam))
			{
				dynamicText.cam = hudCam;
			}
		}
	}

	public static void GridToggled(bool b)
	{
		if ((bool)Instance)
		{
			if (b)
			{
				Instance.positioner.follow = FollowCursor.State.BottomRight;
			}
			Instance.showWithGrid.SetActive(b && OptionsMaster.BesiegeConfig.ShowSurfaceNodeGrid);
		}
	}

	private void OnToolChanged(StatMaster.Tool tool)
	{
		SetTool(tool);
	}

	public static void BlockSelectionChanged(StatMaster.Tool? currentTool = null)
	{
		if ((bool)Instance)
		{
			if (!currentTool.HasValue)
			{
				currentTool = StatMaster.Mode.selectedTool;
			}
			Instance.SetTool(currentTool.Value);
		}
	}

	public void SetTool(StatMaster.Tool currentTool)
	{
		bool flag = false;
		switch (currentTool)
		{
		case StatMaster.Tool.Translate:
		case StatMaster.Tool.Rotate:
		case StatMaster.Tool.Scale:
		case StatMaster.Tool.Mirror:
		case StatMaster.Tool.Paint:
			flag = true;
			break;
		case StatMaster.Tool.Erase:
		case StatMaster.Tool.Modify:
			flag = false;
			break;
		}
		if (!flag)
		{
			SelectedNodes(false);
			SelectedSurfaces(false);
			return;
		}
		List<BlockBehaviour> selectedBlocks = AddPiece.SelectedBlocks;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		for (int i = 0; i < selectedBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = selectedBlocks[i];
			if (!blockBehaviour.SurfaceType)
			{
				SelectedNodes(false);
				SelectedSurfaces(false);
				return;
			}
			bool flag5 = blockBehaviour.Prefab.Type == BlockType.BuildNode || blockBehaviour.Prefab.Type == BlockType.BuildEdge;
			flag2 = flag2 || flag5;
			flag4 |= blockBehaviour.Prefab.Type == BlockType.BuildSurface;
			if (flag3 || !flag5)
			{
				continue;
			}
			flag3 = true;
			List<BuildSurface> surfaces = blockBehaviour.ParentMachine.nodeController.GetSurfaces(blockBehaviour);
			for (int j = 0; j < surfaces.Count; j++)
			{
				if (surfaces[j].IsSelected)
				{
					flag3 = false;
					break;
				}
			}
		}
		if (flag2 && !flag4)
		{
			SelectedNodes(true);
			SelectedSurfaces(false);
		}
		else if (!flag3 && flag4)
		{
			SelectedNodes(false);
			SelectedSurfaces(true);
		}
		else
		{
			SelectedNodes(false);
			SelectedSurfaces(false);
		}
	}

	private void SelectedNodes(bool b)
	{
		if (b)
		{
			positioner.follow = FollowCursor.State.BottomRight;
		}
		showWithNodeSelection.SetActive(b);
	}

	private void SelectedSurfaces(bool b)
	{
		if (b)
		{
			positioner.follow = FollowCursor.State.BottomRight;
		}
		showWithSurfaceSelection.SetActive(b);
	}
}
