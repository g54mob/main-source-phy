using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall
{
	public class RoadmapTextImage : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		public Image m_Image;

		public GameObject m_HoverText;

		public Color m_HoverColor = Color.black;

		public Sprite m_BluredImage;

		private Sprite m_ogImage;

		private Color m_normalColor;

		private Color m_targetColor;

		private void Awake()
		{
			if (m_Image != null)
			{
				m_normalColor = m_Image.color;
				m_targetColor = m_normalColor;
				m_ogImage = m_Image.sprite;
			}
		}

		private void Update()
		{
			if (m_Image != null)
			{
				m_Image.color = Color.Lerp(m_Image.color, m_targetColor, Time.unscaledDeltaTime * 40f);
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			ShowDetails();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			HideDetails();
		}

		public void ShowDetails()
		{
			m_targetColor = m_HoverColor;
			m_HoverText.SetActive(value: true);
			if (m_Image != null)
			{
				m_Image.sprite = m_BluredImage;
			}
		}

		public void HideDetails()
		{
			m_targetColor = m_normalColor;
			m_HoverText.SetActive(value: false);
			if (m_Image != null)
			{
				m_Image.sprite = m_ogImage;
			}
		}
	}
}
