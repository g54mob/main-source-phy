using System;
using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class DateMetadataFieldCreateParams : MetadataFieldCreateParams<DateTime?>
	{
		public DateMetadataFieldCreateParams(string label)
			: base(label)
		{
			base.Type = MetadataFieldType.Date;
		}

		public override void Check()
		{
			base.Check();
			List<Type> allowedValidationTypes = new List<Type>
			{
				typeof(DateGreaterThanValidationParams),
				typeof(DateLessThanValidationParams),
				typeof(AndValidationParams)
			};
			CheckScalarDataModel(allowedValidationTypes);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			if (base.DefaultValue.HasValue)
			{
				BaseParams.AddParam(dict, "default_value", base.DefaultValue.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			}
		}
	}
}
