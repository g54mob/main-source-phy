using System.Collections.Generic;
using System.Linq;

namespace CloudinaryDotNet.Actions
{
	public class ListUsersParams : BaseParams
	{
		public bool? Pending { get; set; }

		public List<string> UserIds { get; set; }

		public string Prefix { get; set; }

		public string SubAccountId { get; set; }

		public override void Check()
		{
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			if (Pending.HasValue)
			{
				BaseParams.AddParam(dict, "pending", Pending.Value);
			}
			if (UserIds != null && UserIds.Any())
			{
				BaseParams.AddParam(dict, "ids", UserIds);
			}
			if (!string.IsNullOrEmpty(Prefix))
			{
				BaseParams.AddParam(dict, "prefix", Prefix);
			}
			if (!string.IsNullOrEmpty(SubAccountId))
			{
				BaseParams.AddParam(dict, "sub_account_id", SubAccountId);
			}
		}
	}
}
