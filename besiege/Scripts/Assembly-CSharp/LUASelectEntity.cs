public class LUASelectEntity : LevelUndoAction
{
	public LUASelectEntity(LevelEntity entity)
		: base(entity)
	{
		isSelectAction = true;
	}

	public override void Undo()
	{
		if ((bool)entity && !entity.isCached)
		{
			levelEditor.Deselect(entity, false);
		}
	}

	public override void Redo()
	{
		if ((bool)entity && !entity.isCached)
		{
			levelEditor.Select(entity, true, false);
		}
	}
}
