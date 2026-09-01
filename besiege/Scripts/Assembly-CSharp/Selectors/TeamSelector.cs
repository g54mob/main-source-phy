using UnityEngine;

namespace Selectors
{
	public class TeamSelector : Selector
	{
		[SerializeField]
		private UIButton[] buttons;

		[SerializeField]
		private Material activeMaterial;

		[SerializeField]
		private DynamicText text;

		private Renderer[] renderers;

		private Material normalMaterial;

		private bool updateCallback;

		public override MapperType MapperType
		{
			get
			{
				return Team;
			}
			set
			{
				if (updateCallback)
				{
					if (Team != null)
					{
						Team.TeamChanged -= OnTeamChanged;
					}
					updateCallback = false;
				}
				Team = (MTeam)value;
				if (Team != null)
				{
					Team.TeamChanged += OnTeamChanged;
					updateCallback = true;
				}
			}
		}

		public MTeam Team { get; set; }

		private void Awake()
		{
			renderers = new Renderer[buttons.Length];
			buttons[0].Click += OnNone;
			buttons[1].Click += OnRed;
			buttons[2].Click += OnGreen;
			buttons[3].Click += OnOrange;
			buttons[4].Click += OnBlue;
			for (int i = 0; i < buttons.Length; i++)
			{
				UIButton uIButton = buttons[i];
				Renderer component = uIButton.GetComponent<Renderer>();
				if (i > 0)
				{
					Renderer component2 = uIButton.transform.GetChild(0).GetComponent<Renderer>();
					component2.material.SetColor("_TintColor", ReferenceMaster.Instance.teamColors[i]);
				}
				renderers[i] = component;
			}
			normalMaterial = renderers[0].material;
		}

		private void OnTeamChanged(MPTeam team)
		{
			UpdateVisual();
		}

		protected void OnDisable()
		{
			if (updateCallback)
			{
				if (Team != null)
				{
					Team.TeamChanged -= OnTeamChanged;
				}
				updateCallback = false;
			}
		}

		private void OnNone()
		{
			OnTeamClicked(MPTeam.None);
		}

		private void OnRed()
		{
			OnTeamClicked(MPTeam.Red);
		}

		private void OnGreen()
		{
			OnTeamClicked(MPTeam.Green);
		}

		private void OnOrange()
		{
			OnTeamClicked(MPTeam.Orange);
		}

		private void OnBlue()
		{
			OnTeamClicked(MPTeam.Blue);
		}

		private void OnTeamClicked(MPTeam team)
		{
			Team.SetValue(team);
			OnEdit();
		}

		public override void Init()
		{
			if (Team == null)
			{
				Debug.LogWarning("MTeam has not been assigned to " + base.transform.name);
			}
			else
			{
				text.SetText(Team.DisplayName.ToUpper());
			}
			base.Init();
			UpdateVisual();
		}

		protected override void UpdateVisual()
		{
			if (Team != null)
			{
				int team = (int)Team.Team;
				for (int i = 0; i < renderers.Length; i++)
				{
					renderers[i].material = ((i != team) ? normalMaterial : activeMaterial);
				}
			}
		}
	}
}
