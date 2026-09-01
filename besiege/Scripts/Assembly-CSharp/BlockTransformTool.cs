using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockTransformTool : TransformTool
{
	protected AdvancedBlockEditor blockEditor;

	protected List<BlockBehaviour> machineSelection;

	protected List<UndoAction> undoActions = new List<UndoAction>();

	protected Machine machine;

	protected bool hasMachine;

	protected bool[] originalFlip;

	protected override void OnEnable()
	{
		blockEditor = AdvancedBlockEditor.Instance;
		base.OnEnable();
		machine = Machine.Active();
		hasMachine = machine != null;
	}

	protected override void OnGizmoClicked()
	{
		OnGizmoClicked(blockEditor.selectionController.Selection);
	}

	protected override void OnGizmoReleased()
	{
		base.OnGizmoReleased();
		blockEditor.UpdateTool();
	}

	protected override void OnGizmoClicked(List<ISelectable> selection)
	{
		machineSelection = selection.Cast<BlockBehaviour>().ToList();
		machine.SetRigidInterpolation(RigidbodyInterpolation.None, (machineSelection.Count <= 0) ? null : machineSelection);
		int count = machineSelection.Count;
		originalFlip = new bool[count];
		for (int i = 0; i < count; i++)
		{
			originalFlip[i] = machineSelection[i].Flipped;
		}
		base.OnGizmoClicked(selection);
	}

	protected override bool SnapKeyHeld()
	{
		return InputManager.AdvancedBuilding.LeftCtrlKey();
	}

	protected override bool ReverseKey()
	{
		return InputManager.AdvancedBuilding.LeftAltKey();
	}

	protected override bool MultiSelectKey()
	{
		return InputManager.AdvancedBuilding.LeftShiftKey();
	}

	protected override bool UseSnap()
	{
		bool flag = SnapKeyHeld();
		return (StatMaster.advancedBuilding || blockEditor.selectionController.Count > 0) && !flag;
	}

	protected override void OnGizmoDrag()
	{
		if (StatMaster.isMP && machineSelection.Count > 0)
		{
			StatMaster.cachingTransformActions = true;
		}
		base.OnGizmoDrag();
		machine.nodeController.RefreshVisuals();
		blockEditor.UpdateTool();
	}

	protected override Transform RevertTransform(int index)
	{
		if (index < 0 || index >= selectedObjects.Count)
		{
			return null;
		}
		ISelectable selectable = selectedObjects[index];
		if (selectable == null)
		{
			return null;
		}
		Transform buildingMachine = machine.BuildingMachine;
		Vector3 originalScale;
		Vector3 originalPos;
		Quaternion originalRot;
		GetBlockInfo(selectable as BlockBehaviour, out originalPos, out originalRot, out originalScale);
		originalPos = buildingMachine.TransformPoint(originalPos);
		originalRot = buildingMachine.rotation * originalRot;
		RevertTransform(selectable, originalPos, originalRot);
		return selectable.GetTransform();
	}

	public bool GetBlockInfo(BlockBehaviour block, out Vector3 originalPos, out Quaternion originalRot, out Vector3 originalScale)
	{
		int num = machineSelection.IndexOf(block);
		if (num != -1)
		{
			originalPos = originalPositions[num];
			originalRot = originalRotations[num];
			originalScale = originalScales[num];
		}
		else
		{
			originalPos = block.Position;
			originalRot = block.Rotation;
			originalScale = block.Scale;
		}
		return num != -1;
	}

	public override void AddUndo(ISelectable entity, int i)
	{
		BlockBehaviour blockBehaviour = entity as BlockBehaviour;
		UndoAction undoAction = CreateUndoAction(blockBehaviour, originalPositions[i], originalRotations[i], originalScales[i]);
		if (undoAction != null)
		{
			undoActions.Add(undoAction);
		}
		if (blockBehaviour.Flipped != originalFlip[i])
		{
			undoActions.Add(new UndoActionFlip(machine, blockBehaviour));
		}
	}

	protected virtual UndoAction CreateUndoAction(BlockBehaviour block, Vector3 oldPosition, Quaternion oldRotation, Vector3 oldScale)
	{
		return null;
	}

	public override void ProcessUndo()
	{
		if (machine.onBatchOperationComplete != null)
		{
			machine.onBatchOperationComplete();
		}
		if (StatMaster.cachingTransformActions)
		{
			(machine as ServerMachine).FlushBlockTransformActions();
		}
		machine.RestoreRigidInterpolation();
		if (undoActions.Count > 0)
		{
			machine.UndoSystem.AddActionsWithTool(undoActions);
			machine.RebuildExistingClusters(machineSelection);
			undoActions.Clear();
		}
	}

	protected override Vector3 SnapVector(Vector3 oldPos, Vector3 delta, float snapValue)
	{
		Vector3 result = oldPos + delta;
		if (!StatMaster.Mode.LevelEditor.grid)
		{
			return result;
		}
		return base.SnapVector(oldPos, delta, snapValue);
	}
}
