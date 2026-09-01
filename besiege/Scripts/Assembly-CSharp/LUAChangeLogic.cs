public class LUAChangeLogic : LevelUndoAction
{
	private XDataHolder previousData;

	public LUAChangeLogic(LevelEntity entity, XDataHolder prevData)
		: base(entity)
	{
		previousData = prevData;
	}

	public override void Undo()
	{
		if ((bool)entity && !entity.isCached)
		{
			GenericEntity behaviour = entity.behaviour;
			XDataHolder data = new XDataHolder();
			behaviour.OnSaveLogicLoadValue(data);
			NetworkEditFieldHandler networkEditFieldHandler = EditFieldHandler.Instance as NetworkEditFieldHandler;
			networkEditFieldHandler.OnEditEntityState(behaviour, true, previousData, CopyMode.Logic);
			previousData = data;
			LevelEditor.Instance.SetActiveTool(StatMaster.Tool.Modify);
			BlockMapper blockMapper = BlockMapper.CurrentInstance;
			if (!blockMapper || blockMapper.Current != entity)
			{
				blockMapper = BlockMapper.Open(behaviour);
			}
			if (blockMapper != null && !blockMapper.IsLogic)
			{
				blockMapper.ToggleLogic(true);
			}
		}
	}

	public override void Redo()
	{
		if ((bool)entity && !entity.isCached)
		{
			GenericEntity behaviour = entity.behaviour;
			XDataHolder data = new XDataHolder();
			behaviour.OnSaveLogicLoadValue(data);
			NetworkEditFieldHandler networkEditFieldHandler = EditFieldHandler.Instance as NetworkEditFieldHandler;
			networkEditFieldHandler.OnEditEntityState(behaviour, true, previousData, CopyMode.Logic);
			previousData = data;
			LevelEditor.Instance.SetActiveTool(StatMaster.Tool.Modify);
			BlockMapper blockMapper = BlockMapper.CurrentInstance;
			if (!blockMapper || blockMapper.Current != entity)
			{
				blockMapper = BlockMapper.Open(behaviour);
			}
			if (blockMapper != null && !blockMapper.IsLogic)
			{
				blockMapper.ToggleLogic(true);
			}
		}
	}
}
