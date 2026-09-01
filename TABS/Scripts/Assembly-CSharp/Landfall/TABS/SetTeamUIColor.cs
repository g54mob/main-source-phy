using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class SetTeamUIColor : MonoBehaviour
	{
		public Team team;

		private SettingsInstance m_flipOption;

		private Image m_image;

		private void Awake()
		{
			m_image = GetComponent<Image>();
		}

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
			Team team = ((value == 0) ? this.team : (1 - this.team));
			if (SceneSettings.UseSceneColorOverwrite)
			{
				m_image.color = SceneSettings.GetTeamBackgroundColor(team);
			}
		}
	}
}
