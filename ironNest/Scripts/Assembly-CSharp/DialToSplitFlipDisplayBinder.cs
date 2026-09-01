using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Mechanical Displays/Dial -> SplitFlip Binder")]
public sealed class DialToSplitFlipDisplayBinder : MonoBehaviour
{
	public enum IndexRoundingMode
	{
		Floor = 0,
		Round = 1,
		Ceil = 2
	}

	[Header("References")]
	[SerializeField]
	[Tooltip("DialInteractable that drives the selection.\n\nRequirements:\n- Must be configured in Limited mode for this binder to produce stable mapped indices.\n- This binder does NOT modify the dial; it only listens to OnValueChanged.\n\nSafe examples:\n- A selector knob dial (Limited mode) with Min/Max output values\n- A detented dial (Limited mode) that snaps values in fixed steps")]
	private DialInteractable dial;

	[SerializeField]
	[Tooltip("SplitFlipDisplay that will display/animate to the selected symbol.\n\nHow it is used:\n- Binder will call SplitFlipDisplay.SetDesiredValueAndApply(symbolString).\n- SplitFlipDisplay is responsible for step-by-step flipping until it reaches the desired symbol.\n\nFormat rules:\n- Only the FIRST character of the desired string is used by SplitFlipDisplay stepping logic.\n- This binder always sends a 1-character string.")]
	private SplitFlipDisplay splitFlipDisplay;

	[Header("Symbol Set + Mapping")]
	[SerializeField]
	[Tooltip("Ordered set of symbols that the dial selects from.\n\nSupported tokens/codes:\n- Plain sequence of characters ONLY (each character is one selectable symbol).\n\nFormat rules:\n- Index 0 corresponds to Output Range Min.\n- Index (Length-1) corresponds to Output Range Max.\n- SHOULD match SplitFlipDisplay's Ordered Symbols for consistent stepping behavior.\n- If empty or length < 1, updates are ignored.\n- Recommended: include a leading space ' ' if you want a real \"blank\" flap state.\n\nSafe examples:\n- \" ABCDEFGHIJKLMNOPQRSTUVWXYZ\" (leading space)\n- \" 0123456789\" (leading space)\n- \" ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-:\"")]
	private string orderedSymbols;

	[SerializeField]
	[Tooltip("Minimum dial OUTPUT value that maps to orderedSymbols[0].\n\nNotes:\n- This should match the DialInteractable Limited mode 'Min Output Value'.\n- The dial may also apply health-constrained effective ranges; if so, this binder cannot read those internal values.\n- In that case, consider driving this binder with the same effective range externally, or disable health constraint for selector dials.\n\nFormat rules:\n- Must be < Output Range Max for meaningful mapping.\n\nSafe examples:\n- 0 for 0..26 mapping to [space]..Z\n- -1 for -1..+1 mapping to a 3-symbol set")]
	private float outputRangeMin;

	[SerializeField]
	[Tooltip("Maximum dial OUTPUT value that maps to orderedSymbols[orderedSymbols.Length - 1].\n\nNotes:\n- This should match the DialInteractable Limited mode 'Max Output Value'.\n\nFormat rules:\n- Must be > Output Range Min for meaningful mapping.\n\nSafe examples:\n- 26 for 0..26 mapping to [space]..Z\n- 9 for 0..9 mapping to digits")]
	private float outputRangeMax;

	[SerializeField]
	[Tooltip("Rounding mode used when converting the dial's float output to a symbol index.\n\nSupported tokens/codes:\n- Floor: always round down (stable when sweeping upward).\n- Round: nearest integer (most natural if dial uses detents).\n- Ceil: always round up.\n\nFormat rules:\n- After rounding, index is clamped to [0..Length-1].\n\nSafe examples:\n- Round for detented selector knobs\n- Floor for 'bucketed' continuous selection")]
	private IndexRoundingMode indexRounding;

	[SerializeField]
	[Tooltip("If true, applies an optional response curve to the normalized dial value before mapping to an index.\n\nWhat it does:\n- Normalized = InverseLerp(OutputRangeMin, OutputRangeMax, dialValue)\n- CurvedNormalized = curve.Evaluate(Normalized)\n- Index = CurvedNormalized mapped to [0..Length-1]\n\nFormat rules:\n- Curve X and Y are expected to be in 0..1.\n- If disabled, mapping is linear.\n\nSafe examples:\n- false for typical selector\n- true with an ease curve for non-linear feel")]
	private bool useResponseCurve;

	[SerializeField]
	[Tooltip("Optional response curve used when 'Use Response Curve' is enabled.\n\nFormat rules:\n- X: normalized dial output (0..1)\n- Y: remapped normalized selection (0..1)\n- If null or empty, linear behavior is used.\n\nSafe examples:\n- Linear (0,0)->(1,1)\n- Ease in/out curve for smoother ends")]
	private AnimationCurve responseCurve;

	[Header("Update Behavior")]
	[SerializeField]
	[Tooltip("If true, pushes the mapped symbol to the SplitFlipDisplay immediately on enable using the dial's current AccumulatedValue.\n\nNotes:\n- Useful when UI/prefab is enabled mid-game and must sync instantly.\n- This does NOT snap the SplitFlipDisplay; it just sets desired value and lets it animate.\n\nSafe examples:\n- true for panels that can appear/disappear\n- false if you only want updates after the first dial change")]
	private bool applyOnEnable;

	[SerializeField]
	[Tooltip("Minimum real-time seconds between updates sent to SplitFlipDisplay.\n\nWhat it does:\n- Prevents spamming desired updates at extremely high rates if the dial fires many events per frame.\n- SplitFlipDisplay can still animate continuously toward the latest desired value.\n\nFormat rules:\n- 0 = no throttling.\n- Uses unscaled time.\n\nSafe examples:\n- 0.00 for small projects / few dials\n- 0.02 (~50 Hz) for lots of dials\n- 0.05 (~20 Hz) for very busy scenes")]
	private float minUpdateIntervalSeconds;

	[SerializeField]
	[Tooltip("If true, updates are only sent when the computed symbol index changes.\n\nWhat it does:\n- Prevents re-sending the same desired symbol repeatedly (helps reduce garbage and unnecessary work).\n\nSafe examples:\n- true (recommended)\n- false if you want to reassert desired value every time (rare)")]
	private bool onlySendOnIndexChange;

	[Header("Debug / Events")]
	[SerializeField]
	[Tooltip("Optional UnityEvent fired when the binder selects a new symbol index.\n\nParameters:\n- int: selected index into Ordered Symbols\n\nSafe examples:\n- Drive a click sound per detent\n- Update a debug label")]
	private UnityEvent<int> onSelectedIndexChanged;

	[SerializeField]
	[Tooltip("Optional UnityEvent fired when the binder selects a new symbol.\n\nParameters:\n- string: 1-character string for the selected symbol\n\nSafe examples:\n- Debug logging\n- Secondary UI mirroring")]
	private UnityEvent<string> onSelectedSymbolChanged;

	private int _lastIndex;

	private float _lastSentTimeUnscaled;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void HandleDialValueChanged(float dialOutputValue)
	{
	}

	private int MapDialValueToSymbolIndex(float dialOutputValue)
	{
		return 0;
	}
}
