public class LUADeselectEntity : LevelUndoAction
{
	public LUADeselectEntity(LevelEntity entity)
		: base(entity)
	{
		isSelectAction = true;
	}

	public override void Undo()
	{
		if ((bool)entity && !entity.isCached)
		{
			levelEditor.Select(entity, true, false);
		}
	}

	public override void Redo()
	{
		if ((bool)entity && !entity.isCached)
		{
			levelEditor.Deselect(entity, false);
		}
	}
}
