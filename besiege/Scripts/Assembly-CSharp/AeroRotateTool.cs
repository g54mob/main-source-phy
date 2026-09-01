using UnityEngine;

public class AeroRotateTool : TransformTool
{
	private Vector3 relativeDir;

	private Quaternion startMachineRot;

	private Vector3 startMachinePos;

	private Vector3 clickPos = Vector3.zero;

	private Quaternion clickRot = Quaternion.identity;

	public static float SNAP_VALUE
	{
		get
		{
			return StatMaster.Mode.Transform.Snap.rotation;
		}
	}

	protected override void OnGizmoClicked()
	{
		clickPos = base.GizmoPosition;
		clickRot = Gizmo().rotation;
		base.OnGizmoClicked();
		SetupRotate();
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

	protected override void TransformEntity(ISelectable sel, int index, bool useSnap)
	{
	}
}
