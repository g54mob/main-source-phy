using System;
using System.Collections.Generic;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(RectTransform))]
public class ImpactIndicator : MonoBehaviour
{
	[Serializable]
	public class LocalSpaceEventDataUnityEvent : UnityEvent<EventData_Impact>
	{
	}

	public enum HitTestMode
	{
		CenterPointInside = 0,
		CircleOverlapsArea = 1
	}

	[Header("Region")]
	[Tooltip("RectTransform that defines the region to test against. If left empty, this component's RectTransform is used.")]
	public RectTransform regionRect;

	[Tooltip("If true, attempts to resolve and cache the root canvas RectTransform once for consistent coordinate space with Impact/Target/Ally/Enemy systems.")]
	public bool cacheRootCanvas;

	[Header("Hit Test")]
	[Tooltip("Determines how a hit is detected relative to the region.\n- CenterPointInside: Triggers if the impact CENTER falls within the region.\n- CircleOverlapsArea: Triggers if the impact CIRCLE (center + radius) overlaps the region area.\nExamples:\n- CenterPointInside is stricter; large-radius impacts at the edge may not trigger unless the center is inside.\n- CircleOverlapsArea is more permissive and accounts for blast radius.")]
	public HitTestMode hitTestMode;

	[Tooltip("Extra padding (in region's local units) added around the region's rect when evaluating hits.\nUse positive values to make the region easier to hit, negative to shrink it slightly.\nSafe examples: 0 (no change), 5 (slight expansion).")]
	public float regionPadding;

	[Header("Filters (Optional)")]
	[Tooltip("If true, will only trigger when the impact's shellId is in 'allowedShellIds'. If false or list is empty, all shells pass.")]
	public bool filterByShellId;

	[Tooltip("List of allowed shell IDs (exact match, case-sensitive). Leave empty to allow all shells.\nExample IDs (safe examples): \"shell_he\", \"shell_ap\"")]
	public List<string> allowedShellIds;

	[Tooltip("Require that the impact reported ANY Success or Failure on Targets (per LocalSpaceEventLogger's legacy 'hitAnyTarget' definition). If false, this condition is ignored.")]
	public bool requireAnyTargetHit;

	[Tooltip("Require that the impact reported ANY Success or Failure on Allies. If false, this condition is ignored.")]
	public bool requireAnyAllyHit;

	[Tooltip("Require that the impact reported ANY Success or Failure on Optional Targets. If false, this condition is ignored.")]
	public bool requireAnyOptionalHit;

	[Tooltip("Require that the impact reported ANY Success or Failure on Enemies. If false, this condition is ignored.")]
	public bool requireAnyEnemyHit;

	[Header("Flow Control")]
	[Tooltip("Minimum time (in seconds) between successive event invocations from this component.\nSet to 0 to allow every qualifying impact to trigger immediately.\nSafe examples: 0, 0.1, 0.25")]
	public float minSecondsBetweenInvokes;

	[Header("Events")]
	[Tooltip("Invoked when an Impact event intersects this region based on the selected hit test mode and filters. Provides the full LocalSpaceEventData payload.")]
	public LocalSpaceEventDataUnityEvent onImpactWithinRegion;

	[Header("Debug")]
	[Tooltip("Enable verbose logs for root canvas resolution, coordinate transformations, and hit test results.")]
	public bool debugLogs;

	private RectTransform _cachedRootCanvasRect;

	private RectTransform _region;

	private float _lastInvokeTime;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private RectTransform ResolveRootCanvasRect(bool force = false)
	{
		return null;
	}

	private void HandleLocalSpaceEvent(EventData_Impact data)
	{
	}
}
