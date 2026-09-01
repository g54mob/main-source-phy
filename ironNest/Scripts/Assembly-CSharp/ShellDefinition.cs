using SleepyNodes;
using UnityEngine;

[CreateAssetMenu(fileName = "ShellDefinition", menuName = "FireMission/Shell Definition", order = 10)]
public class ShellDefinition : ScriptableObject
{
	[Header("Identity")]
	public string ShellId;

	public string DisplayName;

	public string Description;

	[Header("Prefabs")]
	public ShellBlueprint BlueprintPrefab;

	public ImpactLocation ImpactEffectPrefab;

	[Header("Shell Stats")]
	public float ShellSpeed;

	public float shellSpeedVariationPercent;

	public int Damage;

	public float ImpactRadius;

	public int projectilesPerShell;

	public float horizontalDispersion;

	public float verticalDispersion;

	public bool IgnoreInTrackingShotsFired;

	[Header("Impact")]
	public ImpactGraph Graph;

	[Header("Powder Charge System")]
	[Range(1f, 6f)]
	public int maxPowderCharges;

	[Range(1f, 6f)]
	public int defaultPowderCharge;

	public PowderChargeRangeMapping[] chargeRangeMappings;

	public AnimationCurve chargeToSpeedMultiplier;

	public AnimationCurve chargeToHorizontalDispersionMultiplier;

	public AnimationCurve chargeToVerticalDispersionMultiplier;
}
