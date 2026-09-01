using TMPro;
using UnityEngine;

namespace Landfall.TABS.UI.WinConditions
{
	public class TeamToggleTextSwapper : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text m_redTeamText;

		[SerializeField]
		private TMP_Text m_blueTeamText;

		private SettingsInstance m_flipColorSetting;

		private int m_currentValue;

		private void Start()
		{
			m_flipColorSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			m_flipColorSetting.OnValueChanged += SwapTexts;
			SwapTexts(m_flipColorSetting.currentValue);
		}

		private void SwapTexts(int newValue)
		{
			if (newValue != m_currentValue)
			{
				string text = m_redTeamText.text;
				m_redTeamText.text = m_blueTeamText.text;
				m_blueTeamText.text = text;
				m_currentValue = newValue;
			}
		}

		private void OnDestroy()
		{
			m_flipColorSetting.OnValueChanged -= SwapTexts;
		}
	}
}
