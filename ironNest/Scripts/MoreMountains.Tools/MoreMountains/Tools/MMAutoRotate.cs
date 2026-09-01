using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Movement/MMAutoRotate")]
	public class MMAutoRotate : MonoBehaviour
	{
		public enum UpdateModes
		{
			Update = 0,
			LateUpdate = 1,
			FixedUpdate = 2
		}

		[Header("Rotation")]
		public bool Rotating;

		[MMCondition("Rotating", true)]
		public Space RotationSpace;

		public UpdateModes UpdateMode;

		[MMCondition("Rotating", true)]
		public Vector3 RotationSpeed;

		[Header("Orbit")]
		public bool Orbiting;

		[MMCondition("Orbiting", true)]
		public bool AdditiveOrbitRotation;

		[MMCondition("Orbiting", true)]
		public Transform OrbitCenterTransform;

		[MMCondition("Orbiting", true)]
		public Vector3 OrbitCenterOffset;

		[MMCondition("Orbiting", true)]
		public Vector3 OrbitRotationAxis;

		[MMCondition("Orbiting", true)]
		public float OrbitRotationSpeed;

		[MMCondition("Orbiting", true)]
		public float OrbitRadius;

		[MMCondition("Orbiting", true)]
		public float OrbitCorrectionSpeed;

		[Header("Settings")]
		public bool DrawGizmos;

		[MMCondition("DrawGizmos", true)]
		public Color OrbitPlaneColor;

		[MMCondition("DrawGizmos", true)]
		public Color OrbitLineColor;

		[HideInInspector]
		public Vector3 _orbitCenter;

		[HideInInspector]
		public Vector3 _worldRotationAxis;

		[HideInInspector]
		public Plane _rotationPlane;

		[HideInInspector]
		public Vector3 _snappedPosition;

		[HideInInspector]
		public Vector3 _radius;

		protected Quaternion _newRotation;

		protected Vector3 _desiredOrbitPosition;

		private Vector3 _previousPosition;

		protected virtual void Start()
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

		public virtual void Rotate(bool status)
		{
		}

		public virtual void Orbit(bool status)
		{
		}

		protected virtual void Rotate()
		{
		}
	}
}
