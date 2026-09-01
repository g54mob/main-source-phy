using Selectors;

namespace InternalModding.Events
{
	public class TeamButtonHolder : BaseHolder
	{
		public UIButton Toggle;

		public TeamButton TeamButton;

		public event TeamChangedHandler TeamChanged;

		public void Awake()
		{
			Toggle.Click += OnToggle;
		}

		private void OnToggle()
		{
			TeamButton.NextTeam();
			if (this.TeamChanged != null)
			{
				this.TeamChanged(TeamButton.Team);
			}
		}

		public void SetValue(MPTeam team)
		{
			TeamButton.SetTeam(team);
		}
	}
}
