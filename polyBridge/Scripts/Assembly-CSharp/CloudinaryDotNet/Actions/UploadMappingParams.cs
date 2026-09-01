using System;
using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class UploadMappingParams : BaseParams
	{
		public string NextCursor { get; set; }

		public int MaxResults { get; set; }

		public string Folder { get; set; }

		public string Template { get; set; }

		public override void Check()
		{
			if (MaxResults > 500)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The maximal count of folders to return is 500, but {0} given!", MaxResults));
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "folder", Folder);
			BaseParams.AddParam(sortedDictionary, "template", Template);
			if (MaxResults > 0)
			{
				BaseParams.AddParam(sortedDictionary, "max_results", MaxResults);
			}
			if (!string.IsNullOrEmpty(NextCursor))
			{
				BaseParams.AddParam(sortedDictionary, "next_cursor", NextCursor);
			}
			return sortedDictionary;
		}
	}
}
