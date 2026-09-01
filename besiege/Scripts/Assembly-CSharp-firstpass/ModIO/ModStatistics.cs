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
				return (ratingCount != 0) ? ((float)ratingPositiveCount / (float)ratingCount) : 0f;
			}
		}

		[JsonIgnore]
		public float ratingNegativePercentage
		{
			get
			{
				return (ratingCount != 0) ? ((float)ratingNegativeCount / (float)ratingCount) : 0f;
			}
		}

		[JsonIgnore]
		public int ratingTotal
		{
			get
			{
				int num = ((ratingCount != 0) ? (ratingPositiveCount - ratingNegativeCount) : 0);
				if (num < 0)
				{
					num = 0;
				}
				return num;
			}
		}
	}
}
