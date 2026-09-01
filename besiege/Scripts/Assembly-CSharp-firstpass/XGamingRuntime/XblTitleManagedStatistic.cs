using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblTitleManagedStatistic
	{
		public string StatisticName { get; set; }

		public XblTitleManagedStatType StatisticType { get; set; }

		public double NumberValue { get; set; }

		public string StringValue { get; set; }

		internal XblTitleManagedStatistic(XGamingRuntime.Interop.XblTitleManagedStatistic interopStruct)
		{
			StatisticName = interopStruct.statisticName.GetString();
			StatisticType = interopStruct.statisticType;
			NumberValue = interopStruct.numberValue;
			StringValue = interopStruct.stringValue.GetString();
		}

		internal XblTitleManagedStatistic(string statisticName, XblTitleManagedStatType statType, string stringValue, double numberValue)
		{
			StatisticName = statisticName;
			StatisticType = statType;
			StringValue = stringValue;
			NumberValue = numberValue;
		}

		public XblTitleManagedStatistic()
		{
		}

		public XblTitleManagedStatistic(string statisticName, string statisticValue)
			: this(statisticName, XblTitleManagedStatType.String, statisticValue, 0.0)
		{
		}

		public XblTitleManagedStatistic(string statisticName, double statisticValue)
			: this(statisticName, XblTitleManagedStatType.Number, null, statisticValue)
		{
		}
	}
}
