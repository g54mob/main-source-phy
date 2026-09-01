using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class IntGreaterThanValidationParams : ComparisonValidationParams<int>
	{
		public IntGreaterThanValidationParams(int value)
			: base(value)
		{
			base.Type = MetadataValidationType.GreaterThan;
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			dict.Add("value", base.Value);
		}
	}
}
