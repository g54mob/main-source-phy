using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Movement/MMSquashAndStretch")]
	public class MMSquashAndStretch : MonoBehaviour
	{
		public enum Timescales
		{
			Regular = 0,
			Unscaled = 1
		}

		public enum Modes
		{
			Rigidbody = 0,
			Rigidbody2D = 1,
			Position = 2
		}

		[MMInformation("This component will apply squash and stretch based on velocity (either position based or computed from a Rigidbody. It has to be put on an intermediary level in the hierarchy, between the logic (top level) and the model (bottom level).", MMInformationAttribute.InformationType.Info, false)]
		[Header("Velocity Detection")]
		[Tooltip("the possible ways to get velocity from")]
		public Modes Mode;

		[Tooltip("whether we should use deltaTime or unscaledDeltaTime")]
		public Timescales Timescale;

		[Header("Settings")]
		[Tooltip("the intensity of the squash and stretch")]
		public float Intensity;

		[Tooltip("the maximum velocity of your parent object, used to remap the computed one")]
		public float MaximumVelocity;

		[Header("Rescale")]
		[Tooltip("the minimum scale to apply to this object")]
		public Vector3 MinimumScale;

		[Tooltip("the maximum scale to apply to this object")]
		public Vector3 MaximumScale;

		[Tooltip("whether or not to rescale on the x axis")]
		public bool RescaleX;

		[Tooltip("whether or not to rescale on the y axis")]
		public bool RescaleY;

		[Tooltip("whether or not to rescale on the z axis")]
		public bool RescaleZ;

		[Tooltip("whether or not to rotate the transform to align with the current direction")]
		public bool RotateToMatchDirection;

		[Header("Squash")]
		[Tooltip("if this is true, the object will squash once velocity goes below the specified threshold")]
		public bool AutoSquashOnStop;

		[Tooltip("the curve to apply when squashing the object (this describes scale on x and z, will be inverted for y to maintain mass)")]
		public AnimationCurve SquashCurve;

		[Tooltip("the velocity threshold after which a squash can be triggered if the object stops")]
		public float SquashVelocityThreshold;

		[Tooltip("the maximum duration of the squash (will be reduced if velocity is low)")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 SquashDuration;

		[Tooltip("the maximum intensity of the squash")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 SquashIntensity;

		[Header("Spring")]
		[Tooltip("whether or not to add extra spring to the squash and stretch")]
		public bool Spring;

		[Tooltip("the damping to apply to the spring")]
		[MMCondition("Spring", true)]
		public float SpringDamping;

		[Tooltip("the spring's frequency")]
		[MMCondition("Spring", true)]
		public float SpringFrequency;

		[Header("Debug")]
		[MMReadOnly]
		[Tooltip("the current velocity of the parent object")]
		public Vector3 Velocity;

		[MMReadOnly]
		[Tooltip("the remapped velocity")]
		public float RemappedVelocity;

		[MMReadOnly]
		[Tooltip("the current velocity magnitude")]
		public float VelocityMagnitude;

		protected Rigidbody2D _rigidbody2D;

		protected Rigidbody _rigidbody;

		protected Transform _childTransform;

		protected Transform _parentTransform;

		protected Vector3 _direction;

		protected Vector3 _previousPosition;

		protected Vector3 _newLocalScale;

		protected Vector3 _initialScale;

		protected Quaternion _newRotation;

		protected Quaternion _deltaRotation;

		protected float _squashStartedAt;

		protected bool _squashing;

		protected float _squashIntensity;

		protected float _squashDuration;

		protected bool _movementStarted;

		protected float _lastVelocity;

		protected Vector3 _springScale;

		protected Vector3 _springVelocity;

		public virtual float TimescaleTime => 0f;

		public virtual float TimescaleDeltaTime => 0f;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void SquashAndStretch()
		{
		}

		protected virtual void ComputeVelocityAndDirection()
		{
		}

		protected virtual void ComputeNewRotation()
		{
		}

		protected virtual void ComputeNewLocalScale()
		{
		}

		protected virtual void StorePreviousPosition()
		{
		}

		public virtual void Squash(float duration, float intensity)
		{
		}
	}
}
