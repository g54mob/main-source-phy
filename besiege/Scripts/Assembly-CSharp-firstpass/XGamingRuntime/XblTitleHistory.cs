using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblTitleHistory
	{
		public bool HasUserPlayed { get; private set; }

		public DateTime LastTimeUserPlayed { get; private set; }

		internal XblTitleHistory(XGamingRuntime.Interop.XblTitleHistory interopTitleHistory)
		{
			HasUserPlayed = interopTitleHistory.hasUserPlayed;
			LastTimeUserPlayed = interopTitleHistory.lastTimeUserPlayed.DateTime;
		}
	}
}
