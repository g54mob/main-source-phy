using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class LevelCellUI : MonoBehaviour
	{
		private TextMeshProUGUI m_Text;

		public TABSCampaignLevelAsset LevelAsset { get; private set; }

		public bool IsBattle { get; private set; }

		public string FullPath { get; private set; }

		public string LevelName { get; private set; }

		public void Init(string levelName, TABSCampaignLevelAsset level, bool battle, Action onLoad)
		{
			LevelAsset = level;
			string filePath = LevelAsset.FilePath;
			m_Text = GetComponentInChildren<TextMeshProUGUI>();
			if (m_Text != null)
			{
				m_Text.text = levelName;
			}
			FullPath = filePath;
			LevelName = levelName;
			IsBattle = battle;
			GetComponent<Button>().onClick.AddListener(delegate
			{
				onLoad();
			});
		}
	}
}
