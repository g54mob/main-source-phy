using Localisation;
using SleepyNodes;
using UnityEngine;

[CreateAssetMenu(fileName = "Punchcard_", menuName = "Punchcard")]
public class PunchcardDefinitionV2 : ScriptableObject
{
	[Header("Identity")]
	public string ID;

	public int Cost;

	public int MaxUses;

	[Header("Details")]
	public TextIdentifier Title;

	public TextIdentifier Description;

	public Sprite Icon;

	[Header("Prefabs")]
	public PunchcardRuntime Prefab_RuntimeOverride;

	public GameObject Prefab_ConsoleControls;

	[Header("Action")]
	public PunchcardGraph Graph;

	public RequirementSet Requirements;

	[Header("Stats Tracking")]
	public bool IsRecon;

	[Tooltip("If true, this card is immediately ejected when placed in the RequisitionSlot and cannot be redeemed.")]
	public bool AutoEject;

	[Header("Runtime")]
	[ReadOnly]
	public int RemainingUses;
}
