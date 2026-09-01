using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class CreateSubAccountParams : BaseSubAccountParams
	{
		public string BaseSubAccountId { get; set; }

		public CreateSubAccountParams(string subAccountName)
		{
			base.Name = subAccountName;
			base.Enabled = true;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => Name);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			if (!string.IsNullOrEmpty(BaseSubAccountId))
			{
				BaseParams.AddParam(dict, "base_sub_account_id", BaseSubAccountId);
			}
		}
	}
}
