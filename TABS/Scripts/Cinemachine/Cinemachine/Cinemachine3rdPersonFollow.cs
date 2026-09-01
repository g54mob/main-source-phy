using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class Cinemachine3rdPersonFollow : CinemachineComponentBase
	{
		[Tooltip("How responsively the camera tracks the target.  Each axis (camera-local) can have its own setting.  Value is the approximate time it takes the camera to catch up to the target's new position.  Smaller values give a more rigid effect, larger values give a squishier one")]
		public Vector3 Damping;

		[Header("Rig")]
		[Tooltip("Position of the shoulder pivot relative to the Follow target origin.  This offset is in target-local space")]
		public Vector3 ShoulderOffset;

		[Tooltip("Vertical offset of the hand in relation to the shoulder.  Arm length will affect the follow target's screen position when the camera rotates vertically")]
		public float VerticalArmLength;

		[Tooltip("Specifies which shoulder (left, right, or in-between) the camera is on")]
		[Range(0f, 1f)]
		public float CameraSide;

		[Tooltip("How far baehind the hand the camera will be placed")]
		public float CameraDistance;

		[Header("Obstacles")]
		[Tooltip("Camera will avoid obstacles on these layers")]
		public LayerMask CameraCollisionFilter;

		[TagField]
		[Tooltip("Obstacles with this tag will be ignored.  It is a good idea to set this field to the target's tag")]
		public string IgnoreTag = string.Empty;

		[Tooltip("Specifies how close the camera can get to obstacles")]
		public float CameraRadius;

		private Vector3 PreviousFollowTargetPosition;

		private float PreviousHeadingAngle;

		public override bool IsValid
		{
			get
			{
				if (base.enabled)
				{
					return base.FollowTarget != null;
				}
				return false;
			}
		}

		public override CinemachineCore.Stage Stage => CinemachineCore.Stage.Body;

		private void OnValidate()
		{
			CameraSide = Mathf.Clamp(CameraSide, -1f, 1f);
			Damping.x = Mathf.Max(0f, Damping.x);
			Damping.y = Mathf.Max(0f, Damping.y);
			Damping.z = Mathf.Max(0f, Damping.z);
			CameraRadius = Mathf.Max(0.001f, CameraRadius);
		}

		private void Reset()
		{
			CameraCollisionFilter = 1;
			ShoulderOffset = new Vector3(0.5f, -0.4f, 0f);
			VerticalArmLength = 0.4f;
			CameraSide = 1f;
			CameraDistance = 2f;
			Damping = new Vector3(0.1f, 0.5f, 0.3f);
			CameraRadius = 0.2f;
		}

		public override float GetMaxDampTime()
		{
			return Mathf.Max(Damping.x, Mathf.Max(Damping.y, Damping.z));
		}

		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (IsValid)
			{
				if (!base.VirtualCamera.PreviousStateIsValid)
				{
					deltaTime = -1f;
				}
				PositionCamera(ref curState, deltaTime);
			}
		}

		private void PositionCamera(ref CameraState curState, float deltaTime)
		{
			Vector3 followTargetPosition = base.FollowTargetPosition;
			Vector3 vector = ((deltaTime >= 0f) ? PreviousFollowTargetPosition : followTargetPosition);
			Vector3 vector2 = Quaternion.Inverse(curState.RawOrientation) * (followTargetPosition - vector);
			if (deltaTime >= 0f)
			{
				vector2 = base.VirtualCamera.DetachedFollowTargetDamp(vector2, Damping, deltaTime);
			}
			vector2 = vector + curState.RawOrientation * vector2;
			Vector3 forward = Vector3.forward;
			Vector3 up = Vector3.up;
			Vector3 vector3 = base.FollowTargetRotation * forward;
			float num = UnityVectorExtensions.SignedAngle(forward, vector3.ProjectOntoPlane(up), up);
			float num2 = ((deltaTime >= 0f) ? PreviousHeadingAngle : num);
			float angle = num - num2;
			PreviousHeadingAngle = num;
			vector2 = followTargetPosition + Quaternion.AngleAxis(angle, up) * (vector2 - followTargetPosition);
			PreviousFollowTargetPosition = vector2;
			GetRigPositions(out var root, out var _, out var hand);
			hand = PullTowardsStartOnCollision(in root, in hand, in CameraCollisionFilter, CameraRadius * 1.05f);
			Vector3 vector4 = (curState.RawPosition = PullTowardsStartOnCollision(in hand, hand - vector3 * CameraDistance, in CameraCollisionFilter, CameraRadius));
			curState.RawOrientation = base.FollowTargetRotation;
			curState.ReferenceLookAt = vector4 + 1000f * (base.FollowTargetRotation * Vector3.forward);
			curState.ReferenceUp = up;
		}

		public void GetRigPositions(out Vector3 root, out Vector3 shoulder, out Vector3 hand)
		{
			root = PreviousFollowTargetPosition;
			Vector3 vector = Vector3.Lerp(Vector3.Reflect(ShoulderOffset, Vector3.right), ShoulderOffset, CameraSide);
			Vector3 vector2 = new Vector3(0f, VerticalArmLength, 0f);
			shoulder = root + Quaternion.AngleAxis(PreviousHeadingAngle, Vector3.up) * vector;
			hand = shoulder + base.FollowTargetRotation * vector2;
		}

		private Vector3 PullTowardsStartOnCollision(in Vector3 rayStart, in Vector3 rayEnd, in LayerMask filter, float radius)
		{
			Vector3 dir = rayEnd - rayStart;
			if (!RuntimeUtility.SphereCastIgnoreTag(rayStart, radius, dir, out var hitInfo, dir.magnitude, filter, in IgnoreTag))
			{
				return rayEnd;
			}
			return hitInfo.point + hitInfo.normal * radius;
		}
	}
}
