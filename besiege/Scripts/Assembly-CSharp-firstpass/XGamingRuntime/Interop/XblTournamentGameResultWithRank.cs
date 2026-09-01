namespace XGamingRuntime.Interop
{
	internal struct XblTournamentGameResultWithRank
	{
		internal readonly XblTournamentGameResult Result;

		internal readonly ulong Ranking;

		internal XblTournamentGameResultWithRank(XGamingRuntime.XblTournamentGameResultWithRank publicObject)
		{
			Result = publicObject.Result;
			Ranking = publicObject.Ranking;
		}
	}
}
