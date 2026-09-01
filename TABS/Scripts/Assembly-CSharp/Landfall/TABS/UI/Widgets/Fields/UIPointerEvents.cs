using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Landfall.TABS.UI.Widgets.Fields
{
	public class UIPointerEvents : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, ISubmitHandler, ISelectHandler
	{
		[Serializable]
		public class PointerEvent : UnityEvent
		{
		}

		[SerializeField]
		private PointerEvent m_onPointerEnter;

		[SerializeField]
		private PointerEvent m_onPointerExit;

		[SerializeField]
		private PointerEvent m_onPointerClick;

		[SerializeField]
		private PointerEvent m_OnSubmit;

		public PointerEvent OnPointerEnterCallback
		{
			get
			{
				return m_onPointerEnter;
			}
			set
			{
				m_onPointerEnter = value;
			}
		}

		public PointerEvent OnPointerExitCallback
		{
			get
			{
				return m_onPointerExit;
			}
			set
			{
				m_onPointerExit = value;
			}
		}

		public PointerEvent OnPointerClickCallback
		{
			get
			{
				return m_onPointerClick;
			}
			set
			{
				m_onPointerClick = value;
			}
		}

		public PointerEvent OnSubmitCallback
		{
			get
			{
				return m_OnSubmit;
			}
			set
			{
				m_OnSubmit = value;
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			m_onPointerEnter?.Invoke();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			m_onPointerExit?.Invoke();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			m_onPointerClick?.Invoke();
		}

		public void OnSubmit(BaseEventData eventData)
		{
			m_OnSubmit.Invoke();
		}

		public void OnSelect(BaseEventData eventData)
		{
			m_onPointerEnter?.Invoke();
		}
	}
}
