using System;

namespace ModIO.UI
{
	[Serializable]
	[Obsolete("No longer supported.")]
	public struct ModStatisticsDisplayData
	{
		public int modId;

		public int popularityRankPosition;

		public int popularityRankModCount;

		public int downloadCount;

		public int subscriberCount;

		public int ratingCount;

		public int ratingPositiveCount;

		public int ratingNegativeCount;

		public float ratingWeightedAggregate;

		public string ratingDisplayText;

		public int dateExpires;

		public static ModStatisticsDisplayData CreateFromStatistics(ModStatistics statistics)
		{
			return new ModStatisticsDisplayData
			{
				modId = statistics.modId,
				popularityRankPosition = statistics.popularityRankPosition,
				popularityRankModCount = statistics.popularityRankModCount,
				downloadCount = statistics.downloadCount,
				subscriberCount = statistics.subscriberCount,
				ratingCount = statistics.ratingCount,
				ratingPositiveCount = statistics.ratingPositiveCount,
				ratingNegativeCount = statistics.ratingNegativeCount,
				ratingWeightedAggregate = statistics.ratingWeightedAggregate,
				ratingDisplayText = statistics.ratingDisplayText,
				dateExpires = statistics.dateExpires
			};
		}
	}
}
