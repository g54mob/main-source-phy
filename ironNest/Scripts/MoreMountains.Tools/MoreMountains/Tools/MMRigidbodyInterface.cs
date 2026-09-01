using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Rigidbody Interface/MMRigidbodyInterface")]
	public class MMRigidbodyInterface : MonoBehaviour
	{
		protected string _mode;

		protected Rigidbody2D _rigidbody2D;

		protected Rigidbody _rigidbody;

		protected Collider2D _collider2D;

		protected Collider _collider;

		protected Bounds _colliderBounds;

		public Vector3 position
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public Rigidbody2D InternalRigidBody2D => null;

		public Rigidbody InternalRigidBody => null;

		public Vector3 Velocity
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public Bounds ColliderBounds => default(Bounds);

		public bool isKinematic => false;

		public bool Is3D => false;

		public bool Is2D => false;

		protected virtual void Awake()
		{
		}

		public virtual void AddForce(Vector3 force)
		{
		}

		public virtual void AddRelativeForce(Vector3 force)
		{
		}

		public virtual void MovePosition(Vector3 newPosition)
		{
		}

		public virtual void ResetAngularVelocity()
		{
		}

		public virtual void ResetRotation()
		{
		}

		public virtual void IsKinematic(bool status)
		{
		}

		public virtual void EnableBoxCollider(bool status)
		{
		}
	}
}
