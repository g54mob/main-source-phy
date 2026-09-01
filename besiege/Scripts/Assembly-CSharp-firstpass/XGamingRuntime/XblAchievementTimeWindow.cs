using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblAchievementTimeWindow
	{
		public DateTime StartDate { get; private set; }

		public DateTime EndDate { get; private set; }

		internal XblAchievementTimeWindow(XGamingRuntime.Interop.XblAchievementTimeWindow interopTimeWindow)
		{
			StartDate = interopTimeWindow.startDate.DateTime;
			EndDate = interopTimeWindow.endDate.DateTime;
		}
	}
}
