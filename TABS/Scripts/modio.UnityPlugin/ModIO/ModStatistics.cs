using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModStatistics
	{
		[JsonProperty("mod_id")]
		public int modId;

		[JsonProperty("popularity_rank_position")]
		public int popularityRankPosition;

		[JsonProperty("popularity_rank_total_mods")]
		public int popularityRankModCount;

		[JsonProperty("downloads_total")]
		public int downloadCount;

		[JsonProperty("subscribers_total")]
		public int subscriberCount;

		[JsonProperty("ratings_total")]
		public int ratingCount;

		[JsonProperty("ratings_positive")]
		public int ratingPositiveCount;

		[JsonProperty("ratings_negative")]
		public int ratingNegativeCount;

		[JsonProperty("ratings_weighted_aggregate")]
		public float ratingWeightedAggregate;

		[JsonProperty("ratings_display_text")]
		public string ratingDisplayText;

		[JsonProperty("date_expires")]
		public int dateExpires;

		[JsonIgnore]
		public float ratingPositivePercentage
		{
			get
			{
				if (ratingCount != 0)
				{
					return (float)ratingPositiveCount / (float)ratingCount;
				}
				return 0f;
			}
		}

		[JsonIgnore]
		public float ratingNegativePercentage
		{
			get
			{
				if (ratingCount != 0)
				{
					return (float)ratingNegativeCount / (float)ratingCount;
				}
				return 0f;
			}
		}
	}
}
