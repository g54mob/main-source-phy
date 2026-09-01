using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class ListResourcesByTagParams : ListResourcesParams
	{
		public string Tag { get; set; }

		public override void Check()
		{
			base.Check();
			if (string.IsNullOrEmpty(Tag))
			{
				throw new ArgumentException("Tag must be set to filter resources by tag!");
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
