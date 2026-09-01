using Shapes;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class MapMarkerHitTarget : MonoBehaviour
{
	[Header("Disc / Ring Hit Testing (optional, Shapes)")]
	[Tooltip("Optional: Shapes.Disc used as the disc/ring visual.\nIf assigned, this component can perform ring-aware hit testing in map-local space.\nRecommended for hollow ring markers so the transparent center does NOT count as a hit.\nLeave unassigned for line-only markers.")]
	[SerializeField]
	private Disc disc;

	[Tooltip("If true (recommended for hollow disc markers), hit testing is performed only on a ring band around the disc radius.\nIf false, hit testing is treated like a filled disc (any point within radius is a hit).\nNote: Your use case states discs are always hollow, so the safe default is true.")]
	[SerializeField]
	private bool ringOnly;

	[Tooltip("Width of the clickable/hoverable ring band in MAP LOCAL UNITS (same units as drag distance).\nOnly used when 'Ring Only' is enabled.\nA point is considered a hit if its distance from the marker origin is within:\n  [radius - ringHitWidth/2, radius + ringHitWidth/2]\nThen expanded by 'Hit Padding (Local Units)'.\n\nWhy this is separate from Disc.Thickness:\nShapes thickness may be in a different space (meters/pixels/etc.), while pointer/map math is in map-local units.\nThis value is an interaction tuning knob.\n\nSafe examples:\n - 0.08  (fairly precise)\n - 0.12  (easier to click)")]
	[Min(0f)]
	[SerializeField]
	private float ringHitWidthLocalUnits;

	[Tooltip("Extra padding (in MAP LOCAL UNITS) applied to disc/ring hit testing.\nPositive values make it easier to click; negative values make it stricter.\nSafe examples:\n - 0.00  (no padding)\n - 0.02  (slightly easier to click)")]
	[SerializeField]
	private float ringHitPaddingLocalUnits;

	[Header("Line Hit Testing (optional, map-local band)")]
	[Tooltip("If true, enables a map-local 'fat line' hit test.\nUse this for line/ruler markers where you want forgiving clicks near the line.\nThe line direction and length are taken from the marker's current measurement:\n  - originLocal (marker center)\n  - currentLocal (origin + direction * DistanceValue)\nThis does NOT require UI raycasters.\n\nRecommended:\n - Enable for line markers\n - Disable for disc-only markers")]
	[SerializeField]
	private bool enableLineBandHitTest;

	[Tooltip("Half-width of the clickable/hoverable band around the line segment, in MAP LOCAL UNITS.\nThis is your 'buffer space' for forgiving line clicks.\nA point is considered a hit if its shortest distance to the line segment is <= this value.\n\nSafe examples:\n - 0.03 (tight)\n - 0.06 (forgiving)\n - 0.10 (very forgiving)")]
	[Min(0f)]
	[SerializeField]
	private float lineBandHalfWidthLocalUnits;

	[Tooltip("Extra padding added to the line hit test half-width, in MAP LOCAL UNITS.\nUse this if you want to tweak clickability without changing the core half width.\nSafe example: 0.01")]
	[SerializeField]
	private float lineBandPaddingLocalUnits;

	[Tooltip("If true, the line band hit test is only active when the marker has a meaningful distance.\nSpecifically: DistanceValue >= minimumDistanceForLineHit.\nThis prevents a 'zero-length line' from being clickable as a tiny dot.\nSafe default: true.")]
	[SerializeField]
	private bool requireMinimumDistanceForLineHit;

	[Tooltip("Minimum DistanceValue (map local units) required for the line band hit test to be active.\nOnly used when 'Require Minimum Distance For Line Hit' is true.\nSafe example: 0.02")]
	[Min(0f)]
	[SerializeField]
	private float minimumDistanceForLineHit;

	[Header("Extra UI Hit Areas (optional, screen-space rectangles)")]
	[Tooltip("Optional list of RectTransforms that should count as clickable/hoverable.\nUse this for labels (TextMeshProUGUI), icons, or other UI parts of the marker.\nThese are tested using RectangleContainsScreenPoint with the canvas camera provided by MapMarkerPlacer.\n\nImportant:\n - Do NOT include huge/stretch containers here.\n - Include only the specific UI elements you want to be clickable.\n\nSafe examples:\n - Angle label RectTransform\n - Distance label RectTransform\n - Small icon RectTransform")]
	[SerializeField]
	private RectTransform[] extraRectHitAreas;

	[Header("Hover Events (optional)")]
	[Tooltip("Invoked once when the pointer becomes 'over' this marker (hover enter).\nHover is evaluated by MapMarkerPlacer using the same hit testing rules as deletion.\nUse this to enable highlight visuals, play sounds, or show contextual UI.\nNo inputs are read here; this is event-only.")]
	public UnityEvent onHoverEnter;

	[Tooltip("Invoked once when the pointer stops being 'over' this marker (hover exit).\nUse this to disable highlight visuals or revert UI.\nNo inputs are read here; this is event-only.")]
	public UnityEvent onHoverExit;

	public bool IsHovered { get; private set; }

	public bool HitTest(Vector2 pointerScreen, Camera canvasCamera, Vector2 pointerLocalOnMap, Vector2 markerOriginLocalOnMap, float markerDistanceLocalUnits, float markerAngleDegrees)
	{
		return false;
	}

	public void SetHovered(bool hovered)
	{
	}

	private static float DistancePointToSegment(Vector2 p, Vector2 a, Vector2 b)
	{
		return 0f;
	}
}
