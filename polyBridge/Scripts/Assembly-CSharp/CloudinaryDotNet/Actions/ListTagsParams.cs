using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class ListTagsParams : BaseParams
	{
		public ResourceType ResourceType { get; set; }

		public string Prefix { get; set; }

		public int MaxResults { get; set; }

		public string NextCursor { get; set; }

		public ListTagsParams()
		{
			NextCursor = string.Empty;
			Prefix = string.Empty;
		}

		public override void Check()
		{
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			if (MaxResults > 0)
			{
				BaseParams.AddParam(sortedDictionary, "max_results", MaxResults.ToString(CultureInfo.InvariantCulture));
			}
			BaseParams.AddParam(sortedDictionary, "next_cursor", NextCursor);
			BaseParams.AddParam(sortedDictionary, "prefix", Prefix);
			return sortedDictionary;
		}
	}
}
