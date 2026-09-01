using System;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Buoyancy/Buoy")]
	public class Buoy : MonoBehaviour
	{
		public delegate void BuoyEventHandler(Buoy victim);

		[SerializeField]
		[HideInInspector]
		private float _radius = 1f;

		private Transform transformCache;

		[SerializeField]
		private SphereCollider colliderCache;

		private Rigidbody m_rigidBodyCache;

		public float radius
		{
			get
			{
				return _radius;
			}
			set
			{
				_radius = value;
				if (colliderCache != null)
				{
					colliderCache.radius = value;
				}
			}
		}

		public Vector3 position
		{
			get
			{
				return transformCache.position;
			}
		}

		public Vector4 velocityData
		{
			get
			{
				Vector3 velocity = affectedObject.velocity;
				return new Vector4(velocity.x, velocity.y, velocity.z, 0f);
			}
		}

		public Rigidbody affectedObject
		{
			get
			{
				return m_rigidBodyCache;
			}
		}

		private string m_noTriggerExceptionMessage
		{
			get
			{
				return "This buoy needs a sphere trigger. If the current GameObject needs a regular collider, try attaching this buoy to a child object instead.";
			}
		}

		public event BuoyEventHandler willBeDestroyed;

		protected void Reset()
		{
			AddMissingComponents();
		}

		protected void Awake()
		{
			if (Application.isPlaying)
			{
				AddMissingComponents();
			}
		}

		protected void OnEnable()
		{
			transformCache = base.transform;
			if (colliderCache == null)
			{
				throw new MissingComponentException(m_noTriggerExceptionMessage);
			}
			colliderCache.radius = radius;
			m_rigidBodyCache = colliderCache.attachedRigidbody;
			if (affectedObject == null)
			{
				throw new MissingComponentException("Buoy's trigger needs to be attached to a rigidbody");
			}
			affectedObject.WakeUp();
		}

		protected void OnDestroy()
		{
			if (this.willBeDestroyed != null)
			{
				this.willBeDestroyed(this);
			}
		}

		public void addWillBeDestroyedHandler(BuoyEventHandler handler)
		{
			this.willBeDestroyed = (BuoyEventHandler)Delegate.Remove(this.willBeDestroyed, handler);
			this.willBeDestroyed = (BuoyEventHandler)Delegate.Combine(this.willBeDestroyed, handler);
		}

		public void removeWillBeDestroyedHandler(BuoyEventHandler handler)
		{
			this.willBeDestroyed = (BuoyEventHandler)Delegate.Remove(this.willBeDestroyed, handler);
		}

		public void applyBuoyancy(Vector3 force)
		{
			affectedObject.AddForceAtPosition(force, position, ForceMode.Force);
			if (float.IsNaN(force.x))
			{
				Debug.Log("Invalid force position: " + base.transform.position.x + ", " + base.transform.position.y + ", " + base.transform.position.z, this);
			}
		}

		protected void AddMissingComponents()
		{
			SphereCollider component = GetComponent<SphereCollider>();
			if (component != null)
			{
				if (component.isTrigger)
				{
					colliderCache = component;
					_radius = colliderCache.radius;
				}
			}
			else
			{
				colliderCache = base.gameObject.AddComponent<SphereCollider>();
			}
			if (colliderCache != null)
			{
				colliderCache.isTrigger = true;
				return;
			}
			throw new InvalidOperationException(m_noTriggerExceptionMessage);
		}
	}
}
