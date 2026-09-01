using Localisation;
using SleepyNodes;
using UnityEngine;

public class PunchcardDefinitionV2 : ScriptableObject
{
	public string ID;

	public int Cost;

	public int MaxUses;

	public TextIdentifier Title;

	public TextIdentifier Description;

	public Sprite Icon;

	public PunchcardRuntime Prefab_RuntimeOverride;

	public GameObject Prefab_ConsoleControls;

	public PunchcardGraph Graph;

	public RequirementSet Requirements;

	public bool IsRecon;

	public bool AutoEject;

	public int RemainingUses;
}
