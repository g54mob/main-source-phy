using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraZoneTrigger : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CPhaseSubscribeRetryRoutine_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CameraZoneTrigger _003C_003E4__this;

		private int _003Cattempts_003E5__2;

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
		public _003CPhaseSubscribeRetryRoutine_003Ed__61(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CReapplyNextFrame_003Ed__48 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CameraZoneTrigger _003C_003E4__this;

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
		public _003CReapplyNextFrame_003Ed__48(int _003C_003E1__state)
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

	private static readonly HashSet<CameraZoneTrigger> s_allZones;

	private static CameraZoneTrigger s_currentActiveZone;

	[Header("Cameras")]
	[Tooltip("The console/zone virtual camera GameObject (e.g., CinemachineVirtualCamera) to enable when console is active.\n\nNotes:\n- This script toggles GameObject.SetActive(true/false).\n- This camera remains owned by this zone (not by the broker).")]
	public GameObject zoneCamera;

	[Tooltip("The player's standard virtual camera GameObject (e.g., CinemachineVirtualCamera) to re-enable when console is inactive.\n\nNotes:\n- This should be the player's own vcam that zones disable.\n- This camera remains owned by this zone (not by the broker).")]
	public GameObject playerCamera;

	[Header("UI Elements")]
	[Tooltip("UI prompt shown when player is in zone and console is inactive (e.g., 'Press Interact to use console').\n\nNotes:\n- This script toggles GameObject.SetActive(true/false).\n- Leave null if no prompt is desired.")]
	public GameObject promptUI;

	[Tooltip("UI shown while console is active (e.g., panel with exit instructions).\n\nNotes:\n- This script toggles GameObject.SetActive(true/false).\n- Leave null if not needed.")]
	public GameObject unlockUI;

	[Header("Input System (Actions)")]
	[Tooltip("InputActionReference for activating the console while in zone.\n\nTypical:\n- Bound to Player/Interact.\n\nRules:\n- No keybind fallbacks are provided.\n- This action must be enabled (this script can enable it on OnEnable).")]
	public InputActionReference activateAction;

	[Tooltip("InputActionReference for deactivating the console while active.\n\nTypical:\n- Bound to UI/Cancel.\n\nRules:\n- No keybind fallbacks are provided.\n- This action must be enabled (this script can enable it on OnEnable).")]
	public InputActionReference deactivateAction;

	[Header("Events")]
	[Tooltip("UnityEvent invoked AFTER console activation logic completes.\n\nUse for:\n- Starting animations\n- Playing sounds\n- Enabling other systems")]
	public UnityEvent onConsoleActivated;

	[Tooltip("UnityEvent invoked AFTER console deactivation logic completes.\n\nUse for:\n- Ending animations\n- Playing sounds\n- Restoring other systems")]
	public UnityEvent onConsoleDeactivated;

	[Header("Startup / Auto-Activation")]
	[Tooltip("If true, the console activates automatically in Start(), even if the player isn't inside the trigger.\n\nUse cases:\n- Additive menu scenes that should open a console immediately.\n\nNotes:\n- This will acquire a broker lock if enabled below.")]
	[SerializeField]
	private bool activateOnStart;

	[Header("Auto-Resolve By Tags (Across Scenes)")]
	[Tooltip("If true, missing references will be auto-resolved by tags across loaded scenes.\n\nNotes:\n- Uses GameObject.FindGameObjectWithTag (active objects only).\n- If tags are missing/undefined, lookups fail safely.")]
	[SerializeField]
	private bool autoResolveByTags;

	[Tooltip("If true, will try to re-resolve references when scenes are loaded/unloaded.\n\nUse cases:\n- Additive scenes where player/cameras/UI spawn later.\n\nNotes:\n- This does not re-activate zones; it only refreshes references.")]
	[SerializeField]
	private bool reResolveOnSceneEvents;

	[Space(4f)]
	[Tooltip("Tag for the Player controller root.\n\nDefault: 'Player'.\n\nUsage:\n- Used only to detect trigger enter/exit (CompareTag) and to optionally resolve related references.\n\nRules:\n- Must exist in Project Settings > Tags and Layers.")]
	[SerializeField]
	private string playerTag;

	[Tooltip("Tag for the player's virtual camera (Cinemachine vcam).\n\nDefault: 'CMCam'.\n\nUsage:\n- Used to auto-fill the Player Camera reference if missing.\n\nRules:\n- Must exist in Project Settings > Tags and Layers.")]
	[SerializeField]
	private string playerCameraTag;

	[Space(4f)]
	[Tooltip("Optional tag for the Prompt UI GameObject.\n\nDefault: empty (disabled).\n\nUsage:\n- If set, and Prompt UI reference is null, this script will find it by tag.\n\nRules:\n- If empty, lookup is skipped.")]
	[SerializeField]
	private string promptUITag;

	[Tooltip("Optional tag for the Unlock UI GameObject.\n\nDefault: empty (disabled).\n\nUsage:\n- If set, and Unlock UI reference is null, this script will find it by tag.\n\nRules:\n- If empty, lookup is skipped.")]
	[SerializeField]
	private string unlockUITag;

	[Header("Global Zone Exclusivity")]
	[Tooltip("If true (default), only a single CameraZoneTrigger can be active across ALL loaded scenes at any time.\n\nWhen this zone activates, it will automatically deactivate any other active zones.\n\nRecommended ON to prevent conflicting camera ownership.\n\nNotes:\n- This exclusivity is about ZONES (camera/UI ownership), not about broker locks.\n- Broker locks can still overlap (e.g., drag + zone), and are resolved safely by the broker.")]
	[SerializeField]
	private bool enforceSingleActiveZone;

	[Tooltip("If true no other camera zone can be activated while this is active")]
	[SerializeField]
	private bool disalowForceDeactivationBySingleActiveZone;

	[Header("Broker Lock (Camera Freeze + Cursor Mode + Optional UI Map)")]
	[Tooltip("If true, this zone will acquire/release a lock via InteractionLockBroker when activating/deactivating.\n\nRecommended: true.\n\nEffects (via broker):\n- Freeze player controller\n- Switch cursor mode to FreeMouse\n- Optionally switch PlayerInput map to UI\n\nNo fallback:\n- This script does not directly freeze controller or switch cursor modes.\n- If the broker is missing, a warning is logged and only camera/UI toggles occur.")]
	[SerializeField]
	private bool useBrokerLock;

	[Tooltip("Unity Tag used to locate the InteractionLockBroker.\n\nDefault: 'LockBroker'.\n\nSetup:\n- Place one InteractionLockBroker in your master scene.\n- Tag that GameObject with this tag.\n\nRules:\n- Tag must exist in Project Settings > Tags and Layers.")]
	[SerializeField]
	private string lockBrokerTag;

	[Tooltip("If true, the broker lock request will freeze the player's FirstPersonController.\n\nTypical: true for console zones.\n\nNotes:\n- This does not freeze cameras directly; it calls SetFrozen(true) through the broker.")]
	[SerializeField]
	private bool brokerFreezePlayerController;

	[Tooltip("If true, the broker lock request will set DynamicCursorManager mode to FreeMouse while the console is active.\n\nTypical: true for console zones.\n\nNotes:\n- If false, you can have a console camera with FPSLocked cursor mode (uncommon).")]
	[SerializeField]
	private bool brokerUseFreeMouse;

	[Tooltip("If true, the broker lock request will switch PlayerInput to the broker's configured UI action map.\n\nTypical: true if your console UI relies on UI map.\n\nImportant:\n- The broker supports exactly ONE UI action map name and ONE Player action map name.\n- If your project uses multiple UI maps, fix that at source rather than extending the broker.")]
	[SerializeField]
	private bool brokerUseUIActionMap;

	[Tooltip("Debug label sent to the broker for this zone's lock request.\n\nUsed for:\n- Broker logs\n- Diagnosing overlapping locks\n\nSafe examples:\n- 'Zone:MainConsole'\n- 'Zone:ReactorPanelA'")]
	[SerializeField]
	private string brokerDebugLabel;

	[Tooltip("If true and broker lock is used, deactivation will be performed only if the lock is most recent one.\nThis can help to prevent deactivation if there are other interaction locking objects (e.g. manuals) inside the zone.")]
	[SerializeField]
	private bool brokerRequireMostRecentToDeactivate;

	[Header("External Activation (Optional)")]
	[Tooltip("If true, this component remembers the last ForceActivate()/ForceDeactivate() request and will re-apply it when re-enabled.\n\nUse this when this zone lives under a parent GameObject that is toggled SetActive(false/true) by mission/menu scripts.\n\nDefault: false (preserves existing trigger-only behavior).")]
	[SerializeField]
	private bool reapplyForceStateOnEnable;

	[Tooltip("If true and 'Reapply Force State On Enable' is enabled, re-application is performed on the next frame via coroutine.\n\nThis makes UnityEvent-driven enable->ForceActivate sequences reliable when activation happens in the same frame that objects are enabled.\n\nDefault: true.")]
	[SerializeField]
	private bool reapplyForceStateNextFrame;

	[Tooltip("If true and 'Reapply Force State On Enable' is enabled, this component resets its baseline (prompt/off, unlock/off, zoneCamera/off, playerCamera/on)\nwhen it becomes enabled before applying any requested active state.\n\nDefault: true.")]
	[SerializeField]
	private bool resetBaselineOnEnable;

	[Header("Mission Phase Gating")]
	[Tooltip("If true (default), this zone watches MissionManager.PhaseChanged and automatically enables or disables\nplayer interaction based on the current game phase.\n\nEnabled phases (player can interact):\n- MissionActive\n\nDisabled phases (interaction silently blocked):\n- MainMenu\n- BrowsingMap\n\nWhen disabled by phase:\n- The prompt UI is hidden immediately.\n- If the console is somehow active, ForceDeactivate() is called.\n- OnActivatePerformed input is silently ignored.\n\nSet to false only for zones that should always be interactive regardless of phase (rare).")]
	[SerializeField]
	private bool watchMissionPhase;

	private bool playerInZone;

	private bool consoleActive;

	private bool _interactionEnabled;

	private bool _wasTimeScalePaused;

	private bool _forceRequestedActive;

	private Coroutine _reapplyRoutine;

	private Coroutine _phaseSubscribeRetryRoutine;

	private InteractionLockBroker _broker;

	private InteractionLockBroker.LockHandle _brokerHandle;

	private BoxCollider _boxCollider;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void ApplyBaselineVisualState()
	{
	}

	private void ReapplyRequestedState()
	{
	}

	[IteratorStateMachine(typeof(_003CReapplyNextFrame_003Ed__48))]
	private IEnumerator ReapplyNextFrame()
	{
		return null;
	}

	private void ForceActivateImmediate()
	{
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	private void OnSceneUnloaded(Scene scene)
	{
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
	}

	private void OnActivatePerformed(InputAction.CallbackContext ctx)
	{
	}

	private void OnDeactivatePerformed(InputAction.CallbackContext ctx)
	{
	}

	private void ResolveByTagsIfNeeded()
	{
	}

	private static GameObject SafeFindWithTag(string tag)
	{
		return null;
	}

	private void TryFindBroker()
	{
	}

	public void SetInteractionEnabled(bool enabled)
	{
	}

	private void TrySubscribeToPhaseOrRetry()
	{
	}

	[IteratorStateMachine(typeof(_003CPhaseSubscribeRetryRoutine_003Ed__61))]
	private IEnumerator PhaseSubscribeRetryRoutine()
	{
		return null;
	}

	private void SubscribeToPhase()
	{
	}

	private void UnsubscribeFromPhase()
	{
	}

	private void OnPhaseChanged(MissionManager.GamePhase prev, MissionManager.GamePhase next)
	{
	}

	public void ActivateConsole()
	{
	}

	public void DeactivateConsole()
	{
	}

	public void ForceActivate()
	{
	}

	public void ForceDeactivate()
	{
	}

	private void EnsureBrokerLock()
	{
	}

	private void ReleaseBrokerLockIfHeld()
	{
	}

	private void SafeRevertFromDisable()
	{
	}

	private void InteractionLockBroker_OnRequestsChanged()
	{
	}
}
