using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class SetMetadataFieldCreateParams : MetadataFieldCreateParams<List<string>>
	{
		public SetMetadataFieldCreateParams(string label)
			: base(label)
		{
			base.Type = MetadataFieldType.Set;
		}

		public override void Check()
		{
			base.Check();
			if (base.Mandatory)
			{
				Utils.ShouldNotBeEmpty(() => DefaultValue);
			}
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
