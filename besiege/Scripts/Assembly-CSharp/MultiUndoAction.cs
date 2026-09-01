using System.Collections.Generic;

public class MultiUndoAction : UndoAction
{
	public UndoAction[] actions;

	private StatMaster.Tool prevTool = StatMaster.Tool.None;

	private StatMaster.Tool tool = StatMaster.Tool.None;

	public MultiUndoAction(Machine m, UndoAction[] undoActions)
	{
		actions = undoActions;
		machine = m;
		foreach (UndoAction undoAction in undoActions)
		{
			undoAction.isMultiAction = true;
			changesTransform = undoAction.changesTransform || changesTransform;
			changesCount = undoAction.changesCount || changesCount;
			changesOBM = undoAction.changesOBM || changesOBM;
		}
		isMultiAction = true;
	}

	public StatMaster.Tool GetTool()
	{
		return tool;
	}

	public MultiUndoAction SetTool(StatMaster.Tool prev, StatMaster.Tool next)
	{
		prevTool = prev;
		tool = next;
		return this;
	}

	public override bool Redo()
	{
		for (int i = 0; i < actions.Length; i++)
		{
			UndoAction undoAction = actions[i];
			undoAction.Redo();
		}
		if (tool != StatMaster.Tool.None || prevTool != StatMaster.Tool.None)
		{
			AdvancedBlockEditor.Instance.SetActiveTool(tool, false);
		}
		return true;
	}

	public override bool Undo()
	{
		for (int num = actions.Length - 1; num >= 0; num--)
		{
			UndoAction undoAction = actions[num];
			undoAction.Undo();
		}
		if (tool != StatMaster.Tool.None || prevTool != StatMaster.Tool.None)
		{
			AdvancedBlockEditor.Instance.SetActiveTool(tool, false);
		}
		return true;
	}

	public override List<BlockBehaviour> GetBlocks()
	{
		List<BlockBehaviour> list = new List<BlockBehaviour>();
		for (int i = 0; i < actions.Length; i++)
		{
			List<BlockBehaviour> blocks = actions[i].GetBlocks();
			foreach (BlockBehaviour item in blocks)
			{
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
		}
		return list;
	}
}
