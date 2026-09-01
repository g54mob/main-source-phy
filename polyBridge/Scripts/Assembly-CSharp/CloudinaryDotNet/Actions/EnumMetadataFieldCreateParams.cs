using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class EnumMetadataFieldCreateParams : MetadataFieldCreateParams<string>
	{
		public EnumMetadataFieldCreateParams(string label)
			: base(label)
		{
			base.Type = MetadataFieldType.Enum;
		}

		public override void Check()
		{
			base.Check();
			Utils.ShouldBeSpecified(() => DataSource);
			Utils.ShouldNotBeSpecified(() => Validation);
			base.DataSource.Check();
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
