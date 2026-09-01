using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Notepad/Writers/Odometer Note Logger")]
public class OdometerNoteLogger : MonoBehaviour
{
	[Header("References")]
	[Tooltip("Reference to the OdometerDisplay that exposes the current reading via 'DisplayedNumber'. Assign in the Inspector, or this component will try GetComponent<OdometerDisplay>() at runtime.")]
	public OdometerDisplay odometerDisplay;

	[Header("Routing")]
	[Tooltip("Optional direct reference to the target NotepadSection. If not set and 'Auto-Find' is enabled, this logger will try to resolve a section by Unity Tag across loaded scenes.\nIf both are set, this reference takes precedence.")]
	public NotepadSection targetSection;

	[Tooltip("If enabled and 'Target Section' is not assigned, the logger will try to resolve a NotepadSection by Unity Tag across currently loaded scenes and when new scenes load.")]
	[SerializeField]
	private bool autoFindSection;

	[Tooltip("Unity Tag to locate the NotepadSection GameObject at runtime. Designers can type any tag name here.\nImportant: The tag must be defined in Project Settings > Tags and Layers, and assigned to the NotepadSection's GameObject.\nExamples: \"Ballistics\", \"RangeSection\", \"MainData\"")]
	[SerializeField]
	private string sectionTag;

	[Header("Note Format")]
	[Tooltip("Format string for the note to log. Supported tokens:\n  - {value} : Replaced with the odometer value formatted to 2 decimal places (e.g., 123.45)\n\nRules:\n  - Tokens are case-sensitive.\n  - Unknown tokens remain unchanged.\n  - Value uses invariant culture with a dot as decimal separator.\n\nExamples:\n  \"Odometer Reading: {value}\"\n  \"ODO: {value} km\"")]
	public string noteFormat;

	[Header("Write Options")]
	[Tooltip("Write behavior:\n  - Add: Add new content to existing section content.\n  - Replace: Replace the entire section content with the new entry.\nIf 'Replace' is used, only the latest entry will be visible.")]
	[SerializeField]
	private NotepadSection.WriteMode writeMode;

	[Tooltip("If 'Write Mode' is Add, controls where new content goes:\n  - Top: The new entry is placed at the beginning of the section.\n  - Bottom: The new entry is placed at the end of the section.")]
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

	public void LogOdometerNote()
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
}
