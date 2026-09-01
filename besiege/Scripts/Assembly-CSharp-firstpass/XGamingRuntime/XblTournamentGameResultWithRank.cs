using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblTournamentGameResultWithRank
	{
		public XblTournamentGameResult Result { get; private set; }

		public ulong Ranking { get; private set; }

		internal XblTournamentGameResultWithRank(XGamingRuntime.Interop.XblTournamentGameResultWithRank interopStruct)
		{
			Result = interopStruct.Result;
			Ranking = interopStruct.Ranking;
		}
	}
}
