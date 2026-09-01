using UnityEngine;
using UnityEngine.Events;

namespace TrajectorySystem
{
	[DisallowMultipleComponent]
	public sealed class TrajectoryTarget : MonoBehaviour
	{
		[Header("Reset")]
		[Tooltip("Default LOCAL position to reset this trajectory target to when a reset is requested.\n\nLOCAL means relative to this object's current parent.\n\nTips:\n- When first added, Reset() captures the current localPosition.\n- If you want a different default, set it here explicitly.\n- If you re-parent this object at runtime, ensure this default still makes sense under the new parent.")]
		[SerializeField]
		private Vector3 defaultLocalPosition;

		[Tooltip("Maximum speed (in LOCAL units per second) at which this target moves while resetting toward Default Local Position.\n\nRules:\n- 0 disables smoothing and snaps instantly to Default Local Position.\n- This speed is applied to the full localPosition vector (not per-axis).\n\nSafe examples:\n- 2.0 = slow visible slide.\n- 10.0 = quick reset.")]
		[SerializeField]
		private float maxResetSpeed;

		[Header("Follow Offset (Owned by Target)")]
		[Tooltip("Per-target LOCAL offset applied when a follower drives this target.\n\nMeaning:\n- The follower computes a desired local position based on the follower's world position expressed in this target's PARENT space.\n- This offset is then ADDED (in that same parent-local space) before axis constraints are applied.\n\nWhy LOCAL:\n- Keeps alignment correct when the parent is rotated (your use case).\n\nSafe examples:\n- (0, 0, 0) = no offset.\n- (0.25, 0, -0.5) = shift slightly right and back in the parent's local frame.")]
		[SerializeField]
		private Vector3 followLocalOffset;

		[Header("Gizmos")]
		[Tooltip("If enabled, draws gizmos in the Scene view to visualize the Default Local Position and the Follow Offset.\n\nWhat is drawn:\n- A small sphere at the Default Local Position (converted to world).\n- A small sphere at (Default Local Position + Follow Local Offset) (converted to world).\n- A line between them.\n\nThis is editor visualization only and has no runtime cost in builds.")]
		[SerializeField]
		private bool drawGizmos;

		[Tooltip("Color used for the Default Local Position gizmo sphere.\n\nTip:\n- Pick a high-contrast color for your scene background.")]
		[SerializeField]
		private Color gizmoDefaultColor;

		[Tooltip("Color used for the Offset position gizmo sphere and the line between default and offset.\n\nTip:\n- Use a different color than Default to clearly see the offset direction.")]
		[SerializeField]
		private Color gizmoOffsetColor;

		[Tooltip("Radius (in world units) of gizmo spheres.\n\nSafe examples:\n- 0.03 for small targets.\n- 0.10 for large scenes.\n\nNote:\n- This does not scale with the object; it is a constant world-space size.")]
		[SerializeField]
		private float gizmoSphereRadius;

		[Header("Events")]
		[Tooltip("Invoked when the target is successfully claimed (locked) by a follower.")]
		public UnityEvent OnTargetLocked;

		[Tooltip("Invoked when the target loses its follower (either via Release or because the follower was destroyed/removed).")]
		public UnityEvent OnTargetLost;

		[Tooltip("Invoked when target reset to default position is requested (both smooth and snap).")]
		public UnityEvent OnResetRequested;

		private bool isResetting;

		public bool IsClaimed { get; private set; }

		public Object CurrentOwner { get; private set; }

		public Vector3 FollowLocalOffset => default(Vector3);

		private void Reset()
		{
		}

		private void Update()
		{
		}

		public bool TryClaim(Object owner)
		{
			return false;
		}

		public void Release(Object owner)
		{
		}

		public void RequestResetToDefault()
		{
		}

		public void SnapResetToDefault()
		{
		}

		public void SetDefaultLocalPositionToCurrent()
		{
		}
	}
}
