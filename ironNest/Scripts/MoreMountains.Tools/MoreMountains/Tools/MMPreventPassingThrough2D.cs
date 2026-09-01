using UnityEngine;
using UnityEngine.Serialization;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Movement/MMPreventPassingThrough2D")]
	public class MMPreventPassingThrough2D : MonoBehaviour
	{
		public enum Modes
		{
			Raycast = 0,
			BoxCast = 1
		}

		public Modes Mode;

		public LayerMask ObstaclesLayerMask;

		public float SkinWidth;

		public bool RepositionRigidbodyIfHitTrigger;

		[FormerlySerializedAs("RepositionRigidbody")]
		public bool RepositionRigidbodyIfHitNonTrigger;

		[Header("Debug")]
		[MMReadOnly]
		public RaycastHit2D Hit;

		protected float _smallestBoundsWidth;

		protected float _adjustedSmallestBoundsWidth;

		protected float _squaredBoundsWidth;

		protected Vector3 _positionLastFrame;

		protected Rigidbody2D _rigidbody;

		protected Collider2D _collider;

		protected Vector2 _lastMovement;

		protected float _lastMovementSquared;

		protected RaycastHit2D _hitInfo;

		protected Vector2 _colliderSize;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void Update()
		{
		}
	}
}
