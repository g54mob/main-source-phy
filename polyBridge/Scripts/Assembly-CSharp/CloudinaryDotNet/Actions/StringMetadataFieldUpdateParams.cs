using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class StringMetadataFieldUpdateParams : MetadataFieldUpdateParams<string>
	{
		public StringMetadataFieldUpdateParams()
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
