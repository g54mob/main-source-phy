using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMFaceDirection : MonoBehaviour
	{
		public enum UpdateModes
		{
			Update = 0,
			LateUpdate = 1,
			FixedUpdate = 2
		}

		public enum ForwardVectors
		{
			Forward = 0,
			Up = 1,
			Right = 2
		}

		public enum FacingModes
		{
			MovementDirection = 0,
			Target = 1
		}

		[Header("Facing Mode")]
		public FacingModes FacingMode;

		[MMEnumCondition("FacingMode", new int[] { 1 })]
		public Transform FacingTarget;

		[MMEnumCondition("FacingMode", new int[] { 0 })]
		public float MinimumMovementThreshold;

		[Header("Directions")]
		public ForwardVectors ForwardVector;

		public Vector3 DirectionRotationAngles;

		[Header("Axis Locks")]
		public bool LockXAxis;

		public bool LockYAxis;

		public bool LockZAxis;

		[Header("Timing")]
		public UpdateModes UpdateMode;

		public float InterpolationSpeed;

		protected Vector3 _direction;

		protected Vector3 _positionLastFrame;

		protected Transform _transform;

		protected Vector3 _upwards;

		protected Vector3 _targetPosition;

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void FaceDirection()
		{
		}

		protected virtual void ApplyRotation()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void FixedUpdate()
		{
		}
	}
}
