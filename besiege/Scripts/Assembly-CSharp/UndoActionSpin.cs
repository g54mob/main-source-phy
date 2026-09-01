public class UndoActionSpin : UndoAction
{
	public UndoActionSpin(Machine m, BlockBehaviour block)
	{
		machine = m;
		guid = block.Guid;
	}

	public override bool Redo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			machine.SpinBlock(block, false, true);
		}
		return true;
	}

	public override bool Undo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			machine.SpinBlock(block, false, false);
		}
		return true;
	}
}
