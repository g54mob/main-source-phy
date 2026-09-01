using System.Collections.Generic;

public class LUAMultiAction : LevelUndoAction
{
	private LevelUndoAction[] levelUndoAction;

	public LUAMultiAction(LevelUndoAction[] levelActions)
	{
		levelEditor = LevelEditor.Instance;
		placedEntities = new List<EntityController.PlaceEntry>();
		removedEntities = new List<long>();
		this.levelUndoAction = levelActions;
		LevelUndoAction[] array = this.levelUndoAction;
		foreach (LevelUndoAction levelUndoAction in array)
		{
			levelUndoAction.SetMultiAction(true);
		}
	}

	public override void Undo()
	{
		for (int num = this.levelUndoAction.Length - 1; num >= 0; num--)
		{
			LevelUndoAction levelUndoAction = this.levelUndoAction[num];
			if (levelUndoAction != null && !levelUndoAction.isSelectAction)
			{
				levelUndoAction.Undo();
			}
		}
		ProcessEntities();
		for (int num = this.levelUndoAction.Length - 1; num >= 0; num--)
		{
			LevelUndoAction levelUndoAction = this.levelUndoAction[num];
			if (levelUndoAction != null && levelUndoAction.isSelectAction)
			{
				levelUndoAction.Undo();
			}
		}
	}

	public override void Redo()
	{
		for (int i = 0; i < this.levelUndoAction.Length; i++)
		{
			LevelUndoAction levelUndoAction = this.levelUndoAction[i];
			if (levelUndoAction != null && !levelUndoAction.isSelectAction)
			{
				levelUndoAction.Redo();
			}
		}
		ProcessEntities();
		for (int i = 0; i < this.levelUndoAction.Length; i++)
		{
			LevelUndoAction levelUndoAction = this.levelUndoAction[i];
			if (levelUndoAction != null && levelUndoAction.isSelectAction)
			{
				levelUndoAction.Redo();
			}
		}
	}

	public override void Replace(LevelEntity newEntity, long oldID)
	{
		for (int i = 0; i < this.levelUndoAction.Length; i++)
		{
			LevelUndoAction levelUndoAction = this.levelUndoAction[i];
			if (levelUndoAction != null)
			{
				levelUndoAction.Replace(newEntity, oldID);
			}
		}
	}

	public override void FreeMemory()
	{
		for (int i = 0; i < this.levelUndoAction.Length; i++)
		{
			LevelUndoAction levelUndoAction = this.levelUndoAction[i];
			if (levelUndoAction != null)
			{
				levelUndoAction.FreeMemory();
			}
		}
	}

	private void ProcessEntities()
	{
		for (int i = 0; i < this.levelUndoAction.Length; i++)
		{
			LevelUndoAction levelUndoAction = this.levelUndoAction[i];
			if (levelUndoAction != null)
			{
				placedEntities.AddRange(levelUndoAction.placedEntities);
				removedEntities.AddRange(levelUndoAction.removedEntities);
				levelUndoAction.placedEntities.Clear();
				levelUndoAction.removedEntities.Clear();
			}
		}
		UpdateObjects();
	}
}
