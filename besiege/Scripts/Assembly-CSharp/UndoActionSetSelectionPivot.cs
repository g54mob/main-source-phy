using System;

public class UndoActionSetSelectionPivot : UndoAction
{
	private Guid prev;

	public UndoActionSetSelectionPivot(Machine m, Guid oldBlock, Guid newBlock)
	{
		prev = oldBlock;
		guid = newBlock;
		machine = m;
	}

	public override bool Redo()
	{
		return AdvancedBlockEditor.Instance.SetBlockAsLast(machine, guid);
	}

	public override bool Undo()
	{
		return AdvancedBlockEditor.Instance.SetBlockAsLast(machine, prev);
	}
}
