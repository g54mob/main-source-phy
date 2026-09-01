using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Movement/MMPreventPassingThrough3D")]
	public class MMPreventPassingThrough3D : MonoBehaviour
	{
		public enum AdjustmentAxis
		{
			Auto = 0,
			X = 1,
			Y = 2,
			Z = 3
		}

		public LayerMask ObstaclesLayerMask;

		public float SkinWidth;

		public bool RepositionRigidbody;

		public LayerMask RepositionRigidbodyLayerMask;

		public AdjustmentAxis Adjustment;

		protected float _adjustmentDistance;

		protected float _adjustedDistance;

		protected float _squaredBoundsWidth;

		protected Vector3 _positionLastFrame;

		protected Rigidbody _rigidbody;

		protected Collider _collider;

		protected Vector3 _lastMovement;

		protected float _lastMovementSquared;

		protected virtual void OnValidate()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual float ComputeAdjustmentDistance()
		{
			return 0f;
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void FixedUpdate()
		{
		}
	}
}
