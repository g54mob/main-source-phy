using UnityEngine;

namespace MoreMountains.Tools
{
	public abstract class MMTriggerFilter : MonoBehaviour
	{
		public TriggerAndCollisionMask TriggerFilter;

		protected virtual void OnValidate()
		{
		}

		protected virtual bool UseEvent(TriggerAndCollisionMask value)
		{
			return false;
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
