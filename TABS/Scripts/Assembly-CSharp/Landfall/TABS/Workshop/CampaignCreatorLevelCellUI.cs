using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class CampaignCreatorLevelCellUI : MonoBehaviour
	{
		private TextMeshProUGUI m_LevelNameText;

		private Button m_Button;

		[SerializeField]
		private Button m_UpButton;

		[SerializeField]
		private Button m_DownButton;

		public bool IsSelected { get; private set; }

		public TABSCampaignLevelAsset CampaignLevel { get; private set; }

		public string LevelName { get; private set; }

		public void Init(TABSCampaignLevelAsset level, UnityAction OnClickAction, bool isSelected = false, UnityAction OnUpArrowClick = null, UnityAction OnDownArrowClick = null)
		{
			m_Button = GetComponent<Button>();
			m_LevelNameText = GetComponentInChildren<TextMeshProUGUI>();
			CampaignLevel = level;
			FileInfo fileInfo = new FileInfo(CampaignLevel.FilePath);
			LevelName = fileInfo.Name;
			string levelName = LevelName.Remove(LevelName.Length - fileInfo.Extension.Length);
			LevelName = levelName;
			m_LevelNameText.text = LevelName;
			IsSelected = isSelected;
			m_Button.onClick.AddListener(OnClickAction);
			if (IsSelected)
			{
				m_UpButton.onClick.AddListener(OnUpArrowClick);
				m_DownButton.onClick.AddListener(OnDownArrowClick);
			}
		}

		public void SetSiblingIndex(int index)
		{
			if (IsSelected && index > 0)
			{
				m_UpButton.gameObject.SetActive(value: true);
			}
		}

		public void GotSiblingAfterStateChange(bool removed)
		{
			if (IsSelected)
			{
				m_DownButton.gameObject.SetActive(!removed);
			}
		}

		public void GotSiblingBeforeStateChange(bool removed)
		{
			if (IsSelected)
			{
				m_UpButton.gameObject.SetActive(!removed);
			}
		}
	}
}
