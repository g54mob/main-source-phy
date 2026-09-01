using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Input/Escape Menu Open Blocker")]
public sealed class EscapeMenuOpenBlocker : MonoBehaviour
{
	[Header("Identification")]
	[SerializeField]
	[Tooltip("Human-readable label for this blocker.\nPurpose:\n• Shown in debug logs when this blocker is preventing the escape menu from opening.\n• Does not affect runtime behaviour; purely informational.\nExamples:\n• \"Cutscene Player\"\n• \"Console UI\"\n• \"Dialogue System\"")]
	private string blockerLabel;

	[Header("Lookup")]
	[SerializeField]
	[Tooltip("Tag used to find the EscapeMenuToggleUnityEvent in the scene.\nRules:\n• Must match the tag set on the escape menu's GameObject exactly.\n• Default: \"EscapeMenu\" — change only if you renamed the tag.\nNotes:\n• The tag must be declared in Unity's Tag Manager before use.\n• If no object with this tag is found, a warning is logged and the blocker\n  silently does nothing (fail-open for the menu).")]
	private string escapeMenuTag;

	private EscapeMenuToggleUnityEvent cachedEscapeMenu;

	public string BlockerLabel => null;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
	}

	private void Register()
	{
	}

	private void Unregister()
	{
	}

	private EscapeMenuToggleUnityEvent GetEscapeMenu()
	{
		return null;
	}
}
