using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class ListSpecificResourcesParams : ListResourcesParams
	{
		public List<string> PublicIds { get; set; }

		public ListSpecificResourcesParams()
		{
			PublicIds = new List<string>();
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			if (PublicIds != null && PublicIds.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "public_ids", PublicIds);
				if (sortedDictionary.ContainsKey("direction"))
				{
					sortedDictionary.Remove("direction");
				}
			}
			return sortedDictionary;
		}
	}
}
