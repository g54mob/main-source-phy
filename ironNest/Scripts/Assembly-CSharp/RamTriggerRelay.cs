using UnityEngine;

[DisallowMultipleComponent]
public class RamTriggerRelay : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The trigger Collider that represents the ram contact volume.\n\nSetup rules:\n- This relay component MUST be on the same GameObject as this Collider.\n- This Collider MUST have 'Is Trigger' enabled.\n\nPhysics note:\n- Trigger events require at least one Rigidbody on either the ram side or the charge side.\n  The Rigidbody can be kinematic.")]
	private Collider ramTrigger;

	[SerializeField]
	[Tooltip("The controller that will receive forwarded trigger events.\n\nTypical setup:\n- Put RamFakeParentConstraintController on your Animator root (or another convenient object).\n- Drag that component reference here.\n\nIf unassigned:\n- UnityEvents below fire for every trigger interaction (raw passthrough, no charge filtering).\n\nIf assigned:\n- UnityEvents only fire when the controller confirms the entering collider is a valid charge match.")]
	private RamFakeParentConstraintController controller;

	[SerializeField]
	[Tooltip("Fired when a valid charge Collider first enters the ram trigger volume.\n\nArgument:\n- Collider  —  the Collider that entered the trigger.\n\nFiltering:\n- If a controller is assigned, this only fires when the controller confirms a charge match\n  AND it is the first contact of the current push cycle (one-shot per cycle).\n- If no controller is assigned, fires for every trigger enter.\n\nFires AFTER the controller is notified.")]
	private ColliderUnityEvent onRamTriggerEnter;

	[SerializeField]
	[Tooltip("Fired when a valid charge Collider leaves the ram trigger volume.\n\nArgument:\n- Collider  —  the Collider that exited the trigger.\n\nFiltering:\n- If a controller is assigned, this only fires when the controller confirms the exiting\n  collider was a charge match.\n- If no controller is assigned, fires for every trigger exit.\n\nFires AFTER the controller is notified.")]
	private ColliderUnityEvent onRamTriggerExit;

	public ColliderUnityEvent OnRamTriggerEnter => null;

	public ColliderUnityEvent OnRamTriggerExit => null;

	private void Reset()
	{
	}

	private void OnValidate()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}
}
