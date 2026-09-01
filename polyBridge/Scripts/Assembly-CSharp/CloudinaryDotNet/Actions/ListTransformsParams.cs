using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class ListTransformsParams : BaseParams
	{
		public int MaxResults { get; set; }

		public bool? Named { get; set; }

		public string NextCursor { get; set; }

		public ListTransformsParams()
		{
			NextCursor = string.Empty;
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
			if (Named.HasValue)
			{
				BaseParams.AddParam(sortedDictionary, "named", string.Format(CultureInfo.InvariantCulture, "{0}", Named.Value));
			}
			if (!string.IsNullOrWhiteSpace(NextCursor))
			{
				BaseParams.AddParam(sortedDictionary, "next_cursor", NextCursor);
			}
			return sortedDictionary;
		}
	}
}
