using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class FactionCreatorColorSelectCell : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
	{
		public Image m_image;

		private CustomFactionColorDatabase.CustomFactionColor m_color;

		public GameObject Setup(CustomFactionColorDatabase.CustomFactionColor color)
		{
			m_color = color;
			m_image.color = color.m_Color;
			base.gameObject.SetActive(value: true);
			return base.gameObject;
		}

		private void OnClick()
		{
			Object.FindObjectOfType<FactionCreatorColorSelect>().SelectColor(m_color);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnClick();
		}

		public void OnSubmit(BaseEventData eventData)
		{
			OnClick();
		}
	}
}
