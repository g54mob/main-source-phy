using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class ListResourcesByModerationParams : ListResourcesParams
	{
		public string ModerationKind { get; set; }

		public ModerationStatus ModerationStatus { get; set; }

		public override void Check()
		{
			base.Check();
			if (string.IsNullOrEmpty(ModerationKind))
			{
				throw new ArgumentException("ModerationKind must be set to filter resources by moderation kind/status!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			if (sortedDictionary.ContainsKey("type"))
			{
				sortedDictionary.Remove("type");
			}
			return sortedDictionary;
		}
	}
}
