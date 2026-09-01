using UnityEngine;
using UnityEngine.InputSystem;

public class NotepadGamePadTextSelection : MonoBehaviour
{
	[SerializeField]
	private NotepadLineRangeDeleterTMP topNotepad;

	[SerializeField]
	private NotepadLineRangeDeleterTMP bottomNotepad;

	[SerializeField]
	private DynamicCursorManager cursorManager;

	[SerializeField]
	private ClipboardStateController clipboardStateController;

	[SerializeField]
	private InputActionReference upAction;

	[SerializeField]
	private InputActionReference downAction;

	private NotepadLineRangeDeleterTMP currentNotepad;

	private int currentLine;

	private void Update()
	{
	}

	private void CheckForInput()
	{
	}
}
