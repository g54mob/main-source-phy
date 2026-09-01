using System.Collections.Generic;
using System.Linq;

namespace CloudinaryDotNet.Actions
{
	public abstract class BaseUserParams : BaseParams
	{
		public string Name { get; set; }

		public string Email { get; set; }

		public Role? Role { get; set; }

		public List<string> SubAccountIds { get; set; }

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			if (!string.IsNullOrEmpty(Name))
			{
				BaseParams.AddParam(dict, "name", Name);
			}
			if (!string.IsNullOrEmpty(Email))
			{
				BaseParams.AddParam(dict, "email", Email);
			}
			if (Role.HasValue)
			{
				BaseParams.AddParam(dict, "role", ApiShared.GetCloudinaryParam(Role.Value));
			}
			if (SubAccountIds != null && SubAccountIds.Any())
			{
				BaseParams.AddParam(dict, "sub_account_ids", SubAccountIds);
			}
		}
	}
}
