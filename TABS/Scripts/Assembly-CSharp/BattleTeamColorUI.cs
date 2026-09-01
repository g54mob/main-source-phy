using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleTeamColorUI : MonoBehaviour
{
	[Serializable]
	public class TeamGraphicInfo
	{
		public Color m_team1;

		public Color m_team2;

		public Graphic[] m_graphics1;

		public Graphic[] m_graphics2;
	}

	[SerializeField]
	private TeamGraphicInfo[] m_teamInfos;

	private SettingsInstance m_flipOption;

	private void OnEnable()
	{
		m_flipOption = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
		m_flipOption.OnValueChanged += OnTeamSettings;
		OnTeamSettings(m_flipOption.currentValue);
	}

	private void OnDisable()
	{
		if (m_flipOption != null)
		{
			m_flipOption.OnValueChanged -= OnTeamSettings;
		}
	}

	private void OnTeamSettings(int value)
	{
		for (int i = 0; i < m_teamInfos.Length; i++)
		{
			TeamGraphicInfo teamGraphicInfo = m_teamInfos[i];
			for (int j = 0; j < teamGraphicInfo.m_graphics1.Length; j++)
			{
				teamGraphicInfo.m_graphics1[j].color = ((value == 0) ? teamGraphicInfo.m_team1 : teamGraphicInfo.m_team2);
			}
			for (int k = 0; k < teamGraphicInfo.m_graphics2.Length; k++)
			{
				teamGraphicInfo.m_graphics2[k].color = ((value == 0) ? teamGraphicInfo.m_team2 : teamGraphicInfo.m_team1);
			}
		}
	}
}
