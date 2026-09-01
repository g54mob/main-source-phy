using System;
using System.Collections.Generic;

namespace TFBGames.UnitCreatorBakeReport
{
	[Serializable]
	public class BakeReport
	{
		public DateTime lastRunDate;

		public List<BakeReportItem> reportItems;

		public BakeReport()
		{
			lastRunDate = DateTime.Now;
			reportItems = new List<BakeReportItem>();
		}
	}
}
