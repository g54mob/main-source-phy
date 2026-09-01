namespace XGamingRuntime.Interop
{
	internal struct XblTitleManagedStatistic
	{
		internal readonly UTF8StringPtr statisticName;

		internal readonly XblTitleManagedStatType statisticType;

		internal readonly double numberValue;

		internal readonly UTF8StringPtr stringValue;

		internal XblTitleManagedStatistic(XGamingRuntime.XblTitleManagedStatistic publicObject, DisposableCollection disposableCollection)
		{
			statisticName = new UTF8StringPtr(publicObject.StatisticName, disposableCollection);
			statisticType = publicObject.StatisticType;
			numberValue = publicObject.NumberValue;
			stringValue = new UTF8StringPtr(publicObject.StringValue, disposableCollection);
		}
	}
}
