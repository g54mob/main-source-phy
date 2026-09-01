public class LUAChangeEntityData : LevelUndoAction
{
	private XDataHolder previousData;

	private CopyMode mode;

	public LUAChangeEntityData(LevelEntity entity, XDataHolder prevData, CopyMode copyMode)
		: base(entity)
	{
		previousData = prevData;
		mode = copyMode;
	}

	public override void Undo()
	{
		if ((bool)entity && !entity.isCached)
		{
			GenericEntity behaviour = entity.behaviour;
			XDataHolder data = new XDataHolder();
			behaviour.OnSave(data, mode);
			NetworkEditFieldHandler networkEditFieldHandler = EditFieldHandler.Instance as NetworkEditFieldHandler;
			networkEditFieldHandler.OnEditEntityState(behaviour, true, previousData, mode);
			previousData = data;
		}
	}

	public override void Redo()
	{
		if ((bool)entity && !entity.isCached)
		{
			GenericEntity behaviour = entity.behaviour;
			XDataHolder data = new XDataHolder();
			behaviour.OnSave(data, mode);
			NetworkEditFieldHandler networkEditFieldHandler = EditFieldHandler.Instance as NetworkEditFieldHandler;
			networkEditFieldHandler.OnEditEntityState(behaviour, true, previousData, mode);
			previousData = data;
		}
	}
}
