using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class EntryParams : BaseParams
	{
		public string ExternalId { get; set; }

		public string Value { get; set; }

		public EntryParams(string value)
		{
			Value = value;
		}

		public EntryParams(string value, string externalId)
		{
			ExternalId = externalId;
			Value = value;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => Value);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			dict.Add("value", Value);
			if (!string.IsNullOrEmpty(ExternalId))
			{
				dict.Add("external_id", ExternalId);
			}
		}
	}
}
