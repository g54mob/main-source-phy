using UnityEngine;

namespace TFBGames
{
	public class WinConditionsIcons : MonoBehaviour
	{
		public enum ConditionType
		{
			LastTeamStanding = 0,
			TimeLimit = 1,
			MustKill = 2,
			Default = 3
		}

		[SerializeField]
		private Sprite m_MustKillIcon;

		[SerializeField]
		private Sprite m_TimeLimitIcon;

		[SerializeField]
		private Sprite m_LastTeamStandingIcon;

		[SerializeField]
		private Sprite m_DefaultImageIcon;

		public Sprite GetImage(ConditionType conditionType)
		{
			Sprite sprite = null;
			switch (conditionType)
			{
			case ConditionType.LastTeamStanding:
				return m_LastTeamStandingIcon;
			case ConditionType.TimeLimit:
				return m_TimeLimitIcon;
			case ConditionType.MustKill:
				return m_MustKillIcon;
			default:
				return m_DefaultImageIcon;
			}
		}
	}
}
