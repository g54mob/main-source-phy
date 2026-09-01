using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class RenameParams : BaseParams
	{
		public string FromPublicId { get; set; }

		public string ToPublicId { get; set; }

		public ResourceType ResourceType { get; set; }

		public string Type { get; set; }

		public string ToType { get; set; }

		public bool Overwrite { get; set; }

		public bool Invalidate { get; set; }

		public bool Context { get; set; }

		public bool Metadata { get; set; }

		public RenameParams(string fromPublicId, string toPublicId)
		{
			FromPublicId = fromPublicId;
			ToPublicId = toPublicId;
			ResourceType = ResourceType.Image;
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "from_public_id", FromPublicId);
			BaseParams.AddParam(sortedDictionary, "to_public_id", ToPublicId);
			BaseParams.AddParam(sortedDictionary, "overwrite", Overwrite);
			BaseParams.AddParam(sortedDictionary, "type", Type);
			BaseParams.AddParam(sortedDictionary, "to_type", ToType);
			BaseParams.AddParam(sortedDictionary, "invalidate", Invalidate);
			if (Context)
			{
				BaseParams.AddParam(sortedDictionary, "context", Context);
			}
			if (Metadata)
			{
				BaseParams.AddParam(sortedDictionary, "metadata", Metadata);
			}
			return sortedDictionary;
		}

		public override void Check()
		{
			if (string.IsNullOrEmpty(FromPublicId))
			{
				throw new ArgumentException("FromPublicId can't be null!");
			}
			if (string.IsNullOrEmpty(ToPublicId))
			{
				throw new ArgumentException("ToPublicId can't be null!");
			}
		}
	}
}
