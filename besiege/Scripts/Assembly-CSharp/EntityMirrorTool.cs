using UnityEngine;

public class EntityMirrorTool : EntityTransformTool
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
		go = new GameObject("MirrorHelper");
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

	protected override LevelUndoAction CreateUndoAction(LevelEntity entity, Vector3 oldPosition, Quaternion oldRotation, Vector3 oldScale)
	{
		if (ReferenceMaster.CompareQuaternion(entity.Rotation, oldRotation) && entity.Position == oldPosition)
		{
			return null;
		}
		return new LUAScaleEntity(entity, oldPosition, oldRotation, oldScale);
	}

	protected override void TransformEntity(ISelectable entity, int index, bool useSnap)
	{
		if (entity != null)
		{
			Transform transform = entity.GetTransform();
			LevelEntity levelEntity = entity as LevelEntity;
			Vector3 position = originalPositions[index];
			Quaternion rotation = originalRotations[index];
			Vector3 localScale = originalScales[index];
			transform.position = position;
			transform.rotation = rotation;
			transform.localScale = localScale;
			Mirror(Gizmo(), transform, axis, levelEntity.behaviour.prefab.additiveMirrorAxis, levelEntity.behaviour.prefab.additiveMirrorValues, levelEntity.behaviour.prefab.swapScaleOnMirror, levelEntity.behaviour.prefab.ignoreYSwap);
			levelEntity.SetPosition(transform.position);
			levelEntity.SetRotation(transform.rotation);
			levelEntity.SetScale(transform.localScale);
		}
	}

	public void Mirror(Transform pivot, Transform entity, Axes axis, Vector3 additiveAxis, LevelPrefab.MirrorAxisAdditions addiviveRotation, bool swapScale, bool ignoreYSwap)
	{
		goTransform.parent = pivot;
		goTransform.position = entity.position;
		goTransform.rotation = entity.rotation;
		Transform parent = entity.parent;
		entity.parent = goTransform;
		switch (axis)
		{
		case Axes.x:
			goTransform.localPosition = new Vector3(0f - goTransform.localPosition.x, goTransform.localPosition.y, goTransform.localPosition.z);
			goTransform.localRotation = new Quaternion(goTransform.localRotation.x, 0f - goTransform.localRotation.y, 0f - goTransform.localRotation.z, goTransform.localRotation.w);
			if (additiveAxis.x == 0f)
			{
				break;
			}
			entity.localEulerAngles += addiviveRotation.x;
			if (swapScale)
			{
				if (ignoreYSwap)
				{
					entity.localScale = new Vector3(entity.localScale.z, entity.localScale.y, entity.localScale.x);
				}
				else
				{
					entity.localScale = new Vector3(entity.localScale.x, entity.localScale.z, entity.localScale.y);
				}
			}
			break;
		case Axes.y:
			goTransform.localPosition = new Vector3(goTransform.localPosition.x, 0f - goTransform.localPosition.y, goTransform.localPosition.z);
			goTransform.localRotation = new Quaternion(0f - goTransform.localRotation.x, goTransform.localRotation.y, 0f - goTransform.localRotation.z, goTransform.localRotation.w);
			entity.localEulerAngles += Vector3.forward * 180f;
			if (additiveAxis.y == 0f)
			{
				break;
			}
			entity.localEulerAngles += addiviveRotation.y;
			if (swapScale)
			{
				if (ignoreYSwap)
				{
					entity.localScale = new Vector3(entity.localScale.z, entity.localScale.y, entity.localScale.x);
				}
				else
				{
					entity.localScale = new Vector3(entity.localScale.y, entity.localScale.x, entity.localScale.z);
				}
			}
			break;
		case Axes.z:
			goTransform.localPosition = new Vector3(goTransform.localPosition.x, goTransform.localPosition.y, 0f - goTransform.localPosition.z);
			goTransform.localRotation = new Quaternion(0f - goTransform.localRotation.x, 0f - goTransform.localRotation.y, goTransform.localRotation.z, goTransform.localRotation.w);
			entity.localEulerAngles += Vector3.right * 180f + Vector3.forward * 180f;
			if (additiveAxis.z == 0f)
			{
				break;
			}
			entity.localEulerAngles += addiviveRotation.z;
			if (swapScale)
			{
				if (ignoreYSwap)
				{
					entity.localScale = new Vector3(entity.localScale.z, entity.localScale.y, entity.localScale.x);
				}
				else
				{
					entity.localScale = new Vector3(entity.localScale.x, entity.localScale.z, entity.localScale.y);
				}
			}
			break;
		}
		entity.parent = parent;
	}
}
