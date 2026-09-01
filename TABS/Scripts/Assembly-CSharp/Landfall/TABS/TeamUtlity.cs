namespace Landfall.TABS
{
	public static class TeamUtlity
	{
		public static Team GetOtherTeam(Team team)
		{
			if (team == Team.Blue)
			{
				return Team.Red;
			}
			return Team.Blue;
		}
	}
}
