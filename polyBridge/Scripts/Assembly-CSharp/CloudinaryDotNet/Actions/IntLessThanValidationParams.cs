using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class IntLessThanValidationParams : ComparisonValidationParams<int>
	{
		public IntLessThanValidationParams(int value)
			: base(value)
		{
			base.Type = MetadataValidationType.LessThan;
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			dict.Add("value", base.Value);
		}
	}
}
