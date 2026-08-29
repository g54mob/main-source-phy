using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UndoSystem
{
	private readonly List<EditorAction> actions = new List<EditorAction>();

	private int undoAt = -1;

	private int groupAt = -1;

	private string groupName = string.Empty;

	public bool isDirty;

	private int lastDirtyChangeCount;

	public int stackIndex => undoAt;

	public bool isUndoable
	{
		get
		{
			if (groupAt != -1)
			{
				return undoAt > groupAt;
			}
			return undoAt >= 0;
		}
	}

	public bool isRedoable => undoAt < actions.Count - 1;

	public void register(params EditorAction[] newActions)
	{
		pushInternal(dry: true, newActions);
	}

	public void push(params EditorAction[] newActions)
	{
		pushInternal(dry: false, newActions);
	}

	private void pushInternal(bool dry, EditorAction[] newActions)
	{
		if (newActions.Length != 0)
		{
			for (int i = 0; i < newActions.Length; i++)
			{
				newActions[i].groupName = groupName;
			}
			clearRedoActions();
			actions.AddRange(newActions);
			newActions[0].redoCount = newActions.Length;
			newActions[^1].undoCount = newActions.Length;
			redoLast(dry);
		}
	}

	public void undoLast(bool log = false)
	{
		if (!isUndoable)
		{
			return;
		}
		int undoCount = actions[undoAt].undoCount;
		if (undoCount > 1 && log)
		{
			Debug.Log($"=== Undo {undoCount} combined actions ===");
		}
		for (int i = 0; i < undoCount; i++)
		{
			if (log)
			{
				Debug.Log("undo: " + actions[undoAt].name);
			}
			actions[undoAt--].undo();
		}
		updateDirtyFlag();
	}

	public void redoLast(bool dry = false, bool log = false)
	{
		if (!isRedoable)
		{
			return;
		}
		int redoCount = actions[undoAt + 1].redoCount;
		if (redoCount > 1 && log)
		{
			Debug.Log($"=== Redo {redoCount} combined actions ===");
		}
		for (int i = 0; i < redoCount; i++)
		{
			undoAt++;
			if (!dry)
			{
				actions[undoAt].redo();
			}
			if (log)
			{
				Debug.Log((dry ? "dry " : "") + "redo: " + actions[undoAt].name);
			}
		}
		updateDirtyFlag();
	}

	public void beginGroup(string name)
	{
		groupName = name;
		groupAt = undoAt;
	}

	public void endGroup(bool keepChanges = true)
	{
		clearRedoActions();
		int num = undoAt - groupAt;
		linkLast(num);
		if (num > 0 && !keepChanges)
		{
			undoLast();
			clearRedoActions();
		}
		groupName = string.Empty;
		groupAt = -1;
	}

	public void linkLast(int count)
	{
		if (count > 0)
		{
			List<EditorAction> list = actions;
			list[list.Count - count].redoCount = count;
			List<EditorAction> list2 = actions;
			list2[list2.Count - 1].undoCount = count;
		}
	}

	public int calculateUndoableActionCount()
	{
		if (!isUndoable)
		{
			return 0;
		}
		int num = ((groupAt != -1) ? (groupAt + 1) : 0);
		int num2 = 0;
		for (int i = num; i <= undoAt; i += actions[i].redoCount)
		{
			num2++;
		}
		return num2;
	}

	private void clearRedoActions()
	{
		if (isRedoable)
		{
			int num = undoAt + 1;
			actions.RemoveRange(num, actions.Count - num);
		}
	}

	public void reset()
	{
		actions.Clear();
		undoAt = -1;
		groupAt = -1;
		groupName = string.Empty;
		isDirty = false;
		lastDirtyChangeCount = 0;
	}

	private void updateDirtyFlag()
	{
		int num = 0;
		for (int i = 0; i <= undoAt; i++)
		{
			num += (actions[i].affectsRoomState ? 1 : 0);
		}
		if (num != lastDirtyChangeCount)
		{
			isDirty = true;
		}
		lastDirtyChangeCount = num;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(1024);
		stringBuilder.Append($"=== Undo Stack ({actions.Count} actions) ===");
		for (int num = actions.Count - 1; num >= 0; num--)
		{
			stringBuilder.Append("\n");
			stringBuilder.Append((undoAt == num) ? $"[{num}] ->\t" : $"[{num}]\t");
			stringBuilder.Append(string.IsNullOrEmpty(actions[num].groupName) ? "" : ("[" + actions[num].groupName + "] "));
			stringBuilder.Append(actions[num].name);
		}
		if (undoAt < 0)
		{
			stringBuilder.Append("\n->");
		}
		stringBuilder.AppendLine();
		return stringBuilder.ToString();
	}
}
