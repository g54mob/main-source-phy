using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class DeletionParams : BaseParams
	{
		public string PublicId { get; set; }

		public string Type { get; set; }

		public bool Invalidate { get; set; }

		public ResourceType ResourceType { get; set; }

		public DeletionParams(string publicId)
		{
			Type = "upload";
			ResourceType = ResourceType.Image;
			PublicId = publicId;
		}

		public override void Check()
		{
			if (string.IsNullOrEmpty(PublicId))
			{
				throw new ArgumentException("PublicId must be specified in UploadParams!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "public_id", PublicId);
			BaseParams.AddParam(sortedDictionary, "type", Type);
			BaseParams.AddParam(sortedDictionary, "invalidate", Invalidate);
			return sortedDictionary;
		}
	}
}
