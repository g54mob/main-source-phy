using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ItemSlot : MonoBehaviour
{
	public static readonly List<ItemSlot> AllSlots;

	[Header("Runtime State")]
	[Tooltip("The DraggableItem currently held in this slot. Read-only at runtime.")]
	public DraggableItem CurrentItem;

	[Header("Behavior")]
	[Tooltip("If true, placing a new item into an occupied slot ejects the existing item\nback to the drag surface rather than rejecting the new item.")]
	public bool ejectExistingOnNewDrop;

	[Header("Anchor")]
	[Tooltip("If assigned, items placed into this slot are re-parented to this transform.\nIf null, items are re-parented to this slot's transform.")]
	public Transform itemAnchor;

	[Header("Ejection")]
	[Tooltip("OPTIONAL. If assigned, ejected items are placed onto THIS surface instead of\nrelying on the ejected item's own DraggableItem.surfaceRef.\n\nWhy this exists:\n- An item's surfaceRef only updates when the PLAYER drags it and releases it\n  directly onto a DragSurface. If this slot is filled by other code (e.g. a\n  mission script moving an item here after it settled on a different,\n  unrelated surface), the item's surfaceRef can still point at that other,\n  wrong surface.\n- This matters most for slots whose parent object moves/rotates at runtime\n  (e.g. a turret mount or clipboard clip): eject direction/distance are always\n  computed from the surface's CURRENT transform, so using the wrong (stale)\n  surface produces eject results that look completely wrong — often the same\n  fixed wrong direction every time, and a distance that looks wildly\n  exaggerated, regardless of whether this slot happens to be rotating at that\n  exact moment.\n\nAssign the DragSurface that shares this slot's own parent (its local\ntable/clipboard surface) here so ejection is always correct relative to THIS\nslot, no matter what the item's own surfaceRef currently holds.\n\nLeave EMPTY (null) to keep the original behaviour exactly: the ejected item's\nown surfaceRef is used, unchanged.\n\nSafe default: None (null — feature disabled, original behaviour).")]
	public DragSurface ejectSurfaceOverride;

	[Tooltip("Which in-plane axis of the item's DragSurface to launch the ejected item along.\n\nAssumes the DragSurface always uses planeNormalAxis = Forward, so the in-plane\naxes are the surface's local X (Right) and local Y (Up).\n\nPositiveX  : eject toward  +surface.transform.right\nNegativeX  : eject toward  -surface.transform.right  (default — 'left')\nPositiveY  : eject toward  +surface.transform.up\nNegativeY  : eject toward  -surface.transform.up\n\nThe perpendicular in-plane axis is used automatically for spread.\n\nSafe default: NegativeX.")]
	public DraggableItem.EjectAxis ejectAxis;

	[Tooltip("Base distance (world units) the ejected item travels along the eject axis.\nFinal distance = ejectDistance ± ejectDistanceRandomness.\n\nSafe default: 0.8.")]
	public float ejectDistance;

	[Tooltip("Maximum random variance (world units) added to or subtracted from ejectDistance.\nFinal distance = ejectDistance + Random.Range(-ejectDistanceRandomness, +ejectDistanceRandomness).\n\nSet to 0 for a fixed, deterministic eject distance.\n\nSafe default: 0.4.")]
	public float ejectDistanceRandomness;

	[Tooltip("Maximum random spread (world units) applied to the ejected item on the\nperpendicular in-plane axis (i.e. the axis that is NOT the eject axis).\n\nA value of 0 sends every card in a straight line; higher values fan them out.\n\nSafe default: 0.15.")]
	public float spreadAmount;

	[Tooltip("Duration in seconds for the ejected item's slide animation.\n\nSafe default: 0.35.")]
	public float ejectSlideDuration;

	[Header("Events")]
	[Tooltip("Fired when any DraggableItem is successfully placed into this slot.\nThe item's GameObject is passed as the argument.")]
	public UnityEvent<GameObject> onItemAdded;

	[Tooltip("Fired when the item is removed from this slot (by the player dragging it out,\nor by code). The item's GameObject is passed as the argument.")]
	public UnityEvent<GameObject> onItemRemoved;

	[Tooltip("Fired after all placement logic completes and the slot is confirmed occupied.\nNo argument — use onItemAdded to access the item.")]
	public UnityEvent onSlotFilled;

	[Tooltip("Fired after the slot becomes empty (item removed or cleared).")]
	public UnityEvent onSlotCleared;

	[Header("Debug")]
	[Tooltip("If true, logs slot state changes and resolved eject target positions\nto the Console.")]
	public bool debugLogs;

	private BoxCollider boxCol;

	public bool HasItem => false;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	public void PlaceItem(DraggableItem item)
	{
	}

	public void RemoveItem(DraggableItem item, bool autoEject = false)
	{
	}

	public void ClearSlot()
	{
	}

	public bool Overlaps(DraggableItem item)
	{
		return false;
	}

	private static void ResolveEjectAxes(DragSurface surf, DraggableItem.EjectAxis axis, out Vector3 ejectDir, out Vector3 spreadDir)
	{
		ejectDir = default(Vector3);
		spreadDir = default(Vector3);
	}
}
