using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[AddComponentMenu("Input/Key Press UnityEvent (Input System, Toggle, Hold)")]
public sealed class KeyPressUnityEvent : MonoBehaviour
{
	public enum ListenMode
	{
		AnyAction = 0,
		SpecificAction = 1
	}

	[CompilerGenerated]
	private sealed class _003CHoldTimerRoutine_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int tokenAtStart;

		public KeyPressUnityEvent _003C_003E4__this;

		public float durationSeconds;

		private float _003Ct_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CHoldTimerRoutine_003Ed__28(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[Header("Listening")]
	[SerializeField]
	[Tooltip("Which listening mode to use.\n\nModes:\n• AnyAction: Respond when ANY InputActionReference in \"Any Actions\" enters its 'started' phase.\n• SpecificAction: Respond only when the single \"Specific Action\" enters its 'started' phase.\n\nTechnical Notes:\n• Uses Unity's new Input System via InputActionReference.\n• Press detection is based on InputAction.started (usually a single fire for Button actions).\n• If Hold Enabled is true, this component also subscribes to InputAction.canceled to optionally cancel holds.")]
	private ListenMode listenFor;

	[SerializeField]
	[Tooltip("Single InputActionReference used when Listen Mode = SpecificAction.\n\nBehavior:\n• If non-null, this action's 'started' triggers On Key Pressed (+ optional Toggle/Hold behaviors).\n• If null or missing action, it is safely ignored.\n\nRecommendations:\n• Use a Button-type action/binding for clean single presses.\n• Enable/disable the action externally (e.g., PlayerInput / InputActionAsset).")]
	private InputActionReference specificAction;

	[SerializeField]
	[Tooltip("List of InputActionReferences OR-aggregated when Listen Mode = AnyAction.\n\nBehavior:\n• Any non-null action entering 'started' triggers On Key Pressed (+ optional Toggle/Hold behaviors).\n• Duplicate underlying InputAction instances are subscribed only once.\n• Null entries are safely ignored.\n\nRecommendations:\n• Provide multiple device/button variations that should act identically.\n• Keep list tidy; null entries are ignored.")]
	private InputActionReference[] anyActions;

	[Header("Press Event")]
	[SerializeField]
	[Tooltip("Invoked once per valid press.\n\nDefinition of a valid press:\n• The relevant action enters its 'started' phase AND passes Listen Mode checks.\n\nOrdering:\n1) On Key Pressed\n2) Toggle events (if Toggle Enabled)\n3) Hold begins timing (if Hold Enabled)\n\nNotes:\n• Fires regardless of Toggle Enabled / Hold Enabled.\n• No arguments.")]
	private UnityEvent onKeyPressed;

	[Header("Toggle")]
	[SerializeField]
	[Tooltip("Enables local toggle behavior.\n\nBehavior When Enabled:\n• Each valid press flips an internal boolean state.\n• On Toggle On fires when state becomes TRUE.\n• On Toggle Off fires when state becomes FALSE.\n\nBehavior When Disabled:\n• Toggle state remains unchanged.\n• Toggle events do not fire.\n\nNotes:\n• On Key Pressed still fires regardless.\n• Toggle is purely local state; does not affect the action itself.")]
	private bool toggleEnabled;

	[SerializeField]
	[Tooltip("Initial toggle state applied when the component is enabled.\n\nMeaning:\n• TRUE  => the next toggle change (on press) will go FALSE and fire On Toggle Off.\n• FALSE => the next toggle change (on press) will go TRUE and fire On Toggle On.\n\nNotes:\n• Applied once in OnEnable (unless changed by code later).\n• Does not invoke toggle events by itself.")]
	private bool initialToggleState;

	[SerializeField]
	[Tooltip("Invoked AFTER On Key Pressed when Toggle Enabled and the state becomes TRUE.\n\nOrdering:\n1) On Key Pressed\n2) On Toggle On / Off\n\nNotes:\n• Not called if Toggle Enabled is false.\n• Not called if resulting state is FALSE.")]
	private UnityEvent onToggleOn;

	[SerializeField]
	[Tooltip("Invoked AFTER On Key Pressed when Toggle Enabled and the state becomes FALSE.\n\nOrdering:\n1) On Key Pressed\n2) On Toggle On / Off\n\nNotes:\n• Not called if Toggle Enabled is false.\n• Not called if resulting state is TRUE.")]
	private UnityEvent onToggleOff;

	[Tooltip("Runtime view of the current toggle state.\n\nNotes:\n• Updated only when Toggle Enabled and a valid press occurs.\n• Serialized for debugging/inspection; not intended for direct editing.\n• Use SetToggleState(...) / ResetToggleState() for controlled changes.")]
	[SerializeField]
	private bool currentToggleState;

	[Header("Hold")]
	[SerializeField]
	[Tooltip("Enables local 'hold' behavior.\n\nBehavior When Enabled:\n• On a valid press (action.started), a timer begins.\n• If the timer reaches Hold Duration (seconds) without being canceled, On Hold Elapsed is invoked once.\n• If the hold is canceled before completing (see cancellation rules), On Hold Canceled is invoked once.\n\nBehavior When Disabled:\n• No hold timer runs and hold events do not fire.\n\nImportant:\n• Hold is purely local timing; it does not affect the InputAction.\n• Only one hold can be active at a time per component; a new valid press replaces the previous hold.")]
	private bool holdEnabled;

	[SerializeField]
	[Min(0f)]
	[Tooltip("Duration (in seconds) required to invoke On Hold Elapsed.\n\nRules:\n• Must be >= 0.\n• If set to 0, On Hold Elapsed will fire on the next frame after a valid press (if Hold Enabled).\n\nTiming:\n• Uses unscaled time (Time.unscaledDeltaTime) so it is not affected by timeScale.\n• If you want holds affected by pause/slow-mo, request scaled time instead.")]
	private float holdDurationSeconds;

	[SerializeField]
	[Tooltip("If true, cancel an in-progress hold when the action triggers InputAction.canceled.\n\nRecommended:\n• TRUE for typical 'press and hold, then release' button actions.\n\nWhen false:\n• Releasing may not cancel the hold (depending on bindings), so holds may only stop when replaced or when the component disables.\n\nNotes:\n• Cancellation only applies to the action that started the current hold.")]
	private bool holdCancelsOnActionCanceled;

	[SerializeField]
	[Tooltip("Invoked once when the hold duration completes successfully.\n\nRequirements:\n• Hold Enabled must be true.\n• The component must remain enabled.\n• The hold must not be canceled/replaced before the duration completes.\n\nOrdering:\n• Fires later (after Hold Duration), not during action.started.")]
	private UnityEvent onHoldElapsed;

	[SerializeField]
	[Tooltip("Invoked once when an active hold is canceled BEFORE the hold duration completes.\n\nCancellation can happen when:\n• The initiating action triggers InputAction.canceled (only if Hold Cancels On Action Canceled is true).\n• A new valid press starts a new hold (the old hold is replaced).\n• The component is disabled (OnDisable cancels any active hold).\n\nNotes:\n• This does NOT fire if the hold already elapsed.\n• This is useful for 'charge-up canceled' effects, UI feedback, etc.")]
	private UnityEvent onHoldCanceled;

	private readonly List<InputAction> subscribedActions;

	private Coroutine holdRoutine;

	private InputAction holdAction;

	private int holdToken;

	private bool holdElapsedForCurrentToken;

	public UnityEvent OnKeyPressedEvent => null;

	public UnityEvent OnToggleOnEvent => null;

	public UnityEvent OnToggleOffEvent => null;

	public UnityEvent OnHoldElapsedEvent => null;

	public UnityEvent OnHoldCanceledEvent => null;

	public bool CurrentToggleState => false;

	public bool ToggleEnabled => false;

	public bool HoldEnabled => false;

	public float HoldDurationSeconds => 0f;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void SubscribeAll()
	{
	}

	private void UnsubscribeAll()
	{
	}

	private void HandleActionStarted(InputAction.CallbackContext ctx)
	{
	}

	private void HandleActionCanceled(InputAction.CallbackContext ctx)
	{
	}

	private void StartHold(InputAction action)
	{
	}

	private void CancelHold(bool invokeCanceledEvent)
	{
	}

	[IteratorStateMachine(typeof(_003CHoldTimerRoutine_003Ed__28))]
	private IEnumerator HoldTimerRoutine(int tokenAtStart, float durationSeconds)
	{
		return null;
	}

	public void SetListenMode(ListenMode mode)
	{
	}

	public void SetSpecificAction(InputActionReference actionReference)
	{
	}

	public void SetAnyActions(InputActionReference[] actions)
	{
	}

	public void SetToggleEnabled(bool enabled)
	{
	}

	public void SetToggleState(bool newState, bool invokeEvent = false)
	{
	}

	public void ResetToggleState(bool invokeEvent = false)
	{
	}

	public void ForceToggle(bool invokeEvent = true)
	{
	}

	public void RefreshSubscriptions()
	{
	}

	public void SetHoldEnabled(bool enabled)
	{
	}

	public void SetHoldDurationSeconds(float seconds)
	{
	}

	public void CancelHoldTimer()
	{
	}
}
