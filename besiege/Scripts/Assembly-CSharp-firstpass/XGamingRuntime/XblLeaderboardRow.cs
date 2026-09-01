using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblLeaderboardRow
	{
		public string Gamertag { get; private set; }

		public string ModernGamertag { get; private set; }

		public string ModernGamertagSuffix { get; private set; }

		public string UniqueModernGamertag { get; private set; }

		public ulong XboxUserId { get; private set; }

		public double Percentile { get; private set; }

		public uint Rank { get; private set; }

		public uint GlobalRank { get; private set; }

		public string[] ColumnValues { get; private set; }

		internal XblLeaderboardRow(XGamingRuntime.Interop.XblLeaderboardRow interopRow)
		{
			Gamertag = Converters.ByteArrayToString(interopRow.gamertag);
			ModernGamertag = Converters.ByteArrayToString(interopRow.modernGamertag);
			ModernGamertagSuffix = Converters.ByteArrayToString(interopRow.modernGamertagSuffix);
			UniqueModernGamertag = Converters.ByteArrayToString(interopRow.uniqueModernGamertag);
			XboxUserId = interopRow.xboxUserId;
			Percentile = interopRow.percentile;
			Rank = interopRow.rank;
			GlobalRank = interopRow.globalRank;
			ColumnValues = interopRow.GetColumnValues();
		}
	}
}
