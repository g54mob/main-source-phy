using System;
using System.Collections.Generic;
using UnityEngine;

public class UndoSystem : MonoBehaviour
{
	public static bool Enabled = true;

	public Machine Machine;

	private List<UndoAction> undoList = new List<UndoAction>();

	private int snapIndex = -1;

	public static bool processing;

	public List<UndoAction> Clone()
	{
		return new List<UndoAction>(undoList);
	}

	public void Overwrite(Machine m, List<UndoAction> source)
	{
		foreach (UndoAction item in source)
		{
			BlockBehaviour block;
			if (m.GetBlock(item.GetGuid(), out block))
			{
				BlockInfo newInfo = BlockInfo.FromBlockBehaviour(block);
				item.OverwriteInfo(m, newInfo);
			}
		}
		undoList = source;
		snapIndex = source.Count - 1;
	}

	public void Reset()
	{
		undoList.Clear();
		snapIndex = -1;
	}

	public void Undo()
	{
		if (!CanInteract() || !Enabled)
		{
			return;
		}
		processing = true;
		if (StatMaster.isMP)
		{
			StatMaster.cachingTransformActions = true;
		}
		Machine.ToggleUndo(true);
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		while (!flag && snapIndex > -1)
		{
			UndoAction undoAction = undoList[snapIndex--];
			if (undoAction is ReplaceMachineUndoAction)
			{
				flag2 = true;
			}
			if (undoAction.changesCount)
			{
				flag3 = true;
			}
			PreUndoAction(undoAction);
			flag = undoAction.Undo();
			PostUndoAction(undoAction);
		}
		if (Machine.onBatchOperationComplete != null)
		{
			Machine.onBatchOperationComplete();
		}
		if (!flag2)
		{
			Machine.ToggleUndo(false);
		}
		if (StatMaster.cachingTransformActions)
		{
			(Machine as ServerMachine).FlushBlockTransformActions();
			if (flag3)
			{
				(Machine as ServerMachine).DetermineBannedBlocks();
			}
		}
		if (Machine.nodeController.IsBuilding && StatMaster.Mode.selectedTool != StatMaster.Tool.None)
		{
			AdvancedBlockEditor.Instance.ToggleTool(StatMaster.Mode.selectedTool);
		}
		CheckOpenBlockMapper();
		processing = false;
	}

	private void CheckOpenBlockMapper()
	{
		if (Machine == Machine.Active())
		{
			AdvancedBlockEditor.Instance.CheckShowBlockMapper();
		}
	}

	private bool CanInteract()
	{
		Machine machine = Machine.Active();
		if (machine == null)
		{
			return false;
		}
		if (SelectionTool.BatchChange)
		{
			return false;
		}
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		return machine.ReadyForSim;
	}

	private void PreUndoAction(UndoAction undoAction)
	{
		if (undoAction.changesTransform)
		{
			List<BlockBehaviour> blocks = undoAction.GetBlocks();
			if (blocks.Count > 0)
			{
				Machine.SetRigidInterpolation(RigidbodyInterpolation.None, blocks);
			}
			else
			{
				Machine.SetRigidInterpolation(RigidbodyInterpolation.None);
			}
		}
	}

	private void PostUndoAction(UndoAction undoAction)
	{
		if (undoAction.changesTransform)
		{
			Machine.RestoreRigidInterpolation();
			List<BlockBehaviour> blocks = undoAction.GetBlocks();
			Machine.RebuildExistingClusters(blocks);
			SingleInstanceFindOnly<AddPiece>.Instance.UpdateMiddleOfObject();
			if (ReferenceMaster.onMachineModified != null)
			{
				ReferenceMaster.onMachineModified(Machine);
			}
		}
		else if (undoAction.changesCount)
		{
			List<BlockBehaviour> blocks2 = undoAction.GetBlocks();
			Machine.RebuildExistingClusters(blocks2);
		}
		else if (undoAction.changesOBM)
		{
			OverviewBlockMapper currentInstance = OverviewBlockMapper.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.GenerateGroups(true);
			}
		}
		else if (undoAction.changesParameters)
		{
			if (BlockMapper.OnParameterUndo != null)
			{
				BlockMapper.OnParameterUndo(undoAction);
			}
		}
		else if (undoAction is ReplaceMachineUndoAction)
		{
			Machine.RebuildExistingClusters(Machine.BuildingBlocks);
		}
	}

	public void Redo()
	{
		if (!CanInteract() || !Enabled)
		{
			return;
		}
		processing = true;
		if (StatMaster.isMP)
		{
			StatMaster.cachingTransformActions = true;
		}
		Machine.ToggleUndo(true);
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		while (!flag && snapIndex + 1 < undoList.Count)
		{
			snapIndex++;
			UndoAction undoAction = undoList[snapIndex];
			if (undoAction is ReplaceMachineUndoAction)
			{
				flag2 = true;
			}
			if (undoAction.changesCount)
			{
				flag3 = true;
			}
			PreUndoAction(undoAction);
			flag = undoAction.Redo();
			PostUndoAction(undoAction);
		}
		if (Machine.onBatchOperationComplete != null)
		{
			Machine.onBatchOperationComplete();
		}
		if (!flag2)
		{
			Machine.ToggleUndo(false);
		}
		if (StatMaster.cachingTransformActions)
		{
			(Machine as ServerMachine).FlushBlockTransformActions();
			if (flag3)
			{
				(Machine as ServerMachine).DetermineBannedBlocks();
			}
		}
		if (Machine.nodeController.IsBuilding && StatMaster.Mode.selectedTool != StatMaster.Tool.None)
		{
			AdvancedBlockEditor.Instance.ToggleTool(StatMaster.Mode.selectedTool);
		}
		CheckOpenBlockMapper();
		processing = false;
	}

	public void AddBlock(BlockInfo info)
	{
		AddAction(new UndoActionAdd(Machine, info));
	}

	public void RemoveBlock(BlockInfo info)
	{
		AddAction(new UndoActionRemove(Machine, info));
	}

	public void EditBlock(BlockInfo newInfo, BlockInfo prevInfo)
	{
		AddAction(new UndoActionEdit(Machine, newInfo, prevInfo));
	}

	public void ReverseBlock(BlockBehaviour block)
	{
		AddAction(new UndoActionFlip(Machine, block));
	}

	public void EditSkin(BlockBehaviour block, BlockSkinLoader.SkinPack.Skin newSkin, BlockSkinLoader.SkinPack.Skin prevSkin)
	{
		AddAction(new UndoActionSkin(Machine, block.Guid, newSkin, prevSkin));
	}

	public void ChangeTransform(Vector3 oldPosition, Quaternion oldRotation)
	{
		AddAction(new UndoActionTransform(Machine, oldPosition, Machine.Position, oldRotation, Machine.Rotation));
	}

	public void AddActions(List<UndoAction> actions)
	{
		if (actions.Count != 0)
		{
			if (actions.Count == 1)
			{
				AddAction(actions[0]);
			}
			else
			{
				AddAction(new MultiUndoAction(Machine, actions.ToArray()));
			}
		}
	}

	public void AddActionsWithTool(List<UndoAction> actions)
	{
		if (actions.Count == 0)
		{
			return;
		}
		StatMaster.Tool prev = StatMaster.Tool.None;
		if (snapIndex > 0)
		{
			UndoAction undoAction = undoList[snapIndex];
			if (undoAction is MultiUndoAction)
			{
				prev = (undoAction as MultiUndoAction).GetTool();
			}
		}
		AddAction(new MultiUndoAction(Machine, actions.ToArray()).SetTool(prev, StatMaster.Mode.selectedTool));
	}

	public void ChangePosition(Vector3 oldPosition)
	{
		if (!(oldPosition == Machine.Position))
		{
			AddAction(new UndoActionsMachineMove(Machine, oldPosition, Machine.Position));
		}
	}

	public void ChangeRotation(Quaternion oldRotation)
	{
		if (!(oldRotation == Machine.Rotation))
		{
			AddAction(new UndoActionMachineRotate(Machine, oldRotation, Machine.Rotation));
		}
	}

	public void AddAction(UndoAction action)
	{
		if (processing)
		{
			Debug.LogError("[UndoSystem]: trying to create undo while undoing.");
		}
		int num = snapIndex + 1;
		while (undoList.Count > num)
		{
			undoList.RemoveAt(undoList.Count - 1);
		}
		undoList.Add(action);
		snapIndex++;
	}

	public List<BlockBehaviour> GetSelectedBlocks()
	{
		List<Guid> list = new List<Guid>();
		for (int i = 0; i < undoList.Count; i++)
		{
			UndoAction undoAction = undoList[i];
			if (undoAction.isMultiAction)
			{
				MultiUndoAction multiUndoAction = undoAction as MultiUndoAction;
				for (int j = 0; j < multiUndoAction.actions.Length; j++)
				{
					ProcessSelect(multiUndoAction.actions[j], list);
				}
			}
			else
			{
				ProcessSelect(undoAction, list);
			}
		}
		List<BlockBehaviour> list2 = new List<BlockBehaviour>();
		for (int j = 0; j < list.Count; j++)
		{
			BlockBehaviour block;
			if (Machine.GetBlock(list[j], out block))
			{
				list2.Add(block);
			}
		}
		return list2;
	}

	private void ProcessSelect(UndoAction u, List<Guid> guidList)
	{
		if (u is UndoActionSelect)
		{
			Guid guid = (u as UndoActionSelect).GetGuid();
			if (!guidList.Contains(guid))
			{
				guidList.Add(guid);
			}
		}
		else if (u is UndoActionDeselect)
		{
			Guid guid = (u as UndoActionDeselect).GetGuid();
			if (guidList.Contains(guid))
			{
				guidList.Remove(guid);
			}
		}
		else
		{
			if (!(u is UndoActionReplaceSelection))
			{
				return;
			}
			UndoActionReplaceSelection undoActionReplaceSelection = u as UndoActionReplaceSelection;
			for (int i = 0; i < undoActionReplaceSelection.prevIds.Count; i++)
			{
				Guid guid = undoActionReplaceSelection.prevIds[i].guid;
				if (guidList.Contains(guid))
				{
					guidList.Remove(guid);
				}
			}
			for (int i = 0; i < undoActionReplaceSelection.currentIds.Count; i++)
			{
				Guid guid = undoActionReplaceSelection.currentIds[i].guid;
				if (!guidList.Contains(guid))
				{
					guidList.Add(guid);
				}
			}
		}
	}
}
