using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class NewLevelPresetTemplateComponent : MonoBehaviour
	{
		[SerializeField]
		private GameObject m_on;

		[SerializeField]
		private GameObject m_off;

		[SerializeField]
		private GameObject m_selectedArrow;

		[SerializeField]
		private Image m_iconImage;

		[SerializeField]
		private Image m_iconImageMask;

		public LevelPresetData m_levelPreset;

		public void Init(LevelPresetData levelPreset)
		{
			m_levelPreset = levelPreset;
			m_iconImage.sprite = levelPreset.PresetIcon;
			m_iconImageMask.sprite = levelPreset.PresetIcon;
		}

		public void EnterHover()
		{
			m_on.SetActive(value: true);
			m_off.SetActive(value: false);
		}

		public void ExitHover()
		{
			m_on.SetActive(value: false);
			m_off.SetActive(value: true);
		}

		public void Select()
		{
			if (!(this == null))
			{
				m_selectedArrow.SetActive(value: true);
			}
		}

		public void Deselect()
		{
			if (!(this == null))
			{
				m_selectedArrow.SetActive(value: false);
			}
		}
	}
}
