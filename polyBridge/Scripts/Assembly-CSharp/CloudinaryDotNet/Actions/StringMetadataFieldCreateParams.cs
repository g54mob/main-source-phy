using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class StringMetadataFieldCreateParams : MetadataFieldCreateParams<string>
	{
		public StringMetadataFieldCreateParams(string label)
			: base(label)
		{
			base.Type = MetadataFieldType.String;
		}

		public override void Check()
		{
			base.Check();
			List<Type> allowedValidationTypes = new List<Type>
			{
				typeof(StringLengthValidationParams),
				typeof(AndValidationParams)
			};
			CheckScalarDataModel(allowedValidationTypes);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			if (base.DefaultValue != null)
			{
				dict.Add("default_value", base.DefaultValue);
			}
		}
	}
}
