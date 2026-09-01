using UnityEngine;

public class LUAScaleEntity : LevelUndoAction
{
	private Vector3 previousScale;

	private Vector3 previousPosition;

	private Quaternion previousRotation;

	public LUAScaleEntity(LevelEntity entity, Vector3 prevPosition, Quaternion prevRotation, Vector3 prevScale)
		: base(entity)
	{
		previousPosition = prevPosition;
		previousRotation = prevRotation;
		previousScale = prevScale;
	}

	public override void Init()
	{
		levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Transform);
	}

	public override void Undo()
	{
		if ((bool)entity && !entity.isCached)
		{
			Transform transform = entity.transform;
			Vector3 position = transform.position;
			entity.SetPosition(previousPosition);
			transform.position = previousPosition;
			previousPosition = position;
			Quaternion rotation = transform.rotation;
			entity.SetRotation(previousRotation);
			transform.rotation = previousRotation;
			previousRotation = rotation;
			Vector3 localScale = transform.localScale;
			entity.SetScale(previousScale);
			transform.localScale = previousScale;
			previousScale = localScale;
			levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Transform);
		}
	}

	public override void Redo()
	{
		if ((bool)entity && !entity.isCached)
		{
			Transform transform = entity.transform;
			Vector3 position = transform.position;
			entity.SetPosition(previousPosition);
			transform.position = previousPosition;
			previousPosition = position;
			Quaternion rotation = transform.rotation;
			entity.SetRotation(previousRotation);
			transform.rotation = previousRotation;
			previousRotation = rotation;
			Vector3 localScale = transform.localScale;
			entity.SetScale(previousScale);
			transform.localScale = previousScale;
			previousScale = localScale;
			levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Transform);
		}
	}
}
