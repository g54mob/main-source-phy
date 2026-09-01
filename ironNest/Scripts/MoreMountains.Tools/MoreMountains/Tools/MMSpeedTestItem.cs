using System.Diagnostics;

namespace MoreMountains.Tools
{
	public struct MMSpeedTestItem
	{
		public string TestID;

		public Stopwatch Timer;

		public MMSpeedTestItem(string testID)
		{
			TestID = null;
			Timer = null;
		}
	}
}
