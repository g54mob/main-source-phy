using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class ContextParams : BaseParams
	{
		public List<string> PublicIds { get; set; }

		public string Context { get; set; }

		public StringDictionary ContextDict { get; set; }

		public string Type { get; set; }

		public ContextCommand Command { get; set; }

		public ResourceType ResourceType { get; set; }

		public override void Check()
		{
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			List<string> list = new List<string>();
			if (ContextDict?.SafePairs != null)
			{
				list.AddRange(ContextDict.SafePairs);
			}
			if (!string.IsNullOrEmpty(Context))
			{
				list.Add(Context);
			}
			if (list.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "context", Utils.SafeJoin("|", list));
			}
			BaseParams.AddParam(sortedDictionary, "public_ids", PublicIds);
			BaseParams.AddParam(sortedDictionary, "command", ApiShared.GetCloudinaryParam(Command));
			BaseParams.AddParam(sortedDictionary, "resource_type", ApiShared.GetCloudinaryParam(ResourceType));
			return sortedDictionary;
		}
	}
}
