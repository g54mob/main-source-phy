using UnityEngine;
using UnityEngine.EventSystems;

namespace FMODUnity
{
	public abstract class EventHandler : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		public string CollisionTag;

		protected virtual void Start()
		{
		}

		protected virtual void OnDestroy()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnTriggerEnter(Collider other)
		{
		}

		private void OnTriggerExit(Collider other)
		{
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
		}

		private void OnTriggerExit2D(Collider2D other)
		{
		}

		private void OnCollisionEnter()
		{
		}

		private void OnCollisionExit()
		{
		}

		private void OnCollisionEnter2D()
		{
		}

		private void OnCollisionExit2D()
		{
		}

		private void OnMouseEnter()
		{
		}

		private void OnMouseExit()
		{
		}

		private void OnMouseDown()
		{
		}

		private void OnMouseUp()
		{
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
		}

		public void OnPointerExit(PointerEventData eventData)
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
		}

		public void OnPointerUp(PointerEventData eventData)
		{
		}

		protected abstract void HandleGameEvent(EmitterGameEvent gameEvent);
	}
}
