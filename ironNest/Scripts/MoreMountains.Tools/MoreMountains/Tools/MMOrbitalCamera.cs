using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Camera/MMOrbitalCamera")]
	public class MMOrbitalCamera : MonoBehaviour
	{
		public enum Modes
		{
			Mouse = 0,
			Touch = 1
		}

		[Header("Setup")]
		public Modes Mode;

		public Transform Target;

		public Vector3 TargetOffset;

		[MMReadOnly]
		public float DistanceToTarget;

		[Header("Rotation")]
		public bool RotationEnabled;

		public Vector2 RotationSpeed;

		public int MinVerticalAngleLimit;

		public int MaxVerticalAngleLimit;

		[Header("Zoom")]
		public bool ZoomEnabled;

		public float MinimumZoomDistance;

		public float MaximumZoomDistance;

		public int ZoomSpeed;

		public float ZoomDampening;

		[Header("Mouse Zoom")]
		public float MouseWheelSpeed;

		public float MaxMouseWheelClamp;

		[Header("Steps")]
		public float StepThreshold;

		public UnityEvent StepFeedback;

		protected float _angleX;

		protected float _angleY;

		protected float _currentDistance;

		protected float _desiredDistance;

		protected Quaternion _currentRotation;

		protected Quaternion _desiredRotation;

		protected Quaternion _rotation;

		protected Vector3 _position;

		protected float _scrollWheelAmount;

		protected float _stepBuffer;

		protected virtual void Start()
		{
		}

		public virtual void Initialization()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void Rotation()
		{
		}

		protected virtual void StepDetection()
		{
		}

		protected virtual void Zoom()
		{
		}

		protected virtual void ApplyMovement()
		{
		}
	}
}
