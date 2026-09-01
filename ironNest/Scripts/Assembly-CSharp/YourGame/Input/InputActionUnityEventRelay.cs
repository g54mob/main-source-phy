using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace YourGame.Input
{
	[AddComponentMenu("Input/Input Action UnityEvent Relay")]
	public sealed class InputActionUnityEventRelay : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Input Action to listen to (from your Input Actions asset).\nThis script does not define bindings; it uses whatever bindings are already on the referenced action.\nIf this is left empty, nothing will happen.\nSafe default: assign an InputActionReference created from your .inputactions asset.")]
		private InputActionReference action;

		[SerializeField]
		[Tooltip("If true, this component will call Enable() on the referenced action in OnEnable and Disable() in OnDisable.\nUse true for drop-in prefab behavior.\nSet false if another system (e.g., PlayerInput) owns enabling/disabling actions and you want to avoid conflicts.")]
		private bool autoEnableAction;

		[SerializeField]
		[Tooltip("If true, events only fire when the action's current value is considered active/non-default.\nButton actions: requires ReadValueAsButton() == true.\nValue actions (float/Vector2/Vector3): requires magnitude > small epsilon.\nOther value types: non-null is treated as active.\nSafe default: false (fire whenever the callback is received).")]
		private bool requireNonDefaultValue;

		[SerializeField]
		[Tooltip("If true, each event (Started, Performed, Canceled) will fire at most once for the lifetime of this component.\nOnce all three have fired (or whichever are wired up), the component unsubscribes itself from the action.\nUseful for one-shot triggers such as tutorial prompts or first-interaction events.\nSafe default: false (fire every time the action fires).")]
		private bool fireOnce;

		[Header("Events")]
		[SerializeField]
		[Tooltip("Invoked when the action enters the Started phase.\nCommonly: when a control first actuates.\nIf you only care about 'activation', you can ignore this and use OnPerformed instead.")]
		private UnityEvent onStarted;

		[SerializeField]
		[Tooltip("Invoked when the action is Performed.\nThis is usually what you want for 'activated' behavior (e.g., button pressed, action triggered).")]
		private UnityEvent onPerformed;

		[SerializeField]
		[Tooltip("Invoked when the action is Canceled.\nCommonly: when a control is released or the interaction is interrupted.")]
		private UnityEvent onCanceled;

		private Action<InputAction.CallbackContext> _startedHandler;

		private Action<InputAction.CallbackContext> _performedHandler;

		private Action<InputAction.CallbackContext> _canceledHandler;

		private bool _startedFired;

		private bool _performedFired;

		private bool _canceledFired;

		private InputAction BoundAction => null;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void UnsubscribeIfDone()
		{
		}

		private bool ShouldFire(InputAction.CallbackContext ctx)
		{
			return false;
		}
	}
}
