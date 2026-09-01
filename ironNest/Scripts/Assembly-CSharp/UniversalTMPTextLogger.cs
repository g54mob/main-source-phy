using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Notepad/Writers/Universal TMP Text Logger")]
public class UniversalTMPTextLogger : MonoBehaviour
{
	public enum LineSelectionMode
	{
		All = 0,
		FirstN = 1,
		LastN = 2
	}

	[Header("Source")]
	[Tooltip("The TMP_Text (TextMeshPro 3D or TextMeshProUGUI) whose text you want to log.\nThis is required for logging to work.")]
	public TMP_Text sourceTMP;

	[Header("Routing")]
	[Tooltip("Optional direct reference to the target NotepadSection. If not set and 'Auto-Find' is enabled, this logger will try to resolve a section by Unity Tag across loaded scenes.\nIf both are set, this reference takes precedence.")]
	public NotepadSection targetSection;

	[Tooltip("If enabled and 'Target Section' is not assigned, the logger will try to resolve a NotepadSection by Unity Tag across currently loaded scenes and when new scenes load.")]
	[SerializeField]
	private bool autoFindSection;

	[Tooltip("Unity Tag to locate the NotepadSection GameObject at runtime. Designers can type any tag name here.\nImportant: The tag must be defined in Project Settings > Tags and Layers, and assigned to the NotepadSection's GameObject.\nExamples: \"MainData\", \"Ballistics\", \"RangeSection\"")]
	[SerializeField]
	private string sectionTag;

	[Header("Note Format")]
	[Tooltip("Format string for the note to log. Supported tokens:\n  - {text} : Replaced with the selected text from the source TMP_Text\n\nRules:\n  - Tokens are case-sensitive.\n  - Unknown tokens remain unchanged.\n\nExamples:\n  \"Note: {text}\"\n  \"Captured: {text}\"")]
	public string noteFormat;

	[Header("Line Selection From Source")]
	[Tooltip("Which lines from the source TMP_Text to capture:\n  - All: Use the full text.\n  - FirstN: Use only the first N lines.\n  - LastN: Use only the last N lines.")]
	[SerializeField]
	private LineSelectionMode lineSelection;

	[Tooltip("Number of lines to capture when using 'FirstN' or 'LastN'.\nIgnored if 'Line Selection' is 'All'.")]
	[SerializeField]
	private int lineCount;

	[Header("Write Options")]
	[Tooltip("Write behavior:\n  - Add: Add new content to existing section content.\n  - Replace: Replace the entire section content with the selected text.\nIf 'Replace' is used, only the latest captured text will be visible.")]
	[SerializeField]
	private NotepadSection.WriteMode writeMode;

	[Tooltip("If 'Write Mode' is Add, controls where new content goes:\n  - Top: The new content is placed at the beginning of the section.\n  - Bottom: The new content is placed at the end of the section.")]
	[SerializeField]
	private NotepadSection.AddPosition addPosition;

	private static readonly HashSet<string> s_WarnedMissingTags;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnValidate()
	{
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	public void LogTMPTextToNotepad()
	{
	}

	[ContextMenu("Try Resolve Section Now")]
	private void ContextTryResolve()
	{
	}

	private bool TryResolveSection(string context)
	{
		return false;
	}

	private static string SelectLines(string text, LineSelectionMode mode, int count)
	{
		return null;
	}
}
