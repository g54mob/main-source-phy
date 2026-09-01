using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public abstract class BaseSubAccountParams : BaseParams
	{
		public string Name { get; set; }

		public string CloudName { get; set; }

		public StringDictionary CustomAttributes { get; set; }

		public bool? Enabled { get; set; }

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			if (Enabled.HasValue)
			{
				BaseParams.AddParam(dict, "enabled", Enabled.Value);
			}
			if (!string.IsNullOrEmpty(Name))
			{
				BaseParams.AddParam(dict, "name", Name);
			}
			if (!string.IsNullOrEmpty(CloudName))
			{
				BaseParams.AddParam(dict, "cloud_name", CloudName);
			}
			if (CustomAttributes != null)
			{
				dict.Add("custom_attributes", Utils.SafeJoin("|", CustomAttributes.SafePairs));
			}
		}
	}
}
