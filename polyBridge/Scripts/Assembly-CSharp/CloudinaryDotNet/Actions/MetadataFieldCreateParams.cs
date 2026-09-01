using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public abstract class MetadataFieldCreateParams<T> : MetadataFieldBaseParams<T>
	{
		protected MetadataFieldCreateParams(string label)
		{
			base.Label = label;
		}

		public override void Check()
		{
			Utils.ShouldBeSpecified(() => Label);
			if (base.Mandatory)
			{
				Utils.ShouldBeSpecified(() => DefaultValue);
			}
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			BaseParams.AddParam(dict, "label", base.Label);
		}
	}
}
