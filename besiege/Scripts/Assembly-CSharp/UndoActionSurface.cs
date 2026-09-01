using System;

public class UndoActionSurface : UndoAction
{
	private readonly bool isAdd;

	private readonly int index;

	public UndoActionSurface(Machine m, Guid nodeGuid, int symmetryIndex, bool add)
	{
		machine = m;
		index = symmetryIndex;
		guid = nodeGuid;
		isAdd = add;
	}

	public override bool Redo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			machine.nodeController.Toggle(block, isAdd, index);
		}
		return true;
	}

	public override bool Undo()
	{
		BlockBehaviour block;
		if (machine.GetBlock(guid, out block))
		{
			machine.nodeController.Toggle(block, !isAdd, index);
		}
		return true;
	}
}
