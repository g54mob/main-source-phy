using System;
using System.Collections.Generic;
using System.Globalization;

namespace CloudinaryDotNet.Actions
{
	public class GetTransformParams : BaseParams
	{
		public string Transformation { get; set; }

		public int MaxResults { get; set; }

		public string NextCursor { get; set; }

		public string Format { get; set; }

		public GetTransformParams()
		{
			Transformation = string.Empty;
		}

		public override void Check()
		{
			if (string.IsNullOrEmpty(Transformation))
			{
				throw new ArgumentException("Transformation must be set!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			if (MaxResults > 0)
			{
				BaseParams.AddParam(sortedDictionary, "max_results", MaxResults.ToString(CultureInfo.InvariantCulture));
			}
			BaseParams.AddParam(sortedDictionary, "next_cursor", NextCursor);
			BaseParams.AddParam(sortedDictionary, "transformation", (Format != null) ? (Transformation + "/" + Format) : Transformation);
			return sortedDictionary;
		}
	}
}
