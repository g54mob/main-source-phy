using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutOfBoundsEffectReceiver : MonoBehaviour
{
	[Header("Rotation (Single Legacy Reference)")]
	[Tooltip("(Optional legacy) Single rotation target. If rotationTargets list is empty, this will be added automatically at Initialize time.")]
	public Transform rotationTarget;

	[Header("Rotation Targets (Multiple)")]
	[Tooltip("All transforms here will have their local Z angle set to the shell's exit angle.")]
	public List<Transform> rotationTargets;

	[Header("Remaining Distance (Single Legacy Reference)")]
	[Tooltip("(Optional legacy) Single TMP_Text. If remainingDistanceTexts list is empty, this will be added automatically at Initialize time.")]
	public TMP_Text remainingDistanceText;

	[Header("Remaining Distance Text Targets (Multiple)")]
	[Tooltip("All TMP text elements here will be populated with the remaining distance string.")]
	public List<TMP_Text> remainingDistanceTexts;

	[Header("Formatting")]
	[Tooltip("String.Format pattern: {0} = remaining distance value.")]
	public string distanceFormat;

	[Tooltip("Units suffix appended after formatting (e.g., 'm', 'units'). Leave blank for none.")]
	public string unitsSuffix;

	[Header("Per-Border Object Groups")]
	public List<GameObject> topGroup;

	public List<GameObject> bottomGroup;

	public List<GameObject> leftGroup;

	public List<GameObject> rightGroup;

	[Header("Behavior")]
	[Tooltip("If true, non-matching groups are Destroy()ed. If false, they are just SetActive(false).")]
	public bool destroyUnusedGroups;

	private bool initialized;

	public void Initialize(float shellAngleDeg, float remainingDistance, MapBorderSide borderSide)
	{
	}

	private void EnsureLegacyFallbacks()
	{
	}

	private void ApplyRotation(float angleDeg)
	{
	}

	private void ApplyRemainingDistance(float remainingDistance)
	{
	}

	private void ActivateGroupForBorder(MapBorderSide side)
	{
	}
}
