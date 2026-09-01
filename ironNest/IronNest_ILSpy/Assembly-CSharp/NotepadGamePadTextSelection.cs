using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NotepadGamePadTextSelection : MonoBehaviour
{
	private NotepadLineRangeDeleterTMP topNotepad;

	private NotepadLineRangeDeleterTMP bottomNotepad;

	private DynamicCursorManager cursorManager;

	private ClipboardStateController clipboardStateController;

	private InputActionReference upAction;

	private InputActionReference downAction;

	private NotepadLineRangeDeleterTMP currentNotepad;

	private int currentLine;

	private void Update()
	{
		//IL_00ea: Expected I4, but got I8
		//IL_0114: Expected I4, but got I8
		if (cursorManager.IsCurrentDeviceGamepad() && clipboardStateController.IsFocused)
		{
			CheckForInput();
		}
		if (!clipboardStateController.IsFocused && !clipboardStateController.IsHidden)
		{
			currentNotepad = null;
			if (cursorManager.IsCurrentDeviceGamepad())
			{
				NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP = topNotepad;
				notepadLineRangeDeleterTMP._003CHoveredLineIndex_003Ek__BackingField = -1;
				notepadLineRangeDeleterTMP.ClearSelectionAndHighlights();
				NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP2 = bottomNotepad;
				notepadLineRangeDeleterTMP2._003CHoveredLineIndex_003Ek__BackingField = -1;
				notepadLineRangeDeleterTMP2.ClearSelectionAndHighlights();
			}
		}
	}

	private void CheckForInput()
	{
		//IL_0296: Expected I4, but got I8
		//IL_04b2: Expected I4, but got I8
		//IL_03d9: Expected I4, but got I8
		//IL_05f5: Expected I4, but got I8
		if (currentNotepad == null)
		{
			NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP = topNotepad;
			TMP_TextInfo textInfo = notepadLineRangeDeleterTMP.sourceTMP.textInfo;
			NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP3;
			if (textInfo.lineCount <= 0)
			{
				NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP2 = bottomNotepad;
				TMP_TextInfo textInfo2 = notepadLineRangeDeleterTMP2.sourceTMP.textInfo;
				if (textInfo2.lineCount <= 0)
				{
					goto IL_00df;
				}
				notepadLineRangeDeleterTMP3 = bottomNotepad;
			}
			else
			{
				notepadLineRangeDeleterTMP3 = topNotepad;
			}
			currentNotepad = notepadLineRangeDeleterTMP3;
			goto IL_00df;
		}
		goto IL_00ef;
		IL_00df:
		currentLine = 0;
		goto IL_00ef;
		IL_0815:
		currentLine = 0;
		goto IL_07fc;
		IL_07fc:
		NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP4;
		currentNotepad = notepadLineRangeDeleterTMP4;
		NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP5 = currentNotepad;
		notepadLineRangeDeleterTMP5._003CHoveredLineIndex_003Ek__BackingField = currentLine;
		if (currentLine >= 0 && notepadLineRangeDeleterTMP5.highlightHoveredLine)
		{
			notepadLineRangeDeleterTMP5._003CSelectedLineMin_003Ek__BackingField = currentLine;
			notepadLineRangeDeleterTMP5._003CSelectedLineMax_003Ek__BackingField = currentLine;
			notepadLineRangeDeleterTMP5.UpdateHighlightsForSelectionRange();
		}
		else
		{
			notepadLineRangeDeleterTMP5.ClearSelectionAndHighlights();
		}
		goto IL_069c;
		IL_069c:
		if (!(currentNotepad != null))
		{
			return;
		}
		NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP6 = currentNotepad;
		int num = currentLine;
		TMP_TextInfo textInfo3 = notepadLineRangeDeleterTMP6.sourceTMP.textInfo;
		if (currentLine >= 0)
		{
			if (num > textInfo3.lineCount)
			{
				num = textInfo3.lineCount;
			}
		}
		else
		{
			num = 0;
		}
		NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP7 = currentNotepad;
		currentLine = num;
		notepadLineRangeDeleterTMP7._003CHoveredLineIndex_003Ek__BackingField = num;
		if (num >= 0 && notepadLineRangeDeleterTMP7.highlightHoveredLine)
		{
			notepadLineRangeDeleterTMP7._003CSelectedLineMin_003Ek__BackingField = num;
			notepadLineRangeDeleterTMP7._003CSelectedLineMax_003Ek__BackingField = num;
			notepadLineRangeDeleterTMP7.UpdateHighlightsForSelectionRange();
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 1095 Invalid \"Jump target not found in method: 0x180446700\"");
		throw new NullReferenceException();
		IL_00ef:
		InputAction action = upAction.action;
		action.Enable();
		InputAction action2 = downAction.action;
		action2.Enable();
		InputAction action3 = upAction.action;
		if (!action3.WasPressedThisFrame())
		{
			InputAction action4 = downAction.action;
			if (action4.WasPressedThisFrame())
			{
				int num2 = currentLine + 1;
				currentLine = num2;
			}
		}
		else
		{
			int num3 = currentLine - 1;
			currentLine = num3;
		}
		if (currentNotepad == topNotepad && currentLine < 0)
		{
			NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP8 = bottomNotepad;
			TMP_TextInfo textInfo4 = notepadLineRangeDeleterTMP8.sourceTMP.textInfo;
			if (textInfo4.lineCount > 0)
			{
				NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP9 = currentNotepad;
				notepadLineRangeDeleterTMP9._003CHoveredLineIndex_003Ek__BackingField = -1;
				notepadLineRangeDeleterTMP9.ClearSelectionAndHighlights();
				NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP10 = bottomNotepad;
				TMP_TextInfo textInfo5 = notepadLineRangeDeleterTMP10.sourceTMP.textInfo;
				notepadLineRangeDeleterTMP4 = bottomNotepad;
				int num4 = textInfo5.lineCount - 1;
				currentLine = num4;
				goto IL_07fc;
			}
		}
		else
		{
			if (currentNotepad == topNotepad)
			{
				NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP11 = topNotepad;
				TMP_TextInfo textInfo6 = notepadLineRangeDeleterTMP11.sourceTMP.textInfo;
				if (currentLine >= textInfo6.lineCount)
				{
					NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP12 = bottomNotepad;
					TMP_TextInfo textInfo7 = notepadLineRangeDeleterTMP12.sourceTMP.textInfo;
					if (textInfo7.lineCount > 0)
					{
						NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP13 = currentNotepad;
						notepadLineRangeDeleterTMP13._003CHoveredLineIndex_003Ek__BackingField = -1;
						notepadLineRangeDeleterTMP13.ClearSelectionAndHighlights();
						notepadLineRangeDeleterTMP4 = bottomNotepad;
						goto IL_0815;
					}
					goto IL_069c;
				}
			}
			if (currentNotepad == bottomNotepad && currentLine < 0)
			{
				NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP14 = topNotepad;
				TMP_TextInfo textInfo8 = notepadLineRangeDeleterTMP14.sourceTMP.textInfo;
				if (textInfo8.lineCount > 0)
				{
					NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP15 = currentNotepad;
					notepadLineRangeDeleterTMP15._003CHoveredLineIndex_003Ek__BackingField = -1;
					notepadLineRangeDeleterTMP15.ClearSelectionAndHighlights();
					NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP16 = topNotepad;
					TMP_TextInfo textInfo9 = notepadLineRangeDeleterTMP16.sourceTMP.textInfo;
					notepadLineRangeDeleterTMP4 = topNotepad;
					int num5 = textInfo9.lineCount - 1;
					currentLine = num5;
					goto IL_07fc;
				}
			}
			else if (currentNotepad == bottomNotepad)
			{
				NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP17 = bottomNotepad;
				TMP_TextInfo textInfo10 = notepadLineRangeDeleterTMP17.sourceTMP.textInfo;
				if (currentLine >= textInfo10.lineCount)
				{
					NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP18 = topNotepad;
					TMP_TextInfo textInfo11 = notepadLineRangeDeleterTMP18.sourceTMP.textInfo;
					if (textInfo11.lineCount > 0)
					{
						NotepadLineRangeDeleterTMP notepadLineRangeDeleterTMP19 = currentNotepad;
						notepadLineRangeDeleterTMP19._003CHoveredLineIndex_003Ek__BackingField = -1;
						notepadLineRangeDeleterTMP19.ClearSelectionAndHighlights();
						notepadLineRangeDeleterTMP4 = topNotepad;
						goto IL_0815;
					}
				}
			}
		}
		goto IL_069c;
	}
}
