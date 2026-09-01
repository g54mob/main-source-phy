using UnityEngine;

[CreateAssetMenu(fileName = "NewMutator", menuName = "Missions/Mutator Definition", order = 0)]
public class MutatorDefinition : ScriptableObject
{
	[Header("Identity")]
	[Tooltip("Human-readable mutator name shown in UI/tools. Keep it short and unique per project.\nExamples: 'Exact Distance Readout', 'Wide Direction Error', 'No HUD'.")]
	public string displayName;

	[Tooltip("Optional description for UI/tooling. Keep concise.\nExamples: 'Displays exact miss distance instead of ranges.', 'Doubles pointer error angle.'")]
	[TextArea(2, 4)]
	public string description;
}
