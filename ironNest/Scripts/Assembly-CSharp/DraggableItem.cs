using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class DraggableItem : MonoBehaviour, ICursorDraggable
{
	public enum ItemLocation
	{
		Deck = 0,
		Surface = 1,
		Slot = 2
	}

	public enum DragAnchorMode
	{
		[Tooltip("On drag start the object pivot snaps under the cursor.\nEliminates cursor drift during surface handoff.")]
		PivotUnderCursor = 0,
		[Tooltip("Remembers the offset between cursor and pivot at grab time.\nObject does not snap/center under the cursor.")]
		PreserveGrabOffset = 1
	}

	public enum EjectAxis
	{
		[Tooltip("Eject along the surface's local +X axis (transform.right).")]
		PositiveX = 0,
		[Tooltip("Eject along the surface's local -X axis (-transform.right).")]
		NegativeX = 1,
		[Tooltip("Eject along the surface's local +Y axis (transform.up).")]
		PositiveY = 2,
		[Tooltip("Eject along the surface's local -Y axis (-transform.up).")]
		NegativeY = 3
	}

	[CompilerGenerated]
	private sealed class _003CLerpScaleRoutine_003Ed__94 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public DraggableItem _003C_003E4__this;

		public Vector3 from;

		public Vector3 to;

		private float _003Ct_003E5__2;

		private float _003Cdur_003E5__3;

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
		public _003CLerpScaleRoutine_003Ed__94(int _003C_003E1__state)
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
	private sealed class _003CSlideCoroutine_003Ed__105 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DraggableItem _003C_003E4__this;

		public DragSurface surf;

		public Vector3 target;

		public float duration;

		private Transform _003CexpectedParent_003E5__2;

		private Vector3 _003CstartLocal_003E5__3;

		private Vector3 _003CtargetLocal_003E5__4;

		private Vector3 _003ClocalNormal_003E5__5;

		private float _003Celapsed_003E5__6;

		private float _003Cdur_003E5__7;

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
		public _003CSlideCoroutine_003Ed__105(int _003C_003E1__state)
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

	[Header("State - Runtime")]
	[Tooltip("Where this item currently lives.\n\nDeck:    Held in a DraggableItemDeckArea.\nSurface: Freely placed on a DragSurface.\nSlot:    Locked into an ItemSlot.")]
	public ItemLocation CurrentLocation;

	[Tooltip("True while the player is actively dragging this item. Read-only at runtime.")]
	public bool IsBeingDragged;

	[Tooltip("Whether the Universal Pick-Up hover tooltip (ClipboardPickUpHandler's 'Tooltip' field) is allowed to display for THIS item.\n\nThis is a purely visual switch:\n- true  : tooltip shows on hover for this item.\n- false : tooltip never shows for this item, but pick-up itself is completely unaffected — the item can still be hovered and picked up normally (default).\n\nNo tokens/codes — plain on/off switch.\n\nSafe default: false — enable per-item on the items you want a pick-up tooltip for.")]
	public bool showPickupTooltip;

	[Header("References - Runtime")]
	[Tooltip("The deck area this item belongs to when in the Deck state.\nSet automatically by DraggableItemDeckArea on spawn.")]
	public DraggableItemDeckArea deckRef;

	[Tooltip("The 'home' DragSurface for this item.\n\nUsed as:\n- The surface the item starts from when dragged out of a deck.\n- The fallback surface if no other surface is under the pointer on release.\n\nSet automatically by DraggableItemDeckArea on spawn, or assign manually.")]
	public DragSurface surfaceRef;

	[Tooltip("All candidate ItemSlots this item can be placed into.\n\nOn drop, the FIRST slot in this list that overlaps the item is used.\nThis allows one item to be valid in multiple slots (e.g. two record players).\n\nSet via SetReferences() or assign directly in the Inspector.\nUse the SlotRef convenience property to read the currently occupied slot.")]
	public List<ItemSlot> slotRefs;

	[Tooltip("OPTIONAL fallback slot-discovery mechanism for items placed via code\n(e.g. Instantiate()'d at runtime), where there is no Inspector to drag an\nItemSlot reference into ahead of time.\n\nFormat rules:\n- Plain text field (Unity has no built-in tag-picker attribute for scripts),\n  so this must be typed to EXACTLY match a tag defined in\n  Project Settings > Tags and Layers, including capitalisation.\n- This is a single Unity Tag, not a list — it is compared against each\n  candidate ItemSlot's GameObject.tag using a plain string comparison\n  (never throws, even if the tag no longer exists or was mistyped —\n  it will simply fail to match).\n- Leave EMPTY (default) to fully disable this feature and match the\n  original, slotRefs-only behaviour exactly.\n\nResolution order at drop time:\n1. slotRefs is checked first, in list order, exactly as before.\n2. Only if none of those overlap AND this field is non-empty, every\n   enabled ItemSlot in the scene (ItemSlot.AllSlots) whose tag matches\n   this value is checked, in no guaranteed order, first overlap wins.\n3. Any slot found via step 2 is automatically added to slotRefs, so\n   SlotRef / RemoveSlotRef keep working normally afterwards.\n\nSafe examples: \"TurretMountSlot\", \"PunchcardSlot\".\n\nSafe default: \"\" (empty — feature disabled).")]
	public string dynamicSlotTag;

	[Tooltip("The Collider used for pointer hit-testing and overlap checks.\n\nSupported types:\n- BoxCollider     : full support, no restrictions.\n- MeshCollider    : must be marked Convex for overlap checks (deck/slot detection)\n                    to work correctly. Non-convex meshes are silently ignored by\n                    Unity's Physics.ComputePenetration.\n- Any other Collider type (SphereCollider, CapsuleCollider, etc.) is also supported.\n\nAuto-fetched from this GameObject in Awake if left unassigned.\nIt is the designer's responsibility to ensure the correct collider is present.")]
	public Collider Col;

	[Header("Events")]
	[Tooltip("Fired when the player picks this item up (drag begins).")]
	public UnityEvent OnPickedUpByPlayer;

	[Tooltip("Fired when the player releases this item (drag ends).")]
	public UnityEvent OnReleasedByPlayer;

	[Tooltip("Fired when the player picks up item via ClipboardPickUpHandler.")]
	public UnityEvent OnPickedUpToClipboard;

	[Tooltip("Fired when this item is successfully placed into an ItemSlot.\n\nThe ItemSlot's GameObject is passed as the argument so listeners can\nidentify which slot was entered if multiple slots are in use.\n\nFired AFTER CurrentLocation is set to Slot and the slot's own\nonItemAdded / onSlotFilled events have fired.")]
	public UnityEvent<GameObject> OnSlottedIntoSlot;

	[Tooltip("Fired when this item is removed from an ItemSlot (player drags it out).\n\nThe ItemSlot's GameObject is passed as the argument so listeners can\nidentify which slot was exited if multiple slots are in use.\n\nFired AFTER the slot's own onItemRemoved / onSlotCleared events have fired\nand BEFORE the drag position is updated for the new frame.")]
	public UnityEvent<GameObject> OnRemovedFromSlot;

	[Header("Drag Anchor")]
	[Tooltip("Controls how the item follows the cursor during drag.\n\nPivotUnderCursor:\n- Item pivot snaps under the cursor on grab.\n- Recommended: eliminates drift when handing off between surfaces.\n\nPreserveGrabOffset:\n- Remembers the grab offset so the item does not jump to cursor center.\n\nSafe default: PivotUnderCursor.")]
	public DragAnchorMode dragAnchorMode;

	[Header("Drag")]
	[Tooltip("Lift (world units) applied along the surface normal while dragging.\nIf 0 and useSurfaceDefaultLift is true, the active surface's defaultDragLift is used.\n\nSafe default: 0.02.")]
	public float dragLift;

	[Tooltip("If true and dragLift is exactly 0, uses the active DragSurface's defaultDragLift.\n\nSafe default: true.")]
	public bool useSurfaceDefaultLift;

	[Tooltip("Lerp speed used to smoothly follow the pointer position during drag.\nHigher = snappier. Set to 0 for instant snap.\n\nSafe default: 22.")]
	public float dragFollowSpeed;

	[Tooltip("Minimum pointer movement in pixels required while in a deck before the item\nis pulled out onto the surface.\n\nSafe default: 4.")]
	public float pullThresholdPixels;

	[Header("Stacking Offset (Anti Z-Clipping)")]
	[Tooltip("If true, this item participates in the stacking offset system — both as a\nreceiver (offsets itself when dropped onto others) and as a donor (other items\nuse this item's stackingOffsetDonated when landing on top of it).\n\nAlso applied at the end of eject slide animations, so ejected cards that land\non top of already-settled items are correctly offset.\n\nDisable on thick 3D objects whose geometry does not produce visible Z-fighting.\nItems with this disabled are fully invisible to the stacking system in both\ndirections — they neither offset themselves nor donate a step to others.\n\nSafe default: true.")]
	public bool enableStackingOffset;

	[Tooltip("Gap (world units) this item donates to whatever is placed on top of it.\n\nWhen another item is dropped and detects this item as its highest neighbour,\nit uses THIS value (not its own) as the stacking increment:\n\n  droppedItem.StackingNormalOffset = thisItem.StackingNormalOffset\n                                   + thisItem.stackingOffsetDonated\n\nThis lets tall or thick objects declare a larger clearance gap so items\nplaced on top of them are never buried inside the geometry.\n\nNegative = toward camera on a Forward-normal surface.\n\nSafe default: -0.001.")]
	public float stackingOffsetDonated;

	[Tooltip("Base radius (world units, at surface scale 1.0) used to detect overlapping\nneighbours for stacking.\n\nAutomatically scaled by the active surface's surfaceScaleMultiplier, so items\non a 0.4× clipboard surface use a proportionally smaller detection bubble.\n\nUses a flat 2D distance check projected onto the surface plane.\nSet to approximately half the width/diagonal of your largest card at scale 1.0.\n\nSafe default: 0.12.")]
	public float stackingDetectionRadius;

	[Header("Rotation")]
	[Tooltip("If true, the item's rotation matches the active DragSurface's transform.rotation\nwhile dragging and on release.\n\nSafe default: true.")]
	public bool matchSurfaceRotation;

	[Tooltip("If true, surface rotation matching is smoothly slerped during drag\nand during surface transitions.\n\nSafe default: true.")]
	public bool smoothSurfaceRotation;

	[Tooltip("Slerp speed for smooth surface rotation matching.\n\nSafe default: 18.")]
	public float surfaceRotationLerpSpeed;

	[Header("Scale")]
	[Tooltip("If true, applies the active DragSurface's surfaceScaleMultiplier to this item.\nEffective scale = base prefab scale * surface.surfaceScaleMultiplier.\n\nSafe default: true.")]
	public bool useSurfaceScaleMultiplier;

	[Tooltip("If true, scale transitions are smoothed when switching between surfaces.\n\nSafe default: true.")]
	public bool smoothSurfaceScale;

	[Tooltip("Duration in seconds for smooth scale transitions between surfaces.\n\nSafe default: 0.18.")]
	public float surfaceScaleTransitionDuration;

	[Header("Surface Handoff")]
	[Tooltip("If true, this item can hand off to any DragSurface in the scene while dragging.\nThe best surface under the pointer is chosen each frame (closest hit wins).\n\nSafe default: true.")]
	public bool enableSurfaceHandoff;

	[Tooltip("Cooldown in seconds after a surface handoff before another is allowed.\nPrevents rapid flip-flopping when surface colliders overlap.\n\nSafe default: 0.10.")]
	public float handoffCooldownSeconds;

	[Tooltip("Maximum ray distance in world units used for surface pointer-over tests.\n\nSafe default: 1000.")]
	public float handoffRaycastMaxDistance;

	[Header("Eject Slide")]
	[Tooltip("Constant lift (world units) applied along the surface normal for the entire\nduration of an eject slide animation.\n\nThe card travels at this height above the surface so it passes over the top\nof any settled items without clipping through them.\n\nThe lift is removed instantly when the card reaches its destination — the\nstacking offset system then places it at the correct final height, taking\nany cards already resting at the landing position into account.\n\nNegative values lift toward the camera on a Forward-normal surface.\nSet to 0 to disable the travel lift entirely.\n\nSafe default: -0.015.")]
	public float ejectSlideLift;

	[Header("Debug")]
	[Tooltip("If true, logs drag lifecycle events, surface handoffs, stacking offset\ncalculations, and eject-slide completion to the Console.\n\nSafe default: false.")]
	public bool debugDrag;

	private Vector3 _baseLocalScale;

	[NonSerialized]
	public float StackingNormalOffset;

	[NonSerialized]
	public bool IsSliding;

	private float _dragStackingOffset;

	private bool _externallyControlled;

	private Camera _dragCamera;

	private Vector3 _grabOffsetWorld;

	private Plane _activePlane;

	private float _handoffCooldownRemaining;

	private DragSurface _activeSurface;

	private bool _leftDeckThisDrag;

	private Vector2 _lastScreenPos;

	private Coroutine _scaleRoutine;

	private VirtualCursor _cachedVirtualCursor;

	private const EjectAxis k_DefaultEjectAxis = EjectAxis.NegativeX;

	private const float k_DefaultEjectDistance = 0.8f;

	private const float k_DefaultEjectDistanceRandomness = 0.4f;

	private const float k_DefaultSpreadAmount = 0.15f;

	private const float k_DefaultEjectSlideDuration = 0.35f;

	public bool IsDragging => false;

	public ItemSlot SlotRef => null;

	public Vector3 BaseLocalScale => default(Vector3);

	public event Action DragStarted
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

	public event Action DragEnded
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

	private void OnDisable()
	{
	}

	public void SetReferences(DragSurface surface, DraggableItemDeckArea deck, ItemSlot slot)
	{
	}

	public void SetReferences(DragSurface surface, DraggableItemDeckArea deck, List<ItemSlot> slots)
	{
	}

	public void AddSlotRef(ItemSlot slot)
	{
	}

	public void RemoveSlotRef(ItemSlot slot)
	{
	}

	public void SetState(ItemLocation newLoc, DraggableItemDeckArea deck, DragSurface surface, ItemSlot slot)
	{
	}

	[ContextMenu("Resnapshot Base Scale")]
	public void ResnapshotBaseScale()
	{
	}

	public void ApplySurfaceScaleForSurface(DragSurface surf, bool smooth = false)
	{
	}

	public void BeginDragFromManager(Camera raycastCamera, Vector2 screenPos)
	{
	}

	public void EndDragFromManager()
	{
	}

	public void HandlePickedUpToClipboard()
	{
	}

	private void Update()
	{
	}

	private void StartDragInternal(Vector2 pressScreenPos)
	{
	}

	private void UpdateDragPosition(Vector2 screenPos)
	{
	}

	private void EndDragInternal()
	{
	}

	private ItemSlot FindFirstOverlappingSlot()
	{
		return null;
	}

	private void TrySurfaceHandoff(Vector2 screenPos)
	{
	}

	private void HandoffTo(DragSurface newSurface, Vector2 screenPos)
	{
	}

	private DragSurface FindBestSurfaceUnderPointer(Vector2 screenPos)
	{
		return null;
	}

	private void PlaceOnSurface(DragSurface surf, Vector2 screenPos, bool snap)
	{
	}

	private void CaptureGrabOffsetIfNeeded(Plane plane, Vector2 screenPos)
	{
	}

	private float ComputeDragStackingOffset(DragSurface surf)
	{
		return 0f;
	}

	private void ComputeStackingOffset(DragSurface surf)
	{
	}

	private void ApplyFinalRestingPosition(DragSurface surf)
	{
	}

	private float ResolveLift(DragSurface surf)
	{
		return 0f;
	}

	private void ApplySurfaceRotation(DragSurface surf, bool smooth)
	{
	}

	private void ApplySurfaceScale(DragSurface surf, bool smooth)
	{
	}

	[IteratorStateMachine(typeof(_003CLerpScaleRoutine_003Ed__94))]
	private IEnumerator LerpScaleRoutine(Vector3 from, Vector3 to, float duration)
	{
		return null;
	}

	private void StopAllScaleCoroutines()
	{
	}

	private void LeaveDeckToSurface()
	{
	}

	public void MoveToSurface(bool slideLeft = false, bool positionAlreadySet = false)
	{
	}

	public void MoveToSurface(bool slideLeft, bool positionAlreadySet, EjectAxis ejectAxis, float ejectDistance, float ejectDistanceRandomness, float spreadAmount, float slideDuration)
	{
	}

	public void MoveToSurface(DragSurface overrideSurface, bool slideLeft, bool positionAlreadySet, EjectAxis ejectAxis, float ejectDistance, float ejectDistanceRandomness, float spreadAmount, float slideDuration)
	{
	}

	private static void ResolveEjectAxes(DragSurface surf, EjectAxis axis, out Vector3 ejectDir, out Vector3 spreadDir)
	{
		ejectDir = default(Vector3);
		spreadDir = default(Vector3);
	}

	public void SettleOnSurface(DragSurface surf)
	{
	}

	public void MoveToDeck(DraggableItemDeckArea targetDeck)
	{
	}

	public void MoveToSlot()
	{
	}

	public void MoveToSlot(ItemSlot targetSlot)
	{
	}

	[IteratorStateMachine(typeof(_003CSlideCoroutine_003Ed__105))]
	private IEnumerator SlideCoroutine(Vector3 target, float duration, DragSurface surf)
	{
		return null;
	}

	private Vector2 GetScreenPosition()
	{
		return default(Vector2);
	}

	private VirtualCursor FindVirtualCursor()
	{
		return null;
	}

	private static float SmoothStep01(float t)
	{
		return 0f;
	}
}
