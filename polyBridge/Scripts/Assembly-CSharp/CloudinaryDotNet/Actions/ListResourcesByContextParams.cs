using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class ListResourcesByContextParams : ListResourcesParams
	{
		public string Key { get; set; }

		public string Value { get; set; }

		public override void Check()
		{
			if (string.IsNullOrEmpty(Key))
			{
				throw new InvalidOperationException("Key must be set to list resources by context.");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "key", Key);
			BaseParams.AddParam(sortedDictionary, "value", Value);
			return sortedDictionary;
		}
	}
}
