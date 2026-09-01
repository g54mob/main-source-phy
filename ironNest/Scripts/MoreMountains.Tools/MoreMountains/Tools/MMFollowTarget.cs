using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Movement/MMFollowTarget")]
	public class MMFollowTarget : MonoBehaviour
	{
		public enum UpdateModes
		{
			Update = 0,
			FixedUpdate = 1,
			LateUpdate = 2
		}

		public enum FollowModes
		{
			RegularLerp = 0,
			MMLerp = 1,
			MMSpring = 2
		}

		public enum PositionSpaces
		{
			World = 0,
			Local = 1
		}

		[Header("Follow Position")]
		public bool FollowPosition;

		[MMCondition("FollowPosition", true)]
		public bool FollowPositionX;

		[MMCondition("FollowPosition", true)]
		public bool FollowPositionY;

		[MMCondition("FollowPosition", true)]
		public bool FollowPositionZ;

		[MMCondition("FollowPosition", true)]
		public PositionSpaces PositionSpace;

		[Header("Follow Rotation")]
		public bool FollowRotation;

		[Header("Follow Scale")]
		public bool FollowScale;

		[MMCondition("FollowScale", true)]
		public float FollowScaleFactor;

		[Header("Target")]
		public Transform Target;

		[MMCondition("FollowPosition", true)]
		public Vector3 Offset;

		[MMCondition("FollowPosition", true)]
		public bool AddInitialDistanceXToXOffset;

		[MMCondition("FollowPosition", true)]
		public bool AddInitialDistanceYToYOffset;

		[MMCondition("FollowPosition", true)]
		public bool AddInitialDistanceZToZOffset;

		[Header("Position Interpolation")]
		public bool InterpolatePosition;

		[MMCondition("InterpolatePosition", true)]
		public FollowModes FollowPositionMode;

		[MMCondition("InterpolatePosition", true)]
		public float FollowPositionSpeed;

		[MMEnumCondition("FollowPositionMode", new int[] { 2 })]
		[Range(0.01f, 1f)]
		public float PositionSpringDamping;

		[MMEnumCondition("FollowPositionMode", new int[] { 2 })]
		public float PositionSpringFrequency;

		[Header("Rotation Interpolation")]
		public bool InterpolateRotation;

		[MMCondition("InterpolateRotation", true)]
		public FollowModes FollowRotationMode;

		[MMCondition("InterpolateRotation", true)]
		public float FollowRotationSpeed;

		[MMEnumCondition("FollowRotationMode", new int[] { 2 })]
		[Range(0.01f, 1f)]
		public float RotationSpringDamping;

		[MMEnumCondition("FollowRotationMode", new int[] { 2 })]
		public float RotationSpringFrequency;

		[Header("Scale Interpolation")]
		public bool InterpolateScale;

		[MMCondition("InterpolateScale", true)]
		public FollowModes FollowScaleMode;

		[MMCondition("InterpolateScale", true)]
		public float FollowScaleSpeed;

		[MMEnumCondition("FollowScaleMode", new int[] { 2 })]
		[Range(0.01f, 1f)]
		public float ScaleSpringDamping;

		[MMEnumCondition("FollowScaleMode", new int[] { 2 })]
		public float ScaleSpringFrequency;

		[Header("Mode")]
		public UpdateModes UpdateMode;

		public bool DisableSelfOnSetActiveFalse;

		[Header("Distances")]
		public bool UseMinimumDistanceBeforeFollow;

		public float MinimumDistanceBeforeFollow;

		public bool UseMaximumDistance;

		public float MaximumDistance;

		[Header("Anchor")]
		public bool AnchorToInitialPosition;

		[MMCondition("AnchorToInitialPosition", true)]
		public float MaxDistanceToAnchor;

		protected Vector3 _positionVelocity;

		protected Vector3 _scaleVelocity;

		protected Vector3 _rotationVelocity;

		protected Vector3 _initialPosition;

		protected Vector3 _direction;

		protected Vector3 _newPosition;

		protected Vector3 _newRotation;

		protected Vector3 _newScale;

		protected Vector3 _newTargetPosition;

		protected Quaternion _newTargetRotation;

		protected Vector3 _newTargetRotationEulerAngles;

		protected Vector3 _newTargetRotationEulerAnglesLastFrame;

		protected Vector3 _newTargetScale;

		protected float _rotationFloatVelocity;

		protected float _rotationFloatCurrent;

		protected float _rotationFloatTarget;

		protected Vector3 _currentRotationEulerAngles;

		protected Quaternion _rotationBeforeSpring;

		protected Quaternion _initialRotation;

		protected Vector3 _lastTargetPosition;

		protected bool _localSpace => false;

		protected virtual void Start()
		{
		}

		public virtual void Initialization()
		{
		}

		public virtual void StopFollowing()
		{
		}

		public virtual void StartFollowing()
		{
		}

		protected virtual void SetInitialPosition()
		{
		}

		protected virtual void SetOffset()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void FixedUpdate()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void FollowTargetPosition()
		{
		}

		protected virtual float ApplyMinMaxDistancing(float trueDistance, float interpolatedDistance)
		{
			return 0f;
		}

		protected virtual void FollowTargetRotation()
		{
		}

		protected virtual void FollowTargetScale()
		{
		}

		public virtual void ChangeFollowTarget(Transform newTarget)
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
