using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class IntMetadataFieldUpdateParams : MetadataFieldUpdateParams<int?>
	{
		public IntMetadataFieldUpdateParams()
		{
			base.Type = MetadataFieldType.Integer;
		}

		public override void Check()
		{
			base.Check();
			List<Type> allowedValidationTypes = new List<Type>
			{
				typeof(IntLessThanValidationParams),
				typeof(IntGreaterThanValidationParams),
				typeof(AndValidationParams)
			};
			CheckScalarDataModel(allowedValidationTypes);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			if (base.DefaultValue.HasValue)
			{
				dict.Add("default_value", base.DefaultValue.Value);
			}
		}
	}
}
