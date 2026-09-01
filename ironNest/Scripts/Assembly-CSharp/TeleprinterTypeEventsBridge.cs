using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class TeleprinterTypeEventsBridge : MonoBehaviour
{
	[Header("Source")]
	[Tooltip("Optional explicit reference to the Teleprinter to observe. If assigned, it is used and 'Teleprinter Type' is ignored.")]
	[SerializeField]
	private Teleprinter teleprinter;

	[Tooltip("If 'Teleprinter' reference is not assigned, this Teleprinter type will be looked up via Teleprinter.GetTeleprinter(type).")]
	[SerializeField]
	private Teleprinter.Teleprinters teleprinterType;

	[Header("Filtering")]
	[Tooltip("If true, characters '\\n' and '\\r' are NOT treated as typed characters (event #1) and NOT treated as spaces (event #2). They may still cause line transitions (event #3) because TMP lineNumber changes across layout.")]
	[SerializeField]
	private bool excludeLineBreaksFromCharEvents;

	[Tooltip("If true, these characters trigger the 'Space Typed' event (#2):\n- Regular space ' ' (U+0020)\n- Tab '\\t' (U+0009)\n- Non-breaking space '\\u00A0' (U+00A0)\n\nIf false, only regular space ' ' triggers event #2.")]
	[SerializeField]
	private bool treatTabAndNbspAsSpace;

	[Header("Events")]
	[Tooltip("Event #1: Invoked every time a non-space character is typed/revealed.\nThis excludes:\n- Regular space ' '\n- Tab '\\t' (if enabled)\n- Non-breaking space '\\u00A0' (if enabled)\n- Line breaks '\\n' and '\\r' (if 'Exclude Line Breaks From Char Events' is true)\n\nParameter: the character that was typed.")]
	public UnityEvent<char> onNonSpaceCharacterTyped;

	[Tooltip("Event #2: Invoked every time a space-like character is typed/revealed.\nBy default includes:\n- Regular space ' '\n- Tab '\\t'\n- Non-breaking space '\\u00A0'\n\nParameter: the character that was typed (e.g. ' ', '\\t', '\\u00A0').")]
	public UnityEvent<char> onSpaceTyped;

	[Tooltip("Event #3: Invoked once per TMP line transition during typing.\nThis uses TMP's characterInfo[i].lineNumber changes, so it includes:\n- explicit line breaks (newlines)\n- word wrap / auto layout line changes\n\nParameters:\n- fromLine: previous TMP line number\n- toLine: new TMP line number")]
	public UnityEvent<int, int> onLineTransition;

	private int? _lastSeenLineNumber;

	private bool _subscribed;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void ResolveTeleprinter()
	{
	}

	private void Subscribe()
	{
	}

	private void Unsubscribe()
	{
	}

	private void HandleCharacterPrintedDetailed(char character, int charIndex, int lineNumber)
	{
	}

	private void HandleLineTransition(int fromLine, int toLine)
	{
	}

	private bool IsSpaceLike(char c)
	{
		return false;
	}
}
