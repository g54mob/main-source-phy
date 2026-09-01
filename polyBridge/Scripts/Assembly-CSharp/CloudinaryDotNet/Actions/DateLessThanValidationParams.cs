using System;
using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class DateLessThanValidationParams : ComparisonValidationParams<DateTime>
	{
		public DateLessThanValidationParams(DateTime value)
			: base(value)
		{
			base.Type = MetadataValidationType.LessThan;
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			dict.Add("value", base.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
		}
	}
}
