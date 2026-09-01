using UnityEngine;

public class LUARotateEntity : LevelUndoAction
{
	private Quaternion previousRotation;

	private Vector3 previousPosition;

	public LUARotateEntity(LevelEntity entity, Quaternion prevRotation, Vector3 prevPosition)
		: base(entity)
	{
		previousRotation = prevRotation;
		previousPosition = prevPosition;
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
			Quaternion rotation = transform.rotation;
			entity.SetRotation(previousRotation);
			transform.rotation = previousRotation;
			previousRotation = rotation;
			Vector3 position = transform.position;
			entity.SetPosition(previousPosition);
			transform.position = previousPosition;
			previousPosition = position;
			levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Transform);
		}
	}

	public override void Redo()
	{
		if ((bool)entity && !entity.isCached)
		{
			Transform transform = entity.transform;
			Quaternion rotation = transform.rotation;
			entity.SetRotation(previousRotation);
			transform.rotation = previousRotation;
			previousRotation = rotation;
			Vector3 position = transform.position;
			entity.SetPosition(previousPosition);
			transform.position = previousPosition;
			previousPosition = position;
			levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Transform);
		}
	}
}
