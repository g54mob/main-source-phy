using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class DeleteTransformParams : BaseParams
	{
		public string Transformation { get; set; }

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
			BaseParams.AddParam(sortedDictionary, "transformation", Transformation);
			return sortedDictionary;
		}
	}
}
