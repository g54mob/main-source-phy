public class UndoActionEdit : UndoAction
{
	private readonly BlockInfo lastInfo;

	public UndoActionEdit(Machine m, BlockInfo newInfo, BlockInfo prevInfo)
	{
		changesParameters = true;
		lastInfo = prevInfo;
		info = newInfo;
		guid = info.Guid;
		machine = m;
	}

	public override bool Redo()
	{
		ApplyInfo(guid, info);
		return true;
	}

	public override bool Undo()
	{
		ApplyInfo(guid, lastInfo);
		return true;
	}
}
