using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class ExplodeParams : BaseParams
	{
		public string PublicId { get; set; }

		public Transformation Transformation { get; set; }

		public string NotificationUrl { get; set; }

		public string Format { get; set; }

		public AssetType Type { get; set; }

		public ExplodeParams(string publicId, Transformation transformation)
		{
			PublicId = publicId;
			Transformation = transformation;
		}

		public override void Check()
		{
			if (string.IsNullOrEmpty(PublicId))
			{
				throw new ArgumentException("PublicId must be set!");
			}
			if (Transformation == null)
			{
				throw new ArgumentException("Transformation must be set!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "public_id", PublicId);
			BaseParams.AddParam(sortedDictionary, "notification_url", NotificationUrl);
			BaseParams.AddParam(sortedDictionary, "format", Format);
			BaseParams.AddParam(sortedDictionary, "type", ApiShared.GetCloudinaryParam(Type));
			if (Transformation != null)
			{
				BaseParams.AddParam(sortedDictionary, "transformation", Transformation.Generate());
			}
			return sortedDictionary;
		}
	}
}
