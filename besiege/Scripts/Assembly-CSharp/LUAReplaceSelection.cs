using System.Collections.Generic;

public class LUAReplaceSelection : LevelUndoAction
{
	private List<LevelEntity> oldList;

	private List<LevelEntity> newList;

	private List<long> oldIdList;

	private List<long> newIdList;

	public LUAReplaceSelection(List<LevelEntity> prevList, List<LevelEntity> currentList)
	{
		levelEditor = LevelEditor.Instance;
		oldList = prevList;
		newList = currentList;
		oldIdList = new List<long>();
		newIdList = new List<long>();
		foreach (LevelEntity old in oldList)
		{
			oldIdList.Add(old.identifier);
		}
		foreach (LevelEntity @new in newList)
		{
			newIdList.Add(@new.identifier);
		}
		placedEntities = new List<EntityController.PlaceEntry>();
		removedEntities = new List<long>();
		isSelectAction = true;
	}

	public override void Replace(LevelEntity newEntity, long oldID)
	{
		for (int i = 0; i < oldIdList.Count; i++)
		{
			if (oldIdList[i] == oldID)
			{
				oldIdList[i] = newEntity.identifier;
				oldList[i] = newEntity;
			}
		}
		for (int i = 0; i < newIdList.Count; i++)
		{
			if (newIdList[i] == oldID)
			{
				newIdList[i] = newEntity.identifier;
				newList[i] = newEntity;
			}
		}
	}

	public override void Undo()
	{
		levelEditor.DeselectAll(false);
		List<LevelEntity> list = new List<LevelEntity>();
		for (int i = 0; i < oldList.Count; i++)
		{
			LevelEntity levelEntity = oldList[i];
			if (levelEntity != null && !levelEntity.isCached)
			{
				list.Add(levelEntity);
			}
		}
		levelEditor.Select(list, false, false);
	}

	public override void Redo()
	{
		levelEditor.DeselectAll(false);
		List<LevelEntity> list = new List<LevelEntity>();
		for (int i = 0; i < newList.Count; i++)
		{
			LevelEntity levelEntity = newList[i];
			if (levelEntity != null && !levelEntity.isCached)
			{
				list.Add(levelEntity);
			}
		}
		levelEditor.Select(list, false, false);
	}
}
