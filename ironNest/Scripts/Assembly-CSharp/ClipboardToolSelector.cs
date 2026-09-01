using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Clipboard/Clipboard Tool Selector")]
public class ClipboardToolSelector : MonoBehaviour
{
	[Serializable]
	public class ToolChangedEvent : UnityEvent<ClipboardToolSlot>
	{
	}

	private class TransitionState
	{
		public Vector3 startPos;

		public Quaternion startRot;

		public Vector3 startScale;

		public Vector3 endPos;

		public Quaternion endRot;

		public Vector3 endScale;

		public float startTime;

		public float duration;
	}

	private static class ListPool<T>
	{
		private static readonly Stack<List<T>> Pool;

		public static List<T> Get()
		{
			return null;
		}

		public static void Release(List<T> list)
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CAutoSelectOnFirstEnableRoutine_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ClipboardToolSelector _003C_003E4__this;

		private float _003Cstart_003E5__2;

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
		public _003CAutoSelectOnFirstEnableRoutine_003Ed__40(int _003C_003E1__state)
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
	private sealed class _003CCursorOverrideRetryRoutine_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ClipboardToolSelector _003C_003E4__this;

		public float retrySeconds;

		private float _003Cstart_003E5__2;

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
		public _003CCursorOverrideRetryRoutine_003Ed__41(int _003C_003E1__state)
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

	[Header("References")]
	[Tooltip("MapMarkerPlacer that will receive the active marker prefab when a tool is selected.\nRequired if you want selection to change the active marker.\nIf null, selection still drives visuals and movement, but marker placement will not change.")]
	public MapMarkerPlacer mapMarkerPlacer;

	[Tooltip("Transform that represents the desired local pose for the selected tool (top of clipboard).\nThe selected tool will be moved to match this transform's localPosition/localRotation/localScale.\nRequired.\n\nImportant:\n- Tools and this anchor must share the same local space (typically the same parent under the clipboard).")]
	public Transform selectedAnchor;

	[Tooltip("Optional runtime cursor override provider on the MAP Interactable (recommended).\nIf assigned, selecting a tool can request a cursor override texture to be used while hovering/grabbing the map.\n\nImportant:\n- This does NOT change Interactable's serialized cursorOverride fields.\n- UnifiedCursorUI must be configured to use runtime overrides (useRuntimeCursorOverrides = true) for this to show visually.\n- Cursor hotspots are not supported; override textures are always centered.")]
	public InteractableRuntimeCursorOverride mapCursorOverride;

	[Header("Slots")]
	[Tooltip("List of selectable tool slots.\n\nDesigner-friendly approach:\n- Drag each ClipboardToolSlot (tool object) into this list.\n\nOptional:\n- Enable 'Auto Find Slots In Children If Empty' to populate at runtime if you prefer not to maintain the list.\n\nNotes:\n- Each ClipboardToolSlot should have a unique rest pose (either an assigned RestPose transform or captured by that component).\n- If rest pose is captured on enable, consider enabling 'Auto Select Wait One Frame' below.")]
	public List<ClipboardToolSlot> slots;

	[Tooltip("If true, on Awake this selector will search its children (including inactive) for ClipboardToolSlot components and populate 'Slots' if the list is empty.\nSafe default: true.\nDisable if you want strict manual list control.")]
	public bool autoFindSlotsInChildrenIfEmpty;

	[Header("Movement / Animation")]
	[Tooltip("If true, tool transitions (rest <-> selected) will animate over time.\nIf false, tools snap instantly to target poses.\nSafe default: true.")]
	public bool animateTransitions;

	[Tooltip("Duration in seconds for tool transitions when 'Animate Transitions' is true.\nThis uses unscaled time so it remains responsive during pause/slow motion.\nSafe range: 0.05 - 0.5.")]
	[Min(0.01f)]
	public float transitionSeconds;

	[Tooltip("Easing curve for the base transition interpolation.\n\nInput:\n- X: normalized time (0..1)\n- Y: interpolation factor (0..1)\n\nRecommended:\n- EaseInOut(0,0 -> 1,1) for smooth motion.")]
	public AnimationCurve transitionCurve;

	[Header("Transition Depth Pop (Anti-Clipping)")]
	[Tooltip("If true, applies an additional local-space +Z offset during transitions to reduce visual clipping.\nThis offset is added on top of the normal interpolated localPosition.\n\nTypical setup:\n- Enable this.\n- Set Depth Pop Amount to a small value (e.g., 0.01 to 0.05 depending on scale).\n- Set Depth Pop Curve to peak near the midpoint (e.g., 0 at t=0, 1 at t=0.5, 0 at t=1).\n\nNotes:\n- Offset is applied in the tool's local space (positive local Z).\n- If your clipboard faces another axis, rotate the tool parent so local +Z corresponds to 'toward the player'.")]
	public bool enableDepthPopDuringTransition;

	[Tooltip("Maximum additional local-space Z offset (units) applied during transitions.\nThe final depth offset is: DepthPopAmount * DepthPopCurve(t).\n\nExamples:\n- 0.02 means the tool can pop forward by up to 2 cm (if 1 unit = 1 meter).\n- Keep small to avoid noticeable 'float'.")]
	[Min(0f)]
	public float depthPopAmount;

	[Tooltip("Curve controlling how much of Depth Pop Amount is applied over the transition.\n\nInput:\n- X: normalized transition progress (0..1)\n- Y: multiplier (typically 0..1, but can exceed 1 if you want more than Depth Pop Amount)\n\nRecommended anti-clipping shape:\n- (0,0) -> (0.5,1) -> (1,0)\n\nSafe examples:\n- A 'tent' curve peaking at the midpoint.\n- A smooth bell curve (EaseInOut up then down).")]
	public AnimationCurve depthPopCurve;

	[Header("Selection")]
	[Tooltip("If true, automatically selects a slot the FIRST time this ClipboardToolSelector becomes enabled for this prefab instance.\n\nImportant:\n- Once-per-instance behavior: disabling and re-enabling will NOT auto-select again.\n- Auto-select is deferred (see delay settings below) so tool slots can initialize/capture rest poses.\n\nRecommended:\n- Enabled, so the player always has an active tool and sees which tool is selected.")]
	public bool autoSelectOnFirstEnable;

	[Tooltip("Index into 'Slots' to auto-select when Auto Select On First Enable is true.\nDefault 0 selects the first tool.\nClamped into range at runtime.")]
	public int autoSelectIndex;

	[Tooltip("If true, the auto-select waits one frame after OnEnable before selecting.\nThis is a robust default because it gives child components (ClipboardToolSlot) a chance to run Awake/OnEnable and capture rest poses.\n\nRecommended:\n- True for prefab-heavy setups and pooled objects.\n- False only if you know all rest poses are already valid immediately on enable.")]
	public bool autoSelectWaitOneFrame;

	[Tooltip("Additional unscaled-time delay (seconds) before the auto-select occurs on first enable.\nThis is useful if your clipboard enable sequence includes other animations or late initialization.\n\nNotes:\n- Uses unscaled time.\n- Set to 0 for no extra delay.\nSafe range: 0.0 - 0.25")]
	[Min(0f)]
	public float autoSelectDelaySeconds;

	[Tooltip("If true, hovering a selected tool will still apply hover visuals.\nIf false, selected visuals remain and hover visuals are suppressed for the selected tool.")]
	public bool allowHoverVisualsOnSelectedTool;

	[Header("Map Cursor Override Behavior (Optional)")]
	[Tooltip("If true, when a tool requests a map cursor override, the override is applied to Hover state (map cursor when hovering the map).\nThis uses InteractableRuntimeCursorOverride.SetHoverOverride(texture).\n\nNotes:\n- Cursor textures are always centered; hotspots are not supported.")]
	public bool applyHoverCursorOverride;

	[Tooltip("If true, when a tool requests a map cursor override, the override is applied to Grab state as well (map cursor while dragging).\nThis uses InteractableRuntimeCursorOverride.SetGrabOverride(texture).\n\nRecommended:\n- true if you want the tool cursor to stay consistent during drag.\n- false if you want grab to always use the map's own grab override (Interactable cursorGrabOverride).\n\nNotes:\n- Cursor textures are always centered; hotspots are not supported.")]
	public bool applyGrabCursorOverride;

	[Header("Cursor Override Reliability (Optional)")]
	[Tooltip("If true, after auto-selecting on first enable the selector will re-try applying the map cursor override for a short time.\n\nWhy this exists:\n- In prefab enable order, 'mapCursorOverride' may not be assigned/enabled yet when auto-select runs.\n- Manual selection later works because everything is initialized by then.\n\nBehavior:\n- Only re-applies while there is a CurrentSelected slot.\n- Stops automatically after 'Cursor Override Retry Seconds'.\n\nSafe default: true.")]
	public bool reapplyCursorOverrideAfterAutoSelect;

	[Tooltip("How long (unscaled seconds) to keep retrying cursor override application after the first-enable auto-select.\n\nNotes:\n- Uses unscaled time.\n- Keep small; this is only to bridge initialization order.\nSafe range: 0.05 - 1.0")]
	[Min(0f)]
	public float cursorOverrideRetrySeconds;

	[Header("Events")]
	[Tooltip("Fires whenever the active tool actually changes (i.e. CurrentSelected moves from one slot to a different slot, including the very first auto-select on enable, which counts as a change from 'no tool' to a tool).\n\nPayload:\n- The ClipboardToolSlot that is now selected (never null when invoked).\n\nDoes NOT fire:\n- When SelectTool is called again with the already-selected slot (visuals are simply refreshed).\n- On hover enter/exit (NotifyToolHoverEnter/Exit); this event is selection-only.\n\nTypical use:\n- Wire to update diegetic UI, play a click/whoosh sound, or notify other systems (e.g. a HUD label) of the current tool.")]
	public ToolChangedEvent onToolChanged;

	[Header("Diagnostics")]
	[Tooltip("If true, emits debug logs for hover changes, selection, pose moves, and cursor override requests.\nSafe to leave off in production.")]
	public bool debugLogs;

	private readonly Dictionary<ClipboardToolSlot, TransitionState> _transitions;

	private bool _didAutoSelectOnceThisInstance;

	private Coroutine _autoSelectRoutine;

	private Coroutine _cursorRetryRoutine;

	public ClipboardToolSlot CurrentSelected { get; private set; }

	public ClipboardToolSlot CurrentHovered { get; private set; }

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	[Tooltip("Re-applies the map cursor override for the current selected tool (if any).\n\nUse cases:\n- If another script assigns 'Map Cursor Override' at runtime after this selector has already enabled.\n- If you want an explicit refresh after swapping the map interactable.")]
	public void RefreshMapCursorOverride()
	{
	}

	[IteratorStateMachine(typeof(_003CAutoSelectOnFirstEnableRoutine_003Ed__40))]
	private IEnumerator AutoSelectOnFirstEnableRoutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CCursorOverrideRetryRoutine_003Ed__41))]
	private IEnumerator CursorOverrideRetryRoutine(float retrySeconds)
	{
		return null;
	}

	private void Update()
	{
	}

	[Tooltip("Call when a tool is hovered (for example from LookAtTarget.onLookAt).\nThis updates hover visuals for that slot.")]
	public void NotifyToolHoverEnter(ClipboardToolSlot slot)
	{
	}

	[Tooltip("Call when a tool is un-hovered (for example from LookAtTarget.onLookAway).\nThis clears hover visuals for that slot.")]
	public void NotifyToolHoverExit(ClipboardToolSlot slot)
	{
	}

	[Tooltip("Selects the provided tool slot.\nTypical use:\n- Wire LookAtTarget.onClickDown (or onClickUp) to this method, passing the slot.\n\nEffects:\n- Moves previous selected back to its rest pose.\n- Moves new selected to selectedAnchor pose.\n- Updates visuals.\n- Updates MapMarkerPlacer active prefab.\n- Optionally requests cursor override on the map.\n- Invokes 'On Tool Changed' if this selection is different from the current one.")]
	public void SelectTool(ClipboardToolSlot slot)
	{
	}

	private bool IsHoverSuppressedBySelection(ClipboardToolSlot slot)
	{
		return false;
	}

	private void RefreshVisualsForAll()
	{
	}

	private void MoveSlotToSelectedAnchor(ClipboardToolSlot slot)
	{
	}

	private void MoveSlotToRest(ClipboardToolSlot slot)
	{
	}

	private void MoveSlot(ClipboardToolSlot slot, Vector3 targetPos, Quaternion targetRot, Vector3 targetScale)
	{
	}

	private void ApplyMapCursorOverrideForSelection(ClipboardToolSlot slot)
	{
	}
}
