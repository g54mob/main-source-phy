using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Landfall.TABS
{
	public class TextFieldButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
	{
		public bool m_OnSelectUnderline = true;

		public TextMeshProUGUI m_Text;

		private void Awake()
		{
			if (m_Text == null)
			{
				m_Text = GetComponent<TextMeshProUGUI>();
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			m_Text.fontStyle = FontStyles.Underline;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			m_Text.fontStyle = FontStyles.Normal;
		}
	}
}
