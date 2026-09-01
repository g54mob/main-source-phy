using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModRating
	{
		public const int POSITIVE_VALUE = 1;

		public const int NEGATIVE_VALUE = -1;

		public const int APIOBJECT_VALUEINT_NEGATIVERATING = -1;

		public const int APIOBJECT_VALUEINT_POSITIVERATING = 1;

		[JsonProperty("game_id")]
		public int gameId;

		[JsonProperty("mod_id")]
		public int modId;

		[JsonProperty("rating_enum")]
		public ModRatingValue ratingValue;

		[JsonProperty("date_added")]
		public int dateAdded;

		[JsonProperty("rating")]
		private int? _apiRatingValue;

		public static ModRatingValue ConvertIntToEnum(int valueInteger)
		{
			switch (valueInteger)
			{
			case -1:
				return ModRatingValue.Negative;
			case 1:
				return ModRatingValue.Positive;
			default:
				return ModRatingValue.None;
			}
		}

		public static int ConvertEnumToInt(ModRatingValue valueEnum)
		{
			switch (valueEnum)
			{
			case ModRatingValue.Negative:
				return -1;
			case ModRatingValue.Positive:
				return 1;
			default:
				return 0;
			}
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			int? apiRatingValue = _apiRatingValue;
			if (apiRatingValue.HasValue)
			{
				int? apiRatingValue2 = _apiRatingValue;
				ratingValue = ConvertIntToEnum(apiRatingValue2.Value);
			}
		}
	}
}
