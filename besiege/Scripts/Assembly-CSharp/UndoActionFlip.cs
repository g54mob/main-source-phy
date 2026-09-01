public class UndoActionFlip : UndoAction
{
	private readonly bool flipped;

	public UndoActionFlip(Machine m, BlockBehaviour block)
	{
		flipped = block.Flipped;
		machine = m;
		guid = block.Guid;
	}

	public override bool Redo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block) && block.Flipped != flipped)
		{
			machine.ReverseBlock(block, false, true);
		}
		return true;
	}

	public override bool Undo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block) && block.Flipped == flipped)
		{
			machine.ReverseBlock(block, false, true);
		}
		return true;
	}
}
