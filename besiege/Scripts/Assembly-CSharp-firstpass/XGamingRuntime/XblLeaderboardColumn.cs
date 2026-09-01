using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblLeaderboardColumn
	{
		public string StatName { get; private set; }

		public XblLeaderboardStatType StatType { get; private set; }

		internal XblLeaderboardColumn(XGamingRuntime.Interop.XblLeaderboardColumn interopColumn)
		{
			StatName = interopColumn.statName.GetString();
			StatType = interopColumn.statType;
		}
	}
}
