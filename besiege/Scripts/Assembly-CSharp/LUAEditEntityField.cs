public class LUAEditEntityField : LevelUndoAction
{
	private XData previousData;

	public LUAEditEntityField(LevelEntity entity, XData prevData)
		: base(entity)
	{
		previousData = prevData;
	}

	public override void Undo()
	{
		if ((bool)entity && !entity.isCached)
		{
			GenericEntity behaviour = entity.behaviour;
			XData data = behaviour.GetData(previousData.Key);
			NetworkEditFieldHandler networkEditFieldHandler = EditFieldHandler.Instance as NetworkEditFieldHandler;
			networkEditFieldHandler.OnEditEntityField(behaviour, previousData, true);
			previousData = data;
		}
	}

	public override void Redo()
	{
		if ((bool)entity && !entity.isCached)
		{
			GenericEntity behaviour = entity.behaviour;
			XData data = behaviour.GetData(previousData.Key);
			NetworkEditFieldHandler networkEditFieldHandler = EditFieldHandler.Instance as NetworkEditFieldHandler;
			networkEditFieldHandler.OnEditEntityField(behaviour, previousData, true);
			previousData = data;
		}
	}
}
