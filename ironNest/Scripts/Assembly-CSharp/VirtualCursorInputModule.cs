using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[AddComponentMenu("Gameplay/Virtual Cursor Input Module")]
[RequireComponent(typeof(EventSystem))]
public class VirtualCursorInputModule : BaseInputModule
{
	[CompilerGenerated]
	private sealed class _003CDelayedSelectedChange_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VirtualCursorInputModule _003C_003E4__this;

		public GameObject selectable;

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
		public _003CDelayedSelectedChange_003Ed__34(int _003C_003E1__state)
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

	[Header("References (Required)")]
	[Tooltip("VirtualCursor that provides the unified screen-space pointer position (pixels), driven by Input Actions.\nRequired: This module reads VirtualCursor.ScreenPosition every frame to drive the UI pointer.")]
	[SerializeField]
	private VirtualCursor virtualCursor;

	[Tooltip("DynamicCursorManager used to block 3D raycasts and to control cursor visuals while over UI.\nRequired if you want UI hover to suppress world interactions.\nIf null: UI still works, but world blocking/hover state updates are skipped.")]
	[SerializeField]
	private DynamicCursorManager cursorManager;

	[Header("Input System")]
	[Tooltip("Primary click actions (buttons). Any enabled action in this list can press/release for UI clicks.\nRecommended: Assign the SAME action(s) as DynamicCursorManager (e.g., Universal/PrimaryClick).\nAggregation rules:\n- If ANY action is pressed => pointer is considered pressed.\n- Pointer is released only when ALL actions are released.\nNo fallbacks are provided; if these actions are not assigned/enabled, UI clicks will not work.")]
	[SerializeField]
	private List<InputActionReference> primaryClickActions;

	[SerializeField]
	private InputActionReference scrollAction;

	[Tooltip("If true, enables all listed click actions on OnEnable() when not already enabled.\nDisable if a higher-level system (e.g., PlayerInput) manages action enable/disable.")]
	[SerializeField]
	private bool enableActionsOnEnable;

	[Header("Mode-Based UI Interaction")]
	[Tooltip("If true, UI interaction is enabled while DynamicCursorManager is in FPSLocked mode (center-screen reticle).\nUseful for interactable HUDs.\nIf false, UI will be ignored in FPSLocked mode.")]
	[SerializeField]
	private bool enableUIInFPSLockedMode;

	[Tooltip("If true, UI interaction is enabled while DynamicCursorManager is in FreeMouse mode (free pointer).\nTypical menu behavior: true.\nIf false, UI will be ignored in FreeMouse mode.")]
	[SerializeField]
	private bool enableUIInFreeMouseMode;

	[Header("World Blocking / Cursor State")]
	[Tooltip("If true, when hovering over interactable UI this module tells DynamicCursorManager to block all 3D/world raycasts.\nRecommended: true (UI blocks world interactions behind it).")]
	[SerializeField]
	private bool blockWorldRaycastsWhenOverUI;

	[Tooltip("If true, when hovering over interactable UI this module requests Hover visual state from DynamicCursorManager.\nRecommended: true for consistent cursor feedback between UI and 3D.")]
	[SerializeField]
	private bool setCursorHoverStateForUI;

	[Header("Hierarchy Search (Interactable Target)")]
	[Tooltip("Maximum hierarchy depth to search upward for interactable UI components.\nWhy: Unity UI often raycasts a child Graphic (Text/Image), while the Button/Slider lives on a parent.\nDefault 10 is safe for typical prefabs.")]
	[SerializeField]
	private int maxHierarchySearchDepth;

	[Header("Drag Support (Required for Slider Handle Dragging)")]
	[Tooltip("If true, this module generates full UGUI drag events while the click is held:\n- InitializePotentialDrag on press\n- BeginDrag when movement exceeds threshold\n- Drag each frame while held\n- EndDrag on release\nRequired for: Slider handle dragging, ScrollRect dragging, etc.")]
	[SerializeField]
	private bool enableDragEvents;

	[Tooltip("If true, uses EventSystem.pixelDragThreshold to decide when a drag begins.\nIf false, BeginDrag is sent immediately on press if a drag handler exists.")]
	[SerializeField]
	private bool useDragThreshold;

	[Header("Diagnostics")]
	[Tooltip("If true, logs UI hover enter/exit, click down/up, and click execution.\nWarning: may be spammy; enable only when debugging.")]
	[SerializeField]
	private bool logUIEvents;

	[Tooltip("If true, logs click aggregation press/release counts and state transitions.\nWarning: very detailed; enable only for Input System debugging.")]
	[SerializeField]
	private bool logClickAggregation;

	[Tooltip("If true, logs raw raycast hits and hierarchy-search decisions.\nWarning: extremely spammy (can log every frame). Use only for deep debugging.")]
	[SerializeField]
	private bool logRaycastDetails;

	private PointerEventData _pointerEventData;

	private GameObject _currentPointerTarget;

	private GameObject _currentRaycastHit;

	private GameObject _currentPointerPress;

	private bool _isOverInteractableUI;

	private Vector2 _lastPointerPosition;

	private bool _draggingThisPress;

	private bool _clickPressed;

	private bool _wasClickPressed;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _startedHandlers;

	private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _canceledHandlers;

	protected override void Awake()
	{
	}

	protected override void OnEnable()
	{
	}

	protected override void OnDisable()
	{
	}

	public override void Process()
	{
	}

	private bool ValidateSetup()
	{
		return false;
	}

	private bool IsUIInteractionEnabled()
	{
		return false;
	}

	public void SetSelectedEventSystemObject(GameObject selectable)
	{
	}

	public void SetSelectedObjectToNull()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayedSelectedChange_003Ed__34))]
	private IEnumerator DelayedSelectedChange(GameObject selectable)
	{
		return null;
	}

	private void ProcessHover()
	{
	}

	private GameObject FindInteractableInHierarchy(GameObject start)
	{
		return null;
	}

	private void ClearPointerState(bool sendExit)
	{
	}

	private void ProcessPressReleaseAndDrag()
	{
	}

	private void HandlePress()
	{
	}

	private void HandleDragWhileHeld()
	{
	}

	private void HandleRelease()
	{
	}

	private static bool IsChildOf(GameObject child, GameObject parent)
	{
		return false;
	}

	private void UpdateManagerState()
	{
	}

	private void SubscribeToClickActions()
	{
	}

	private void UnsubscribeFromClickActions()
	{
	}

	private void OnAnyClickStarted()
	{
	}

	private void OnAnyClickCanceled()
	{
	}

	private bool IsAnyActionPressed()
	{
		return false;
	}

	private void ResolvePressState()
	{
	}

	private void ForceReleaseIfPressed(string reason)
	{
	}

	private void ProcessScroll()
	{
	}
}
