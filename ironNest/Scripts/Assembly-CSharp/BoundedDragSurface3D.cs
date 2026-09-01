using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Gameplay/Bounded Drag Surface 3D")]
public class BoundedDragSurface3D : MonoBehaviour
{
	public enum SurfaceAxis
	{
		Up = 0,
		Forward = 1,
		Right = 2
	}

	[Header("Plane Definition (Required)")]
	[SerializeField]
	[Tooltip("BoxCollider that defines BOTH:\n- Movement plane orientation (via boundsBox.transform)\n- Movement bounds / clamp extents (via BoxCollider.size and BoxCollider.center)\n\nRequired.\n\nPlane definition:\n- Plane passes through BoxCollider.center (local) converted to world.\n- Plane normal is selected by 'planeNormalAxis'.\n\nIf dragging feels like it moves on the wrong axis, verify:\n- boundsBox transform rotation\n- planeNormalAxis\n- boundsBox size/center")]
	private BoxCollider boundsBox;

	[SerializeField]
	[Tooltip("Which local axis of 'boundsBox.transform' defines the plane normal.\n\nThis affects BOTH:\n- The drag plane normal used for ray-plane intersection.\n- Which axis is considered the plane normal for clamping.\n\nExamples:\n- Flat map table: Up\n- Vertical board: Forward\n\nIf motion is wrong, this is the first setting to verify.")]
	private SurfaceAxis planeNormalAxis;

	[Header("Pointer-Over Detection (Optional but Recommended)")]
	[SerializeField]
	[Tooltip("Collider used to detect whether the pointer ray is 'over' this surface.\n\nImportant behavior:\n- This collider is ray-tested directly using Collider.Raycast(ray,...)\n  so other scene geometry does NOT block this test.\n\nRecommended:\n- A thin BoxCollider aligned with the surface.\n- Can be the SAME collider as boundsBox.\n\nIf null:\n- Surface still supports plane math + clamping,\n  but handoff detection into this surface will not work.")]
	private Collider raycastTargetCollider;

	[Header("Surface Defaults (Optional)")]
	[SerializeField]
	[Tooltip("Default lift (world units) that draggables may apply along this surface normal while dragging.\n\nThis component does not apply lift itself; it is a per-surface hint value.\n\nSafe default: 0.02")]
	private float defaultDragLift;

	[SerializeField]
	[Tooltip("Scale multiplier for objects while they are on this surface.\n\nRecommended usage pattern:\n- Draggable stores prefab-authored base localScale.\n- Effective scale on this surface = baseScale * surfaceScaleMultiplier.\n\nExamples:\n- Clipboard HUD: 0.4\n- Map table: 1.0\n\nSafe default: 1.0")]
	private float surfaceScaleMultiplier;

	[SerializeField]
	[Tooltip("If true, draggables may align their rotation to this surface when entering it.\n\nFor your current UX:\n- Cards should match surface orientation (clipboard/map) and NOT do camera-facing logic.\n\nSafe default: true")]
	private bool preferAlignRotationOnEnter;

	public BoxCollider BoundsBox => null;

	public Collider RaycastTargetCollider => null;

	public float DefaultDragLift => 0f;

	public float SurfaceScaleMultiplier => 0f;

	public bool PreferAlignRotationOnEnter => false;

	public Vector3 GetPlaneNormal()
	{
		return default(Vector3);
	}

	public Vector3 GetPlaneOriginPoint()
	{
		return default(Vector3);
	}

	public Vector3 GetSurfaceCenterWorldPosition()
	{
		return default(Vector3);
	}

	public Plane GetPlane()
	{
		return default(Plane);
	}

	public Vector3 ClampToSurfaceBounds(Vector3 worldPos)
	{
		return default(Vector3);
	}

	public Vector3 ClampToSurfaceBoundsPreserveNormalOffset(Vector3 worldPos)
	{
		return default(Vector3);
	}

	public bool IsPointerOverSurface(Camera cam, Vector2 screenPos, float maxDistance, out RaycastHit hit)
	{
		hit = default(RaycastHit);
		return false;
	}

	private void OnValidate()
	{
	}
}
