using UnityEngine;

public class LUAMoveEntity : LevelUndoAction
{
	private Vector3 previousPosition;

	public LUAMoveEntity(LevelEntity entity, Vector3 prevPosition)
		: base(entity)
	{
		previousPosition = prevPosition;
	}

	public override void Init()
	{
		levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Position);
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
			levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Position);
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
			levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Position);
		}
	}
}
