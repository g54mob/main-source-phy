using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[RequireComponent(typeof(TMP_Text))]
public class TeleprinterLineRangeSelector3D : MonoBehaviour
{
	public enum CopyExtractionMode
	{
		RichTextRawSlice_PreserveAuthorNewlines = 0,
		VisualLayoutLinesJoined_WithSeparator = 1
	}

	public enum HighlightStyle
	{
		Solid = 0,
		Outline = 1
	}

	private struct OutlineParts
	{
		public bool IsValid;

		public Transform Top;

		public Transform Bottom;

		public Transform Left;

		public Transform Right;
	}

	[Header("Cursor Manager (DynamicCursorManager)")]
	[Tooltip("Optional direct reference to the DynamicCursorManager.\n\nIf assigned:\n- This reference takes precedence over Auto-Find by Tag.\n\nIf not assigned and Auto-Find is enabled:\n- The script will attempt to find a GameObject with the configured Unity Tag and get DynamicCursorManager from it.\n\nRequired:\n- If a manager cannot be resolved, this script remains idle (no selection/copy).")]
	[SerializeField]
	private DynamicCursorManager cursorManager;

	[Tooltip("If true and 'Cursor Manager' is not assigned, attempts to find DynamicCursorManager by Unity Tag.\n\nResolution attempts happen:\n- Awake\n- OnEnable\n- When new scenes load\n\nDisable this if you always assign Cursor Manager directly.")]
	[SerializeField]
	private bool autoFindCursorManagerByTag;

	[Tooltip("Unity Tag used to locate the DynamicCursorManager GameObject when Auto-Find is enabled.\n\nImportant:\n- The tag MUST be defined in Project Settings > Tags and Layers.\n- The GameObject that has your DynamicCursorManager component must have this tag.\n\nDefault: \"CursorManager\"")]
	[SerializeField]
	private string cursorManagerTag;

	[Header("Interactable Gate")]
	[Tooltip("Interactable that represents the teleprinter/paper surface for this selector.\n\nHow it's used:\n- If assigned: this selector only runs when the cursor manager is hovering THIS Interactable.\n- If not assigned: the script attempts to find an Interactable on this GameObject or in its parents at runtime.\n\nWhy this matters:\n- Prevents the selector from reacting while the player is hovering other objects.\n\nTip:\n- Put the Interactable on the teleprinter root (or paper) and use Interactable's collider filtering to restrict hits.")]
	[SerializeField]
	private Interactable expectedInteractable;

	[Header("TMP Source + Hit Testing")]
	[Tooltip("The TMP_Text used for line hit-testing and text extraction.\n\nIf left empty, the script auto-finds TMP_Text on the same GameObject at runtime.\n\nSelection uses TMP layout lines (TMP_TextInfo.lineInfo), which includes word-wrap (visual lines).")]
	[SerializeField]
	private TMP_Text sourceTMP;

	[Tooltip("Camera used for TMP hit-testing (TMP_TextUtilities.FindIntersectingLine).\n\nWhy you might need this:\n- DynamicCursorManager raycasts with a camera that may NOT be Camera.main.\n- TMP hit testing should use the same camera perspective for correct results.\n\nIf left empty:\n- Camera.main is used at runtime.\n\nRecommendation:\n- Assign this explicitly to your interaction camera.")]
	[SerializeField]
	private Camera hitTestCamera;

	[Header("Copy Output (Clipboard)")]
	[Tooltip("If true, on primary click release (when a press started on this teleprinter), the selected text is copied to the system clipboard\nvia GUIUtility.systemCopyBuffer.\n\nNotes:\n- No keybinds are hardcoded here; the click comes from DynamicCursorManager.\n- Disable this if you want notepad-only behavior.")]
	[SerializeField]
	private bool copyToClipboardOnRelease;

	[Header("Copy Extraction")]
	[Tooltip("Controls how selected text is extracted.\n\nRichTextRawSlice_PreserveAuthorNewlines (recommended):\n- Preserves <b>/<i>/<u> tags.\n- Preserves ONLY newlines that exist in the original source string.\n- Does NOT add newlines for visual wrap lines.\n\nVisualLayoutLinesJoined_WithSeparator (legacy):\n- Copies only visible characters (no tags).\n- Adds newlines for each visual layout line using Line Separator.")]
	[SerializeField]
	private CopyExtractionMode copyMode;

	[Tooltip("String inserted between selected TMP layout lines when using Copy Mode = VisualLayoutLinesJoined_WithSeparator.\n\nSupported tokens/codes:\n- None. This string is inserted literally.\n\nFormat rules:\n- Inserted exactly between lines.\n- No additional whitespace is automatically added.\n\nSafe examples:\n- \"\\n\" (multi-line)\n- \" | \" (joins lines with separators)")]
	[SerializeField]
	private string lineSeparator;

	[Tooltip("If true, trims the final selected text (leading/trailing whitespace removed) before writing to clipboard and/or notepad.\n\nUseful when TMP layout lines include padding spaces or trailing spaces.\nDisable if you need exact spacing preserved.")]
	[SerializeField]
	private bool trimSelectedText;

	[Header("Copy Output (Notepad)")]
	[Tooltip("If true, on primary click release (when a press started on this teleprinter), the selected text is also written to a NotepadSection.\n\nNotepadSection resolution:\n- If Target Section is assigned: it is used.\n- Else if Auto-Find is enabled: resolves by Unity Tag (Section Tag).\n\nDisable this if you only want clipboard copy.")]
	[SerializeField]
	private bool writeToNotepadOnRelease;

	[Tooltip("Optional direct reference to the NotepadSection to write into.\n\nIf assigned:\n- This reference takes precedence over Auto-Find by Tag.\n\nIf not assigned and Auto-Find is enabled:\n- The script resolves a NotepadSection by Unity Tag using NotepadSection.ResolveByTag(sectionTag).")]
	[SerializeField]
	private NotepadSection targetSection;

	[Tooltip("If true and Target Section is not assigned, attempts to resolve a NotepadSection by Unity Tag.\n\nResolution attempts happen:\n- Awake\n- OnEnable\n- When new scenes load\n- Whenever a copy/write is attempted\n\nDisable this if you always assign Target Section directly.")]
	[SerializeField]
	private bool autoFindNotepadSectionByTag;

	[Tooltip("Unity Tag used to locate the NotepadSection GameObject at runtime when Auto-Find is enabled.\n\nImportant:\n- The tag MUST be defined in Project Settings > Tags and Layers.\n- The NotepadSection's GameObject must have this tag.\n\nDefault: \"MainNotes\"")]
	[SerializeField]
	private string sectionTag;

	[Tooltip("Format string for the note written to NotepadSection.\n\nSupported tokens (case-sensitive):\n- {text} : Replaced with the selected text\n\nFormat rules:\n- Tokens are case-sensitive.\n- Unknown tokens remain unchanged.\n\nSafe examples:\n- \"{text}\"\n- \"Teleprinter:\\n{text}\"")]
	[SerializeField]
	private string noteFormat;

	[Tooltip("Write mode used when writing to NotepadSection.\n\nAdd: Adds the note to existing content.\nReplace: Replaces the entire section content.\n\nMatches NotepadSection.WriteMode.")]
	[SerializeField]
	private NotepadSection.WriteMode writeMode;

	[Tooltip("When Write Mode is Add, controls whether the new note goes at the top or bottom.\n\nTop: New content before existing content.\nBottom: New content after existing content.\n\nMatches NotepadSection.AddPosition.")]
	[SerializeField]
	private NotepadSection.AddPosition addPosition;

	[Header("Notepad Reveal Override (Recommended)")]
	[Tooltip("If true, teleprinter-triggered writes to NotepadSection will override the section's default reveal mode.\n\nWhy:\n- If you write rich text using Typewriter reveal (raw string Substring), TMP tags like <b> may visibly appear/disappear while typing.\n- For teleprinter copies, Instant reveal is usually the cleanest result.")]
	[SerializeField]
	private bool overrideNotepadRevealForTeleprinterWrites;

	[Tooltip("Reveal mode used when Override Notepad Reveal is enabled.\n\nSafe default:\n- Instant (prevents rich text tags from visibly typing).")]
	[SerializeField]
	private NotepadSection.TextRevealMode notepadRevealModeOverride;

	[Tooltip("Delay seconds used when Override Notepad Reveal is enabled.\n\nFormat rules:\n- Negative values are clamped to 0.\n\nSafe examples:\n- 0 (immediate)\n- 0.2 (slight pause before appearing)")]
	[SerializeField]
	private float notepadDelayOverrideSeconds;

	[Header("Hover / Drag Behavior")]
	[Tooltip("If true, the currently hovered TMP line is always highlighted when hovering the expected Interactable.\n\nThis matches your requirement: selection happens automatically based on hovering.\n\nIf false, no highlight is shown until the user presses primary click.")]
	[SerializeField]
	private bool highlightHoveredLine;

	[Tooltip("Drag threshold in LINES.\n\nOn primary click release:\n- If the selection range expanded by at least this many lines, it is treated as a drag-range copy.\n- Otherwise, it is treated as a single-line copy.\n\nSafe default: 1 (moving to a different line counts as a drag range).")]
	[SerializeField]
	private int dragLineThreshold;

	[Header("Highlight Visual (World Space Quads)")]
	[Tooltip("Optional prefab used to render each per-line highlight.\n\nUsage depends on Highlight Style:\n- Solid: prefab should be a single flat quad/plane-like object (local XY is width/height).\n- Outline: prefab should be a CONTAINER transform with 4 children named:\n    Top, Bottom, Left, Right\n  Each child should have a Renderer and be a thin quad aligned in local space.\n\nIf not assigned:\n- The script auto-creates the required quads at runtime.\n\nImportant:\n- Remove colliders on the prefab/children to avoid interfering with interactions.")]
	[SerializeField]
	private Transform highlightPrefab;

	[Tooltip("Material used for auto-created highlight quads.\n\nNotes:\n- For Solid: applied to the fill quad.\n- For Outline: applied to all 4 edge quads.\n\nIf left empty:\n- The script creates a basic Unlit/Color material at runtime and tints it using Highlight Color.\n\nNote:\n- In URP/HDRP, supply a transparent material for correct blending.")]
	[SerializeField]
	private Material highlightMaterial;

	[Tooltip("Highlight color (RGBA).\n\nIf using a custom material/prefab, the script attempts to set its color via:\n- _BaseColor\n- _Color\n\nSafe example:\n- (1, 1, 0, 0.20)")]
	[SerializeField]
	private Color highlightColor;

	[Tooltip("Controls how the highlight is rendered.\n\nSolid:\n- A filled rectangle covering the whole line.\n\nOutline:\n- A rectangular border drawn using 4 thin quads (Top/Bottom/Left/Right).\n- Thickness is controlled by Outline Thickness (Local).")]
	[SerializeField]
	private HighlightStyle highlightStyle;

	[Tooltip("Outline thickness in TMP LOCAL units (only used when Highlight Style = Outline).\n\nInterpretation:\n- This is the thickness of each border edge quad.\n- Because TMP line bounds are in TMP local space, the world thickness scales with the TMP transform.\n\nSafe starting values:\n- 0.01 (thin)\n- 0.03 (chunkier)\n\nNotes:\n- If the outline looks 'too fat' on small text, reduce this value.\n- If you see gaps at corners, slightly increase thickness.")]
	[SerializeField]
	private float outlineThicknessLocal;

	[Tooltip("Extra padding applied to each line highlight bounds in TMP LOCAL units.\n\nX expands left/right; Y expands up/down.\n\nBecause TMP bounds are in TMP local space, the world padding scales with the TMP transform.\n\nSafe starting values:\n- (0.03, 0.03)")]
	[SerializeField]
	private Vector2 localPadding;

	[Tooltip("Offset along the TMP text plane normal (TMP local +Z) to avoid z-fighting.\n\nHighlights are placed at Z = normalOffset in TMP local space.\n\nIncrease if you see flickering (typical 0.002 to 0.02 depending on scale).")]
	[SerializeField]
	private float normalOffset;

	[Tooltip("If true, highlight objects are parented under the TMP transform and positioned/scaled in TMP local space.\n\nRecommended for 3D TMP because line bounds are reported in TMP local units.")]
	[SerializeField]
	private bool parentHighlightsToTMP;

	[Header("Broker-based Drag Lock (Optional)")]
	[Tooltip("If true, while the player is dragging to select teleprinter lines, this selector will acquire a lock from InteractionLockBroker\nIF the drag begins while DynamicCursorManager is in FPSLocked mode.\n\nThe acquired lock request is:\n- FreezePlayerController = true\n- UseFreeMouse = true\n- UseUIActionMap = false\n\nOn drag end/disable, this selector releases ONLY its own handle.\n\nNested lock safety:\n- Releasing this handle will not override other active locks (the broker resolves state across all handles).")]
	[SerializeField]
	private bool useBrokerLockWhileDragging;

	[Tooltip("Unity Tag used to locate the InteractionLockBroker.\n\nDefault: 'LockBroker'.\n\nSetup:\n- Place one InteractionLockBroker in your master scene.\n- Tag that GameObject with this tag.\n\nRules:\n- Tag must exist in Project Settings > Tags and Layers.\n\nNo fallback:\n- If the broker is missing, a warning is logged and selection still works, but without auto FreeMouse/freeze.")]
	[SerializeField]
	private string lockBrokerTag;

	[Tooltip("Debug label sent to the broker for this selector's drag lock request.\n\nFormat rules:\n- Any string; used for logging only.\n\nSafe examples:\n- 'TeleprinterDrag:MainConsole'\n- 'TeleprinterDrag:ReactorLog'")]
	[SerializeField]
	private string brokerDebugLabel;

	[Header("Debug")]
	[Tooltip("If true, logs hover/drag/copy routing and resolution steps to the Console.\n\nDisable in production to avoid log spam.")]
	[SerializeField]
	private bool debugLogs;

	[SerializeField]
	private InputActionReference upAction;

	[SerializeField]
	private InputActionReference downAction;

	private TMP_TextInfo _ti;

	private bool _subscribed;

	private bool _isActiveHoverTarget;

	private bool _pressActive;

	private int _pressStartLine;

	private int _dragAnchorLine;

	private readonly List<Transform> _highlights;

	private readonly Dictionary<Transform, OutlineParts> _outlinePartsByRoot;

	private InteractionLockBroker _broker;

	private InteractionLockBroker.LockHandle _dragHandle;

	public int HoveredLineIndex { get; private set; }

	public bool IsDraggingSelection { get; private set; }

	public int SelectedLineMin { get; private set; }

	public int SelectedLineMax { get; private set; }

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	private void Update()
	{
	}

	private void CheckForInput()
	{
	}

	private bool TryResolveCursorManager(string context)
	{
		return false;
	}

	private bool TryResolveNotepadSection(string context)
	{
		return false;
	}

	private bool EnsureReady()
	{
		return false;
	}

	private void SubscribeIfPossible()
	{
	}

	private void Unsubscribe()
	{
	}

	private void HandleCursorTargetChanged(Interactable hover)
	{
	}

	private void HandlePrimaryClickDown(Interactable pressedHover)
	{
	}

	private void HandlePrimaryClickUp(Interactable releasedHover)
	{
	}

	private void TryFindBroker()
	{
	}

	private void TryAcquireBrokerDragLockIfNeeded()
	{
	}

	private void ReleaseBrokerDragLockIfHeld()
	{
	}

	private void UpdateHoveredLineFromCursorManager()
	{
	}

	private static Vector2 GetCursorScreenPositionFromManager(DynamicCursorManager mgr)
	{
		return default(Vector2);
	}

	private void ExpandSelectionToHovered()
	{
	}

	private string BuildSelectedTextFromLayoutLineRange(int minLayoutLine, int maxLayoutLine)
	{
		return null;
	}

	private string BuildSelectedText_LegacyLayoutLinesJoined(int minLine, int maxLine)
	{
		return null;
	}

	private static void AppendLineToBuilder(TMP_TextInfo ti, int lineIndex, StringBuilder sb)
	{
	}

	private void UpdateHighlightsForHoverOnly()
	{
	}

	private void UpdateHighlightsForSelectionRange()
	{
	}

	private void EnsureHighlightsCount(int needed)
	{
	}

	private void ClearSelectionAndHighlights()
	{
	}

	private Transform CreateHighlightInstance()
	{
		return null;
	}

	private Transform CreateAutoQuad(string quadName, Transform parent)
	{
		return null;
	}

	private static void RemoveAllCollidersRecursive(Transform root)
	{
	}

	private void CacheOutlinePartsIfNeeded(Transform root)
	{
	}

	private void UpdateHighlightForLine(Transform h, int lineIndex)
	{
	}

	private void ApplyHighlightTint(Transform h)
	{
	}

	private static void SetHighlightActive(Transform h, bool active)
	{
	}
}
