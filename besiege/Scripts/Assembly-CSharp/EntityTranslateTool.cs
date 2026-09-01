using UnityEngine;

public class EntityTranslateTool : EntityTransformTool
{
	public static float SNAP_VALUE = 1f;

	public bool moveAcrossNormal;

	protected override void OnGizmoDrag()
	{
		base.OnGizmoDrag();
		GizmoDragTranslate();
	}

	protected override void OnGizmoClicked()
	{
		StatMaster.Mode.isTranslating = true;
		base.OnGizmoClicked();
		UpdateLocalVecAcrossNormal(moveAcrossNormal);
	}

	protected override void ResetTool()
	{
		StatMaster.Mode.isTranslating = false;
		base.ResetTool();
	}

	protected override void Step()
	{
		StepTranslate(SNAP_VALUE);
	}

	protected override LevelUndoAction CreateUndoAction(LevelEntity entity, Vector3 oldPosition, Quaternion oldRotation, Vector3 oldScale)
	{
		return (!(entity.Position != oldPosition)) ? null : new LUAMoveEntity(entity, oldPosition);
	}

	protected override void TransformEntity(ISelectable entity, int index, bool useSnap)
	{
		if (entity != null)
		{
			bool linked = StatMaster.Mode.LevelEditor.linked;
			bool global = StatMaster.Mode.LevelEditor.global;
			Vector3 vector = ((linked || global) ? moveVector : entity.GetTransform().TransformDirection(localMoveVector));
			if (useSnap)
			{
				vector = TransformTool.Snap(vector, (linked || global) ? Gizmo() : entity.GetTransform(), SNAP_VALUE);
			}
			entity.SetPosition(originalPositions[index] + vector);
		}
	}

	protected override Vector3 GetMouseOffset()
	{
		return (!moveAcrossNormal) ? base.GetMouseOffset() : CastMouseRay(MouseRayType.Translate, base.transform.forward, movePosition);
	}
}
