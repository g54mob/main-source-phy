using System;
using System.Collections.Generic;

public class UndoActionReplaceSelection : UndoAction
{
	public class ReplaceEntry
	{
		public Guid guid;

		public bool isExtra;

		public int symmetryIndex;

		public float transformMultiplier;
	}

	public readonly List<ReplaceEntry> prevIds;

	public readonly List<ReplaceEntry> currentIds;

	public UndoActionReplaceSelection(Machine m, List<ReplaceEntry> prevList, List<ReplaceEntry> currentList)
	{
		machine = m;
		prevIds = prevList;
		currentIds = currentList;
	}

	public override bool Undo()
	{
		BlockSelectionTool selectionController = AdvancedBlockEditor.Instance.selectionController;
		SelectionTool.BatchChange = true;
		selectionController.DeselectAll(false);
		for (int i = 0; i < prevIds.Count; i++)
		{
			ReplaceEntry replaceEntry = prevIds[i];
			BlockBehaviour block;
			if (machine.GetBlock(replaceEntry.guid, out block))
			{
				selectionController.Select(block, true, false, replaceEntry.isExtra, replaceEntry.symmetryIndex, replaceEntry.transformMultiplier);
			}
		}
		SelectionTool.BatchChange = false;
		machine.Analyze();
		return true;
	}

	public override bool Redo()
	{
		BlockSelectionTool selectionController = AdvancedBlockEditor.Instance.selectionController;
		SelectionTool.BatchChange = true;
		selectionController.DeselectAll(false);
		for (int i = 0; i < currentIds.Count; i++)
		{
			ReplaceEntry replaceEntry = currentIds[i];
			BlockBehaviour block;
			if (machine.GetBlock(replaceEntry.guid, out block))
			{
				selectionController.Select(block, true, false, replaceEntry.isExtra, replaceEntry.symmetryIndex, replaceEntry.transformMultiplier);
			}
		}
		SelectionTool.BatchChange = false;
		machine.Analyze();
		return true;
	}
}
