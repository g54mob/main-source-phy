using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Activation/MMTriggerAndCollision")]
	public class MMTriggerAndCollision : MonoBehaviour
	{
		public LayerMask CollisionLayerMask;

		public UnityEvent OnCollisionEnterEvent;

		public UnityEvent OnCollisionExitEvent;

		public UnityEvent OnCollisionStayEvent;

		public LayerMask TriggerLayerMask;

		public UnityEvent OnTriggerEnterEvent;

		public UnityEvent OnTriggerExitEvent;

		public UnityEvent OnTriggerStayEvent;

		public LayerMask Collision2DLayerMask;

		public UnityEvent OnCollision2DEnterEvent;

		public UnityEvent OnCollision2DExitEvent;

		public UnityEvent OnCollision2DStayEvent;

		public LayerMask Trigger2DLayerMask;

		public UnityEvent OnTrigger2DEnterEvent;

		public UnityEvent OnTrigger2DExitEvent;

		public UnityEvent OnTrigger2DStayEvent;

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
		}

		protected virtual void OnCollisionExit2D(Collision2D collision)
		{
		}

		protected virtual void OnCollisionStay2D(Collision2D collision)
		{
		}

		protected virtual void OnTriggerEnter2D(Collider2D collider)
		{
		}

		protected virtual void OnTriggerExit2D(Collider2D collider)
		{
		}

		protected virtual void OnTriggerStay2D(Collider2D collider)
		{
		}

		protected virtual void OnCollisionEnter(Collision c)
		{
		}

		protected virtual void OnCollisionExit(Collision c)
		{
		}

		protected virtual void OnCollisionStay(Collision c)
		{
		}

		protected virtual void OnTriggerEnter(Collider collider)
		{
		}

		protected virtual void OnTriggerExit(Collider collider)
		{
		}

		protected virtual void OnTriggerStay(Collider collider)
		{
		}

		protected virtual void Reset()
		{
		}
	}
}
