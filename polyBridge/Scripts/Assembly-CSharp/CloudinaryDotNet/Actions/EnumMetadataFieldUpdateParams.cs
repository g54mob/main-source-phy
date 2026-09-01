using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class EnumMetadataFieldUpdateParams : MetadataFieldUpdateParams<string>
	{
		public EnumMetadataFieldUpdateParams()
		{
			base.Type = MetadataFieldType.Enum;
		}

		public override void Check()
		{
			base.Check();
			base.DataSource?.Check();
			Utils.ShouldNotBeSpecified(() => Validation);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			if (base.DefaultValue != null)
			{
				BaseParams.AddParam(dict, "default_value", base.DefaultValue);
			}
		}
	}
}
