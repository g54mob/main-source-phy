using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapPiece3D : MonoBehaviour, ICursorDraggable
{
	public enum SurfaceAxis
	{
		Up = 0,
		Forward = 1,
		Right = 2
	}

	[Header("References")]
	[Tooltip("Camera used for ray-to-plane intersection. If null at runtime, will try Camera.main.")]
	public Camera cam;

	[Tooltip("Collider used for picking hit-point (grab point).\nThis should be the collider representing THIS map piece.\nIt is used to compute the initial hit point on press.\nRequired.")]
	public Collider interactionCollider;

	[Tooltip("BoxCollider that defines:\n- movement plane orientation (via its transform)\n- movement bounds (via its local size/center)\nRequired.")]
	public BoxCollider boundsBox;

	[Header("Pointer Source")]
	[Tooltip("If assigned, this VirtualCursor is used as the screen-space pointer source.\nIf not assigned and autoFindVirtualCursor is true, the script will try to find it by tag.\nIf still not found, uses screen center as a last resort.\nRecommended: assign or tag-resolve for FreeMouse mode.")]
	[SerializeField]
	private VirtualCursor virtualCursor;

	[Tooltip("If true, attempts to find a VirtualCursor at runtime using 'virtualCursorTag' if not already assigned.\nSafe default: true.")]
	[SerializeField]
	private bool autoFindVirtualCursor;

	[Tooltip("Unity Tag used to locate the VirtualCursor instance at runtime across scenes.\nMust match a tag defined in Unity's Tag Manager.\nExample: \"VirtualCursor\"")]
	[SerializeField]
	private string virtualCursorTag;

	[Header("Surface Settings")]
	[Tooltip("Which local axis of the boundsBox defines the plane normal (table 'up').")]
	public SurfaceAxis planeAxis;

	[Header("Drag Settings")]
	[Tooltip("Lift applied along the plane normal while dragging (visual separation).")]
	public float dragLift;

	[Tooltip("Follow smoothing speed. Higher = snappier, lower = more floaty.")]
	public float dragFollowSpeed;

	[Tooltip("Pointer movement threshold (pixels) before drag updates due to pointer motion.\nCamera motion (position/rotation) always updates drag immediately when enabled.\nSet to 0 to update immediately on any pointer motion.")]
	public float pullThresholdPixels;

	[Tooltip("If true, while dragging the target updates when the camera moves (position or rotation),\neven if the pointer has not moved.\nRecommended: true (prevents perceived lag when camera moves).")]
	public bool updateWhileDraggingOnCameraMotion;

	[Tooltip("Max raycast distance used when calculating initial press hit-point on the interactionCollider.")]
	public float pickRayDistance;

	[Header("Drag Behavior Mode")]
	[Tooltip("If true, when a drag begins the piece's transform origin is immediately centered under the cursor\nand then follows the cursor directly (no grab-point offset).\nIf false, the piece preserves the original grab point offset relative to the cursor for the entire drag.")]
	public bool centerOriginOnCursorWhileDragging;

	[Header("Debug")]
	[Tooltip("If true, logs drag lifecycle and target updates.")]
	public bool debug;

	[Tooltip("If true, draws gizmos to visualize the plane and normal in the editor.")]
	public bool drawGizmos;

	[Tooltip("Gizmo color for the movement plane preview.")]
	public Color gizmoPlaneColor;

	private bool dragging;

	private bool _externallyControlled;

	private Vector3 dragOffsetWorld;

	private Vector2 lastPointerPos;

	private Plane dragPlane;

	private Vector3 lastCamPos;

	private Quaternion lastCamRot;

	public bool IsDragging => false;

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

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	public void BeginDragFromManager(Camera raycastCamera, Vector2 screenPos)
	{
	}

	public void EndDragFromManager()
	{
	}

	public void SetVirtualCursor(VirtualCursor vc)
	{
	}

	private void StartDragInternal(Vector3 hitPoint, Vector2 pressScreenPos)
	{
	}

	private void UpdateDrag(Vector2 screenPos)
	{
	}

	private void EndDragInternal()
	{
	}

	private Vector2 GetPointerScreenPosition()
	{
		return default(Vector2);
	}

	private bool TryResolveVirtualCursor(bool logWarnings = false)
	{
		return false;
	}

	private Vector3 GetPlaneNormal()
	{
		return default(Vector3);
	}

	private Vector3 GetPlaneOriginPoint()
	{
		return default(Vector3);
	}

	private static Vector3 ProjectPointOnPlane(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
	{
		return default(Vector3);
	}

	private Vector3 ClampToBoundsPlane(Vector3 worldPos)
	{
		return default(Vector3);
	}
}
