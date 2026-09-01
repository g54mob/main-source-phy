using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[AddComponentMenu("Missions/Mutators/Mutator Relay")]
public class MutatorRelay : MonoBehaviour
{
	public enum Condition
	{
		AnyOfActive = 0,
		AllOfActive = 1,
		NoneActive = 2
	}

	[Header("Match Condition")]
	[Tooltip("Which condition to test against the active mutator set:\n- AnyOfActive: True if ANY required mutator is active.\n- AllOfActive: True if ALL required mutators are active.\n- NoneActive: True if NONE of the required mutators are active.\nTip: Use 'AnyOfActive' to show a variant for any single mutator; use 'AllOfActive' for combined variants.")]
	public Condition condition;

	[Tooltip("Mutator assets this relay checks against the global active set.\nDrag one or more MutatorDefinition assets here.\nExamples: [Exact Distance Readout], [Wide Direction Error].")]
	public List<MutatorDefinition> requiredMutators;

	[Header("Action")]
	[Tooltip("If true, targets are SET ACTIVE when the condition is TRUE, and SET INACTIVE when FALSE.\nIf false, targets are SET INACTIVE when the condition is TRUE (inverted), and SET ACTIVE when FALSE.")]
	public bool activateTargetsWhenConditionTrue;

	[Tooltip("Targets to toggle on/off when the condition evaluates. If empty, NO ACTION is taken (safety).\nAdd explicit targets to avoid accidentally disabling this component's own GameObject.")]
	public List<GameObject> targets;

	[Header("Events")]
	[Tooltip("Invoked after targets are toggled (or computed if no targets), passing the final intended 'active' state.\nNote: This fires whenever the relay evaluates (on enable and on mutator changes), regardless of whether state changed.")]
	public UnityEvent<bool> onApplied;

	[Header("Debug")]
	[Tooltip("If true, logs condition checks and state transitions.")]
	public bool verbose;

	private MutatorRuntime _runtime;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void EnsureRuntime()
	{
	}

	private void OnMutatorsChanged(IReadOnlyList<MutatorDefinition> _)
	{
	}

	[ContextMenu("Apply Now")]
	public void ApplyNow()
	{
	}

	private bool EvaluateCondition()
	{
		return false;
	}
}
