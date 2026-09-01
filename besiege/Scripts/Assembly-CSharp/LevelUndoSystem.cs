using System.Collections.Generic;
using UnityEngine;

public static class LevelUndoSystem
{
	private static List<LevelUndoAction> undoList = new List<LevelUndoAction>();

	private static List<LevelUndoAction> redoList = new List<LevelUndoAction>();

	private static List<LevelEntity> undoObjects = new List<LevelEntity>();

	private static LevelEditor levelEditor;

	public static void Add(List<LevelUndoAction> levelActions)
	{
		Add(levelActions, true);
	}

	public static void Add(List<LevelUndoAction> levelActions, bool clearCache)
	{
		if (levelActions.Count != 0)
		{
			Add((levelActions.Count != 1) ? new LUAMultiAction(levelActions.ToArray()) : levelActions[0], clearCache);
		}
	}

	public static void Add(LevelUndoAction levelEditorAction)
	{
		Add(levelEditorAction, true);
	}

	public static void Add(LevelUndoAction levelEditorAction, bool clearCache)
	{
		if (!levelEditor)
		{
			levelEditor = LevelEditor.Instance;
		}
		if (clearCache)
		{
			ClearCache();
		}
		redoList.Clear();
		undoList.Add(levelEditorAction);
		levelEditorAction.Init();
	}

	private static bool GetCachedEntity(long id, out LevelEntity entity)
	{
		entity = null;
		for (int i = 0; i < undoObjects.Count; i++)
		{
			entity = undoObjects[i];
			if (entity.identifier == id)
			{
				return true;
			}
		}
		return false;
	}

	public static void ReplaceEntity(LevelEntity entity, long oldID)
	{
		if (!levelEditor)
		{
			levelEditor = LevelEditor.Instance;
		}
		int num = 0;
		LevelEntity entity2;
		if (GetCachedEntity(oldID, out entity2))
		{
			Transform transform = entity.transform;
			transform.position = entity2.Position;
			transform.rotation = entity2.Rotation;
			transform.localScale = entity2.Scale;
			if (entity2.IsSelected)
			{
				levelEditor.Select(entity, true, false);
				entity2.Select(false);
			}
			levelEditor.DestroyEntity(entity2);
			undoObjects.Remove(entity2);
		}
		for (num = 0; num < undoList.Count; num++)
		{
			undoList[num].Replace(entity, oldID);
		}
		for (num = 0; num < redoList.Count; num++)
		{
			redoList[num].Replace(entity, oldID);
		}
	}

	public static void CacheEntity(LevelEntity entity)
	{
		if (levelEditor == null)
		{
			levelEditor = LevelEditor.Instance;
		}
		levelEditor.RemoveSelect(entity);
		undoObjects.Add(entity);
		entity.gameObject.SetActive(false);
		entity.isCached = true;
		entity.transform.parent = null;
		levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Remove);
	}

	public static void ClearCache()
	{
		for (int i = 0; i < redoList.Count; i++)
		{
			redoList[i].FreeMemory();
		}
	}

	public static void Reset()
	{
		int num = 0;
		ClearCache();
		for (num = 0; num < undoList.Count; num++)
		{
			undoList[num].FreeMemory();
		}
		undoList.Clear();
		for (num = 0; num < redoList.Count; num++)
		{
			redoList[num].FreeMemory();
		}
		redoList.Clear();
	}

	public static void Redo()
	{
		int num = redoList.Count - 1;
		if (num >= 0)
		{
			LevelUndoAction levelUndoAction = redoList[num];
			undoList.Add(levelUndoAction);
			redoList.RemoveAt(num);
			levelUndoAction.Redo();
		}
	}

	public static void Undo()
	{
		int num = undoList.Count - 1;
		if (num >= 0)
		{
			LevelUndoAction levelUndoAction = undoList[num];
			redoList.Add(levelUndoAction);
			undoList.RemoveAt(num);
			levelUndoAction.Undo();
		}
	}
}
