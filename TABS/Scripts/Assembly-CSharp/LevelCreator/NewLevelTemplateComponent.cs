using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class NewLevelTemplateComponent : MonoBehaviour
	{
		public string m_levelPath;

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
