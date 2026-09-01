using System.Collections.Generic;

public class LevelUndoAction
{
	protected long objectID;

	protected LevelEntity entity;

	protected LevelEditor levelEditor;

	protected bool isMultiAction;

	public bool isSelectAction;

	public List<EntityController.PlaceEntry> placedEntities;

	public List<long> removedEntities;

	public LevelUndoAction()
	{
	}

	public LevelUndoAction(LevelEntity levelEntity)
	{
		levelEditor = LevelEditor.Instance;
		entity = levelEntity;
		objectID = entity.identifier;
		placedEntities = new List<EntityController.PlaceEntry>();
		removedEntities = new List<long>();
	}

	public void SetMultiAction(bool toggle)
	{
		isMultiAction = toggle;
	}

	public virtual void Replace(LevelEntity newEntity, long oldID)
	{
		if (oldID == objectID)
		{
			entity = newEntity;
			objectID = entity.identifier;
		}
	}

	protected void UpdateObjects()
	{
		if (!isMultiAction)
		{
			if (placedEntities.Count > 0)
			{
				levelEditor.Add(placedEntities, true, false, true);
				placedEntities.Clear();
			}
			if (removedEntities.Count > 0)
			{
				levelEditor.Remove(removedEntities, true);
				removedEntities.Clear();
			}
		}
	}

	public virtual void Init()
	{
	}

	public virtual void Undo()
	{
	}

	public virtual void Redo()
	{
	}

	public virtual void FreeMemory()
	{
	}
}
