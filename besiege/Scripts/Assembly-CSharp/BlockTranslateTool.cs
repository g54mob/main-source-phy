using UnityEngine;

public class BlockTranslateTool : BlockTransformTool
{
	public MachineTranslateTool translateTool;

	public bool moveAcrossNormal;

	private Vector3 originalMachinePos;

	private bool moveMachine;

	private Axes currentAxis;

	public static float SNAP_VALUE
	{
		get
		{
			return StatMaster.Mode.Transform.Snap.position;
		}
	}

	public float GetSnap()
	{
		return (!StatMaster.advancedBuilding) ? 0.5f : SNAP_VALUE;
	}

	protected override void OnGizmoDrag()
	{
		base.OnGizmoDrag();
		Vector3 position = GizmoDragTranslate();
		if (StatMaster.Mode.Transform.pivot)
		{
			AdvancedBlockEditor.Instance.selectionController.lastClickedTransformInfo.position = position;
		}
		if (moveMachine)
		{
			Vector3 position2 = originalMachinePos + ((!UseSnap()) ? moveVector : TransformTool.Snap(moveVector, GetSnap()));
			machine.Position = position2;
		}
	}

	protected override void OnGizmoClicked()
	{
		StatMaster.Mode.isTranslating = true;
		StatMaster.Mode.currentBlockTool = this;
		base.OnGizmoClicked();
		UpdateLocalVecAcrossNormal(moveAcrossNormal);
		moveMachine = machineSelection.Count == 0;
		if (moveMachine)
		{
			originalMachinePos = machine.Position;
			if (translateTool != null)
			{
				currentAxis = GetAxis(base.name);
				translateTool.StartTranslateMachine(machine, currentAxis);
			}
		}
	}

	protected override void OnGizmoReleased()
	{
		if (moveMachine)
		{
			Vector3 newPos = originalMachinePos + ((!UseSnap()) ? moveVector : TransformTool.Snap(moveVector, GetSnap()));
			translateTool.StopTranslateMachine(machine, currentAxis, newPos);
			if (originalMachinePos != machine.Position)
			{
				SingleInstanceFindOnly<AddPiece>.Instance.UpdateMiddleOfObject();
				machine.UndoSystem.ChangePosition(originalMachinePos);
			}
		}
		base.OnGizmoReleased();
	}

	protected override void ResetTool()
	{
		StatMaster.Mode.isTranslating = false;
		base.ResetTool();
	}

	protected override void Step()
	{
		StepTranslate(GetSnap());
	}

	protected override UndoAction CreateUndoAction(BlockBehaviour block, Vector3 oldPosition, Quaternion oldRotation, Vector3 oldScale)
	{
		return (!(block.Position != oldPosition)) ? null : new UndoActionMove(machine, block.Guid, block.Position, oldPosition);
	}

	protected override void TransformEntity(ISelectable entity, int index, bool useSnap)
	{
		if (hasMachine)
		{
			bool flag = !StatMaster.advancedBuilding || StatMaster.Mode.Transform.linked;
			bool flag2 = StatMaster.advancedBuilding && StatMaster.Mode.Transform.global;
			Transform transform = entity.GetTransform();
			BlockBehaviour blockBehaviour = entity as BlockBehaviour;
			switch (blockBehaviour.BlockID)
			{
			case 71:
			case 72:
			case 73:
				flag = true;
				break;
			}
			Vector3 vector = moveVector;
			if (StatMaster.advancedBuilding && entity.SymmetryIndex > 0)
			{
				SymmetryController symmetryController = SingleInstanceFindOnly<AddPiece>.Instance.symmetryController;
				int index2 = entity.SymmetryIndex - 1;
				vector = symmetryController.MirrorDirection(index2, vector);
			}
			bool flag3 = useSnap;
			Vector3 vector2 = ((!flag) ? transform.TransformDirection((!flag2) ? localMoveVector : relativeMoveVector) : vector);
			Vector3 vector3 = ((!flag3) ? vector2 : TransformTool.Snap(vector2, flag ? Gizmo() : ((!flag2) ? transform : Gizmo()), GetSnap())) * entity.TransformMultiplier;
			entity.SetPosition(machine.BuildingMachine.TransformPoint(originalPositions[index]) + vector3);
		}
	}

	protected override void Update()
	{
		base.Update();
		if (StatMaster.Mode.isTranslating && moveMachine && !moveAcrossNormal)
		{
			translateTool.UpdateTransformInfo(machine);
		}
	}

	protected override Vector3 GetMouseOffset()
	{
		return (!moveAcrossNormal) ? base.GetMouseOffset() : CastMouseRay(MouseRayType.Translate, base.transform.forward, movePosition);
	}
}
