using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Gameplay/Drag Surface")]
public class DragSurface : MonoBehaviour
{
	public enum SurfaceAxis
	{
		Up = 0,
		Forward = 1,
		Right = 2
	}

	public static readonly List<DragSurface> AllSurfaces;

	[Header("Plane Definition")]
	[Tooltip("Which local axis of this transform defines the drag plane normal.\n\nThis controls BOTH:\n- The plane normal used for ray-plane intersection during drag.\n- Which axis is the 'up' of the surface for clamping and lift.\n\nExamples:\n- Flat table / map: Up\n- Vertical board / clipboard: Forward\n\nIf drag motion feels wrong, this is the first setting to check.\n\nSafe default: Up.")]
	public SurfaceAxis planeNormalAxis;

	[Tooltip("Additional offset (world units) applied along the plane normal after the base surface\npoint is calculated.\n\nUse this to nudge the drag plane slightly above the physical surface.\n\nSafe default: 0.")]
	public float additionalSurfaceOffset;

	[Header("Pointer-Over Detection")]
	[Tooltip("Collider used to detect whether the pointer ray is 'over' this surface.\n\nBehavior:\n- Tested directly via Collider.Raycast(ray,...) so other geometry does NOT block it.\n\nRecommendation:\n- Assign a thin BoxCollider aligned with the surface.\n- Can be the SAME BoxCollider as the bounds box (auto-assigned if left null).\n\nIf null:\n- Surface still supports plane math and clamping, but surface handoff\n  detection will not work.")]
	public Collider raycastTargetCollider;

	[Header("Bounds & Clamp")]
	[Tooltip("If true, item positions are clamped inside the BoxCollider footprint\nwhen they settle or are dragged.\n\nSafe default: true.")]
	public bool clampToBounds;

	[Tooltip("Inward inset (local units) subtracted from each in-plane edge when clamping.\nPrevents items from sitting exactly on the edge.\n\nSafe default: 0.01.")]
	public float clampInset;

	[Header("Drag Defaults")]
	[Tooltip("Default lift (world units) applied along this surface normal while dragging.\nDraggableItem uses this value when its own dragLift is 0 and\nuseSurfaceDefaultLift is true.\n\nSafe default: 0.02.")]
	public float defaultDragLift;

	[Header("Scale & Rotation")]
	[Tooltip("Scale multiplier applied to DraggableItems while they live on this surface.\nEffective scale = item base scale * surfaceScaleMultiplier.\n\nExamples:\n- Clipboard HUD (small): 0.4\n- Map table (full size): 1.0\n\nSafe default: 1.0.")]
	public float surfaceScaleMultiplier;

	[Tooltip("If true, DraggableItems entering this surface will align their rotation\nto match this surface's transform.rotation.\n\nSafe default: true.")]
	public bool preferAlignRotationOnEnter;

	[Header("Priority")]
	[Tooltip("Higher priority surfaces win when two surfaces are equidistant under the pointer.\nIn practice the closest-hit distance is used first; priority is only a tiebreaker.\n\nUseful example:\n- Give the player's clipboard a higher priority than world tables so it\n  always wins when the clipboard is directly in front of a table.\n\nSafe default: 0.")]
	public int handoffPriority;

	public DraggableItemDeckArea sourceDeckArea;

	[Header("Runtime")]
	[Tooltip("All DraggableItems currently registered as living on this surface.\nRead-only at runtime.")]
	public List<DraggableItem> items;

	private BoxCollider _boundsBox;

	public BoxCollider BoundsBox => null;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnValidate()
	{
	}

	public Vector3 GetPlaneNormal()
	{
		return default(Vector3);
	}

	public Vector3 GetPlaneOriginPoint()
	{
		return default(Vector3);
	}

	public Plane GetSurfacePlane()
	{
		return default(Plane);
	}

	public Vector3 ProjectOntoSurface(Vector3 worldPos)
	{
		return default(Vector3);
	}

	public Vector3 ClampOnSurface(Vector3 worldPos)
	{
		return default(Vector3);
	}

	public Vector3 ClampOnSurfacePreserveNormalOffset(Vector3 worldPos)
	{
		return default(Vector3);
	}

	public Vector3 GetSurfaceCenterWorldPosition()
	{
		return default(Vector3);
	}

	public bool IsPointerOverSurface(Camera cam, Vector2 screenPos, float maxDistance, out RaycastHit hit)
	{
		hit = default(RaycastHit);
		return false;
	}

	public void AddItem(DraggableItem item)
	{
	}

	public void RemoveItem(DraggableItem item)
	{
	}

	public void MoveItemsToDeck()
	{
	}

	public static Vector3 ProjectPointOnPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
	{
		return default(Vector3);
	}
}
