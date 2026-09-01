using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringRotationAround")]
	public class MMSpringRotationAround : MMSpringFloatComponent<Transform>
	{
		public Transform RotationCenter;

		public Vector3 RotationAxis;

		public bool FaceRotationCenter;

		protected float _currentAngle;

		protected Vector3 _initialPosition;

		protected Quaternion _initialRotation;

		public override float TargetFloat
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		protected override void Initialization()
		{
		}
	}
}
