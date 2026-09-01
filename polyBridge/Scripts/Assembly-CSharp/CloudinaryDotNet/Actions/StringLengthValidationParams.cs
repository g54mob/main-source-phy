using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class StringLengthValidationParams : MetadataValidationParams
	{
		public int? Min { get; set; }

		public int? Max { get; set; }

		public StringLengthValidationParams()
		{
			base.Type = MetadataValidationType.StringLength;
		}

		public override void Check()
		{
			if (!Min.HasValue && !Max.HasValue)
			{
				throw new ArgumentException("Either Min or Max must be specified");
			}
			if (Min.HasValue && Min.Value < 0)
			{
				throw new ArgumentException("Min must be a positive integer");
			}
			if (Max.HasValue && Max.Value < 0)
			{
				throw new ArgumentException("Max must be a positive integer");
			}
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			if (Min.HasValue)
			{
				dict.Add("min", Min.Value);
			}
			if (Max.HasValue)
			{
				dict.Add("max", Max.Value);
			}
		}
	}
}
