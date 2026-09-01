using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Movement/MMStayInPlace")]
	public class MMStayInPlace : MonoBehaviour
	{
		public enum Spaces
		{
			World = 0,
			Local = 1
		}

		public enum UpdateModes
		{
			Update = 0,
			FixedUpdate = 1,
			LateUpdate = 2
		}

		[Header("Modes")]
		public UpdateModes UpdateMode;

		public Spaces Space;

		[Header("Attributes")]
		public bool FixedPosition;

		public bool FixedRotation;

		public bool FixedScale;

		[Header("Overrides")]
		public bool OverridePosition;

		[MMCondition("OverridePosition", true)]
		public Vector3 OverridePositionValue;

		public bool OverrideRotation;

		[MMCondition("OverrideRotation", true)]
		public Vector3 OverrideRotationValue;

		public bool OverrideScale;

		[MMCondition("OverrideScale", true)]
		public Vector3 OverrideScaleValue;

		protected Vector3 _initialPosition;

		protected Quaternion _initialRotation;

		protected Vector3 _initialScale;

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
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

		protected virtual void StayInPlace()
		{
		}
	}
}
