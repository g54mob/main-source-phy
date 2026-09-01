using System;
using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class DateGreaterThanValidationParams : ComparisonValidationParams<DateTime>
	{
		public DateGreaterThanValidationParams(DateTime value)
			: base(value)
		{
			base.Type = MetadataValidationType.GreaterThan;
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			dict.Add("value", base.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
		}
	}
}
