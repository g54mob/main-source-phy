namespace XGamingRuntime.Interop
{
	internal struct XblLeaderboardColumn
	{
		internal readonly UTF8StringPtr statName;

		internal readonly XblLeaderboardStatType statType;

		internal XblLeaderboardColumn(XGamingRuntime.XblLeaderboardColumn column, DisposableCollection disposableCollection)
		{
			statName = new UTF8StringPtr(column.StatName, disposableCollection);
			statType = column.StatType;
		}
	}
}
