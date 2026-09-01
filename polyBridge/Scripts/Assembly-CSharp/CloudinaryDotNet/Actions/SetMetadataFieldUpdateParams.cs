using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class SetMetadataFieldUpdateParams : MetadataFieldUpdateParams<List<string>>
	{
		public SetMetadataFieldUpdateParams()
		{
			base.Type = MetadataFieldType.Set;
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
