using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Gameplay/Dynamic Cursor Manager (Logic Only)")]
public class DynamicCursorManager : MonoBehaviour
{
	public enum PresentationMode
	{
		FPSLocked = 0,
		FreeMouse = 1
	}

	public enum CursorVisualState
	{
		Default = 0,
		Hover = 1,
		Grab = 2
	}

	[Header("Mode")]
	[Tooltip("Initial presentation mode.\nFPSLocked = center-ray aim (good for first-person).\nFreeMouse = virtual-cursor ray (good for UI / console interaction).")]
	[SerializeField]
	private PresentationMode initialMode;

	[Header("Raycasting")]
	[Tooltip("Maximum distance (world units) for Interactable detection.")]
	[SerializeField]
	private float maxRayDistance;

	[Tooltip("LayerMask used to filter which objects can be detected as Interactables.")]
	[SerializeField]
	private LayerMask interactableLayers;

	[Tooltip("LayerMask for cursor blockers (occluders).\nIf a collider on one of these layers is hit BEFORE a valid Interactable, ALL world interactions are blocked.")]
	[SerializeField]
	private LayerMask cursorBlockerLayers;

	[Tooltip("Seconds between raycasts. 0 = every frame.\nClick-down forces an immediate refresh so blockers cannot be clicked through even with a non-zero interval.")]
	[SerializeField]
	private float raycastInterval;

	[Tooltip("If true, draws a cyan debug ray each time a detection ray is cast (Scene view only).")]
	[SerializeField]
	private bool debugDrawRay;

	[Tooltip("If true, uses the first valid Interactable hit by distance (fast).\nIf false, searches all hits and picks the closest valid Interactable.\nNote: Cursor blockers ALWAYS win by distance regardless of this setting.")]
	[SerializeField]
	private bool stopAtFirstValidHit;

	[Tooltip("If true, trigger colliders are ignored by the detection raycast.\nNote: This applies to BOTH interactables and blockers.")]
	[SerializeField]
	private bool ignoreTriggerColliders;

	[Tooltip("Camera used to generate the interaction ray. If null at runtime, Camera.main is used.")]
	[SerializeField]
	private Camera raycastCamera;

	[Header("System Cursor Handling")]
	[Tooltip("If true, hides the OS/system cursor in BOTH modes (you rely entirely on a UI cursor).")]
	[SerializeField]
	private bool hideSystemCursor;

	[Tooltip("If true, locks the system cursor to center in FPSLocked mode (Cursor.lockState = Locked).")]
	[SerializeField]
	private bool lockCursorInFPSMode;

	[Tooltip("If true, confines system cursor to window bounds in FreeMouse mode (when visible).")]
	[SerializeField]
	private bool confineSystemCursorInFreeMode;

	[Header("Drag / Grab Behavior")]
	[Tooltip("If true, when a draggable object is grabbed (dragging), the Grab state persists even if the ray moves off its collider.\nNote: Cursor blockers override this while blocked (cursor becomes Default, as if nothing is there).")]
	[SerializeField]
	private bool persistGrabDuringDrag;

	[Tooltip("If true, once a drag ends the cursor state forcibly returns to Default before re-hover evaluation.\nIf false, it immediately re-evaluates hover that frame.")]
	[SerializeField]
	private bool forceDefaultAfterGrabEnd;

	[Header("Events / Safety")]
	[Tooltip("If true, OnCursorTargetChanged is invoked whenever the hovered Interactable changes.")]
	[SerializeField]
	private bool emitHoverChangeEvents;

	[Tooltip("If true, OnCursorVisualStateChanged is invoked when visual state changes or is forcibly broadcast.")]
	[SerializeField]
	private bool emitVisualStateEvents;

	[Tooltip("If true, re-broadcasts the current visual state (even if unchanged) whenever the presentation mode switches.")]
	[SerializeField]
	private bool broadcastStateOnModeSwitch;

	[Tooltip("If true, when this component is disabled/destroyed, the system cursor is made visible & unlocked.")]
	[SerializeField]
	private bool restoreSystemCursorOnDisable;

	[Header("Click Broadcast")]
	[Tooltip("If true, broadcasts OnPrimaryClickDown/OnPrimaryClickUp.\nThe 'Down' carries the Interactable hovered at press time (may be null), and 'Up' uses the same captured target.\nIf a cursor blocker is in front, the captured target will be null (no click-through).\n\nNote:\n- If the manager is suppressed by the InteractionLockBroker, no click events are broadcast.")]
	[SerializeField]
	private bool emitPrimaryClickEvents;

	[Header("Input System")]
	[Tooltip("Primary click actions (buttons). Any enabled action in this list can start/end a click.\nRecommended: Assign ONLY your 'Universal/PrimaryClick' action here (always enabled).\nImportant:\n- This script does not provide device/keybind fallbacks.\n- If these actions are not assigned/enabled, clicks will not be detected.\n\nNote:\n- If the manager is suppressed by the InteractionLockBroker, presses are ignored and any active press is force-released.")]
	[SerializeField]
	private List<InputActionReference> primaryClickActions;

	[Tooltip("If true, enables all listed click actions on OnEnable() (useful if not controlled by PlayerInput).")]
	[SerializeField]
	private bool enableActionsOnEnable;

	[Header("Click Aggregation Safety")]
	[Tooltip("If true, each frame we verify whether ANY listed action is still physically pressed.\nIf none are pressed but the internal state thinks a press is active, we synthesize a safe release.\n\nMulti-device note: this is the primary defence against mis-counted presses when the same action\nhas bindings on both mouse and gamepad — Unity fires 'started' once per binding, but IsPressed()\nis already correctly aggregated across all bindings. The sanitizer uses IsPressed() as ground truth.")]
	[SerializeField]
	private bool sanitizeClickStateEachFrame;

	[Tooltip("If true, logs sanitizer decisions and press-state transitions to help diagnose sticky-press issues.")]
	[SerializeField]
	private bool logClickAggregation;

	[Header("Virtual Cursor (Required for FreeMouse)")]
	[Tooltip("The unified VirtualCursor that provides a single screen-space pointer, driven by Input Actions.\nRequired for FreeMouse mode.\nIn FPSLocked mode, the VirtualCursor may be bound to center by its own settings.")]
	[SerializeField]
	private VirtualCursor virtualCursor;

	[Header("Manager-Driven Drag Routing (Prevents stack-grab)")]
	[Tooltip("If true, DynamicCursorManager will begin/end MapPiece3D drags directly on click down/up.\nThis prevents multiple MapPiece3D objects from all starting drag independently when stacked.\nSafe default: true.")]
	[SerializeField]
	private bool routeMapPieceDragsThroughManager;

	[Tooltip("If true, when a MapPiece3D drag starts, the manager will assign its own VirtualCursor reference to the piece (if available).\nThis is prefab-friendly and avoids needing to manually set VirtualCursor on every piece.\nSafe default: true.")]
	[SerializeField]
	private bool autoAssignVirtualCursorToMapPieces;

	[Tooltip("If true, DynamicCursorManager will begin/end DraggableItem drags directly on click down/up.\nEnsures only one DraggableItem starts drag per press, even when items are stacked.\nSafe default: true.")]
	[SerializeField]
	private bool routeDraggableItemsThroughManager;

	[SerializeField]
	private PlayerInput _playerInput;

	private PresentationMode _currentMode;

	private float _lastRayTime;

	private Interactable _currentHover;

	private Interactable _currentPassiveHover;

	private ICursorDraggable _activeDrag;

	private CursorVisualState _currentVisualState;

	private bool _clickPressed;

	private bool _clickDownThisFrame;

	private bool _clickWasPressedLastFrame;

	private Interactable _pressSourceForBroadcast;

	private bool _uiIsBlockingWorld;

	private bool _uiWantsHoverState;

	private bool _worldIsBlockedByOccluder;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _startedHandlers;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _canceledHandlers;

	private ICursorDraggable _capturedDraggableOnPress;

	private MapPiece3D _capturedMapPieceOnPress;

	private DraggableItem _capturedDraggableItemOnPress;

	private bool _suppressedByLockBroker;

	public bool ClampMouseToValveSetting;

	public bool IsSuppressedByLockBroker => false;

	public VirtualCursor VirtualCursorRef => null;

	public PresentationMode CurrentMode => default(PresentationMode);

	public CursorVisualState CurrentVisualState => default(CursorVisualState);

	public Interactable CurrentHover => null;

	public Interactable CurrentPassiveHover => null;

	public bool IsDragging => false;

	public bool IsWorldBlockedByCursorBlocker => false;

	public bool IsClampedToValve { get; private set; }

	public bool IsClampingMouse { get; private set; }

	public Vector2 ValveScreenPosition { get; private set; }

	public Vector2 ValveDefaultScreenPosition { get; private set; }

	public float CursorDistanceMultiplierFromCenter { get; private set; }

	public bool IsAngleConstrained { get; private set; }

	public bool ResetToDefault { get; private set; }

	public float MinAngle { get; private set; }

	public float MaxAngle { get; private set; }

	public Interactable CurrentGrabInteractable => null;

	[Tooltip("Event fired when the cursor visual state changes (Default/Hover/Grab).")]
	public event Action<CursorVisualState> OnCursorVisualStateChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	[Tooltip("Event fired whenever the hovered Interactable changes (may be null).")]
	public event Action<Interactable> OnCursorTargetChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	[Tooltip("Event fired whenever the passive-hovered Interactable changes (may be null).\n\nA passive Interactable (IsPassive = true) is detected by the raycast but does NOT\nbecome CurrentHover and does NOT affect cursor visual state.\nThe ray continues past it to find a normal Interactable behind it.\n\nUse cases:\n- Medal slots in front of a mission card: this event tells listeners which slot\n  the cursor is over without breaking the card's hover state.")]
	public event Action<Interactable> OnPassiveTargetChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	[Tooltip("Event fired when a primary click press starts (button down), passing the Interactable hovered at press time (may be null).")]
	public event Action<Interactable> OnPrimaryClickDown
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	[Tooltip("Event fired when a primary click press ends (button up). Carries the same Interactable captured on Down (may be null).")]
	public event Action<Interactable> OnPrimaryClickUp
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	[Tooltip("Event fired when the manager is suppressed/unsuppressed by the InteractionLockBroker.\n\nUsage:\n- UnifiedCursorUI can listen to hide/show its renderer immediately.\n\nNotes:\n- Suppression means: hide virtual cursor + block world cursor interactions.")]
	public event Action<bool> OnSuppressedByLockBrokerChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	private void OnDisable()
	{
	}

	private void OnApplicationFocus(bool hasFocus)
	{
	}

	private void OnApplicationPause(bool pauseStatus)
	{
	}

	public void SetSuppressedByLockBroker(bool suppressed, bool forceRefresh = true)
	{
	}

	private void PerformHoverDetection()
	{
	}

	private Ray BuildRay()
	{
		return default(Ray);
	}

	private Vector2 GetActivePointerScreenPosition()
	{
		return default(Vector2);
	}

	private void HandleGrabInput()
	{
	}

	private void BeginDrag(ICursorDraggable draggable)
	{
	}

	private void OnDragStarted()
	{
	}

	private void OnDragEnded()
	{
	}

	private void EndDragSequence()
	{
	}

	private void ClearActiveDrag()
	{
	}

	private void ClearCapturedPressDrag()
	{
	}

	private void OnAnyClickStarted()
	{
	}

	private void OnAnyClickCanceled()
	{
	}

	private void ResolvePressStateFromActions()
	{
	}

	private bool IsAnyActionPressed()
	{
		return false;
	}

	private void SanitizeAggregatedClickState()
	{
	}

	private void ForceReleaseIfPressed(string reason)
	{
	}

	private void TryAdoptExternalActiveDrag()
	{
	}

	public void NotifyExternalDragStarted(ICursorDraggable draggable)
	{
	}

	public void NotifyExternalDragEnded(ICursorDraggable draggable)
	{
	}

	private void SetVisualState(CursorVisualState newState, bool forceBroadcast = false)
	{
	}

	public void ForceBroadcastVisualState()
	{
	}

	private void ApplyModeSettings(bool initializing)
	{
	}

	private void ApplySystemCursorSettings()
	{
	}

	public void SwitchToPresentationMode(PresentationMode mode)
	{
	}

	public void SwitchToFPSLocked()
	{
	}

	public void SwitchToFreeMouse()
	{
	}

	public void SetUIBlocking(bool shouldBlock, bool wantsHoverState)
	{
	}

	public void ForceRefresh(bool forceBroadcast = false)
	{
	}

	public bool IsCurrentDeviceGamepad()
	{
		return false;
	}

	public void ClampCursorToValve(Vector3 position, float distance, bool isClampingMouse, bool angleConstraint = false, bool resetToDefault = false, float minAngle = 0f, float maxAngle = 0f)
	{
	}

	public void DisableValveClamping()
	{
	}
}
