using UnityEngine;

public class EntityRotateTool : EntityTransformTool
{
	public static float SNAP_VALUE = 22.5f;

	private Vector3 clickPos = Vector3.zero;

	private Quaternion clickRot = Quaternion.identity;

	protected override void OnGizmoClicked()
	{
		clickPos = base.GizmoPosition;
		clickRot = Gizmo().rotation;
		StatMaster.Mode.isRotating = true;
		base.OnGizmoClicked();
		SetupRotate();
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
	}

	protected override void UpdateGizmo()
	{
		Transform transform = Gizmo();
		transform.rotation = clickRot;
		Vector3 axis = transform.TransformDirection(localDir);
		float num = ((!UseSnap()) ? deltaAngle : TransformTool.Snap(deltaAngle, SNAP_VALUE));
		transform.RotateAround(clickPos, axis, num);
	}

	protected override void Step()
	{
		StepRotate(SNAP_VALUE);
	}

	protected override LevelUndoAction CreateUndoAction(LevelEntity entity, Vector3 oldPosition, Quaternion oldRotation, Vector3 oldScale)
	{
		return (ReferenceMaster.CompareQuaternion(entity.Rotation, oldRotation) && !(entity.Position != oldPosition)) ? null : new LUARotateEntity(entity, oldRotation, oldPosition);
	}

	protected override void TransformEntity(ISelectable sel, int index, bool useSnap)
	{
		if (sel != null)
		{
			bool linked = StatMaster.Mode.LevelEditor.linked;
			bool flag = linked || StatMaster.Mode.LevelEditor.global;
			RevertTransform(index);
			Vector3 pivot = (linked ? clickPos : ((!StatMaster.Mode.LevelEditor.objectPivot) ? sel.GetCenter() : (sel as LevelEntity).Position));
			Vector3 axis = ((!flag) ? sel.GetTransform().TransformDirection(localDir) : base.transform.up);
			float num = ((!useSnap) ? deltaAngle : TransformTool.Snap(deltaAngle, SNAP_VALUE));
			RotateEntity(sel, pivot, axis, num);
		}
	}
}
