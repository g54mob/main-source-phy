using UnityEngine;

public class BlockRotateTool : BlockTransformTool
{
	public MachineRotation rotationTool;

	private Vector3 relativeDir;

	private Quaternion startMachineRot;

	private Vector3 startMachinePos;

	private bool rotateMachine;

	private Vector3 clickPos = Vector3.zero;

	private Quaternion clickRot = Quaternion.identity;

	public static float SNAP_VALUE
	{
		get
		{
			return StatMaster.Mode.Transform.Snap.rotation;
		}
	}

	public float GetSnap()
	{
		return (!StatMaster.advancedBuilding) ? 45f : SNAP_VALUE;
	}

	protected override void OnGizmoClicked()
	{
		clickPos = base.GizmoPosition;
		clickRot = Gizmo().rotation;
		StatMaster.Mode.isRotating = true;
		StatMaster.Mode.currentBlockTool = this;
		base.OnGizmoClicked();
		SetupRotate();
		rotateMachine = selectedObjects.Count == 0;
		if (rotateMachine)
		{
			startMachineRot = machine.Rotation;
			startMachinePos = machine.Position;
		}
	}

	protected override void OnGizmoReleased()
	{
		if (rotateMachine)
		{
			machine.Position = startMachinePos;
			machine.Rotation = startMachineRot;
			float num = ((!UseSnap()) ? deltaAngle : TransformTool.Snap(deltaAngle, GetSnap()));
			machine.BuildingMachine.RotateAround(base.transform.position, base.transform.up, num);
			if (rotationTool != null)
			{
				rotationTool.SendTransformInfo(machine);
			}
			if (!ReferenceMaster.CompareQuaternion(machine.Rotation, startMachineRot))
			{
				MachineRotation.ApplyMachineRotation(machine, startMachinePos, machine.Position, startMachineRot, machine.Rotation);
			}
		}
		base.OnGizmoReleased();
	}

	protected override void ResetTool()
	{
		StatMaster.Mode.isRotating = false;
		base.ResetTool();
	}

	protected override void OnGizmoDrag()
	{
		base.OnGizmoDrag();
		GizmoDragRotate();
		Transform transform = RevertTransform(selectedObjects.Count - 1);
		relativeDir = ((!(transform != null)) ? localDir : transform.InverseTransformDirection(base.transform.up));
		if (rotateMachine)
		{
			machine.Position = startMachinePos;
			machine.Rotation = startMachineRot;
			float val = deltaAngle;
			if (UseSnap())
			{
				val = TransformTool.Snap(val, GetSnap());
			}
			machine.BuildingMachine.RotateAround(base.transform.position, base.transform.up, val);
			if (rotationTool != null)
			{
				rotationTool.UpdateTransformInfo(machine);
			}
		}
	}

	protected override void UpdateGizmo()
	{
		Transform transform = Gizmo();
		transform.rotation = clickRot;
		Vector3 axis = transform.TransformDirection(localDir);
		float num = ((!UseSnap()) ? deltaAngle : TransformTool.Snap(deltaAngle, GetSnap()));
		transform.RotateAround(clickPos, axis, num);
	}

	protected override void Step()
	{
		StepRotate(GetSnap());
	}

	protected override UndoAction CreateUndoAction(BlockBehaviour block, Vector3 oldPosition, Quaternion oldRotation, Vector3 oldScale)
	{
		Vector3 position = block.Position;
		Quaternion rotation = block.Rotation;
		return (ReferenceMaster.CompareQuaternion(rotation, oldRotation) && !(position != oldPosition)) ? null : new UndoActionRotate(machine, block.Guid, position, oldPosition, rotation, oldRotation);
	}

	protected override void TransformEntity(ISelectable sel, int index, bool useSnap)
	{
		if (!hasMachine)
		{
			return;
		}
		bool flag = !StatMaster.advancedBuilding || StatMaster.Mode.Transform.linked;
		bool flag2 = StatMaster.advancedBuilding && StatMaster.Mode.Transform.global;
		bool flag3 = !StatMaster.advancedBuilding || StatMaster.Mode.Transform.pivot;
		BlockBehaviour blockBehaviour = sel as BlockBehaviour;
		switch (blockBehaviour.BlockID)
		{
		case 71:
		case 72:
		case 73:
			flag = true;
			break;
		}
		Transform transform = RevertTransform(index);
		Vector3 vector = (flag ? clickPos : (flag3 ? transform.position : ((!flag2) ? sel.GetCenter() : clickPos)));
		Vector3 vector2 = (flag ? base.transform.up : ((!flag2) ? transform.TransformDirection(localDir) : transform.TransformDirection(relativeDir)));
		float num = ((!useSnap) ? deltaAngle : TransformTool.Snap(deltaAngle, GetSnap())) * sel.TransformMultiplier;
		if (StatMaster.advancedBuilding)
		{
			int num2 = sel.SymmetryIndex - 1;
			if (num2 > -1)
			{
				SymmetryController symmetryController = SingleInstanceFindOnly<AddPiece>.Instance.symmetryController;
				if (vector != transform.position)
				{
					vector = symmetryController.MirrorVector(num2, vector);
					vector2 = symmetryController.MirrorDirection(num2, vector2);
				}
				switch (num2)
				{
				case 0:
				case 1:
				case 2:
				case 6:
					num = 0f - num;
					break;
				}
			}
		}
		RotateEntity(sel, vector, vector2, num, blockBehaviour.BlockID == 72);
	}
}
