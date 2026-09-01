using System.Collections.Generic;
using UnityEngine;

public class BlockGroundTool : BlockTransformTool
{
	public LayerMask layerMask;

	public MachineGround groundTool;

	public static BlockGroundTool instance;

	protected override bool UseDragTool
	{
		get
		{
			return false;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		instance = this;
		if (groundTool == null)
		{
			Debug.LogWarning("MachineGround is not correctly assigned!");
		}
	}

	protected override void OnGizmoClicked()
	{
		List<ISelectable> selection = blockEditor.selectionController.Selection;
		if (selection.Count == 0)
		{
			Vector3 position = machine.Position;
			MachineGround.MoveDown(machine);
			if (machine.Position != position)
			{
				if (groundTool != null)
				{
					groundTool.SendTransformInfo(machine);
				}
				machine.SetPosition(machine.Position);
				machine.UndoSystem.ChangePosition(position);
				SingleInstanceFindOnly<AddPiece>.Instance.UpdateMiddleOfObject();
				AdvancedBlockEditor.Instance.UpdateTool();
			}
		}
		else if (StatMaster.isMP)
		{
			StatMaster.cachingTransformActions = true;
		}
		OnGizmoClicked(selection);
	}

	protected override void OnGizmoReleased()
	{
		if (!hasMachine)
		{
			return;
		}
		bool flag = true;
		float num = float.MaxValue;
		bool flag2 = false;
		for (int i = 0; i < machineSelection.Count; i++)
		{
			BlockBehaviour blockBehaviour = machineSelection[i];
			if (blockBehaviour == null)
			{
				continue;
			}
			LevelBoundingBox.GroundResult groundResult = blockBehaviour.Ground(layerMask);
			if (!groundResult.hasHit)
			{
				continue;
			}
			if (flag)
			{
				BlockBehaviour componentInParent = groundResult.hitCollider.GetComponentInParent<BlockBehaviour>();
				if ((!(componentInParent != null) || !machineSelection.Contains(componentInParent)) && (!flag2 || groundResult.hitDistance < num))
				{
					num = groundResult.hitDistance;
					flag2 = true;
				}
			}
			else if (groundResult.hitDistance > 0f)
			{
				UndoActionMove undoActionMove = GroundEntity(machine, blockBehaviour, i, groundResult.hitDistance);
				if (undoActionMove != null)
				{
					undoActions.Add(undoActionMove);
				}
			}
		}
		if (flag && flag2 && num > 0f)
		{
			for (int i = 0; i < machineSelection.Count; i++)
			{
				BlockBehaviour blockBehaviour = machineSelection[i];
				if (blockBehaviour != null)
				{
					UndoActionMove undoActionMove = GroundEntity(machine, machineSelection[i], i, num);
					if (undoActionMove != null)
					{
						undoActions.Add(undoActionMove);
					}
				}
			}
		}
		ProcessUndo();
		blockEditor.UpdateTool();
		ResetTool();
	}

	private UndoActionMove GroundEntity(Machine m, BlockBehaviour block, int index, float dist)
	{
		if (!hasMachine)
		{
			return null;
		}
		Vector3 position = block.transform.position + Vector3.down * dist;
		block.SetPosition(position);
		return new UndoActionMove(m, block.Guid, block.Position, originalPositions[index]);
	}
}
