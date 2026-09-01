using UnityEngine;
using UnityEngine.InputSystem;

namespace TutorialCutaways
{
	[DisallowMultipleComponent]
	public class TutorialCutawayForceEndInput : MonoBehaviour
	{
		public enum ForceEndMode
		{
			[Tooltip("Treat forced end as an interruption (fires onCutawayInterrupted on the cue).")]
			Interrupt = 0,
			[Tooltip("Treat forced end as a normal completion (fires onCutawayCompleted on the cue).")]
			Complete = 1
		}

		[Header("Service Discovery")]
		[Tooltip("Optional explicit reference to the TutorialCutawayService.\nAssign to skip search.\nLeave null → automatically resolved on enable / first input via:\n 1) Singleton Instance\n 2) GameObject.FindWithTag(serviceTag)\n 3) FindObjectOfType<TutorialCutawayService>(true)\nIf resolution fails, input attempts will log a warning (if debugLogging).")]
		public TutorialCutawayService serviceReference;

		[Tooltip("Unity Tag used to locate the service if 'serviceReference' is null and no singleton exists.\nMust match the tag on the TutorialCutawayService GameObject.\nExample: 'TutorialCutawayService'")]
		public string serviceTag;

		[Header("Input Action")]
		[Tooltip("InputActionReference pointing to a Button-like action (Press / Key / Gamepad button / UI event).\nRequirements:\n- Action type should be 'Button' or 'Pass-Through' mapping to a control you press.\n- Bindings configured in the Input Actions Asset (no hardcoded keys here).\nBehavior:\n- On performed: attempts to force end the current active cutaway.\nIf null: component will do nothing (with warnings if debugLogging=true).")]
		public InputActionReference forceEndAction;

		[Header("Force End Settings")]
		[Tooltip("How the forced end is treated:\n- Interrupt: Fires onCutawayInterrupted on the active cue (use when user 'skips').\n- Complete: Fires onCutawayCompleted (use when action represents an accelerated finish).\nDoes not adjust usage counts; counts remain as they incremented on activation.")]
		public ForceEndMode endMode;

		[Tooltip("Automatically enable (action.Enable()) and disable (action.Disable()) the referenced InputAction on this component's enable/disable.\nIf false: YOU must manage the action's lifecycle elsewhere (e.g., PlayerInput).\nRecommended: true for simple prefab setups.")]
		public bool autoManageActionLifecycle;

		[Header("Behavior")]
		[Tooltip("If true, logs detailed resolution attempts, input events, and outcomes (no active cue, success, failure to resolve service).\nUseful for development; disable for production.")]
		public bool debugLogging;

		[Tooltip("If true, the input press will be ignored when there is no active cutaway (only logs if debugLogging).\nIf false, will still log attempt (debugLogging) but has no functional difference—no active means no action.\nProvided as a semantic switch in case you later want alt behavior (e.g., recall last cue). Currently no-op besides intent.")]
		public bool ignoreWithoutActive;

		private TutorialCutawayService _cachedService;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnForceEndPerformed(InputAction.CallbackContext ctx)
		{
		}

		private TutorialCutawayService ResolveServiceIfNeeded()
		{
			return null;
		}

		[ContextMenu("Simulate Force End (Interrupt)")]
		private void Context_ForceEndInterrupt()
		{
		}

		[ContextMenu("Simulate Force End (Complete)")]
		private void Context_ForceEndComplete()
		{
		}

		[ContextMenu("Log Service Resolution")]
		private void Context_LogServiceResolution()
		{
		}
	}
}
