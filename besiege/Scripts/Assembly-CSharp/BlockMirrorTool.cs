using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockMirrorTool : BlockTransformTool
{
	public static float SNAP_VALUE = 1f;

	public Axes axis;

	protected GameObject go;

	protected Transform goTransform;

	protected override bool UseDragTool
	{
		get
		{
			return false;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		go = new GameObject("MirrorHelper_blocks");
		goTransform = go.transform;
	}

	protected override void UpdateReverseVisual()
	{
	}

	public override void OnDisable()
	{
		base.OnDisable();
		if (go != null)
		{
			Object.Destroy(go);
		}
	}

	protected override void OnGizmoClicked()
	{
		List<ISelectable> selection = blockEditor.selectionController.Selection;
		if (selection.Count > 0)
		{
			OnGizmoClicked(selection);
		}
		else if (hasMachine)
		{
			OnGizmoClicked(machine.BuildingBlocks.Cast<ISelectable>().ToList());
		}
	}

	protected override void OnGizmoReleased()
	{
		if (StatMaster.isMP)
		{
			StatMaster.cachingTransformActions = true;
		}
		if (selectedObjects.Count > 0)
		{
			OnGizmoReleased(selectedObjects);
		}
		else if (hasMachine)
		{
			OnGizmoReleased(machine.BuildingBlocks.Cast<ISelectable>().ToList());
		}
		ResetTool();
	}

	protected override UndoAction CreateUndoAction(BlockBehaviour block, Vector3 oldPosition, Quaternion oldRotation, Vector3 oldScale)
	{
		if (block is GenericDraggedBlock && IsValidDragged(block))
		{
			GenericDraggedBlock genericDraggedBlock = block as GenericDraggedBlock;
			Vector3 position = genericDraggedBlock.startPoint.position;
			Vector3 position2 = genericDraggedBlock.endPoint.position;
			Vector3 eulerAngles = genericDraggedBlock.startPoint.eulerAngles;
			Vector3 eulerAngles2 = genericDraggedBlock.endPoint.eulerAngles;
			if (!ReferenceMaster.CompareQuaternion(block.Rotation, oldRotation) || block.Position != oldPosition || position != genericDraggedBlock.savedPosA || position2 != genericDraggedBlock.savedPosB || eulerAngles != genericDraggedBlock.savedEulerA || eulerAngles2 != genericDraggedBlock.savedEulerB)
			{
				return new UndoActionMirrorDragged(machine, block.Guid, block.Position, oldPosition, block.Rotation, oldRotation, position, genericDraggedBlock.savedPosA, position2, genericDraggedBlock.savedPosB, eulerAngles, genericDraggedBlock.savedEulerA, eulerAngles2, genericDraggedBlock.savedEulerB);
			}
		}
		else if (!ReferenceMaster.CompareQuaternion(block.Rotation, oldRotation) || block.Position != oldPosition)
		{
			return new UndoActionRotate(machine, block.Guid, block.Position, oldPosition, block.Rotation, oldRotation);
		}
		return null;
	}

	protected override void TransformEntity(ISelectable entity, int index, bool useSnap)
	{
		if (hasMachine && entity != null)
		{
			Transform transform = entity.GetTransform();
			BlockBehaviour blockBehaviour = entity as BlockBehaviour;
			Transform buildingMachine = machine.BuildingMachine;
			Vector3 position = buildingMachine.TransformPoint(originalPositions[index]);
			Quaternion rotation = buildingMachine.rotation * originalRotations[index];
			transform.position = position;
			transform.rotation = rotation;
			Mirror(Gizmo(), blockBehaviour, axis);
			blockBehaviour.SetRotation(transform.rotation);
			blockBehaviour.SetPosition(transform.position);
		}
	}

	private bool IsValidDragged(BlockBehaviour block)
	{
		Vector3 localScale = block.transform.localScale;
		float y = (block as GenericDraggedBlock).cylinder.localScale.y;
		return BraceCode.BraceType(localScale, y) != BraceState.Cube;
	}

	public void Mirror(Transform pivot, BlockBehaviour block, Axes axis)
	{
		if (!hasMachine)
		{
			return;
		}
		switch (block.BlockID)
		{
		case 2:
		case 13:
		case 17:
		case 22:
		case 26:
		case 28:
		case 39:
		case 46:
		case 48:
		case 52:
		case 55:
			MirrorAndFlip(pivot, block, axis);
			break;
		case 80:
		{
			MirrorBlock(pivot, block, axis);
			NauticalScrew nauticalScrew = block as NauticalScrew;
			if (nauticalScrew.allowChiralityChange)
			{
				nauticalScrew.Chirality.SetValue(!nauticalScrew.Chirality.IsActive);
				nauticalScrew.SetChirality(nauticalScrew.Chirality.IsActive);
				XDataHolder data = new XDataHolder();
				block.OnSave(data);
			}
			break;
		}
		case 7:
		case 9:
		case 45:
		case 75:
			if (IsValidDragged(block))
			{
				MirrorDraggedBlock(pivot, block, axis);
			}
			else
			{
				MirrorBlock(pivot, block, axis);
			}
			break;
		case 73:
			if ((block as BuildSurface).IsDirty())
			{
				MirrorBlock(pivot, block, axis);
			}
			break;
		default:
			MirrorBlock(pivot, block, axis);
			break;
		}
	}

	public void MirrorBlock(Transform pivot, BlockBehaviour block, Axes axis)
	{
		MirrorObject(pivot, block.transform, axis, true);
	}

	public void MirrorAndFlip(Transform pivot, BlockBehaviour block, Axes axis)
	{
		machine.ReverseBlock(block, false, false);
		MirrorBlock(pivot, block, axis);
	}

	public void MirrorDraggedBlock(Transform pivot, BlockBehaviour block, Axes axis)
	{
		GenericDraggedBlock genericDraggedBlock = block as GenericDraggedBlock;
		Transform transform = block.transform;
		genericDraggedBlock.SaveBraceState();
		Transform startPoint = genericDraggedBlock.startPoint;
		Transform endPoint = genericDraggedBlock.endPoint;
		startPoint.parent = pivot;
		endPoint.parent = pivot;
		genericDraggedBlock.hasOffset = false;
		MirrorObject(pivot, transform, axis, true);
		MirrorObject(pivot, startPoint, axis, true);
		MirrorObject(pivot, endPoint, axis, true);
		startPoint.parent = transform;
		endPoint.parent = transform;
		genericDraggedBlock.SetPositionsGlobal(startPoint.position, startPoint.eulerAngles, endPoint.position, endPoint.eulerAngles, true);
	}

	protected void MirrorObjectOld(Transform pivot, Transform obj, Axes axis, bool mirrorRotation)
	{
		goTransform.parent = pivot;
		goTransform.position = obj.position;
		goTransform.rotation = obj.rotation;
		Transform parent = obj.parent;
		obj.parent = goTransform;
		switch (axis)
		{
		case Axes.x:
			goTransform.localPosition = new Vector3(0f - goTransform.localPosition.x, goTransform.localPosition.y, goTransform.localPosition.z);
			if (mirrorRotation)
			{
				goTransform.localRotation = new Quaternion(goTransform.localRotation.x, 0f - goTransform.localRotation.y, 0f - goTransform.localRotation.z, goTransform.localRotation.w);
			}
			break;
		case Axes.y:
			goTransform.localPosition = new Vector3(goTransform.localPosition.x, 0f - goTransform.localPosition.y, goTransform.localPosition.z);
			if (mirrorRotation)
			{
				goTransform.localRotation = new Quaternion(0f - goTransform.localRotation.x, goTransform.localRotation.y, 0f - goTransform.localRotation.z, goTransform.localRotation.w);
				obj.localEulerAngles += Vector3.forward * 180f;
			}
			break;
		case Axes.z:
			goTransform.localPosition = new Vector3(goTransform.localPosition.x, goTransform.localPosition.y, 0f - goTransform.localPosition.z);
			if (mirrorRotation)
			{
				goTransform.localRotation = new Quaternion(0f - goTransform.localRotation.x, 0f - goTransform.localRotation.y, goTransform.localRotation.z, goTransform.localRotation.w);
				obj.localEulerAngles += Vector3.right * 180f + Vector3.forward * 180f;
			}
			break;
		}
		obj.parent = parent;
	}

	protected void MirrorObject(Transform pivot, Transform obj, Axes axis, bool mirrorRotation)
	{
		Vector3 position = pivot.InverseTransformPoint(obj.position);
		switch (axis)
		{
		case Axes.x:
			position.x = 0f - position.x;
			break;
		case Axes.y:
			position.y = 0f - position.y;
			break;
		case Axes.z:
			position.z = 0f - position.z;
			break;
		}
		Vector3 position2 = pivot.TransformPoint(position);
		Quaternion rotation = obj.rotation;
		if (mirrorRotation)
		{
			Quaternion quaternion = Quaternion.Inverse(pivot.rotation) * obj.rotation;
			Quaternion quaternion2;
			switch (axis)
			{
			case Axes.x:
				quaternion2 = new Quaternion(quaternion.x, 0f - quaternion.y, 0f - quaternion.z, quaternion.w);
				break;
			case Axes.y:
				quaternion2 = new Quaternion(0f - quaternion.x, quaternion.y, 0f - quaternion.z, quaternion.w) * Quaternion.Euler(0f, 0f, 180f);
				break;
			case Axes.z:
				quaternion2 = new Quaternion(0f - quaternion.x, 0f - quaternion.y, quaternion.z, quaternion.w) * Quaternion.Euler(180f, 0f, 180f);
				break;
			default:
				quaternion2 = quaternion;
				break;
			}
			rotation = pivot.rotation * quaternion2;
		}
		obj.position = position2;
		if (mirrorRotation)
		{
			obj.rotation = rotation;
		}
	}
}
