using System;

public class EditorAction
{
	public string name;

	public string groupName;

	public Action redo;

	public Action undo;

	public int redoCount;

	public int undoCount;

	public bool affectsRoomState = true;
}
