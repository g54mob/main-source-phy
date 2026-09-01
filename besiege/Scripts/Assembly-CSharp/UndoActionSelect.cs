using System;

public class UndoActionSelect : UndoAction
{
	private readonly StatMaster.Tool tool;

	private readonly bool extra;

	private readonly int symmetryIndex;

	private readonly float transformMultiplier;

	public UndoActionSelect(Machine m, Guid g, bool isExtra, int index, float multiplier)
	{
		guid = g;
		tool = StatMaster.Mode.selectedTool;
		machine = m;
		extra = isExtra;
		symmetryIndex = index;
		transformMultiplier = multiplier;
	}

	public override bool Redo()
	{
		return AdvancedBlockEditor.Instance.Select(tool, machine, guid, extra, symmetryIndex, transformMultiplier);
	}

	public override bool Undo()
	{
		return AdvancedBlockEditor.Instance.Deselect(tool, machine, guid);
	}
}
