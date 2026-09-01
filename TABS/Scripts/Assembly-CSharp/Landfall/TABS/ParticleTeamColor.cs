using UnityEngine;

namespace Landfall.TABS
{
	public class ParticleTeamColor : MonoBehaviour
	{
		public bool playSystem = true;

		public bool useColor = true;

		public Color redColor;

		public Color blueColor;

		private Holdable holdable;

		public bool useMaterial;

		public Material redMat;

		public Material blueMat;

		private Team m_currentTeam;

		private SettingsInstance m_flipColorsSetting;

		private ParticleSystem m_particleSystem;

		private bool m_setOnce;

		private void Awake()
		{
			m_flipColorsSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
		}

		private void Start()
		{
			m_particleSystem = GetComponent<ParticleSystem>();
			ParticleSystem.MainModule main = m_particleSystem.main;
			TeamHolder componentInParent = GetComponentInParent<TeamHolder>();
			Unit componentInParent2 = GetComponentInParent<Unit>();
			if ((bool)componentInParent)
			{
				SetTeam(componentInParent.team);
			}
			else if ((bool)componentInParent2)
			{
				SetTeam(componentInParent2.Team);
			}
			else
			{
				holdable = GetComponentInParent<Holdable>();
			}
			main.playOnAwake = false;
			m_flipColorsSetting.OnValueChanged += UpdateTeamColors;
		}

		private void OnDestroy()
		{
			m_flipColorsSetting.OnValueChanged -= UpdateTeamColors;
		}

		private void Update()
		{
			if (!m_setOnce && (bool)holdable && (bool)holdable.holderData)
			{
				SetTeam(holdable.holderData.team);
			}
		}

		private void SetTeam(Team team)
		{
			m_currentTeam = team;
			UpdateTeamColors();
		}

		private void UpdateTeamColors()
		{
			UpdateTeamColors(m_flipColorsSetting.currentValue);
		}

		private void UpdateTeamColors(int value)
		{
			Team team = m_currentTeam;
			if (value == 1)
			{
				switch (team)
				{
				case Team.Red:
					team = Team.Blue;
					break;
				case Team.Blue:
					team = Team.Red;
					break;
				}
			}
			ParticleSystem.MainModule main = m_particleSystem.main;
			ParticleSystemRenderer component = GetComponent<ParticleSystemRenderer>();
			if (team == Team.Red)
			{
				if (useColor)
				{
					main.startColor = new ParticleSystem.MinMaxGradient(redColor);
				}
				if (useMaterial && (bool)redMat)
				{
					component.material = redMat;
				}
			}
			else
			{
				if (useColor)
				{
					main.startColor = new ParticleSystem.MinMaxGradient(blueColor);
				}
				if (useMaterial && (bool)blueMat)
				{
					component.material = blueMat;
				}
			}
			m_setOnce = true;
			if (playSystem)
			{
				m_particleSystem.Play();
			}
		}
	}
}
