namespace XGamingRuntime.Interop
{
	internal struct XblTournamentTeamResult
	{
		internal readonly UTF8StringPtr Team;

		internal readonly XblTournamentGameResultWithRank GameResult;

		internal XblTournamentTeamResult(XGamingRuntime.XblTournamentTeamResult publicObject, DisposableCollection disposableCollection)
		{
			Team = new UTF8StringPtr(publicObject.Team, disposableCollection);
			GameResult = new XblTournamentGameResultWithRank(publicObject.GameResult);
		}
	}
}
