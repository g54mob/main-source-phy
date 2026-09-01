using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class SaveMenuThumbnailTemplate : MonoBehaviour
	{
		public Texture2D m_thumbnail;

		[SerializeField]
		private Image m_selectionArrow;

		public void Select()
		{
			if (!(this == null) && m_selectionArrow != null && m_selectionArrow.gameObject != null)
			{
				m_selectionArrow.gameObject.SetActive(value: true);
			}
		}

		public void Deselect()
		{
			if (!(this == null) && m_selectionArrow != null && m_selectionArrow.gameObject != null)
			{
				m_selectionArrow.gameObject.SetActive(value: false);
			}
		}
	}
}
