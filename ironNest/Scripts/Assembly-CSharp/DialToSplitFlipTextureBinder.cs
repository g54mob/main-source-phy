using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Mechanical Displays/Dial -> SplitFlipTexture Binder")]
public sealed class DialToSplitFlipTextureBinder : MonoBehaviour
{
	public enum DialMode
	{
		Limited = 0,
		Unlimited = 1
	}

	public enum IndexRoundingMode
	{
		Floor = 0,
		Round = 1,
		Ceil = 2
	}

	[Header("References")]
	[SerializeField]
	[Tooltip("DialInteractable that drives the selection.\n\nRequirements:\n- Can be Limited mode (fixed range selector) or Unlimited mode (continuous looping).\n- This binder does NOT modify the dial; it only listens to OnValueChanged.\n\nSafe examples:\n- A selector knob dial (Limited mode) with Min/Max output values\n- A detented dial (Limited mode) that snaps values in fixed steps\n- A continuous rotation dial (Unlimited mode) for looping selection")]
	private DialInteractable dial;

	[SerializeField]
	[Tooltip("SplitFlipTextureDisplay that will display/animate to the selected texture.\n\nHow it is used:\n- Binder will call SplitFlipTextureDisplay.SetDesiredIndexAndApply(index).\n- SplitFlipTextureDisplay is responsible for step-by-step flipping until it reaches the desired index.\n- Texture count and valid index range are read automatically from this component.\n\nFormat rules:\n- Must have at least 1 texture in orderedTextures.\n- Indices are automatically clamped to [0..TextureCount-1].")]
	private SplitFlipTextureDisplay splitFlipTextureDisplay;

	[Header("Dial Mode")]
	[SerializeField]
	[Tooltip("How the dial's output value is interpreted.\n\nSupported modes:\n- Limited: Dial has fixed min/max range that maps to texture indices (typical selector knob).\n- Unlimited: Dial can rotate continuously; value is wrapped/modulo into texture indices (infinite rotation selector).\n\nFormat rules:\n- Limited mode uses Output Range Min/Max.\n- Unlimited mode uses Steps Per Texture and ignores range min/max.\n\nSafe examples:\n- Limited for a 0-26 dial selecting A-Z\n- Unlimited for a continuously rotating selector wheel")]
	private DialMode dialMode;

	[Header("Limited Mode Settings")]
	[SerializeField]
	[Tooltip("Minimum dial OUTPUT value that maps to texture index 0.\n\nNotes:\n- Only used when Dial Mode is Limited.\n- This should match the DialInteractable Limited mode 'Min Output Value'.\n- The dial may also apply health-constrained effective ranges; if so, this binder cannot read those internal values.\n- In that case, consider driving this binder with the same effective range externally, or disable health constraint for selector dials.\n\nFormat rules:\n- Must be < Output Range Max for meaningful mapping.\n\nSafe examples:\n- 0 for 0..26 mapping to indices 0-26\n- -1 for -1..+1 mapping to a 3-texture set")]
	private float outputRangeMin;

	[SerializeField]
	[Tooltip("Maximum dial OUTPUT value that maps to texture index (textureCount - 1).\n\nNotes:\n- Only used when Dial Mode is Limited.\n- This should match the DialInteractable Limited mode 'Max Output Value'.\n\nFormat rules:\n- Must be > Output Range Min for meaningful mapping.\n\nSafe examples:\n- 26 for 0..26 mapping to indices 0-26\n- 9 for 0..9 mapping to 10 textures")]
	private float outputRangeMax;

	[Header("Unlimited Mode Settings")]
	[SerializeField]
	[Tooltip("How many dial output value units equal one texture step in Unlimited mode.\n\nWhat it does:\n- Only used when Dial Mode is Unlimited.\n- Index = Floor(dialValue / stepsPerTexture) % textureCount\n- Larger values = more rotation needed per texture step.\n\nFormat rules:\n- Must be > 0.\n- Common values: 1.0 (one output unit per texture), 10.0, 360.0 (degrees per texture).\n\nSafe examples:\n- 1.0 for simple integer stepping\n- 13.846 for 27 textures in 360 degrees (360/27)\n- 36.0 for 10 textures in 360 degrees")]
	private float stepsPerTexture;

	[SerializeField]
	[Tooltip("Starting offset for Unlimited mode indexing.\n\nWhat it does:\n- Only used when Dial Mode is Unlimited.\n- Index = Floor((dialValue - unlimitedIndexOffset) / stepsPerTexture) % textureCount\n- Allows you to adjust which texture appears at dialValue = 0.\n\nFormat rules:\n- Any float value.\n\nSafe examples:\n- 0 for no offset\n- -6.923 to shift by half a texture in a 360/27 setup")]
	private float unlimitedIndexOffset;

	[Header("Index Mapping")]
	[SerializeField]
	[Tooltip("Rounding mode used when converting the dial's float output to a texture index.\n\nSupported tokens/codes:\n- Floor: always round down (stable when sweeping upward).\n- Round: nearest integer (most natural if dial uses detents).\n- Ceil: always round up.\n\nFormat rules:\n- After rounding, index is clamped/wrapped to [0..textureCount-1].\n\nSafe examples:\n- Round for detented selector knobs\n- Floor for 'bucketed' continuous selection")]
	private IndexRoundingMode indexRounding;

	[SerializeField]
	[Tooltip("If true, applies an optional response curve to the normalized dial value before mapping to an index.\n\nWhat it does:\n- Only used in Limited mode.\n- Normalized = InverseLerp(OutputRangeMin, OutputRangeMax, dialValue)\n- CurvedNormalized = curve.Evaluate(Normalized)\n- Index = CurvedNormalized mapped to [0..textureCount-1]\n\nFormat rules:\n- Curve X and Y are expected to be in 0..1.\n- If disabled, mapping is linear.\n\nSafe examples:\n- false for typical selector\n- true with an ease curve for non-linear feel")]
	private bool useResponseCurve;

	[SerializeField]
	[Tooltip("Optional response curve used when 'Use Response Curve' is enabled.\n\nFormat rules:\n- X: normalized dial output (0..1)\n- Y: remapped normalized selection (0..1)\n- If null or empty, linear behavior is used.\n- Only used in Limited mode.\n\nSafe examples:\n- Linear (0,0)->(1,1)\n- Ease in/out curve for smoother ends")]
	private AnimationCurve responseCurve;

	[Header("Update Behavior")]
	[SerializeField]
	[Tooltip("If true, pushes the mapped index to the SplitFlipTextureDisplay immediately on enable using the dial's current AccumulatedValue.\n\nNotes:\n- Useful when UI/prefab is enabled mid-game and must sync instantly.\n- This does NOT snap the SplitFlipTextureDisplay; it just sets desired index and lets it animate.\n\nSafe examples:\n- true for panels that can appear/disappear\n- false if you only want updates after the first dial change")]
	private bool applyOnEnable;

	[SerializeField]
	[Tooltip("Minimum real-time seconds between updates sent to SplitFlipTextureDisplay.\n\nWhat it does:\n- Prevents spamming desired updates at extremely high rates if the dial fires many events per frame.\n- SplitFlipTextureDisplay can still animate continuously toward the latest desired index.\n\nFormat rules:\n- 0 = no throttling.\n- Uses unscaled time.\n\nSafe examples:\n- 0.00 for small projects / few dials\n- 0.02 (~50 Hz) for lots of dials\n- 0.05 (~20 Hz) for very busy scenes")]
	private float minUpdateIntervalSeconds;

	[SerializeField]
	[Tooltip("If true, updates are only sent when the computed texture index changes.\n\nWhat it does:\n- Prevents re-sending the same desired index repeatedly (helps reduce garbage and unnecessary work).\n\nSafe examples:\n- true (recommended)\n- false if you want to reassert desired index every time (rare)")]
	private bool onlySendOnIndexChange;

	[Header("Debug / Events")]
	[SerializeField]
	[Tooltip("Optional UnityEvent fired when the binder selects a new texture index.\n\nParameters:\n- int: selected texture index\n\nSafe examples:\n- Drive a click sound per detent\n- Update a debug label")]
	private UnityEvent<int> onSelectedIndexChanged;

	private int _lastIndex;

	private float _lastSentTimeUnscaled;

	public int CurrentIndex => 0;

	public int TextureCount => 0;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void HandleDialValueChanged(float dialOutputValue)
	{
	}

	private int MapDialValueToTextureIndex(float dialOutputValue, int textureCount)
	{
		return 0;
	}

	private int MapLimitedMode(float dialOutputValue, int textureCount)
	{
		return 0;
	}

	private int MapUnlimitedMode(float dialOutputValue, int textureCount)
	{
		return 0;
	}

	private int ApplyRounding(float rawIndex)
	{
		return 0;
	}

	public void SetDesiredIndex(int index)
	{
	}

	public void ForceRefresh()
	{
	}
}
