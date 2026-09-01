using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public abstract class MetadataFieldUpdateParams<T> : MetadataFieldBaseParams<T>
	{
		public override void Check()
		{
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			if (!string.IsNullOrEmpty(base.Label))
			{
				BaseParams.AddParam(dict, "label", base.Label);
			}
		}
	}
}
