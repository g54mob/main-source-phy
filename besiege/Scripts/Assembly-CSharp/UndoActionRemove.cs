public class UndoActionRemove : UndoAction
{
	public UndoActionRemove(Machine m, BlockInfo blockInfo)
	{
		info = blockInfo;
		guid = blockInfo.Guid;
		changesCount = true;
		machine = m;
	}

	public override bool Redo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			machine.RemoveBlock(block);
		}
		return true;
	}

	public override bool Undo()
	{
		BlockBehaviour block;
		if (machine.AddBlock(info, out block))
		{
			block.VisualController.PlaceFromBlockInfo(info);
		}
		return true;
	}
}
