using UnityEngine;

public class LUARemoveEntity : LevelUndoAction
{
	private Transform parent;

	public LUARemoveEntity(LevelEntity entity)
		: base(entity)
	{
	}

	public override void Undo()
	{
		if ((bool)entity && entity.isCached)
		{
			placedEntities.Add(new EntityController.PlaceEntry(entity.behaviour.prefab.ID, entity.Position, entity.Rotation, entity.Scale, entity.GetEntityData(), entity.identifier));
		}
		UpdateObjects();
	}

	public override void Redo()
	{
		if ((bool)entity && !entity.isCached)
		{
			removedEntities.Add(objectID);
		}
		UpdateObjects();
	}

	public override void FreeMemory()
	{
		if ((bool)entity && !entity.transform.parent)
		{
			levelEditor.DestroyEntity(entity);
		}
	}
}
