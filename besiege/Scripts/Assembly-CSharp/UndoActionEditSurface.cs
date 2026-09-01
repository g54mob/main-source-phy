public class UndoActionEditSurface : UndoAction
{
	private readonly BlockInfo lastInfo;

	public UndoActionEditSurface(Machine m, BlockInfo newInfo, BlockInfo prevInfo)
	{
		lastInfo = prevInfo;
		info = newInfo;
		guid = info.Guid;
		machine = m;
	}

	public override bool Redo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			machine.EditBlockData(block, info.BlockData);
		}
		return true;
	}

	public override bool Undo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			machine.EditBlockData(block, lastInfo.BlockData);
		}
		return true;
	}
}
