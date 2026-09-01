using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	public class MMOnMouse : MonoBehaviour
	{
		[Tooltip("OnMouseDown is called when the user has pressed the mouse button while over the Collider.")]
		public UnityEvent OnMouseDownEvent;

		[Tooltip("OnMouseDrag is called when the user has clicked on a Collider and is still holding down the mouse.")]
		public UnityEvent OnMouseDragEvent;

		[Tooltip("Called when the mouse enters the Collider.")]
		public UnityEvent OnMouseEnterEvent;

		[Tooltip("Called when the mouse is not any longer over the Collider.")]
		public UnityEvent OnMouseExitEvent;

		[Tooltip("Called every frame while the mouse is over the Collider.")]
		public UnityEvent OnMouseOverEvent;

		[Tooltip("OnMouseUp is called when the user has released the mouse button.")]
		public UnityEvent OnMouseUpEvent;

		[Tooltip("OnMouseUpAsButton is only called when the mouse is released over the same Collider as it was pressed.")]
		public UnityEvent OnMouseUpAsButtonEvent;

		protected virtual void OnMouseDown()
		{
		}

		protected virtual void OnMouseDrag()
		{
		}

		protected virtual void OnMouseEnter()
		{
		}

		protected virtual void OnMouseExit()
		{
		}

		protected virtual void OnMouseOver()
		{
		}

		protected virtual void OnMouseUp()
		{
		}

		protected virtual void OnMouseUpAsButton()
		{
		}
	}
}
