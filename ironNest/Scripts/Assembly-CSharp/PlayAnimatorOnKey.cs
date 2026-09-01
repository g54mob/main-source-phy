using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Input/Play Animator On Input (New Input System)")]
[RequireComponent(typeof(Animator))]
public class PlayAnimatorOnKey : MonoBehaviour
{
	public enum BehaviourTypes
	{
		Trigger = 0,
		Bool = 1,
		BoolHold = 2,
		Play = 3
	}

	[Tooltip("Target Animator to control.\n- If left empty, this component will try to auto-assign the Animator on this GameObject.\n- Safe default: the Animator on the same GameObject.")]
	public Animator animator;

	[Tooltip("How the Animator should react when the input is pressed (and released for BoolHold):\n- Trigger: Calls Animator.SetTrigger(parameter).\n- Bool: Toggles Animator.SetBool(parameter) true/false on each press.\n- BoolHold: Sets Animator.SetBool(parameter) true on press, false on release.\n- Play: Calls Animator.Play(stateName) on press.")]
	public BehaviourTypes Behaviour;

	[Tooltip("Name used by the selected Behaviour.\n- Trigger / Bool / BoolHold: Animator parameter name (must exist and be of the correct type).\n- Play: Animator state name (can be a simple state name on the default layer, or a path including sub-state machines, e.g., \"Base Layer/Run\").\nExamples:\n- Parameter: \"Shoot\", \"IsAiming\", \"AttackTrigger\"\n- State: \"Run\", \"Base Layer/Run\", \"UpperBody/Fire\"\nNotes:\n- Names are case-sensitive.\n- For Play, this calls Animator.Play(name) on layer 0 from the beginning.")]
	public string variableName;

	[Tooltip("Input Action that triggers this component.\n- Recommended: Action Type = Button.\n- This component treats 'performed' as PRESS and 'canceled' as RELEASE.\n- If you add Press/Hold/Tap interactions, 'performed' timing follows the interaction's rules.\nSetup tips:\n- Create an Input Actions asset.\n- Add a 'Button' action bound to a key, gamepad button, etc.\n- Assign that action here as an InputActionReference.")]
	public InputActionReference inputAction;

	[Tooltip("Whether this component should enable/disable the assigned InputAction automatically.\n- Enable on OnEnable and Disable on OnDisable when true.\n- Set to false if a PlayerInput or another system manages action/map lifetime.")]
	public bool manageActionEnable;

	private bool boolState;

	private InputAction _action;

	private void Reset()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnActionPerformed(InputAction.CallbackContext ctx)
	{
	}

	private void OnActionCanceled(InputAction.CallbackContext ctx)
	{
	}

	private bool EnsureParameter(Animator anim, string name, AnimatorControllerParameterType expectedType)
	{
		return false;
	}

	private static bool HasAnimatorParameter(Animator anim, string name, AnimatorControllerParameterType type)
	{
		return false;
	}
}
