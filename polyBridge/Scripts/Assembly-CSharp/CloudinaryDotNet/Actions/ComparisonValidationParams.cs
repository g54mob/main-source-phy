using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public abstract class ComparisonValidationParams<T> : MetadataValidationParams
	{
		public T Value { get; set; }

		public bool IsEqual { get; set; }

		protected ComparisonValidationParams(T value)
		{
			Value = value;
		}

		public override void Check()
		{
			Utils.ShouldBeSpecified(() => Value);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			BaseParams.AddParam(dict, "equals", IsEqual);
		}
	}
}
