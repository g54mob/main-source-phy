using System;

namespace ModIO.API
{
	public class AddModRatingParameters : RequestParameters
	{
		public const int APIVALUE_NEGATIVERATING = -1;

		public const int APIVALUE_POSITIVERATING = 1;

		public ModRatingValue ratingValue
		{
			set
			{
				SetStringValue("rating", ConvertEnumToInt(value).ToString());
			}
		}

		[Obsolete("Use ratingValue instead.")]
		public int rating
		{
			set
			{
				SetStringValue("rating", value.ToString());
			}
		}

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
	}
}
