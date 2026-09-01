using System;
using UnityEngine;

namespace TFBGames.UnitCreatorBakeReport
{
	[Serializable]
	public class BakeReportLog
	{
		public BakeReportLogType reportLogLogType;

		[Multiline]
		public string message;

		public BakeReportLog(BakeReportLogType logType, string message)
		{
			reportLogLogType = logType;
			this.message = message;
		}
	}
}
