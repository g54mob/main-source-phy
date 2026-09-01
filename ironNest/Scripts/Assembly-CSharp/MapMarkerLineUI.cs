using System;
using System.Collections.Generic;
using Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MapMarkerLineUI : MonoBehaviour, IFloatValueProvider
{
	[Serializable]
	public class DistanceAngleEvent : UnityEvent<float, float>
	{
	}

	[Header("Line Visuals (optional, Shapes)")]
	public Line line;

	[SerializeField]
	private List<Line> additionalLines;

	[Header("Disc Visuals (optional, Shapes)")]
	public Disc disc;

	[SerializeField]
	private List<Disc> additionalDiscs;

	[SerializeField]
	private float discRadiusMultiplier;

	[SerializeField]
	private float discRadiusMin;

	[SerializeField]
	private float discRadiusMax;

	[SerializeField]
	private bool driveDiscThicknessFromRadius;

	[Range(0f, 1f)]
	[SerializeField]
	private float discThicknessFraction;

	[Header("Pointer / Tip (optional)")]
	public Transform pointerTip;

	[SerializeField]
	private bool rotatePointerTip;

	[SerializeField]
	private float pointerRotationOffsetDegrees;

	[SerializeField]
	private bool hidePointerTipUntilDragged;

	[Header("Rotate With Drag Direction (optional)")]
	[Tooltip("Optional: Any transform you want to rotate to face the drag direction.\nRotation is applied as a Z-axis rotation in this marker's local space.\nConvention: local UP (Vector3.up) will point along the drag direction.")]
	public Transform faceDragDirectionTarget;

	[Tooltip("If true, faceDragDirectionTarget will rotate when dragging.\nIf false, it will not be rotated.")]
	[SerializeField]
	private bool rotateFaceDragDirectionTarget;

	[Tooltip("Rotation offset applied after aligning local UP to the drag direction.\nUse this if your art points a different way by default.")]
	[SerializeField]
	private float faceDragDirectionRotationOffsetDegrees;

	[Header("Angle Label (TextMeshPro - UI or 3D)")]
	public TMP_Text angleLabel;

	[Header("Distance Label (TextMeshPro - UI or 3D)")]
	public TMP_Text distanceLabel;

	[Header("Display / Threshold")]
	public bool hideLabelsUntilDragged;

	public float minimumDragDistance;

	[Header("Placement Tooltip (optional)")]
	public GameObject placementTooltip;

	public bool hidePlacementTooltipOnDrag;

	[Header("Notepad / Logging")]
	[SerializeField]
	private bool allowNoteLogging;

	[Header("Marker Speed (Audio / FMOD)")]
	[Tooltip("The tip movement speed (in local map units per second) that maps to a NormalizedMarkerSpeed of 1.0.\n\nHow to calibrate:\n  - Play the game, drag the marker at the fastest natural pace you expect.\n  - Read 'Inspector: Raw Speed (units/s)' and set this to roughly that value.\n\nEffect:\n  - Speeds at or above this value → NormalizedMarkerSpeed = 1.0\n  - Zero movement → NormalizedMarkerSpeed = 0.0\n\nSafe default: 200 — adjust to your map's local unit scale.\nThis value is in the same coordinate space as DistanceValue (local RectTransform units).")]
	[SerializeField]
	private float speedNormalizationRange;

	[Tooltip("Time source used when computing tip speed.\n\n  True  → Time.unscaledDeltaTime (speed is unaffected by Time.timeScale; recommended if you pause with timeScale=0).\n  False → Time.deltaTime (speed scales with game speed; suitable if you never pause or slow-mo).")]
	[SerializeField]
	private bool useUnscaledTime;

	[Tooltip("If true, NormalizedMarkerSpeed is forced to 0 as soon as dragging stops (mouse released / FinalizePlacement called).\n\n  True  → Raw speed snaps to 0 immediately on release; FMODParameterSetter's own smoothing will fade it out.\n  False → Speed naturally decays to 0 over the next frame(s) as deltas shrink.\n\nRecommended: True — lets FMODParameterSetter control all fade behaviour cleanly.")]
	[SerializeField]
	private bool resetSpeedOnFinalize;

	[Header("Inspector: Speed Diagnostics (Read-Only)")]
	[Tooltip("Live tip movement speed in local map units per second.\nUse this to calibrate 'Speed Normalization Range'.\nRead-only — driven at runtime.")]
	[SerializeField]
	private float inspectorRawSpeedUnitsPerSecond;

	[Tooltip("NormalizedMarkerSpeed clamped to [0..1].\nThis is the value FMODParameterSetter reads via IFloatValueProvider.\nRead-only — driven at runtime.")]
	[SerializeField]
	private float inspectorNormalizedSpeed;

	[Header("Events")]
	public DistanceAngleEvent onDragProgress;

	public DistanceAngleEvent onMinimumDragDistanceReached;

	public DistanceAngleEvent onPlacementFinalized;

	private RectTransform markerRectTransform;

	private bool placementTooltipFinalized;

	private bool placementEventFired;

	private bool minimumDistanceEventFired;

	private Vector3 previousTipLocalPosition;

	private bool hasPreviousTipPosition;

	private bool isDragging;

	public bool AllowNoteLogging => false;

	public float AngleValue { get; private set; }

	public float DistanceValue { get; private set; }

	public Vector2 OriginLocal { get; private set; }

	public Vector3 TipLocalPosition { get; private set; }

	public string AngleLabelText => null;

	public string DistanceLabelText => null;

	public bool HasReachedMinimumDragDistance { get; private set; }

	public float NormalizedMarkerSpeed { get; private set; }

	public float GetFloatValue()
	{
		return 0f;
	}

	private void Awake()
	{
	}

	public void Initialize(Vector2 originLocal, RectTransform mapRect)
	{
	}

	public void UpdateLine(Vector2 originLocal, Vector2 targetLocal, RectTransform mapRect)
	{
	}

	public void FinalizePlacement()
	{
	}

	private void MeasureAndUpdateSpeed(Vector3 currentTipLocal)
	{
	}

	private void SetNormalizedSpeed(float normalized, float rawUnitsPerSec = 0f)
	{
	}

	private void DriveLines(Vector3 endLocal)
	{
	}

	private void ApplyDrivenLineEndpoints(Line l, Vector3 start, Vector3 end)
	{
	}

	private void DrivePointerTip(Vector2 directionLocalOnMap)
	{
	}

	private void DriveFaceDragDirectionTarget(Vector2 directionLocalOnMap)
	{
	}

	private void DriveDiscs(float lengthLocalOnMap)
	{
	}

	private void ApplyDrivenDiscValues(Disc d, float drivenRadius)
	{
	}

	private void SetLabelsVisible(bool visible)
	{
	}
}
