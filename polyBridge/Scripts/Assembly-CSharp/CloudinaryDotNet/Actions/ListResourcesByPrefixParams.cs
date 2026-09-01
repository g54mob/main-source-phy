using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class ListResourcesByPrefixParams : ListResourcesParams
	{
		public string Prefix { get; set; }

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "prefix", Prefix);
			return sortedDictionary;
		}
	}
}
