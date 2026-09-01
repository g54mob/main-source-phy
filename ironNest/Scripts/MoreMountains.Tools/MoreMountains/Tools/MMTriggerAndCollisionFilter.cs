using UnityEngine;

namespace MoreMountains.Tools
{
	public abstract class MMTriggerAndCollisionFilter : MonoBehaviour
	{
		public TriggerAndCollisionMask TriggerAndCollisionFilter;

		protected virtual bool UseEvent(TriggerAndCollisionMask value)
		{
			return false;
		}

		protected abstract void OnCollisionEnter2D_(Collision2D collision);

		private void OnCollisionEnter2D(Collision2D collision)
		{
		}

		protected abstract void OnCollisionExit2D_(Collision2D collision);

		private void OnCollisionExit2D(Collision2D collision)
		{
		}

		protected abstract void OnCollisionStay2D_(Collision2D collision);

		private void OnCollisionStay2D(Collision2D collision)
		{
		}

		protected abstract void OnTriggerEnter2D_(Collider2D collider);

		private void OnTriggerEnter2D(Collider2D collider)
		{
		}

		protected abstract void OnTriggerExit2D_(Collider2D collider);

		private void OnTriggerExit2D(Collider2D collider)
		{
		}

		protected abstract void OnTriggerStay2D_(Collider2D collider);

		private void OnTriggerStay2D(Collider2D collider)
		{
		}

		protected abstract void OnCollisionEnter_(Collision c);

		private void OnCollisionEnter(Collision c)
		{
		}

		protected abstract void OnCollisionExit_(Collision c);

		private void OnCollisionExit(Collision c)
		{
		}

		protected abstract void OnCollisionStay_(Collision c);

		private void OnCollisionStay(Collision c)
		{
		}

		protected abstract void OnTriggerEnter_(Collider collider);

		private void OnTriggerEnter(Collider collider)
		{
		}

		protected abstract void OnTriggerExit_(Collider collider);

		private void OnTriggerExit(Collider collider)
		{
		}

		protected abstract void OnTriggerStay_(Collider collider);

		private void OnTriggerStay(Collider collider)
		{
		}
	}
}
