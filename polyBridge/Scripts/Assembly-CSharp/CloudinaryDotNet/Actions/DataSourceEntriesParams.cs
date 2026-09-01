using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class DataSourceEntriesParams : BaseParams
	{
		public List<string> ExternalIds { get; set; }

		public DataSourceEntriesParams(List<string> externalIds)
		{
			ExternalIds = externalIds;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => ExternalIds);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			BaseParams.AddParam(dict, "external_ids", ExternalIds);
		}
	}
}
