using System;
using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class ListResourcesParams : BaseParams
	{
		public ResourceType ResourceType { get; set; }

		public string Type { get; set; }

		public int MaxResults { get; set; }

		public bool Tags { get; set; }

		public bool Moderations { get; set; }

		public bool Context { get; set; }

		public bool Metadata { get; set; }

		public string NextCursor { get; set; }

		public string Direction { get; set; }

		public DateTime StartAt { get; set; }

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
			BaseParams.AddParam(sortedDictionary, "start_at", StartAt);
			BaseParams.AddParam(sortedDictionary, "next_cursor", NextCursor);
			BaseParams.AddParam(sortedDictionary, "tags", Tags);
			BaseParams.AddParam(sortedDictionary, "moderations", Moderations);
			BaseParams.AddParam(sortedDictionary, "context", Context);
			BaseParams.AddParam(sortedDictionary, "direction", Direction);
			BaseParams.AddParam(sortedDictionary, "type", Type);
			BaseParams.AddParam(sortedDictionary, "metadata", Metadata);
			return sortedDictionary;
		}
	}
}
