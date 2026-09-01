using System.Collections.Generic;
using System.Linq;

namespace CloudinaryDotNet.Actions
{
	public class ListSubAccountsParams : BaseParams
	{
		public bool? Enabled { get; set; }

		public List<string> Ids { get; set; }

		public string Prefix { get; set; }

		public ListSubAccountsParams()
		{
			Ids = new List<string>();
		}

		public override void Check()
		{
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			if (Enabled.HasValue)
			{
				BaseParams.AddParam(dict, "enabled", Enabled.Value);
			}
			if (Ids != null && Ids.Any())
			{
				BaseParams.AddParam(dict, "ids", Ids);
			}
			if (!string.IsNullOrEmpty(Prefix))
			{
				BaseParams.AddParam(dict, "prefix", Prefix);
			}
		}
	}
}
