using UnityEngine;
using UnityEngine.EventSystems;

namespace ModIO.UI
{
	public class EventPasser : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IMoveHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, ISubmitHandler, ICancelHandler, IEventSystemHandler
	{
		public GameObject target;

		public bool onMove;

		public bool onPointerDown;

		public bool onPointerUp;

		public bool onPointerEnter;

		public bool onPointerExit;

		public bool onPointerClick;

		public bool onSubmit;

		public bool onCancel;

		public void OnMove(AxisEventData eventData)
		{
			if (!(target != null) || !onMove)
			{
				return;
			}
			IMoveHandler[] components = target.gameObject.GetComponents<IMoveHandler>();
			foreach (IMoveHandler moveHandler in components)
			{
				if (moveHandler != null && ((MonoBehaviour)moveHandler).isActiveAndEnabled)
				{
					moveHandler.OnMove(eventData);
				}
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!(target != null) || !onPointerDown)
			{
				return;
			}
			IPointerDownHandler[] components = target.gameObject.GetComponents<IPointerDownHandler>();
			foreach (IPointerDownHandler pointerDownHandler in components)
			{
				if (pointerDownHandler != null && ((MonoBehaviour)pointerDownHandler).isActiveAndEnabled)
				{
					pointerDownHandler.OnPointerDown(eventData);
				}
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!(target != null) || !onPointerUp)
			{
				return;
			}
			IPointerUpHandler[] components = target.gameObject.GetComponents<IPointerUpHandler>();
			foreach (IPointerUpHandler pointerUpHandler in components)
			{
				if (pointerUpHandler != null && ((MonoBehaviour)pointerUpHandler).isActiveAndEnabled)
				{
					pointerUpHandler.OnPointerUp(eventData);
				}
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!(target != null) || !onPointerEnter)
			{
				return;
			}
			IPointerEnterHandler[] components = target.gameObject.GetComponents<IPointerEnterHandler>();
			foreach (IPointerEnterHandler pointerEnterHandler in components)
			{
				if (pointerEnterHandler != null && ((MonoBehaviour)pointerEnterHandler).isActiveAndEnabled)
				{
					pointerEnterHandler.OnPointerEnter(eventData);
				}
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!(target != null) || !onPointerExit)
			{
				return;
			}
			IPointerExitHandler[] components = target.gameObject.GetComponents<IPointerExitHandler>();
			foreach (IPointerExitHandler pointerExitHandler in components)
			{
				if (pointerExitHandler != null && ((MonoBehaviour)pointerExitHandler).isActiveAndEnabled)
				{
					pointerExitHandler.OnPointerExit(eventData);
				}
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!(target != null) || !onPointerClick)
			{
				return;
			}
			IPointerClickHandler[] components = target.gameObject.GetComponents<IPointerClickHandler>();
			foreach (IPointerClickHandler pointerClickHandler in components)
			{
				if (pointerClickHandler != null && ((MonoBehaviour)pointerClickHandler).isActiveAndEnabled)
				{
					pointerClickHandler.OnPointerClick(eventData);
				}
			}
		}

		public void OnSubmit(BaseEventData eventData)
		{
			if (!(target != null) || !onSubmit)
			{
				return;
			}
			ISubmitHandler[] components = target.gameObject.GetComponents<ISubmitHandler>();
			foreach (ISubmitHandler submitHandler in components)
			{
				if (submitHandler != null && ((MonoBehaviour)submitHandler).isActiveAndEnabled)
				{
					submitHandler.OnSubmit(eventData);
				}
			}
		}

		public void OnCancel(BaseEventData eventData)
		{
			if (!(target != null) || !onCancel)
			{
				return;
			}
			ICancelHandler[] components = target.gameObject.GetComponents<ICancelHandler>();
			foreach (ICancelHandler cancelHandler in components)
			{
				if (cancelHandler != null && ((MonoBehaviour)cancelHandler).isActiveAndEnabled)
				{
					cancelHandler.OnCancel(eventData);
				}
			}
		}
	}
}
