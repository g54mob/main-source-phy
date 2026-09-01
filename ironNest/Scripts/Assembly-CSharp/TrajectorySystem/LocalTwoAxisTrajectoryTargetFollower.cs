using UnityEngine;
using UnityEngine.Events;

namespace TrajectorySystem
{
	[DisallowMultipleComponent]
	public sealed class LocalTwoAxisTrajectoryTargetFollower : MonoBehaviour
	{
		public enum TwoAxisPlane
		{
			[Tooltip("Drive target local X and local Y. Preserve target local Z.")]
			XY = 0,
			[Tooltip("Drive target local X and local Z. Preserve target local Y.")]
			XZ = 1,
			[Tooltip("Drive target local Y and local Z. Preserve target local X.")]
			YZ = 2
		}

		[Header("Target Discovery")]
		[Tooltip("Unity Tag used to find eligible Trajectory Target GameObjects.\n\nRequirements:\n- The tag MUST exist in Project Settings > Tags and Layers.\n- Both trajectory target objects should have this same tag.\n- Each target must also have a TrajectoryTarget component.\n\nBehavior:\n- OnEnable, this script searches for objects with this tag and claims the first unclaimed target.\n- If all targets are claimed, nothing happens (safe no-op).")]
		[SerializeField]
		private string trajectoryTargetTag;

		[Header("Constraint Axes (LOCAL)")]
		[Tooltip("Which 2 LOCAL axes of the claimed trajectory target will be driven by this follower.\n\nImportant:\n- This modifies target.transform.localPosition (relative to its parent).\n- The remaining axis is NOT modified; it stays whatever it currently is in local space.\n\nTypical for your case:\n- XZ: only local X and local Z change, local Y stays unchanged.\n\nNote:\n- The target's own Follow Local Offset (configured on the TrajectoryTarget component) is applied before these axis rules.")]
		[SerializeField]
		private TwoAxisPlane constrainPlane;

		[Header("Update")]
		[Tooltip("If true, updates happen in LateUpdate. This is usually safest because it runs after most movement code.\nIf false, updates happen in Update.")]
		[SerializeField]
		private bool useLateUpdate;

		[Tooltip("If true, the script will attempt to claim a target again if it doesn't have one.\nThis is useful if the two targets spawn after this prefab, or if targets may be released later.\n\nIf false, it only attempts once in OnEnable (minimal + fastest).")]
		[SerializeField]
		private bool retryAcquireWhenMissing;

		[Tooltip("Seconds between retry attempts when retryAcquireWhenMissing is enabled.\n\nNotes:\n- This limits the cost of repeated tag searches.\n- Minimum enforced internally: 0.01 seconds.\n\nSafe examples:\n- 0.25 (default) = responsive but not too spammy.\n- 1.0 = very low overhead if targets rarely appear late.")]
		[SerializeField]
		private float retryAcquireIntervalSeconds;

		[Header("Reset Event (UI Hook)")]
		[Tooltip("UnityEvent you can hook up in the Inspector (e.g., from a UI Button) as a routing mechanism.\n\nNotes:\n- This script provides InvokeResetEvent() to invoke this event.\n- If you want this follower to request reset on its claimed target, you can add a listener to call RequestResetClaimedTarget().\n\nImportant:\n- TrajectoryTarget currently ignores reset requests while claimed (safe).")]
		[SerializeField]
		private UnityEvent onResetRequested;

		[Header("Debug (Read Only)")]
		[Tooltip("The currently claimed target (if any). For debugging/inspection only.\nDo not assign this in the Inspector.")]
		[SerializeField]
		private TrajectoryTarget claimedTarget;

		private float nextAcquireTime;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnDestroy()
		{
		}

		private void Update()
		{
		}

		private void LateUpdate()
		{
		}

		private void Tick()
		{
		}

		[ContextMenu("Try Acquire Target Now")]
		public void TryAcquireNow()
		{
		}

		[ContextMenu("Release Target Now")]
		public void Release()
		{
		}

		public void InvokeResetEvent()
		{
		}

		public void RequestResetClaimedTarget()
		{
		}

		private void DriveClaimedTargetLocal2Axis()
		{
		}
	}
}
